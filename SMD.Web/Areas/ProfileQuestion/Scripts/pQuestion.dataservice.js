/*
    Data service module with ajax calls to the server
*/
define("pQuestion/pQuestion.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {

                    //Search Profile Questions
                    amplify.request.define('searchProfileQuestions', 'ajax', {
                        url: '/Api/ProfileQuestion',
                        dataType: 'json',
                        type: 'GET'
                    });

                    isInitialized = true;
                }
            },       
            // Search Profile Questions
            searchProfileQuestions = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'searchProfileQuestions',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            searchProfileQuestions: searchProfileQuestions
        };
    })();
    return dataService;
});