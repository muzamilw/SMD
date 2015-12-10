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
                    isEditorVisible = ko.observable(true),
                    Languages = ko.observableArray([]),
                    Countries = ko.observableArray([]),
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
                                      Languages.removeAll();
                                      ko.utils.arrayPushAll(Languages(), data.Languages);// [{ LanguageId: 1, LanguageName: "Abkhaz" }, { LanguageId: 2, LanguageName: "Afar" }]);
                                      Languages.valueHasMutated();

                                      Countries.removeAll();
                                      ko.utils.arrayPushAll(Countries(), data.countries);
                                      Countries.valueHasMutated();
                                  }

                              },
                              error: function (response) {

                              }
                          });

                      },
                     // Add new Profile Question
                    addNewCampaign = function () {
                        isEditorVisible(true);
                    },
                    closeNewCampaignDialog = function () {
                        isEditorVisible(false);
                    },
                      saveCampaignData = function () {
                          console.log(campaignModel());
                         
                          dataservice.addCampaignData(campaignModel().convertToServerData(), {
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
                  //  pager(pagination.Pagination({ PageSize: 10 }, advertGridContent, getAdCampaignGridContent));
                    getBaseData();
                   // getAdCampaignGridContent();
                };
                return {
                    initialize: initialize,
                    pager: pager,
                    isEditorVisible:isEditorVisible,
                    advertGridContent: advertGridContent,
                    addNewCampaign: addNewCampaign,                   
                    Languages: Languages,
                    Countries: Countries,
                    campaignModel: campaignModel,
                    saveCampaignData: saveCampaignData,
                    closeNewCampaignDialog: closeNewCampaignDialog
                };
            })()
        };
        return ist.Ads.viewModel;
    });
