/*
    View for the ProfileQuestion. Used to keep the viewmodel clear of UI related logic
*/
define("FranchiseDashboard/profileQuestionApp.view",
    ["jquery", "FranchiseDashboard/profileQuestionApp.viewModel"], function ($, profileQuestionViewModel) {
        var ist = window.ist || {};
        // View 
        ist.ProfileQuestion.view = (function (specifiedViewModel) {
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
        })(profileQuestionViewModel);
        // Initialize the view model
        if (ist.ProfileQuestion.view.bindingRoot) {
            profileQuestionViewModel.initialize(ist.ProfileQuestion.view);
        }
        return ist.ProfileQuestion.view;
    });
