/*
    Data service module with ajax calls to the server
*/
define("Layout/Layout.dataService", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
               
                if (!isInitialized) {

                    isInitialized = true;
                    amplify.request.define('getBranchCategory', 'ajax', {
                        url: '/Api/BranchCategory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getBranchByBranchId', 'ajax', {
                        url: '/Api/BranchCategory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getBranchFiledsByCategoryID', 'ajax', {
                        url: '/Api/BranchCategory',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('SaveCompanyBranch', 'ajax', {
                        url: '/Api/BranchCategory',
                        dataType: 'json',
                        
                        type: 'POST'
                    });
                    amplify.request.define('SaveCategory', 'ajax', {
                        url: '/Api/Category',
                        dataType: 'json',

                        type: 'POST'
                    });
                    amplify.request.define('DeleteCurrentBranch', 'ajax', {
                        url: '/Api/BranchCategory',
                        dataType: 'json',

                        type: 'DELETE'
                    });
                    amplify.request.define('DeleteCurrentCategory', 'ajax', {
                        url: '/Api/Category',
                        dataType: 'json',

                        type: 'DELETE'
                    });
                    amplify.request.define('getAllCountries', 'ajax', {
                        url: '/Api/GetCountry',
                        dataType: 'json',
                        type: 'GET'
                    });
                }
            };

        getBranchCategory = function (callbacks) {
            initialize();
            
            return amplify.request({
                resourceId: 'getBranchCategory',
                success: callbacks.success,
                error: callbacks.error,
            });
        }
        getBranchFiledsByCategoryID = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getBranchFiledsByCategoryID',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        }
        getBranchByBranchId = function (parms, callbacks) {
            initialize();

            return amplify.request({
                resourceId: 'getBranchByBranchId',
                data:parms,
                success: callbacks.success,
                error: callbacks.error,
            });
        }
        SaveCompanyBranch = function (parms, callbacks) {
            initialize();

            return amplify.request({
                resourceId: 'SaveCompanyBranch',
                success: callbacks.success,
                error: callbacks.error,
                data: parms
            });
        },
            SaveCategory = function (parms, callbacks) {
                initialize();

                return amplify.request({
                    resourceId: 'SaveCategory',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: parms
                });
            },
        DeleteCurrentBranch = function (parms, callbacks) {
            initialize();

            return amplify.request({
                resourceId: 'DeleteCurrentBranch',
                success: callbacks.success,
                error: callbacks.error,
                data: parms
            });
        }
        DeleteCurrentCategory = function (parms, callbacks) {
            initialize();

            return amplify.request({
                resourceId: 'DeleteCurrentCategory',
                success: callbacks.success,
                error: callbacks.error,
                data: parms
            });
        }
        getAllCountries = function (callbacks) {
            initialize();

            return amplify.request({
                resourceId: 'getAllCountries',
                success: callbacks.success,
                error: callbacks.error,
            });
        }
        return {
            getBranchCategory: getBranchCategory,
            getBranchByBranchId: getBranchByBranchId,
            getBranchFiledsByCategoryID: getBranchFiledsByCategoryID,
            DeleteCurrentBranch: DeleteCurrentBranch,
            DeleteCurrentCategory:DeleteCurrentCategory,
            SaveCompanyBranch: SaveCompanyBranch,
            SaveCategory: SaveCategory,
            getAllCountries: getAllCountries
        };
    })();
    return dataService;
});