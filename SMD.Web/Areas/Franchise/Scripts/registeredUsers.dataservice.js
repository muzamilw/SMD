/*
    Data service module with ajax calls to the server
*/
define("FranchiseDashboard/registeredUsers.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    //Search AdCampaigns
                    amplify.request.define('getRegisteredUsers', 'ajax', {
                        url: '/Api/RegisteredUsers',
                        dataType: 'json',
                        type: 'GET'
                    });


                    // Edit AdCampaign
                    amplify.request.define('updateComanyStatus', 'ajax', {
                        url: '/Api/RegisteredUsers',
                        dataType: 'json',
                        type: 'POST'
                    });
                    amplify.request.define('getCompanyData', 'ajax', {
                        url: '/Api/CompanyDetail',
                        dataType: 'json',
                        type: 'GET'
                    });

                    isInitialized = true;
                }
            },
            // Search Ad Campaigns
            getRegisteredUsers = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getRegisteredUsers',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // Save Ad Campaign edit
            updateComanyStatus = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'updateComanyStatus',
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
         };
            
        return {
            updateComanyStatus: updateComanyStatus,
            getRegisteredUsers: getRegisteredUsers,
            getCompanyData: getCompanyData
          

        };
    })();
    return dataService;
});