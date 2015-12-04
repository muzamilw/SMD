/*
    View for the ads. Used to keep the viewmodel clear of UI related logic
*/
/*
    View for the Order. Used to keep the viewmodel clear of UI related logic
*/
define("ads/ads.view",
    ["jquery", "ads/ads.viewModel"], function ($, contentViewModel) {

        var ist = window.ist || {};
      //  ist.Ads = window.ist.Ads || {};

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
                viewModel: viewModel,

            };
        })(contentViewModel);

        // Initialize the view model
        if (ist.Ads.view.bindingRoot) {
            ist.Ads.viewModel.initialize(ist.Ads.view);
        }
        return ist.Ads.view;
    });
