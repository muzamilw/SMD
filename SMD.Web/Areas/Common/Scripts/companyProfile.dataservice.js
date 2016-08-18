/*
    Data service module with ajax calls to the server
*/
define("common/companyProfile.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {

                    //Get User Profile Data's Base Data
                    amplify.request.define('getBaseDataForCompanyProfile', 'ajax', {
                        url: '/Api/UpdateUserFromWebBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                   
                    //Get User Profile
                    amplify.request.define('getCompanyProfile', 'ajax', {
                        url: '/Api/Company',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    //Update User Profile
                    amplify.request.define('saveCompanyProfile', 'ajax', {
                        url: '/Api/UpdateUserFromWeb',
                        dataType: 'json',
                        type: 'POST'
                    });

                    // Save User Profile
                    amplify.request.define('getCitiesByCountry', 'ajax', {
                        url: '/Api/GetCityByCountry',
                        dataType: 'json',
                        type: 'GET'
                    });
                   
                    isInitialized = true;
                }
            },

            // Save User Profile
            saveCompanyProfile = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveCompanyProfile',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            
            // Get User Profile
            getCompanyProfile = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getCompanyProfile',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

        // Get Base data
        getBaseDataForCompanyProfile = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getBaseDataForCompanyProfile',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        },

             // Get Cities
        getCitiesByCountry = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getCitiesByCountry',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });

        };
           

        return {
            getBaseDataForCompanyProfile: getBaseDataForCompanyProfile,
         
            getCompanyProfile: getCompanyProfile,
            saveCompanyProfile: saveCompanyProfile,
            getCitiesByCountry: getCitiesByCountry
         
        };
    })();
    return dataService;
});