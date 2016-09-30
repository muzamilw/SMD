
define("Report/walletReport.dataservice", function () {

    // Data service for WalletReport 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Get wallet Report
                    amplify.request.define('getWalletReport', 'ajax', {
                        url: '/Api/Statement',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getreferralComponies', 'ajax', {
                        url: '/Api/ReferralComponiesController',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            // Search Ad Campaigns
            getWalletReport = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getWalletReport',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
           getreferralComponies = function (callbacks) {
                      initialize();
                      return amplify.request({
                          resourceId: 'getreferralComponies',
                          success: callbacks.success,
                          error: callbacks.error,
                      });
                  };

        return {
          
            getWalletReport: getWalletReport,
            getreferralComponies: getreferralComponies,
        };
    })();
    return dataService;
});