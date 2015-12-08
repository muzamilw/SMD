/*
    Module with the view model for the Stripe Example
*/
define("pQuestion/stripeExample.viewModel",
    ["jquery", "amplify", "ko", "common/stripeChargeCustomer.viewModel"],
    function ($, amplify, ko, stripeChargeCustomer) {
        var ist = window.ist || {};
        ist.stripeExample = {
            viewModel: (function() {
                var view,
                    // Charge Customer
                    chargeCustomer = function() {
                        stripeChargeCustomer.show(undefined, 2000, '2 Widgets');
                    },
                    // Charge Existing Customer
                    chargeExistingCustomer = function () {
                        stripeChargeCustomer.show(undefined, 2000, '2 Widgets', true);
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                    };
                return {
                    initialize: initialize,
                    chargeCustomer: chargeCustomer,
                    chargeExistingCustomer: chargeExistingCustomer
                };
            })()
        };
        return ist.stripeExample.viewModel;
    });
