define("Layout/Layout.view",
    ["jquery", "Layout/Layout.viewModel"], function ($, branchViewModel) {
        var ist = window.ist || {};
        // View 
        ist.Layout.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#branchBinding")[0],
                bindingPartial = $("#bindingPartialViews")[0],
                     // Show BranchCategory dialog
                showBranchCategoryDialog = function () {
                    $("#branchCategoryDialog").modal("show");
                },
                // Hide BranchCategory dialog
                hideBranchCategoryDialog = function () {
                    $("#branchCategoryDialog").modal("hide");
                };
            return {
                bindingRoot: bindingRoot,
                bindingPartial : bindingPartial,
                viewModel: viewModel,
                showBranchCategoryDialog: showBranchCategoryDialog,
                hideBranchCategoryDialog: hideBranchCategoryDialog,
            };
        })(branchViewModel);

        // Initialize the view model
        if (ist.Layout.view.bindingRoot) {
            branchViewModel.initialize(ist.Layout.view);
        }
        return ist.Layout.view;
    });