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
                    getAdCampaignGridContent = function () {
                        dataservice.getCampaignData({
                            FirstLoad: true,
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
                                        console.log(item);
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
                        selectedCriteria(new model.AdCampaignTargetCriteriasModel());
                       
                        campaignModel().Gender('2');
                        campaignModel().Type('2');
                        campaignModel().reset();
                        view.initializeTypeahead();
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
                          campignServerObj.Criterias = campignServerObj.AdCampaignTargetCriterias;
                         
                          dataservice.addCampaignData(campignServerObj, {
                              success: function (data) {
                            
                                      //profileQuestionList([]);
                                      //profileAnswerList.removeAll();
                                      //surveyQuestionList.removeAll();
                                      criteriaCount(0);
                                      
                                      //campaignModel(new model.campaignModel());
                                      //selectedCriteria(new model.CriteriaModel());
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
                                PQID: selectedCriteria().PQID(),
                                PQAnswerID: selectedCriteria().PQAnswerID(),
                                SQID: selectedCriteria().SQID(),
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
                        if (item.Type() == "1") {
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
                            var selectedSurveyQuestionId = $("#ddsurveyQuestion").val();
                            var matchSurveyQuestion = ko.utils.arrayFirst(surveyQuestionList(), function (item) {
                                return item.SQID == selectedSurveyQuestionId;
                            });
                            selectedCriteria().surveyQuestLeftImageSrc(matchSurveyQuestion.LeftPicturePath);
                            selectedCriteria().surveyQuestRightImageSrc(matchSurveyQuestion.RightPicturePath);
                            isShowSurveyAns(true);
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
                         confirmation.afterProceed(function () {
                             deleteLocation(item);
                         });
                         confirmation.show();
                     },
                    deleteLocation = function (item) {
                        console.log(item.ID());
                        if (item.ID() != 0) {
                            //dataservice.deleteProfileQuestion(item.convertToServerData(), {
                            //    success: function () {
                            //        var newObjtodelete = questions.find(function (temp) {
                            //            return temp.qId() == temp.qId();
                            //        });
                            //        questions.remove(newObjtodelete);
                            //        toastr.success("You are Good!");
                            //    },
                            //    error: function () {
                            //        toastr.error("Failed to delete!");
                            //    }
                            //});
                        } else {
                            campaignModel().AdCampaignTargetLocation.remove(item);
                            toastr.success("Removed Successfully!");
                        }

                    },
                    //add location
                    onAddLocation = function (item) {

                        selectedLocation().Radius = (selectedLocationRadius);
                        selectedLocation().IncludeorExclude = (selectedLocationIncludeExclude);
                        campaignModel().AdCampaignTargetLocation.push(new model.AdCampaignTargetLocation.Create({
                            CountryID: selectedLocation().CountryID,
                            CityID: selectedLocation().CityID,
                            Radius: selectedLocation().Radius(),
                            Country: selectedLocation().Country,
                            City: selectedLocation().City,
                            IncludeorExclude: selectedLocation().IncludeorExclude(),
                            ID: 0,
                            CampaignID: campaignModel().CampaignID()
                        }));
                        $(".locVisibility,.locMap").css("display", "none");
                        resetLocations();
                    },
                    resetLocations = function () {
                        $("#searchCampaignLocations").val("");
                        selectedLocationRadius("");
                    },
                    addLanguage = function (selected) {
                        console.log(campaignModel());
                        campaignModel().AdCampaignTargetCriterias.push(new model.AdCampaignTargetCriteriasModel.Create({
                            Language: selected.LanguageName,
                            LanguageID: selected.LanguageId,
                            IncludeorExclude: parseInt(selectedLangIncludeExclude()),
                            Type: 3,
                            CriteriaID: 0,
                            CampaignID: campaignModel().CampaignID()
                        }));
                        $("#searchLanguages").val("");
                    },
                    onRemoveLanguage = function (item) {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            deleteLanguage(item);
                        });
                        confirmation.show();
                    },
                    deleteLanguage = function (item) {
                        console.log(item.ID());
                        if (item.ID() != 0) {
                            //dataservice.deleteProfileQuestion(item.convertToServerData(), {
                            //    success: function () {
                            //        var newObjtodelete = questions.find(function (temp) {
                            //            return temp.qId() == temp.qId();
                            //        });
                            //        questions.remove(newObjtodelete);
                            //        toastr.success("You are Good!");
                            //    },
                            //    error: function () {
                            //        toastr.error("Failed to delete!");
                            //    }
                            //});
                        } else {
                            campaignModel().AdCampaignTargetCriterias.remove(item);
                            toastr.success("Removed Successfully!");
                        }

                    },
                       // Has Changes
                    hasChangesOnQuestion = ko.computed(function () {
                        if (campaignModel() == undefined) {
                            return false;
                        }
                        return (campaignModel().hasChanges());
                    }),
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
                        isNewCriteria: isNewCriteria
                    };
            })()
        };
        return ist.Ads.viewModel;
    });
