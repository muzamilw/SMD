/*
    Data service module with ajax calls to the server
*/
define("FranchiseDashboard/Coupons.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    //Search AdCampaigns
                    amplify.request.define('getCouponsForApproval', 'ajax', {
                        url: '/Api/CouponApproval',
                        dataType: 'json',
                        type: 'GET'
                    });


                    // Edit AdCampaign
                    amplify.request.define('saveCoupon', 'ajax', {
                        url: '/Api/CouponApproval',
                        dataType: 'json',
                        type: 'POST'
                    });

                    amplify.request.define('getCurrenybyID', 'ajax', {
                        url: '/Api/GetCurrency',
                        dataType: 'json',
                        type: 'GET'
                    });

                    amplify.request.define('getCouponCategories', 'ajax', {
                        url: '/Api/Coupon',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Send Mail
                    amplify.request.define('sendApprovalRejectionEmail', 'ajax', {
                        url: '/Api/BuyIt',
                        dataType: 'json',
                        type: 'POST'
                    });
                    amplify.request.define('getCompanyData', 'ajax', {
                        url: '/Api/CompanyDetail',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getCouponPriceOption', 'ajax', {
                        url: '/Api/GetCouponPriceOption',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getapprovalCount', 'ajax', {
                        url: '/Api/GetApprovalCount',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            // Search Ad Campaigns
            getCouponsForApproval = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCouponsForApproval',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },


             // Search Ad Campaigns
            sendApprovalRejectionEmail = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'sendApprovalRejectionEmail',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
               getCurrenybyID = function (params, callbacks) {
                   initialize();
                   return amplify.request({
                       resourceId: 'getCurrenybyID',
                       success: callbacks.success,
                       error: callbacks.error,
                       data: params
                   });
               },
                getCouponCategories = function (params, callbacks) {
                    initialize();
                    return amplify.request({
                        resourceId: 'getCouponCategories',
                        success: callbacks.success,
                        error: callbacks.error,
                        data: params
                    });
                },
            // Save Ad Campaign edit
            saveCoupon = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveCoupon',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
              getCompanyData = function (params, callbacks) {
                  initialize();
                  return amplify.request({
                      resourceId: 'getCompanyData',
                      success: callbacks.success,
                      error: callbacks.error,
                      data: params
                  });
              },
        getCouponPriceOption = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getCouponPriceOption',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        },
          getapprovalCount = function (callbacks) {
              initialize();
              return amplify.request({
                  resourceId: 'getapprovalCount',
                  success: callbacks.success,
                  error: callbacks.error,
              });
          };

        return {
            saveCoupon: saveCoupon,
            getCouponsForApproval: getCouponsForApproval,
            getCurrenybyID: getCurrenybyID,
            getCouponCategories:getCouponCategories,
            sendApprovalRejectionEmail: sendApprovalRejectionEmail,
            getCompanyData: getCompanyData,
            getCouponPriceOption: getCouponPriceOption,
            getapprovalCount: getapprovalCount
        };
    })();
    return dataService;
});