
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
                        url: '/Api/ApproveProfileQuestion',
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
            };

        return {
          
            getWalletReport: getWalletReport,
        };
    })();
    return dataService;
});