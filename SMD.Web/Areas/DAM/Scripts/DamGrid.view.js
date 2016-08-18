/*
    View for the ads. Used to keep the viewmodel clear of UI related logic
*/
define("DamGrid/DamGrid.view",
    ["jquery", "DamGrid/DamGrid.viewModel"], function ($, parentHireGroupViewModel) {
        var ist = window.ist || {};
        // View 
        ist.DamGrid.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#DamGridBinding")[0],
               
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
        if (ist.DamGrid.view.bindingRoot) {
            parentHireGroupViewModel.initialize(ist.DamGrid.view);
        }
        return ist.DamGrid.view;
    });
