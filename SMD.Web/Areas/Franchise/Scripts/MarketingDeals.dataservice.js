/*
    Data service module with ajax calls to the server
*/
define("FranchiseDashboard/MarketingDeals.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    //Search AdCampaigns
                    amplify.request.define('getMarketingDeals', 'ajax', {
                        url: '/Api/MarketingDeals',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getCouponPriceOption', 'ajax', {
                        url: '/Api/GetCouponPriceOption',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getCurrenybyID', 'ajax', {
                        url: '/Api/GetCurrency',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getCompanyData', 'ajax', {
                        url: '/Api/CompanyDetail',
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
            getMarketingDeals = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getMarketingDeals',
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
           getCurrenybyID = function (params, callbacks) {
               initialize();
               return amplify.request({
                   resourceId: 'getCurrenybyID',
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
            getMarketingDeals: getMarketingDeals,
            getCouponPriceOption: getCouponPriceOption,
            getCurrenybyID: getCurrenybyID,
            getCompanyData: getCompanyData

        };
    })();
    return dataService;
});