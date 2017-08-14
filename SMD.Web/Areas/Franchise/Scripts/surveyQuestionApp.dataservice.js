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
            saveSurveyQuestion: saveSurveyQuestion,
            searchSurveyQuestions: searchSurveyQuestions,
            getCompanyData: getCompanyData,
            getapprovalCount: getapprovalCount
        };
    })();
    return dataService;
});