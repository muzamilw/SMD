/*
    Data service module with ajax calls to the server
*/
define("common/phraseLibrary.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get Get Sections List 
                    amplify.request.define('getSections', 'ajax', {
                        url: ist.siteUrl + '/Api/PhaseLibrary',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to get Phrases By Phrase Field Id
                    amplify.request.define('getPhrasesByPhraseById', 'ajax', {
                        url: ist.siteUrl + '/Api/PhaseLibrary',
                        dataType: 'json',
                        type: 'GET'
                    });
                    // Define request to Phase Library
                    amplify.request.define('savePhaseLibrary', 'ajax', {
                        url: ist.siteUrl + '/Api/PhaseLibrary',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                    // Define request to Get Phrase Fileds By Section Id
                    
                    isInitialized = true;
                }
            },
            // get Sections
            getSections = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getSections',
                    success: callbacks.success,
                    error: callbacks.error,
                });

            },
             //Get Phrase Fileds By Section Id
            getPhraseFiledsBySectionId = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPhraseFiledsBySectionId',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
            // get Phrases By Phrase Field Id
            getPhrasesByPhraseById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getPhrasesByPhraseById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             // save Store
            savePhaseLibrary = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'savePhaseLibrary',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };
        return {
            getSections: getSections,
            getPhrasesByPhraseById: getPhrasesByPhraseById,
            savePhaseLibrary: savePhaseLibrary,
            getPhraseFiledsBySectionId: getPhraseFiledsBySectionId,
        };
    })();

    return dataService;
});