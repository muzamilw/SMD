﻿/*
    Data service module with ajax calls to the server
*/
define("user/user.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {

                    //Get User Profile Data's Base Data
                    amplify.request.define('getBaseDataForUserProfile', 'ajax', {
                        url: '/Api/UpdateUserFromWebBase',
                        dataType: 'json',
                        type: 'GET'
                    });

                    //Get User Profile
                    amplify.request.define('getUserProfile', 'ajax', {
                        url: '/Api/UpdateUserFromWeb',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    //Update User Profile
                    amplify.request.define('saveUserProfile', 'ajax', {
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
            saveUserProfile = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveUserProfile',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            
            // Get User Profile
            getUserProfile = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getUserProfile',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

        // Get Base data
        getBaseDataForUserProfile = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getBaseDataForUserProfile',
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
            getBaseDataForUserProfile: getBaseDataForUserProfile,
            getUserProfile: getUserProfile,
            saveUserProfile: saveUserProfile,
            getCitiesByCountry: getCitiesByCountry
        };
    })();
    return dataService;
});