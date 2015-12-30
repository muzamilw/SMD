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
                    selectedIndustryIncludeExclude = ko.observable(true),
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
                    canSubmitForApproval = ko.observable(true),
                    // age list 
                    ageRange = ko.observableArray([]),
                    //audience reach
                    reachedAudience = ko.observable(0),
                    //total audience
                    totalAudience = ko.observable(0),
                    // audience reach mode 
                    audienceReachMode = ko.observable(1),
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
                            item.StatusValue = "Draft";
                        } else if (item.Status == 2) {
                            item.StatusValue = "Submitted for Approval"; canSubmitForApproval(false);
                        } else if (item.Status == 3) {
                            item.StatusValue = "Live"; canSubmitForApproval(false);
                        } else if (item.Status == 4) {
                            item.StatusValue = "Paused"; canSubmitForApproval(false);
                        } else if (item.Status == 5) {
                            item.StatusValue = "Completed"; canSubmitForApproval(false);
                        } else if (item.Status == 6) {
                            item.StatusValue = "Approval Rejected"; canSubmitForApproval(false);
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
                        selectedQuestion().AgeRangeStart(10);
                        selectedQuestion().AgeRangeEnd(90);
                        selectedQuestion().reset();
                        selectedQuestion().SurveyQuestionTargetCriteria([]);
                        getAudienceCount();
                        isEditorVisible(true);
                        canSubmitForApproval(true);
                        view.initializeTypeahead();
                        bindAudienceReachCount();
                    },
                    // Close Editor 
                    closeEditDialog = function () {
                        isEditorVisible(false);
                    },
                    // On editing of existing PQ
                    onEditSurvey = function (item) {
                        titleText("Edit survey");
                        if (item.Status() == 1 || item.Status() == null) {
                            canSubmitForApproval(true);
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
                                       getAudienceCount();
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
                                       } bindAudienceReachCount();
                                       isEditorVisible(true);
                                   },
                                   error: function () {
                                       toastr.error("Failed to load  question!");
                                   }
                               });
                        }
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
                            LanguageId: selected.LanguageId,
                            IncludeorExclude: parseInt(selectedLangIncludeExclude()),
                            Type: 3,
                            SQID: selectedQuestion().SQID()
                        }));
                        $("#searchLanguages").val("");
                    },
                     addIndustry = function (selected) {
                         selectedQuestion().SurveyQuestionTargetCriteria.push(new model.SurveyQuestionTargetCriteria.Create({
                             Industry: selected.IndustryName,
                             IndustryId: selected.IndustryId,
                             IncludeorExclude: parseInt(selectedIndustryIncludeExclude()),
                             Type: 4,
                             SQID: selectedQuestion().SQID()
                         }));
                         $("#searchIndustries").val("");
                     },
                    onRemoveIndustry = function (item) {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            selectedQuestion().SurveyQuestionTargetCriteria.remove(item);
                            toastr.success("Removed Successfully!");
                        });
                        confirmation.show();
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
                        if (!isNewCriteria()) {
                            selectedQuestion().SurveyQuestionTargetCriteria.remove(selectedCriteria());
                        } else {

                        }
                        isShowSurveyAns(false);
                        //getAudienceCount();
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
                      //  getAudienceCount();
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
                   // submit  survey question for approval
                    onSubmitSurveyQuestion = function () {
                        saveSurveyQuestion(2);
                    },
                    saveSurveyQuestion = function (mode) {
                        selectedQuestion().Status(mode);
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
                    getAudienceCount = function () {
                        var countryIds = '', cityIds = '', countryIdsExcluded = '', cityIdsExcluded = '';
                        _.each(selectedQuestion().SurveyQuestionTargetLocation(), function (item) {
                            if(item.CityID() == 0 || item.CityID() == null)
                            {
                                if(item.IncludeorExclude() == '0')
                                {
                                    if(countryIdsExcluded == '')
                                    {
                                        countryIdsExcluded += item.CountryId();
                                    } else {
                                        countryIdsExcluded += ','+ item.CountryId();
                                    }
                                } else {
                                    if (countryIds == '') {
                                        countryIds += item.CountryId();
                                    } else {
                                        countryIds += ',' + item.CountryId();
                                    }
                                }
                            } else {
                                if (item.IncludeorExclude() == '0') {
                                    if (cityIdsExcluded == '') {
                                        cityIdsExcluded += item.CityID();
                                    } else {
                                        cityIdsExcluded += ',' + item.CityID();
                                    }
                                } else {
                                    if (cityIds == '') {
                                        cityIds += item.CityID();
                                    } else {
                                        cityIds += ',' + item.CityID();
                                    }
                                }
                            }
                        });
                        var languageIds = '', industryIds = '', languageIdsExcluded = '',
                            industryIdsExcluded = '', profileQuestionIds = '', profileAnswerIds = '',
                            surveyQuestionIds = '', surveyAnswerIds = '', profileQuestionIdsExcluded = '', profileAnswerIdsExcluded = '',
                            surveyQuestionIdsExcluded = '', surveyAnswerIdsExcluded = '';
                        _.each(selectedQuestion().SurveyQuestionTargetCriteria(), function (item) {
                            if (item.Type() == 1) {
                                if (item.IncludeorExclude() == '0') {
                                    if (profileQuestionIdsExcluded == '') {
                                        profileQuestionIdsExcluded += item.PQID();
                                    } else {
                                        profileQuestionIdsExcluded += ',' + item.PQID();
                                    }
                                    if (profileAnswerIdsExcluded == '') {
                                        profileAnswerIdsExcluded += item.PQAnswerID();
                                    } else {
                                        profileAnswerIdsExcluded += ',' + item.PQAnswerID();
                                    }
                                } else {
                                    if (profileQuestionIds == '') {
                                        profileQuestionIds += item.PQID();
                                    } else {
                                        profileQuestionIds += ',' + item.PQID();
                                    }
                                    if (profileAnswerIds == '') {
                                        profileAnswerIds += item.PQAnswerID();
                                    } else {
                                        profileAnswerIds += ',' + item.PQAnswerID();
                                    }
                                }
                            } else if (item.Type() == 2) {
                                if (item.IncludeorExclude() == '0') {
                                    if (surveyQuestionIdsExcluded == '') {
                                        surveyQuestionIdsExcluded += item.LinkedSQID();
                                    } else {
                                        surveyQuestionIdsExcluded += ',' + item.LinkedSQID();
                                    }
                                    if (surveyAnswerIdsExcluded == '') {
                                        surveyAnswerIdsExcluded += item.LinkedSQAnswer();
                                    } else {
                                        surveyAnswerIdsExcluded += ',' + item.LinkedSQAnswer();
                                    }
                                } else {
                                    if (surveyQuestionIds == '') {
                                        surveyQuestionIds += item.LinkedSQID();
                                    } else {
                                        surveyQuestionIds += ',' + item.LinkedSQID();
                                    }
                                    if (surveyAnswerIds == '') {
                                        surveyAnswerIds += item.LinkedSQAnswer();
                                    } else {
                                        surveyAnswerIds += ',' + item.LinkedSQAnswer();
                                    }
                                }
                            } else if (item.Type() == 3) {
                                if(item.IncludeorExclude() == '0')
                                {
                                    if (languageIdsExcluded == '') {
                                        languageIdsExcluded += item.LanguageID();
                                    } else {
                                        languageIdsExcluded += ',' + item.LanguageID();
                                    }
                                } else {
                                    if (languageIds == '') {
                                        languageIds += item.LanguageID();
                                    } else {
                                        languageIds += ',' + item.LanguageID();
                                    }
                                }
                            } else if (item.Type() == 4) {
                                if (item.IncludeorExclude() == '0') {
                                    if (industryIdsExcluded == '') {
                                        industryIdsExcluded += item.IndustryID();
                                    } else {
                                        industryIdsExcluded += ',' + item.IndustryID();
                                    }
                                } else {
                                    if (industryIds == '') {
                                        industryIds += item.IndustryID();
                                    } else {
                                        industryIds += ',' + item.IndustryID();
                                    }
                                }
                            }
                        });
                        var surveyData = {
                            ageFrom: selectedQuestion().AgeRangeStart(),
                            ageTo: selectedQuestion().AgeRangeEnd(),
                            gender: selectedQuestion().Gender(),
                            countryIds: countryIds,
                            cityIds: cityIds,
                            languageIds: languageIds,
                            industryIds: industryIds,
                            profileQuestionIds: profileQuestionIds,
                            profileAnswerIds: profileAnswerIds,
                            surveyQuestionIds: surveyQuestionIds,
                            surveyAnswerIds: surveyAnswerIds,
                            countryIdsExcluded: countryIdsExcluded,
                            cityIdsExcluded: cityIdsExcluded,
                            languageIdsExcluded: languageIdsExcluded,
                            industryIdsExcluded: industryIdsExcluded,
                            profileQuestionIdsExcluded: profileQuestionIdsExcluded,
                            profileAnswerIdsExcluded: profileAnswerIdsExcluded,
                            surveyQuestionIdsExcluded: surveyQuestionIdsExcluded,
                            surveyAnswerIdsExcluded: surveyAnswerIdsExcluded
                        };
                        dataservice.getAudienceData(surveyData, {
                            success: function (data) {
                                reachedAudience(data.MatchingUsers);
                                totalAudience(data.AllUsers);
                                var percent = data.MatchingUsers / data.AllUsers;
                                if (percent < 0.20) {
                                    audienceReachMode(1);
                                } else if (percent < 0.70) {
                                    audienceReachMode(2);
                                } else {
                                    audienceReachMode(3);
                                }
                                if (audienceReachMode() == 1) {
                                    $(".meterPin").removeClass("spec_aud").removeClass("defined_aud").removeClass("broad_aud").addClass("spec_aud");
                                } else if (audienceReachMode() == 2) {
                                    $(".meterPin").removeClass("spec_aud").removeClass("defined_aud").removeClass("broad_aud").addClass("defined_aud");
                                } else if (audienceReachMode() == 3) {
                                    $(".meterPin").removeClass("spec_aud").removeClass("defined_aud").removeClass("broad_aud").addClass("broad_aud");
                                }
                            },
                            error: function (response) {
                                toastr.error("Error while getting audience count.");
                            }
                        });
                    },
                     visibleTargetAudience = function (mode) {

                         if (mode != undefined) {
                            
                             var matcharry = ko.utils.arrayFirst(selectedQuestion().SurveyQuestionTargetCriteria(), function (item) {

                                 return item.Type() == mode;
                             });

                             if (matcharry != null) {
                                 return 1;
                             } else {
                                 return 0;
                             }
                         } else {
                             return 0;
                         }
                     },
                     bindAudienceReachCount = function () {
                         selectedQuestion().AgeRangeStart.subscribe(function (value) {
                             getAudienceCount();
                         });
                         selectedQuestion().AgeRangeEnd.subscribe(function (value) {
                             getAudienceCount();
                         });
                         selectedQuestion().Gender.subscribe(function (value) {
                             getAudienceCount();
                         });
                         selectedQuestion().SurveyQuestionTargetLocation.subscribe(function (value) {
                             getAudienceCount();
                         });
                         selectedQuestion().SurveyQuestionTargetCriteria.subscribe(function (value) {
                             getAudienceCount();
                         });
                     },
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
                    selectedLangIncludeExclude: selectedLangIncludeExclude,
                    selectedIndustryIncludeExclude:selectedIndustryIncludeExclude,
                    selectedLocationLat: selectedLocationLat,
                    selectedLocationLong: selectedLocationLong,
                    addLanguage: addLanguage,
                    addIndustry:addIndustry,
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
                    onSubmitSurveyQuestion: onSubmitSurveyQuestion,
                    canSubmitForApproval:canSubmitForApproval,
                    titleText: titleText,
                    onRemoveIndustry: onRemoveIndustry,
                    getAudienceCount: getAudienceCount,
                    visibleTargetAudience: visibleTargetAudience,
                    totalAudience: totalAudience,
                    reachedAudience: reachedAudience,
                    audienceReachMode: audienceReachMode,
                    bindAudienceReachCount: bindAudienceReachCount
                };
            })()
        };
        return ist.survey.viewModel;
    });
