/*
    Module with the view model for the Profile Questions
*/
define("ads/ads.viewModel",
    ["jquery", "amplify", "ko", "ads/ads.dataservice", "ads/ads.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.Ads = {
            viewModel: (function () {
                var view,
                    advertGridContent = ko.observableArray([]),
                    getAdvertGridContent = function () {

                        dataservice.GetAdverts({}, {
                            success: function (data) {
                                console.log("add data " + data);
                                if (data != null) {
                                    advertGridContent.removeAll();

                                    $.each(data, function (index, item) {
                                        console.log("add module " + item.CampaignId);
                                        var module = model.adsMapper(item);
                                        console.log("add module " + module);
                                        advertGridContent.push(module);
                                    });
                                }
                                
                            },
                            error: function (response) {

                            }
                        });

                    }
                // Initialize the view model
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    alert();
                    getAdvertGridContent();
                };
                return {
                    initialize: initialize,
                    advertGridContent: advertGridContent
                };
            })()
        };
        return ist.Ads.viewModel;
    });
