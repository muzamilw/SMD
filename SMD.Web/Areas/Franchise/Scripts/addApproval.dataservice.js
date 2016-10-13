/*
    Data service module with ajax calls to the server
*/
define("FranchiseDashboard/addApproval.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {

                    //Search AdCampaigns
                    amplify.request.define('searchAdCampaigns', 'ajax', {
                        url: '/Api/AdCampaign',
                        dataType: 'json',
                        type: 'GET'
                    });
                    

                    // Edit AdCampaign
                    amplify.request.define('saveAdCampaign', 'ajax', {
                        url: '/Api/AdCampaignApproval',
                        dataType: 'json',
                        type: 'POST'
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
                    amplify.request.define('getapprovalCount', 'ajax', {
                        url: '/Api/GetApprovalCount',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },       
            // Search Ad Campaigns
            searchAdCampaigns = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'searchAdCampaigns',
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
            // Save Ad Campaign edit
            saveAdCampaign = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveAdCampaign',
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
                getapprovalCount = function (callbacks) {
                      initialize();
                      return amplify.request({
                          resourceId: 'getapprovalCount',
                          success: callbacks.success,
                          error: callbacks.error,
                      });
                  };

        return {
            saveAdCampaign: saveAdCampaign,
            searchAdCampaigns: searchAdCampaigns,
            sendApprovalRejectionEmail: sendApprovalRejectionEmail,
            getCompanyData: getCompanyData,
            getapprovalCount: getapprovalCount
        };
    })();
    return dataService;
});