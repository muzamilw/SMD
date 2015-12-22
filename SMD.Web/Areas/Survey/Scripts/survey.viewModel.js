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
                    // criteria selection 
                    selectedCriteria = ko.observable(),
                    profileQuestionList = ko.observable([]),
                    surveyQuestionList = ko.observableArray([]),
                    profileAnswerList = ko.observable([]),
                    editCriteriaHeading = ko.observable("Add Profile Criteria"),
                    titleText = ko.observable("Add new survey"),
                    isNewCriteria = ko.observable(true),
                    // age list 
                    ageRange = ko.observableArray([])
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
                        titleText("Add new survey");
                        selectedQuestion(new model.Survey());
                        selectedQuestion().Gender("1");
                        selectedQuestion().LeftPicturePath("Content/Images/Company_Default.png");
                        selectedQuestion().RightPicturePath("Content/Images/Company_Default.png");
                        selectedQuestion().StatusValue("Draft");
                        selectedQuestion().Status(1);
                        selectedQuestion().reset();
                        isEditorVisible(true);
                        view.initializeTypeahead();
                    },
                    // Close Editor 
                    closeEditDialog = function () {
                        isEditorVisible(false);
                    },
                    // On editing of existing PQ
                    onEditSurvey = function (item) {
                        titleText("Edit survey");
                        //call function to edit survey
                        dataservice.getSurveyQuestion(
                           {
                               SqId: item.SQID(),
                               FirstLoad: false
                           },
                           {
                               success: function (data) {
                                   //
                                   selectedQuestion(model.Survey.Create(updateSurveryItem(data.SurveyQuestion)));
                                   selectedQuestion().reset();
                                   view.initializeTypeahead();
                                   // load survey questions
                                   if (surveyQuestionList().length == 0) {
                                       dataservice.getBaseData({
                                           RequestId: 4,
                                           QuestionId: 0,
                                           SQID: selectedQuestion().SQID()
                                       }, {
                                           success: function (data) {
                                               if (data != null) {
                                                   surveyQuestionList([]);
                                                   ko.utils.arrayPushAll(surveyQuestionList(), data.SurveyQuestions);
                                                   surveyQuestionList.valueHasMutated();
                                               }

                                           },
                                           error: function (response) {

                                           }
                                       });
                                   }
                                   // load profile questions 
                                   if (profileQuestionList().length == 0) {
                                       dataservice.getBaseData({
                                           RequestId: 2,
                                           QuestionId: 0,
                                       }, {
                                           success: function (data) {
                                               if (data != null) {
                                                   _.each(data.ProfileQuestions, function (question) {
                                                       question.PQID = question.PqId;
                                                   });
                                                   profileQuestionList([]);
                                                   ko.utils.arrayPushAll(profileQuestionList(), data.ProfileQuestions);
                                                   profileQuestionList.valueHasMutated();
                                               }

                                           },
                                           error: function (response) {

                                           }
                                       });
                                   }
                                   isEditorVisible(true);
                               },
                               error: function () {
                                   toastr.error("Failed to load  question!");
                               }
                           });
                        
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
                        selectedQuestion().SurveyQuestionTargetLocation.remove(item);
                        toastr.success("Removed Successfully!");
                     },
                    //add location
                    onAddLocation = function (item) {
                        selectedLocation().Radius = (selectedLocationRadius);
                        selectedLocation().IncludeorExclude = (selectedLocationIncludeExclude);
                        selectedQuestion().SurveyQuestionTargetLocation.push( new model.SurveyQuestionTargetLocation.Create( {
                            CountryId: selectedLocation().CountryID,
                            CityId: selectedLocation().CityID,
                            Radius: selectedLocation().Radius(),
                            Country: selectedLocation().Country,
                            City: selectedLocation().City,
                            IncludeorExclude: selectedLocation().IncludeorExclude(),
                          //  ID: 0,
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
                        selectedQuestion().SurveyQuestionTargetCriteria.remove(item);
                        toastr.success("Removed Successfully!");
                        // delete from model on save click
                     },
                    isShowSurveyAns = ko.observable(false),
                    // survey question and  profile question selection criteria 

                    // Add new profile Criteria
                    addNewProfileCriteria = function () {
                        editCriteriaHeading("Add Profile Criteria");
                        var objProfileCriteria = new model.SurveyQuestionTargetCriteria();
                        isNewCriteria(true);
                        objProfileCriteria.Type("1");
                        objProfileCriteria.IncludeorExclude("1");
                        selectedCriteria(objProfileCriteria);
                        if (profileQuestionList().length == 0) {
                            dataservice.getBaseData({
                                RequestId: 2,
                                QuestionId: 0,
                            }, {
                                success: function (data) {
                                    if (data != null) {
                                        _.each(data.ProfileQuestions, function (question) {
                                            question.PQID = question.PqId;
                                        });
                                        profileQuestionList([]);
                                        ko.utils.arrayPushAll(profileQuestionList(), data.ProfileQuestions);
                                        profileQuestionList.valueHasMutated();
                                    }

                                },
                                error: function (response) {

                                }
                            });
                        }

                    },

                    // Add new survey Criteria
                    addNewSurveyCriteria = function () {
                        editCriteriaHeading("Add Survey Criteria");
                        isNewCriteria(true);
                        var objSurveyCriteria = new model.SurveyQuestionTargetCriteria();
                        objSurveyCriteria.Type("2");
                        objSurveyCriteria.IncludeorExclude("1");
                        selectedCriteria(objSurveyCriteria);
                        if (surveyQuestionList().length == 0) {
                            dataservice.getBaseData({
                                RequestId: 4,
                                QuestionId: 0,
                                SQID:selectedQuestion().SQID()
                            }, {
                                success: function (data) {
                                    if (data != null) {
                                        surveyQuestionList([]);
                                        ko.utils.arrayPushAll(surveyQuestionList(), data.SurveyQuestions);
                                        surveyQuestionList.valueHasMutated();
                                    }

                                },
                                error: function (response) {

                                }
                            });
                        }
                    },
                    saveCriteria = function () {
                        debugger;
                        if (selectedCriteria().Type() == "1") {
                            var selectedQuestionstring = $("#ddprofileQuestion option[value=" + $("#ddprofileQuestion").val() + "]").text();
                            selectedCriteria().questionString(selectedQuestionstring);
                            selectedCriteria().PQID($("#ddprofileQuestion").val());
                            var selectedQuestionAnswerstring = $("#ddprofileAnswers option[value=" + $("#ddprofileAnswers").val() + "]").text();
                            selectedCriteria().answerString(selectedQuestionAnswerstring);
                            selectedCriteria().PQAnswerID($("#ddprofileAnswers").val());
                        } else if (selectedCriteria().Type() == "2") {
                            var selectedQuestionstring = $("#ddsurveyQuestion option[value=" + $("#ddsurveyQuestion").val() + "]").text();
                            selectedCriteria().questionString(selectedQuestionstring);
                            var matchSurveyQuestion = ko.utils.arrayFirst(surveyQuestionList(), function (question) {
                                return question.SQID == $("#ddsurveyQuestion").val();
                            });
                            if (selectedCriteria().LinkedSQAnswer() == "1") {
                                selectedCriteria().answerString(matchSurveyQuestion.LeftPicturePath);
                            } else {
                                selectedCriteria().answerString(matchSurveyQuestion.RightPicturePath);
                            }
                        }
                        if (isNewCriteria()) {
                            selectedQuestion().SurveyQuestionTargetCriteria.push(new model.SurveyQuestionTargetCriteria.Create({
                                Type: selectedCriteria().Type(),
                                PqId: selectedCriteria().PQID(),
                                PqAnswerId: selectedCriteria().PQAnswerID(),
                                LinkedSqId: selectedCriteria().LinkedSQID(),
                                LinkedSqAnswer: selectedCriteria().LinkedSQAnswer(),
                                questionString: selectedCriteria().questionString(),
                                answerString: selectedCriteria().answerString(),
                                IncludeorExclude: selectedCriteria().IncludeorExclude()
                            }));
                        } else {
                            // already observable
                        }
                        isShowSurveyAns(false);
                    },
                    onEditCriteria = function (item) {
                        isNewCriteria(false);
                        var val = item.PQAnswerID() + 0;
                        if (item.Type() == "1") {
                            editCriteriaHeading("Edit Profile Criteria");
                            dataservice.getBaseData({
                                RequestId: 3,
                                QuestionId: item.PQID(),
                            }, {
                                success: function (data) {
                                    if (data != null) {
                                        _.each(data.ProfileQuestionAnswers, function (question) {
                                            question.PQID = question.PqId;
                                            question.PQAnswerID = question.PqAnswerId;
                                        });
                                        profileAnswerList([]);
                                        ko.utils.arrayPushAll(profileAnswerList(), data.ProfileQuestionAnswers);
                                        profileAnswerList.valueHasMutated();
                                        item.PQAnswerID(val);
                                    }
                                },
                                error: function (response) {

                                }
                            });

                            selectedCriteria(item);
                        } else {
                            editCriteriaHeading("Edit Survey Criteria");
                            selectedCriteria(item);
                            var selectedSurveyQuestionId = $("#ddsurveyQuestion").val();
                            var matchSurveyQuestion = ko.utils.arrayFirst(surveyQuestionList(), function (survey) {
                                return survey.SQID == item.LinkedSQID();
                            });
                            selectedCriteria().surveyQuestLeftImageSrc(matchSurveyQuestion.LeftPicturePath);
                            selectedCriteria().surveyQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);
                            isShowSurveyAns(true);
                        }

                    },
                    // Delete Handler PQ
                    onDeleteCriteria = function (item) {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            selectedQuestion().SurveyQuestionTargetCriteria.remove(item);
                        });
                        confirmation.show();
                    },

                    onChangeProfileQuestion = function () {
                        var selectedQuestionId = $("#ddprofileQuestion").val();
                        dataservice.getBaseData({
                            RequestId: 3,
                            QuestionId: selectedQuestionId,
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    _.each(data.ProfileQuestionAnswers, function (question) {
                                        question.PQID = question.PqId;
                                        question.PQAnswerID = question.PqAnswerId;
                                    });
                                    if (profileAnswerList().length > 0) {
                                        profileAnswerList([]);
                                    }
                                    ko.utils.arrayPushAll(profileAnswerList(), data.ProfileQuestionAnswers);
                                    profileAnswerList.valueHasMutated();

                                }

                            },
                            error: function (response) {

                            }
                        });
                    },

                    onChangeSurveyQuestion = function (item) {
                     
                        var selectedSurveyQuestionId = $("#ddsurveyQuestion").val();
                        var matchSurveyQuestion = ko.utils.arrayFirst(surveyQuestionList(), function (item) {
                            return item.SQID == selectedSurveyQuestionId;
                        });
                        item.LinkedSQAnswer("1");
                        item.surveyQuestLeftImageSrc(matchSurveyQuestion.LeftPicturePath);
                        item.surveyQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);
                        $("#surveyAnswersContainer").show();
                        isShowSurveyAns(true);
                    },
                     // Has Changes
                    hasChangesOnQuestion = ko.computed(function () {
                        if (selectedQuestion() == undefined) {
                            return false;
                        }
                        return (selectedQuestion().hasChanges());
                    }),
                    // save survey question 
                    onSaveSurveyQuestion = function () {
                        // now saving survey as draft but check stripe intergration and save it for submit for approval
                        saveSurveyQuestion(1);
                    },
                    saveSurveyQuestion = function (mode) {
                        var surveyData = selectedQuestion().convertToServerData();
                        dataservice.addSurveyData(surveyData, {
                            success: function (data) {
                                isEditorVisible(false);
                                getQuestions();
                                toastr.success("Successfully saved.");
                            },
                            error: function (response) {

                            }
                        });
                    }
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        for (var i = 10; i < 100; i++)
                        {
                            var text = i.toString();
                            if (i == 99)
                                text += "+";
                            ageRange.push({ value: i.toString(), text: text });
                        }
                        pager(pagination.Pagination({ PageSize: 10 }, questions, getQuestions));
                        // Base Data Call
                        getBasedata();
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
                    onRemoveLanguage: onRemoveLanguage,
                    selectedCriteria: selectedCriteria,
                    profileQuestionList: profileQuestionList,
                    profileAnswerList: profileAnswerList,
                    surveyQuestionList: surveyQuestionList,
                    addNewProfileCriteria: addNewProfileCriteria,
                    addNewSurveyCriteria: addNewSurveyCriteria,
                    saveCriteria: saveCriteria,
                    onEditCriteria: onEditCriteria,
                    onDeleteCriteria: onDeleteCriteria,
                    onChangeProfileQuestion: onChangeProfileQuestion,
                    onChangeSurveyQuestion: onChangeSurveyQuestion,
                    isShowSurveyAns: isShowSurveyAns,
                    hasChangesOnQuestion: hasChangesOnQuestion,
                    editCriteriaHeading: editCriteriaHeading,
                    isNewCriteria: isNewCriteria,
                    onSaveSurveyQuestion: onSaveSurveyQuestion,
                    titleText: titleText
                };
            })()
        };
        return ist.survey.viewModel;
    });
