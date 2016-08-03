﻿/*
    Module with the view model for the Profile Questions
*/
define("Coupons/Coupons.viewModel",
    ["jquery", "amplify", "ko", "Coupons/Coupons.dataservice", "Coupons/Coupons.model", "common/pagination", "common/confirmation.viewModel",
        "common/stripeChargeCustomer.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, stripeChargeCustomer) {
        var ist = window.ist || {};
        ist.Coupons = {
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
                    couponModel = ko.observable(),
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
                   
                //
                    isEditCampaign = ko.observable(false),
                    canSubmitForApproval = ko.observable(true),
                    isNewCampaignVisible = ko.observable(false),
                    isShowArchiveBtn = ko.observable(false),
                    isTerminateBtnVisible = ko.observable(false),
                    correctAnswers = ko.observableArray([{ id: 1, name: "Choice 1" }, { id: 2, name: "Choice 2" }, { id: 3, name: "Choice 3" }, { id: 0, name: "Ask User Suggestion" }]),
                    selectedIndustryIncludeExclude = ko.observable(true),
                    UserAndCostDetail = ko.observable(),
                    pricePerclick = ko.observable(0),
                    isLocationPerClickPriceAdded = ko.observable(false),
                    isLanguagePerClickPriceAdded = ko.observable(false),
                    isIndustoryPerClickPriceAdded = ko.observable(false),
                    isProfileSurveyPerClickPriceAdded = ko.observable(false),
                    isEducationPerClickPriceAdded = ko.observable(false),
                    isBuyItPerClickPriceAdded = ko.observable(false),
                    isVoucherPerClickPriceAdded = ko.observable(false),
                  
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
                    errorList = ko.observableArray([]),
                      // unique country list used to bind location dropdown
                    selectedQuestionCountryList = ko.observableArray([]),
                    educations = ko.observableArray([]),
                    professions = ko.observableArray([]),
                    voucherQuestionStatus = ko.observable(false),
                    buyItQuestionStatus = ko.observable(false),
                    AditionalCriteriaMode = ko.observable("1"), //1 = main buttons, 2 = profile questions , 3 = ad linked questions
                    couponCategories = ko.observableArray([]),
                    quizQuestionStatus = ko.observable(false),
                    quizPriceLbl = ko.observable("1"),
                    tenPriceLbl = ko.observable("1"),
                    fivePriceLbl = ko.observable("1"),
                    threePriceLbl = ko.observable("1"),
                    buyItPriceLbl = ko.observable("1"),
                    voucherPriceLbl = ko.observable("1"),
                    alreadyAddedDeliveryValue = ko.observable(false),
                    isQuizQPerClickPriceAdded = ko.observable(false),
                    campaignNamePlaceHolderValue = ko.observable('Campaign Name (36 characters)'),
                    genderppc = ko.observable(),
                    professionppc = ko.observable(),
                    ageppc = ko.observable(),
                    BetterListitemToAdd = ko.observable(""),
                    allCouponCodeItems = ko.observableArray([]),// Initial items
                    selectedCouponCodeItems = ko.observableArray([]),                            // Initial selection
                    UsedCouponQuantity = ko.observable(0),
                    advertiserLogo = ko.observable(""),
                    previewVideoTagUrl = ko.observable(""),
                    randonNumber = ko.observable("?r=0"),
                    vouchers = ko.observableArray(),
                    numberOFCouponsToGenerate = ko.observable(0),
                    previewScreenNumber = ko.observable(1);
                CurrPage = ko.observable(9);
                MaxPage = ko.observable(12);
                    getCampaignBaseContent = function () {
                        dataservice.getBaseData({
                            RequestId: 1,
                            QuestionId: 0,
                        }, {
                        success: function (data) {

                            if (data != null) {
                                
                                UserAndCostDetail(data.UserAndCostDetails);
                                advertiserLogo(UserAndCostDetail().UserProfileImage);
                                buyItPriceLbl(UserAndCostDetail().BuyItClausePrice + "p");
                                quizPriceLbl(UserAndCostDetail().QuizQuestionClausePrice);
                                tenPriceLbl(" (" + UserAndCostDetail().TenDayDeliveryClausePrice + "p)");
                                voucherPriceLbl(UserAndCostDetail().VoucherClausePrice);
                                threePriceLbl(" (" + UserAndCostDetail().ThreeDayDeliveryClausePrice + "p)");
                                fivePriceLbl(" (" + UserAndCostDetail().FiveDayDeliveryClausePrice + "p)");
                                if (UserAndCostDetail().GenderClausePrice != null) {
                                    genderppc(" (" + UserAndCostDetail().GenderClausePrice + "p)");
                                    pricePerclick(pricePerclick() + UserAndCostDetail().GenderClausePrice);
                                }
                                if (UserAndCostDetail().AgeClausePrice != null) {
                                    ageppc(" (" + UserAndCostDetail().AgeClausePrice + "p)");
                                    pricePerclick(pricePerclick() + UserAndCostDetail().AgeClausePrice);
                                }
                                if (UserAndCostDetail().ProfessionClausePrice != null) {
                                    professionppc(" (" + UserAndCostDetail().ProfessionClausePrice + "p)");

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

                                if (data.CouponCategories != null) {
                                    couponCategories.removeAll();
                                    ko.utils.arrayPushAll(couponCategories(), data.CouponCategories);
                                    couponCategories.valueHasMutated();
                                }
                                
                                if (data.DiscountVouchers != null) {
                                    vouchers.removeAll();
                                    ko.utils.arrayPushAll(vouchers(), data.DiscountVouchers);
                                    vouchers.valueHasMutated();
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
                        ShowCoupons:true
                    }, {
                        success: function (data) {
                            if (data != null) {

                                // set grid content
                                campaignGridContent.removeAll();
                                _.each(data.Coupon, function (item) {
                                    campaignGridContent.push(model.Coupon.Create(updateCampaignGridItem(item)));
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
                        item.StatusValue = ("Terminated by user");
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
                addNewCampaign = function () {
                    openEditScreen(5);
                    isFromEdit(true);
                    isListVisible(false);
                    isNewCampaign(true);
                    isTerminateBtnVisible(false);
                    isNewCampaignVisible(false);
                    isShowArchiveBtn(false);
                    
                },

                closeNewCampaignDialog = function () {
                    //if (couponModel().hasChanges() && (couponModel().Status() == null || couponModel().Status() == 1)) {
                        confirmation.messageText("Do you want to save changes?");
                        confirmation.afterProceed(function () {
                            saveCampaignData();
                            isEditorVisible(false);

                            if (isFromEdit() == true) {
                                isListVisible(true);
                                isWelcomeScreenVisible(false);
                            }
                            else {
                                isListVisible(false);
                                isWelcomeScreenVisible(true);
                            }

                            isListVisible(true);
                            isWelcomeScreenVisible(false);
                          
                            $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                            $("#btnSubmitForApproval,#saveBtn,.table-link").css("display", "inline-block");
                            $("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign,#btnStopAndTerminate,#btnCopyCampaign").removeAttr('disabled');
                        });
                        confirmation.afterCancel(function () {

                            couponModel();
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
                    //} else {
                    //    isEditorVisible(false);
                    //    if (isFromEdit() == true) {
                    //        isListVisible(true);
                    //        isWelcomeScreenVisible(false);
                    //    }
                    //    else {
                    //        isListVisible(false);
                    //        isWelcomeScreenVisible(true);
                    //    }

                    //    $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                    //    $("#btnSubmitForApproval,#saveBtn,.table-link").css("display", "inline-block");
                    //    $("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign,#btnCopyCampaign,#btnStopAndTerminate").removeAttr('disabled');
                    //}
                    isFromEdit(false);
                },

                saveCampaignData = function () {

                    //if (couponModel().isValid()) {
                        if (couponModel().Status() == 3) {
                            saveCampaign(3);

                        } else {
                            saveCampaign(1);

                        }


                    //} else {
                    //    couponModel().errors.showAllMessages();
                    //    toastr.error("Please fill the required feilds to continue.");
                    //}
                },

                 BackToAds = function () {
                     isEditorVisible(false);
                     isWelcomeScreenVisible(false);
                     isListVisible(true);
                 },
                  openEditScreen = function (mode) {
             
                      couponModel(new model.Coupon());
                      campaignNamePlaceHolderValue('New Voucher');
                      isEnableVedioVerificationLink(false);
                 
                      //couponModel().CouponImage2("");
                      //couponModel().CouponImage3("");
                      //couponModel().couponImage1("");
                     
                      isWelcomeScreenVisible(false);

                      isEditorVisible(true);
                      canSubmitForApproval(true);



                      //selectedCriteria();

                
                      view.initializeTypeahead();

                      isEditCampaign(false);

                  },


                submitCampaignData = function () {
                    
                        saveCampaign(2);
                },
                  terminateCampaign = function () {
                      saveCampaign(7);
                  },
                  ArchiveCampaign = function () {
                      saveCampaign(8);
                  },
                saveCampaign = function (mode) {
            
                    var isPopulateErrorList = false;
                 
                      
                        //var selectedCouponCategories = $.grep(couponCategories(), function (n, i) {
                        //    return (n.IsSelected == true);
                        //});

                        //_.each(selectedCouponCategories, function (coup) {

                        //    couponModel().CouponCategories.push(new model.selectedCouponCategory.Create({
                        //        CategoryId: coup.CategoryId,
                        //        Name: coup.Name
                        //    }));
                        //});
                      
                        couponModel().Status(mode);
                        
                       
                        var campignServerObj = couponModel().convertToServerData();

                        dataservice.addCampaignData(campignServerObj, {
                            success: function (data) {

                                isEditorVisible(false);
                                getAdCampaignGridContent();
                                isListVisible(true);
                                isWelcomeScreenVisible(false);
                                toastr.success("Successfully saved.");
                             //   allCouponCodeItems.removeAll();
                            },
                            error: function (response) {

                            }
                        });


                  //  }

                }
                // Add new profile Criteria
                addNewProfileCriteria = function () {},
                  saveProfileQuestion = function (item) {

                      var selectedQuestionstring = $(".active .parent-list-title").text();
                      selectedCriteria().questionString(selectedQuestionstring);
                      selectedCriteria().PQID(item.PQID);
                      var selectedQuestionAnswerstring = item.Answer;
                      selectedCriteria().answerString(selectedQuestionAnswerstring);
                      selectedCriteria().PQAnswerID(item.PqAnswerId);


                      var matchedProfileCriteriaRec = ko.utils.arrayFirst(couponModel().AdCampaignTargetCriterias(), function (arrayitem) {

                          return arrayitem.PQID() == item.PQID
                      });

                      if (matchedProfileCriteriaRec == null) {
                          if (UserAndCostDetail().OtherClausePrice != null) {
                              pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);

                          }
                          couponModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                              Type: 1,
                              PQId: selectedCriteria().PQID(),
                              PQAnswerId: selectedCriteria().PQAnswerID(),
                              SQId: selectedCriteria().SQID(),
                              SQAnswer: selectedCriteria().SQAnswer(),
                              questionString: selectedCriteria().questionString(),
                              answerString: selectedCriteria().answerString(),
                              IncludeorExclude: selectedCriteria().IncludeorExclude(),
                              CampaignId: couponModel().CampaignID,
                              criteriaPrice: UserAndCostDetail().OtherClausePrice
                          }));
                      } else {
                          couponModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                              Type: 1,
                              PQId: selectedCriteria().PQID(),
                              PQAnswerId: selectedCriteria().PQAnswerID(),
                              SQId: selectedCriteria().SQID(),
                              SQAnswer: selectedCriteria().SQAnswer(),
                              questionString: selectedCriteria().questionString(),
                              answerString: selectedCriteria().answerString(),
                              IncludeorExclude: selectedCriteria().IncludeorExclude(),
                              CampaignId: couponModel().CampaignID,
                              criteriaPrice: 0
                          }));
                      }

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

                    var matchedSurveyCriteriaRec = null;

                    _.each(couponModel().AdCampaignTargetCriterias(), function (itemarry) {

                        if (itemarry.QuizCampaignId() == item.CampaignId) {

                            matchedSurveyCriteriaRec = itemarry;
                        }
                    });


                    if (isNewCriteria()) {
                        if (matchedSurveyCriteriaRec == null) {
                            if (UserAndCostDetail().OtherClausePrice != null) {
                                pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);

                            }

                            couponModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                                Type: 6,
                                PQId: selectedCriteria().PQID(),
                                PQAnswerId: selectedCriteria().PQAnswerID(),
                                QuizCampaignId: item.CampaignId,
                                QuizAnswerId: type,
                                questionString: selectedCriteria().questionString(),
                                answerString: selectedCriteria().answerString(),
                                IncludeorExclude: selectedCriteria().IncludeorExclude(),
                                CampaignId: couponModel().CampaignID,
                                criteriaPrice: UserAndCostDetail().OtherClausePrice
                            }));

                        } else {


                            couponModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                                Type: 6,
                                PQId: selectedCriteria().PQID(),
                                PQAnswerId: selectedCriteria().PQAnswerID(),
                                QuizCampaignId: item.CampaignId,
                                QuizAnswerId: type,
                                questionString: selectedCriteria().questionString(),
                                answerString: selectedCriteria().answerString(),
                                IncludeorExclude: selectedCriteria().IncludeorExclude(),
                                CampaignId: couponModel().CampaignID,
                                criteriaPrice: 0
                            }));

                        }
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

                    pricePerclick(pricePerclick() - item.criteriaPrice());
                    couponModel().AdCampaignTargetCriterias.remove(item);

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
                },

                onRemoveLocation = function (item) {
                    // Ask for confirmation
                    deleteLocation(item);

                },

                deleteLocation = function (item) {
                    couponModel().AdCampaignTargetLocations.remove(item);

                    if (couponModel().AdCampaignTargetLocations() == null || couponModel().AdCampaignTargetLocations().length == 0) {
                        isLocationPerClickPriceAdded(false);
                        pricePerclick(pricePerclick() - UserAndCostDetail().LocationClausePrice);
                    }
                    selectedQuestionCountryList([]);
                    _.each(couponModel().AdCampaignTargetLocations(), function (item) {
                        addCountryToCountryList(item.CountryID(), item.Country());
                    });
                    toastr.success("Removed Successfully!");
                },
                //add location
                onAddLocation = function (item) {

                    selectedLocation().Radius = (selectedLocationRadius);
                    selectedLocation().IncludeorExclude = (selectedLocationIncludeExclude);
                    couponModel().AdCampaignTargetLocations.push(new model.AdCampaignTargetLocation.Create({
                        CountryId: selectedLocation().CountryID,
                        CityId: selectedLocation().CityID,
                        Radius: selectedLocation().Radius(),
                        Country: selectedLocation().Country,
                        City: selectedLocation().City,
                        IncludeorExclude: selectedLocation().IncludeorExclude(),
                        CampaignId: couponModel().CampaignID(),
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

                    couponModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                        Language: selected.LanguageName,
                        LanguageId: selected.LanguageId,
                        IncludeorExclude: parseInt(selectedLangIncludeExclude()),
                        Type: 3,
                        CriteriaId: 0,
                        CampaignId: couponModel().CampaignID()
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

                    couponModel().AdCampaignTargetCriterias.remove(item);

                    var matchedLanguageCriterias = ko.utils.arrayFirst(couponModel().AdCampaignTargetCriterias(), function (arrayitem) {

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
                    if (couponModel() == undefined) {
                        return false;
                    }
                    return (couponModel().hasChanges());
                }),

                

                campaignTypeImageCallback = function (file, data) {
                    couponModel().CampaignTypeImagePath(data);
                },

                campaignImageCallback = function (file, data) {
                    couponModel().LogoImageBytes(data);
                    couponModel().LogoUrl(data);
                },
                CouponImage2Callback = function (file, data) {
                    couponModel().CouponImage2(data);
                },
                CouponImage3Callback = function (file, data) {
                    couponModel().CouponImage3(data);
                },
                couponImage1Callback = function (file, data) {
                    couponModel().couponImage1(data);
                },
                  campaignCSVCallback = function (file, data) {
                    
                  },
                onEditCampaign = function (item) {
                    previewScreenNumber(1);
                    isTerminateBtnVisible(false);
                    isNewCampaignVisible(false);
                    isShowArchiveBtn(false);
                    if (item.Status() == 1 || item.Status() == 2 || item.Status() == 3 || item.Status() == 4 || item.Status() == null || item.Status() == 7 || item.Status() == 9) {
                        canSubmitForApproval(true);
                        dataservice.getCampaignData({
                            CampaignId: item.CouponId(),
                            SearchText: ""
                        }, {
                            success: function (data) {
                                
                                if (data != null) {
                                    couponModel(model.Coupon.Create(data.Coupon[0]));
                                   
                                    view.initializeTypeahead();
                                    if (couponModel().Status() == 1) {
                                      
                                        isNewCampaign(true);
                                        couponModel().StatusValue("Draft");
                                    } else if (couponModel().Status() == 2) {
                                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                                        $("#saveBtn").css("display", "none")
                                        $("#btnCancel,#btnPauseCampaign").removeAttr('disabled');
                                        couponModel().StatusValue("Submitted for Approval");
                                    } else if (couponModel().Status() == 3) {
                                        //$("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                                        //$("#saveBtn").css("display", "none");
                                        //$("#btnPauseCampaign").css("display", "inline-block");
                                        //$("#btnCancel,#btnPauseCampaign,#btnCopyCampaign,#btnStopAndTerminate").removeAttr('disabled');
                                        couponModel().StatusValue("Live");
                                        isTerminateBtnVisible(true);
                                        isNewCampaignVisible(true);
                                    } else if (couponModel().Status() == 4) {
                                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey,.table-link").css("display", "none");
                                        $("#saveBtn").css("display", "none");
                                        $("#btnResumeCampagin").css("display", "inline-block");
                                        $("#btnCancel,#btnResumeCampagin,#btnCopyCampaign,#btnStopAndTerminate").removeAttr('disabled');
                                        couponModel().StatusValue("Paused");
                                        isTerminateBtnVisible(true);
                                        isNewCampaignVisible(true);
                                    } else if (couponModel().Status() == 5) {
                                        couponModel().StatusValue("Completed");
                                    } else if (couponModel().Status() == 6) {
                                        couponModel().StatusValue("Approval Rejected");
                                    } else if (couponModel().Status() == 7) {
                                        couponModel().StatusValue("Terminated by user");
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
                                  //  isNewCampaign(false);
                                    //  buildMap();

                                    
                                  
                                    ////buyItQuestionStatus
                                    //// handle 2nd edit error 
                                    ////  $(".modal-backdrop").remove();

                                    //_.each(couponModel().CouponCategories(), function (coup) {
                                    //    _.each(couponCategories(), function (coupcc) {

                                    //        if (coupcc.CategoryId == coup.CategoryId()) {

                                    //            coupcc.IsSelected = true;
                                    //        }
                                    //    });

                                    //});

                                    
                                    //var arrayOfUpdatedList = couponCategories().clone();
                                    //couponCategories.removeAll();
                                    //ko.utils.arrayPushAll(couponCategories(), arrayOfUpdatedList);
                                    //couponCategories.valueHasMutated();
                                    //randonNumber("?r=" + Math.floor(Math.random() * (20 - 1 + 1)) + 1);


                                 
                                    $.unblockUI(spinner);
                                   
                                }

                            },
                            error: function (response) {

                            }
                        });
                    }
                },
                changeStatus = function (status) {
                    if (couponModel() != undefined)
                        saveCampaign(status);

                    $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                    $("#btnSubmitForApproval,#saveBtn,.lang_delSurvey,.table-link").css("display", "inline-block");
                    $("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign").removeAttr('disabled');
                }, 
                nextPreviewScreen = function () {
                    var hasErrors = false;
                    if (previewScreenNumber() == 1) {
                        if (couponModel().CouponTitle() == "" || couponModel().CouponTitle() == undefined) {
                            hasErrors = true;
                            toastr.error("Please enter Coupon Title.");
                        }
                        if (couponModel().Price() == "" || couponModel().Price() == undefined) {
                            hasErrors = true;
                            toastr.error("Please enter Price.");
                        }
                        if (couponModel().Savings() == "" || couponModel().Savings() == undefined) {
                            hasErrors = true;
                            toastr.error("Please enter Saving.");
                        }
                    }
                    if (hasErrors)
                        return;
                    if (previewScreenNumber() < 5) {
                        previewScreenNumber(previewScreenNumber() + 1);
                    }
                    
                },
                
                 backScreen = function () {
                     
                     if (previewScreenNumber() > 1) {
                         previewScreenNumber(previewScreenNumber() - 1);
                     }
                 },
                addIndustry = function (selected) {

                    couponModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                        Industry: selected.IndustryName,
                        IndustryId: selected.IndustryId,
                        IncludeorExclude: true,//parseInt(selected.IndustryIncludeExclude),
                        Type: 4,
                        CampaignId: couponModel().CampaignID()
                    }));

                    $("#searchIndustries").val("");
                    if (UserAndCostDetail().ProfessionClausePrice != null && isIndustoryPerClickPriceAdded() == false) {
                        pricePerclick(pricePerclick() + UserAndCostDetail().ProfessionClausePrice);
                        isIndustoryPerClickPriceAdded(true);
                    }
                },
                addQuizQuestPrice = function () {

                    if (quizQuestionStatus() == false) {
                        if (UserAndCostDetail().QuizQuestionClausePrice != null && isQuizQPerClickPriceAdded() == true) {


                            pricePerclick(pricePerclick() - UserAndCostDetail().QuizQuestionClausePrice);
                            isQuizQPerClickPriceAdded(false);

                        }
                    } else {

                        if (UserAndCostDetail().QuizQuestionClausePrice != null && isQuizQPerClickPriceAdded() == false) {


                            pricePerclick(pricePerclick() + UserAndCostDetail().QuizQuestionClausePrice);
                            isQuizQPerClickPriceAdded(true);

                        }
                    }

                    return true;
                },
                addBuyItPrice = function () {

                    if (buyItQuestionStatus() == false) {
                        if (UserAndCostDetail().BuyItClausePrice != null && isBuyItPerClickPriceAdded() == true) {


                            pricePerclick(pricePerclick() - UserAndCostDetail().BuyItClausePrice);
                            isBuyItPerClickPriceAdded(false);

                        }
                    } else {

                        if (UserAndCostDetail().BuyItClausePrice != null && isBuyItPerClickPriceAdded() == false) {


                            pricePerclick(pricePerclick() + UserAndCostDetail().BuyItClausePrice);
                            isBuyItPerClickPriceAdded(true);

                        }
                    }

                    return true;
                },
                addDeliveryPrice = function () {

                    return true;
                },
                onRemoveIndustry = function (item) {
                    // Ask for confirmation

                    couponModel().AdCampaignTargetCriterias.remove(item);
                    var matchedIndustoryCriterias = ko.utils.arrayFirst(couponModel().AdCampaignTargetCriterias(), function (arrayitem) {

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
                        var matcharry = ko.utils.arrayFirst(couponModel().AdCampaignTargetCriterias(), function (item) {

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
                    couponModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                        Education: selected.Title,
                        EducationId: selected.EducationId,
                        IncludeorExclude: parseInt(selectedEducationIncludeExclude()),
                        Type: 5,
                        CampaignId: couponModel().CampaignID()
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

                    couponModel().AdCampaignTargetCriterias.remove(item);
                    var matchedEducationCriterias = ko.utils.arrayFirst(couponModel().AdCampaignTargetCriterias(), function (arrayitem) {

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
                    _.each(couponModel().AdCampaignTargetLocations(), function (item) {
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
                    _.each(couponModel().AdCampaignTargetCriterias(), function (item) {
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
                        ageFrom: couponModel().AgeRangeStart(),
                        ageTo: couponModel().AgeRangeEnd(),
                        gender: couponModel().Gender(),
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
                    $("#spinnerAudience").css("display", "block");
                    dataservice.getAudienceData(campData, {
                        success: function (data) {
                            $("#spinnerAudience").css("display", "none");
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
                    couponModel().AgeRangeStart.subscribe(function (value) {
                        getAudienceCount();
                    });
                    couponModel().AgeRangeEnd.subscribe(function (value) {
                        getAudienceCount();
                    });
                    couponModel().Gender.subscribe(function (value) {
                        getAudienceCount();
                    });
                    couponModel().AdCampaignTargetLocations.subscribe(function (value) {
                        getAudienceCount();
                        //  buildMap();
                    });
                    couponModel().AdCampaignTargetCriterias.subscribe(function (value) {
                        getAudienceCount();
                    });
                },
                buildMap = function () { },
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

                    var list = ko.utils.arrayFilter(couponModel().AdCampaignTargetLocations(), function (prod) {
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
                    couponModel().VoucherImagePath(data);
                },
                buyItImageCallback = function (file, data) {
                    couponModel().buyItImageBytes(data);
                },
                 LogoUrlImageCallback = function (file, data) {
                     couponModel().LogoImageBytes(data);
                 },
                ShowCouponPromotions = function () {
                   getAdCampaignGridContent();
                },
                ShowAdCampaigns = function () {
                    window.location.href = "/Ads/Ads";
                },
                gotoProfile = function () {
                    window.location.href = "/User/ManageUser/Index";
                },
                gotoManageUsers = function () {
                    window.location.href = "/user/ManageUser/ManageUsers";
                },
                copyCampaign = function (item) {

                    dataservice.copyCampaignById({ CampaignId: item.CampaignID }, {
                        success: function (data) {

                        },
                        error: function (response) {
                            toastr.error("Error while getting audience count.");
                        }
                    });
                },
                showAdditionCriteria = function (mode) {
                    AditionalCriteriaMode(mode);
                },
                showAdditionUserCriteria = function () {
                    isNewCriteria(true);
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
                    AditionalCriteriaMode(2);
                },
                showAdditionQuizCriteria = function () {
                    isNewCriteria(true);
                    var objProfileCriteria = new model.AdCampaignTargetCriteriasModel();

                    objProfileCriteria.Type("1");
                    objProfileCriteria.IncludeorExclude("1");
                    criteriaCount(criteriaCount() + 1);
                    objProfileCriteria.CriteriaID(criteriaCount());
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
                                }

                            },
                            error: function (response) {

                            }
                        });
                    }
                    AditionalCriteriaMode(3);
                },
                // open Product Category Dialog
                openProductCategoryDialog = function () {
                    $("#productCategoryDialog").modal("show");
                },
                 openVideoDialog = function () {
                     
                     if (couponModel().Type() == 1) {
                         var videoLink = couponModel().LandingPageVideoLink();
                         videoLink = videoLink.replace('watch?v=', 'embed/');
                         previewVideoTagUrl(videoLink);
                        // $("#objVideoLink").attr("data", videoLink);
                         $('#appendVideoTag').append('  <object id="objVideoLink" data="' + videoLink + '" width="100%" height="150"></object>');
                     }
                 },
                opencouponCodesDialog = function () {
                       $("#couponCodesDialog").modal("show");
                },
                  closePreviewDialog = function () {
                      
                      if (couponModel().Type() == 1) {
                          $('#appendVideoTag').empty();
                      }
                  },
                addItemToCouponCodeList = function () {
              
                    if ((this.BetterListitemToAdd() != "") && (allCouponCodeItems.indexOf(this.BetterListitemToAdd()) < 0)) {
                        if (this.BetterListitemToAdd().indexOf(',') > 0) {
                           
                            _.each(this.BetterListitemToAdd().split(','), function (item) {
                               
                                if (allCouponCodeItems.indexOf(item) < 0) {
                                    allCouponCodeItems.push(item);
                                    couponModel().CouponCodes.push(new model.AdCampaignCouponCodes.Create({
                                        CodeId: 0,
                                        CampaignId: couponModel().CampaignID(),
                                        Code: item,
                                        IsTaken: false,
                                        UserId: "",
                                        UserName: "",
                                        TakenDateTime: null
                                    }));
                                } 
                            });
                           
                        } else {
                            allCouponCodeItems.push(this.BetterListitemToAdd());
                            couponModel().CouponCodes.push(new model.AdCampaignCouponCodes.Create({
                                CodeId: 0,
                                CampaignId: couponModel().CampaignID(),
                                Code: this.BetterListitemToAdd(),
                                IsTaken: false,
                                UserId: "",
                                UserName: "",
                                TakenDateTime: null
                            }));
                        }
                        BetterListitemToAdd(""); // Clear the text box
                      
                        $("#alreadyaddedCodeError").css("display", "none");
                    } // Prevent blanks and duplicates
                    else {
                        
                        $("#alreadyaddedCodeError").css("display", "block");
                    }
                    couponModel().CouponQuantity(allCouponCodeItems().length);
                },
                removeSelectedCouponCodeItem = function (item) {
          
                    //console.log(allCouponCodeItems().remove(item.Code()));
                    allCouponCodeItems.removeAll();
                    selectedCouponCodeItems([]); // Clear selection

                    couponModel().CouponCodes.remove(item);
                    _.each(couponModel().CouponCodes(), function (cc) {

                        allCouponCodeItems.push(cc.Code);
                    });
                    console.log(allCouponCodeItems());
                    couponModel().CouponQuantity(allCouponCodeItems().length);
                },
                addVoucherClickRate = function () {

                    if (couponModel().IsShowVoucherSetting() == false) {
                        if (UserAndCostDetail().VoucherClausePrice != null && isVoucherPerClickPriceAdded() == true) {


                            pricePerclick(pricePerclick() - UserAndCostDetail().VoucherClausePrice);
                            isVoucherPerClickPriceAdded(false);

                        }
                    } else {

                        if (UserAndCostDetail().VoucherClausePrice != null && isVoucherPerClickPriceAdded() == false) {


                            pricePerclick(pricePerclick() + UserAndCostDetail().VoucherClausePrice);
                            isVoucherPerClickPriceAdded(true);

                        }
                    }

                    
                },
                 updateExistingCodeVal = function (item) {
                     
                     if (event.which == 13 || event.which == 0) {
                         if ((item.Code() != "") && (allCouponCodeItems.indexOf(item.Code()) < 0)) {
                             couponModel().CouponCodes.remove(item);
                             couponModel().CouponCodes.push(new model.AdCampaignCouponCodes.Create({
                                 CodeId: item.CodeId(),
                                 CampaignId: couponModel().CampaignID(),
                                 Code: item.Code(),
                                 IsTaken: item.IsTaken(),
                                 UserId: item.UserId(),
                                 UserName: item.UserName()
                             }));
                             couponModel().CouponQuantity(allCouponCodeItems().length);
                             $("#gridupdateCodeError").css("display", "none");
                         } else {
                             $("#gridupdateCodeError").css("display", "block");
                         }
                         
                     }
                     return true;
                 },
               
                 generateCouponCodes = function () {
                     
                     var gData = {
                         CampaignId: couponModel().CampaignID(),
                         number: numberOFCouponsToGenerate()
                     };
                     dataservice.generateCouponCodes(gData, {
                         success: function (data) {
                             _.each(data.CouponList, function (item) {
                                 allCouponCodeItems.push(item.Code);
                                 couponModel().CouponCodes.push(new model.AdCampaignCouponCodes.Create({
                                     CodeId: item.CodeId,
                                     CampaignId: item.CampaignId,
                                     Code: item.Code,
                                     IsTaken: false,
                                     UserId: item.UserId,
                                     UserName: "",
                                     TakenDateTime: null
                                 }));
                             });
                             //var cQty = parseInt(couponModel().CouponQuantity()) + parseInt(numberOFCouponsToGenerate());
                             couponModel().CouponQuantity(data.CouponQuantity);
                             numberOFCouponsToGenerate(0);
                             toastr.success("Codes generated successfully.");
                         },
                         error: function (response) {
                             toastr.error("Error while generating codes.");
                         }
                     });
                 },
                showCouponGenerationWarning = function () {
                    toastr.warning("Please first save the coupon.");
                },
                 gotoScreen = function (number) {
                     previewScreenNumber(number);

                 },
                // Initialize the view model
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    for (var i = 10; i < 81; i++) {
                        var text = i.toString();
                        if (i == 110)
                            text += "+";
                        ageRange.push({ value: i.toString(), text: text });
                    }
                    ageRange.push({ value: 120, text: "80+" });
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
                    couponModel: couponModel,
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
                    campaignTypeImageCallback: campaignTypeImageCallback,
                    campaignImageCallback: campaignImageCallback,
                    couponImage1Callback: couponImage1Callback,
                    CouponImage3Callback: CouponImage3Callback,
                    CouponImage2Callback: CouponImage2Callback,
                    correctAnswers: correctAnswers,
                    onEditCampaign: onEditCampaign,
                    canSubmitForApproval: canSubmitForApproval,
                    isTerminateBtnVisible: isTerminateBtnVisible,
                    isNewCampaignVisible: isNewCampaignVisible,
                    isShowArchiveBtn: isShowArchiveBtn,
                    submitCampaignData: submitCampaignData,
                    terminateCampaign: terminateCampaign,
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
                    ChangeVoucherSettings: ChangeVoucherSettings,
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
                    ShowAdCampaigns: ShowAdCampaigns,
                    ShowCouponPromotions: ShowCouponPromotions,
                    gotoProfile: gotoProfile,
                    gotoManageUsers: gotoManageUsers,
                    ArchiveCampaign: ArchiveCampaign,
                    copyCampaign: copyCampaign,
                    AditionalCriteriaMode: AditionalCriteriaMode,
                    showAdditionCriteria: showAdditionCriteria,
                    showAdditionUserCriteria: showAdditionUserCriteria,
                    showAdditionQuizCriteria: showAdditionQuizCriteria,
                    addBuyItPrice: addBuyItPrice,
                    couponCategories: couponCategories,
                    openProductCategoryDialog: openProductCategoryDialog,
                    quizQuestionStatus: quizQuestionStatus,
                    quizPriceLbl: quizPriceLbl,
                    tenPriceLbl: tenPriceLbl,
                    fivePriceLbl: fivePriceLbl,
                    threePriceLbl: threePriceLbl,
                    addDeliveryPrice: addDeliveryPrice,
                    addQuizQuestPrice: addQuizQuestPrice,
                    campaignNamePlaceHolderValue: campaignNamePlaceHolderValue,
                    genderppc: genderppc,
                    professionppc: professionppc,
                    ageppc: ageppc,
                    buyItPriceLbl: buyItPriceLbl,
                    BetterListitemToAdd: BetterListitemToAdd,
                    allCouponCodeItems: allCouponCodeItems,
                    selectedCouponCodeItems: selectedCouponCodeItems,
                    addItemToCouponCodeList: addItemToCouponCodeList,
                    removeSelectedCouponCodeItem: removeSelectedCouponCodeItem,
                    opencouponCodesDialog: opencouponCodesDialog,
                    campaignCSVCallback: campaignCSVCallback,
                    updateExistingCodeVal: updateExistingCodeVal,
                    UsedCouponQuantity: UsedCouponQuantity,
                    advertiserLogo: advertiserLogo,
                    openVideoDialog: openVideoDialog,
                    previewVideoTagUrl: previewVideoTagUrl,
                    closePreviewDialog: closePreviewDialog,
                    LogoUrlImageCallback: LogoUrlImageCallback,
                    randonNumber: randonNumber,
                    vouchers:vouchers,
                    voucherPriceLbl: voucherPriceLbl,
                    numberOFCouponsToGenerate: numberOFCouponsToGenerate,
                    showCouponGenerationWarning: showCouponGenerationWarning,
                    previewScreenNumber: previewScreenNumber,
                    nextPreviewScreen: nextPreviewScreen,
                    gotoScreen:gotoScreen,
                    backScreen: backScreen,
                    CurrPage: CurrPage,
                    MaxPage: MaxPage
                };
            })()
        };
        return ist.Coupons.viewModel;
    });
