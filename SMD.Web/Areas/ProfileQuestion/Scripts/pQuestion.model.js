define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
        question = function (questionId, spcQuestion) {
            var 
                qId = ko.observable(questionId),
                questionString = ko.observable(spcQuestion),
              
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
                        
                    };
                };
            return {
                qId: qId,
                questionString: questionString,
               
                hasChanges: hasChanges,
                reset: reset,
                convertToServerData: convertToServerData,
                isValid: isValid,
                errors: errors
            };
        };

    //server to client mapper For Hire Group Category 
    var questionServertoClientMapper = function (itemFromServer) {
        return new question(itemFromServer.PqId, itemFromServer.Question);
    };
    
    // Function to attain cancel button functionality
    question.CreateFromClientModel = function (item) {
        return new question(item.qId, item.questionString);
    };

    return {
        question: question,
        questionServertoClientMapper: questionServertoClientMapper,
        
        
    };
});