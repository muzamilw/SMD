/*
    View for the User. Used to keep the viewmodel clear of UI related logic
*/
define("common/companyProfile.view",
    ["jquery", "common/companyProfile.viewModel"], function ($, companyViewModel) {
        var ist = window.ist || {};
        // View 
        ist.companyProfile.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#companyProfileBinding")[0],
                bindingPartial = $("#bindingCompanyProfilePartialViews")[0],
                // Binding root used with knockout
                //bindingRootUser = $("#manageUserBinding")[0],
                // Show BranchCategory dialog
                showCompanyProfileDialog = function () {
                 
                    $("#companyProfileDialog").modal("show");
                },
                CloseCompanyProfileDialog = function () {

                    $("#companyProfileDialog").modal("hide");
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
                showCompanyProfileDialog: showCompanyProfileDialog,
                CloseCompanyProfileDialog: CloseCompanyProfileDialog,
            
             

            };
        })(companyViewModel);
        // Initialize the view model
        if (ist.companyProfile.view.bindingRoot) {
            companyViewModel.initialize(ist.companyProfile.view);
        }

       
        return ist.companyProfile.view;
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
            ist.companyProfile.viewModel.selectedUser().imageUrl(img.src);
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

