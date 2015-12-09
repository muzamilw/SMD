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
                    CampaignType = ko.observable('1'),
                    validFromDate = ko.observable(),
                    validUptoDate = ko.observable(),
                    Languages = ko.observableArray([]),
                    Countries = ko.observableArray([]),
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
                    CampaignType: CampaignType,
                    validFromDate: validFromDate,
                    validUptoDate: validUptoDate,
                    Languages: Languages,
                    Countries: Countries
                };
            })()
        };
        return ist.Ads.viewModel;
    });
