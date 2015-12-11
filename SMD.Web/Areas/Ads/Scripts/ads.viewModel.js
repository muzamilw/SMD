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
                        dataservice.getBaseData({}, {
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
                          dataservice.addCampaignData(campignServerObj
                              , {
                              success: function (data) {
                               
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
                        closeNewCampaignDialog: closeNewCampaignDialog
                    };
            })()
        };
        return ist.Ads.viewModel;
    });
