/*
    Data service module with ajax calls to the server
*/
define("analytic/analytic.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    amplify.request.define('getactiveuser', 'ajax', {
                        url: '/Api/ActiveUser',
                        dataType: 'json',
                        type: 'GET'
                    });

                    amplify.request.define('getDashboardInsights', 'ajax', {
                        url: '/Api/DashboardInsight',
                        dataType: 'json',
                        type: 'GET'
                    });
					amplify.request.define('getCampaignsByStatus', 'ajax', {
                        url: '/Api/SuperNovaCampaign',
                        dataType: 'json',
                        type: 'GET'
                    });
					 amplify.request.define('getRevenueOverTime', 'ajax', {
                        url: '/Api/Revenue',
                        dataType: 'json',
                        type: 'GET'
                    });
					amplify.request.define('getPayoutVSRevenueOverTime', 'ajax', {
                        url: '/Api/Revenue',
                        dataType: 'json',
                        type: 'GET'
                    });
					 amplify.request.define('getUserCounts', 'ajax', {
                        url: '/Api/User',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            // get DashboardInsight
            getDashboardInsights = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getDashboardInsights',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
			},
			 getCampaignsByStatus = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCampaignsByStatus',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
			},
			getUserCounts = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getUserCounts',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
			},
			getPayoutVSRevenueOverTime = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPayoutVSRevenueOverTime',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
			},
			getRevenueOverTime = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getRevenueOverTime',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
			},
            // Search Ad Campaigns
            getactiveuser = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getactiveuser',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            }; 

        return {
            getactiveuser: getactiveuser,
			getDashboardInsights:getDashboardInsights,
			getRevenueOverTime:getRevenueOverTime,
			getCampaignsByStatus: getCampaignsByStatus,
			getUserCounts:getUserCounts,
			getPayoutVSRevenueOverTime:getPayoutVSRevenueOverTime
        };
    })();
    return dataService;
});