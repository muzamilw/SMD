/*
    View for the ProfileQuestion. Used to keep the viewmodel clear of UI related logic
*/
define("FranchiseDashboard/payPallApp.view",
    ["jquery", "FranchiseDashboard/payPallApp.viewModel"], function ($, payPallAppViewModel) {
        var ist = window.ist || {};
        // View 
        ist.PayPall.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#PPApprovalBindingSpot")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("payPallApprovalLVTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getPayOutHistory);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(payPallAppViewModel);
        // Initialize the view model
        if (ist.PayPall.view.bindingRoot) {
            payPallAppViewModel.initialize(ist.PayPall.view);
        }
        return ist.PayPall.view;
    });
