/*
    View for the Stripe Example. Used to keep the viewmodel clear of UI related logic
*/
define("pQuestion/stripeExample.view",
    ["jquery", "pQuestion/stripeExample.viewModel"], function ($, stripeViewModel) {
        var ist = window.ist || {};
        // View 
        ist.stripeExample.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#stripeBinding")[0],
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
        })(stripeViewModel);
        // Initialize the view model
        if (ist.stripeExample.view.bindingRoot) {
            stripeViewModel.initialize(ist.stripeExample.view);
        }
        return ist.stripeExample.view;
    });
