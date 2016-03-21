/*
    Module with the view model for the Profile Questions
*/
define("ads/ads.viewModel",
    ["jquery", "amplify", "ko", "ads/ads.dataservice", "ads/ads.model", "common/pagination", "common/confirmation.viewModel", "common/stripeChargeCustomer.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, stripeChargeCustomer) {
        var ist = window.ist || {};
        ist.Ads = {
            viewModel: (function () {
                var view,
                    campaignGridContent = ko.observableArray([]),
                    pager = ko.observable(),
                       // Controlls editor visibility 
                    searchFilterValue = ko.observable(),
                    isEditorVisible = ko.observable(false),
                    langs = ko.observableArray([]),
                    countoryidList = [],
                    cityidList = [],
                    langidList = [],
                    campaignModel = ko.observable(),
                    selectedCriteria = ko.observable(),
                    profileQuestionList = ko.observable([]),
                    myQuizQuestions = ko.observableArray([]),
                    profileAnswerList = ko.observable([]),
                    criteriaCount = ko.observable(0),
                    isShowSurveyAns = ko.observable(false),
                     // selected location 
                    selectedLocation = ko.observable(),
                    selectedLocationRadius = ko.observable(),
                    selectedLocationIncludeExclude = ko.observable(true),
                    selectedLangIncludeExclude = ko.observable(true),
                    selectedLocationLat = ko.observable(0),
                    selectedLocationLong = ko.observable(0),
                    ageRange = ko.observableArray([]),
                    isNewCriteria = ko.observable(true),
                    isEnableVedioVerificationLink = ko.observable(false),
                    //caption variablels 
                    lblCampaignName = ko.observable("Campaign Name"),
                    lblDetailsHeading = ko.observable("Free Display Ad Details"),
                    lblAdTitle = ko.observable("Ad Title"),
                    lblFirstLine = ko.observable("First line"),
                    lbllSecondLine = ko.observable("Second Line"),
                    lblCampaignSchedule = ko.observable("Campaign Schedule"),
                    campaignTypePlaceHolderValue = ko.observable('Enter in the YouTube video link'),
                //
                    isEditCampaign = ko.observable(false),
                    canSubmitForApproval = ko.observable(true),
                    isNewCampaignVisible = ko.observable(false),
                    isShowArchiveBtn = ko.observable(false),
                    isTerminateBtnVisible = ko.observable(false),
                    correctAnswers = ko.observableArray([{ id: 1, name: "Answer 1" }, { id: 2, name: "Answer 2" }, { id: 0, name: "Ask User Suggestion" }]),
                    selectedIndustryIncludeExclude = ko.observable(true),
                    UserAndCostDetail = ko.observable(),
                    pricePerclick = ko.observable(0),
                    isLocationPerClickPriceAdded = ko.observable(false),
                    isLanguagePerClickPriceAdded = ko.observable(false),
                    isIndustoryPerClickPriceAdded = ko.observable(false),
                    isProfileSurveyPerClickPriceAdded = ko.observable(false),
                    isEducationPerClickPriceAdded = ko.observable(false),
                    isDisplayCouponsAds = ko.observable(false),
                    selectedEducationIncludeExclude = ko.observable(true),
                    isListVisible = ko.observable(true),
                    isWelcomeScreenVisible = ko.observable(false),
                    isDetailEditorVisible = ko.observable(false),
                    isNewCampaign = ko.observable(false),
                    isFromEdit = ko.observable(false),
                     //audience reach
                    reachedAudience = ko.observable(0),
                    //total audience
                    totalAudience = ko.observable(0),
                    // audience reach mode 
                    audienceReachMode = ko.observable("1"),
                    MainHeading = ko.observable("My Ads"),
                    errorList = ko.observableArray([]),
                      // unique country list used to bind location dropdown
                    selectedQuestionCountryList = ko.observableArray([]),
                     educations = ko.observableArray([]),
                      professions = ko.observableArray([]),
                      voucherQuestionStatus = ko.observable(false),
                      buyItQuestionStatus = ko.observable(false),
                      AditionalCriteriaMode = ko.observable("1"), //1 = main buttons, 2 = profile questions , 3 = ad linked questions
                    getCampaignBaseContent = function () {
                        dataservice.getBaseData({
                            RequestId: 1,
                            QuestionId: 0,
                        }, {
                            success: function (data) {

                                if (data != null) {

                                    UserAndCostDetail(data.UserAndCostDetails);
                                    if (UserAndCostDetail().GenderClausePrice != null) {
                                        pricePerclick(pricePerclick() + UserAndCostDetail().GenderClausePrice);
                                    }
                                    if (UserAndCostDetail().AgeClausePrice != null) {
                                        pricePerclick(pricePerclick() + UserAndCostDetail().AgeClausePrice);
                                    }


                                    if (data.Languages != null) {
                                        langs.removeAll();
                                        ko.utils.arrayPushAll(langs(), data.Languages);
                                        langs.valueHasMutated();

                                    }

                                    if (data.Educations != null) {
                                        educations.removeAll();
                                        ko.utils.arrayPushAll(educations(), data.Educations);
                                        educations.valueHasMutated();
                                    }

                                    if (data.Professions != null) {
                                        professions.removeAll();
                                        ko.utils.arrayPushAll(professions(), data.Professions);
                                        professions.valueHasMutated();
                                    }
                                }

                            },
                            error: function (response) {

                            }
                        });
                    },

                    getAdCampaignGridContent = function () {
                        dataservice.getCampaignData({
                            CampaignId: 0,
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SearchText: searchFilterValue(),
                            ShowCoupons: isDisplayCouponsAds()
                        }, {
                            success: function (data) {
                                if (data != null) {

                                    // set grid content
                                    campaignGridContent.removeAll();
                                    _.each(data.Campaigns, function (item) {

                                        campaignGridContent.push(model.Campaign.Create(updateCampaignGridItem(item)));
                                    });
                                    pager().totalCount(data.TotalCount);


                                }

                            },
                            error: function (response) {

                            }
                        });

                    },

                    updateCampaignGridItem = function (item) {
                    
                        canSubmitForApproval(false);
                        if (item.Status == 1) {
                            item.StatusValue = "Draft";
                            canSubmitForApproval(true);
                        } else if (item.Status == 2) {
                            item.StatusValue = "Pending Approval"
                        } else if (item.Status == 3) {
                            item.StatusValue = "Live";
                        } else if (item.Status == 4) {
                            item.StatusValue = "Paused"
                        } else if (item.Status == 5) {
                            item.StatusValue = "Completed"
                        } else if (item.Status == 6) {
                            item.StatusValue = "Approval Rejected"
                        } else if (item.Status == 7) {
                            item.StatusValue= ("Terminated by user");
                        } else if (item.Status == 9) {
                            item.StatusValue = ("Completed");
                        } else if (item.Status == 8) {
                            item.StatusValue = ("Archived");
                        }
                        return item;
                    },

                    getCampaignByFilter = function () {
                        getAdCampaignGridContent();
                    },
                     // Add new Profile Question
                    addNewCampaign = function () {

                        if (isDisplayCouponsAds() == false) {
                            isWelcomeScreenVisible(true);
                        } else {
                            openEditScreen(5);
                            isFromEdit(true);
                        }
                        isListVisible(false);
                        isNewCampaign(true);
                        isTerminateBtnVisible(false);
                        isNewCampaignVisible(false);
                        isShowArchiveBtn(false);
                        //isEditorVisible(true);
                        //canSubmitForApproval(true);
                        //campaignModel(new model.Campaign());


                        //selectedCriteria();
                    //    getAudienceCount();
                        //campaignModel().Gender('1');
                        //campaignModel().Type('1');
                        //campaignModel().MaxBudget('1');
                        //campaignModel().AgeRangeEnd(80);
                        //campaignModel().AgeRangeStart(13);
                        //view.initializeTypeahead();
                        //isEnableVedioVerificationLink(false);
                        //isEditCampaign(false);
                        //campaignModel().CampaignTypeImagePath("");
                        //campaignModel().CampaignImagePath("");
                        //campaignModel().VoucherImagePath("");
                        //campaignModel().LanguageId(41);
                        //campaignModel().CampaignName('New Campaign');
                        
                       
                        //bindAudienceReachCount();
                        //selectedQuestionCountryList([]);

                        // above lines

                        //if (UserAndCostDetail().CountryId != null && UserAndCostDetail().CityId != null) {

                        //    var objCity = {
                        //        CountryID: UserAndCostDetail().CountryId,
                        //        CityID: UserAndCostDetail().CityId,
                        //        Radius: 0,
                        //        Country: UserAndCostDetail().Country,
                        //        City: UserAndCostDetail().City,
                        //        Latitude: UserAndCostDetail().GeoLat,
                        //        Longitude: UserAndCostDetail().GeoLong
                        //    }
                        //    selectedLocation(objCity);
                        //    onAddLocation();
                        //}
                    },

                    closeNewCampaignDialog = function () {
                        if (campaignModel().hasChanges() && (campaignModel().Status() == null || campaignModel().Status() == 1)) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(function () {
                                saveCampaignData();
                                isEditorVisible(false);

                                if (isFromEdit() == true)
                                {
                                    isListVisible(true);
                                    isWelcomeScreenVisible(false);
                                }
                                else
                                {
                                    isListVisible(false);
                                    isWelcomeScreenVisible(true);
                                }
                               
                                if (isDisplayCouponsAds() == true) {
                                    isListVisible(true);
                                    isWelcomeScreenVisible(false);
                                }
                                
                                $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                                $("#btnSubmitForApproval,#saveBtn,.table-link").css("display", "inline-block");
                                $("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign,#btnStopAndTerminate,#btnCopyCampaign").removeAttr('disabled');
                            });
                            confirmation.afterCancel(function () {

                                campaignModel();
                                selectedCriteria();
                                isEditorVisible(false);
                                if (isFromEdit() == true) {
                                    isListVisible(true);
                                    isWelcomeScreenVisible(false);
                                }
                                else {
                                    isListVisible(false);
                                    isWelcomeScreenVisible(true);
                                }
                            });
                            confirmation.show();
                            return;
                        } else {
                            isEditorVisible(false);
                            if (isFromEdit() == true) {
                                isListVisible(true);
                                isWelcomeScreenVisible(false);
                            }
                            else {
                                isListVisible(false);
                                isWelcomeScreenVisible(true);
                            }

                            $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                            $("#btnSubmitForApproval,#saveBtn,.table-link").css("display", "inline-block");
                            $("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign,#btnCopyCampaign,#btnStopAndTerminate").removeAttr('disabled');
                        }
                        isFromEdit(false);
                    },

                    saveCampaignData = function () {

                        if (campaignModel().isValid()) {

                            saveCampaign(1);


                        } else {
                            campaignModel().errors.showAllMessages();
                            toastr.error("Please fill the required feilds to continue.");
                        }
                    },

                     BackToAds = function () {
                         isEditorVisible(false);
                         isWelcomeScreenVisible(false);
                         isListVisible(true);
                     },
                      openEditScreen = function (mode) {

                          campaignModel(new model.Campaign());
                          campaignModel().CampaignName('New Campaign');
                          if (mode == 1)
                          {
                              campaignModel().Type('1');
                              isEnableVedioVerificationLink(true);
                          }
                          else if (mode == 2)
                          {
                              campaignModel().Type('3');
                              isEnableVedioVerificationLink(true);
                          }
                          else if (mode == 3)
                          {
                              campaignModel().Type('3');
                              isEnableVedioVerificationLink(true);
                          }
                          else if (mode == 4)
                          {
                              campaignModel().Type('4');
                              isEnableVedioVerificationLink(false);
                          }else if (mode == 5) {
                              campaignModel().CampaignName('New Coupon');
                              isEnableVedioVerificationLink(false);
                              campaignModel().Type('5');
                              lblCampaignName("Coupon Name");
                              lblDetailsHeading("Coupon Display Details");
                              lblAdTitle("Coupon Title");
                              lblFirstLine("First line");
                              lbllSecondLine("Second Line");
                              lblCampaignSchedule("Coupon Schedule");
                          }
                          isWelcomeScreenVisible(false);

                          isEditorVisible(true);
                          canSubmitForApproval(true);
                         


                          selectedCriteria();

                          campaignModel().Gender('1');
                       
                          campaignModel().MaxBudget('1');
                          campaignModel().AgeRangeEnd(80);
                          campaignModel().AgeRangeStart(13);
                          view.initializeTypeahead();
                         
                          isEditCampaign(false);
                          campaignModel().CampaignTypeImagePath("");
                          campaignModel().CampaignImagePath("");
                          campaignModel().VoucherImagePath("");
                          campaignModel().LanguageId(41);
                          
                          getAudienceCount();

                          bindAudienceReachCount();
                          selectedQuestionCountryList([]);

                      },


                    submitCampaignData = function () {
                        if (campaignModel().isValid()) {
                            if (reachedAudience() > 0) {
                                if (UserAndCostDetail().isStripeIntegrated == false) {
                                    stripeChargeCustomer.show(function () {
                                        UserAndCostDetail().isStripeIntegrated = true;

                                    }, 2000, 'Enter your details');
                                } else {

                                    saveCampaign(2);



                                }
                            } else {
                                toastr.error("You have no audience against the specified criteria please broad your audience definition.");
                            }
                            

                        } else {
                            campaignModel().errors.showAllMessages();
                            toastr.error("Please fill the required feilds to continue.");
                        }
                    },
                      terminateCampaign = function () {
                          saveCampaign(7);
                      },
                      ArchiveCampaign = function () {
                          saveCampaign(8);
                      },
                    saveCampaign = function (mode) {
                        campaignModel().Status(mode);
                        campaignModel().ClickRate(pricePerclick());
                        var campignServerObj = campaignModel().convertToServerData();

                        dataservice.addCampaignData(campignServerObj, {
                            success: function (data) {

                                criteriaCount(0);
                                pricePerclick(0);
                                isEditorVisible(false);
                                getAdCampaignGridContent();
                                isLocationPerClickPriceAdded(false);
                                isLanguagePerClickPriceAdded(false);
                                isIndustoryPerClickPriceAdded(false);
                                isProfileSurveyPerClickPriceAdded(false);
                                isEducationPerClickPriceAdded(false);
                                isListVisible(true);
                                isWelcomeScreenVisible(false);
                                toastr.success("Successfully saved.");
                            },
                            error: function (response) {

                            }
                        });
                    }
                // Add new profile Criteria
                addNewProfileCriteria = function () {

                    isNewCriteria(true);
                    AditionalCriteriaMode("1");
                    var objProfileCriteria = new model.AdCampaignTargetCriteriasModel();

                    objProfileCriteria.Type("1");
                    objProfileCriteria.IncludeorExclude("1");
                    criteriaCount(criteriaCount() + 1);
                    objProfileCriteria.CriteriaID(criteriaCount());
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
                                }

                            },
                            error: function (response) {

                            }
                        });
                    }
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
                                }

                            },
                            error: function (response) {

                            }
                        });
                    }
                },
                  saveProfileQuestion = function (item) {
                      var selectedQuestionstring = $(".active .parent-list-title").text();
                      selectedCriteria().questionString(selectedQuestionstring);
                      selectedCriteria().PQID(item.PQID);
                      var selectedQuestionAnswerstring = item.Answer;
                      selectedCriteria().answerString(selectedQuestionAnswerstring);
                      selectedCriteria().PQAnswerID(item.PqAnswerId);
                      campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                          Type: 1,
                          PQId: selectedCriteria().PQID(),
                          PQAnswerId: selectedCriteria().PQAnswerID(),
                          SQId: selectedCriteria().SQID(),
                          SQAnswer: selectedCriteria().SQAnswer(),
                          questionString: selectedCriteria().questionString(),
                          answerString: selectedCriteria().answerString(),
                          IncludeorExclude: selectedCriteria().IncludeorExclude(),
                          CampaignId: campaignModel().CampaignID
                      }));
                      $(".close").click();
                  },
                    updateSurveyCriteria = function (type, item) {
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
                    var selectedQuestionstring = item.VerifyQuestion;
                    selectedCriteria().questionString(selectedQuestionstring);
                    if (type == 1) {
                        selectedCriteria().answerString(item.Answer1);
                    } else {
                        selectedCriteria().answerString(item.Answer2);
                    }
                        
                    if (isNewCriteria()) {
                        campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                            Type: 6,
                            PQId: selectedCriteria().PQID(),
                            PQAnswerId: selectedCriteria().PQAnswerID(),
                            QuizCampaignId: item.CampaignId,
                            QuizAnswerId: type,
                            questionString: selectedCriteria().questionString(),
                            answerString: selectedCriteria().answerString(),
                            IncludeorExclude: selectedCriteria().IncludeorExclude(),
                            CampaignId: campaignModel().CampaignID
                        }));
                    }
                    $(".close").click();
                    isShowSurveyAns(false);
                },

                onEditCriteria = function (item) {
                    AditionalCriteriaMode("2");
                    isNewCriteria(false);
                    var val = item.PQAnswerID() + 0;
                    var valQuest = item.PQID() + 0;
                    if (item.Type() == "1") {

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
                                        item.PQID(valQuest);
                                    }

                                },
                                error: function (response) {

                                }
                            });
                        }


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
                        selectedCriteria(item);
                        var matchSurveyQuestion = ko.utils.arrayFirst(myQuizQuestions(), function (survey) {
                            return survey.CampaignId == item.QuizCampaignId();
                        });
                        selectedCriteria().surveyQuestLeftImageSrc(matchSurveyQuestion.Answer1);
                        selectedCriteria().surveyQuestRightImageSrc(matchSurveyQuestion.Answer2);
                        // adjust item
                    }

                },
                // Delete Handler PQ
                onDeleteCriteria = function (item) {


                    campaignModel().AdCampaignTargetCriterias.remove(item);

                    if (item.Type() == "1") {
                        var matchedProfileCriteria = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (arrayitem) {

                            return arrayitem.PQID() == item.PQID()
                        });

                        if (matchedProfileCriteria == null) {
                            if (UserAndCostDetail().OtherClausePrice != null) {
                                pricePerclick(pricePerclick() - UserAndCostDetail().OtherClausePrice);

                            }
                        }
                    } else if (item.Type() == "2") {
                        var matchedSurveyCriteria = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (arrayitem) {

                            return arrayitem.SQID() == item.SQID();
                        });

                        if (matchedSurveyCriteria == null) {
                            if (UserAndCostDetail().OtherClausePrice != null) {
                                pricePerclick(pricePerclick() - UserAndCostDetail().OtherClausePrice);

                            }
                        }
                    }

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
                    //item.surveyQuestLeftImageSrc(matchSurveyQuestion.LeftPicturePath);
                    //item.surveyQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);
                    //$("#surveyAnswersContainer").show();
                    //isShowSurveyAns(true);
                },

                onRemoveLocation = function (item) {
                    // Ask for confirmation
                    deleteLocation(item);

                },

                deleteLocation = function (item) {
                    if (item.CountryID() == UserAndCostDetail().CountryId && item.CityID() == UserAndCostDetail().CityId) {
                        toastr.error("You cannot remove your home town or country!");
                    } else {
                        campaignModel().AdCampaignTargetLocations.remove(item);

                        if (campaignModel().AdCampaignTargetLocations() == null || campaignModel().AdCampaignTargetLocations().length == 0) {
                            isLocationPerClickPriceAdded(false);
                            pricePerclick(pricePerclick() - UserAndCostDetail().LocationClausePrice);
                        }
                        selectedQuestionCountryList([]);
                        _.each(campaignModel().AdCampaignTargetLocations(), function (item) {
                            addCountryToCountryList(item.CountryID(), item.Country());
                        });
                        toastr.success("Removed Successfully!");
                    }

                },
                //add location
                onAddLocation = function (item) {
                    selectedLocation().Radius = (selectedLocationRadius);
                    selectedLocation().IncludeorExclude = (selectedLocationIncludeExclude);
                    campaignModel().AdCampaignTargetLocations.push(new model.AdCampaignTargetLocation.Create({
                        CountryId: selectedLocation().CountryID,
                        CityId: selectedLocation().CityID,
                        Radius: selectedLocation().Radius(),
                        Country: selectedLocation().Country,
                        City: selectedLocation().City,
                        IncludeorExclude: selectedLocation().IncludeorExclude(),
                        CampaignId: campaignModel().CampaignID(),
                        Latitude: selectedLocation().Latitude,
                        Longitude: selectedLocation().Longitude,
                    }));
                    addCountryToCountryList(selectedLocation().CountryID, selectedLocation().Country);

                    if (UserAndCostDetail().LocationClausePrice != null && isLocationPerClickPriceAdded() == false) {
                        pricePerclick(pricePerclick() + UserAndCostDetail().LocationClausePrice);
                        isLocationPerClickPriceAdded(true);
                    }
                },

                resetLocations = function () {
                    $("#searchCampaignLocations").val("");
                    selectedLocationRadius("");
                },

                addLanguage = function (selected) {

                    campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                        Language: selected.LanguageName,
                        LanguageId: selected.LanguageId,
                        IncludeorExclude: parseInt(selectedLangIncludeExclude()),
                        Type: 3,
                        CriteriaId: 0,
                        CampaignId: campaignModel().CampaignID()
                    }));
                    $("#searchLanguages").val("");
                    if (UserAndCostDetail().OtherClausePrice != null && isLanguagePerClickPriceAdded() == false) {
                        pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);
                        isLanguagePerClickPriceAdded(true);
                    }
                },

                onRemoveLanguage = function (item) {
                    // Ask for confirmation
                    deleteLanguage(item);


                },

                deleteLanguage = function (item) {

                    campaignModel().AdCampaignTargetCriterias.remove(item);

                    var matchedLanguageCriterias = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (arrayitem) {

                        return arrayitem.Type() == item.Type();
                    });

                    if (matchedLanguageCriterias == null) {
                        isLanguagePerClickPriceAdded(false);
                        pricePerclick(pricePerclick() - UserAndCostDetail().OtherClausePrice);
                    }
                    toastr.success("Removed Successfully!");

                },
                // Has Changes
                hasChangesOnQuestion = ko.computed(function () {
                    if (campaignModel() == undefined) {
                        return false;
                    }
                    return (campaignModel().hasChanges());
                }),

                OnChangeCampaignType = function () {
                    if (campaignModel().Type() == "1") {
                        isEnableVedioVerificationLink(true);
                        campaignTypePlaceHolderValue('Enter in the YouTube video link');
                    } else {
                        isEnableVedioVerificationLink(false);
                        if (campaignModel().Type() == "2") {
                            campaignTypePlaceHolderValue('Enter in your Web site landing Page link');
                        }
                        if (campaignModel().Type() == "3") {
                            campaignTypePlaceHolderValue('Enter in your Web site landing Page link');
                            isEnableVedioVerificationLink(true);
                        }
                    }
                },

                campaignTypeImageCallback = function (file, data) {
                    campaignModel().CampaignTypeImagePath(data);
                },

                campaignImageCallback = function (file, data) {
                    campaignModel().CampaignImagePath(data);
                },

                onEditCampaign = function (item) {
                    isTerminateBtnVisible(false);
                    isNewCampaignVisible(false);
                    isShowArchiveBtn(false);
                    if (item.Status() == 1 || item.Status() == 2 || item.Status() == 3 || item.Status() == 4 || item.Status() == null || item.Status() == 7 || item.Status() == 9) {
                        canSubmitForApproval(true);
                        dataservice.getCampaignData({
                            CampaignId: item.CampaignID(),
                            SearchText: ""
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    // set languages drop down
                                    selectedCriteria();
                                    pricePerclick(0);

                                    campaignModel(model.Campaign.Create(data.Campaigns[0]));
                                    campaignModel().reset();
                                    view.initializeTypeahead();

                                    selectedQuestionCountryList([]);
                                    _.each(campaignModel().AdCampaignTargetLocations(), function (item) {
                                        addCountryToCountryList(item.CountryID(), item.Country());
                                    });

                                    if (campaignModel().Type() == "1") {
                                        isEnableVedioVerificationLink(true);
                                        campaignTypePlaceHolderValue('Enter in the YouTube video link');
                                    } else {
                                        isEnableVedioVerificationLink(false);
                                        if (campaignModel().Type() == "2") {
                                            campaignTypePlaceHolderValue('Enter in your Web site landing Page link');
                                        }
                                    }

                                    if (campaignModel().Status() == 1) {
                                        campaignModel().StatusValue("Draft");
                                    } else if (campaignModel().Status() == 2) {
                                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                                        $("#saveBtn").css("display", "none")
                                        $("#btnCancel,#btnPauseCampaign").removeAttr('disabled');
                                        campaignModel().StatusValue("Submitted for Approval");
                                    } else if (campaignModel().Status() == 3) {
                                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                                        $("#saveBtn").css("display", "none");
                                        $("#btnPauseCampaign").css("display", "inline-block");
                                        $("#btnCancel,#btnPauseCampaign,#btnCopyCampaign,#btnStopAndTerminate").removeAttr('disabled');
                                        campaignModel().StatusValue("Live");
                                        isTerminateBtnVisible(true);
                                        isNewCampaignVisible(true);
                                    } else if (campaignModel().Status() == 4) {
                                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                                        $("#saveBtn").css("display", "none");
                                        $("#btnResumeCampagin").css("display", "inline-block");
                                        $("#btnCancel,#btnResumeCampagin,#btnCopyCampaign,#btnStopAndTerminate").removeAttr('disabled');
                                        campaignModel().StatusValue("Paused");
                                        isTerminateBtnVisible(true);
                                        isNewCampaignVisible(true);
                                    } else if (campaignModel().Status() == 5) {
                                        campaignModel().StatusValue("Completed");
                                    } else if (campaignModel().Status() == 6) {
                                        campaignModel().StatusValue("Approval Rejected");
                                    } else if (campaignModel().Status() == 7) {
                                        campaignModel().StatusValue("Terminated by user");
                                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                                        $("#saveBtn").css("display", "none");
                                        $("#btnPauseCampaign").css("display", "inline-block");
                                        $("#btnCancel,#btnPauseCampaign,#btnCopyCampaign,#btnArchive").removeAttr('disabled');
                                        isNewCampaignVisible(true);
                                        isShowArchiveBtn(true);
                                    } else if (item.Status == 9) {
                                        item.StatusValue = ("Completed");
                                    } else if (item.Status == 8) {
                                        item.StatusValue = ("Archived");
                                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                                        $("#saveBtn").css("display", "none");
                                        $("#btnPauseCampaign").css("display", "inline-block");
                                        $("#btnCancel,#btnPauseCampaign,#btnCopyCampaign,#btnArchive").removeAttr('disabled');
                                        isNewCampaignVisible(true);
                                        isShowArchiveBtn(true);
                                    }
                                    isEditCampaign(true);
                                    isEditorVisible(true);
                                    isListVisible(false);
                                    isFromEdit(true);
                                    isNewCampaign(false);
                                    buildMap();
                                    var profileQIds = [];
                                    var surveyQIds = [];
                                    if (campaignModel().AdCampaignTargetLocations() != null || campaignModel().AdCampaignTargetLocations().length > 0) {
                                        if (UserAndCostDetail().LocationClausePrice != null && isLocationPerClickPriceAdded() == false) {
                                            pricePerclick(pricePerclick() + UserAndCostDetail().LocationClausePrice);
                                            isLocationPerClickPriceAdded(true);
                                        }
                                    } else {
                                        isLocationPerClickPriceAdded(true);
                                    }

                                    if (UserAndCostDetail().GenderClausePrice != null) {
                                        pricePerclick(pricePerclick() + UserAndCostDetail().GenderClausePrice);
                                    }
                                    if (UserAndCostDetail().AgeClausePrice != null) {
                                        pricePerclick(pricePerclick() + UserAndCostDetail().AgeClausePrice);
                                    }

                                    _.each(campaignModel().AdCampaignTargetCriterias(), function (item) {

                                        if (item.Type() == "1") { // profile

                                            if (profileQIds.indexOf(item.PQID()) == -1) {
                                                profileQIds.push(item.PQID());
                                                if (UserAndCostDetail().OtherClausePrice != null) {
                                                    pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);
                                                    isProfileSurveyPerClickPriceAdded(true);
                                                }
                                            }

                                        }
                                        if (item.Type() == "2") { // survey
                                            if (surveyQIds.indexOf(item.SQID()) == -1) {
                                                surveyQIds.push(item.SQID());
                                                if (UserAndCostDetail().OtherClausePrice != null) {
                                                    pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);
                                                    isProfileSurveyPerClickPriceAdded(true);
                                                }
                                            }

                                        }
                                        if (item.Type() == "3") { // language
                                            if (isLanguagePerClickPriceAdded() == false) {
                                                isLanguagePerClickPriceAdded(true);
                                                pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);
                                            }
                                        }
                                        if (item.Type() == "4") { // industry
                                            if (isIndustoryPerClickPriceAdded() == false) {
                                                isIndustoryPerClickPriceAdded(true);
                                                pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);
                                            }
                                        }

                                        if (item.Type() == "5") { // education
                                            if (isEducationPerClickPriceAdded() == false) {
                                                isEducationPerClickPriceAdded(true);
                                                pricePerclick(pricePerclick() + UserAndCostDetail().EducationClausePrice);
                                            }
                                        }
                                    });
                                    getAudienceCount();
                                    bindAudienceReachCount();
                                    if (campaignModel().VoucherImagePath() != null || campaignModel().Voucher1Value() != null || campaignModel().Voucher1Description() != null || campaignModel().Voucher1Heading() != null)
                                    {
                                        voucherQuestionStatus(true);
                                    }
                                    if (campaignModel().BuuyItLine1() != null || campaignModel().BuyItLine2() != null || campaignModel().BuyItLine3() != null || campaignModel().BuyItButtonLabel() != null) {
                                        buyItQuestionStatus(true);
                                    }
                                    //buyItQuestionStatus
                                    // handle 2nd edit error 
                                    //  $(".modal-backdrop").remove();
                                    $.unblockUI(spinner);

                                }

                            },
                            error: function (response) {

                            }
                        });
                    }
                },
                changeStatus = function (status) {
                    if (campaignModel() != undefined)
                        saveCampaign(status);

                    $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                    $("#btnSubmitForApproval,#saveBtn,.lang_delSurvey,.table-link").css("display", "inline-block");
                    $("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign").removeAttr('disabled');
                },
                addIndustry = function (selected) {

                    campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                        Industry: selected.IndustryName,
                        IndustryId: selected.IndustryId,
                        IncludeorExclude: true,//parseInt(selected.IndustryIncludeExclude),
                        Type: 4,
                        CampaignId: campaignModel().CampaignID()
                    }));

                    $("#searchIndustries").val("");
                    if (UserAndCostDetail().ProfessionClausePrice != null && isIndustoryPerClickPriceAdded() == false) {
                        pricePerclick(pricePerclick() + UserAndCostDetail().ProfessionClausePrice);
                        isIndustoryPerClickPriceAdded(true);
                    }
                },
                onRemoveIndustry = function (item) {
                    // Ask for confirmation

                    campaignModel().AdCampaignTargetCriterias.remove(item);
                    var matchedIndustoryCriterias = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (arrayitem) {

                        return arrayitem.Type() == item.Type();
                    });

                    if (matchedIndustoryCriterias == null) {
                        isIndustoryPerClickPriceAdded(false);
                        pricePerclick(pricePerclick() - UserAndCostDetail().ProfessionClausePrice);
                    }
                    toastr.success("Removed Successfully!");

                },
                visibleTargetAudience = function (mode) {

                    if (mode != undefined) {
                        var matcharry = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (item) {

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
                addEducation = function (selected) {
                    campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                        Education: selected.Title,
                        EducationId: selected.EducationId,
                        IncludeorExclude: parseInt(selectedEducationIncludeExclude()),
                        Type: 5,
                        CampaignId: campaignModel().CampaignID()
                    }));

                    $("#searchEducations").val("");

                    if (UserAndCostDetail().EducationClausePrice != null && isEducationPerClickPriceAdded() == false) {
                        pricePerclick(pricePerclick() + UserAndCostDetail().EducationClausePrice);
                        isEducationPerClickPriceAdded(true);
                    }
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
                onRemoveEducation = function (item) {
                    // Ask for confirmation

                    campaignModel().AdCampaignTargetCriterias.remove(item);
                    var matchedEducationCriterias = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (arrayitem) {

                        return arrayitem.Type() == item.Type();
                    });

                    if (matchedEducationCriterias == null) {
                        isEducationPerClickPriceAdded(false);
                        pricePerclick(pricePerclick() - UserAndCostDetail().EducationClausePrice);
                    }
                    toastr.success("Removed Successfully!");

                },
                getAudienceCount = function () {
                    var countryIds = '', cityIds = '', countryIdsExcluded = '', cityIdsExcluded = '';
                    var educationIds = '', educationIdsExcluded = '';
                    _.each(campaignModel().AdCampaignTargetLocations(), function (item) {
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
                    _.each(campaignModel().AdCampaignTargetCriterias(), function (item) {

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
                                    surveyQuestionIdsExcluded += item.SQID();
                                } else {
                                    surveyQuestionIdsExcluded += ',' + item.SQID();
                                }
                                if (surveyAnswerIdsExcluded == '') {
                                    surveyAnswerIdsExcluded += item.SQAnswer();
                                } else {
                                    surveyAnswerIdsExcluded += ',' + item.SQAnswer();
                                }
                            } else {
                                if (surveyQuestionIds == '') {
                                    surveyQuestionIds += item.SQID();
                                } else {
                                    surveyQuestionIds += ',' + item.SQID();
                                }
                                if (surveyAnswerIds == '') {
                                    surveyAnswerIds += item.SQAnswer();
                                } else {
                                    surveyAnswerIds += ',' + item.SQAnswer();
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
                                    educationIdsExcluded += item.EducationID();
                                } else {
                                    educationIdsExcluded += ',' + item.EducationID();
                                }
                            } else {
                                if (educationIds == '') {
                                    educationIds += item.EducationID();
                                } else {
                                    educationIds += ',' + item.EducationID();
                                }
                            }
                        }
                    });
                    var campData = {
                        ageFrom: campaignModel().AgeRangeStart(),
                        ageTo: campaignModel().AgeRangeEnd(),
                        gender: campaignModel().Gender(),
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
                    dataservice.getAudienceData(campData, {
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
                bindAudienceReachCount = function () {
                    campaignModel().AgeRangeStart.subscribe(function (value) {
                        getAudienceCount();
                    });
                    campaignModel().AgeRangeEnd.subscribe(function (value) {
                        getAudienceCount();
                    });
                    campaignModel().Gender.subscribe(function (value) {
                        getAudienceCount();
                    });
                    campaignModel().AdCampaignTargetLocations.subscribe(function (value) {
                        getAudienceCount();
                        buildMap();
                    });
                    campaignModel().AdCampaignTargetCriterias.subscribe(function (value) {
                        getAudienceCount();
                    });
                },
                buildMap = function () {
                 $(".locMap").css("display", "none");
                 var initialized = false;
                 _.each(campaignModel().AdCampaignTargetLocations(), function (item) {
                    // $(".locMap").css("display", "inline-block");
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
             }
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

                    var list = ko.utils.arrayFilter(campaignModel().AdCampaignTargetLocations(), function (prod) {
                        return prod.CountryID() == id;
                    });
                    return list;
                },
                ChangeVoucherSettings = function () {
                     if (voucherQuestionStatus() == false) {
                         voucherQuestionStatus(true);
                     } else {
                         voucherQuestionStatus(false);
                     }
                 },
                VoucherImageCallback = function (file, data) {
                    campaignModel().VoucherImagePath(data);
                },
                buyItImageCallback = function (file, data) {
                    campaignModel().buyItImageBytes(data);
                },
                ShowCouponPromotions = function () {
                    isDisplayCouponsAds(true);
                    MainHeading("My Coupons");
                    getAdCampaignGridContent();
                },
                ShowAdCampaigns = function () {
                    isDisplayCouponsAds(false);
                    MainHeading("My Ads");
                    getAdCampaignGridContent();
                },
                gotoProfile = function () {
                    window.location.href = "/User/ManageUser/Index";
                },
                gotoManageUsers = function () {
                    window.location.href = "/";
                },
                copyCampaign = function () {

                },
                showAdditionCriteria = function (mode) {
                    AditionalCriteriaMode(mode);
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
                    pager(pagination.Pagination({ PageSize: 10 }, campaignGridContent, getAdCampaignGridContent));
                    getAdCampaignGridContent();
                    getCampaignBaseContent();
                    isEditorVisible(false);
                };

                return {
                    initialize: initialize,
                    pager: pager,
                    hasChangesOnQuestion: hasChangesOnQuestion,
                    isEditorVisible: isEditorVisible,
                    campaignGridContent: campaignGridContent,
                    addNewCampaign: addNewCampaign,
                    langs: langs,
                    campaignModel: campaignModel,
                    saveCampaignData: saveCampaignData,
                    closeNewCampaignDialog: closeNewCampaignDialog,
                    selectedCriteria: selectedCriteria,
                    profileQuestionList: profileQuestionList,
                    profileAnswerList: profileAnswerList,
                    saveCriteria: saveCriteria,
                    onDeleteCriteria: onDeleteCriteria,
                    onEditCriteria: onEditCriteria,
                    addNewProfileCriteria: addNewProfileCriteria,
                    onChangeProfileQuestion: onChangeProfileQuestion,
                    myQuizQuestions: myQuizQuestions,
                 //   addNewSurveyCriteria: addNewSurveyCriteria,
                    onChangeSurveyQuestion: onChangeSurveyQuestion,
                    getCampaignByFilter: getCampaignByFilter,
                    searchFilterValue: searchFilterValue,
                    isShowSurveyAns: isShowSurveyAns,
                    selectedLocation: selectedLocation,
                    selectedLocationRadius: selectedLocationRadius,
                    selectedLocationIncludeExclude: selectedLocationIncludeExclude,
                    selectedLangIncludeExclude: selectedLangIncludeExclude,
                    selectedLocationLat: selectedLocationLat,
                    selectedLocationLong: selectedLocationLong,
                    onAddLocation: onAddLocation,
                    onRemoveLocation: onRemoveLocation,
                    deleteLocation: deleteLocation,
                    addLanguage: addLanguage,
                    onRemoveLanguage: onRemoveLanguage,
                    deleteLanguage: deleteLanguage,
                    ageRange: ageRange,
                    isNewCriteria: isNewCriteria,
                    isEnableVedioVerificationLink: isEnableVedioVerificationLink,
                    campaignTypePlaceHolderValue: campaignTypePlaceHolderValue,
                    OnChangeCampaignType: OnChangeCampaignType,
                    campaignTypeImageCallback: campaignTypeImageCallback,
                    campaignImageCallback: campaignImageCallback,
                    correctAnswers: correctAnswers,
                    onEditCampaign: onEditCampaign,
                    canSubmitForApproval: canSubmitForApproval,
                    isTerminateBtnVisible: isTerminateBtnVisible,
                    isNewCampaignVisible: isNewCampaignVisible,
                    isShowArchiveBtn:isShowArchiveBtn,
                    submitCampaignData: submitCampaignData,
                    terminateCampaign:terminateCampaign,
                    selectedIndustryIncludeExclude: selectedIndustryIncludeExclude,
                    addIndustry: addIndustry,
                    onRemoveIndustry: onRemoveIndustry,
                    visibleTargetAudience: visibleTargetAudience,
                    pricePerclick: pricePerclick,
                    selectedEducationIncludeExclude: selectedEducationIncludeExclude,
                    addEducation: addEducation,
                    reachedAudience: reachedAudience,
                    audienceReachMode: audienceReachMode,
                    onRemoveEducation: onRemoveEducation,
                    bindAudienceReachCount: bindAudienceReachCount,
                    errorList: errorList,
                    addCountryToCountryList: addCountryToCountryList,
                    findLocationsInCountry: findLocationsInCountry,
                    selectedQuestionCountryList: selectedQuestionCountryList,
                    changeStatus: changeStatus,
                    educations: educations,
                    professions: professions,
                    addNewEducationCriteria: addNewEducationCriteria,
                    addNewProfessionCriteria: addNewProfessionCriteria,
                    voucherQuestionStatus: voucherQuestionStatus,
                    ChangeVoucherSettings:ChangeVoucherSettings,
                    VoucherImageCallback: VoucherImageCallback,
                    saveProfileQuestion: saveProfileQuestion,
                    updateProfileQuestion: updateProfileQuestion,
                    updateSurveyCriteria: updateSurveyCriteria,
                    buyItQuestionStatus: buyItQuestionStatus,
                    buyItImageCallback: buyItImageCallback,
                    openEditScreen: openEditScreen,
                    isWelcomeScreenVisible: isWelcomeScreenVisible,
                    isDetailEditorVisible: isDetailEditorVisible,
                    isListVisible: isListVisible,
                    isNewCampaign: isNewCampaign,
                    BackToAds: BackToAds,
                    MainHeading: MainHeading,
                    ShowAdCampaigns: ShowAdCampaigns,
                    ShowCouponPromotions: ShowCouponPromotions,
                    isDisplayCouponsAds: isDisplayCouponsAds,
                    lblCampaignName :lblCampaignName,
                    lblDetailsHeading :lblDetailsHeading,
                    lblAdTitle:lblAdTitle,
                    lblFirstLine:lblFirstLine,
                    lbllSecondLine:lbllSecondLine,
                    lblCampaignSchedule: lblCampaignSchedule,
                    gotoProfile: gotoProfile,
                    gotoManageUsers: gotoManageUsers,
                    ArchiveCampaign: ArchiveCampaign,
                    copyCampaign: copyCampaign,
                    AditionalCriteriaMode: AditionalCriteriaMode,
                    showAdditionCriteria: showAdditionCriteria
                };
            })()
        };
        return ist.Ads.viewModel;
    });
