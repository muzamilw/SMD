/*
    Module with the view model for the Profile Questions
*/
define("ads/ads.viewModel",
    ["jquery", "amplify", "ko", "ads/ads.dataservice", "ads/ads.model","common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.Ads = {
            viewModel: (function () {
                var view,
                    advertGridContent = ko.observableArray([]),
                    getAdvertGridContent = function () {
                        
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

                     }
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                       
                    };
                return {
                    initialize: initialize,
                    advertGridContent:advertGridContent
                };
            })()
        };
        return ist.Ads.viewModel;
    });
