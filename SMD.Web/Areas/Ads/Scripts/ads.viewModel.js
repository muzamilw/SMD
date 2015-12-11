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
                    advertGridContent = ko.observableArray([]),
                    pager = ko.observable(),
                       // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                    langs = ko.observableArray([]),
                    countoryidList = [],
                    cityidList = [],
                    langidList = [],
                    campaignModel = ko.observable(new model.campaignModel()),
                    selectedCriteria = ko.observable(new model.CriteriaModel()),
                    selectedCriteriaList = ko.observableArray([]),
                    profileQuestionList = ko.observable([]),
                    profileAnswerList = ko.observable([]),
                    criteriaCount = ko.observable(0);
                    getAdCampaignGridContent = function () {
                        dataservice.getCampaignData({}, {
                            success: function (data) {
                                if (data != null) {
                                    advertGridContent.removeAll();
                                    ko.utils.arrayPushAll(advertGridContent(), data);
                                    advertGridContent.valueHasMutated();
                                  
                                }
                                
                            },
                            error: function (response) {

                            }
                        });

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
                               
                              },
                              error: function (response) {

                              }
                          });

                    },
                    // Add new Criteria
                    addNewProfileCriteria = function () {
                        
                        var objCT = new model.CriteriaModel();
                        objCT.Type("1");
                        objCT.IncludeorExclude("1");
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
                    saveCriteria = function () {
                        var selectedQuestionstring = $("#ddprofileQuestion option[value=" + $("#ddprofileQuestion").val() + "]").text();
                        selectedCriteria().questionString(selectedQuestionstring);
                        var selectedQuestionAnswerstring = $("#ddprofileAnswers option[value=" + $("#ddprofileAnswers").val() + "]").text();
                        selectedCriteria().answerString(selectedQuestionAnswerstring);
                        selectedCriteria().PQAnswerID($("#ddprofileAnswers").val());
                        var criteriaServerObj = selectedCriteria().convertToServerData();
                        console.log(criteriaServerObj);
                        selectedCriteriaList.push(criteriaServerObj);
                        console.log(selectedCriteriaList());
                    },
                    onEditCriteria = function (item) {
                        console.log(item.PQAnswerID);
                        selectedCriteria(item);
                        //selectedCriteria().;
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
                        console.log(selectedCriteria().PQAnswerID);
                    },
                    onDeleteCriteria = function () {
                       
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
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView; 
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, advertGridContent, getAdCampaignGridContent));
                        getBaseData();
                        getAdCampaignGridContent();
                        
                    };
                    return {
                        initialize: initialize,
                        pager: pager,
                        isEditorVisible:isEditorVisible,
                        advertGridContent: advertGridContent,
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
                        onChangeProfileQuestion: onChangeProfileQuestion
                    };
            })()
        };
        return ist.Ads.viewModel;
    });
