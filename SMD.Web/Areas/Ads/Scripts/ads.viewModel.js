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
                    isEditorVisible = ko.observable(false),
                    langs = ko.observableArray([]),
                    countoryidList = [],
                    cityidList = [],
                    langidList = [],
                    campaignModel = ko.observable(),
                    selectedCriteria = ko.observable(),
                    selectedCriteriaList = ko.observableArray([]),
                    profileQuestionList = ko.observable([]),
                    surveyQuestionList = ko.observableArray([]),
                    profileAnswerList = ko.observable([]),
                    criteriaCount = ko.observable(0);
                    getAdCampaignGridContent = function () {
                        dataservice.getCampaignData({
                            FirstLoad: true,
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage()
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    console.log("campaigngrid data");
                                    console.log(data);
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
                // populate country, language and status fields 
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
                    getBaseData = function () {
                        dataservice.getBaseData({
                            RequestId: 1,
                            QuestionId: 0,
                        }, {
                              success: function (data) {
                                  if (data != null) {
                                      langs.removeAll();
                                      ko.utils.arrayPushAll(langs(), data.Languages);
                                      langs.valueHasMutated();
                                  }

                              },
                              error: function (response) {

                              }
                        });

                      },
                     // Add new Profile Question
                    addNewCampaign = function () {
                        isEditorVisible(true);
                        campaignModel(new model.campaignModel());
                        selectedCriteria(new model.CriteriaModel());
                       
                        campaignModel().Gender('2');
                        campaignModel().Type('2');
                    },
                    closeNewCampaignDialog = function () {
                        isEditorVisible(false);
                    },
                    saveCampaignData = function () {
                          
                          for (var i = 0; i < $('div.count_city_newcnt').length; i++)
                          {
                              var idOfEle = $('div.count_city_newcnt')[i].id;
                            
                              var res_array = idOfEle.split("_");
                              if (res_array[1] == "City")
                              {
                                  cityidList.push(res_array[0]);
                              } else if (res_array[1] == "Country") {
                                  countoryidList.push(res_array[0]);
                              }
                          }

                          for (var i = 0; i < $('div.lang_newcnt').length; i++) {
                              langidList.push($('div.lang_newcnt')[i].id);
                          }
                         
                          var campignServerObj = campaignModel().convertToServerData();
                          campignServerObj.Countries = countoryidList;
                          campignServerObj.Cities = cityidList;
                          campignServerObj.Languages = langidList;
                          campignServerObj.AdCampaignTargetCriterias = selectedCriteriaList();
                         
                          dataservice.addCampaignData(campignServerObj
                              , {
                                  success: function (data) {
                                      profileQuestionList([]);
                                      profileAnswerList([]);
                                      surveyQuestionList([]);
                                      criteriaCount(0);
                                      selectedCriteriaList.removeAll();
                                      campaignModel(new model.campaignModel());
                                      selectedCriteria(new model.CriteriaModel());
                                      toastr.success("Successfully saved.");
                              },
                              error: function (response) {

                              }
                          });

                    },
                    // Add new profile Criteria
                    addNewProfileCriteria = function () {
                        
                        var objCT = new model.CriteriaModel();
                        objCT.Type("1");
                        objCT.IncludeorExclude("1");
                        criteriaCount(criteriaCount() + 1);
                        objCT.CriteriaID(criteriaCount());
                        selectedCriteria(objCT);
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

                        var objCT = new model.CriteriaModel();
                        objCT.Type("2");
                        objCT.IncludeorExclude("1");
                        criteriaCount(criteriaCount() + 1);
                        objCT.CriteriaID(criteriaCount());
                        selectedCriteria(objCT);
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
                        
                        var criteriaServerObj = selectedCriteria().convertToServerData();
                     
                        selectedCriteriaList.push(criteriaServerObj);
                        console.log(selectedCriteriaList());
                    },
                    onEditCriteria = function (item) {
                        dataservice.getBaseData({
                            RequestId: 3,
                            QuestionId: item.PQID,
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    if (profileAnswerList().length > 0) {
                                        profileAnswerList([]);
                                    }
                                    ko.utils.arrayPushAll(profileAnswerList(), data.ProfileQuestionAnswers);
                                    profileAnswerList.valueHasMutated();
                                    $("#profileAnswersContainer").show();
                                }

                            },
                            error: function (response) {

                            }
                        });
                        selectedCriteria(item);
                       
                    },
                      // Delete Handler PQ
                    onDeleteCriteria = function (item) {
                        selectedCriteriaList.remove(item);
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
                                    $("#profileAnswersContainer").show();
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
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView; 
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, campaignGridContent, getAdCampaignGridContent));
                        getBaseData();
                        getAdCampaignGridContent();
                        
                    };
                    return {
                        initialize: initialize,
                        pager: pager,
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
                        selectedCriteriaList: selectedCriteriaList,
                        saveCriteria: saveCriteria,
                        onDeleteCriteria: onDeleteCriteria,
                        onEditCriteria: onEditCriteria,
                        addNewProfileCriteria: addNewProfileCriteria,
                        onChangeProfileQuestion: onChangeProfileQuestion,
                        surveyQuestionList: surveyQuestionList,
                        addNewSurveyCriteria: addNewSurveyCriteria,
                        onChangeSurveyQuestion: onChangeSurveyQuestion
                    };
            })()
        };
        return ist.Ads.viewModel;
    });
