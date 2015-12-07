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
                    CampaignType = ko.observable('Video'),
                    getAdCampaignGridContent = function () {
                        dataservice.GetAdverts({}, {
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
                     // Add new Profile Question
                    addNewCampaign = function () {
                        isEditorVisible(true);
                    },
                // Initialize the view model
                initialize = function (specifiedView) {
                    view = specifiedView; 
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    pager(pagination.Pagination({ PageSize: 10 }, advertGridContent, getAdCampaignGridContent));
                    getAdCampaignGridContent();
                };
                return {
                    initialize: initialize,
                    pager: pager,
                    isEditorVisible:isEditorVisible,
                    advertGridContent: advertGridContent,
                    addNewCampaign: addNewCampaign,
                    CampaignType: CampaignType
                };
            })()
        };
        return ist.Ads.viewModel;
    });
