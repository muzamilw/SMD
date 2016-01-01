/*
    Module with the view model for the Profile Questions
*/
define("ads/ads.viewModel",
    ["jquery", "amplify", "ko", "ads/ads.dataservice", "ads/ads.model", "common/pagination", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
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
                    surveyQuestionList = ko.observableArray([]),
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
                    campaignTypePlaceHolderValue = ko.observable('Enter a link'),
                    isEditCampaign = ko.observable(false),
                    canSubmitForApproval = ko.observable(true),
                    correctAnswers = ko.observableArray([{ id: 1, name: "Answer 1" }, { id: 1, name: "Answer 2" }, { id: 3, name: "Answer 3" }]),
                    selectedIndustryIncludeExclude = ko.observable(true),
                    UserAndCostDetail = ko.observable(),
                    pricePerclick = ko.observable(0),
                    isLocationPerClickPriceAdded = ko.observable(false),
                    isLanguagePerClickPriceAdded = ko.observable(false),
                    isIndustoryPerClickPriceAdded = ko.observable(false),
                    isProfileSurveyPerClickPriceAdded = ko.observable(false),
                    isEducationPerClickPriceAdded = ko.observable(false),
                    selectedEducationIncludeExclude = ko.observable(true),
                     //audience reach
                    reachedAudience = ko.observable(0),
                    //total audience
                    totalAudience = ko.observable(0),
                    // audience reach mode 
                    audienceReachMode = ko.observable(1),
                    getCampaignBaseContent = function () {
                            dataservice.getBaseData({
                                RequestId: 1,
                                QuestionId: 0,
                            }, {
                                success: function (data) {
                                   
                                    if (data != null) {
                                        langs.removeAll();
                                        ko.utils.arrayPushAll(langs(), data.Languages);
                                        langs.valueHasMutated();
                                        UserAndCostDetail(data.UserAndCostDetails);
                                        if (UserAndCostDetail().GenderClausePrice != null) {
                                            pricePerclick(pricePerclick() + UserAndCostDetail().GenderClausePrice);
                                        }
                                        if (UserAndCostDetail().AgeClausePrice != null) {
                                            pricePerclick(pricePerclick() + UserAndCostDetail().AgeClausePrice);
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
                            SearchText:searchFilterValue()
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
                    },
                 
                    getCampaignByFilter = function () {
                        getAdCampaignGridContent();
                    },
                     // Add new Profile Question
                    addNewCampaign = function () {
                        isEditorVisible(true);
                        canSubmitForApproval(true);
                        campaignModel(new model.Campaign());
                      
                        selectedCriteria();
                       
                        campaignModel().Gender('2');
                        campaignModel().Type('2');
                        campaignModel().MaxBudget('0');
                        
                        campaignModel().reset();
                        view.initializeTypeahead();
                        isEnableVedioVerificationLink(false);
                        isEditCampaign(false);
                        campaignModel().CampaignTypeImagePath("");
                        campaignModel().CampaignImagePath("");
                        campaignModel().LanguageId(41);
                        bindAudienceReachCount();
                    },

                    closeNewCampaignDialog = function () {
                        isEditorVisible(false);
                    },

                    saveCampaignData = function () {
                         
                        saveCampaign(1);

                    },
                    submitCampaignData = function () {
                        saveCampaign(2);
                    }
                    saveCampaign = function (mode) {
                        campaignModel().Status(mode);
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
                                toastr.success("Successfully saved.");
                            },
                            error: function (response) {

                            }
                        });
                    }
                    // Add new profile Criteria
                    addNewProfileCriteria = function () {
                        isNewCriteria(true);
                        var objProfileCriteria = new model.AdCampaignTargetCriteriasModel();
                      
                        objProfileCriteria.Type("1");
                        objProfileCriteria.IncludeorExclude("1");
                        criteriaCount(criteriaCount() + 1);
                        objProfileCriteria.CriteriaID(criteriaCount());
                        selectedCriteria(objProfileCriteria);

                      
                        if (profileQuestionList().length == 0)
                        {
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
                    
                    },

                    // Add new survey Criteria
                    addNewSurveyCriteria = function () {
                        isNewCriteria(true);
                        var objSurveyCriteria = new model.AdCampaignTargetCriteriasModel();
                        objSurveyCriteria.Type("2");
                        objSurveyCriteria.IncludeorExclude("1");
                        criteriaCount(criteriaCount() + 1);
                        objSurveyCriteria.CriteriaID(criteriaCount());
                        selectedCriteria(objSurveyCriteria);
                      
                        if (surveyQuestionList().length == 0) {
                            dataservice.getBaseData({
                                RequestId: 4,
                                QuestionId: 0,
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
                        if (selectedCriteria().Type() == "1")
                        {
                            var selectedQuestionstring = $("#ddprofileQuestion option[value=" + $("#ddprofileQuestion").val() + "]").text();
                            selectedCriteria().questionString(selectedQuestionstring);
                            var selectedQuestionAnswerstring = $("#ddprofileAnswers option[value=" + $("#ddprofileAnswers").val() + "]").text();
                            selectedCriteria().answerString(selectedQuestionAnswerstring);
                            selectedCriteria().PQAnswerID($("#ddprofileAnswers").val());

                            var matchedProfileCriteria = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (arrayitem) {
                                
                                return arrayitem.PQID() == $("#ddprofileQuestion").val();
                            });
                           
                            if (matchedProfileCriteria == null) {
                                if (UserAndCostDetail().OtherClausePrice != null) {
                                    pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);
                                    isProfileSurveyPerClickPriceAdded(true);
                                }
                            }
                        } else if (selectedCriteria().Type() == "2") {
                          
                            var selectedQuestionstring = $("#ddsurveyQuestion option[value=" + $("#ddsurveyQuestion").val() + "]").text();
                            selectedCriteria().questionString(selectedQuestionstring);
                            var matchSurveyQuestion = ko.utils.arrayFirst(surveyQuestionList(), function (item) {
                                return item.SQID == $("#ddsurveyQuestion").val();
                            });
                            if (selectedCriteria().SQAnswer() == "1") {
                                selectedCriteria().answerString(matchSurveyQuestion.LeftPicturePath);
                            } else {
                                selectedCriteria().answerString(matchSurveyQuestion.RightPicturePath);
                            }
                          
                            var matchedSurveyCriteria = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (arrayitem) {

                                return arrayitem.SQID() == $("#ddsurveyQuestion").val();
                            });
                            if (matchedSurveyCriteria == null) {
                                if (UserAndCostDetail().OtherClausePrice != null) {
                                    pricePerclick(pricePerclick() + UserAndCostDetail().OtherClausePrice);
                                    isProfileSurveyPerClickPriceAdded(true);
                                }
                            }
                        }
                      
                        if (isNewCriteria()) {
                            campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                                Type: selectedCriteria().Type(),
                                PQId: selectedCriteria().PQID(),
                                PQAnswerId: selectedCriteria().PQAnswerID(),
                                SQId: selectedCriteria().SQID(),
                                SQAnswer: selectedCriteria().SQAnswer(),
                                questionString: selectedCriteria().questionString(),
                                answerString: selectedCriteria().answerString(),
                                IncludeorExclude: selectedCriteria().IncludeorExclude()
                            }));
                        }
                       
                        isShowSurveyAns(false);
                    },

                    onEditCriteria = function (item) {
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
                            var selectedSurveyQuestionId = item.SQID() + 0;
                            var selectedSurveyAnsId = item.SQAnswer() + 0;
                            var SurQuestionList = [];
                            if (surveyQuestionList().length == 0) {
                                dataservice.getBaseData({
                                    RequestId: 4,
                                    QuestionId: 0,
                                }, {
                                    success: function (data) {
                                        if (data != null) {
                                           
                                            surveyQuestionList.removeAll();
                                            ko.utils.arrayPushAll(surveyQuestionList(), data.SurveyQuestions);
                                            surveyQuestionList.valueHasMutated();
                                            item.SQID(selectedSurveyQuestionId);
                                            item.SQAnswer(selectedSurveyAnsId + "");
                                            selectedCriteria(item);
                                            
                                            var matchSurveyQuestion = ko.utils.arrayFirst(surveyQuestionList(), function (item) {
                                                return item.SQID == selectedSurveyQuestionId;
                                            });
                                            selectedCriteria().surveyQuestLeftImageSrc(matchSurveyQuestion.LeftPicturePath);
                                            selectedCriteria().surveyQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);
                                            isShowSurveyAns(true);
                                         
                                        }

                                    },
                                    error: function (response) {

                                    }
                                });
                            }
                           
                        }
                       
                    },
                      // Delete Handler PQ
                    onDeleteCriteria = function (item) {
                        
                        campaignModel().AdCampaignTargetCriterias.remove(item);
                       
                       if (item.Type() == "1")
                       {
                           var matchedProfileCriteria = ko.utils.arrayFirst(campaignModel().AdCampaignTargetCriterias(), function (arrayitem) {
                             
                               return arrayitem.PQID() == item.PQID()
                           });

                           if (matchedProfileCriteria == null) {
                               if (UserAndCostDetail().OtherClausePrice != null) {
                                   pricePerclick(pricePerclick() - UserAndCostDetail().OtherClausePrice);
                                  
                               }
                           }
                       } else if (item.Type() == "2")
                       {
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

                    onChangeProfileQuestion = function () {
                        var selectedQuestionId = $("#ddprofileQuestion").val();
                        dataservice.getBaseData({
                            RequestId: 3,
                            QuestionId: selectedQuestionId,
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    if (profileAnswerList().length > 0)
                                    {
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
                        item.SQAnswer("1");
                        item.surveyQuestLeftImageSrc(matchSurveyQuestion.LeftPicturePath);
                        item.surveyQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);
                        $("#surveyAnswersContainer").show();
                        isShowSurveyAns(true);
                    },

                    onRemoveLocation = function (item) {
                         // Ask for confirmation
                             deleteLocation(item);
                       
                    },

                    deleteLocation = function (item) {
                        campaignModel().AdCampaignTargetLocations.remove(item);
                      
                        if (campaignModel().AdCampaignTargetLocations() == null || campaignModel().AdCampaignTargetLocations().length == 0) {
                            isLocationPerClickPriceAdded(false);
                            pricePerclick(pricePerclick() - UserAndCostDetail().LocationClausePrice);
                        }
                        toastr.success("Removed Successfully!");
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
                            Id: 0,
                            CampaignId: campaignModel().CampaignID()
                        }));
                        $(".locVisibility,.locMap").css("display", "none");
                        resetLocations();

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
                             campaignTypePlaceHolderValue('Enter a video embed code');
                         } else {
                             isEnableVedioVerificationLink(false);
                             if (campaignModel().Type() == "2") {
                                 campaignTypePlaceHolderValue('Enter a link');
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
                        if (item.Status() == 1 || item.Status() == null) {
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
                                        if (campaignModel().Type() == "1") {
                                            isEnableVedioVerificationLink(true);
                                            campaignTypePlaceHolderValue('Enter a video embed code');
                                        } else {
                                            isEnableVedioVerificationLink(false);
                                            if (campaignModel().Type() == "2") {
                                                campaignTypePlaceHolderValue('Enter a link');
                                            }
                                        }

                                        if (campaignModel().Status() == 1) {
                                            campaignModel().StatusValue("Draft");
                                        } else if (campaignModel().Status() == 2) {
                                            campaignModel().StatusValue("Submitted for Approval");
                                        } else if (campaignModel().Status() == 3) {
                                            campaignModel().StatusValue("Live");
                                        } else if (campaignModel().Status() == 4) {
                                            campaignModel().StatusValue("Paused");
                                        } else if (campaignModel().Status() == 5) {
                                            campaignModel().StatusValue("Completed");
                                        } else if (campaignModel().Status() == 6) {
                                            campaignModel().StatusValue("Approval Rejected");
                                        }
                                        isEditCampaign(true);
                                        isEditorVisible(true);
                                       
                                        var profileQIds = [];
                                        var surveyQIds = [];
                                        if (campaignModel().AdCampaignTargetLocations() != null || campaignModel().AdCampaignTargetLocations().length > 0) {
                                            if (UserAndCostDetail().LocationClausePrice != null && isLocationPerClickPriceAdded() == false) {
                                                pricePerclick(pricePerclick() + UserAndCostDetail().LocationClausePrice);
                                                isLocationPerClickPriceAdded(true);
                                            }
                                        }else{
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
                                                    console.log(profileQIds.indexOf(item.PQID()));
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
                                        bindAudienceReachCount();
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
                    addIndustry = function (selected) {
                       
                        campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                            Industry: selected.IndustryName,
                            IndustryId: selected.IndustryId,
                            IncludeorExclude: parseInt(selected.IndustryIncludeExclude),
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
                             console.log(item);
                             if (item.CityID() == 0 || item.CityID() == null) {
                                 if (item.IncludeorExclude() == '0') {
                                     if (countryIdsExcluded == '') {
                                         countryIdsExcluded += item.CountryId();
                                     } else {
                                         countryIdsExcluded += ',' + item.CountryId();
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
                         });
                         campaignModel().AdCampaignTargetCriterias.subscribe(function (value) {
                             getAudienceCount();
                         });
                     },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView; 
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        for (var i = 13; i < 65; i++) {
                            ageRange.push({ value: i.toString(), text: i.toString() });
                        }
                        pager(pagination.Pagination({ PageSize: 10 }, campaignGridContent, getAdCampaignGridContent));
                        getAdCampaignGridContent();
                        getCampaignBaseContent();
                    };
                    
                    return {
                        initialize: initialize,
                        pager: pager,
                        hasChangesOnQuestion:hasChangesOnQuestion,
                        isEditorVisible:isEditorVisible,
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
                        surveyQuestionList: surveyQuestionList,
                        addNewSurveyCriteria: addNewSurveyCriteria,
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
                        submitCampaignData: submitCampaignData,
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
                        bindAudienceReachCount: bindAudienceReachCount
                    };
            })()
        };
        return ist.Ads.viewModel;
    });
