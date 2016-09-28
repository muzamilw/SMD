﻿/*
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
                    amplify.request.define('getCompanyDate', 'ajax', {
                        url: '/Api/Company',
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
              getCompanyDate = function (params, callbacks) {
                  initialize();
                  return amplify.request({
                      resourceId: 'getCompanyDate',
                      success: callbacks.success,
                      error: callbacks.error,
                      data: params
                  });
              };

        return {
            saveCoupon: saveCoupon,
            getCouponsForApproval: getCouponsForApproval,
            getCurrenybyID: getCurrenybyID,
            getCouponCategories:getCouponCategories,
            sendApprovalRejectionEmail: sendApprovalRejectionEmail,
            getCompanyDate: getCompanyDate
        };
    })();
    return dataService;
});