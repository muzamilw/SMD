define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
        question = function (questionId, spcQuestion, pr,linkQ,gName, langId, countId, groupId, spcType,
        spcRefreshtime,spcSkipped, spcCreationD, spcModDate,penality,spcStatus) {
            var 
                qId = ko.observable(questionId),
                questionString = ko.observable(spcQuestion),
                priority = ko.observable(pr),
                hasLinkedQuestions = ko.observable(linkQ),
                profileGroupName = ko.observable(gName),
                
                languageId = ko.observable(langId),
                countryId = ko.observable(countId),
                profileGroupId = ko.observable(groupId),
                type = ko.observable(spcType),

                refreshTime = ko.observable(spcRefreshtime),
                skippedCount = ko.observable(spcSkipped),
                creationDate = ko.observable(spcCreationD),
                modifiedDate = ko.observable(spcModDate),
                
                penalityForNotAnswering = ko.observable(penality),
                status = ko.observable(spcStatus),
                
                errors = ko.validation.group({                    
                    
                }),
                // Is Valid
                isValid = ko.computed(function() {
                    return errors().length === 0;
                }),
                dirtyFlag = new ko.dirtyFlag({
                   
                }),
                // Has Changes
                hasChanges = ko.computed(function() {
                    return dirtyFlag.isDirty();
                }),
                // Reset
                reset = function() {
                    dirtyFlag.reset();
                },
                // Convert to server data
                convertToServerData = function() {
                    return {
                        PqId:qId(),
                        Question:questionString(),
                        Priority:priority(),
                        HasLinkedQuestions:hasLinkedQuestions(),
                        ProfileGroupName: profileGroupName()
                    };
                };
            return {
                qId: qId,
                questionString: questionString,
                priority:priority,
                hasLinkedQuestions: hasLinkedQuestions,
                profileGroupName: profileGroupName,
                
                languageId :languageId,
                countryId :countryId,
                profileGroupId :profileGroupId,
                type :type,

                refreshTime :refreshTime,
                skippedCount:skippedCount,
                creationDate :creationDate,
                modifiedDate :modifiedDate,
                
                penalityForNotAnswering :penalityForNotAnswering,
                status: status,

                hasChanges: hasChanges,
                reset: reset,
                convertToServerData: convertToServerData,
                isValid: isValid,
                errors: errors
            };
        };

    //server to client mapper For Hire Group Category 
    var questionServertoClientMapper = function (itemFromServer) {
        return new question(itemFromServer.PqId, itemFromServer.Question, itemFromServer.Priority,
            itemFromServer.HasLinkedQuestions, itemFromServer.ProfileGroupName, itemFromServer.LanguageId, itemFromServer.CountryId, itemFromServer.ProfileGroupId, itemFromServer.Type, itemFromServer.RefreshTime
        , itemFromServer.SkippedCount, itemFromServer.CreationDate, itemFromServer.ModifiedDate, itemFromServer.PenalityForNotAnswering, itemFromServer.Status);
    };
    
    // Function to attain cancel button functionality
    question.CreateFromClientModel = function (item) {
        return new question(item.qId, item.questionString, item.priority, item.hasLinkedQuestions, item.profileGroupName, item.languageId, item.countryId, item.profileGroupId, item.type, item.refreshTime
        , item.skippedCount, item.creationDate, item.modifiedDate, item.penalityForNotAnswering, item.status);
    };

    return {
        question: question,
        questionServertoClientMapper: questionServertoClientMapper,
        
        
    };
});