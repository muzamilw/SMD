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

// Reads File - Print Out Section
function readPhotoURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = new Image;
            img.onload = function () {
                if (img.height > 250 || img.width > 250) {
                 //   toastr.error("Image Max. width 1280 and height 1024px; please resize the image and try again");
                } else {
                    $('#vehicleImage')
                    .attr('src', e.target.result)
                    .width(120)
                    .height(120);

                }
            };
            img.src = reader.result;
            ist.ProfileQuestion.viewModel.selectedAnswer().imagePath(img.src);
        };
        reader.readAsDataURL(input.files[0]);
    }
}