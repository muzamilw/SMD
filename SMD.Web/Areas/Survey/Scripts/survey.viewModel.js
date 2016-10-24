/*
    Module with the view model for the Profile Questions
*/
define("survey/survey.viewModel",
    ["jquery", "amplify", "ko", "survey/survey.dataservice", "common/pagination", "survey/survey.model", "common/confirmation.viewModel", "common/stripeChargeCustomer.viewModel"],
    function ($, amplify, ko, dataservice, pagination, model, confirmation, stripeChargeCustomer) {
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
                    Modelheading = ko.observable(""),
                    // Search Filter value 
                    filterValue = ko.observable(),
                    langfilterValue = ko.observable(0),
                    countryfilterValue = ko.observable(0),
                    TemporaryProfileList = ko.observableArray([]),
                    TemporaryQuizQuestions = ko.observableArray([]),
                    TemporarySurveyList = ko.observableArray([]),
                    price = ko.observable(0),
                    SearchProfileQuestion = ko.observable(''),
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
                    selectedEducationIncludeExclude = ko.observable(true),
                    selectedLocationLat = ko.observable(0),
                    selectedLocationLong = ko.observable(0),
                    ShowAudienceCounter = ko.observable(),
                    DefaultRangeValue = ko.observable(100),
                    DefaultCountValue = ko.observable(100),
                    // criteria selection 
                    selectedCriteria = ko.observable(),
                    profileQuestionList = ko.observable([]),
                    surveyQuestionList = ko.observableArray([]),
                    profileAnswerList = ko.observable([]),
                    editCriteriaHeading = ko.observable("Add Profile Criteria"),
                    titleText = ko.observable("New Survey card"),
                    myQuizQuestions = ko.observableArray([]),
                    surveyquestionList = ko.observableArray([]),
                    isNewCriteria = ko.observable(true),
                    canSubmitForApproval = ko.observable(true),
                    isTerminateBtnVisible = ko.observable(true),
                     showCompanyProfileQuestions = ko.observable(false),
                     criteriaCount = ko.observable(0),
                    // age list 
                    ageRange = ko.observableArray([]),
                    AditionalCriteriaMode = ko.observable("1"), //1 = main buttons, 2 = profile questions , 3 = ad linked questions
                    //audience reach
                    reachedAudience = ko.observable(0),
                    isNewCampaign = ko.observable(false),
                    //total audience
                    totalAudience = ko.observable(0),
                    selectedQuestionCountryList = ko.observableArray([]),
                    // audience reach mode 
                    audienceReachMode = ko.observable(1),
                    userBaseData = ko.observable({ CurrencySymbol: '', isStripeIntegrated: false }),
                    setupPrice = ko.observable(0),
                    // unique country list used to bind location dropdown
                    isShowSurveyAns = ko.observable(false),
                    errorList = ko.observableArray([]),
                      educations = ko.observableArray([]),
                      professions = ko.observableArray([]),
                    previewScreenNumber = ko.observable(0),
                    isShowArchiveBtn = ko.observable(true),
                    canSubmitForApproval = ko.observable(true),
                    SelectedPvcVal = ko.observable(0),
                    HeaderText = ko.observable(0),
                    StatusValue = ko.observable(0),
                    qStatuses = ko.observableArray([{ id: 0, value: 'All' }, { id: 1, value: 'Draft' }, { id: 2, value: 'Submitted for Approval' }, { id: 3, value: 'Live' }, { id: 4, value: 'Paused' }, { id: 5, value: 'Completed' }, { id: 6, value: 'Rejected' }]);
					statusFilterValue = ko.observable();
				// Advertiser Analytics 
					isAdvertdashboardPollVisible = ko.observable(false),
					selectedCampStatusAnalytics = ko.observable(1),
					selecteddateRangeAnalytics = ko.observable(1),
					selectedGranularityAnalytics = ko.observable(1) ,
				    selectedSQIDAnalytics = ko.observable() ,
					SQAnalyticsData = ko.observableArray([]), 
					CampaignTblAnalyticsData = ko.observableArray([]), 
					CampaignROItblAnalyticData = ko.observableArray([]), 
					granularityDropDown = ko.observableArray([{ id: 1, name: "Daily" }, { id: 2, name: "Weekly" }, { id: 3, name: "Monthly" }, { id: 4, name: "Quarterly" }, { id: 5, name: "Yearly" }]),
					DateRangeDropDown  = ko.observableArray([{ id: 1, name: "Last 30 days" }, { id: 2, name: "All Time" }]),
					CampaignStatusDropDown  = ko.observableArray([{ id: 1, name: "Answered" }, { id: 2, name: "Skipped" }]),
					CampaignRatioAnalyticData = ko.observable(1), 
				    openAdvertiserDashboardPollScreen = function () {
					getSurvayAnalytics();
					$("#ddGranularityDropDown").removeAttr("disabled");
					$("#ddDateRangeDropDown").removeAttr("disabled");
					$("#ddCampaignStatusDropDown").removeAttr("disabled");
					
					isAdvertdashboardPollVisible(true);
				},
				getSurvayAnalytics = function () {
					dataservice.getSurvayAnalytics({
						SQId: selectedSQIDAnalytics(),
						CampStatus : selectedCampStatusAnalytics(),
						dateRange :selecteddateRangeAnalytics(),
						Granularity : selectedGranularityAnalytics(),
					},{
						success: function (data) {
							if (data != null) {
								SQAnalyticsData.removeAll();
								ko.utils.arrayPushAll(SQAnalyticsData(), data.lineCharts);
								SQAnalyticsData.valueHasMutated();
								CampaignRatioAnalyticData(data.pieCharts);
								CampaignTblAnalyticsData.removeAll();
								ko.utils.arrayPushAll(CampaignTblAnalyticsData(), data.tbl);
								CampaignTblAnalyticsData.valueHasMutated();
								
								CampaignROItblAnalyticData.removeAll();
								ko.utils.arrayPushAll(CampaignROItblAnalyticData(), data.pieChartstbl);
								CampaignROItblAnalyticData.valueHasMutated();
								
							}
						},
						error: function (response) {

                        }
					});
					
				},					
					
					ClosePollAnalyticView = function () {
					isAdvertdashboardPollVisible(false);
					CampaignRatioAnalyticData(1);
				},
					
					//End Advertiser Analytics 
			   //Get Questions
                getQuestions = function () {
                    dataservice.searchSurveyQuestions(
                        {
                            SearchText: filterValue(),
                            LanguageFilter: langfilterValue(),
                            CountryFilter: countryfilterValue(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            Status: statusFilterValue(),
                            FirstLoad: false,
                            fmode: isfMode()
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
                        PageNo: pager().currentPage(),
                        fmode: isfMode()
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
                                //populateSurveyQuestions(data);
                                userBaseData(data.objBaseData);
                                setupPrice(data.setupPrice);

                                educations.removeAll();
                                ko.utils.arrayPushAll(educations(), data.Educations);
                                educations.valueHasMutated();

                                professions.removeAll();
                                ko.utils.arrayPushAll(professions(), data.Professions);
                                professions.valueHasMutated();

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

                 if (item.AnswerNeeded != null && item.AnswerNeeded > 0 && item.ResultClicks != null && item.ResultClicks > 0) {
                     percent = (item.ResultClicks / item.AnswerNeeded) * 100;
                 }
                 return Math.round(percent);
             },

                 updateSurveyCriteriass = function (type, item) {
                     selectedCriteria().LinkedSQAnswer(type);
                     if (type == 1) {
                         selectedCriteria().answerString(selectedCriteria().surveyQuestLeftImageSrc());
                     } else {
                         selectedCriteria().answerString(selectedCriteria().surveyQuestRightImageSrc());
                     }
                     $(".close").click();
                 },
                // Search Filter 
                filterSurveyQuestion = function () {
                    getQuestions();
                },
                // Make Filters Claer
                clearFilters = function () {
                    langfilterValue(undefined);
                    countryfilterValue(undefined);
                    filterValue(undefined);
                    statusFilterValue(undefined);
                    getQuestions();
                },
                // Add new Profile Question
                addNewSurvey = function () {
                   
                    $("#panelArea,#topArea,#Heading_div").css("display", "none");
                    selectedQuestionCountryList([]);
                    gotoScreen(1);
                    isTerminateBtnVisible(false);
                    isShowArchiveBtn(false);
                    HeaderText("Add new survey card");
                    StatusValue('');
                    isNewCampaign(true);
                    StatusValue("Draft");
                    selectedQuestion(new model.Survey());
                    selectedQuestion().Gender("1");
                    selectedQuestion().LeftPicturePath("/Images/select_image.jpg");
                    selectedQuestion().RightPicturePath("/Images/select_image.jpg");
                    //selectedQuestion().LeftPicturePath("/images/standardplaceholder.png");
                    //selectedQuestion().RightPicturePath("/images/standardplaceholder.png");
                    selectedQuestion().StatusValue("Draft");
                    selectedQuestion().AgeRangeStart(13);
                    selectedQuestion().AgeRangeEnd(80);

                    if (!reachedAudience() > 0) {
                        getAudienceCountForAdd(selectedQuestion());
                    }
                    else {
                        selectedQuestion().answerNeeded(reachedAudience());
                    }

                    selectedQuestion().reset();
                    selectedQuestion().SurveyQuestionTargetCriteria([]);
                    selectedQuestion().SurveyQuestionTargetLocation([]);

                    buildParentSQList();

                    getAudienceCount();

                    bindAudienceReachCount();
                    isEditorVisible(true);
                    canSubmitForApproval(true);
                    view.initializeTypeahead();

                    selectedQuestionCountryList([]);
                    selectedQuestion().reset();
                    //if (userBaseData().CountryId != null) {
                    //    selectedQuestion().SurveyQuestionTargetLocation.push(new model.SurveyQuestionTargetLocation.Create({
                    //        CountryId: userBaseData().CountryId,
                    //        CityId: userBaseData().CityId,
                    //        Country: userBaseData().Country,
                    //        City: userBaseData().City,
                    //        IncludeorExclude: true,
                    //        Latitude: userBaseData().Latitude,
                    //        Longitude: userBaseData().Longitude,

                    //    }));
                    //    addCountryToCountryList(userBaseData().CountryId, userBaseData().Country);
                    //}
                },
                // Close Editor
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

                var ansNeeeded;
                var calculatePrice
                if (selectedQuestion() == undefined) {
                    return 0;
                }
                else {
                    ansNeeeded = selectedQuestion().answerNeeded();
                    if (ansNeeeded > 0 && ansNeeeded <= 1000) {
                        calculatePrice = price();
                        selectedQuestion().AmountCharged(calculatePrice);
                        return "$ " + calculatePrice + " usd";
                    }
                    if (ansNeeeded > 1000 && ansNeeeded % 1000 == 0) {
                        var val = ansNeeeded / 1000;
                        calculatePrice = val * price();
                        selectedQuestion().AmountCharged(calculatePrice);
                        return "$ " + calculatePrice + " usd";
                    }
                    else {
                        if (ansNeeeded > 1000 && ansNeeeded % 1000 != 0) {
                            var val2 = ansNeeeded / 1000
                            calculatePrice = price() * Math.ceil(val2);
                            selectedQuestion().AmountCharged(calculatePrice);
                            return "$ " + calculatePrice + " usd";
                        }

                    }
                }
            }),
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
				  ClosePollAnalyticView();
				  enableControls();
                  $("#panelArea,#topArea,#Heading_div").css("display", "block");
              },
                SurveyQuestionsByFilter = function () {
                    getQuestions();
                },
            getUrlVars = function () {
                var vars = [], hash;
                var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                for (var i = 0; i < hashes.length; i++) {
                    hash = hashes[i].split('=');
                    vars.push(hash[0]);
                    vars[hash[0]] = hash[1];
                }
                return vars;
            },
            isfMode = function () {
                if (getUrlVars()["m"] == "1")
                    return true;
                else
                    return false;
            },
            gotoScreen = function (number) {

                previewScreenNumber(number);

            },
                // On editing of existing PQ
                onEditSurvey = function (item) {
					selectedSQIDAnalytics(item.SQID());
                    selectedQuestionCountryList([]); $("#panelArea,#topArea,#Heading_div").css("display", "none");
                    gotoScreen(1);
                    isTerminateBtnVisible(false);
                    isShowArchiveBtn(false);
                    if (item.Status() == 1 || item.Status() == 2 || item.Status() == 3 || item.Status() == 4 || item.Status() == null || item.Status() == 7 || item.Status() == 9) {
                        canSubmitForApproval(true);
                    }
                    HeaderText(item.Question());
                    StatusValue(item.StatusValue());
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
                               // build location dropdown
                               selectedQuestionCountryList([]);

                               _.each(selectedQuestion().SurveyQuestionTargetLocation(), function (item) {
                                   addCountryToCountryList(item.CountryID(), item.Country());
                               });

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
                               bindAudienceReachCount();
                               buildMap();
                               isEditorVisible(true);
                               if (item.Status() != 1 && item.Status() != 6 && item.Status() != null) {
                                   if (userBaseData().isUserAddmin == true) {
                                   } else {
                                       disableControls(item.Status());
                                   }

                               }
                               selectedQuestion().reset();
                               //  getParentSurveyList();
                           },
                           error: function () {
                               toastr.error("Failed to load  question!");
                           }
                       });

                    SelectedPvcVal(item.answerNeeded());
                    // }
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
                        // build location dropdown
                        selectedQuestionCountryList.removeAll();
                        _.each(selectedQuestion().SurveyQuestionTargetLocation(), function (item) {
                            addCountryToCountryList(item.CountryID(), item.Country());
                        });
                    });
                    confirmation.show();

                },
                deleteLocation = function (item) {
                    if (item.CountryID() == userBaseData().CountryId && item.CityID() == userBaseData().CityId) {
                        toastr.error("You cannot remove your home town or country!");
                    } else {
                        selectedQuestion().SurveyQuestionTargetLocation.remove(item);
                        toastr.success("Removed Successfully!");
                    }

                },
                //add location
                onAddLocation = function (item) {

                    selectedLocation().Radius = (selectedLocationRadius);
                    selectedLocation().IncludeorExclude = (selectedLocationIncludeExclude);
                    selectedQuestion().SurveyQuestionTargetLocation.push(new model.SurveyQuestionTargetLocation.Create({
                        CountryId: selectedLocation().CountryID,
                        CityId: selectedLocation().CityID,
                        Radius: selectedLocation().Radius(),
                        Country: selectedLocation().Country,
                        City: selectedLocation().City,
                        IncludeorExclude: selectedLocation().IncludeorExclude(),
                        SQID: selectedQuestion().SQID(),
                        Latitude: selectedLocation().Latitude,
                        Longitude: selectedLocation().Longitude,

                    }));
                    addCountryToCountryList(selectedLocation().CountryID, selectedLocation().Country);
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
                  addEducation = function (selected) {
                      selectedQuestion().SurveyQuestionTargetCriteria.push(new model.SurveyQuestionTargetCriteria.Create({
                          Education: selected.Title,
                          EducationId: selected.EducationId,
                          IncludeorExclude: parseInt(selectedEducationIncludeExclude()),
                          Type: 5,
                          SQID: selectedQuestion().SQID()
                      }));
                      $("#searchEducations").val("");
                  },
                // same function used to remove education
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
                isCriteriaEditable = ko.observable(false),
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
               showAdditionUserCriteria = function () {
                   Modelheading('Profile Questions');
                   isNewCriteria(true);
                   var objProfileCriteria = new model.SurveyQuestionTargetCriteria();

                   objProfileCriteria.Type("1");
                   objProfileCriteria.IncludeorExclude("1");
                   criteriaCount(criteriaCount() + 1);
                   objProfileCriteria.ID(criteriaCount());
                   selectedCriteria(objProfileCriteria);


                   if (profileQuestionList().length == 0) {
                       dataservice.getBaseData({
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
                },

            showAdditionalSurveyQuestions = function () {
                Modelheading('Survey Questions');
                isNewCriteria(true);
                var objProfileCriteria = new model.SurveyQuestionTargetCriteria();

                objProfileCriteria.Type("1");
                objProfileCriteria.IncludeorExclude("1");
                criteriaCount(criteriaCount() + 1);
                objProfileCriteria.ID(criteriaCount());
                selectedCriteria(objProfileCriteria);


                if (profileQuestionList().length == 0) {
                    dataservice.getBaseData({
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
                                console.log(data)
                            }

                        },
                        error: function (response) {

                        }
                    });

                }

                AditionalCriteriaMode(2);
                showCompanyProfileQuestions(true);
            },
            showAdditionUserSurveyCriteria = function () {
                Modelheading('Polls');
                isNewCriteria(true);
                var objProfileCriteria = new model.SurveyQuestionTargetCriteria();

                objProfileCriteria.Type("1");
                objProfileCriteria.IncludeorExclude("1");
                criteriaCount(criteriaCount() + 1);
                objProfileCriteria.ID(criteriaCount());
                selectedCriteria(objProfileCriteria);


                if (surveyquestionList().length == 0) {
                    dataservice.getBaseData({
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


            showAdditionQuizCriteria = function () {
                Modelheading('Your Quiz Questions');
                isNewCriteria(true);
                var objProfileCriteria = new model.SurveyQuestionTargetCriteria();

                objProfileCriteria.Type("1");
                objProfileCriteria.IncludeorExclude("1");
                criteriaCount(criteriaCount() + 1);
                objProfileCriteria.ID(criteriaCount());
                selectedCriteria(objProfileCriteria);

                if (myQuizQuestions().length == 0) {
                    dataservice.getBaseData({
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
                            }

                        },
                        error: function (response) {

                        }
                    });
                }
                AditionalCriteriaMode(3);
            },
                // Add new survey Criteria
                //addNewSurveyCriteria = function () {
                //    editCriteriaHeading("Add Survey Criteria");
                //    isNewCriteria(true);
                //    var objSurveyCriteria = new model.SurveyQuestionTargetCriteria();
                //    objSurveyCriteria.Type("2");
                //    objSurveyCriteria.IncludeorExclude("1");
                //    selectedCriteria(objSurveyCriteria);
                //    if (surveyQuestionList().length == 0) {
                //        dataservice.getBaseData({
                //            RequestId: 4,
                //            QuestionId: 0,
                //            SQID:selectedQuestion().SQID()
                //        }, {
                //            success: function (data) {
                //                if (data != null) {
                //                    surveyQuestionList([]);
                //                    ko.utils.arrayPushAll(surveyQuestionList(), data.SurveyQuestions);
                //                    surveyQuestionList.valueHasMutated();
                //                }

                //            },
                //            error: function (response) {

                //            }
                //        });
                //    }
                //},
                saveProfileQuestion = function (item) {
                    var selectedQuestionstring = $(".active .parent-list-title").text();
                    selectedCriteria().questionString(selectedQuestionstring);
                    selectedCriteria().PQID(item.PQID);
                    var selectedQuestionAnswerstring = item.Answer;
                    selectedCriteria().answerString(selectedQuestionAnswerstring);
                    selectedCriteria().PQAnswerID(item.PQAnswerID);


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
                    $(".close").click();
                    isCriteriaEditable(false);
                },
                updateSurveyCriteria = function (type, item) {

                    selectedCriteria().LinkedSQAnswer(type);
                    selectedCriteria().QuizAnswerId(type);
                    if (type == 1) {
                        selectedCriteria().answerString(selectedCriteria().surveyQuestLeftImageSrc());
                    } else {
                        selectedCriteria().answerString(selectedCriteria().surveyQuestRightImageSrc());
                    }
                    $(".close").click();
                },
                 updateProfileQuestion = function (item) {
                     selectedCriteria().answerString(item.Answer);
                     selectedCriteria().PQAnswerID(item.PQAnswerID);
                     $(".close").click();
                 },
                saveCriteria = function (type, item) {

                    // save only survey question
                    var selectedQuestionstring = item.VerifyQuestion;
                    selectedCriteria().questionString(selectedQuestionstring);
                    if (type == 1) {
                        selectedCriteria().answerString(item.Answer1);
                    } else {
                        selectedCriteria().answerString(item.Answer2);
                    }

                    var matchedSurveyCriteriaRec = null;

                    _.each(selectedQuestion().SurveyQuestionTargetCriteria(), function (itemarry) {

                        if (itemarry.QuizCampaignId() == item.CampaignId) {

                            matchedSurveyCriteriaRec = itemarry;
                        }
                    });


                    if (isNewCriteria()) {
                        if (matchedSurveyCriteriaRec == null) {
                            selectedQuestion().SurveyQuestionTargetCriteria.push(new model.SurveyQuestionTargetCriteria.Create({
                                Type: 6,
                                PqId: selectedCriteria().PQID(),
                                PqAnswerId: selectedCriteria().PQAnswerID(),
                                QuizCampaignId: item.CampaignId,
                                QuizAnswerId: type,
                                questionString: selectedCriteria().questionString(),
                                answerString: selectedCriteria().answerString(),
                                IncludeorExclude: selectedCriteria().IncludeorExclude(),
                                CampaignId: selectedQuestion().CampaignID
                            }));

                        } else {


                            selectedQuestion().SurveyQuestionTargetCriteria.push(new model.SurveyQuestionTargetCriteria.Create({
                                Type: 6,
                                PqId: selectedCriteria().PQID(),
                                PqAnswerId: selectedCriteria().PQAnswerID(),
                                QuizCampaignId: item.CampaignId,
                                QuizAnswerId: type,
                                questionString: selectedCriteria().questionString(),
                                answerString: selectedCriteria().answerString(),
                                IncludeorExclude: selectedCriteria().IncludeorExclude(),
                                CampaignId: selectedQuestion().CampaignID
                            }));

                        }
                    }
                    $(".close").click();
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
                    }
                    else if (item.Type() == "2") {

                        if (surveyquestionList().length == 0) {
                            dataservice.getBaseData({
                                RequestId: 6,
                                QuestionId: 0,
                            }, {
                                success: function (data) {

                                    if (data != null) {

                                        surveyquestionList([]);
                                        ko.utils.arrayPushAll(surveyquestionList(), data.SurveyQuestions);
                                        surveyquestionList.valueHasMutated();

                                        var matchSurveyQuestion = ko.utils.arrayFirst(surveyquestionList(), function (survey) {
                                            return survey.SQID == item.SQID();
                                        });
                                        selectedCriteria(item);
                                        selectedCriteria().surveyQuestLeftImageSrc(matchSurveyQuestion.LeftPicturePath);
                                        selectedCriteria().surveyQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);

                                    }

                                },
                                error: function (response) {

                                }
                            });
                        }
                        else {

                            var matchSurveyQuestion = ko.utils.arrayFirst(surveyquestionList(), function (survey) {
                                return survey.SQID == item.SQID();
                            });
                            selectedCriteria(item);
                            selectedCriteria().surveyQuestLeftImageSrc(matchSurveyQuestion.LeftPicturePath);
                            selectedCriteria().surveyQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);
                        }



                    }

                    else {
                        selectedCriteria(item);

                        if (myQuizQuestions().length == 0) {
                            dataservice.getBaseData({
                                RequestId: 12,
                                QuestionId: 0,
                            }, {
                                success: function (data) {
                                    if (data != null) {
                                        myQuizQuestions([]);
                                        ko.utils.arrayPushAll(myQuizQuestions(), data.AdCampaigns);
                                        myQuizQuestions.valueHasMutated();
                                        var matchSurveyQuestion = ko.utils.arrayFirst(myQuizQuestions(), function (survey) {
                                            return survey.CampaignId == item.QuizCampaignId();
                                        });
                                        selectedCriteria().surveyQuestLeftImageSrc(matchSurveyQuestion.Answer1);
                                        selectedCriteria().surveyQuestRightImageSrc(matchSurveyQuestion.Answer2);
                                    }

                                },
                                error: function (response) {

                                }
                            });
                        } else {
                            var matchSurveyQuestion = ko.utils.arrayFirst(myQuizQuestions(), function (survey) {
                                return survey.CampaignId == item.QuizCampaignId();
                            });
                            selectedCriteria().surveyQuestLeftImageSrc(matchSurveyQuestion.Answer1);
                            selectedCriteria().surveyQuestRightImageSrc(matchSurveyQuestion.Answer2);
                            // adjust item
                        }
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

                onChangeProfileQuestion = function (item) {
                    if (item == null)
                        return;
                    var selectedQuestionId = item.PqId;
                    dataservice.getBaseData({
                        RequestId: 3,
                        QuestionId: selectedQuestionId,
                    }, {
                        success: function (data) {
                            if (data != null) {
                                _.each(data.ProfileQuestionAnswers, function (question) {
                                    question.PQID = item.PqId;
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

                //onChangeSurveyQuestion = function (item) {

                //    var selectedSurveyQuestionId = $("#ddsurveyQuestion").val();
                //    var matchSurveyQuestion = ko.utils.arrayFirst(surveyQuestionList(), function (item) {
                //        return item.SQID == selectedSurveyQuestionId;
                //    });
                //    item.LinkedSQAnswer("1");
                //    item.surveyQuestLeftImageSrc(matchSurveyQuestion.LeftPicturePath);
                //    item.surveyQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);
                //    $("#surveyAnswersContainer").show();
                // //   isCriteriaEditable(true);
                //},
                // Has Changes
                hasChangesOnQuestion = ko.computed(function () {
                    if (selectedQuestion() == undefined) {
                        return false;
                    }
                    return (selectedQuestion().hasChanges());
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

                      _.each(selectedQuestion().SurveyQuestionTargetCriteria(), function (itemarry) {

                          if (itemarry.SQID == item.SQID) {

                              matchedSurveyCriteriaRec = itemarry;
                          }
                      });


                      if (isNewCriteria()) {
                          if (matchedSurveyCriteriaRec == null) {

                              selectedQuestion().SurveyQuestionTargetCriteria.push(new model.SurveyQuestionTargetCriteria.Create({
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


                              selectedQuestion().SurveyQuestionTargetCriteria.push(new model.SurveyQuestionTargetCriteria.Create({
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



                // save survey question 
                onSaveSurveyQuestion = function () {
                    if (selectedQuestion().isValid()) {
                        if (ValidateSurvey() == true) {
                            saveSurveyQuestion(1);
                        }

                    } else {
                        selectedQuestion().errors.showAllMessages();
                    }

                },

                 SaveAsDraft = function () {
                     if (selectedQuestion().isValid()) {

                         if (ValidateSurvey() == true) {

                             saveSurveyQuestion(1);

                         }
                         else {

                             if (errorList().length > 0) {

                                 ko.utils.arrayForEach(errorList(), function (errorList) {

                                     toastr.error(errorList.name);
                                 });
                             }
                         }

                     }
                     else {



                         if (isEditorVisible()) {
                             selectedQuestion().errors.showAllMessages();

                             toastr.error("Please fill the required feilds to continue.");
                             if (errorList().length > 0) {
                                 $.each(errorList(), function (key, value) {
                                     toastr.error(value);
                                 });
                             }
                         }

                     }

                 },
                // submit  survey question for approval
                onSubmitSurveyQuestion = function () {

                    if (selectedQuestion().isValid()) {

                        if (ValidateSurvey() == true) {

                            if (reachedAudience() > 0) {
                                if (userBaseData().Status == null || userBaseData().Status == 0)
                                {
                                    confirmation.showOKpopupforinfo();
                                    return;
                                }
                                else
                                {
                                    if (userBaseData().isStripeIntegrated == false) {
                                        stripeChargeCustomer.show(function () {
                                            userBaseData().isStripeIntegrated = true;
                                            saveSurveyQuestion(2);
                                        }, 2000, 'Enter your details');
                                    } else {
                                        saveSurveyQuestion(2);
                                    }
                                }
                            } else {
                                toastr.error("You have no audience against the specified criteria please broad your audience definition.");
                            }

                        }
                        else {

                            if (errorList().length > 0) {

                                ko.utils.arrayForEach(errorList(), function (errorList) {

                                    toastr.error(errorList.name);
                                });
                            }
                        }

                    }
                    else {



                        if (isEditorVisible()) {
                            selectedQuestion().errors.showAllMessages();

                            toastr.error("Please fill the required feilds to continue.");
                            if (errorList().length > 0) {
                                $.each(errorList(), function (key, value) {
                                    toastr.error(value);
                                });
                            }
                        }

                    }

                },
                saveSurveyQuestion = function (mode) {
                    if (selectedQuestion().isValid()) {
                        if (ValidateSurvey() == true) {
                            selectedQuestion().Status(mode);
                            var surveyData = selectedQuestion().convertToServerData();
                            dataservice.addSurveyData(surveyData, {
                                success: function (data) {
                                    isEditorVisible(false);
                                    getQuestions();
                                    toastr.success("Successfully saved.");

                                    $("#Heading_div").css("display", "block");
                                    CloseContent();


                                },
                                error: function (response) {

                                }
                            });
                        }
                    } else {
                        if (isEditorVisible()) {
                            selectedQuestion().errors.showAllMessages();
                            toastr.error("Please fill the required feilds to continue.");
                        }
                    }

                }
                getAudienceCount = function () {

                    var countryIds = '', cityIds = '', countryIdsExcluded = '', cityIdsExcluded = '';
                    var educationIds = '', educationIdsExcluded = '';
                    _.each(selectedQuestion().SurveyQuestionTargetLocation(), function (item) {
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
                        surveyQuestionIdsExcluded = '', surveyAnswerIdsExcluded = '', CampaignQuizIds = '', CampaignQuizAnswerIds = '', CampaignQuizAnswerIdsExcluded = '', CampaignQuizIdsExcluded = '';
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
                        else if (item.Type() == 6) {        //Quiz questions
                            if (item.IncludeorExclude() == '0') {
                                if (CampaignQuizIdsExcluded == '') {
                                    CampaignQuizIdsExcluded += item.QuizCampaignId();
                                } else {
                                    surveyQuestionIdsExcluded += ',' + item.QuizCampaignId();
                                }
                                if (CampaignQuizAnswerIdsExcluded == '') {
                                    CampaignQuizAnswerIdsExcluded += item.QuizAnswerId();
                                } else {
                                    CampaignQuizAnswerIdsExcluded += ',' + item.QuizAnswerId();
                                }
                            } else {
                                if (CampaignQuizIds == '') {
                                    CampaignQuizIds += item.QuizCampaignId();
                                } else {
                                    CampaignQuizIds += ',' + item.QuizCampaignId();
                                }
                                if (CampaignQuizAnswerIds == '') {
                                    CampaignQuizAnswerIds += item.QuizAnswerId();
                                } else {
                                    CampaignQuizAnswerIds += ',' + item.QuizAnswerId();
                                }
                            }
                        }
                    });
                 
                    var ProfileData = {
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
                        educationIdsExcluded: educationIdsExcluded,
                        CampaignQuizIds: CampaignQuizIds,
                        CampaignQuizAnswerIds: CampaignQuizAnswerIds,
                        CampaignQuizAnswerIdsExcluded: CampaignQuizAnswerIdsExcluded,
                        CampaignQuizIdsExcluded: CampaignQuizIdsExcluded
                    };
                    
                    dataservice.getAudienceData(ProfileData, {
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
                          //  toastr.error("Error while getting audience count.");
                        }
                    });
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

                        var list = ko.utils.arrayFilter(selectedQuestion().SurveyQuestionTargetLocation(), function (prod) {

                            return prod.CountryID() == id;
                        });
                        return list;
                    },
                GetAudienceCount = function (val) {
                    while (/(\d+)(\d{3})/.test(val.toString())) {
                        val = val.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
                    }
                    return val;

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
                            // update map 
                            buildMap();
                        });
                        selectedQuestion().SurveyQuestionTargetCriteria.subscribe(function (value) {
                            getAudienceCount();
                        });
                    },
                    buildMap = function () {
                        $(".locMap").css("display", "none");
                        var initialized = false;
                        _.each(selectedQuestion().SurveyQuestionTargetLocation(), function (item) {
                            //   $(".locMap").css("display", "inline-block");
                            clearRadiuses();
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
                     submitResumeData = function () {
                         if (selectedQuestion() != undefined)
                             saveSurveyQuestion(3);

                         //$("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                         //$("#btnSubmitForApproval,#saveBtn,.lang_delSurvey,.table-link").css("display", "inline-block");
                         //$("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign").removeAttr('disabled');
                     },
                    buildParentSQList = function () {
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
                    },
                    ValidateSurvey = function () {

                        if (selectedQuestion().SQID() > 0)
                            return true;
                        errorList.removeAll();


                        if (errorList() == null || errorList().length == 0) {
                            return true;
                        } else {
                            return false;
                        }
                    },
                 SavePassChanges = function () {
                     if (selectedQuestion() != undefined)
                         saveSurveyQuestion(4);

                     //$("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                     $("#btnSubmitForApproval,#saveBtn,.lang_delSurvey,.table-link").css("display", "inline-block");
                     //$("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign").removeAttr('disabled');
                 },
                terminateSaveChanges = function (item) {
                    confirmation.messageText("Are you sure you want to remove this Poll ? This action cannot be undone.");
                    confirmation.show();
                    confirmation.afterCancel(function () {
                        confirmation.hide();
                    });
                    confirmation.afterProceed(function () {
                        if (selectedQuestion() != undefined)
                            saveSurveyQuestion(7);
                    });
                },

                getAudienceCountForAdd = function (SelectedQuestion) {

                    var countryIds = '', cityIds = '', countryIdsExcluded = '', cityIdsExcluded = '';
                    var educationIds = '', educationIdsExcluded = '';
                    _.each(selectedQuestion().SurveyQuestionTargetLocation(), function (item) {
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
                    var ProfileData = {
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

                    dataservice.getAudienceData(ProfileData, {
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
                            toastr.error("Error while getting audience count.");
                        }
                    });
                },

                    disableControls = function (status) {
                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                        //  $("#saveBtn").css("display", "none");
                        $("#saveBtn").removeAttr('disabled');

                        //  $("#closeBtn").removeAttr('disabled');
                        if (status == 3) {
                            $("#btnPauseCampaign").css("display", "inline-block");
                            $("#btnPauseCampaign").removeAttr('disabled');
                        } else if (status == 4) {
                            $("#btnResumeCampagin").css("display", "inline-block");
                            $("#btnResumeCampagin").removeAttr('disabled');
                        }
                        $("#topArea a").removeAttr('disabled');
                    },
                    enableControls = function (mode) {
                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                        $("#btnSubmitForApproval,#saveBtn,.lang_delSurvey,.table-link").css("display", "inline-block");
                        $("input,button,textarea,a,select,#closeBtn,#btnPauseCampaign").removeAttr('disabled');
                    },
                     changeStatus = function (status) {
                         if (selectedQuestion() != undefined)
                             saveSurveyQuestion(status);

                         enableControls()
                     },
                    addNewEducationCriteria = function () {
                        if ($("#ddpEducation").val() != "") {

                            var matchedEducationRec = ko.utils.arrayFirst(educations(), function (arrayitem) {

                                return arrayitem.EducationId == $("#ddpEducation").val();
                            });
                            if (matchedEducationRec != null) {
                                addEducation(matchedEducationRec);
                            }
                        }
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
                // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        for (var i = 10; i < 111; i++) {
                            var text = i.toString();
                            if (i == 110)
                                text += "+";
                            ageRange.push({ value: i.toString(), text: text });
                        }
                        pager(pagination.Pagination({ PageSize: 10 }, questions, getQuestions));
                        // Base Data Call
                        getBasedata();
                        getQuestions();
                        getProductPrice();


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
                    selectedIndustryIncludeExclude: selectedIndustryIncludeExclude,
                    selectedEducationIncludeExclude: selectedEducationIncludeExclude,
                    selectedLocationLat: selectedLocationLat,
                    selectedLocationLong: selectedLocationLong,
                    addLanguage: addLanguage,
                    addIndustry: addIndustry,
                    onRemoveLanguage: onRemoveLanguage,
                    selectedCriteria: selectedCriteria,
                    profileQuestionList: profileQuestionList,
                    profileAnswerList: profileAnswerList,
                    surveyQuestionList: surveyQuestionList,
                    addNewProfileCriteria: addNewProfileCriteria,
                    myQuizQuestions: myQuizQuestions,
                    surveyquestionList: surveyquestionList,
                    //      addNewSurveyCriteria: addNewSurveyCriteria,
                    saveCriteria: saveCriteria,
                    onEditCriteria: onEditCriteria,
                    onDeleteCriteria: onDeleteCriteria,
                    onChangeProfileQuestion: onChangeProfileQuestion,
                    AditionalCriteriaMode: AditionalCriteriaMode,
                    //  onChangeSurveyQuestion: onChangeSurveyQuestion,
                    isCriteriaEditable: isCriteriaEditable,
                    hasChangesOnQuestion: hasChangesOnQuestion,
                    editCriteriaHeading: editCriteriaHeading,
                    isNewCriteria: isNewCriteria,
                    onSaveSurveyQuestion: onSaveSurveyQuestion,
                    onSubmitSurveyQuestion: onSubmitSurveyQuestion,
                    canSubmitForApproval: canSubmitForApproval,
                    saveSurveyQuestionCriteria: saveSurveyQuestionCriteria,
                    titleText: titleText,
                    onRemoveIndustry: onRemoveIndustry,
                    getAudienceCount: getAudienceCount,
                    visibleTargetAudience: visibleTargetAudience,
                    totalAudience: totalAudience,
                    reachedAudience: reachedAudience,
                    audienceReachMode: audienceReachMode,
                    bindAudienceReachCount: bindAudienceReachCount,
                    userBaseData: userBaseData,
                    setupPrice: setupPrice,
                    addEducation: addEducation,
                    selectedQuestionCountryList: selectedQuestionCountryList,
                    addCountryToCountryList: addCountryToCountryList,
                    findLocationsInCountry: findLocationsInCountry,
                    errorList: errorList,
                    changeStatus: changeStatus,
                    educations: educations,
                    professions: professions,
                    addNewEducationCriteria: addNewEducationCriteria,
                    addNewProfessionCriteria: addNewProfessionCriteria,
                    statusFilterValue: statusFilterValue,
                    qStatuses: qStatuses,
                    saveProfileQuestion: saveProfileQuestion,
                    updateProfileQuestion: updateProfileQuestion,
                    updateSurveyCriteria: updateSurveyCriteria,
                    SurveyQuestionsByFilter: SurveyQuestionsByFilter,
                    gotoScreen: gotoScreen,
                    isShowSurveyAns: isShowSurveyAns,
                    previewScreenNumber: previewScreenNumber,
                    showAdditionUserCriteria: showAdditionUserCriteria,
                    showAdditionalSurveyQuestions: showAdditionalSurveyQuestions,
                    showAdditionUserSurveyCriteria: showAdditionUserSurveyCriteria,
                    showAdditionQuizCriteria: showAdditionQuizCriteria,
                    isShowArchiveBtn: isShowArchiveBtn,
                    canSubmitForApproval: canSubmitForApproval,
                    isTerminateBtnVisible: isTerminateBtnVisible,
                    SelectedPvcVal: SelectedPvcVal,
                    HeaderText: HeaderText,
                    StatusValue: StatusValue,
                    getUrlVars: getUrlVars,
                    isfMode: isfMode,
                    isNewCampaign: isNewCampaign,
                    showCompanyProfileQuestions: showCompanyProfileQuestions,
                    SaveAsDraft: SaveAsDraft,
                    updateSurveyCriteriass: updateSurveyCriteriass,
                    ShowAudienceCounter: ShowAudienceCounter,
                    totalPrice: totalPrice,
                    SavePassChanges: SavePassChanges,
                    terminateSaveChanges: terminateSaveChanges,
                    DefaultRangeValue: DefaultRangeValue ,
					getSurvayAnalytics:getSurvayAnalytics,
					ClosePollAnalyticView:ClosePollAnalyticView,
					isAdvertdashboardPollVisible :isAdvertdashboardPollVisible,
					selectedCampStatusAnalytics:selectedCampStatusAnalytics,
					selecteddateRangeAnalytics:selecteddateRangeAnalytics,
					selectedGranularityAnalytics :selectedGranularityAnalytics,
                    Modelheading: Modelheading,
                    getQuestionByFilter: getQuestionByFilter,
                    SearchProfileQuestion: SearchProfileQuestion,
                    TemporaryProfileList:TemporaryProfileList,
                    TemporaryQuizQuestions:TemporaryQuizQuestions,
                    TemporarySurveyList: TemporarySurveyList,
                    submitResumeData: submitResumeData,
					selectedSQIDAnalytics : selectedSQIDAnalytics,
					SQAnalyticsData : SQAnalyticsData,
					granularityDropDown : granularityDropDown,
					DateRangeDropDown : DateRangeDropDown ,
					CampaignStatusDropDown : CampaignStatusDropDown , 
					openAdvertiserDashboardPollScreen : openAdvertiserDashboardPollScreen,
					CampaignRatioAnalyticData:CampaignRatioAnalyticData,
					CampaignTblAnalyticsData:CampaignTblAnalyticsData,
					CampaignROItblAnalyticData:CampaignROItblAnalyticData
                };
            })()
        };
        return ist.survey.viewModel;
    });