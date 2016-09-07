/*
    View for the Coupons. Used to keep the viewmodel clear of UI related logic
*/
define("analytic/analytic.view",
    ["jquery", "analytic/analytic.viewModel"], function ($, analyticViewModel) {
        var ist = window.ist || {};
        // View 
        ist.analytic.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#analyticsdashboardBindingRoot")[0],
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
        })(analyticViewModel);
        // Initialize the view model
        if (ist.analytic.view.bindingRoot) {
            analyticViewModel.initialize(ist.analytic.view);
        }
        return ist.analytic.view;
    });
