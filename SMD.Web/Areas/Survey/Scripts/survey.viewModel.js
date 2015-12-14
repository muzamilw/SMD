/*
    Module with the view model for the Profile Questions
*/
define("survey/survey.viewModel",
    ["jquery", "amplify", "ko", "survey/survey.dataservice", "common/pagination", "survey/survey.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, pagination, model, confirmation) {
        var ist = window.ist || {};
        ist.survey = {
            viewModel: (function () {
                var view,

                    //  Array
                    questions = ko.observableArray([]),
                    // Base Data
                    langs = ko.observableArray([]),
                    countries = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    // Search Filter value 
                    filterValue = ko.observable(),
                    langfilterValue = ko.observable(41),
                    countryfilterValue = ko.observable(214),
                    // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                    ////selected Question
                    selectedQuestion = ko.observable(new model.Survey()),
                    // selected location 
                    selectedLocation = ko.observable(),
                    selectedLocationRadius = ko.observable(),
                    selectedLocationIncludeExclude = ko.observable(true),
                    selectedLangIncludeExclude = ko.observable(true),
                    selectedLocationLat = ko.observable(0),
                    selectedLocationLong = ko.observable(0),
                    // age list 
                    ageRange = ko.observable([{ value: '11', text: '11' }, { value: '12', text: '12' }, { value: '13', text: '13' }, { value: '14', text: '14' }, { value: '15', text: '15' }, { value: '16', text: '16' }, { value: '17', text: '17' }, { value: '18', text: '18' }, { value: '19', text: '19' }])
                    //Get Questions
                    getQuestions = function () {   
                        dataservice.searchSurveyQuestions(
                            {
                                SearchText: filterValue(),
                                LanguageFilter: langfilterValue(),
                                CountryFilter: countryfilterValue(),
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                FirstLoad:false
                            },
                            {
                                success: function (data) {
                                    populateSurveyQuestions(data);
                                },
                                error: function () {
                                    toastr.error("Failed to load  questions!");
                                }
                            });
                    },

                    // //Get Base Data for Questions
                    getBasedata = function () {
                        dataservice.searchSurveyQuestions({
                            FirstLoad: true,
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage()
                        },
                            {
                                success: function (data) {
                                    // popullate base data 
                                    langs.removeAll();
                                    countries.removeAll();
                                    ko.utils.arrayPushAll(langs(), data.LanguageDropdowns);
                                    ko.utils.arrayPushAll(countries(), data.CountryDropdowns);
                                    langs.valueHasMutated();
                                    countries.valueHasMutated();
                                    // populate survey questions 
                                    populateSurveyQuestions(data);
                    
                                },
                                error: function () {
                                    toastr.error("Failed to load base data!");
                                }
                            });
           
                    },
                    // update survey questions 
                    populateSurveyQuestions = function (data) {
                        questions.removeAll();
                       
                        _.each(data.SurveyQuestions, function (item) {
                            questions.push(model.Survey.Create(updateSurveryItem(item)));
                        });
                        pager().totalCount(data.TotalCount);
                    }
                    // populate country, language and status fields 
                    updateSurveryItem = function (item) {
                
                        _.each(langs(), function (language) {
                            if(language.LanguageId == item.LanguageId) {
                                item.Language = language.LanguageName;
                            }
                        });
                        _.each(countries(), function (country) {
                            if (country.CountryId == item.CountryId) {
                                item.Country = country.CountryName;
                            }
                        });
                        if (item.Status == 1) {
                            item.StatusValue = "Draft"
                        } else if (item.Status == 2) {
                            item.StatusValue = "Submitted for Approval"
                        } else if (item.Status == 3) {
                            item.StatusValue = "Live"
                        } else if (item.Status == 4) {
                            item.StatusValue = "Paused"
                        } else if (item.Status == 5) {
                            item.StatusValue = "Completed"
                        } else if (item.Status == 6) {
                            item.StatusValue = "Approval Rejected"
                        }
                        return item;
                    }
                    // Search Filter 
                    filterSurveyQuestion = function () {
                        getQuestions();
                    },
                    // Make Filters Claer
                    clearFilters = function () {
                        langfilterValue(undefined);
                        countryfilterValue(undefined);
                        filterValue(undefined);
                        getQuestions();
                    },
                    // Add new Profile Question
                    addNewSurvey = function () {
                        selectedQuestion(new model.Survey());
                        selectedQuestion().Gender("1");
                        isEditorVisible(true);
                        view.initializeTypeahead();
                    },
                    // Close Editor 
                    closeEditDialog = function () {
                        isEditorVisible(false);
                    },
                    // On editing of existing PQ
                    onEditSurvey = function (item) {
                       //call function to edit survey
                        isEditorVisible(true);
                    },
                    onDeleteSurvey = function (item) {
                        //// Ask for confirmation
                        //confirmation.afterProceed(function () {
                        //    deleteSurvey(item);
                        //});
                        //confirmation.show();
                    },
                    // Delete PQ
                    deleteSurvey = function (item) {
                        //dataservice.deleteProfileQuestion(item.convertToServerData(), {
                        //    success: function () {
                        //        var newObjtodelete = questions.find(function (temp) {
                        //            return temp.qId() == temp.qId();
                        //        });
                        //        questions.remove(newObjtodelete);
                        //        toastr.success("You are Good!");
                        //    },
                        //    error: function () {
                        //        toastr.error("Failed to delete!");
                        //    }
                        //});
                    },
                    // store left side ans image
                    storeLeftImageCallback = function (file, data) {
                        selectedQuestion().LeftPictureBytes(data);
                    },
                    // store right side ans image
                    storeRightImageCallback = function (file, data) {
                         selectedQuestion().RightPictureBytes(data);
                    },
                     // remove location 
                    onRemoveLocation = function (item) {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            deleteLocation(item);
                        });
                        confirmation.show();
                    },
                    deleteLocation = function (item) {
                         console.log(item.ID());
                         if(item.ID() != 0 )
                         {
                             //dataservice.deleteProfileQuestion(item.convertToServerData(), {
                             //    success: function () {
                             //        var newObjtodelete = questions.find(function (temp) {
                             //            return temp.qId() == temp.qId();
                             //        });
                             //        questions.remove(newObjtodelete);
                             //        toastr.success("You are Good!");
                             //    },
                             //    error: function () {
                             //        toastr.error("Failed to delete!");
                             //    }
                             //});
                         } else {
                             selectedQuestion().SurveyQuestionTargetLocation.remove(item);
                             toastr.success("Removed Successfully!");
                         }
                        
                     },
                    //add location
                    onAddLocation = function (item) {
                      
                        selectedLocation().Radius = (selectedLocationRadius);
                        selectedLocation().IncludeorExclude = (selectedLocationIncludeExclude);
                        selectedQuestion().SurveyQuestionTargetLocation.push( new model.SurveyQuestionTargetLocation.Create( {
                            CountryID: selectedLocation().CountryID,
                            CityID: selectedLocation().CityID,
                            Radius: selectedLocation().Radius(),
                            Country: selectedLocation().Country,
                            City: selectedLocation().City,
                            IncludeorExclude: selectedLocation().IncludeorExclude(),
                            ID: 0,
                            SQID: selectedQuestion().SQID()
                        }));
                        $(".locVisibility,.locMap").css("display", "none");
                        resetLocations();
                    },
                    resetLocations = function () {
                        $("#searchSurveyLocations").val("");
                        selectedLocationRadius("");
                    },
                    addLanguage = function (selected) {
                        selectedQuestion().SurveyQuestionTargetCriteria.push(new model.SurveyQuestionTargetCriteria.Create({
                            Language: selected.LanguageName,
                            LanguageID: selected.LanguageId,
                            IncludeorExclude: parseInt(selectedLangIncludeExclude()),
                            Type: 3,
                            ID: 0,
                            SQID: selectedQuestion().SQID()
                        }));
                        $("#searchLanguages").val("");
                    },
                    onRemoveLanguage = function (item) {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            deleteLanguage(item);
                        });
                        confirmation.show();
                    },
                     deleteLanguage = function (item) {
                         console.log(item.ID());
                         if (item.ID() != 0) {
                             //dataservice.deleteProfileQuestion(item.convertToServerData(), {
                             //    success: function () {
                             //        var newObjtodelete = questions.find(function (temp) {
                             //            return temp.qId() == temp.qId();
                             //        });
                             //        questions.remove(newObjtodelete);
                             //        toastr.success("You are Good!");
                             //    },
                             //    error: function () {
                             //        toastr.error("Failed to delete!");
                             //    }
                             //});
                         } else {
                             selectedQuestion().SurveyQuestionTargetCriteria.remove(item);
                             toastr.success("Removed Successfully!");
                         }

                     },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, questions, getQuestions));
                        // Base Data Call
                        getBasedata();
                        selectedQuestion().Gender("1");
                        selectedQuestion().SQID(0);
                    };
                return {
                    initialize: initialize,
                    pager: pager,
                    selectedQuestion: selectedQuestion,
                    questions: questions,
                    getQuestions: getQuestions,
                    getBasedata: getBasedata,
                    isEditorVisible: isEditorVisible,
                    filterSurveyQuestion: filterSurveyQuestion,
                    filterValue: filterValue,
                    addNewSurvey: addNewSurvey,
                    closeEditDialog: closeEditDialog,
                    onEditSurvey: onEditSurvey,
                    deleteSurvey: deleteSurvey,
                    langs: langs,
                    countries: countries,
                    langfilterValue: langfilterValue,
                    clearFilters: clearFilters,
                    countryfilterValue: countryfilterValue,
                    storeLeftImageCallback: storeLeftImageCallback,
                    storeRightImageCallback: storeRightImageCallback,
                    ageRange: ageRange,
                    selectedLocation: selectedLocation,
                    selectedLocationRadius: selectedLocationRadius,
                    onAddLocation: onAddLocation,
                    onRemoveLocation: onRemoveLocation,
                    deleteLocation: deleteLocation,
                    selectedLocationIncludeExclude: selectedLocationIncludeExclude,
                    selectedLangIncludeExclude:selectedLangIncludeExclude,
                    selectedLocationLat: selectedLocationLat,
                    selectedLocationLong: selectedLocationLong,
                    addLanguage: addLanguage,
                    onRemoveLanguage: onRemoveLanguage
                };
            })()
        };
        return ist.survey.viewModel;
    });
