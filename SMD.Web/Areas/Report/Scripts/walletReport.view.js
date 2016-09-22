/*
    View for the Coupons. Used to keep the viewmodel clear of UI related logic
*/
define("Report/walletReport.view",
    ["jquery", "Report/walletReport.viewModel"], function ($, walletreportViewModel) {
        var ist = window.ist || {};
        // View 
        ist.Report.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#walletreportBindingRoot")[0],
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
        })(walletreportViewModel);
        // Initialize the view model
        if (ist.Report.view.bindingRoot) {
            walletreportViewModel.initialize(ist.Report.view);
        }
        return ist.Report.view;
    });
