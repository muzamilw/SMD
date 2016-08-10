/*
    Data service module with ajax calls to the server
*/
define("PhraseLibrary/phraseLibrary.dataService", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {

                if (!isInitialized) {

                    isInitialized = true;
                    amplify.request.define('getSections', 'ajax', {
                        url: '/Api/PhaseLibrary',
                        dataType: 'json',
                        type: 'GET'
                    });

                    amplify.request.define('getPhraseBySectionID', 'ajax', {
                        url: '/Api/PhaseLibrary',
                        dataType: 'json',
                        type: 'GET'
                    });

                    amplify.request.define('savePhaseLibrary', 'ajax', {
                        url:'/Api/PhaseLibrary',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });
                }
            };

        getSections = function (params, callbacks) {
            initialize();

            return amplify.request({
                resourceId: 'getSections',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        },
        getPhraseBySectionID = function (params, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'getPhraseBySectionID',
                data: params,
                success: callbacks.success,
                error: callbacks.error,
            });
        }
        savePhaseLibrary = function (param, callbacks) {
            initialize();
            return amplify.request({
                resourceId: 'savePhaseLibrary',
                success: callbacks.success,
                error: callbacks.error,
                data: param,
            });
        };
        return {
            getSections: getSections,
            getPhraseBySectionID: getPhraseBySectionID,
            savePhaseLibrary: savePhaseLibrary
        };
    })();
    return dataService;
});