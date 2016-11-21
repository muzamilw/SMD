/*
    Data service module with ajax calls to the server
*/
define("Coupons/couponReview.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    //Search AdCampaigns
                    amplify.request.define('getCouponReviews', 'ajax', {
                        url: '/Api/MarketingDeals',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Edit AdCampaign
                    amplify.request.define('saveCoupon', 'ajax', {
                        url: '/Api/MarketingDeals',
                        dataType: 'json',
                        type: 'POST'
                    });
                    isInitialized = true;
                }
            },
            // Search Ad Campaigns
            getCouponReviews = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCouponReviews',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

             // Search Ad Campaigns



            // Save Ad Campaign edit
            saveCoupon = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveCoupon',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            saveCoupon: saveCoupon,
            getCouponReviews: getCouponReviews

        };
    })();
    return dataService;
});