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
                        url: '/Api/CouponReview',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Edit AdCampaign
                    amplify.request.define('saveCoupon', 'ajax', {
                        url: '/Api/CouponReview',
                        dataType: 'json',
                        type: 'POST'
                    });
                    amplify.request.define('getReviewCount', 'ajax', {
                        url: '/Api/CouponReviewCount',
                        dataType: 'json',
                        type: 'GET'
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
              getReviewCount = function (callbacks) {
                  initialize();
                  return amplify.request({
                      resourceId: 'getReviewCount',
                      success: callbacks.success,
                      error: callbacks.error,
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
            getCouponReviews: getCouponReviews,
            getReviewCount: getReviewCount

        };
    })();
    return dataService;
});