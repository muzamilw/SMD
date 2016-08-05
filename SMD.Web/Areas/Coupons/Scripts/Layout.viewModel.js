define("Layout/Layout.viewModel",
    ["jquery", "amplify", "ko", "Layout/Layout.dataService", "Layout/Layout.model", "common/confirmation.viewModel"],
<<<<<<< HEAD
    function ($, amplify, ko, dataService, model, confirmation) {
=======
    function ($, amplify, ko, dataservice, model, confirmation) {
>>>>>>> origin/dev
        var ist = window.ist || {};
        ist.Layout = {
            viewModel: (function () {
                var // The view 
                   view,
                      showBranchDialoge = function () {
                          view.showBranchCategoryDialog();
                      },
                       showphraseLibraryDialog = function () {
                           view.showphraseLibraryDialog();

                       };
                sections = ko.observableArray([]),
                selectedSection = ko.observable(),
                isOpenFromPhraseLibrary = ko.observable(true),
                selectedPhraseField = ko.observable(),
                selectedSection = ko.observable(),
                 selectedPhrase = ko.observable(false),
                AddEditDeleteFlag = ko.observable(false),
                  templateToUse = function () {
                     
                      if (isOpenFromPhraseLibrary()) {
                          return 'phraseEditItemTemplate';
                      } else {
                          return 'phraseItemTemplate';
                      }
                  },
                 addPhrase = function () {
                     if (selectedPhraseField() != undefined) {
                         selectedPhraseField().phrases.splice(0, 0, model.Phrase(0, "", selectedPhraseField().fieldId()));
                         phrases.splice(0, 0, selectedPhraseField().phrases()[0]);
                     }
                 },
                selectSection = function () {
                    alert(selectedSection.sectionId);
                }
            },
                       //PhaseLibrary Concerned!
                        getAllSections = function () {
                            dataService.getSections(null,{
                                success: function (data) {
                                    
                                    //sections.removeAll();
                                    _.each(data, function (item) {
                                      
                                       
                                        var section = new model.Section.Create(item);
                                        
                                         sections.push(section);

                                        //_.each(item.PhrasesFields, function (phraseFieldItem) {
                                        //    var phraseField = new model.PhraseField.Create(phraseFieldItem);
                                        //    section.phrasesFields.push(phraseField);
                                        //});

                                    });
                                   // selectDefaultSectionForProduct();
                                },
                                error: function () {
                                    toastr.error("Failed to phrase library.");
                                }
                            });
                        },

                    getPhraseFields = function (section) {
                        dataservice.getPhraseBySectionID({
                            sectionId: section.sectionId(),
                        }, {
                            success: function (data) {
                                if (data != null) {

                                    section.Phrases.removeAll();

                                    _.each(data, function (phraseFieldItem) {
                                        var phraseField1 = new model.PhraseField.Create(phraseFieldItem);
                                        section.Phrases.push(phraseField1);

                                    });

                                  

                                    // true, Refresh The Phrase Fields as well as Phrases
                                //    if (afterSaveRefreshListFlag) {
                                //        var sectionFilter = _.filter(sections(), function (sectionItem) {
                                //            return sectionItem.sectionId() === selectedPhraseField().sectionId();
                                //        });
                                //        if (sectionFilter.length > 0) {
                                //            var phraseField = _.filter(sectionFilter[0].phrasesFields(), function (phrase) {
                                //                return phrase.fieldId() === selectedPhraseField().fieldId();
                                //            });
                                //            if (phraseField.length > 0) {
                                //                selectedPhraseField(phraseField[0]);
                                //                getPhrasesByPhraseFieldId(selectedPhraseField().fieldId(), true);
                                //            }
                                //        }
                                //    }
                                //    else {
                                //        resetTreeExpension(section);
                                //    }

                                //}
                            },
                            error: function (response) {
                                toastr.error("Failed to load Phrase Fileds . Error: ");
                            }
                        });

                // Initialize the view model
                 initialize = function (specifiedView) {
                     view = specifiedView;
<<<<<<< HEAD
                     ko.applyBindings(view.viewModel, view.bindingRoot);
                     ko.applyBindings(view.viewModel, view.bindingPartial);
                     getAllSections();
                 };
                return {

                    initialize: initialize,
                    showBranchDialoge: showBranchDialoge,
                    showphraseLibraryDialog: showphraseLibraryDialog,
                    getAllSections: getAllSections,
                    sections: sections,
                    selectedSection: selectedSection,
                    isOpenFromPhraseLibrary: isOpenFromPhraseLibrary,
                    addPhrase: addPhrase,
                    selectedPhraseField: selectedPhraseField,
                    templateToUse: templateToUse,
                    selectedSection: selectedSection,
                    selectedPhrase:selectedPhrase,
                    AddEditDeleteFlag: AddEditDeleteFlag
                };

            })()
=======
                     ko.applyBindings(view.viewModel, view.bindingRoot);
                     ko.applyBindings(view.viewModel, view.bindingPartial);
                     
                 };
                return {

                    initialize: initialize,
                    showBranchDialoge: showBranchDialoge

                };

            })()
        };

        return ist.Layout.viewModel;

    });
define("Layout/Layout.viewModel",
    ["jquery", "amplify", "ko", "Layout/Layout.dataService", "Layout/Layout.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.Layout = {
            viewModel: (function () {
                var // The view 
                   view,
                      showBranchDialoge = function () {
                          view.showBranchCategoryDialog();
                      },
                // Initialize the view model
                 initialize = function (specifiedView) {
                     view = specifiedView;
                     ko.applyBindings(view.viewModel, view.bindingRoot);
                     ko.applyBindings(view.viewModel, view.bindingPartial);
                     
                 };
                return {

                    initialize: initialize,
                    showBranchDialoge: showBranchDialoge

                };

            })()
>>>>>>> origin/dev
        };

        return ist.Layout.viewModel;

    });