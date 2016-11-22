/*
    Module with the view model for the AdCampaign
*/
define("Coupons/couponReview.viewModel",
    ["jquery", "amplify", "ko", "Coupons/couponReview.dataservice", "Coupons/couponReview.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.CouponReview = {
            viewModel: (function () {
                var view,
                    couponReview = ko.observableArray([]),
                    selectedCoupon = ko.observableArray(),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(5),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    getCouponReviews = function () {
                        dataservice.getCouponReviews(
                                {
                                    PageSize: pager().pageSize(),
                                    PageNo: pager().currentPage(),
                                    SortBy: sortOn(),
                                    IsAsc: sortIsAsc(),
                                    ReviewStatus: $('#selectStatus').val(),
                                },
                                {
                                    success: function (data) {
                                        couponReview.removeAll();
                                        console.log("Coupon Reviews");
                                        console.log(data.CouponsReview);
                                        _.each(data.CouponsReview, function (item) {
                                            couponReview.push(model.CouponReviewServertoClientMapper(item));
                                        });
                                        //pager().totalCount(0);
                                        pager().totalCount(data.TotalCount);
                                        getReviewCount();

                                    },
                                    error: function () {
                                        toastr.error("Failed to load Ad Campaigns!");
                                    }
                                });
                    },
                    SaveCoupon = function (mode,item) {
                        selectedCoupon(item);
                        selectedCoupon().status(mode);
                        dataservice.saveCoupon(selectedCoupon().convertToServerData(), {
                             success: function (response) {
                                 getCouponReviews();
                          
                             },
                             error: function () {
                                 toastr.error("Failed to save!");
                             }
                         });
                        
                    },
                    onPublishCoupon = function (item) {
                         var conformTet = "Are you sure you want to publish this ?";
                         confirmation.messageText(conformTet);
                         confirmation.show();
                         confirmation.afterCancel(function () {
                             confirmation.hide();
                         });
                         confirmation.afterProceed(function () {
                             SaveCoupon(2,item);
                         });
                    },
                     onhideCoupon = function (item) {
                            var conformTet = "Are you sure you want to hide this ?";
                            confirmation.messageText(conformTet);
                            confirmation.show();
                            confirmation.afterCancel(function () {
                                confirmation.hide();
                            });
                            confirmation.afterProceed(function () {
                                SaveCoupon(3, item);
                            });
                     },
                        getReviewCount = function () {
                            dataservice.getReviewCount({
                                success: function (data) {
                                    if (data > 0) {
                                        $("#imgRedbell").css("display", "block");
                                        $("#whiteicon").css("display", "none");
                                    }
                                    else {
                                        $("#whiteicon").css("display", "block");
                                        $("#imgRedbell").css("display", "none");
                                    }

                                },
                                error: function () {
                                    toastr.error("Failed to load Approval Count.");
                                }
                            });
                        },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, couponReview, getCouponReviews));
                        //// First request for LV
                        getCouponReviews();
                    };
                return {

                    initialize: initialize,
                    getCouponReviews: getCouponReviews,
                    couponReview: couponReview,
                    pager: pager,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    onPublishCoupon: onPublishCoupon,
                    onhideCoupon:onhideCoupon,
                    SaveCoupon: SaveCoupon,
                    getReviewCount: getReviewCount
                };
            })()
        };
        return ist.CouponReview.viewModel;
    });
