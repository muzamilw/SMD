define("Layout/Layout.view",
    ["jquery", "Layout/Layout.viewModel"], function ($, branchViewModel) {
        var ist = window.ist || {};
        // View 
<<<<<<< HEAD
        
        ist.Layout.view = (function (specifiedViewModel) {
=======
        ist.Layout.view = (function (specifiedViewModel) {
>>>>>>> origin/dev
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
<<<<<<< HEAD
            // Show BranchCategory dialog
            showphraseLibraryDialog = function () {
                $("#phraseLibraryDialog").modal("show");
            },
            // Hide BranchCategory dialog
            HidephraseLibraryDialog = function () {
                $("#phraseLibraryDialog").modal("hide");
            };

            return {
=======
            return {
>>>>>>> origin/dev
                bindingRoot: bindingRoot,
                bindingPartial : bindingPartial,
                viewModel: viewModel,
                showBranchCategoryDialog: showBranchCategoryDialog,
<<<<<<< HEAD
                hideBranchCategoryDialog: hideBranchCategoryDialog,
                showphraseLibraryDialog: showphraseLibraryDialog,
                HidephraseLibraryDialog: HidephraseLibraryDialog
            };
=======
                hideBranchCategoryDialog: hideBranchCategoryDialog,
            };
>>>>>>> origin/dev
        })(branchViewModel);

        // Initialize the view model
        if (ist.Layout.view.bindingRoot) {
            branchViewModel.initialize(ist.Layout.view);
        }
        return ist.Layout.view;
    });
    