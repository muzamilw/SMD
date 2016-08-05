/*
    View for the Paypal Example. Used to keep the viewmodel clear of UI related logic
*/
define("pQuestion/paypalExample.view",
    ["jquery", "pQuestion/paypalExample.viewModel"], function ($, paypalViewModel) {
        var ist = window.ist || {};
        // View 
        ist.paypalExample.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#paypalBinding")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(paypalViewModel);
        // Initialize the view model
        if (ist.paypalExample.view.bindingRoot) {
            paypalViewModel.initialize(ist.paypalExample.view);
        }
        return ist.paypalExample.view;
    });
