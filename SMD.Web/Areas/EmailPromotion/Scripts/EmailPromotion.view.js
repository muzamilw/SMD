/*
    View for the ads. Used to keep the viewmodel clear of UI related logic
*/
/*
    View for the Order. Used to keep the viewmodel clear of UI related logic
*/
define("Emails/EmailPromotion.view",
    ["jquery", "Emails/EmailPromotion.viewModel"], function ($, contentViewModel) {

        var ist = window.ist || {};
        //  ist.Ads = window.ist.Ads || {};

        // View 
        ist.EmailPromotion.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#emailBinding")[0],
                //bindingRootgoSocial = $("#dialog-goSocial")[0],
                //showSocialDialog = function () {
                //    $("#dialog-goSocial").modal("show");
                //},
                //hideSocialDialog = function () {
                //    $("#dialog-goSocial").modal("hide");
                //},
                
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }

                };
            initialize();

            return {
                bindingRoot: bindingRoot,
                //bindingRootgoSocial: bindingRootgoSocial,
                //showSocialDialog: showSocialDialog,
                //hideSocialDialog: hideSocialDialog,
                viewModel: viewModel,
            };
        })(contentViewModel);

        // Initialize the view model
        if (ist.EmailPromotion.view.bindingRoot) {
            ist.EmailPromotion.viewModel.initialize(ist.EmailPromotion.view);
        }
        return ist.EmailPromotion.view;
    });
