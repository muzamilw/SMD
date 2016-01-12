/*
    View for the User. Used to keep the viewmodel clear of UI related logic
*/
define("user/user.view",
    ["jquery", "user/user.viewModel"], function ($, userViewModel) {
        var ist = window.ist || {};
        // View 
        ist.userProfile.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#userProfileScreen")[0],
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
        })(userViewModel);
        // Initialize the view model
        if (ist.userProfile.view.bindingRoot) {
            userViewModel.initialize(ist.userProfile.view);
        }
        return ist.userProfile.view;
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
            ist.userProfile.viewModel.selectedUser().imageUrl(img.src);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
