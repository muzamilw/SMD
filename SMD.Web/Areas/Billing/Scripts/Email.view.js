/*
    View for the Invoice. Used to keep the viewmodel clear of UI related logic
*/
define("invoice/Email.view",
    ["jquery", "invoice/Email.viewModel"], function ($, emailViewModel) {
        var ist = window.ist || {};
        // View 
        ist.Email.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#EmailBindingSpot")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("emailLVTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getEmails);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(emailViewModel);
        // Initialize the view model
        if (ist.Email.view.bindingRoot) {
            emailViewModel.initialize(ist.Email.view);
        }
        return ist.Email.view;
    });
