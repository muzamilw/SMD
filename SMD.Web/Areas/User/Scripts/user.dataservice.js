/*
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
                    //Get User Profile Data's Base Data
                    amplify.request.define('getUserProfileById', 'ajax', {
                        url: '/Api/UpdateUserFromWeb',
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
                    // get Manage user list
                    amplify.request.define('getDataforManageUser', 'ajax', {
                        url: '/Api/ManageUsers',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // invite User
                    amplify.request.define('inviteUser', 'ajax', {
                        url: '/Api/InviteUser',
                        dataType: 'json',
                        type: 'POST'
                    });
                    // remove User
                    amplify.request.define('removeUser', 'ajax', {
                        url: '/Api/Manageusers',
                        dataType: 'json',
                        type: 'DELETE'
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

          // Get user data
        getUserProfileById = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getUserProfileById',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        },
          // Get manage users
        getDataforManageUser = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getDataforManageUser',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        },
       // invite user
       inviteUser = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'inviteUser',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
       },
        // remove user
       removeUser = function (params, callbacks) {
           initialize();
           return amplify.request({
               resourceId: 'removeUser',
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
            getUserProfileById: getUserProfileById,
            getDataforManageUser: getDataforManageUser,
            getUserProfile: getUserProfile,
            saveUserProfile: saveUserProfile,
            getCitiesByCountry: getCitiesByCountry,
            inviteUser: inviteUser,
            removeUser: removeUser
        };
    })();
    return dataService;
});