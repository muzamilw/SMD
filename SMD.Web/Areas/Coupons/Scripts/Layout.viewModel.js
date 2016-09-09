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
                countries = ko.observableArray([]),
                afterBranchSelect = null,
                selectedBranchField = ko.observable(),
                isSaveChangesEnable = ko.observable(false),
                isMapVisible = ko.observable(false);
                isdeleteEnable = ko.observable(true),
                isCodeAddressEdit = ko.observable(false),
                count = ko.observable(0),
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
                                isdeleteEnable(true);
                                isCodeAddressEdit(true)
                                initializeGEO();
                                codeAddress();
                                isMapVisible(true);

                                //}

                            },
                            error: function () {
                                toastr.error("Failed to load Branch.");
                            }
                        });

                },
                SaveChanges = function () {

                    if (doBeforeSave()) {
                        dataService.SaveCompanyBranch(

                                              selectedBranch().convertToServerData(), {
                                                  success: function (data) {
                                                      if (data > 0) {
                                                          selectedBranch().branchId(data);
                                                          _.each(branchCategory(), function (item) {
                                                              if (item.categoryId() == selectedBranch().branchCategoryId()) {
                                                                  item.brachFeilds.push(selectedBranch());
                                                                  isSaveChangesEnable(false);
                                                                  isdeleteEnable(false);
                                                                  isMapVisible(false);
                                                                  toastr.success("Successfully saved.");

                                                              }
                                                          })

                                                      }
                                                      else {
                                                          _.each(selectedCategory().brachFeilds(), function (item) {
                                                              if (item.branchId() == selectedBranch().branchId()) {
                                                                  item.branchTitle(selectedBranch().branchTitle());
                                                                  //  selectedBranch(null);
                                                                  toastr.success("Successfully updated.");
                                                                  selectedCategory().isExpanded(false);
                                                                  isSaveChangesEnable(false);
                                                                  isdeleteEnable(false);
                                                                  isMapVisible(false);

                                                              }
                                                          })
                                                      }
                                                      selectedBranch(null);
                                                      isCodeAddressEdit(false);
                                                      if (afterBranchSelect && typeof afterBranchSelect === "function") {
                                                         
                                                          afterBranchSelect(data);
                                                      }

                                                  },
                                                  error: function (response) {
                                                      toastr.error("Failed to Save . Error: " + response);
                                                  }
                                              });

                    }
                    else {
                        toastr.warning("please click on 'Locate on Map' for Map: ");
                        return;
                    }



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
                                 toastr.success("Successfully saved.");
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
                                         //if (callback && typeof callback === "function") {
                                         //    callback();
                                         //}

                                     },
                                     error: function () {
                                         toastr.error("Failed to load branchCategory.");
                                     }
                                 });

                             },
                             error: function (response) {
                                 toastr.error("Failed to Save . Error: " + response);
                             }
                         });



                },
                doBeforeSave = function () {
                    var flag = true;
                    if (!selectedBranch().isValid()) {
                        selectedBranch().showAllErrors();
                        flag = false;
                    }
                    return flag;
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
                         isdeleteEnable(false);
                         selectedBranch(null);

                     }
                 },
                AddNewCategory = function () {
                    count(count() - 1);
                    var newCategory = new model.BranchCategory.Create({});
                    newCategory.categoryId(count());
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
                                                 isSaveChangesEnable(false);
                                                 isdeleteEnable(false);


                                             }
                                         })
                                         selectedBranch(null);
                                         if (afterBranchSelect && typeof afterBranchSelect === "function") {
                                             afterBranchSelect();
                                         }
                                     },
                                     error: function (response) {
                                         toastr.error("Failed to Delete Branch . Error: " + response);
                                     }
                                 });
                    });

                },
                DeleteCategory = function (category) {
                    if (category.brachFeilds().length > 0) {
                        confirmation.showOKpopup();

                    }
                    else {

                        confirmation.messageText("Do you want to delete Branch?");
                        confirmation.show();

                    }
                    confirmation.afterProceed(function () {
                        if (category.brachFeilds().length > 0) {
                            return;
                        }
                        else {
                            selectedCategory(category);
                            dataService.DeleteCurrentCategory(
                                        category.convertToServerData(), {
                                            success: function (data) {
                                                if (data == true)
                                                    branchCategory.remove(selectedCategory());
                                                udateCategoryDropDown();

                                            },
                                            error: function (response) {
                                                toastr.error("Failed to Delete Category . Error: " + response);
                                            }
                                        });
                        }

                    });

                    confirmation.afterCancel(function () {
                        confirmation.hide();
                    });

                },
                udateCategoryDropDown = function () {
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
                            //if (callback && typeof callback === "function") {
                            //    callback();
                            //}

                        },
                        error: function () {
                            toastr.error("Failed to load branchCategory.");
                        }
                    });

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
                  isAddressFilled = ko.computed(function () {
                      if (selectedBranch() != undefined && selectedBranch() != null) {
                          if ((selectedBranch().branchAddressline1() == undefined || selectedBranch().branchAddressline1() == "") || (selectedBranch().branchCity() == undefined || selectedBranch().branchCity() == "") || (selectedBranch().branchState() == undefined || selectedBranch().branchState() == "")) {
                              return false;
                          }
                          else {
                              isMapVisible(true);
                              return true;
                          }


                      }
                      else {

                          return false;
                      }


                  }),
                CreateNewBranchLocation = function () {

                    var newBranchLocation = model.Branch({});
                    selectedBranch(undefined)
                    selectedBranch(newBranchLocation);
                    isSaveChangesEnable(true);
                    isdeleteEnable(false);
                    isMapVisible(true);
                },
                resetTreeExpensionAfterSave = function (category) {
                    category.isExpanded(true);
                },
                showBranchDialoge = function (callback) {
                    afterBranchSelect = callback;
                    getBranchCategories(viewBranchDialog);
                    //getAllCountries();
                },
                hideBranchCategoryDialog = function () {

                    if (selectedBranch() == undefined || selectedBranch() == null) {
                        view.hideBranchCategoryDialog();
                        selectedBranch(null);
                        isSaveChangesEnable(false);
                        isdeleteEnable(false);
                        isMapVisible(false);
                    }

                    else if (selectedBranch() != null && selectedBranch() != undefined && selectedBranch().hasChanges()) {
                        confirmation.messageText("Do you want to save changes?");
                        confirmation.show();

                    }
                    else {
                        view.hideBranchCategoryDialog();
                        selectedBranch(null);
                        isSaveChangesEnable(false);
                        isdeleteEnable(false);
                        isMapVisible(false);

                    }
                    confirmation.afterCancel(function () {
                        view.hideBranchCategoryDialog();
                        selectedBranch(null);
                        isSaveChangesEnable(false);
                        isdeleteEnable(false);
                        isMapVisible(false);
                        confirmation.hide();


                    });
                    confirmation.afterProceed(function () {
                        SaveChanges();
                        view.hideBranchCategoryDialog();
                        selectedBranch(null);
                        isSaveChangesEnable(false);
                        isdeleteEnable(false);

                    });

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
                  getAllCountries = function () {
                      dataService.getAllCountries({
                          success: function (data) {
                              _.each(data, function (Item) {
                                  countries.push(Item);
                              });

                          },
                          error: function () {
                              toastr.error("Failed to load Countries.");
                          }
                      });
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
                CodeAddressonMap = function () {
                    initializeGEO();
                    codeAddress();
                    google.maps.event.addDomListener(window, 'load', initializeGEO);
                },
               initializeGEO = function () {
                   geocoder = new google.maps.Geocoder();
                   var latlng = new google.maps.LatLng(-34.397, 150.644);
                   var mapOptions = {
                       zoom: 15,
                       center: latlng,
                       mapTypeId: google.maps.MapTypeId.ROADMAP
                   }
                   map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

               }
                codeAddress = function () {

                    // var address = selectedBranch().branchAddressline1().toLowerCase() + ',' + selectedBranch().branchCity().toLowerCase() + ',' + selectedBranch().branchZipCode() + ',' + selectedBranch().branchState().toLowerCase();
                    var address = selectedBranch().branchAddressline1().toLowerCase() + ' ' + selectedBranch().branchCity().toLowerCase() + ' ' + selectedBranch().branchZipCode() + ' ' + selectedBranch().branchState().toLowerCase();
                    geocoder.geocode({
                        'address': address
                    }, function (results, status) {
                        if (status == google.maps.GeocoderStatus.OK) {
                            if (isCodeAddressEdit() == false) {
                                selectedBranch().branchLocationLat(results[0].geometry.location.lat());
                                selectedBranch().branchLocationLon(results[0].geometry.location.lng());
                            }
                            map.setCenter(results[0].geometry.location);

                            var marker = new google.maps.Marker({
                                map: map,
                                position: results[0].geometry.location
                            });
                            google.maps.event.addListener(map, 'click', function (event) {
                                selectedBranch().branchLocationLat(event.latLng.lat());
                                selectedBranch().branchLocationLon(event.latLng.lng());
                                var geocoder = new google.maps.Geocoder();
                                geocoder.geocode({
                                    "latLng": event.latLng
                                }, function (results, status) {
                                    console.log(results, status);
                                    if (status == google.maps.GeocoderStatus.OK) {
                                        console.log(results);
                                        var lat = results[0].geometry.location.lat(),
                                            lng = results[0].geometry.location.lng(),
                                            placeName = results[0].address_components[0].long_name,
                                            latlng = new google.maps.LatLng(lat, lng);

                                        moveMarker(placeName, latlng);
                                    }
                                });
                            });
                            function moveMarker(placeName, latlng) {
                                //marker.setIcon(image);
                                marker.setPosition(latlng);
                                //infowindow.setContent(placeName);
                                //infowindow.open(map, marker);
                            }
                            isCodeAddressEdit(false);
                        } else {
                            toastr.error("Failed to Search Address,please add valid address and search it . Error: " + status);
                            isMapVisible(false);
                            //alert('Geocode was not successful for the following reason: ' + status);
                        }
                    });
                }
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
                     var geocoder;
                     var map;
                     //  initializeGEO();
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
                    isMapVisible: isMapVisible,
                    isSaveChangesEnable: isSaveChangesEnable,
                    isdeleteEnable: isdeleteEnable,
                    isAddressFilled: isAddressFilled,
                    hideBranchCategoryDialog: hideBranchCategoryDialog,
                    CodeAddressonMap: CodeAddressonMap,
                    branchDdlist: branchDdlist,
                    getAllCountries: getAllCountries,
                    countries: countries
                };

            })()
        };

        return ist.Layout.viewModel;

    });


