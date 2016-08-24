/*
    Data service module with ajax calls to the server
*/
define("FranchiseDashboard/surveyQuestionApp.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {

                    //Search Survey Questions
                    amplify.request.define('searchSurveyQuestions', 'ajax', {
                        url: '/Api/SurveyQuestionApproval',
                        dataType: 'json',
                        type: 'GET'
                    });
                    

                    // Edit Survey Questions
                    amplify.request.define('saveSurveyQuestion', 'ajax', {
                        url: '/Api/SurveyQuestionApproval',
                        dataType: 'json',
                        type: 'POST'
                    });
                    isInitialized = true;
                }
            },       
            // Search Survey Questions
            searchSurveyQuestions = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'searchSurveyQuestions',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            

            
            // Save Survey  Questions edit
            saveSurveyQuestion = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveSurveyQuestion',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };

        return {
            saveSurveyQuestion: saveSurveyQuestion,
            searchSurveyQuestions: searchSurveyQuestions
        };
    })();
    return dataService;
});