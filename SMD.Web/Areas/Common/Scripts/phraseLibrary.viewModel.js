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
                IsSaveChanges = ko.observable(false),
                TemporarySection = ko.observable(),
                TemporarySecEvent = ko.observable(),
                showphraseLibraryDialog = function () {

                    view.showphraseLibraryDialog();
                    Phrases.removeAll();
                    sections.removeAll();
                    IsDisplayAddPhBtn(false);
                    getAllSections();
                    IsPharsesAvailiable(false);
                },
                RefreshPhraseLibrary = function () {
                    Phrases.removeAll();
                    sections.removeAll();
                    IsDisplayAddPhBtn(false);
                    IsPharsesAvailiable(false);
                    isOpenFromPhraseLibrary(true)
                }
                ,
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
               FinalSaveCall = function (section,flag) {
                   debugger;
                   var IsArrayChanges = false;
                   PhraseModel = model.PhraseLibrarySaveModel();
                   var SectionList = PhraseModel.Sections;

                   _.each(Phrases(), function (item) {
                      // alert(item.phraseText() + '' + item.isValid() + 'has changes' + item.hasChanges());
                       if (item.hasChanges() && item.isValid() || item.IsDeleted() == true) {
                           
                           PhraseModel.PhrasesList.push(item.convertToServerData(item));
                           IsArrayChanges = true;
                       }
                   });

                   if (IsArrayChanges) {
                       if (PhraseModel.PhrasesList.length > 0) {
                          
                               savePhraseLibrary(PhraseModel,section,flag);
                       }
                       else {
                           selectedSection(section);
                           BindSectionFields(section);
                       }
                   }

                   else {
                       selectedSection(section);
                       BindSectionFields(section);
                   }

                   confirmation.afterCancel(function () {
                       selectedSection(section);
                       BindSectionFields(section);

                   });

               },
                onSelectSection = function (section, event) {
                    //selectedSection = section;
                    //  if (setselectdsection != null) {
                      TemporarySection(section);
                    
                       TemporarySecEvent(event);
                   
                        SelectedSectionId = section.sectionId();
                        //if (SelectedSectionId>0) {
                        //    IsDisplayAddPhBtn(true);
                        //}
                        closeNewCampaignDialog(section,event);
                },


                SetSelectedIndex = function (event)
                {

                    if (event.target.classList.contains("dd-handle")) {
                       // event.target.classList.remove("fa-chevron-circle-right");
                        //event.target.classList.add("dd-handle");
                        //event.target.classList.add("selectedRow");
                        alert('contains');
                    }

                    // $(event.target).closest('li')[0]

                },

                closeNewCampaignDialog = function (section,event) {
                    
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
                   
                    if (IsArrayChanges) {
                        if (PhraseModel.PhrasesList.length > 0) {
                            confirmation.messageText("Do you want to save changes?");

                            confirmation.afterProceed(function () {
                                savePhraseLibrary(PhraseModel, section,null,event);
                            });
                            confirmation.show();
                        }
                        else {

                            selectedSection(section);
                            BindSectionFields(section);
                        }
                    }

                    else {
                        
                        selectedSection(section);
                        BindSectionFields(section);
                    }
                    
                    confirmation.afterCancel(function () {
                        selectedSection(section);
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
                        selectPhrase = function (phrase) {
                          //  alert(phrase.phraseText());
                          //  SelectPhraseforcustomUpdate(phrase.phraseText());
                            if (afterSelectPhrase && typeof afterSelectPhrase === "function") {
                                //if (phrase.phraseId() === undefined || phrase.phraseId() === 0) {
                                //    toastr.error("Please First save the phrase.");
                                //    //phrase.isPhraseChecked(false);
                                //} else {
                                //    afterSelectPhrase(phrase.phraseText());
                                //    afterSelectPhrase = null;
                                //    view.hidePhraseLibraryDialog();
                                //}
                               // savePhraseLibrary(null, true);

                                afterSelectPhrase(phrase.phraseText());
                                afterSelectPhrase = null;
                                view.HidephraseLibraryDialog();
                            }
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
                         show = function (afterSelectPhraseCallback) {
                             //old menu collapse
                             //if (selectedSection() !== undefined) {
                             //    selectedSection().isExpanded(false);
                             //}
                             selectedSection(new model.Section());
                             view.showphraseLibraryDialog();
                             //if (sections().length === 0) {
                             //    getAllSections();
                             //}
                             //else {
                             //    selectDefaultSectionForProduct();
                             //}
                             getAllSections();
                             afterSelectPhrase = afterSelectPhraseCallback;
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
                                         
                                         IntialLength = Phrases().length;
                                         if (IntialLength > 0) {
                                             IsPharsesAvailiable(false);
                                         }
                                         else {
                                             IsPharsesAvailiable(true);
                                         }
                                         
                                         IsDisplayAddPhBtn(true);
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
                                              
                                         IsDisplayAddPhBtn(true);
                                         
                                     },
                                     error: function (response) {
                                         toastr.error("Failed to load Phrase Fileds . Error: ");
                                     }
                                 });

                             }
                         },

                         savePhraseLibrary = function (model,section,flag,event) {
                           
                             saveLibrary(model, section, flag,event);

                         },
                         savePhraseData = function () {

                            // closeNewCampaignDialog(selectedSection);
                             FinalSaveCall(selectedSection,false);
                         },
                             saveLibrary = function (PhraseModel,section,refreshphrases,event) {

                                 dataService.savePhaseLibrary(

                                 PhraseModel, {
                                     success: function (data) {
                                         
                                             
                                                 toastr.success("Successfully save.");
                                         
                                                 var setSelectedSection = new model.Section.Create(data);
                                                 
                                                 getPhraseFields(null, setSelectedSection.sectionId());
                                                // selectedSection(null);
                                               //  selectedSection(section);
                                              //   SetSelectedIndex(event);
                                              //   if (setSelectedSection.sectionId() == SelectedSectionId) {
                                                 //    alert('sdasa');
                                                   //  SetSelectedIndex(TemporarySecEvent);
                                                 //}
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
                    IsPharsesAvailiable: IsPharsesAvailiable,
                    show: show,
                    selectPhrase: selectPhrase,
                    RefreshPhraseLibrary: RefreshPhraseLibrary
                };

            })()
        };

        return ist.Layout.viewModel;

});


