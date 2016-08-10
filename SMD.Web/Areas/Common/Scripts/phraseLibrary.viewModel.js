define("PhraseLibrary/phraseLibrary.viewModel",
    ["jquery", "amplify", "ko", "PhraseLibrary/phraseLibrary.dataService", "PhraseLibrary/phraseLibrary.model", "common/confirmation.viewModel"],

    function ($, amplify, ko, dataService, model, confirmation) {

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
                           Phrases.removeAll();
                           sections.removeAll();
                           IsDisplayAddPhBtn(false);
                           getAllSections();
                       },
                sections = ko.observableArray([]),
               
                isOpenFromPhraseLibrary = ko.observable(true),
                selectedPhraseField = ko.observable(),
                selectedSection = ko.observable(),
                 selectedPhrase = ko.observable(false),
                AddEditDeleteFlag = ko.observable(false),
                Phrases = ko.observableArray([]),
                SelectedSectionId = ko.observable(0),
                PhrasesSavingList = ko.observableArray([]),
                SectionModel = ko.observable(),
                IntialLength = ko.observable(0),
                PhraseModel = ko.observable(),
                SectionList = ko.observableArray([]),
                PreviousSection = ko.observable(),
                IsDisplayAddPhBtn = ko.observable(false),
                SelectedPhrasecolor = ko.observable('#000000'),
                IsPharsesAvailiable = ko.observable(false),
                  templateToUse = function () {

                      if (isOpenFromPhraseLibrary()) {
                          return 'phraseEditItemTemplate';
                      } else {
                          return 'phraseItemTemplate';
                      }
                  },
                 addPhrase = function () {
                         var NewPhrase = {
                             PhraseName: '',
                             SectionId: SelectedSectionId
                         };
                         IsPharsesAvailiable(false);
                         var phrase = new model.Phrase.Create(NewPhrase);
                         Phrases.push(phrase);
                 },
               FinalSaveCall = function (section) {

                   var IsArrayChanges = false;
                   PhraseModel = model.PhraseLibrarySaveModel();
                   var SectionList = PhraseModel.Sections;

                   _.each(Phrases(), function (item) {

                       if (item.hasChanges() && item.isValid() || item.IsDeleted() == true) {
                          // alert(item.phraseText() + '' + item.isValid());
                           PhraseModel.PhrasesList.push(item.convertToServerData(item));
                           IsArrayChanges = true;
                       }
                   });

                   if (Phrases().length > IntialLength || IsArrayChanges == true) {
                       if (PhraseModel.PhrasesList.length > 0) {
                          
                               savePhraseLibrary(PhraseModel, section);
                       }
                       else {
                           BindSectionFields(section);
                       }
                   }

                   else {

                       BindSectionFields(section);
                   }

                   confirmation.afterCancel(function () {

                       BindSectionFields(section);

                   });


               },
                onSelectSection = function (section) {
                    //selectedSection = section;
                    selectedSection(section);
                    SelectedSectionId = section.sectionId();
                    if (selectedSection != null) {
                        IsDisplayAddPhBtn(true);
                    }
                    closeNewCampaignDialog(section);
                }

                closeNewCampaignDialog = function (section) {
                    
                    var IsArrayChanges = false;
                    PhraseModel = model.PhraseLibrarySaveModel();
                    var SectionList = PhraseModel.Sections;
              
                    _.each(Phrases(), function (item) {
                        
                        if (item.hasChanges() && item.isValid() || item.IsDeleted() == true)
                        {
                            PhraseModel.PhrasesList.push(item.convertToServerData(item));
                            IsArrayChanges = true;
                        }
                    });
                   
                    if (Phrases().length > IntialLength || IsArrayChanges==true) {
                        if (PhraseModel.PhrasesList.length > 0) {
                            confirmation.messageText("Do you want to save changes?");

                            confirmation.afterProceed(function () {
                                savePhraseLibrary(PhraseModel, section);
                            });
                            confirmation.show();
                        }
                        else {
                            BindSectionFields(section);
                        }
                    }

                    else {
                       
                        BindSectionFields(section);
                    }
                    
                    confirmation.afterCancel(function () {

                        BindSectionFields(section);
                       
                    });

              
                },
                       //PhaseLibrary Concerned!
                        getAllSections = function () {
                            dataService.getSections(null, {
                                success: function (data) {
                                    
                                    _.each(data, function (item) {
                                        var section = new model.Section.Create(item);
                                        sections.push(section);
                                    });
                                    Phrases.removeAll();
                                },
                                error: function () {
                                    toastr.error("Failed to phrase library.");
                                }
                            });
                        },
                        deletePhrase = function (phrase) {


                            confirmation.messageText("Do you want to delete phrase?");

                            confirmation.afterProceed(function () {

                                phrase.IsDeleted(true);


                            });
                            confirmation.afterCancel(function () {

                                confirmation.hide();

                            });
                            confirmation.show();
                            
                        },
                       PhraseLibraryPopUpClose = function ()
                       {
                           Phrases.removeAll();
                           sections.removeAll();
                          // window.location.reload();
                       },
                         getPhraseFields = function (section, SectionID) {
                            
                             //IsDisplayAddPhraseBtn(true);
                             if (SectionID > 0) {
                                 dataService.getPhraseBySectionID({
                                     PhraseID: SectionID,
                                 }, {
                                     success: function (data) {
                                         Phrases.removeAll();
                                         _.each(data, function (phrases) {
      
                                             var phrase = new model.Phrase.Create(phrases);
                                             Phrases.push(phrase);
                                         });
                                         // 
                                         
                                         IntialLength = Phrases().length;
                                         if (IntialLength > 0) {
                                             IsPharsesAvailiable(false);
                                         }
                                         else {
                                             IsPharsesAvailiable(true);
                                         }
                                     },
                                     error: function (response) {
                                         toastr.error("Failed to load Phrase Fileds . Error: ");
                                     }
                                 });
                             }
                             else {
                                 dataService.getPhraseBySectionID({
                                     PhraseID: section.sectionId(),
                                 }, {
                                     success: function (data) {
                                         Phrases.removeAll();
                                         _.each(data, function (phrases) {

                                             var phrase = new model.Phrase.Create(phrases);
                                             Phrases.push(phrase);
                                         });

                                         IntialLength = Phrases().length;
                                         if (IntialLength > 0) {
                                             IsPharsesAvailiable(false);
                                         }
                                         else {
                                             IsPharsesAvailiable(true);
                                         }
                                              
                                         
                                     },
                                     error: function (response) {
                                         toastr.error("Failed to load Phrase Fileds . Error: ");
                                     }
                                 });

                             }
                         },

                         savePhraseLibrary = function (model,section) {
                             
                             saveLibrary(model, section);

                         },
                         savePhraseData = function () {

                            // closeNewCampaignDialog(selectedSection);
                             FinalSaveCall(selectedSection);
                         },
                             saveLibrary = function (PhraseModel,section) {

                                 dataService.savePhaseLibrary(

                                 PhraseModel, {
                                     success: function (data) {
                                         
                                              //   refershPhraseLibraryAfterSave();
                                                 toastr.success("Successfully save.");
                                                // alert(section.sectionId());
                                                 getPhraseFields(null, data);
                                             },
                                             error: function (response) {
                                                 toastr.error("Failed to Save . Error: " + response);
                                             }
                                         });
                             },
                       
                        BindSectionFields = function (Section) {
                            getPhraseFields(Section);
                        };

                // Initialize the view model
                 initialize = function (specifiedView) {
                     view = specifiedView;
                     ko.applyBindings(view.viewModel, view.bindingRoot);
                     ko.applyBindings(view.viewModel, view.bindingPartial);
                    
                 };
                return {

                    initialize: initialize,
                    showBranchDialoge: showBranchDialoge,
                    showphraseLibraryDialog: showphraseLibraryDialog,
                    getAllSections: getAllSections,
                    sections: sections,
                    onSelectSection: onSelectSection,
                    isOpenFromPhraseLibrary: isOpenFromPhraseLibrary,
                    addPhrase: addPhrase,
                    selectedPhraseField: selectedPhraseField,
                    templateToUse: templateToUse,
                    selectedPhrase: selectedPhrase,
                    AddEditDeleteFlag: AddEditDeleteFlag,
                    Phrases: Phrases,
                    deletePhrase: deletePhrase,
                    selectedSection: selectedSection,
                    savePhraseLibrary: savePhraseLibrary,
                    saveLibrary: saveLibrary,
                    PhrasesSavingList: PhrasesSavingList,
                    closeNewCampaignDialog: closeNewCampaignDialog,
                    savePhraseData: savePhraseData,
                    PhraseLibraryPopUpClose: PhraseLibraryPopUpClose,
                    IsDisplayAddPhBtn: IsDisplayAddPhBtn,
                    SelectedPhrasecolor: SelectedPhrasecolor,
                    IsPharsesAvailiable: IsPharsesAvailiable
                };

            })()
        };

        return ist.Layout.viewModel;

});


