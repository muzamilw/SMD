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
                    amplify.request.define('getBaseDataForProfileQuestions', 'ajax', {
                        url: '/Api/AdCampaignBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    amplify.request.define('getFilterBaseData', 'ajax', {
                        url: '/Api/AdCampaignBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    //Delete Profile Questions
                    amplify.request.define('deleteProfileQuestion', 'ajax', {
                        url: '/Api/ProfileQuestion',
                        dataType: 'json',
                        type: 'DELETE'
                    });

                    
                    //Get Base Data for Profile Questions
                    amplify.request.define('getBaseData', 'ajax', {
                        url: '/Api/ProfileQuestionBase',
                        dataType: 'json',
                        type: 'GET'
                    });
                    
                    amplify.request.define('getAudienceData', 'ajax', {
                        url: '/Api/SurveyAudience',
                        dataType: 'json',
                        dataMap: JSON.stringify,
                        contentType: "application/json; charset=utf-8",
                        type: 'POST'
                    });

                    //Get Profile Question Answer 
                    amplify.request.define('getPqAnswer', 'ajax', {
                        url: '/Api/ProfileQuestionAnswer',
                        dataType: 'json',
                        type: 'GET'
                    });


                    // Add/Edit Profile Questions
                    amplify.request.define('saveProfileQuestion', 'ajax', {
                        url: '/Api/ProfileQuestion',
                        dataType: 'json',
                        type: 'POST'
                    });
                    amplify.request.define('getProduct', 'ajax', {
                        url: '/Api/GetProduct',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
             // callings ads comapaign service for profile questions and survey questions list 
            getFilterBaseData = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getFilterBaseData',
                    data: params,
                    success: callbacks.success,
                    error: callbacks.error,
                });
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
            },
            getBaseDataForProfileQuestions = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseDataForProfileQuestions',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
              getAudienceData = function (params, callbacks) {
                  initialize();
                  return amplify.request({
                      resourceId: 'getAudienceData',
                      success: callbacks.success,
                      error: callbacks.error,
                      data: params
                  });
              },


             // Get Base Data of Profile Questions
            getBaseData = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getBaseData',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            
             // Get Profile Questions Answer On edit 
            getPqAnswer = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPqAnswer',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },

              // Get Base Data of Profile Questions
            deleteProfileQuestion = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteProfileQuestion',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            
            // Save Profile Questions add /edit
            saveProfileQuestion = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveProfileQuestion',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };
        getProduct = function (callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getProduct',
                success: callbacks.success,
                error: callbacks.error,
            });
        };

        return {
            searchProfileQuestions: searchProfileQuestions,
            getBaseData: getBaseData,
            deleteProfileQuestion: deleteProfileQuestion,
            getPqAnswer: getPqAnswer,
            saveProfileQuestion: saveProfileQuestion,
            getAudienceData: getAudienceData,
            getProduct: getProduct,
            getFilterBaseData:getFilterBaseData,
            getBaseDataForProfileQuestions: getBaseDataForProfileQuestions
        };
    })();
    return dataService;
});