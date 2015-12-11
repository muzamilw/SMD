/*
    Module with the view model for the Stripe Example
*/
define("pQuestion/paypalExample.viewModel",
    ["jquery", "amplify", "ko", "common/paypalPayment.viewModel"],
    function ($, amplify, ko, paypalPayment) {
        var ist = window.ist || {};
        ist.paypalExample = {
            viewModel: (function() {
                var view,
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);

                        paypalPayment.setupPayment(null, 100, 'khurram-facilitator@innostark.com', 'khurram-buyer@innostark.com');
                    };
                return {
                    initialize: initialize
                };
            })()
        };
        return ist.paypalExample.viewModel;
    });
