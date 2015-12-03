define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
        question = function (questionId, spcQuestion, pr,linkQ,gName) {
            var 
                qId = ko.observable(questionId),
                questionString = ko.observable(spcQuestion),
                priority = ko.observable(pr),
                hasLinkedQuestions = ko.observable(linkQ),
                profileGroupName = ko.observable(gName),
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
            itemFromServer.HasLinkedQuestions, itemFromServer.ProfileGroupName);
    };
    
    // Function to attain cancel button functionality
    question.CreateFromClientModel = function (item) {
        return new question(item.qId, item.questionString, item.priority, item.hasLinkedQuestions, item.profileGroupName);
    };

    return {
        question: question,
        questionServertoClientMapper: questionServertoClientMapper,
        
        
    };
});