/*
    View for the ads. Used to keep the viewmodel clear of UI related logic
*/
define("ads/ads.view",
    ["jquery", "ads/ads.viewModel"], function ($, parentHireGroupViewModel) {
        var ist = window.ist || {};
        // View 
        ist.Ads.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#adsBinding")[0],
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
        })(parentHireGroupViewModel);
        // Initialize the view model
        if (ist.Ads.view.bindingRoot) {
            parentHireGroupViewModel.initialize(ist.Ads.view);
        }
        return ist.Ads.view;
    });
