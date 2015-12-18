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
                    // posting survey to save/update
                    amplify.request.define('addSurveyData', 'ajax', {
                        url: '/Api/Survey',
                        dataType: 'json',
                        dataMap: JSON.stringify,
                        contentType: "application/json; charset=utf-8",
                        type: 'POST'
                    });
                    // getting survey for editor
                    amplify.request.define('getSurveyQuestion', 'ajax', {
                        url: '/Api/SurveyQuestionEditor',
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
            },
               // get survey Questions
            getSurveyQuestion = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getSurveyQuestion',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
           addSurveyData = function (params, callbacks) {
               initialize();
               return amplify.request({
                   resourceId: 'addSurveyData',
                   data: params,
                   success: callbacks.success,
                   error: callbacks.error,
               });
           };

        return {
            getBaseData: getBaseData,
            searchSurveyQuestions: searchSurveyQuestions,
            addSurveyData: addSurveyData,
            getSurveyQuestion: getSurveyQuestion
        };
       
    })();
    return dataService;
});