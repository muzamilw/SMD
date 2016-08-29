/*
    View for the AdCampaign. Used to keep the viewmodel clear of UI related logic
*/
define("FranchiseDashboard/Coupons.view",
    ["jquery", "FranchiseDashboard/Coupons.viewModel"], function ($, couponsViewModel) {
        var ist = window.ist || {};
        // View 
        ist.Coupons.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#CouponsApprovalBindingSpot")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("adCampaignApprovalLVTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getCampaigns);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(couponsViewModel);
        // Initialize the view model
        if (ist.Coupons.view.bindingRoot) {
            couponsViewModel.initialize(ist.Coupons.view);
        }
        return ist.Coupons.view;
    });
