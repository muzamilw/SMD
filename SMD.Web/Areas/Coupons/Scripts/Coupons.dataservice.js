/*
    Data service module with ajax calls to the server
*/
define("Coupons/Coupons.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    isInitialized = true;
                    amplify.request.define('getBaseData', 'ajax', {
                        url: '/Api/AdCampaignBase',
                        dataType: 'json',
                        type: 'GET'
                    });

                    amplify.request.define('getCampaignData', 'ajax', {
                        url: '/Api/Coupon',
                        dataType: 'json',
                        type: 'GET'
                    });

                    amplify.request.define('addCampaignData', 'ajax', {
                        url: '/Api/Coupon',
                        dataType: 'json',
                        dataMap: JSON.stringify,
                        contentType: "application/json; charset=utf-8",
                        type: 'POST'
                    });

                    amplify.request.define('UpdateCampaignCriteriaOrLocation', 'ajax', {
                        url: '/Api/UpdateCampaign',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // getting audience for survey
                    amplify.request.define('getAudienceData', 'ajax', {
                        url: '/Api/SurveyAudience',
                        dataType: 'json',
                        dataMap: JSON.stringify,
                        contentType: "application/json; charset=utf-8",
                        type: 'POST'
                    });

                    amplify.request.define('copyCampaignById', 'ajax', {
                        url: '/Api/CopyCampaign',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // 
                    amplify.request.define('generateCouponCodes', 'ajax', {
                        url: '/Api/GenerateCoupon',
                        dataType: 'json',
                        ////dataMap: JSON.stringify,
                        //contentType: "application/json; charset=utf-8",
                        type: 'Get'
                    });
                }
            };

        getBaseData = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getBaseData',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        },
        getCampaignData = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getCampaignData',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        },
        addCampaignData = function (params, callbacks) {
             initialize();
             return amplify.request({
                 resourceId: 'addCampaignData',
                 data: params,
                 success: callbacks.success,
                 error: callbacks.error,
             });
        },
         UpdateCampaignCriteriaOrLocation = function (params, callbacks) {
              initialize();
              return amplify.request({
                  resourceId: 'UpdateCampaignCriteriaOrLocation',
                  data: params,
                  success: callbacks.success,
                  error: callbacks.error,
              });
         },
        // get audiance Count
             getAudienceData = function (params, callbacks) {
                 initialize();
                 return amplify.request({
                     resourceId: 'getAudienceData',
                     data: params,
                     success: callbacks.success,
                     error: callbacks.error,
                 });
             },
              // copy campaign
             copyCampaignById = function (params, callbacks) {
                 initialize();
                 return amplify.request({
                     resourceId: 'copyCampaignById',
                     data: params,
                     success: callbacks.success,
                     error: callbacks.error,
                 });
             },
        generateCouponCodes = function (params, callbacks) {
                 initialize();
                 return amplify.request({
                     resourceId: 'generateCouponCodes',
                     data: params,
                     success: callbacks.success,
                     error: callbacks.error,
                 });
             }
        return {
            getBaseData: getBaseData,
            getCampaignData: getCampaignData,
            addCampaignData: addCampaignData,
            UpdateCampaignCriteriaOrLocation: UpdateCampaignCriteriaOrLocation,
            getAudienceData: getAudienceData,
            copyCampaignById: copyCampaignById,
            generateCouponCodes: generateCouponCodes
        };
    })();
    return dataService;
});