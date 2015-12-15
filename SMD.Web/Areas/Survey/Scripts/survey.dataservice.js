/*
    Data service module with ajax calls to the server
*/
define("survey/survey.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    //Search Profile Questions
                    amplify.request.define('searchSurveyQuestions', 'ajax', {
                        url: '/Api/Survey',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // callings ads comapaign service for profile questions and survey questions list 
                    amplify.request.define('getBaseData', 'ajax', {
                        url: '/Api/AdCampaignBase',
                        dataType: 'json',
                        type: 'GET'
                    });


                    isInitialized = true;
                }
            },
            // callings ads comapaign service for profile questions and survey questions list 
            getBaseData = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
              // Search Profile Questions
            searchSurveyQuestions = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'searchSurveyQuestions',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            getBaseData: getBaseData,
            searchSurveyQuestions: searchSurveyQuestions
        };
       
    })();
    return dataService;
});