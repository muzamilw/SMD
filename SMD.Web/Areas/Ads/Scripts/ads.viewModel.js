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
                    buyItQuestionStatus = ko.observable(false),
					isAdvertdashboardVisible = ko.observable(false),
                    ButItOtherLabel = ko.observable(''),
                    langs = ko.observableArray([]),
                    TemporaryList = ko.observableArray([]),
                    TemporaryProfileList = ko.observableArray([]),
                    TemporaryQuizQuestions = ko.observableArray([]),
                    TemporarySurveyList = ko.observableArray([]),
                    BuyItStatus = ko.observable(false),
                    countoryidList = [],
                    cityidList = [],
                    langidList = [],
                    campaignModel = ko.observable(),
                    selectedCriteria = ko.observable(),
                    profileQuestionList = ko.observable([]),
                    myQuizQuestions = ko.observableArray([]),
                    profileAnswerList = ko.observable([]),
                    ShowAudienceCounter = ko.observable(),
                    surveyAnswerList = ko.observable([]),
                    SearchProfileQuestion = ko.observable(''),
                    criteriaCount = ko.observable(0),
                    isShowSurveyAns = ko.observable(false),
                    IsprofileQuestion = ko.observable(false),
                     // selected location 
                    selectedLocation = ko.observable(),
                    selectedLocationRadius = ko.observable(),
                    selectedLocationIncludeExclude = ko.observable(true),
                    selectedLangIncludeExclude = ko.observable(true),
                    selectedLocationLat = ko.observable(0),
                    selectedLocationLong = ko.observable(0),
                    ageRange = ko.observableArray([]),
                    isNewCriteria = ko.observable(true),
                    IsShownforVideo = ko.observable(true),
                    isEnableVedioVerificationLink = ko.observable(false),
                    //caption variablels 
                    lblCampaignName = ko.observable("Campaign Name"),
                    Modelheading = ko.observable(""),
                    lblDetailsHeading = ko.observable("Free Display Ad Details"),
                    lblAdTitle = ko.observable("Ad Title"),
                    lblFirstLine = ko.observable("First line"),
                    lbllSecondLine = ko.observable("Second Line"),
                    lblCampaignSchedule = ko.observable("Schedule"),
                    campaignTypePlaceHolderValue = ko.observable('Enter in the YouTube video link'),
                //
                    isEditCampaign = ko.observable(false),
                    canSubmitForApproval = ko.observable(true),
                    isNewCampaignVisible = ko.observable(false),
                    isShowArchiveBtn = ko.observable(false),
                    isTerminateBtnVisible = ko.observable(false),
                    correctAnswers = ko.observableArray([{ id: 1, name: "Choice 1" }, { id: 2, name: "Choice 2" }, { id: 3, name: "Choice 3" }, { id: 0, name: "Any of the above" }]),
                    DefaultTextBtns = ko.observableArray([
                        { id: "Apply Now", name: "Apply Now" },
                        { id: "Book Now", name: "Book Now" },
                        { id: "Contact Us", name: "Contact Us" },
                        { id: "Download", name: "Download" },
                        { id: "Learn More", name: "Learn More" },
                        { id: "Shop Now", name: "Shop Now" },
                        { id: "Sign Up", name: "Sign Up" },
                        { id: "Watch More", name: "Watch More" },
                        { id: "Buy Now", name: "Buy Now" },
                        { id: "Check Availability", name: "Check Availability" },
                        { id: "Custom Button Label", name: "Custom Button Label" }
                    ]),
                    selectedIndustryIncludeExclude = ko.observable(true),
                    UserAndCostDetail = ko.observable(),
                    pricePerclick = ko.observable(0),
                    UrlHeadings = ko.observable('')
                isLocationPerClickPriceAdded = ko.observable(false),
                isLanguagePerClickPriceAdded = ko.observable(false),
                isIndustoryPerClickPriceAdded = ko.observable(false),
                isProfileSurveyPerClickPriceAdded = ko.observable(false),
                isEducationPerClickPriceAdded = ko.observable(false),
                isBuyItPerClickPriceAdded = ko.observable(false),
                isVoucherPerClickPriceAdded = ko.observable(false),
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
                MainHeading = ko.observable("Video ads - Upload a video and target audiences near you."),
                uploadTitle = ko.observable("Video"),
                SubHeading = ko.observable("Reward audiences 50% of your ‘ad click’ Increase branding and drive people to your web site with one ‘ad click’ Show a video ad, ask a reinforcing question and show your deals.");

                tab1Heading = ko.observable("Upload your video commercial (max 30 seconds , mp4 format).");
                tab2Heading = ko.observable("Target audience in different cities.");
                tab4SubHeading = ko.observable("Select your ad campaign delivery mode:");
                errorListNew = ko.observableArray([]),
                // unique country list used to bind location dropdown
                selectedQuestionCountryList = ko.observableArray([]),
                educations = ko.observableArray([]),
                professions = ko.observableArray([]),
                surveyquestionList = ko.observableArray([]),
                voucherQuestionStatus = ko.observable(false),
                
                AditionalCriteriaMode = ko.observable("1"), //1 = main buttons, 2 = profile questions , 3 = ad linked questions
            showCompanyProfileQuestions = ko.observable(false),
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
                previewScreenNumber = ko.observable(1),
                VideoLink2src = ko.observable(),
                TodisplayImg = ko.observable(true),
                SearchSelectedStatus = ko.observable();
                FlagToShowDivs = ko.observable(false);

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
                    status: SearchSelectedStatus(),
                    PageSize: pager().pageSize(),
                    PageNo: pager().currentPage(),
                    SearchText: searchFilterValue(),
                    ShowCoupons: isDisplayCouponsAds(),
                    mode: mode
                }, {
                    success: function (data) {
                        if (data != null) {

                            // set grid content
                            campaignGridContent.removeAll();
                            _.each(data.Campaigns, function (item) {

                                campaignGridContent.push(model.Campaign.Create(updateCampaignGridItem(item)));
                            });
                            pager().totalCount(data.TotalCount);

                            //  LoadAnswers();
                        }

                    },
                    error: function (response) {

                    }
                });

            },
                LoadAnswers = function () {


                    dataservice.getBaseData({
                        RequestId: 3,
                        QuestionId: 0,
                    }, {
                        success: function (data) {
                            if (data != null) {

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
                    item.StatusValue = ("Remove");
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



                $("#logo_div").css("display", "block");

                $("#panelArea").css("display", "none");

                $(".hideInCoupons").css("display", "none");

                $("#MarketobjDiv").css("display", "none");
                $("#topArea").css("display", "none");
                $("#headlabel,#headdesc").css("display", "none");

                collapseMainMenu();
                TodisplayImg(true);
                openEditScreen(1);
                isFromEdit(true);

                isListVisible(false);
                isNewCampaign(true);
                isTerminateBtnVisible(false);
                isNewCampaignVisible(false);
                VideoLink2src(0);
                FlagToShowDivs(false);
                campaignModel().StatusValue('Draft');
                VideoLink2src(null);
                isShowArchiveBtn(false);
                campaignModel().ChannelType("1");
                campaignModel().ClickRate("0.12");
                campaignModel().MaxDailyBudget("5");
                campaignModel().MaxBudget("20");
                campaignModel().Type(mode);
                campaignModel().DeliveryDays("3");
                campaignModel().LandingPageVideoLink("https://www.");

                if (mode == 4) {
                    campaignModel().CampaignName("New display ad");
                    $("#logo_div").css("display", "block");
                }

                else {
                    campaignModel().CampaignName("New video campaign");
                    $("#logo_div").css("display", "none");
                }


                campaignModel().reset();


            },
           GoToHomePage = function () {
               //isListVisible(false);
               isEditorVisible(false);
               isWelcomeScreenVisible(false);
               $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
               $("#btnSubmitForApproval,#saveBtn,.table-link").css("display", "inline-block");
               $("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign,#btnStopAndTerminate,#btnCopyCampaign,#imgLogo").removeAttr('disabled');
               // showMainMenu();
           },
            closeNewCampaignDialog = function () {

                if (campaignModel().hasChanges() || logoImage != '') {    //&& (campaignModel().Status() == null || campaignModel().Status() == 1)

                    confirmation.messageText("Do you want to save changes?");
                    confirmation.afterProceed(function () {

                        if (ValidateCampaign()) {

                            if (campaignModel().Status() == 3) {
                                saveCampaign(3);

                            } else {
                                saveCampaign(1);

                            }

                            isEditorVisible(false);

                            if (isFromEdit() == true) {
                                isListVisible(true);
                                isWelcomeScreenVisible(false);
                            }
                            else {
                                isListVisible(false);
                                isWelcomeScreenVisible(true);
                            }

                            if (isDisplayCouponsAds() == true) {
                                isListVisible(true);
                                isWelcomeScreenVisible(false);
                            }

                            $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                            $("#btnSubmitForApproval,#saveBtn,.table-link").css("display", "inline-block");
                            $("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign,#btnStopAndTerminate,#btnCopyCampaign,#imgLogo").removeAttr('disabled');

                            $("#headlabel, #Heading_div").css("display", "block");

                            $("#panelArea,#headdesc").css("display", "block");
                            //show the main menu;
                            showMainMenu();
                            logoImage = '';


                        }
                        else {
                            if (errorListNew().length > 0) {
                                ko.utils.arrayForEach(errorListNew(), function (errorListNew) {
                                    toastr.error(errorListNew.name);
                                });
                            }
                        }




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
                        //show the main menu;
                        showMainMenu();
                        $("input,button,textarea,a,select").removeAttr('disabled');
                        $("#headlabel").css("display", "block");

                        $(".hideInCoupons").css("display", "block");

                        $("#MarketobjDiv").css("display", "block");
                        $("#topArea,#headdesc").css("display", "block");
                    });

                    confirmation.show();


                    return;
                } else { // no changes go close it
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
                    //show the main menu;
                    showMainMenu();

                    $("input,button,textarea,a,select").removeAttr('disabled');



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
                }
                isFromEdit(false);
                $("#panelArea").css("display", "block");
                $(".hideInCoupons").css("display", "block");

                $("#MarketobjDiv").css("display", "block");
                $("#topArea").css("display", "block");
                $("#headlabel").css("display", "block");
                $("#headdesc").css("display", "block")

            },


                   GetAudienceCount = function (val) {
                       while (/(\d+)(\d{3})/.test(val.toString())) {
                           val = val.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
                       }
                       return val;

                   },

                closeContent = function () {
                    campaignModel();
                    selectedCriteria();
                    isEditorVisible(false);
                    if (isFromEdit() == true) {
                        isListVisible(true);
                        isWelcomeScreenVisible(false);
                    }
                    else {

                        if (campaignModel().Status() == 7) {
                            isWelcomeScreenVisible(false);
                            isListVisible(true);
                        }
                        else {
                            isWelcomeScreenVisible(true);
                            isListVisible(false);
                        }
                    }
                    //show the main menu;
                    showMainMenu();

                    $("input,button,textarea,a,select").removeAttr('disabled');



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

                    isFromEdit(false);
                    $("#panelArea").css("display", "block");

                    $(".hideInCoupons").css("display", "block");

                    $("#MarketobjDiv").css("display", "block");
                    $("#topArea").css("display", "block");
                    $("#headlabel").css("display", "block");

                    $("#headdesc").css("display", "block");
                },

             BackToAds = function () {
                 isEditorVisible(false);
                 isWelcomeScreenVisible(false);
                 isListVisible(true);
             },
              openEditScreen = function (mode) {

                  campaignModel(new model.Campaign());
                  // campaignModel().CampaignName('New Campaign');

                  if (mode == 1) { // video
                      campaignModel().Type('1');
                      isEnableVedioVerificationLink(true);
                  }
                  else if (mode == 2) {
                      campaignModel().Type('3');
                      isEnableVedioVerificationLink(true);
                  }
                  else if (mode == 3) { // flyer
                      campaignModel().Type('3');
                      isEnableVedioVerificationLink(true);
                  }
                  else if (mode == 4) { // game
                      campaignModel().Type('4');
                      isEnableVedioVerificationLink(false);
                  } else if (mode == 5) {
                      //campaignModel().CampaignName('New Coupon');
                      campaignNamePlaceHolderValue('New Voucher');
                      isEnableVedioVerificationLink(false);
                      campaignModel().Type('5');
                      lblCampaignName("Voucher Name");
                      lblDetailsHeading("Voucher Display Details");
                      lblAdTitle("Voucher Title");
                      lblFirstLine("First line");
                      lbllSecondLine("Second Line");
                      lblCampaignSchedule("Schedule");
                      campaignModel().couponImage2("");
                      campaignModel().CouponImage3("");
                      campaignModel().CouponImage4("");
                      campaignModel().CouponType('1');
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

                  campaignModel().IsUseFilter('0');
                  campaignModel().CampaignImagePath("");
                  campaignModel().VoucherImagePath("");
                  campaignModel().LanguageId(41);
                  campaignModel().DeliveryDays('10');
                  campaignModel().LogoUrl('/images/standardplaceholder.png');

                  // campaignModel().LogoImageBytes("/images/default-placeholder.png");

                  campaignModel().IsShowVoucherSetting(false);
                  if (UserAndCostDetail() != null || UserAndCostDetail() != undefined) {
                      alreadyAddedDeliveryValue(10);
                      quizQuestionStatus(true);
                      isQuizQPerClickPriceAdded(true);
                      pricePerclick(pricePerclick() + UserAndCostDetail().QuizQuestionClausePrice);
                      pricePerclick(pricePerclick() + UserAndCostDetail().TenDayDeliveryClausePrice);
                  }

                  getAudienceCount();

                  bindAudienceReachCount();
                  selectedQuestionCountryList([]);

              },


            submitCampaignData = function () {
                //if (campaignModel().isValid()) {
                if (ValidateCampaign()) {
                    if (UserAndCostDetail().IsSpecialAccount == true) {
                        campaignModel().ClickRate(0);
                        saveCampaign(2);
                    }
                    else {
                        if (UserAndCostDetail().isStripeIntegrated == false) {

                            stripeChargeCustomer.show(function () {
                                UserAndCostDetail().isStripeIntegrated = true;
                                saveCampaign(2);
                            }, 2000, 'Enter your details');


                        } else {
                            saveCampaign(2);
                        }
                    }

                }
                else {
                    if (errorListNew().length > 0) {

                        ko.utils.arrayForEach(errorListNew(), function (errorListNew) {

                            toastr.error(errorListNew.name);
                        });
                    }
                }
                //} else {
                //    campaignModel().errors.showAllMessages();
                //    toastr.error("Please fill the required feilds to continue.");
                //}
            },
                 ValidateCampaign = function () {

                     errorListNew.removeAll();

                     if (campaignModel().CampaignName() == "" || campaignModel().CampaignName() == undefined) {
                         errorListNew.push({ name: "Please enter ad Title.", element: "" });

                     }

                     if (campaignModel().ClickRate() == undefined) {
                         campaignModel().ClickRate(0);
                     }

                     if (campaignModel().ClickRate() < 0.06) {
                         errorListNew.push({ name: "Ad Click should be greater than $ 0.06 USD", element: "" });
                     }

                     if ((parseInt(campaignModel().MaxBudget()) < parseInt(campaignModel().ClickRate()))) {
                         errorListNew.push({ name: "Campaign budget should be greater than Ad click.", element: "" });
                     }

                     if (reachedAudience() == 0) {
                         errorListNew.push({
                             name: "You have no audience against the specified criteria please broaden your audience definition.", element: ""
                         });
                     }

                     if (errorListNew() == null || errorListNew().length == 0) {
                         return true;
                     } else {
                         return false;
                     }
                 },
                SaveDraftCampaign = function () {
                    if (ValidateCampaign()) {
                        saveCampaign(1);
                    }
                    else {
                        if (errorListNew().length > 0) {

                            ko.utils.arrayForEach(errorListNew(), function (errorListNew) {

                                toastr.error(errorListNew.name);
                            });
                        }

                    }
                },

              removeAdd = function (item) {
                  if (item.Status() == 1)
                      campaignModel().CampaignID(item.CampaignID());
                  confirmation.messageText("Are you sure you want to remove this ad ? This action cannot be undone.");
                  confirmation.show();
                  confirmation.afterCancel(function () {
                      confirmation.hide();
                  });
                  confirmation.afterProceed(function () {
                      if (campaignModel() != undefined)
                          saveCampaign(7);
                  });
              },
              ArchiveCampaign = function () {
                  saveCampaign(8);
              },
            saveCampaign = function (mode) {

                var isPopulateErrorList = false;
                if (isDisplayCouponsAds() == false) {

                    if (campaignModel().Type() != 4) {
                        isPopulateErrorList = true;
                        //if (quizQuestionStatus() == true) {
                        //    if ((campaignModel().VerifyQuestion() == null || campaignModel().VerifyQuestion() == '') || (campaignModel().Answer1() == null || campaignModel().Answer1() == '') || (campaignModel().Answer2() == null || campaignModel().Answer2() == '')) {
                        //        isPopulateErrorList = true;
                        //    }
                        //    if (campaignModel().VerifyQuestion() == null || campaignModel().VerifyQuestion() == '') {
                        //        $("#txtVerifyQuestion").addClass("errorFill");
                        //    } else {
                        //        $("#txtVerifyQuestion").removeClass("errorFill");
                        //    }
                        //    if (campaignModel().Answer1() == null || campaignModel().Answer1() == '') {
                        //        $("#txtVerifyAnswer1").addClass("errorFill");
                        //    } else {
                        //        $("#txtVerifyAnswer1").removeClass("errorFill");
                        //    }
                        //    if (campaignModel().Answer2() == null || campaignModel().Answer2() == '') {
                        //        $("#txtVerifyAnswer2").addClass("errorFill");
                        //    } else {
                        //        $("#txtVerifyAnswer2").removeClass("errorFill");
                        //    }


                        //}
                    }



                    if (buyItQuestionStatus() == true) {

                        //if ((campaignModel().BuuyItLine1() == null || campaignModel().BuuyItLine1() == '') || (campaignModel().BuyItLine2() == null || campaignModel().BuyItLine2() == '') || (campaignModel().BuyItLine3() == null || campaignModel().BuyItLine3() == '') || (campaignModel().VideoUrl() == null || campaignModel().VideoUrl() == '') || (campaignModel().BuyItButtonLabel() == null || campaignModel().BuyItButtonLabel() == '')) {
                        //    isPopulateErrorList = true;
                        //}
                        //if (campaignModel().BuuyItLine1() == null || campaignModel().BuuyItLine1() == '') {
                        //    $("#txtBuyItLine1").addClass("errorFill");
                        //} else {
                        //    $("#txtBuyItLine1").removeClass("errorFill");
                        //}
                        //if (campaignModel().BuyItLine2() == null || campaignModel().BuyItLine2() == '') {
                        //    $("#txtBuyItLine2").addClass("errorFill");
                        //} else {
                        //    $("#txtBuyItLine2").removeClass("errorFill");
                        //}
                        //if (campaignModel().BuyItLine3() == null || campaignModel().BuyItLine3() == '') {
                        //    $("#txtBuyItLine3").addClass("errorFill");
                        //} else {
                        //    $("#txtBuyItLine3").removeClass("errorFill");
                        //}
                        //if (campaignModel().VideoUrl() == null || campaignModel().VideoUrl() == '') {
                        //    $("#txtBuyItUrl").addClass("errorFill");
                        //} else {
                        //    $("#txtBuyItUrl").removeClass("errorFill");
                        //}
                        //if (campaignModel().BuyItButtonLabel() == null || campaignModel().BuyItButtonLabel() == '') {
                        //    $("#txtBuyItlbl").addClass("errorFill");
                        //} else {
                        //    $("#txtBuyItlbl").removeClass("errorFill");
                        //}
                    }

                }




                //if (isPopulateErrorList == true) {
                //    errorList.removeAll();
                //    errorList.push({ name: "Please fill the required feilds to continue." });
                //} else {
                //    errorList.removeAll();
                //    if (isDisplayCouponsAds() == true) {
                //        if (campaignModel().CouponQuantity() != null &&  parseInt(campaignModel().CouponQuantity()) > 0) {
                //            if (allCouponCodeItems() == null || allCouponCodeItems().length == 0) {
                //                errorList.push({ name: "Please add coupon codes to proceed." });
                //                $("#couponCodeSelectorLink").addClass("errorOutline");
                //            }
                //            else if (parseInt(campaignModel().CouponQuantity()) != allCouponCodeItems().length) {
                //                errorList.push({ name: "Please add coupon codes equals to codes quantity to proceed." });
                //                $("#couponCodeSelectorLink").addClass("errorOutline");
                //            }
                //        } 

                //    }
                //}

                //if (errorList().length > 0) {
                //    return false;
                //} else {

                //bullshit code written below by someone. 
                ////if (quizQuestionStatus() == false) {
                ////    campaignModel().VerifyQuestion('');
                ////    campaignModel().Answer1('');
                ////    campaignModel().Answer2('');

                ////}
                if (isDisplayCouponsAds() == true) {

                    var selectedCouponCategories = $.grep(couponCategories(), function (n, i) {
                        return (n.IsSelected == true);
                    });

                    _.each(selectedCouponCategories, function (coup) {

                        campaignModel().CouponCategories.push(new model.selectedCouponCategory.Create({
                            CategoryId: coup.CategoryId,
                            Name: coup.Name
                        }));
                    });

                    //_.each(allCouponCodeItems(), function (coupcode) {

                    //    campaignModel().CouponCodes.push(new model.AdCampaignCouponCodes.Create({
                    //        CodeId: 0,
                    //        CampaignId: campaignModel().CampaignID(),
                    //        Code: coupcode,
                    //        IsTaken: null,
                    //        UserId: null
                    //    }));
                    //});
                }


                if (campaignModel().IsUseFilter() == "0") {
                    campaignModel().IsUseFilter(0);
                } else {
                    campaignModel().IsUseFilter(1);
                }
                debugger;

                campaignModel().Status(mode);

                //disabling the following line so that user can customize their click rate.
                //campaignModel().ClickRate(pricePerclick());


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
                        allCouponCodeItems.removeAll();

                        $("#topArea").css("display", "block");
                        //$("#MainBtnClose").click();
                        //GoToHomePage();

                        closeContent();
                    },
                    error: function (response) {
                        $("#topArea").css("display", "block");
                    }
                });
            },

                // Add new profile Criteria
                addNewProfileCriteria = function () {

                    //isNewCriteria(true);
                    //AditionalCriteriaMode("1");
                    //var objProfileCriteria = new model.AdCampaignTargetCriteriasModel();

                    //objProfileCriteria.Type("1");
                    //objProfileCriteria.IncludeorExclude("1");
                    //criteriaCount(criteriaCount() + 1);
                    //objProfileCriteria.CriteriaID(criteriaCount());
                    //selectedCriteria(objProfileCriteria);


                    //if (profileQuestionList().length == 0) {
                    //    dataservice.getBaseData({
                    //        RequestId: 2,
                    //        QuestionId: 0,
                    //    }, {
                    //        success: function (data) {
                    //            if (data != null) {
                    //                profileQuestionList([]);
                    //                ko.utils.arrayPushAll(profileQuestionList(), data.ProfileQuestions);
                    //                profileQuestionList.valueHasMutated();
                    //            }

                    //        },
                    //        error: function (response) {

                    //        }
                    //    });
                    //}
                    //if (myQuizQuestions().length == 0) {
                    //    dataservice.getBaseData({
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
                },
                  saveProfileQuestion = function (item) {

                      var selectedQuestionstring = $(".active .parent-list-title").text();
                      selectedCriteria().questionString(selectedQuestionstring);
                      selectedCriteria().PQID(item.PQID);
                      var selectedQuestionAnswerstring = item.Answer;
                      selectedCriteria().answerString(selectedQuestionAnswerstring);
                      selectedCriteria().PQAnswerID(item.PqAnswerId);


                      var matchedProfileCriteriaRec = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (arrayitem) {

                          return arrayitem.PQID() == item.PQID
                      });

                      if (matchedProfileCriteriaRec == null) {
                          if (UserAndCostDetail().OtherClausePrice != null) {
                              pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);

                          }
                          campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                              Type: 1,
                              PQId: selectedCriteria().PQID(),
                              PQAnswerId: selectedCriteria().PQAnswerID(),
                              SQId: selectedCriteria().SQID(),
                              SQAnswer: selectedCriteria().SQAnswer(),
                              questionString: selectedCriteria().questionString(),
                              answerString: selectedCriteria().answerString(),
                              IncludeorExclude: selectedCriteria().IncludeorExclude(),
                              CampaignId: campaignModel().CampaignID,
                              criteriaPrice: UserAndCostDetail().OtherClausePrice
                          }));
                      } else {
                          campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                              Type: 1,
                              PQId: selectedCriteria().PQID(),
                              PQAnswerId: selectedCriteria().PQAnswerID(),
                              SQId: selectedCriteria().SQID(),
                              SQAnswer: selectedCriteria().SQAnswer(),
                              questionString: selectedCriteria().questionString(),
                              answerString: selectedCriteria().answerString(),
                              IncludeorExclude: selectedCriteria().IncludeorExclude(),
                              CampaignId: campaignModel().CampaignID,
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

                     updateSurveyCriteriass = function (type, item) {
                         selectedCriteria().SQAnswer(type);
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

                    _.each(campaignModel().AdCampaignTargetCriterias(), function (itemarry) {

                        if (itemarry.QuizCampaignId() == item.CampaignId) {

                            matchedSurveyCriteriaRec = itemarry;
                        }
                    });


                    if (isNewCriteria()) {
                        if (matchedSurveyCriteriaRec == null) {
                            if (UserAndCostDetail().OtherClausePrice != null) {
                                pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);

                            }

                            campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                                Type: 6,
                                PQId: selectedCriteria().PQID(),
                                PQAnswerId: selectedCriteria().PQAnswerID(),
                                QuizCampaignId: item.CampaignId,
                                QuizAnswerId: type,
                                questionString: selectedCriteria().questionString(),
                                answerString: selectedCriteria().answerString(),
                                IncludeorExclude: selectedCriteria().IncludeorExclude(),
                                CampaignId: campaignModel().CampaignID,
                                criteriaPrice: UserAndCostDetail().OtherClausePrice
                            }));

                        } else {


                            campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                                Type: 6,
                                PQId: selectedCriteria().PQID(),
                                PQAnswerId: selectedCriteria().PQAnswerID(),
                                QuizCampaignId: item.CampaignId,
                                QuizAnswerId: type,
                                questionString: selectedCriteria().questionString(),
                                answerString: selectedCriteria().answerString(),
                                IncludeorExclude: selectedCriteria().IncludeorExclude(),
                                CampaignId: campaignModel().CampaignID,
                                criteriaPrice: 0
                            }));

                        }
                    }
                    $(".close").click();
                    isShowSurveyAns(false);
                },
                 handleBuyIt = function (item) {
                     var selectionoption = $("#ddTextBtns").val();

                     if (selectionoption == '0') {
                         buyItQuestionStatus(false);
                         campaignModel().ShowBuyitBtn(false);
                         BuyItStatus(false);
                         ButItOtherLabel('');
                     }
                     else if (selectionoption == '999')  //other scenario
                     {
                         buyItQuestionStatus(true);
                         campaignModel().ShowBuyitBtn(true);
                          BuyItStatus(true);
                         campaignModel().BuyItButtonLabel('');

                     }
                     else if (selectionoption == 'Custom Button Label')
                     {
                         BuyItStatus(true);
                         campaignModel().ShowBuyitBtn(true);
                     }
                     else {
                         buyItQuestionStatus(false);
                         campaignModel().ShowBuyitBtn(true);
                         BuyItStatus(false);
                         ButItOtherLabel('');
                         campaignModel().BuyItButtonLabel('');
                     }

                 },
                 saveSurveyQuestion = function (type, item) {

                     var selectedQuestionstring = item.DisplayQuestion;
                     selectedCriteria().questionString(selectedQuestionstring);

                     selectedCriteria().SQID(item.SQID);


                     if (type == 1) {
                         selectedCriteria().answerString(item.LeftPicturePath);
                     } else {
                         selectedCriteria().answerString(item.RightPicturePath);
                     }

                     var matchedSurveyCriteriaRec = null;

                     _.each(campaignModel().AdCampaignTargetCriterias(), function (itemarry) {

                         if (itemarry.SQID == item.SQID) {

                             matchedSurveyCriteriaRec = itemarry;
                         }
                     });


                     if (isNewCriteria()) {
                         if (matchedSurveyCriteriaRec == null) {
                             if (UserAndCostDetail().OtherClausePrice != null) {
                                 pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);

                             }

                             campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                                 Type: 2,
                                 SQId: item.SQID,
                                 SQAnswer: type,
                                 //PQAnswerId: selectedCriteria().PQAnswerID(),
                                 //QuizCampaignId: item.CampaignId,
                                 //QuizAnswerId: type,
                                 questionString: selectedCriteria().questionString(),
                                 answerString: selectedCriteria().answerString(),
                                 IncludeorExclude: selectedCriteria().IncludeorExclude(),
                                 //  CampaignId: campaignModel().CampaignID,
                                 criteriaPrice: UserAndCostDetail().OtherClausePrice
                             }));

                         } else {


                             campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                                 Type: 2,
                                 SQID: item.SQID,
                                 SQAnswer: type,
                                 // QuizCampaignId: item.CampaignId,
                                 // QuizAnswerId: type,
                                 questionString: selectedCriteria().questionString(),
                                 answerString: selectedCriteria().answerString(),

                                 IncludeorExclude: selectedCriteria().IncludeorExclude(),
                                 //  CampaignId: campaignModel().CampaignID,
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
                    campaignModel().AdCampaignTargetCriterias.remove(item);

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

                onChangeProfileQuestion = function (item) {

                    var y = $(".listview").scrollTop();  //your current y position on the page

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

                                $(".listview").scrollTop(y + 60);
                            }

                        },
                        error: function (response) {

                        }
                    });
                },
                 onChangeSurveyQuestion = function (item) {

                     var y = $(".listview").scrollTop();  //your current y position on the page

                     if (item == null)
                         return;
                     var selectedQuestionId = item.SQID;

                     dataservice.getBaseData({
                         RequestId: 7,
                         QuestionId: selectedQuestionId,
                     }, {
                         success: function (data) {

                             if (data != null) {
                                 if (surveyAnswerList().length > 0) {
                                     surveyAnswerList([]);
                                 }

                                 _.each(data.GetSurveyQuestionAnser, function (question) {
                                     question.SQID = item.SqId;
                                 });

                                 ko.utils.arrayPushAll(surveyAnswerList(), data.SurveyQuestions);

                                 surveyAnswerList.valueHasMutated();

                                 $(".listview").scrollTop(y + 60);
                             }
                         },
                         error: function (response) {

                         }
                     });
                 },


                onRemoveLocation = function (item) {
                    // Ask for confirmation
                    deleteLocation(item);

                },

                deleteLocation = function (item) {
                    //if (item.CountryID() == UserAndCostDetail().CountryId && item.CityID() == UserAndCostDetail().CityId) {
                    //    toastr.error("You cannot remove your home town or country!");
                    //} else {
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
                    // }

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
                        campaignTypePlaceHolderValue('Enter in the YouTube video link (20 characters)');
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
                couponImage2Callback = function (file, data) {
                    campaignModel().couponImage2(data);
                },
                CouponImage3Callback = function (file, data) {
                    campaignModel().CouponImage3(data);
                },
                CouponImage4Callback = function (file, data) {
                    campaignModel().CouponImage4(data);
                },
                  campaignCSVCallback = function (file, data) {

                  },
                onEditCampaign = function (item) {

                    previewScreenNumber(1);
                    isTerminateBtnVisible(false);
                    isNewCampaignVisible(false);
                    isShowArchiveBtn(false);
                    $("#logo_div").css("display", "block");
                    $(".hideInCoupons").css("display", "none");

                    $("#MarketobjDiv").css("display", "none");
                    $("#topArea").css("display", "none");
                    $("#panelArea").css("display", "none");

                    $("#Heading_div").css("display", "none");


                    if (item.Status() == 1 || item.Status() == 2 || item.Status() == 3 || item.Status() == 4 || item.Status() == 6 || item.Status() == null || item.Status() == 7 || item.Status() == 9) {
                        collapseMainMenu();

                        if (item.Status() == 1)//because it is in draft mode.
                            isNewCampaign(true);
                        else
                            isNewCampaign(false);

                        canSubmitForApproval(true);
                        dataservice.getCampaignData({
                            CampaignId: item.CampaignID(),
                            SearchText: ""
                        }, {
                            success: function (data) {

                                if (data != null) {
                                    // set languages drop down
                                    var profileQIds = [];
                                    var surveyQIds = [];
                                    selectedCriteria();
                                    pricePerclick(0);

                                    var clonedVersofCariterias = data.Campaigns[0].AdCampaignTargetCriterias.clone();


                                    _.each(data.Campaigns[0].CouponCodes, function (cc) {

                                        allCouponCodeItems.push(cc.Code);
                                    });

                                    campaignModel(model.Campaign.Create(data.Campaigns[0]));

                                    if (campaignModel().LogoUrl() == '' || campaignModel().LogoUrl() == undefined) {

                                        campaignModel().LogoUrl("/images/standardplaceholder.png");
                                    }


                                    VideoLink2src(campaignModel().VideoLink2() + '' + '');

                                    if (VideoLink2src() != null && VideoLink2src() != '') {
                                        FlagToShowDivs(false);
                                    }
                                    else {
                                        FlagToShowDivs(true);
                                    }

                                    //if (campaignModel().LogoUrl() != null && campaignModel().LogoUrl() != '') {
                                    //    TodisplayImg(false);

                                    //}
                                    //else {
                                    //    TodisplayImg(true);
                                    //}
                                    _.each(clonedVersofCariterias, function (cclist) {
                                        if (cclist.Type == 1) {

                                            if (profileQIds.indexOf(cclist.PQId) == -1) {
                                                profileQIds.push(cclist.PQId);
                                                campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({

                                                    Type: cclist.Type,
                                                    PQId: cclist.PQId,
                                                    PQAnswerId: cclist.PQAnswerId,
                                                    QuizCampaignId: cclist.QuizCampaignId,
                                                    QuizAnswerId: cclist.QuizAnswerId,
                                                    questionString: cclist.questionString,
                                                    answerString: cclist.answerString,
                                                    IncludeorExclude: cclist.IncludeorExclude,
                                                    CampaignId: cclist.CampaignId,
                                                    criteriaPrice: UserAndCostDetail().OtherClausePrice,
                                                    CriteriaId: cclist.CriteriaId
                                                }));
                                            } else {
                                                campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({

                                                    Type: cclist.Type,
                                                    PQId: cclist.PQId,
                                                    PQAnswerId: cclist.PQAnswerId,
                                                    QuizCampaignId: cclist.QuizCampaignId,
                                                    QuizAnswerId: cclist.QuizAnswerId,
                                                    questionString: cclist.questionString,
                                                    answerString: cclist.answerString,
                                                    IncludeorExclude: cclist.IncludeorExclude,
                                                    CampaignId: cclist.CampaignId,
                                                    criteriaPrice: 0,
                                                    CriteriaId: cclist.CriteriaId
                                                }));
                                            }


                                        } else if (cclist.Type == 6) {
                                            if (surveyQIds.indexOf(cclist.QuizCampaignId) == -1) {
                                                surveyQIds.push(cclist.QuizCampaignId);
                                                campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({

                                                    Type: cclist.Type,
                                                    PQId: cclist.PQId,
                                                    PQAnswerId: cclist.PQAnswerId,
                                                    QuizCampaignId: cclist.QuizCampaignId,
                                                    QuizAnswerId: cclist.QuizAnswerId,
                                                    questionString: cclist.questionString,
                                                    answerString: cclist.answerString,
                                                    IncludeorExclude: cclist.IncludeorExclude,
                                                    CampaignId: cclist.CampaignId,
                                                    criteriaPrice: UserAndCostDetail().OtherClausePrice,
                                                    CriteriaId: cclist.CriteriaId
                                                }));
                                            } else {
                                                campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({

                                                    Type: cclist.Type,
                                                    PQId: cclist.PQId,
                                                    PQAnswerId: cclist.PQAnswerId,
                                                    QuizCampaignId: cclist.QuizCampaignId,
                                                    QuizAnswerId: cclist.QuizAnswerId,
                                                    questionString: cclist.questionString,
                                                    answerString: cclist.answerString,
                                                    IncludeorExclude: cclist.IncludeorExclude,
                                                    CampaignId: cclist.CampaignId,
                                                    criteriaPrice: 0,
                                                    CriteriaId: cclist.CriteriaId
                                                }));
                                            }
                                        }

                                    });

                                    DefaultTextBtns.push({ id: campaignModel().BuyItButtonLabel(), name: campaignModel().BuyItButtonLabel() });
                                    campaignModel().reset();


                                    view.initializeTypeahead();

                                    selectedQuestionCountryList([]);
                                    _.each(campaignModel().AdCampaignTargetLocations(), function (item) {

                                        addCountryToCountryList(item.CountryID(), item.Country());
                                    });

                                    if (campaignModel().Type() == "1") {
                                        isEnableVedioVerificationLink(true);
                                        campaignTypePlaceHolderValue('Enter in the YouTube video link (20 characters)');
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
                                        $("#btnSubmitForApproval2").css("display", "none");

                                        $('#imgLogo').prop('disabled', true);

                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,#btnPauseCampaign,.lang_delSurvey").css("display", "none");
                                        $("#saveBtn").css("display", "none")
                                        $("#btnCancel,#btnPauseCampaign,#btnClose").removeAttr('disabled');
                                        campaignModel().StatusValue("Submitted for Approval");
                                    } else if (campaignModel().Status() == 3) {
                                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnStopAndTerminate,#btnPauseCampaign").removeAttr('disabled');
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey").css("display", "none");
                                        //$("#saveBtn").css("display", "none");
                                        //$("#btnPauseCampaign").css("display", "inline-block");
                                        //$("#btnCancel,#btnPauseCampaign,#btnCopyCampaign,#btnStopAndTerminate").removeAttr('disabled');
                                        $("#btnPauseCampaign").css("display", "inline-block");
                                        campaignModel().StatusValue("Live");
                                        //isTerminateBtnVisible(true);
                                        isNewCampaignVisible(true);
                                    } else if (campaignModel().Status() == 4) {
                                        // $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey").css("display", "none");
                                        $("#saveBtn").css("display", "none");
                                        $("#btnResumeCampagin").css("display", "inline-block");
                                        $("#btnCancel,#btnResumeCampagin,#btnCopyCampaign,#btnStopAndTerminate").removeAttr('disabled');
                                        campaignModel().StatusValue("Paused");
                                        // isTerminateBtnVisible(true);
                                        isNewCampaignVisible(true);
                                    } else if (campaignModel().Status() == 5) {
                                        campaignModel().StatusValue("Completed");
                                    } else if (campaignModel().Status() == 6) {
                                        //  $("input,button,textarea,select").attr('disabled', 'disabled'); // disable all controls
                                        $("#btnSubmitForApproval2").css("display", "inline-block");
                                        $("#btnSubmitForApproval2").removeAttr('disabled');
                                        $("#btnPauseCampaign").css("display", "none");
                                        campaignModel().StatusValue("Approval Rejected");
                                    } else if (campaignModel().Status() == 7) {

                                        campaignModel().StatusValue("Remove");
                                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnArchive,#btnPauseCampaign,.lang_delSurvey").css("display", "none");
                                        $("#saveBtn").css("display", "none");
                                        // $("#btnPauseCampaign").css("display", "inline-block");
                                        $("#btnCancel,#btnPauseCampaign,#btnCopyCampaign,#btnArchive").removeAttr('disabled');
                                        isNewCampaignVisible();
                                        isShowArchiveBtn(false);
                                    } else if (item.Status == 9) {
                                        item.StatusValue = ("Completed");
                                    } else if (item.Status == 8) {
                                        item.StatusValue = ("Archived");
                                        $("input,button,textarea,a,select").attr('disabled', 'disabled'); // disable all controls 
                                        $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign,.lang_delSurvey").css("display", "none");
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
                                    $("#MainBtnClose").removeAttr('disabled');
                                    //  buildMap();

                                    if (campaignModel().AdCampaignTargetLocations() != null && campaignModel().AdCampaignTargetLocations().length > 0) {
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

                                        if (item.Type() == 1) { // profile
                                            if (item.criteriaPrice() != null) {
                                                pricePerclick(pricePerclick() + item.criteriaPrice());

                                            }

                                        }
                                        if (item.Type() == 6) { // quiz
                                            if (item.criteriaPrice() != null) {
                                                pricePerclick(pricePerclick() + item.criteriaPrice());

                                            }


                                        }
                                        if (item.Type() == 2) { // survey
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


                                    if (campaignModel().VoucherImagePath() != null || campaignModel().Voucher1Value() != null || campaignModel().Voucher1Description() != null || campaignModel().Voucher1Heading() != null) {
                                        voucherQuestionStatus(true);
                                    }
                                    if (campaignModel().BuuyItLine1() != null || campaignModel().BuyItLine2() != null || campaignModel().BuyItLine3() != null || campaignModel().BuyItButtonLabel() != null) {
                                        buyItQuestionStatus(true);

                                        pricePerclick(pricePerclick() + UserAndCostDetail().BuyItClausePrice);
                                        isBuyItPerClickPriceAdded(true);


                                    }

                                    if ((campaignModel().VerifyQuestion() != null && campaignModel().VerifyQuestion() != '') || (campaignModel().Answer1() != null && campaignModel().Answer1() != '') || (campaignModel().Answer2() != null && campaignModel().Answer2() != '')) {
                                        quizQuestionStatus(true);
                                        pricePerclick(pricePerclick() + UserAndCostDetail().QuizQuestionClausePrice);
                                        isQuizQPerClickPriceAdded(true);
                                    } else {
                                        quizQuestionStatus(false);
                                        isQuizQPerClickPriceAdded(false);
                                    }
                                    //buyItQuestionStatus
                                    // handle 2nd edit error 
                                    //  $(".modal-backdrop").remove();

                                    _.each(campaignModel().CouponCategories(), function (coup) {
                                        _.each(couponCategories(), function (coupcc) {

                                            if (coupcc.CategoryId == coup.CategoryId()) {

                                                coupcc.IsSelected = true;
                                            }
                                        });

                                    });


                                    //BuyItButtonLabel(campaignModel().ShowBuyitBtn());

                                    //var buyitbuttonlabel = couponModel().BuyitBtnLabel();

                                    //if (couponModel().ShowBuyitBtn() == false) {
                                    //    $("#buyItddl").val('0');
                                    //}
                                    //else {
                                    //    if (buyitbuttonlabel == 'Apply Now' ||
                                    //        buyitbuttonlabel == 'Book Now' ||
                                    //        buyitbuttonlabel == 'Contact Us' ||
                                    //        buyitbuttonlabel == 'Download' ||
                                    //        buyitbuttonlabel == 'Learn More' ||
                                    //        buyitbuttonlabel == 'Shop Now' ||
                                    //        buyitbuttonlabel == 'Sign Up' ||
                                    //        buyitbuttonlabel == 'Watch More'
                                    //         ) {
                                    //        buyItQuestionLabelStatus(false);
                                    //        $("#buyItddl").val(buyitbuttonlabel);
                                    //    }
                                    //    else {
                                    //        $("#buyItddl").val('999');
                                    //        buyItQuestionLabelStatus(true);
                                    //        ButItOtherLabel(buyitbuttonlabel);
                                    //    }
                                    //}


                                    if (campaignModel().DeliveryDays() != null) {
                                        if (campaignModel().DeliveryDays() == 3) {
                                            if (UserAndCostDetail().ThreeDayDeliveryClausePrice != null) {
                                                pricePerclick(pricePerclick() + UserAndCostDetail().ThreeDayDeliveryClausePrice);

                                            }
                                        } else if (campaignModel().DeliveryDays() == 5) {
                                            if (UserAndCostDetail().FiveDayDeliveryClausePrice != null) {
                                                pricePerclick(pricePerclick() + UserAndCostDetail().FiveDayDeliveryClausePrice);

                                            }
                                        } else if (campaignModel().DeliveryDays() == 10) {
                                            if (UserAndCostDetail().TenDayDeliveryClausePrice != null) {
                                                pricePerclick(pricePerclick() + UserAndCostDetail().TenDayDeliveryClausePrice);
                                            }
                                        }
                                        alreadyAddedDeliveryValue(campaignModel().DeliveryDays());
                                    } else {
                                        campaignModel().DeliveryDays('10');
                                        alreadyAddedDeliveryValue(10);
                                        pricePerclick(pricePerclick() + UserAndCostDetail().TenDayDeliveryClausePrice);
                                    }

                                    if ((campaignModel().IsShowVoucherSetting() != null && campaignModel().IsShowVoucherSetting() != true)) {
                                        pricePerclick(pricePerclick() + UserAndCostDetail().VoucherClausePrice);
                                        isVoucherPerClickPriceAdded(true);
                                    }

                                    var takenCouponCodes = $.grep(campaignModel().CouponCodes(), function (n, i) {
                                        return (n.IsTaken() == true);
                                    });

                                    UsedCouponQuantity(takenCouponCodes.length);
                                    var arrayOfUpdatedList = couponCategories().clone();
                                    couponCategories.removeAll();
                                    ko.utils.arrayPushAll(couponCategories(), arrayOfUpdatedList);
                                    couponCategories.valueHasMutated();
                                    randonNumber("?r=" + Math.floor(Math.random() * (20 - 1 + 1)) + 1);

                                    if (campaignModel().Type() == 5) {
                                        campaignNamePlaceHolderValue('New Coupon');

                                        lblCampaignName("Voucher Name");
                                        lblDetailsHeading("voucher Display Details");
                                        lblAdTitle("Voucher Title");
                                        lblFirstLine("First line");
                                        lbllSecondLine("Second Line");
                                        lblCampaignSchedule("Schedule");
                                    }

                                    getAudienceCount();
                                    bindAudienceReachCount();

                                    if (mode == 4) {

                                        $("#logo_div").css("display", "block");
                                    }

                                    else {

                                        $("#logo_div").css("display", "none");
                                    }

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
                submitResumeData = function () {
                    if (campaignModel() != undefined)
                        saveCampaign(3);

                    $("#btnSubmitForApproval,#btnResumeCampagin,#btnPauseCampaign").css("display", "none");
                    $("#btnSubmitForApproval,#saveBtn,.lang_delSurvey,.table-link").css("display", "inline-block");
                    $("input,button,textarea,a,select,#btnCancel,#btnPauseCampaign").removeAttr('disabled');
                },
                nextPreviewScreen = function () {

                    var noErrors = true;
                    if (previewScreenNumber() == 1) {

                        if (campaignModel().CampaignName() == "" || campaignModel().CampaignName() == undefined) {
                            noErrors = false;
                            toastr.error("Please enter ad Title.");
                        }
                        //if (campaignModel().Description() == "" || campaignModel().Description() == undefined) {
                        //    noErrors = false;
                        //    toastr.error("Please enter first line.");
                        //}
                        //if (campaignModel().CampaignDescription() == "" || campaignModel().CampaignDescription() == undefined) {
                        //    noErrors = false;
                        //    toastr.error("Please enter second line.");
                        //}

                        if (campaignModel().MaxBudget() < campaignModel().ClickRate()) {
                            noErrors = false;
                            toastr.error("Manage budget should be greater than ppvc.");
                        }

                        //if (campaignModel().MaxBudget() == 0 || campaignModel().MaxBudget()=='') {
                        //    noErrors = false;
                        //    toastr.error("Manage budget is required");
                        //}
                        //if (campaignModel().ClickRate() ==0||campaignModel().ClickRate()=='') {
                        //    noErrors = false;
                        //    toastr.error(" ppvc is required.");
                        //}
                    }

                    if (previewScreenNumber() == 3) {
                        if (campaignModel().VerifyQuestion() == "" || campaignModel().VerifyQuestion() == undefined) {
                            noErrors = false;
                            toastr.error("Please enter verify question.");
                        }
                        if (campaignModel().Answer1() == "" || campaignModel().Answer1() == undefined) {
                            noErrors = false;
                            toastr.error("Please enter answer 1.");
                        }
                        if (campaignModel().Answer2() == "" || campaignModel().Answer2() == undefined) {
                            noErrors = false;
                            toastr.error("Please enter answer 2.");
                        }
                        if (campaignModel().Answer3() == "" || campaignModel().Answer3() == undefined) {
                            noErrors = false;
                            toastr.error("Please enter answer 3.");
                        }
                    }
                    if (noErrors == true) {

                        if (previewScreenNumber() < 5) {
                            previewScreenNumber(previewScreenNumber() + 1);
                            $('html, body').animate({ scrollTop: 0 }, 800);
                        }

                    }

                },

                 backScreen = function () {
                     if (previewScreenNumber() > 1) {
                         previewScreenNumber(previewScreenNumber() - 1);
                     }
                     $('html, body').animate({ scrollTop: 0 }, 800);
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

                    if (alreadyAddedDeliveryValue() == 3) {
                        pricePerclick(pricePerclick() - UserAndCostDetail().ThreeDayDeliveryClausePrice);


                    } else if (alreadyAddedDeliveryValue() == 5) {

                        pricePerclick(pricePerclick() - UserAndCostDetail().FiveDayDeliveryClausePrice);

                    } else if (alreadyAddedDeliveryValue() == 10) {

                        pricePerclick(pricePerclick() - UserAndCostDetail().TenDayDeliveryClausePrice);

                    }

                    alreadyAddedDeliveryValue(campaignModel().DeliveryDays());
                    if (campaignModel().DeliveryDays() == 3) {
                        if (UserAndCostDetail().ThreeDayDeliveryClausePrice != null) {
                            pricePerclick(pricePerclick() + UserAndCostDetail().ThreeDayDeliveryClausePrice);

                        }
                    } else if (campaignModel().DeliveryDays() == 5) {
                        if (UserAndCostDetail().FiveDayDeliveryClausePrice != null) {
                            pricePerclick(pricePerclick() + UserAndCostDetail().FiveDayDeliveryClausePrice);

                        }
                    } else if (campaignModel().DeliveryDays() == 10) {
                        if (UserAndCostDetail().TenDayDeliveryClausePrice != null) {
                            pricePerclick(pricePerclick() + UserAndCostDetail().TenDayDeliveryClausePrice);
                        }
                    }
                    return true;
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
                    $("#spinnerAudience").css("display", "block");
                    dataservice.getAudienceData(campData, {
                        success: function (data) {
                            $("#spinnerAudience").css("display", "none");
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
                        //  buildMap();
                    });
                    campaignModel().AdCampaignTargetCriterias.subscribe(function (value) {
                        getAudienceCount();
                    });
                },
                buildMap = function () {
                    //$(".locMap").css("display", "none");
                    //var initialized = false;
                    //_.each(campaignModel().AdCampaignTargetLocations(), function (item) {
                    //   // $(".locMap").css("display", "inline-block");
                    //    clearRadiuses();
                    //    if (item.CityID() == 0 || item.CityID() == null) {
                    //        addCountryMarker(item.Country());
                    //    } else {
                    //        if (!initialized)
                    //            initializeMap(parseFloat(item.Longitude()), parseFloat(item.Latitude()));
                    //        initialized = true;
                    //        var included = true;
                    //        if (item.IncludeorExclude() == '0') {
                    //            included = false;
                    //        }
                    //        addPointer(parseFloat(item.Longitude()), parseFloat(item.Latitude()), item.City(), parseFloat(item.Radius()), included);
                    //    }
                    //});
                },
                addCountryToCountryList = function (country, name) {
                    if (country != undefined) {
                        ;
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
                 LogoUrlImageCallback = function (file, data) {
                     campaignModel().LogoImageBytes(data);
                     TodisplayImg(false);
                 },

                 VideoUrlCallback = function (file, data) {

                     if (file.size < 88888888) {

                         if (file.type === 'video/mp4') {

                             campaignModel().VideoBytes(data);
                         }
                         else {
                             toastr.error("sorry you can upload Mp4 video only.");
                         }
                     }
                     else {
                         toastr.error("sorry you can upload upto 88Mb video only.");
                     }

                 },
                ShowCouponPromotions = function () {
                    window.location.href = "/Coupons/Coupons";
                    //isDisplayCouponsAds(true);
                    //MainHeading("Coupon Promotions");
                    //SubHeading("Paid coupon promotions are listed for whole one calendar month. Submission fee includes unlimited issuing and redemption at all branches.");
                    //getAdCampaignGridContent();
                },

                ShowAdCampaigns = function () {
                    isDisplayCouponsAds(false);

                    MainHeading("Video Campaigns");
                    SubHeading("Video campaigns can be paused and terminated at any time. Increase your conversions and reduce your spend by using profile filters.");
                    if (mode == 4) {
                        MainHeading("Sponsor an app ‘Brain game’.");
                        SubHeading("Reward audiences 50% of your ‘ad click’Drive people to your web site, ask a reinforcing question and show your deals –All for one ‘ad click’ fee.");
                        IsShownforVideo(false);
                    }
                    else {
                        IsShownforVideo(true);
                    }
                    getAdCampaignGridContent();
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
                    IsprofileQuestion(true);
                    Modelheading('Profile Questions');
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
                showAdditionalSurveyQuestions = function () {
                    isNewCriteria(true);
                    var objProfileCriteria = new model.AdCampaignTargetCriteriasModel();
                    Modelheading('Survey Questions');
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
                showAdditionUserSurveyCriteria = function () {
                    Modelheading('Polls');
                    isNewCriteria(true);

                    var objProfileCriteria = new model.AdCampaignTargetCriteriasModel();

                    objProfileCriteria.Type("1");
                    objProfileCriteria.IncludeorExclude("1");
                    criteriaCount(criteriaCount() + 1);
                    objProfileCriteria.CriteriaID(criteriaCount());
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

                showAdditionQuizCriteria = function () {
                    Modelheading('Your Quiz Questions');
                    IsprofileQuestion(false);
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
                // open Product Category Dialog
                openProductCategoryDialog = function () {
                    $("#productCategoryDialog").modal("show");
                },
                 openVideoDialog = function () {

                     if (campaignModel().Type() == 1) {
                         var videoLink = campaignModel().LandingPageVideoLink();
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

                      if (campaignModel().Type() == 1) {
                          $('#appendVideoTag').empty();
                      }
                  },


                addItemToCouponCodeList = function () {

                    if ((this.BetterListitemToAdd() != "") && (allCouponCodeItems.indexOf(this.BetterListitemToAdd()) < 0)) {
                        if (this.BetterListitemToAdd().indexOf(',') > 0) {

                            _.each(this.BetterListitemToAdd().split(','), function (item) {

                                if (allCouponCodeItems.indexOf(item) < 0) {
                                    allCouponCodeItems.push(item);
                                    campaignModel().CouponCodes.push(new model.AdCampaignCouponCodes.Create({
                                        CodeId: 0,
                                        CampaignId: campaignModel().CampaignID(),
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
                            campaignModel().CouponCodes.push(new model.AdCampaignCouponCodes.Create({
                                CodeId: 0,
                                CampaignId: campaignModel().CampaignID(),
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
                    campaignModel().CouponQuantity(allCouponCodeItems().length);
                },
                removeSelectedCouponCodeItem = function (item) {

                    //console.log(allCouponCodeItems().remove(item.Code()));
                    allCouponCodeItems.removeAll();
                    selectedCouponCodeItems([]); // Clear selection

                    campaignModel().CouponCodes.remove(item);
                    _.each(campaignModel().CouponCodes(), function (cc) {

                        allCouponCodeItems.push(cc.Code);
                    });
                    campaignModel().CouponQuantity(allCouponCodeItems().length);
                },
                addVoucherClickRate = function () {

                    if (campaignModel().IsShowVoucherSetting() == false) {
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
                             campaignModel().CouponCodes.remove(item);
                             campaignModel().CouponCodes.push(new model.AdCampaignCouponCodes.Create({
                                 CodeId: item.CodeId(),
                                 CampaignId: campaignModel().CampaignID(),
                                 Code: item.Code(),
                                 IsTaken: item.IsTaken(),
                                 UserId: item.UserId(),
                                 UserName: item.UserName()
                             }));
                             campaignModel().CouponQuantity(allCouponCodeItems().length);
                             $("#gridupdateCodeError").css("display", "none");
                         } else {
                             $("#gridupdateCodeError").css("display", "block");
                         }

                     }
                     return true;
                 },

                 generateCouponCodes = function () {

                     var gData = {
                         CampaignId: campaignModel().CampaignID(),
                         number: numberOFCouponsToGenerate()
                     };
                     dataservice.generateCouponCodes(gData, {
                         success: function (data) {

                             _.each(data.CouponList, function (item) {
                                 allCouponCodeItems.push(item.Code);
                                 campaignModel().CouponCodes.push(new model.AdCampaignCouponCodes.Create({
                                     CodeId: item.CodeId,
                                     CampaignId: item.CampaignId,
                                     Code: item.Code,
                                     IsTaken: false,
                                     UserId: item.UserId,
                                     UserName: "",
                                     TakenDateTime: null
                                 }));
                             });
                             //var cQty = parseInt(campaignModel().CouponQuantity()) + parseInt(numberOFCouponsToGenerate());
                             campaignModel().CouponQuantity(data.CouponQuantity);
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
                     //  toastr.error("Validation.");

                     previewScreenNumber(number);

                 },
                GetAnswers = function (id) {

                    if (profileAnswerList().length > 0) {
                        var list = ko.utils.arrayFilter(profileAnswerList(), function (prod) {

                            return prod.PqId == id;
                        });
                        return list;
                    }

                }
                // BindStatusDD();

                BindStatusDD = function () {
                    var data = [
                        {
                            "id": "2",
                            "name": "Pending Approval"
                        },
                    {
                        "id": "3",
                        "name": "Live"
                    },
                        {
                            "id": "4",
                            "name": "Paused"
                        },
                            {
                                "id": "5",
                                "name": "Completed"
                            },
                                {
                                    "id": "6",
                                    "name": "Approval Rejected"
                                },
                                    {
                                        "id": "7",
                                        "name": "Remove"
                                    },
                                        {
                                            "id": "8",
                                            "name": "Archived"
                                        },
                                            {
                                                "id": "9",
                                                "name": "Draft"
                                            }


                    ];
                    var Status = $("#ddlStatus");
                    Status.html('');
                    Status.append($('<option/>').attr("value", 0).text('All'));
                    $.each(data, function (i, option) {
                        Status.append($('<option/>').attr("value", option.id).text(option.name));
                    });
                    Status[0].selectedIndex = 0;
                },
                // Initialize the view model
                initialize = function (specifiedView) {
                    if (mode == 4) {
                        MainHeading("Sponsor an app ‘brain game’.");
                        SubHeading("Reward audiences 50% of your ‘ad click’Drive people to your web site, ask a reinforcing question and show your deals –All for one ‘ad click’ fee.");
                        IsShownforVideo(false);
                        lblAdTitle("Game  Title");
                        uploadTitle("Upload");
                        tab1Heading("Leaderboard banners appear when app users play brain training games.");
                        tab2Heading("Define the target audience to deliver game ad.");
                        tab4SubHeading("Select your game campaign delivery mode:");
                        UrlHeadings("Leatherboard banner click thru url to your landing  page.");
                    }
                    else {
                        UrlHeadings("Direct viewers to a landing page at the end of your video ad.");
                        IsShownforVideo(true);
                    }
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    for (var i = 10; i < 81; i++) {
                        var text = i.toString();
                        if (i == 110)
                            text += "+";
                        ageRange.push({ value: i.toString(), text: text });
                    }
                    BindStatusDD();
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
                    campaignModel: campaignModel,

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
                    CouponImage4Callback: CouponImage4Callback,
                    CouponImage3Callback: CouponImage3Callback,
                    couponImage2Callback: couponImage2Callback,
                    correctAnswers: correctAnswers,
                    onEditCampaign: onEditCampaign,
                    canSubmitForApproval: canSubmitForApproval,
                    isTerminateBtnVisible: isTerminateBtnVisible,
                    isNewCampaignVisible: isNewCampaignVisible,
                    isShowArchiveBtn: isShowArchiveBtn,
                    submitCampaignData: submitCampaignData,
                    removeAdd: removeAdd,
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
                    errorListNew: errorListNew,
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
                    
                    buyItImageCallback: buyItImageCallback,
                    openEditScreen: openEditScreen,
                    isWelcomeScreenVisible: isWelcomeScreenVisible,
                    isDetailEditorVisible: isDetailEditorVisible,
                    isListVisible: isListVisible,
                    isNewCampaign: isNewCampaign,
                    BackToAds: BackToAds,
                    MainHeading: MainHeading,
                    uploadTitle: uploadTitle,
                    SubHeading: SubHeading,
                    ShowAdCampaigns: ShowAdCampaigns,
                    ShowCouponPromotions: ShowCouponPromotions,
                    isDisplayCouponsAds: isDisplayCouponsAds,
                    lblCampaignName: lblCampaignName,
                    lblDetailsHeading: lblDetailsHeading,
                    lblAdTitle: lblAdTitle,
                    lblFirstLine: lblFirstLine,
                    lbllSecondLine: lbllSecondLine,
                    lblCampaignSchedule: lblCampaignSchedule,
                    tab1Heading: tab1Heading,
                    tab2Heading: tab2Heading,
                    tab4SubHeading: tab4SubHeading,

                    gotoProfile: gotoProfile,
                    gotoManageUsers: gotoManageUsers,
                    ArchiveCampaign: ArchiveCampaign,
                    copyCampaign: copyCampaign,
                    AditionalCriteriaMode: AditionalCriteriaMode,
                    showCompanyProfileQuestions: showCompanyProfileQuestions,
                    showAdditionCriteria: showAdditionCriteria,
                    showAdditionUserCriteria: showAdditionUserCriteria,
                    showAdditionalSurveyQuestions: showAdditionalSurveyQuestions,
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
                    DefaultTextBtns: DefaultTextBtns,
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
                    vouchers: vouchers,
                    voucherPriceLbl: voucherPriceLbl,
                    numberOFCouponsToGenerate: numberOFCouponsToGenerate,
                    showCouponGenerationWarning: showCouponGenerationWarning,
                    previewScreenNumber: previewScreenNumber,
                    nextPreviewScreen: nextPreviewScreen,
                    gotoScreen: gotoScreen,
                    backScreen: backScreen,
                    CurrPage: CurrPage,
                    MaxPage: MaxPage,
                    SearchSelectedStatus: SearchSelectedStatus,
                    SaveDraftCampaign: SaveDraftCampaign,
                    VideoUrlCallback: VideoUrlCallback,
                    VideoLink2src: VideoLink2src,
                    FlagToShowDivs: FlagToShowDivs,
                    TodisplayImg: TodisplayImg,
                    showAdditionUserSurveyCriteria: showAdditionUserSurveyCriteria,
                    surveyquestionList: surveyquestionList,
                    surveyAnswerList: surveyAnswerList,
                    saveSurveyQuestion: saveSurveyQuestion,
                    updateSurveyCriteriass: updateSurveyCriteriass,
                    ShowAudienceCounter: ShowAudienceCounter,
                    IsShownforVideo: IsShownforVideo,
                    UrlHeadings: UrlHeadings,
                    GetAnswers: GetAnswers,
                    SearchProfileQuestion: SearchProfileQuestion,
                    getQuestionByFilter: getQuestionByFilter,
                    submitResumeData: submitResumeData,
                    IsprofileQuestion: IsprofileQuestion,
                    Modelheading: Modelheading,
                    GetAudienceCount: GetAudienceCount,
                    isAdvertdashboardVisible: isAdvertdashboardVisible,
                    handleBuyIt: handleBuyIt,
                    BuyItStatus: BuyItStatus,
                    buyItQuestionStatus: buyItQuestionStatus,
                    ButItOtherLabel: ButItOtherLabel
                };
            })()
        };
        return ist.Ads.viewModel;
    });
