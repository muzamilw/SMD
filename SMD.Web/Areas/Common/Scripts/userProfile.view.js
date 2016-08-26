﻿/*
    View for the User. Used to keep the viewmodel clear of UI related logic
*/
define("common/userProfile.view",
    ["jquery", "common/userProfile.viewModel"], function ($, userViewModel) {
        var ist = window.ist || {};
        // View 
        ist.userProfile.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#userProfileBinding")[0],
                bindingPartial = $("#bindingUserProfilePartialViews")[0],
                // Binding root used with knockout
                //bindingRootUser = $("#manageUserBinding")[0],
                // Show BranchCategory dialog
                showUserProfileDialog = function () {
                 
                    $("#userProfileDialog").modal("show");
                },
                CloseUserProfileDialog = function () {

                    $("#userProfileDialog").modal("hide");
                },
                 // Show Contact Company the dialog
                showChangePassword = function () {
                    $("#ChangePassword").modal("show");
                    initializeLabelPopovers();
                },
                 // Hide Company Contact the dialog
                hideChangePassword = function () {
                    $("#ChangePassword").modal("hide");
                },
                

            
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                bindingPartial: bindingPartial,
                viewModel: viewModel,
                showUserProfileDialog: showUserProfileDialog,
                CloseUserProfileDialog : CloseUserProfileDialog,
                showChangePassword: showChangePassword,
                hideChangePassword: hideChangePassword,
               
                hideChangePassword: hideChangePassword,
             

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
// Initialize Label Popovers
initializeLabelPopovers = function () {
    // ReSharper disable UnknownCssClass
    $('.bs-example-tooltips a').popover();
    // ReSharper restore UnknownCssClass
}
