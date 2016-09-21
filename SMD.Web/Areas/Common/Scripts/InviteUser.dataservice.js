/*
    Data service module with ajax calls to the server
*/
define("common/InviteUser.dataService", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {

                if (!isInitialized) {

                    isInitialized = true;
                    amplify.request.define('InviteUserSvc', 'ajax', {
                        url: '/Api/PhaseLibrary',
                        dataType: 'json',
                        type: 'GET'
                    });

                 
                }
            };

        InviteUserSvc = function (params, callbacks) {
            initialize();

            return amplify.request({
                resourceId: 'InviteUserSvc',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        };
       
        return {
            InviteUserSvc: InviteUserSvc
          
        };
    })();
    return dataService;
});