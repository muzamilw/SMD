﻿/*
    Module with the view model for the Profile Questions
*/
define("pQuestion/pQuestion.viewModel",
    ["jquery", "amplify", "ko", "pQuestion/pQuestion.dataservice", "pQuestion/pQuestion.model", "common/pagination",
     "common/confirmation.viewModel", "common/stripeChargeCustomer.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, stripeChargeCustomer) {
        var ist = window.ist || {};
        ist.ProfileQuestion = {
            viewModel: (function () {
                var view,
                    //  Questions list on LV
                    questions = ko.observableArray([]),
                    //  Question list on Editor for linked questions
                    linkedQuestions = ko.observableArray([]),
                    // Base Data
                    langs = ko.observableArray([]),
                    Modelheading = ko.observable(''),
                    IsShowPriceDiv = ko.observable(false),
                    surveyQuestionList = ko.observableArray([]),
                    surveyquestionList = ko.observableArray([]),
                    countries = ko.observableArray([]),
                    userBaseData = ko.observable({ CurrencySymbol: '', isStripeIntegrated: false }),
                    qGroup = ko.observableArray([]),
                    selectedQuestionCountryList = ko.observableArray([]),
                    professions = ko.observableArray([]),
                    ageRange = ko.observableArray([]),
                    AgeRangeEnd = ko.observable(80),
                    AgeRangeStart = ko.observable(13),
                    ShowAudienceCounter = ko.observable(0),
                    SelectedPvcVal = ko.observable(0),
                    isNewCriteria = ko.observable(true),
                    showCompanyProfileQuestions = ko.observable(false),
                    selectedCriteria = ko.observable(),
                    TemporaryProfileList = ko.observableArray([]),
                    TemporaryQuizQuestions = ko.observableArray([]),
                    TemporarySurveyList = ko.observableArray([]),
                    Gender = ko.observable('1'),
                     AditionalCriteriaMode = ko.observable("1"), //1 = main buttons, 2 = profile questions , 3 = ad linked questions
                    totalAudience = ko.observable(0),
                    audienceReachMode = ko.observable(1),
                    selectedLocationLong = ko.observable(0),
                    GetAllLocationList = ko.observableArray([]),
                    selectedLocationLat = ko.observable(0),
                    genderppc = ko.observable(),
                     SearchProfileQuestion = ko.observable(''),
                    isNewCampaign = ko.observable(false),
                    myQuizQuestions = ko.observableArray([]),
                    profileQuestionList = ko.observable([]),
                    selectedLocation = ko.observable(),
                    selectedLocationRadius = ko.observable(),
                    selectedIndustryIncludeExclude = ko.observable(true),
                    HeaderText = ko.observable(),
                    StatusText = ko.observable(),
                    reachedAudience = ko.observable(0),
                    priorityList = ko.observableArray([0, 1, 2, 3, 4, 5, 6, 7, 8, 9]),
                    selectedLocationIncludeExclude = ko.observable(true),
                    isShowArchiveBtn = ko.observable(true),
                    canSubmitForApproval = ko.observable(true),
                    isShowSurveyAns = ko.observable(false),
                    IsPauseBtnVisible = ko.observable(false),
                    criteriaCount = ko.observable(0),
                    DefaultRangeValue = ko.observable(100),
                    IsOnAddLocationAdded = ko.observable(false),
                    isNewCampaignVisible = ko.observable(false),
                    profileAnswerList = ko.observable([]),
                    questiontype = ko.observableArray([{
                        typeId: 1,
                        typeName: 'Single Choice'
                    }, {
                        typeId: 2,
                        typeName: 'Multiple Choice'
                    }]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(1),
                    // Search Filter value 
                    filterValue = ko.observable(),
                    langfilterValue = ko.observable(41),
                    countryfilterValue = ko.observable(214),
                    qGroupfilterValue = ko.observable(undefined),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                    // Controls visibility of Image/text on answer editor 
                    isAnswerIsImage = ko.observable(false),
                    // Editor View Model
                    editorViewModel = new ist.ViewModel(model.hireGroupImage),
                    //selected Question
                    //selectedQuestion = editorViewModel.itemForEditing,
                     //selected Answer
                    selectedQuestion = ko.observable(new model.question()),
                    selectedAnswer = ko.observable(),
                    ageppc = ko.observable(),
                    GetObj = ko.observable(),
                    isTerminateBtnVisible = ko.observable(true),
                    previewScreenNumber = ko.observable(0),
                    // Random number
                    randomIdForNewObjects = -1,
                    price = ko.observable(0),
					// Advertiser Analytics 
					isAdvertdashboardSurvayVisible = ko.observable(false),
					selectedCampStatusAnalytics = ko.observable(1),
					selecteddateRangeAnalytics = ko.observable(1),
					selectedGranularityAnalytics = ko.observable(1) ,
				    selectedPQIDAnalytics = ko.observable() ,
					PQAnalyticsData = ko.observableArray([]), 
					CampaignTblAnalyticsData = ko.observableArray([]), 
					granularityDropDown = ko.observableArray([{ id: 1, name: "Daily" }, { id: 2, name: "Weekly" }, { id: 3, name: "Monthly" }, { id: 4, name: "Quarterly" }, { id: 5, name: "Yearly" }]),
					DateRangeDropDown  = ko.observableArray([{ id: 1, name: "Last 30 days" }, { id: 2, name: "All Time" }]),
					CampaignStatusDropDown  = ko.observableArray([{ id: 1, name: "Answered" }, { id: 2, name: "Skipped" }]),
				    CampaignRatioAnalyticData = ko.observable(1), 
					openAdvertiserDashboardSurvayScreen = function () {
					getSurvayAnalytics();
					$("#ddGranularityDropDown").removeAttr("disabled");
					$("#ddDateRangeDropDown").removeAttr("disabled");
					$("#ddCampaignStatusDropDown").removeAttr("disabled");
					isAdvertdashboardSurvayVisible(true);
				},
					
				getSurvayAnalytics = function () {
					dataservice.getSurvayAnalytics({
						PQId: selectedPQIDAnalytics(),
						CampStatus : selectedCampStatusAnalytics(),
						dateRange :selecteddateRangeAnalytics(),
						Granularity : selectedGranularityAnalytics(),
					},{
						success: function (data) {
							if (data != null) {
							PQAnalyticsData.removeAll();
							ko.utils.arrayPushAll(PQAnalyticsData(), data.lineCharts);
							PQAnalyticsData.valueHasMutated();
							CampaignRatioAnalyticData(data.pieCharts);
							CampaignTblAnalyticsData.removeAll();
							ko.utils.arrayPushAll(CampaignTblAnalyticsData(), data.tbl);
							CampaignTblAnalyticsData.valueHasMutated();
							}
						},
						error: function (response) {

                        }
					});
					
				},					
					
					CloseSurvayAnalyticView = function () {
					isAdvertdashboardSurvayVisible(false);
					CampaignRatioAnalyticData(1);
				},
					
					//End Advertiser Analytics 
					
					
                    //Get Questions
                    getQuestions = function (defaultCountryId, defaultLanguageId) {
                        dataservice.searchProfileQuestions(
                            {
                                ProfileQuestionFilterText: filterValue(),
                                LanguageFilter: langfilterValue() || defaultLanguageId,
                                QuestionGroupFilter: qGroupfilterValue(),
                                CountryFilter: countryfilterValue() || defaultCountryId,
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc(),
                                fmode: fmodevar
                            },
                            {
                                success: function (data) {

                                    questions.removeAll();

                                    ko.utils.arrayPushAll(professions(), data.Professions);

                                    professions.valueHasMutated();

                                    _.each(data.ProfileQuestions, function (item) {


                                        questions.push(model.questionServertoClientMapper(SetStatusForQuestion(item)));

                                        //GetAllLocationList.removeAll();
                                        //GetAllLocationList().push(item.ProfileQuestionTargetLocation);
                                        //GetAllLocationList.valueHasMutated();

                                        //GetAllLocationList.push(item.ProfileQuestionTargetLocation);

                                        //ko.utils.arrayPushAll(GetAllLocationList(), item.ProfileQuestionTargetLocation);

                                    });

                                    pager().totalCount(data.TotalCount);
                                },
                                error: function () {
                                    toastr.error("Failed to load profile questions!");
                                }
                            });
                    },
                      GetAudienceCount = function (val) {
                          while (/(\d+)(\d{3})/.test(val.toString())) {
                              val = val.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
                          }
                          return val;

                      },
                     updateSurveryItem = function (item) {

                         _.each(langs(), function (language) {
                             if (language.LanguageId == item.LanguageId) {
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
                             item.StatusValue = "Approval Rejected"; canSubmitForApproval(true);
                         }
                         item.CreatedBy = DilveredPercentage(item);
                         if (item.ResultClicks == null)
                             item.ResultClicks = 0;
                         return item;
                     },

                       DilveredPercentage = function (item) {
                           var percent = 0.0;
                           if (item.AsnswerCount != null && item.AsnswerCount > 0 && item.AnswerNeeded != null && item.AnswerNeeded > 0) {
                               percent = (item.AsnswerCount / item.AnswerNeeded) * 100;
                           }
                           return Math.round(percent);
                       },
                    SetStatusForQuestion = function (item) {

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
                            item.StatusValue = "Approval Rejected"; canSubmitForApproval(true);
                        }
                        item.CreatedBy = DilveredPercentage(item);
                        return item;
                    },

                     //Get Base Data for Questions
                    getBasedata = function () {

                        dataservice.getBaseData(null, {
                            success: function (baseDataFromServer) {
                                langs.removeAll();
                                countries.removeAll();
                                qGroup.removeAll();
                                linkedQuestions.removeAll();
                                userBaseData(baseDataFromServer.objBaseData);
                                ko.utils.arrayPushAll(langs(), baseDataFromServer.LanguageDropdowns);
                                ko.utils.arrayPushAll(countries(), baseDataFromServer.CountryDropdowns);
                                ko.utils.arrayPushAll(qGroup(), baseDataFromServer.ProfileQuestionGroupDropdowns);
                                ko.utils.arrayPushAll(linkedQuestions(), baseDataFromServer.ProfileQuestionDropdowns);

                                langs.valueHasMutated();
                                countries.valueHasMutated();
                                qGroup.valueHasMutated();
                                linkedQuestions.valueHasMutated();

                                langfilterValue(41);
                                countryfilterValue(214);
                            },
                            error: function () {
                                toastr.error("Failed to load base data!");
                            }
                        });
                    },
                     onRemoveIndustry = function (item) {
                         // Ask for confirmation
                         confirmation.afterProceed(function () {
                             //selectedQuestion().ProfileQuestionTargetCriteria.remove(item);

                             item.IsDeleted(true);

                             toastr.success("Removed Successfully!");
                         });
                         confirmation.show();
                     },
                    // Search Filter 
                    filterProfileQuestion = function () {
                        pager().reset();
                        getQuestions();
                    },
                    getQuestionsByFilter = function () {
                        pager().reset();
                        getQuestions();
                    },
                      enableControls = function (mode) {
                          $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                          $("#btnSubmitForApproval,.lang_delSurvey,.table-link").css("display", "inline-block");
                          $("input,button,textarea,a,select,#closeBtn,#btnPauseCampaign").removeAttr('disabled');
                      },
                    onRemoveLocation = function (item) {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            deleteLocation(item);
                            // build location dropdown

                            selectedQuestionCountryList.removeAll();

                            _.each(selectedQuestion().ProfileQuestionTargetLocation(), function (item) {

                                addCountryToCountryList(item.CountryID(), item.Country());
                            });
                        });
                        confirmation.show();

                    },
                    deleteLocation = function (item) {

                        if (item.CountryID() == userBaseData().CountryId && item.CityID() == userBaseData().CityId) {
                            toastr.error("You cannot remove your home town or country!");
                        } else {
                            selectedQuestion().ProfileQuestionTargetLocation.remove(item);
                            toastr.success("Removed Successfully!");
                        }

                    },
                       addCountryToCountryList = function (country, name) {

                           if (country != undefined) {

                               var matcharry = ko.utils.arrayFirst(selectedQuestionCountryList(), function (item) {
                                   return item.id == country;
                               });


                               if (matcharry == null) {
                                   selectedQuestionCountryList.push({ id: country, name: name });
                               }
                           }
                       },
                findLocationsInCountry = function (id) {

                    var list = ko.utils.arrayFilter(selectedQuestion().ProfileQuestionTargetLocation(), function (prod, i) {

                        return prod.CountryID() == id;
                    });
                    return list;

                },
                getQuestionByFilter = function () {

                    if (AditionalCriteriaMode() == 2) {

                        if (SearchProfileQuestion() != '') {

                            profileQuestionList(TemporaryProfileList());

                            var list = ko.utils.arrayFilter(profileQuestionList(), function (prod) {
                                return prod.Question.toLowerCase().indexOf(SearchProfileQuestion().toLowerCase()) != -1;
                            });
                            profileQuestionList().clear;
                            profileQuestionList(list);
                        }
                        else {
                            profileQuestionList.clear;
                            profileQuestionList(TemporaryProfileList());
                        }
                    }
                    else if (AditionalCriteriaMode() == 3) {

                        if (SearchProfileQuestion() != '') {
                            myQuizQuestions(TemporaryQuizQuestions());
                            var list = ko.utils.arrayFilter(myQuizQuestions(), function (prod) {

                                return prod.VerifyQuestion.toLowerCase().indexOf(SearchProfileQuestion().toLowerCase()) != -1;
                            });
                            myQuizQuestions().clear;
                            myQuizQuestions(list);
                        }
                        else {
                            myQuizQuestions.clear;
                            myQuizQuestions(TemporaryQuizQuestions());
                        }

                    }

                    else if (AditionalCriteriaMode() == 4) {

                        if (SearchProfileQuestion() != '') {
                            surveyquestionList(TemporarySurveyList());
                            var list = ko.utils.arrayFilter(surveyquestionList(), function (prod) {

                                return prod.DisplayQuestion.toLowerCase().indexOf(SearchProfileQuestion().toLowerCase()) != -1;
                            });
                            surveyquestionList().clear;
                            surveyquestionList(list);
                        }
                        else {
                            surveyquestionList.clear;
                            surveyquestionList(TemporarySurveyList());
                        }

                    }
                }

                // Add new Profile Question
                addNewProfileQuestion = function () {
                    $("#panelArea,#topArea,#Heading_div").css("display", "none");
                    HeaderText("New Survey Question");




                    StatusText("Draft");
                    isTerminateBtnVisible(false);
                    isNewCampaign(true);
                    isShowArchiveBtn(false);
                    IsPauseBtnVisible(false);

                    canSubmitForApproval(true);
                    selectedQuestion(new model.question());
                    selectedQuestion().Gender("1");

                    selectedQuestion().AgeRangeStart(13);
                    selectedQuestion().statusValue('Draft');
                    selectedQuestion().AgeRangeEnd(80);
                    selectedQuestion().reset();
                    selectedQuestion().ProfileQuestionTargetCriteria([]);
                    selectedQuestion().ProfileQuestionTargetLocation([]);

                    // Set Country Id and Language Id for now as UK and English
                    selectedQuestion().countryId(214);
                    selectedQuestion().languageId(41);
                    selectedQuestion().penalityForNotAnswering(0);

                    if (!reachedAudience() > 0) {
                        getAudienceCountForAdd(selectedQuestion());
                    }
                    else {
                        selectedQuestion().answerNeeded(reachedAudience());
                    }

                    
                    previewScreenNumber(1);
                  //  getAudienceCount();
                    //buildParentSQList();
                    bindAudienceReachCount();
                    selectedQuestion().reset();
                    view.initializeTypeahead();
                    isEditorVisible(true);
                },
                getAudienceCountForAdd = function (SelectedQuestion) {
                    var countryIds = '', cityIds = '', countryIdsExcluded = '', cityIdsExcluded = '';
                    var educationIds = '', educationIdsExcluded = '';
                    _.each(selectedQuestion().ProfileQuestionTargetLocation(), function (item) {
                        if (item.CityID() == 0 || item.CityID() == null) {
                            if (item.IncludeorExclude() == '0') {
                                if (countryIdsExcluded == '') {
                                    countryIdsExcluded += item.CountryID();
                                } else {
                                    countryIdsExcluded += ',' + item.CountryID();
                                }
                            } else {
                                if (countryIds == '') {
                                    countryIds += item.CountryID();
                                } else {
                                    countryIds += ',' + item.CountryID();
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
                    _.each(selectedQuestion().ProfileQuestionTargetCriteria(), function (item) {
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
                            if (item.IncludeorExclude() == '0') {
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
                        else if (item.Type() == 5) {
                            if (item.IncludeorExclude() == '0') {
                                if (educationIdsExcluded == '') {
                                    educationIdsExcluded += item.EducationId();
                                } else {
                                    educationIdsExcluded += ',' + item.EducationId();
                                }
                            } else {
                                if (educationIds == '') {
                                    educationIds += item.EducationId();
                                } else {
                                    educationIds += ',' + item.EducationId();
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
                        surveyAnswerIdsExcluded: surveyAnswerIdsExcluded,
                        educationIds: educationIds,
                        educationIdsExcluded: educationIdsExcluded
                    };

                    dataservice.getAudienceData(surveyData, {
                        success: function (data) {

                            reachedAudience(data.MatchingUsers);

                            ShowAudienceCounter(GetAudienceCount(data.MatchingUsers));
                            totalAudience(data.AllUsers);
                            SelectedQuestion.answerNeeded(data.MatchingUsers);
                            var percent = data.MatchingUsers / data.AllUsers;
                            if (percent < 0.20) {
                                audienceReachMode(1);
                            } else if (percent < 0.70) {
                                audienceReachMode(2);
                            } else {
                                audienceReachMode(3);
                            }
                            var dialPercent = percent * 180;
                            if (dialPercent > 90)
                                dialPercent -= 90;
                            else
                                dialPercent = (90 - dialPercent) * -1;
                            $(".meterPin").css("-webkit-transform", "rotate(" + dialPercent + "deg)");
                        },
                        error: function (response) {
                            //toastr.error("Error while getting audience count.");
                        }
                    });
                 
                },

                 resetLocations = function () {
                     $("#searchCampaignLocations").val("");
                     selectedLocationRadius("");
                 },
                   buildParentSQList = function () {
                       if (profileQuestionList().length == 0) {
                           dataservice.getBaseData({
                               RequestId: 4,
                               QuestionId: 0,
                               SQID: selectedQuestion().SQID()
                           }, {
                               success: function (data) {
                                   if (data != null) {
                                       surveyQuestionList([]);
                                       ko.utils.arrayPushAll(profileQuestionList(), data.ProfileQuestions);
                                       profileQuestionList.valueHasMutated();
                                   }
                               },
                               error: function (response) {
                               }
                           });
                       }
                   },
                    addCountryToCountryList = function (country, name) {
                        if (country != undefined) {

                            var matcharry = ko.utils.arrayFirst(selectedQuestionCountryList(), function (item) {

                                return item.id == country;
                            });

                            if (matcharry == null) {
                                selectedQuestionCountryList.push({ id: country, name: name });
                            }
                        }
                    },

                // Close Editor 
                //closeEditDialog = function () {
                //    $("#panelArea,#topArea").css("display", "block");
                //    if (!hasChangesOnQuestion()) {
                //        isEditorVisible(false);
                //        return;
                //    }
                // Ask for confirmation
                //confirmation.afterProceed(function () {
                //        selectedQuestion().answers.removeAll();
                //        selectedQuestion(undefined);
                //        isEditorVisible(false);
                //  });
                //  confirmation.show();
                //},
                // On editing of existing PQ
                onEditProfileQuestion = function (item) {
					selectedPQIDAnalytics(item.qId());
                    IsPauseBtnVisible(false);
                    canSubmitForApproval(false);
				
                    $("#panelArea,#topArea,#Heading_div").css("display", "none");

                    AgeRangeStart(13);
                    AgeRangeEnd(80);
                    isTerminateBtnVisible(false);
                    isShowArchiveBtn(false);
                    if (item.status() == 1 || item.status() == 2 || item.status() == 3 || item.status() == 4 || item.status() == null || item.status() == 7 || item.status() == 9) {
                        canSubmitForApproval(true);
                    }
                    getQuestionAnswer(item.qId());
                    selectedQuestion(item);
                    if (selectedQuestion().status() == 1) {
                        selectedQuestion().statusValue("Draft");
                    } else if (selectedQuestion().status() == 2) {
                        canSubmitForApproval(false);

                        $("input,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 


                        $("#CloseBtn").removeAttr('disabled');

                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                        //  $("#saveBtn").css("display", "none")
                        $("#btnCancel,#btnPauseCampaign,.btnClose").removeAttr('disabled');

                        selectedQuestion().statusValue("Submitted for Approval");

                    } else if (selectedQuestion().status() == 3) {
                        $("input,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                        //$("#saveBtn").css("display", "none");
                        //$("#btnPauseCampaign").css("display", "inline-block");
                        //$("#btnCancel,#btnPauseCampaign,#btnCopyCampaign,#btnStopAndTerminate").removeAttr('disabled');
                        selectedQuestion().statusValue("Live");
                        isTerminateBtnVisible(true);
                        isNewCampaignVisible(true);
                        canSubmitForApproval(false);
                        IsPauseBtnVisible(true);

                    } else if (selectedQuestion().status() == 4) {
                        $("input,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                       // $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                        $("#btnResumeCampagin").css("display", "inline-block");
                        //    $("#saveBtn").css("display", "none");
                        //$("#btnResumeCampagin").css("display", "none");
                        $("#btnCancel,#btnCopyCampaign,#btnStopAndTerminate").removeAttr('disabled');
                        selectedQuestion().statusValue("Paused");
                        isTerminateBtnVisible(true);
                        isNewCampaignVisible(true);
                        canSubmitForApproval(false);
                    } else if (selectedQuestion().status() == 5) {
                        selectedQuestion().statusValue("Completed");
                    } else if (selectedQuestion().status() == 6) {
                        canSubmitForApproval(true);
                        selectedQuestion().statusValue("Approval Rejected");
                    } else if (selectedQuestion().status() == 7) {
                        selectedQuestion().statusValue("Remove");
                        $("input,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                        //$("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                        // $("#saveBtn").css("display", "none");
                        canSubmitForApproval(false);
                        $("#btnPauseCampaign").css("display", "none");
                        $("#btnCancel,#btnPauseCampaign,#btnCopyCampaign,#btnArchive").removeAttr('disabled');
                        //isNewCampaignVisible(true);
                        //isShowArchiveBtn(true);
                    } else if (item.status == 9) {
                        item.statusValue = ("Completed");
                    } else if (item.status == 8) {
                        item.statusValue = ("Archived");
                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                        // $("#saveBtn").css("display", "none");
                        $("#btnPauseCampaign").css("display", "inline-block");
                        $("#btnCancel,#btnPauseCampaign,#btnCopyCampaign,#btnArchive").removeAttr('disabled');
                        isNewCampaignVisible(true);
                        isShowArchiveBtn(true);
                    }

                    isEditorVisible(true);
                    gotoScreen(1);
                    HeaderText(item.questionString());
                    StatusText(item.statusValue());
                    isTerminateBtnVisible(false);
                    view.initializeTypeahead();
                    isShowArchiveBtn(false);

                    //if (item.status() == 1 || item.status() == 2 || item.status() == 3 || item.status() == 4 || item.status() == null || item.status() == 7 || item.status() == 9) {
                    //    canSubmitForApproval(true);
                    //}
                    SelectedPvcVal(item.answerNeeded());

                    console.log(item.ProfileQuestionTargetCriteria());
                    selectedQuestion().reset();

                },
                   SavePassChanges = function () {
                       if (selectedQuestion() != undefined)
                           onSaveProfileQuestion(4);

                       //$("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                       $("#btnSubmitForApproval,#saveBtn,.lang_delSurvey,.table-link").css("display", "inline-block");
                       //$("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign").removeAttr('disabled');
                   },
                     SaveResumeChanges = function () {
                         if (selectedQuestion() != undefined)
                             onSaveProfileQuestion(3);

                         //$("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                         $("#btnSubmitForApproval,#saveBtn,.lang_delSurvey,.table-link").css("display", "inline-block");
                         //$("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign").removeAttr('disabled');
                     },
                   terminateCampaign = function () {
                       confirmation.messageText("Are you sure you want to remove this Survey ? This action cannot be undone.");
                       confirmation.show();
                       confirmation.afterCancel(function () {
                           confirmation.hide();
                       });
                       confirmation.afterProceed(function () {
                           if (selectedQuestion() != undefined)
                               onSaveProfileQuestion(7);
                       });
                   },

             getSelectedItems = function (items, pqid) {
                 return ko.utils.arrayFilter(items, function (item, index) {

                     return item[index].PQID == pqid;
                 });
             },
                // On Edit PQ, Get PQ Answer & linked Question 
                getQuestionAnswer = function (profileQuestionId) {

                    dataservice.getPqAnswer(
                       {
                           ProfileQuestionId: profileQuestionId
                       },
                       {
                           success: function (answers) {

                               selectedQuestion().answers.removeAll();

                               _.each(answers, function (item) {
                                   selectedQuestion().answers.push(model.questionAnswerServertoClientMapper(item));
                               });
                               selectedQuestion().reset();

                               // var List = answers[0].ProfileQuestion.ProfileQuestionTargetCriteria;

                               //   selectedQuestion().ProfileQuestionTargetLocation.push(answers[0].ProfileQuestion.ProfileQuestionTargetLocation)


                               selectedQuestion().ProfileQuestionTargetCriteria.removeAll();

                               selectedQuestion().ProfileQuestionTargetLocation.removeAll();




                               // ko.utils.arrayPushAll(selectedQuestion().ProfileQuestionTargetLocation(), answers[0].ProfileQuestion.ProfileQuestionTargetLocation);


                               _.each(answers[0].ProfileQuestion.ProfileQuestionTargetLocation, function (itemtemp, i) {

                                   selectedQuestion().ProfileQuestionTargetLocation.push(new model.ProfileQuestionTargetLocation.Create({
                                       CountryId: itemtemp.CountryID,
                                       Country: itemtemp.Country,
                                       PqId: itemtemp.PQID,
                                       Radius: itemtemp.Radius,
                                       Id: itemtemp.ID,
                                       CityId: itemtemp.CityID,
                                       City: itemtemp.City
                                   }));
                               });





                               _.each(selectedQuestion().ProfileQuestionTargetLocation(), function (item) {
                                   addCountryToCountryList(item.CountryID(), item.Country());
                               });



                               _.each(answers[0].ProfileQuestion.ProfileQuestionTargetCriteria, function (itemtemp, i) {
                                   selectedQuestion().ProfileQuestionTargetCriteria.push(new model.ProfileQuestionTargetCriteria.Create({
                                       Industry: itemtemp.Industry,
                                       IndustryId: itemtemp.IndustryID,
                                       IncludeorExclude: itemtemp.IncludeorExclude == true ? 1 : 0,
                                       Type: itemtemp.Type,
                                       PQID: itemtemp.PQID,
                                       questionString: itemtemp.questionString,
                                       answerString: itemtemp.answerString,
                                       PQQuestionID: itemtemp.PQQuestionID,
                                       ID: itemtemp.ID,
                                       AdCampaignID: itemtemp.AdCampaignID,
                                       LinkedSQID: itemtemp.LinkedSQID,
                                       LinkedSQAnswer: itemtemp.LinkedSQAnswer,
                                       PQAnswerID: itemtemp.PQAnswerID,
                                       AdCampaignAnswer: itemtemp.AdCampaignAnswer


                                   }));
                               });
                               selectedQuestion().reset();
                           },
                           error: function () {
                               toastr.error("Failed to load profile question!");
                           }
                       });
                },
                        addNewProfessionCriteria = function () {

                            if ($("#ddpIndustory").val() != "") {

                                var matchedprofessionRec = ko.utils.arrayFirst(professions(), function (arrayitem) {

                                    return arrayitem.IndustryId == $("#ddpIndustory").val();
                                });
                                if (matchedprofessionRec != null) {
                                    addIndustry(matchedprofessionRec);
                                }
                            }
                        },
                        addIndustry = function (selected) {

                            selectedQuestion().ProfileQuestionTargetCriteria.push(new model.ProfileQuestionTargetCriteria.Create({
                                Industry: selected.IndustryName,
                                IndustryId: selected.IndustryId,
                                IncludeorExclude: parseInt(selectedIndustryIncludeExclude()),
                                Type: 4,
                                //PQID: selectedQuestion().qId()
                            }));
                            $("#searchIndustries").val("");

                        },
                // Delete Handler PQ
                onDeleteProfileQuestion = function (item) {
                    // Ask for confirmation
                    confirmation.afterProceed(function () {
                        deleteProfileQuestion(item);
                    });
                    confirmation.show();
                },
                // Delete PQ
                deleteProfileQuestion = function (item) {
                    dataservice.deleteProfileQuestion(item.convertToServerData(), {

                        success: function () {
                            var newObjtodelete = questions.find(function (temp) {
                                return item.qId() == temp.qId();
                            });
                            questions.remove(newObjtodelete);
                            toastr.success("Deleted Successfully!");
                        },
                        error: function () {
                            toastr.error("Failed to delete!");
                        }
                    });
                },
                // Make Filters Claer
                clearFilters = function () {
                    // Language and country are hidden for now
                    //langfilterValue(undefined);
                    //countryfilterValue(undefined);
                    qGroupfilterValue(undefined);
                    filterValue(undefined);
                    filterProfileQuestion();
                },
                // Add new Answer
                addNewAnswer = function () {
                    var obj = new model.questionAnswer();
                    obj.type("1");
                    obj.pqAnswerId(randomIdForNewObjects);
                    randomIdForNewObjects--;
                    selectedAnswer(obj);
                },
                answerTypeChangeHandler = function (item) {
                    //var tfff = this;
                    //return true;
                },
                onEditQuestionAnswer = function (item) {
                    selectedAnswer(item);
                },
                  gotoScreen = function (number) {
                      //  toastr.error("Validation.");
                      previewScreenNumber(number);
                  },
                doBeforeSaveAnswer = function () {
                    var isValid = true;
                    if (!selectedAnswer().isValid()) {
                        selectedAnswer().errors.showAllMessages();
                        isValid = false;
                    }
                    return isValid;
                },
                //
                onSaveNewAnswer = function () {
                    if (!doBeforeSaveAnswer()) {
                        return;
                    }
                    var newOnj = selectedAnswer();
                    var objId = newOnj.pqAnswerId();
                    if (objId < 0) {
                        var newlyAddedobj = selectedQuestion().answers.find(function (ans) {
                            return ans.pqAnswerId() == objId;
                        });
                        selectedQuestion().answers.remove(newlyAddedobj);
                        selectedQuestion().answers.push(newOnj);
                    } else {
                        var existingAns = selectedQuestion().answers.find(function (ans) {
                            return ans.pqAnswerId() == objId;
                        });
                        existingAns.answerString(newOnj.answerString());
                        existingAns.imagePath(newOnj.imagePath());
                        existingAns.linkedQuestion1Id(newOnj.linkedQuestion1Id());
                        existingAns.question1String(model.setAnswerString(existingAns.linkedQuestion1Id(), existingAns));
                        existingAns.linkedQuestion2Id(newOnj.linkedQuestion2Id());
                        existingAns.question2String(model.setAnswerString(existingAns.linkedQuestion2Id(), existingAns));
                        existingAns.linkedQuestion3Id(newOnj.linkedQuestion3Id());
                        existingAns.question3String(model.setAnswerString(existingAns.linkedQuestion3Id(), existingAns));
                        existingAns.linkedQuestion4Id(newOnj.linkedQuestion4Id());
                        existingAns.question4String(model.setAnswerString(existingAns.linkedQuestion4Id(), existingAns));
                        existingAns.linkedQuestion5Id(newOnj.linkedQuestion5Id());
                        existingAns.question5String(model.setAnswerString(existingAns.linkedQuestion5Id(), existingAns));
                        existingAns.linkedQuestion6Id(newOnj.linkedQuestion6Id());
                        existingAns.question6String(model.setAnswerString(existingAns.linkedQuestion6Id(), existingAns));
                        existingAns.type(newOnj.type());

                    }

                    view.hideAnswerDialog();
                },
                // To do before save
                doBeforeSavepq = function () {

                    var isValid = true;
                    if (!selectedQuestion().isValid()) {
                        selectedQuestion().errors.showAllMessages();
                        isValid = false;
                    }
                    if (selectedQuestion().answers().length === 0) {
                        isValid = false;
                        toastr.info("Question must have atleast 2 answers");
                    }
                    if ((selectedQuestion().answers().length) % 2 !== 0) {
                        isValid = false;
                        toastr.info("Answers must be even in number");
                    }
                    if (selectedQuestion().hasLinkedQuestions()) {
                        var answerWithLinkedQtns = selectedQuestion().answers.find(function (answer) {
                            return answer.linkedQustionsCount() > 0;
                        });
                        if (!answerWithLinkedQtns) {
                            isValid = false;
                            toastr.info("Linked Questions should have atleast 1 question linked");
                        }
                    }
                    return isValid;
                },
                // Save Question / Add 
                SaveChanges = function () {
                    if (userBaseData().IsSpecialAccount == true) {
                        onSaveProfileQuestion(2);
                    }
                    else {
                        if (userBaseData().isStripeIntegrated == false) {

                            stripeChargeCustomer.show(function () {
                                userBaseData().isStripeIntegrated = true;
                                onSaveProfileQuestion(2);
                            }, 2000, 'Enter your details');


                        } else {
                            onSaveProfileQuestion(2); (2);
                        }
                    }

                },
                SaveAsDraft = function () {
                    onSaveProfileQuestion(1);
                },
                onSaveProfileQuestion = function (mode) {
                    debugger;
                    if (!doBeforeSavepq()) {
                        return;
                    }
                    var serverAnswers = [];
                    _.each(selectedQuestion().answers(), function (item) {
                        if (item !== null && typeof item === 'object') {
                            serverAnswers.push(item.convertToServerData());
                        } else {
                            serverAnswers.push(item().convertToServerData());
                        }
                    });

                    selectedQuestion().status(mode);

                    var serverQuestion = selectedQuestion().convertToServerData();


                    serverQuestion.ProfileQuestionAnswers = serverAnswers;

                    dataservice.saveProfileQuestion(serverQuestion, {

                        success: function (obj) {

                            var newAssigendGroup = qGroup.find(function (temp) {
                                return obj.ProfileGroupId == temp.ProfileGroupId;
                            });


                            selectedQuestion().questionString(obj.Question);
                            selectedQuestion().priority(obj.Priority);
                            selectedQuestion().hasLinkedQuestions(obj.HasLinkedQuestions);
                            selectedQuestion().qId(obj.PqId);
                            // Update Linked Questions
                            linkedQuestions.push({ PqId: obj.PqId, Question: obj.Question });
                            isEditorVisible(false);
                            toastr.success("Saved Successfully.");

                            $("#topArea,#panelArea,#Heading_div").css("display", "block");

                            CloseContent();
                            selectedQuestion().ProfileQuestionTargetCriteria.removeAll();
                        },
                        error: function () {
                            toastr.error("Failed to save!");
                        }
                    });
                },
                // Delete Answer
                onDeleteQuestionAnswer = function (itemTobeDeleted) {
                    // Ask for confirmation
                    confirmation.afterProceed(function () {
                        deleteAnswer(itemTobeDeleted);
                    });
                    confirmation.show();
                },
                // Delete answer 
                deleteAnswer = function (itemTobeDeleted) {
                    var newObjtodelete = selectedQuestion().answers.find(function (temp) {
                        return itemTobeDeleted.pqAnswerId() == temp.pqAnswerId();
                    });
                    selectedQuestion().answers.remove(newObjtodelete);
                },
                // Has Changes
                hasChangesOnQuestion = ko.computed(function () {
                    if (selectedQuestion() == undefined) {
                        return false;
                    }
                    return (selectedQuestion().hasChanges());
                }),

                // show / hide add answers button
                CanAddAnswers = function () {
                    if (selectedQuestion().answers().length == 6) {
                        return false;
                    } else {
                        return true;
                    }
                },
                // Filter Linked Questions on edit of Profile Question 
                filteredLinkedQuestions = ko.computed(function () {
                    if (!selectedQuestion()) {
                        return linkedQuestions();
                    }
                    // Return all but that is being edited
                    return linkedQuestions.filter(function (temp) {
                        return selectedQuestion().qId() !== temp.PqId;
                    });
                }),
                onEditCriteria = function (item) {
                    debugger;
                    isNewCriteria(false);
                    var val = item.PQAnswerID() + 0;
                    if (item.Type() == "1") {

                        //    editCriteriaHeading("Edit Profile Criteria");
                        dataservice.getFilterBaseData({
                            RequestId: 3,
                            QuestionId: item.PQQuestionID(),
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
                    }
                    else if (item.Type() == "2") {
                        if (surveyquestionList().length == 0) {
                            dataservice.getFilterBaseData({
                                RequestId: 6,
                                QuestionId: 0,
                            }, {
                                success: function (data) {

                                    if (data != null) {

                                        surveyquestionList([]);
                                        ko.utils.arrayPushAll(surveyquestionList(), data.SurveyQuestions);
                                        surveyquestionList.valueHasMutated();

                                        var matchSurveyQuestion = ko.utils.arrayFirst(surveyquestionList(), function (survey) {
                                            return survey.SQID == item.LinkedSQID();
                                        });
                                        selectedCriteria(item);
                                        selectedCriteria().profileQuestRightImageSrc(matchSurveyQuestion.LeftPicturePath);
                                        selectedCriteria().profileQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);

                                    }

                                },
                                error: function (response) {

                                }
                            });
                        }
                        else {

                            var matchSurveyQuestion = ko.utils.arrayFirst(surveyquestionList(), function (survey) {
                                return survey.SQID == item.LinkedSQID();
                            });
                            selectedCriteria(item);
                            selectedCriteria().profileQuestLeftImageSrc(matchSurveyQuestion.LeftPicturePath);
                            selectedCriteria().profileQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);
                        }



                    }

                    else {
                        selectedCriteria(item);

                        if (myQuizQuestions().length == 0) {
                            dataservice.getFilterBaseData({
                                RequestId: 12,
                                QuestionId: 0,
                            }, {
                                success: function (data) {
                                    if (data != null) {
                                        myQuizQuestions([]);
                                        ko.utils.arrayPushAll(myQuizQuestions(), data.AdCampaigns);
                                        myQuizQuestions.valueHasMutated();
                                        var matchSurveyQuestion = ko.utils.arrayFirst(myQuizQuestions(), function (survey) {
                                            return survey.CampaignId == item.AdCampaignID();
                                        });
                                        selectedCriteria().profileQuestLeftImageSrc(matchSurveyQuestion.Answer1);
                                        selectedCriteria().profileQuestRightImageSrc(matchSurveyQuestion.Answer2);
                                    }
                                },
                                error: function (response) {

                                }
                            });
                        } else {
                            var matchSurveyQuestion = ko.utils.arrayFirst(myQuizQuestions(), function (survey) {
                                return survey.CampaignId == item.AdCampaignID();
                            });
                            selectedCriteria().profileQuestLeftImageSrc(matchSurveyQuestion.Answer1);
                            selectedCriteria().profileQuestRightImageSrc(matchSurveyQuestion.Answer2);
                            // adjust item
                        }
                    }
                },
                // get sorted array of answers 
                getSortedAnswers = ko.computed(function () {

                    if (selectedQuestion() == null)
                        return null;
                    return selectedQuestion().answers().sort(function (left, right) {

                        var leftOrder = 100, rightOrder = 100;
                        if (right.sortOrder() != null)
                            rightOrder = right.sortOrder();
                        if (left.sortOrder() != null)
                            leftOrder = left.sortOrder();
                        return leftOrder == rightOrder ?
                             null :
                             (leftOrder < rightOrder ? -1 : 1);
                    });
                }),

            onDeleteCriteria = function (item) {




                confirmation.afterProceed(function () {
                    //selectedQuestion().ProfileQuestionTargetCriteria.remove(item);

                    //  item.IsDeleted(true);
                    selectedQuestion().ProfileQuestionTargetCriteria.remove(item);
                    toastr.success("Removed Successfully!");
                });
                confirmation.show();



                //selectedQuestion().ProfileQuestionTargetCriteria.remove(item);

                //if (item.Type() == "1") {
                //    var matchedProfileCriteria = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (arrayitem) {

                //        return arrayitem.PQID() == item.PQID()
                //    });

                //    if (matchedProfileCriteria == null) {
                //        if (UserAndCostDetail().OtherClausePrice != null) {
                //            pricePerclick(pricePerclick() - UserAndCostDetail().OtherClausePrice);

                //        }
                //    }
                //} else if (item.Type() == "2") {
                //    var matchedSurveyCriteria = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (arrayitem) {

                //        return arrayitem.SQID() == item.SQID();
                //    });

                //    if (matchedSurveyCriteria == null) {
                //        if (UserAndCostDetail().OtherClausePrice != null) {
                //            pricePerclick(pricePerclick() - UserAndCostDetail().OtherClausePrice);

                //        }
                //    }
                //}

            },

                onAddLocation = function (item) {

                    IsOnAddLocationAdded(true);

                    selectedLocation().Radius = (selectedLocationRadius);
                    selectedLocation().IncludeorExclude = (selectedLocationIncludeExclude);

                    selectedQuestion().ProfileQuestionTargetLocation.push(new model.ProfileQuestionTargetLocation.Create({
                        CountryId: selectedLocation().CountryID,
                        CityId: selectedLocation().CityID,
                        Radius: selectedLocation().Radius(),
                        Country: selectedLocation().Country,
                        City: selectedLocation().City,
                        IncludeorExclude: selectedLocation().IncludeorExclude(),
                        //PQID: selectedQuestion().qId(),
                        Latitude: selectedLocation().Latitude,
                        Longitude: selectedLocation().Longitude,
                    }));

                    addCountryToCountryList(selectedLocation().CountryID, selectedLocation().Country);

                    resetLocations();
                },
                 visibleTargetAudience = function (mode) {

                     if (mode != undefined) {

                         var matcharry = ko.utils.arrayFirst(selectedQuestion().ProfileQuestionTargetCriteria(), function (item) {

                             return item.Type == mode;
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
             getAudienceCount = function () {
                 var countryIds = '', cityIds = '', countryIdsExcluded = '', cityIdsExcluded = '';
                 var educationIds = '', educationIdsExcluded = '';
                 _.each(selectedQuestion().ProfileQuestionTargetLocation(), function (item) {
                     if (item.CityID() == 0 || item.CityID() == null) {
                         if (item.IncludeorExclude() == '0') {
                             if (countryIdsExcluded == '') {
                                 countryIdsExcluded += item.CountryID();
                             } else {
                                 countryIdsExcluded += ',' + item.CountryID();
                             }
                         } else {
                             if (countryIds == '') {
                                 countryIds += item.CountryID();
                             } else {
                                 countryIds += ',' + item.CountryID();
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
                 _.each(selectedQuestion().ProfileQuestionTargetCriteria(), function (item) {
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
                         if (item.IncludeorExclude() == '0') {
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
                     else if (item.Type() == 5) {
                         if (item.IncludeorExclude() == '0') {
                             if (educationIdsExcluded == '') {
                                 educationIdsExcluded += item.EducationId();
                             } else {
                                 educationIdsExcluded += ',' + item.EducationId();
                             }
                         } else {
                             if (educationIds == '') {
                                 educationIds += item.EducationId();
                             } else {
                                 educationIds += ',' + item.EducationId();
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
                     surveyAnswerIdsExcluded: surveyAnswerIdsExcluded,
                     educationIds: educationIds,
                     educationIdsExcluded: educationIdsExcluded
                 };

                 dataservice.getAudienceData(surveyData, {
                     success: function (data) {

                         reachedAudience(data.MatchingUsers);

                         ShowAudienceCounter(GetAudienceCount(data.MatchingUsers));
                         totalAudience(data.AllUsers);
                         var percent = data.MatchingUsers / data.AllUsers;
                         if (percent < 0.20) {
                             audienceReachMode(1);
                         } else if (percent < 0.70) {
                             audienceReachMode(2);
                         } else {
                             audienceReachMode(3);
                         }
                         var dialPercent = percent * 180;
                         if (dialPercent > 90)
                             dialPercent -= 90;
                         else
                             dialPercent = (90 - dialPercent) * -1;
                         $(".meterPin").css("-webkit-transform", "rotate(" + dialPercent + "deg)");
                     },
                     error: function (response) {
                         //toastr.error("Error while getting audience count.");
                     }
                 });
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
                 selectedQuestion().ProfileQuestionTargetLocation.subscribe(function (value) {
                     getAudienceCount();
                     // update map 
                     //  buildMap();
                 });
                 selectedQuestion().ProfileQuestionTargetCriteria.subscribe(function (value) {

                     getAudienceCount();
                 });
             }, buildMap = function () {
                 $(".locMap").css("display", "none");
                 var initialized = false;
                 _.each(selectedQuestion().ProfileQuestionTargetLocation(), function (item) {
                     //   $(".locMap").css("display", "inline-block");
                     //clearRadiuses();
                     if (item.CityID() == 0 || item.CityID() == null) {
                         addCountryMarker(item.Country());
                     } else {
                         if (!initialized)
                             initializeMap(parseFloat(item.Longitude()), parseFloat(item.Latitude()));
                         initialized = true;
                         var included = true;
                         if (item.IncludeorExclude() == '0') {
                             included = false;
                         }
                         addPointer(parseFloat(item.Longitude()), parseFloat(item.Latitude()), item.City(), parseFloat(item.Radius()), included);
                     }
                 });
             },
              showAdditionCriteria = function (mode) {
                  AditionalCriteriaMode(mode);
              },
            showAdditionUserCriteria = function () {

                //  isNewCriteria(true);
                //  var objProfileCriteria = new model.ProfileQuestionTargetCriteria();

                //  objProfileCriteria.Type("1");
                //  objProfileCriteria.IncludeorExclude("1");
                //  criteriaCount(criteriaCount() + 1);
                ////  objProfileCriteria.CriteriaID(criteriaCount());
                //  selectedCriteria(objProfileCriteria);


                //  if (profileQuestionList().length == 0) {
                //      dataservice.getBaseDataForProfileQuestions({
                //          RequestId: 2,
                //          QuestionId: 0,
                //      }, {
                //          success: function (data) {
                //              if (data != null) {
                //                  profileQuestionList([]);
                //                  ko.utils.arrayPushAll(profileQuestionList(), data.ProfileQuestions);
                //                  profileQuestionList.valueHasMutated();
                //              }

                //          },
                //          error: function (response) {

                //          }
                //      });
                //  }
                //  AditionalCriteriaMode(2);


                ////////////////////////
                isNewCriteria(true);
                var objProfileCriteria = new model.ProfileQuestionTargetCriteria();
                Modelheading('Profile Questions');
                objProfileCriteria.Type("1");
                objProfileCriteria.IncludeorExclude("1");
                criteriaCount(criteriaCount() + 1);
                objProfileCriteria.ID(criteriaCount());
                selectedCriteria(objProfileCriteria);


                if (profileQuestionList().length == 0) {
                    dataservice.getFilterBaseData({
                        RequestId: 2,
                        QuestionId: 0,
                    }, {
                        success: function (data) {
                            if (data != null) {
                                profileQuestionList([]);
                                ko.utils.arrayPushAll(profileQuestionList(), data.ProfileQuestions);
                                profileQuestionList.valueHasMutated();
                                TemporaryProfileList.clear;
                                TemporaryProfileList(profileQuestionList());

                            }

                        },
                        error: function (response) {

                        }
                    });
                }
                AditionalCriteriaMode(2);
                showCompanyProfileQuestions(false);
            },
             onChangeProfileQuestion = function (item) {
                 if (item == null)
                     return;
                 var selectedQuestionId = item.PqId;

                 dataservice.getBaseDataForProfileQuestions({
                     RequestId: 3,
                     QuestionId: selectedQuestionId,
                 }, {
                     success: function (data) {
                         if (data != null) {
                             if (profileAnswerList().length > 0) {
                                 profileAnswerList([]);
                             }
                             _.each(data.ProfileQuestionAnswers, function (question) {
                                 question.PQID = item.PqId;
                             });
                             ko.utils.arrayPushAll(profileAnswerList(), data.ProfileQuestionAnswers);
                             profileAnswerList.valueHasMutated();


                         }

                     },
                     error: function (response) {

                     }
                 });
             },

             onChangeSurveyQuestion = function (item) {
                 //var selectedSurveyQuestionId = $("#ddsurveyQuestion").val();
                 //var matchSurveyQuestion = ko.utils.arrayFirst(surveyQuestionList(), function (item) {
                 //    return item.SQID == selectedSurveyQuestionId;
                 //});
                 //item.SQAnswer("1");
                 //item.profileQuestLeftImageSrc(matchSurveyQuestion.LeftPicturePath);
                 //item.profileQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);
                 //$("#surveyAnswersContainer").show();
                 //isShowSurveyAns(true);
             },
             updateSurveyCriteria = function (type, item) {

                 selectedCriteria().AdCampaignAnswer(type);
                 if (type == 1) {
                     selectedCriteria().answerString(selectedCriteria().profileQuestLeftImageSrc());
                 } else {
                     selectedCriteria().answerString(selectedCriteria().profileQuestRightImageSrc());
                 }

                 $(".close").click();
             },
                 saveProfileQuestion = function (item) {
                     debugger;
                     var selectedQuestionstring = $(".active .parent-list-title").text();
                     selectedCriteria().questionString(selectedQuestionstring);
                     selectedCriteria().PQID(item.PQID);

                     var selectedQuestionAnswerstring = item.Answer;

                     selectedCriteria().answerString(selectedQuestionAnswerstring);

                     selectedCriteria().PQAnswerID(item.PqAnswerId);

                     var matchedProfileCriteriaRec = ko.utils.arrayFirst(selectedQuestion().ProfileQuestionTargetCriteria, function (arrayitem) {

                         return arrayitem.PQID() == item.PQID
                     });

                     if (matchedProfileCriteriaRec == null) {
                         //if (UserAndCostDetail().OtherClausePrice != null) {
                         //    pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);

                         //}
                         selectedQuestion().ProfileQuestionTargetCriteria.push(new model.ProfileQuestionTargetCriteria.Create({
                             Type: 1,
                             PqId: selectedCriteria().PQID(),
                             PQAnswerID: selectedCriteria().PQAnswerID(),
                             SqId: selectedCriteria().SQID(),
                             //SQAnswer: selectedCriteria().SQAnswer(),
                             questionString: selectedCriteria().questionString(),
                             answerString: selectedCriteria().answerString(),
                             IncludeorExclude: selectedCriteria().IncludeorExclude(),
                             PQQuestionID: selectedCriteria().PQID(),

                             //CampaignId: campaignModel().CampaignID,
                             //criteriaPrice: UserAndCostDetail().OtherClausePrice
                         }));
                     }
                     else {
                         selectedQuestion().ProfileQuestionTargetCriteria.push(new model.ProfileQuestionTargetCriteria.Create({
                             Type: 1,
                             PqId: selectedCriteria().PQID(),
                             PQAnswerID: selectedCriteria().PQAnswerID(),
                             SqId: selectedCriteria().SQID(),
                             //SQAnswer: selectedCriteria().SQAnswer(),
                             questionString: selectedCriteria().questionString(),
                             answerString: selectedCriteria().answerString(),
                             IncludeorExclude: selectedCriteria().IncludeorExclude(),
                             PQQuestionID: selectedCriteria().PQID()
                             //CampaignId: campaignModel().CampaignID,
                             //criteriaPrice: 0
                         }));
                     }

                     selectedQuestion().ProfileQuestionTargetCriteria.valueHasMutated();

                     $(".close").click();

                 },
                 updateProfileQuestion = function (item) {

                     selectedCriteria().answerString(item.Answer);
                     selectedCriteria().PQAnswerID(item.PQAnswerID);
                     $(".close").click();
                 },
                    closeEditDialog = function () {


                        if (selectedQuestion().hasChanges()) {

                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(function () {
                                SaveAsDraft();
                            });
                            confirmation.afterCancel(function () {

                                CloseContent();
                            });

                            confirmation.show();

                        }
                        else {
                            CloseContent();
                        }
                        selectedQuestion().reset();
                    },

                    CloseContent = function () {
                        isEditorVisible(false); 
						CloseSurvayAnalyticView();
						enableControls();
                        $("#panelArea,#topArea,#headlabel,#Heading_div").css("display", "block");
                        filterProfileQuestion();

                    }
            ,
            saveCriteria = function (type, item) {

                var selectedQuestionstring = item.VerifyQuestion;

                selectedCriteria().questionString(selectedQuestionstring);
                if (type == 1) {
                    selectedCriteria().answerString(item.Answer1);
                } else {
                    selectedCriteria().answerString(item.Answer2);
                }
                selectedCriteria().AdCampaignID(item.CampaignId);

                var matchedSurveyCriteriaRec = null;

                //_.each(selectedQuestion().ProfileQuestionTargetCriteria(), function (itemarry) {

                //    if (itemarry.QuizCampaignId() == item.CampaignId) {

                //        matchedSurveyCriteriaRec = itemarry;
                //    }
                //});



                selectedQuestion().ProfileQuestionTargetCriteria().push(new model.ProfileQuestionTargetCriteria.Create({
                    Type: 6,
                    PqId: selectedCriteria().PQID(),
                    PqAnswerId: selectedCriteria().PQAnswerID(),
                    AdCampaignAnswer: type,
                    questionString: selectedCriteria().questionString(),
                    answerString: selectedCriteria().answerString(),
                    IncludeorExclude: selectedCriteria().IncludeorExclude(),
                    PQQuestionID: selectedCriteria().PQID(),
                    AdCampaignID: selectedCriteria().AdCampaignID()
                }));
                selectedQuestion().ProfileQuestionTargetCriteria.valueHasMutated();


                if (!isNewCriteria()) {
                    selectedQuestion().ProfileQuestionTargetCriteria.remove(selectedCriteria());
                } else {

                }
                $(".close").click();
                //isCriteriaEditable(false);
            }

            ,

                        showAdditionQuizCriteria = function () {
                            
                            Modelheading('Your Quiz Questions');
                            //   selectedCriteria(null);
                            //   isNewCriteria(true);
                            //   var objProfileCriteria = new model.ProfileQuestionTargetCriteria();

                            //   objProfileCriteria.Type("1");
                            //   objProfileCriteria.IncludeorExclude("1");

                            //   selectedCriteria(objProfileCriteria);

                            //if (myQuizQuestions().length == 0) {
                            //    dataservice.getBaseDataForProfileQuestions({
                            //        RequestId: 12,
                            //        QuestionId: 0,
                            //    }, {
                            //        success: function (data) {
                            //            if (data != null) {
                            //                myQuizQuestions([]);
                            //                ko.utils.arrayPushAll(myQuizQuestions(), data.AdCampaigns);
                            //                myQuizQuestions.valueHasMutated();
                            //            }
                            //        },
                            //        error: function (response) {

                            //        }
                            //    });
                            //}
                            //AditionalCriteriaMode(3);
                            isNewCriteria(true);
                            var objProfileCriteria = new model.ProfileQuestionTargetCriteria();

                            objProfileCriteria.Type("1");
                            objProfileCriteria.IncludeorExclude("1");
                            criteriaCount(criteriaCount() + 1);
                            objProfileCriteria.ID(criteriaCount());
                            selectedCriteria(objProfileCriteria);

                            if (myQuizQuestions().length == 0) {
                                dataservice.getFilterBaseData({
                                    RequestId: 12,
                                    QuestionId: 0,
                                }, {
                                    success: function (data) {
                                        if (data != null) {
                                            myQuizQuestions([]);
                                            ko.utils.arrayPushAll(myQuizQuestions(), data.AdCampaigns);
                                            myQuizQuestions.valueHasMutated();
                                            TemporaryQuizQuestions.clear;
                                            TemporaryQuizQuestions(myQuizQuestions());
                                            debugger;
                                        }

                                    },
                                    error: function (response) {

                                    }
                                });
                            }
                            AditionalCriteriaMode(3);
                        },
                           showAdditionUserSurveyCriteria = function () {

                               isNewCriteria(true);
                               var objProfileCriteria = new model.ProfileQuestionTargetCriteria();
                               Modelheading('Polls');
                               objProfileCriteria.Type("1");
                               objProfileCriteria.IncludeorExclude("1");
                               criteriaCount(criteriaCount() + 1);
                               objProfileCriteria.ID(criteriaCount());
                               selectedCriteria(objProfileCriteria);


                               if (surveyquestionList().length == 0) {
                                   dataservice.getFilterBaseData({
                                       RequestId: 6,
                                       QuestionId: 0,
                                   }, {
                                       success: function (data) {

                                           if (data != null) {

                                               surveyquestionList([]);
                                               ko.utils.arrayPushAll(surveyquestionList(), data.SurveyQuestions);
                                               surveyquestionList.valueHasMutated();
                                               TemporarySurveyList.clear;
                                               TemporarySurveyList(surveyquestionList());
                                           }

                                       },
                                       error: function (response) {

                                       }
                                   });
                               }
                               AditionalCriteriaMode(4);
                           },

                                showAdditionalSurveyQuestions = function () {
                                    Modelheading('Survey Questions');
                                    isNewCriteria(true);
                                    var objProfileCriteria = new model.ProfileQuestionTargetCriteria();

                                    objProfileCriteria.Type("1");
                                    objProfileCriteria.IncludeorExclude("1");
                                    criteriaCount(criteriaCount() + 1);
                                    objProfileCriteria.ID(criteriaCount());
                                    selectedCriteria(objProfileCriteria);


                                    if (profileQuestionList().length == 0) {
                                        dataservice.getFilterBaseData({
                                            RequestId: 2,
                                            QuestionId: 0,
                                        }, {
                                            success: function (data) {
                                                if (data != null) {
                                                    profileQuestionList([]);
                                                    ko.utils.arrayPushAll(profileQuestionList(), data.ProfileQuestions);
                                                    console.log(data.profileQuestionList)
                                                    profileQuestionList.valueHasMutated();

                                                    TemporaryProfileList.clear;
                                                    TemporaryProfileList(profileQuestionList());

                                                }

                                            },
                                            error: function (response) {

                                            }
                                        });

                                    }

                                    AditionalCriteriaMode(2);
                                    showCompanyProfileQuestions(true);
                                },

                      getProductPrice = function () {
                          dataservice.getProduct({
                              success: function (data) {
                                  price(data.SetupPrice);

                              },
                              error: function () {
                                  toastr.error("Failed to load product.");
                              }
                          });
                      },
                        totalPrice = ko.computed(function () {
                            IsShowPriceDiv(true);
                            var ansNeeeded;
                            var calculatePrice
                            if (selectedQuestion() == undefined) {
                                return 0;
                            }
                            else {
                                ansNeeeded = selectedQuestion().answerNeeded();
                                if (ansNeeeded > 0 && ansNeeeded <= 1000) {
                                    calculatePrice = price();
                                    selectedQuestion().amountCharge(calculatePrice);
                                    return "$ " + calculatePrice + " usd";
                                }
                                if (ansNeeeded > 1000 && ansNeeeded % 1000 == 0) {
                                    var val = ansNeeeded / 1000;
                                    calculatePrice = val * price();
                                    selectedQuestion().amountCharge(calculatePrice);
                                    return "$ " + calculatePrice + " usd";
                                }
                                else {
                                    if (ansNeeeded > 1000 && ansNeeeded % 1000 != 0) {
                                        var val2 = ansNeeeded / 1000
                                        calculatePrice = price() * Math.ceil(val2);
                                        selectedQuestion().amountCharge(calculatePrice);
                                        return "$ " + calculatePrice + " usd";
                                    }

                                }
                            }
                        }),
                           saveSurveyQuestionCriteria = function (type, item) {

                               var selectedQuestionstring = item.DisplayQuestion;
                               selectedCriteria().questionString(selectedQuestionstring);

                               selectedCriteria().SQID(item.SQID);


                               if (type == 1) {
                                   selectedCriteria().answerString(item.LeftPicturePath);
                               } else {
                                   selectedCriteria().answerString(item.RightPicturePath);
                               }

                               var matchedSurveyCriteriaRec = null;

                               _.each(selectedQuestion().ProfileQuestionTargetCriteria(), function (itemarry) {

                                   if (itemarry.SQID == item.SQID) {

                                       matchedSurveyCriteriaRec = itemarry;
                                   }
                               });


                               if (isNewCriteria()) {
                                   if (matchedSurveyCriteriaRec == null) {

                                       selectedQuestion().ProfileQuestionTargetCriteria.push(new model.ProfileQuestionTargetCriteria.Create({
                                           Type: 2,
                                           LinkedSqId: item.SQID,
                                           LinkedSqAnswer: type,

                                           //PQAnswerId: selectedCriteria().PQAnswerID(),
                                           //QuizCampaignId: item.CampaignId,
                                           //QuizAnswerId: type,
                                           questionString: selectedCriteria().questionString(),
                                           answerString: selectedCriteria().answerString(),
                                           IncludeorExclude: selectedCriteria().IncludeorExclude()
                                       }));

                                   } else {


                                       selectedQuestion().ProfileQuestionTargetCriteria.push(new model.ProfileQuestionTargetCriteria.Create({
                                           Type: 2,
                                           LinkedSqId: item.SQID,
                                           LinkedSqAnswer: type,
                                           // QuizCampaignId: item.CampaignId,
                                           // QuizAnswerId: type,
                                           questionString: selectedCriteria().questionString(),
                                           answerString: selectedCriteria().answerString(),

                                           IncludeorExclude: selectedCriteria().IncludeorExclude(),
                                           //  CampaignId: selectedQuestion().CampaignID,
                                           criteriaPrice: 0
                                       }));

                                   }
                               }
                               $(".close").click();
                               isShowSurveyAns(false);
                           },

                                updateSurveyCriteriass = function (type, item) {
                                    selectedCriteria().LinkedSQAnswer(type);
                                    if (type == 1) {
                                        selectedCriteria().answerString(selectedCriteria().profileQuestLeftImageSrc());
                                    } else {
                                        selectedCriteria().answerString(selectedCriteria().profileQuestRightImageSrc());
                                    }
                                    $(".close").click();
                                },
                // Initialize the view model
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(pagination.Pagination({ PageSize: 10 }, questions, getQuestions));
                    // Base Data Call
                    getBasedata();
                    getProductPrice();
                    // First request for LV
                    getQuestions(214, 41);
                    for (var i = 10; i < 81; i++) {
                        var text = i.toString();
                        if (i == 110)
                            text += "+";
                        ageRange.push({ value: i.toString(), text: text });
                    }

                    ageRange.push({ value: 120, text: "80+" });
                };
                return {
                    initialize: initialize,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    selectedQuestion: selectedQuestion,
                    questions: questions,
                    getQuestions: getQuestions,
                    getBasedata: getBasedata,
                    isEditorVisible: isEditorVisible,
                    filterProfileQuestion: filterProfileQuestion,
                    filterValue: filterValue,
                    addNewProfileQuestion: addNewProfileQuestion,
                    closeEditDialog: closeEditDialog,
                    onEditProfileQuestion: onEditProfileQuestion,
                    onDeleteProfileQuestion: onDeleteProfileQuestion,
                    langs: langs,
                    countries: countries,
                    qGroup: qGroup,
                    langfilterValue: langfilterValue,
                    countryfilterValue: countryfilterValue,
                    qGroupfilterValue: qGroupfilterValue,
                    clearFilters: clearFilters,
                    priorityList: priorityList,
                    questiontype: questiontype,
                    isAnswerIsImage: isAnswerIsImage,
                    addNewAnswer: addNewAnswer,
                    answerTypeChangeHandler: answerTypeChangeHandler,
                    selectedAnswer: selectedAnswer,
                    linkedQuestions: linkedQuestions,
                    onEditQuestionAnswer: onEditQuestionAnswer,
                    onSaveNewAnswer: onSaveNewAnswer,
                    onSaveProfileQuestion: onSaveProfileQuestion,
                    onDeleteQuestionAnswer: onDeleteQuestionAnswer,
                    hasChangesOnQuestion: hasChangesOnQuestion,
                    CanAddAnswers: CanAddAnswers,
                    filteredLinkedQuestions: filteredLinkedQuestions,
                    getSortedAnswers: getSortedAnswers,
                    getQuestionsByFilter: getQuestionsByFilter,
                    SetStatusForQuestion: SetStatusForQuestion,
                    previewScreenNumber: previewScreenNumber,
                    gotoScreen: gotoScreen,
                    selectedQuestionCountryList: selectedQuestionCountryList,
                    ageRange: ageRange,
                    AgeRangeStart: AgeRangeStart,
                    AgeRangeEnd: AgeRangeEnd,
                    Gender: Gender,
                    selectedLocationIncludeExclude: selectedLocationIncludeExclude,
                    selectedLocationRadius: selectedLocationRadius,
                    selectedLocationLat: selectedLocationLat,
                    selectedLocationLong: selectedLocationLong,
                    genderppc: genderppc,
                    ageppc: ageppc,
                    addNewProfessionCriteria: addNewProfessionCriteria,
                    professions: professions,
                    HeaderText: HeaderText,
                    StatusText: StatusText,
                    SelectedPvcVal: SelectedPvcVal,
                    isTerminateBtnVisible: isTerminateBtnVisible,
                    isShowArchiveBtn: isShowArchiveBtn,
                    canSubmitForApproval: canSubmitForApproval,
                    resetLocations: resetLocations,
                    onAddLocation: onAddLocation,
                    addCountryToCountryList: addCountryToCountryList,
                    findLocationsInCountry: findLocationsInCountry,
                    selectedLocation: selectedLocation,
                    onRemoveLocation: onRemoveLocation,
                    deleteLocation: deleteLocation,
                    reachedAudience: reachedAudience,
                    totalAudience: totalAudience,
                    audienceReachMode: audienceReachMode,
                    selectedIndustryIncludeExclude: selectedIndustryIncludeExclude,
                    userBaseData: userBaseData,
                    onRemoveIndustry: onRemoveIndustry,
                    showAdditionCriteria: showAdditionCriteria,
                    showAdditionUserCriteria: showAdditionUserCriteria,
                    profileQuestionList: profileQuestionList,
                    AditionalCriteriaMode: AditionalCriteriaMode,
                    isNewCriteria: isNewCriteria,
                    criteriaCount: criteriaCount,
                    selectedCriteria: selectedCriteria,
                    profileAnswerList: profileAnswerList,
                    onChangeProfileQuestion: onChangeProfileQuestion,
                    saveProfileQuestion: saveProfileQuestion,
                    SaveChanges: SaveChanges,
                    visibleTargetAudience: visibleTargetAudience,
                    onDeleteCriteria: onDeleteCriteria,
                    myQuizQuestions: myQuizQuestions,
                    showAdditionQuizCriteria: showAdditionQuizCriteria,
                    saveCriteria: saveCriteria,
                    isShowSurveyAns: isShowSurveyAns,
                    onEditCriteria: onEditCriteria,
                    updateSurveyCriteria: updateSurveyCriteria,
                    updateProfileQuestion: updateProfileQuestion,
                    totalPrice: totalPrice,
                    isNewCampaign: isNewCampaign,
                    SaveAsDraft: SaveAsDraft,
                    showAdditionUserSurveyCriteria: showAdditionUserSurveyCriteria,
                    showAdditionalSurveyQuestions: showAdditionalSurveyQuestions,
                    showCompanyProfileQuestions: showCompanyProfileQuestions,
                    surveyQuestionList: surveyQuestionList,
                    surveyquestionList: surveyquestionList,
                    saveSurveyQuestionCriteria: saveSurveyQuestionCriteria,
                    updateSurveyCriteriass: updateSurveyCriteriass,
                    isNewCampaignVisible: isNewCampaignVisible,
                    SavePassChanges: SavePassChanges,
                    SaveResumeChanges: SaveResumeChanges,
                    IsPauseBtnVisible: IsPauseBtnVisible,
                    terminateCampaign: terminateCampaign,
                    ShowAudienceCounter: ShowAudienceCounter,
                    SearchProfileQuestion: SearchProfileQuestion,
                    getQuestionByFilter: getQuestionByFilter,
                    Modelheading: Modelheading,
                    IsShowPriceDiv: IsShowPriceDiv,
                    DefaultRangeValue: DefaultRangeValue,
					isAdvertdashboardSurvayVisible : isAdvertdashboardSurvayVisible,
					selectedCampStatusAnalytics: selectedCampStatusAnalytics,
					selecteddateRangeAnalytics : selecteddateRangeAnalytics,
					selectedGranularityAnalytics : selectedGranularityAnalytics ,
				    selectedPQIDAnalytics : selectedPQIDAnalytics ,
					PQAnalyticsData : PQAnalyticsData, 
					granularityDropDown : granularityDropDown,
					DateRangeDropDown : DateRangeDropDown,
					CampaignStatusDropDown : CampaignStatusDropDown,
				    openAdvertiserDashboardSurvayScreen:openAdvertiserDashboardSurvayScreen,
					getSurvayAnalytics:getSurvayAnalytics,
					CloseSurvayAnalyticView:CloseSurvayAnalyticView,
					CampaignRatioAnalyticData:CampaignRatioAnalyticData,
					CampaignTblAnalyticsData:CampaignTblAnalyticsData
                };
            })()
        };
        return ist.ProfileQuestion.viewModel;
    });
