/*
    View for the AdCampaign. Used to keep the viewmodel clear of UI related logic
*/
define("addApproval/addApproval.view",
    ["jquery", "addApproval/addApproval.viewModel"], function ($, adCampaignViewModel) {
        var ist = window.ist || {};
        // View 
        ist.AdCampaign.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#AdCampaignApprovalBindingSpot")[0],
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
        })(adCampaignViewModel);
        // Initialize the view model
        if (ist.AdCampaign.view.bindingRoot) {
            adCampaignViewModel.initialize(ist.AdCampaign.view);
        }
        return ist.AdCampaign.view;
    });
