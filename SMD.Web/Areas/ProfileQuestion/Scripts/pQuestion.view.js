/*
    View for the Questions. Used to keep the viewmodel clear of UI related logic
*/
define("pQuestion/pQuestion.view",
    ["jquery", "pQuestion/pQuestion.viewModel"], function ($, parentHireGroupViewModel) {
        var ist = window.ist || {};
        // View 
        ist.ProfileQuestion.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#ProfileQuestionBindingSpot")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("profileQuestionLVTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getQuestions);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(parentHireGroupViewModel);
        // Initialize the view model
        if (ist.ProfileQuestion.view.bindingRoot) {
            parentHireGroupViewModel.initialize(ist.ProfileQuestion.view);
        }
        return ist.ProfileQuestion.view;
    });
