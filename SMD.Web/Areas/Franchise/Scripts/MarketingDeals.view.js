/*
    View for the Marketing Deals. Used to keep the viewmodel clear of UI related logic
*/
define("FranchiseDashboard/MarketingDeals.view",
    ["jquery", "FranchiseDashboard/MarketingDeals.viewModel"], function ($, marketingViewModel) {
        var ist = window.ist || {};
        // View 
        ist.Deal.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#marketingDealBindingSpot")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("marketingDealTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getMarketingDeals);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(marketingViewModel);
        // Initialize the view model
        if (ist.Deal.view.bindingRoot) {
            marketingViewModel.initialize(ist.Deal.view);
        }
        return ist.Deal.view;
    });
