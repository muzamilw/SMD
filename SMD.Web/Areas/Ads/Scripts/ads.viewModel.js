﻿/*
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
                    correctAnswers = ko.observableArray([{ id: 1, name: "Answer 1" }, { id: 1, name: "Answer 2" }, { id: 3, name: "Answer 3" }]),
                    getAdCampaignGridContent = function () {
                        dataservice.getCampaignData({
                            CampaignId: 0,
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SearchText:searchFilterValue()
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    // set languages drop down
                                    langs.removeAll();
                                    ko.utils.arrayPushAll(langs(), data.LanguageDropdowns);
                                    langs.valueHasMutated();
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
                    },
                 
                    getCampaignByFilter = function () {
                        getAdCampaignGridContent();
                    },
                     // Add new Profile Question
                    addNewCampaign = function () {
                        isEditorVisible(true);
                        campaignModel(new model.Campaign());
                      
                        selectedCriteria();
                       
                        campaignModel().Gender('2');
                        campaignModel().Type('2');
                        campaignModel().reset();
                        view.initializeTypeahead();
                        isEnableVedioVerificationLink(false);
                        isEditCampaign(false);
                        campaignModel().CampaignTypeImagePath("");
                        campaignModel().CampaignImagePath("");
                       
                    },

                    closeNewCampaignDialog = function () {
                        isEditorVisible(false);
                    },

                    saveCampaignData = function () {
                         
                        console.log($("#ddpCorrectAns").val());
                          var campignServerObj = campaignModel().convertToServerData();
                          console.log(campignServerObj);

                          dataservice.addCampaignData(campignServerObj, {
                              success: function (data) {
                                
                                      criteriaCount(0);
                                      isEditorVisible(false);
                                      getAdCampaignGridContent();
                                      toastr.success("Successfully saved.");
                              },
                              error: function (response) {

                              }
                          });

                    },

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
                        } else if (selectedCriteria().Type() == "2") {
                            console.log(selectedCriteria().SQAnswer());
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
                                            console.log(selectedCriteria());
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
                    },

                    onRemoveLanguage = function (item) {
                        // Ask for confirmation
                            deleteLanguage(item);
                        
                       
                    },

                    deleteLanguage = function (item) {
                       
                        campaignModel().AdCampaignTargetCriterias.remove(item);
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
                        
                        dataservice.getCampaignData({
                            CampaignId: item.CampaignID(),
                            SearchText: ""
                        }, {
                            success: function (data) {
                              
                                if (data != null) {
                                    // set languages drop down
                                    langs.removeAll();
                                    ko.utils.arrayPushAll(langs(), data.LanguageDropdowns);
                                    langs.valueHasMutated();
                                    console.log(data.Campaigns);

                                    campaignModel(model.Campaign.Create(data.Campaigns[0]));

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
                                    isEditorVisible(true);
                                    
                                }

                            },
                            error: function (response) {

                            }
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
                        onEditCampaign: onEditCampaign
                    };
            })()
        };
        return ist.Ads.viewModel;
    });
