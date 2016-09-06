/*
    Module with the view model for the AdCampaign
*/
define("analytic/analytic.viewModel",
    ["jquery", "amplify", "ko", "analytic/analytic.dataservice", "analytic/analytic.model"],
    function ($, amplify, ko, dataservice, model) {
        var ist = window.ist || {};
        ist.Coupons = {
            viewModel: (function () {
                var view,
                    
                    getAnalytic = function () {
                       
                    },
               
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        getAnalytic();
                    };
                return {

                    initialize: initialize,
                    getAnalytic: getAnalytic                   

                };
            })()
        };
        return ist.analytic.viewModel;
    });
