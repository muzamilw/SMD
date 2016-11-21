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

                                },
                                {
                                    success: function (data) {
                                        //couponReview.removeAll();
                                        //console.log("Coupon Reviews");
                                        //console.log(data.Coupons);
                                        //_.each(data.Coupons, function (item) {
                                        //    marketingDeals.push(model.MarketingDealServertoClientMapper(item));
                                        //});
                                        ////pager().totalCount(0);
                                        //pager().totalCount(data.TotalCount);

                                    },
                                    error: function () {
                                        toastr.error("Failed to load Ad Campaigns!");
                                    }
                                });
                    },
                    SaveCoupon = function () {
                         var couponId = selectedCoupon().couponId();
                         dataservice.saveCoupon(selectedCoupon().convertToServerData(), {
                             success: function (response) {
                                 getCouponReviews();
                                 isEditorVisible(false);
                                 toastr.success("Saved Successfully.");
                             },
                             error: function () {
                                 toastr.error("Failed to save!");
                             }
                         });
                        
                    },
                    onSaveCoupon = function () {
                         var conformTet = "Save changes?";
                         confirmation.messageText(conformTet);
                         confirmation.show();
                         confirmation.afterCancel(function () {
                             confirmation.hide();
                         });
                         confirmation.afterProceed(function () {
                             SaveCoupon();
                             //$("#topArea").css("display", "block");
                             //$("#divApprove").css("display", "block");
                             //toastr.success("Approved Successfully.");
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
                    onSaveCoupon:onSaveCoupon
                };
            })()
        };
        return ist.CouponReview.viewModel;
    });
