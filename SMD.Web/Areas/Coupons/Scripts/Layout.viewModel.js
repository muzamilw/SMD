define("Layout/Layout.viewModel",
    ["jquery", "amplify", "ko", "Layout/Layout.dataService", "Layout/Layout.model", "common/confirmation.viewModel"],

    function ($, amplify, ko, dataService, model, confirmation) {

        var ist = window.ist || {};
        ist.Layout = {
            viewModel: (function () {
                var // The view 
                   view,

                branchCategory = ko.observableArray([]),
                selectedBranch = ko.observable(),
                branchDdlist = ko.observableArray([]),
                selectedCategory = ko.observable(),
                selectedBranchField = ko.observable(),
                isSaveChangesEnable = ko.observable(false),
                counter = ko.observable(0),
                defaultOpenBranchFieldName = ko.observable(),
                viewBranchDialog = function () {
                    view.showBranchCategoryDialog()
                },
                getBranchCategories = function (callback) {
                    dataService.getBranchCategory({
                        success: function (data) {
                            branchCategory.removeAll();
                            branchDdlist.removeAll();
                            _.each(data, function (item) {

                                var category = new model.BranchCategory.Create(item);
                                branchDdlist.push(category);
                                _.each(item.CompanyBranches, function (branchFieldItem) {
                                    var branchField = new model.BranchField.Create(branchFieldItem);
                                    category.brachFeilds.push(branchField);
                                });
                                branchCategory.push(category);

                            });
                            if (callback && typeof callback === "function") {
                                callback();
                            }
                        },
                        error: function () {
                            toastr.error("Failed to load branchCategory.");
                        }
                    });
                },
                getBranchesByBranchFieldId = function (branchId, resetTreeExpensionAfterSave1) {

                    dataService.getBranchByBranchId(
                        { branchId: branchId },
                        {
                            success: function (data) {
                                var branch = new model.Branch.Create(data);
                                selectedBranch(branch);
                                isSaveChangesEnable(true);

                                //}

                            },
                            error: function () {
                                toastr.error("Failed to load Branch.");
                            }
                        });

                },
                SaveChanges = function () {
                    dataService.SaveCompanyBranch(

                             selectedBranch().convertToServerData(), {
                                 success: function (data) {
                                     if (data > 0) {
                                         selectedBranch().branchId(data);
                                         _.each(branchCategory(), function (item) {
                                             if (item.categoryId() == selectedBranch().branchCategoryId()) {
                                                 item.brachFeilds.push(selectedBranch());
                                                 selectedBranch(null);
                                                 isSaveChangesEnable(false);

                                             }
                                         })
                                     }
                                     else {
                                         _.each(selectedCategory().brachFeilds(), function (item) {
                                             if (item.branchId() == selectedBranch().branchId()) {
                                                 item.branchTitle(selectedBranch().branchTitle());
                                                 selectedBranch(null);

                                             }
                                         })

                                     }

                                 },
                                 error: function (response) {
                                     toastr.error("Failed to Save . Error: " + response);
                                 }
                             });
                },
                SaveCategory = function () {

                    dataService.SaveCategory(

                         selectedCategory().convertToServerData(), {
                             success: function (id) {
                                 if (id > 1) {
                                     selectedCategory().isEditMode(false);
                                     selectedCategory().isVisibleDelete(true);
                                 }
                                 else {
                                     selectedCategory().isEditMode(false);
                                 }

                             },
                             error: function (response) {
                                 toastr.error("Failed to Save . Error: " + response);
                             }
                         });



                },
                 changeIcon = function (event) {
                     if (event.target.classList.contains("fa-chevron-circle-right")) {
                         event.target.classList.remove("fa-chevron-circle-right");
                         event.target.classList.add("fa-chevron-circle-down");
                     } else {
                         event.target.classList.remove("fa-chevron-circle-down");
                         event.target.classList.add("fa-chevron-circle-right");
                         selectedCategory().isExpanded(false);
                         isSaveChangesEnable(false);
                         selectedBranch(null);

                     }
                 },
                AddNewCategory = function () {
                    counter(counter() - 1);
                    var newCategory = new model.BranchCategory.Create({});
                    newCategory.categoryId(counter());
                    newCategory.name("New Category");
                    newCategory.isEditMode(true);
                    newCategory.isExpanded(false);
                    newCategory.isVisibleDelete(false);
                    selectedCategory(newCategory)
                    _.each(branchCategory(), function (item) {
                        if (item.categoryId() != selectedCategory().categoryId()) {
                            item.isEditMode(false);
                        }

                    });
                    branchCategory().push(selectedCategory());
                    branchCategory.valueHasMutated();

                },
                DeleteBranch = function () {

                    confirmation.messageText("Do you want to delete Branch?");
                    if (selectedBranch() != undefined) {
                        confirmation.show();

                    }

                    confirmation.afterCancel(function () {
                        confirmation.hide();
                    });
                    confirmation.afterProceed(function () {
                        dataService.DeleteCurrentBranch(
                                 selectedBranch().convertToServerData(), {
                                     success: function (data) {

                                         _.each(selectedCategory().brachFeilds(), function (item) {
                                             if (item.branchId() == selectedBranch().branchId()) {
                                                 selectedCategory().brachFeilds.remove(item);
                                                 selectedCategory().isExpanded(false);
                                                 selectedBranch(null);
                                                 isSaveChangesEnable(false);


                                             }
                                         })
                                     },
                                     error: function (response) {
                                         toastr.error("Failed to Delete Branch . Error: " + response);
                                     }
                                 });
                    });

                },
                DeleteCategory = function (category) {
                    confirmation.messageText("Please delete or move all branches from this category to delete it.");
                    confirmation.afterProceed(function () {
                        selectedCategory(category);
                        dataService.DeleteCurrentCategory(
                                    category.convertToServerData(), {
                                        success: function (data) {
                                            if (data == true)
                                                branchCategory.remove(selectedCategory());
                                        },
                                        error: function (response) {
                                            toastr.error("Failed to Delete Category . Error: " + response);
                                        }
                                    });
                    });

                    confirmation.afterCancel(function () {
                        confirmation.hide();
                    });
                    confirmation.show();
                },
                EditCategory = function (category) {
                    selectedCategory(category)
                    selectedCategory().isEditMode(true);
                    selectedCategory().isExpanded(false);
                    _.each(branchCategory(), function (item) {
                        if (item.categoryId() != selectedCategory().categoryId()) {
                            item.isEditMode(false);
                        }

                    });


                },
                CreateNewBranchLocation = function () {

                    var newBranchLocation = model.Branch({});
                    selectedBranch(undefined)
                    selectedBranch(newBranchLocation);
                },
                resetTreeExpensionAfterSave = function (category) {
                    category.isExpanded(true);
                },
                showBranchDialoge = function () {
                    getBranchCategories(viewBranchDialog);
                },
                hideBranchCategoryDialog = function () {

                    if (selectedBranch().hasChanges()) {
                        confirmation.messageText("Do you want to save changes?");
                        
                    }
                    confirmation.afterCancel(function () {
                        view.hideBranchCategoryDialog();
                        selectedBranch(null);
                        isSaveChangesEnable(false);
                        confirmation.hide();
                        
                    });
                    confirmation.afterProceed(function () {
                        SaveChanges();
                        view.hideBranchCategoryDialog();
                        selectedBranch(null);
                        isSaveChangesEnable(false);

                    });
                    confirmation.show();
                    //view.hideBranchCategoryDialog();
                    //selectedBranch(null);
                    //isSaveChangesEnable(false);

                },
                resetTreeExpension = function (category) {
                    //old menu collapse
                    if (selectedCategory() !== undefined) {
                        selectedCategory().isExpanded(false);
                    }
                    //new selected section expand
                    category.isExpanded(true);
                    selectedCategory(category);
                    //  selectedBranchField(undefined);
                    // branches.removeAll();

                    if (category.brachFeilds().length > 0) {


                        if (defaultOpenBranchFieldName() !== undefined) {
                            var defaultBranchFieldOpenCategory = category.brachFeilds().find(function (brachFeilds) {
                                return brachFeilds.branchTitle() === defaultOpenBranchFieldName();
                            });


                            selectbranchField(defaultBranchFieldOpenCategory);
                            //defaultOpenBranchFieldName(undefined);
                        }
                        else {
                            selectedBranchField(category.brachFeilds()[0]);
                        }
                    }
                },
                selectbranchField = function (brachFeilds) {
                    // branches.removeAll();
                    selectedBranchField(brachFeilds);
                    getBranchesByBranchFieldId(brachFeilds.branchId());

                },
                selectCategory = function (category, event) {

                    branchCategory()[0].isEditMode(false);

                    if (category.brachFeilds().length > 0) {
                        resetTreeExpension(category);
                    } else {
                        getBranchFields(category);
                    }
                    _.each(branchCategory(), function (item) {
                        if (item.categoryId() != selectedCategory().categoryId()) {
                            item.isEditMode(false);
                        }

                    });
                    changeIcon(event);

                },
                getBranchFields = function (category, afterSaveRefreshListFlag) {
                    dataservice.getBranchFiledsByCategoryID({
                        categoryId: category.categoryId(),
                    }, {
                        success: function (data) {
                            if (data != null) {
                                category.branchFields.removeAll();
                                _.each(data, function (Item) {
                                    var branchfield1 = new model.BranchField.Create(Item);
                                    category.branchFields.push(phraseField1);
                                });



                                // true, Refresh The Phrase Fields as well as Phrases
                                if (afterSaveRefreshListFlag) {
                                    alert("test");

                                }
                                else {
                                    resetTreeExpension(category);
                                }

                            }
                        },
                        error: function (response) {
                            toastr.error("Failed to load Branch Fileds . Error: ");
                        }
                    });
                },



                // Initialize the view model
                 initialize = function (specifiedView) {
                     view = specifiedView;
                     ko.applyBindings(view.viewModel, view.bindingRoot);
                     ko.applyBindings(view.viewModel, view.bindingPartial);
                 };
                return {

                    initialize: initialize,
                    selectedCategory: selectedCategory,
                    selectedBranchField: selectedBranchField,
                    selectCategory: selectCategory,
                    selectbranchField: selectbranchField,
                    defaultOpenBranchFieldName: defaultOpenBranchFieldName,
                    showBranchDialoge: showBranchDialoge,
                    getBranchCategories: getBranchCategories,
                    selectedBranch: selectedBranch,
                    branchCategory: branchCategory,
                    AddNewCategory: AddNewCategory,
                    SaveChanges: SaveChanges,
                    CreateNewBranchLocation: CreateNewBranchLocation,
                    DeleteBranch: DeleteBranch,
                    EditCategory: EditCategory,
                    SaveCategory: SaveCategory,
                    DeleteCategory: DeleteCategory,
                    isSaveChangesEnable: isSaveChangesEnable,
                    hideBranchCategoryDialog: hideBranchCategoryDialog,
                    branchDdlist: branchDdlist
                };

            })()
        };

        return ist.Layout.viewModel;

    });


