/*
    View for the Marketing Deals. Used to keep the viewmodel clear of UI related logic
*/
define("Coupons/couponReview.view",
    ["jquery", "Coupons/couponReview.viewModel"], function ($, couponReviewViewModel) {
        var ist = window.ist || {};
        // View 
        ist.CouponReview.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#couponReviewBindingRoot")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("couponReviewTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getCouponReviews);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(couponReviewViewModel);
        // Initialize the view model
        if (ist.CouponReview.view.bindingRoot) {
            couponReviewViewModel.initialize(ist.CouponReview.view);
        }
        return ist.CouponReview.view;
    });
