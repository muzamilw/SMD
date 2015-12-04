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
                answers = ko.observableArray([]),

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
                answers: answers,

                hasChanges: hasChanges,
                reset: reset,
                convertToServerData: convertToServerData,
                isValid: isValid,
                errors: errors
            };
        };


    var // ReSharper disable InconsistentNaming
      questionAnswer = function (ansString, imgpath, linkQ1, linkQ2, linkQ3, linkQ4, linkQ5, linkQ6, ansId,
      qstId, srtOrder, spcType) {
          var
              answerString = ko.observable(ansString),
              imagePath = ko.observable(imgpath),
              linkedQuestion1Id = ko.observable(linkQ1),
              linkedQuestion2Id = ko.observable(linkQ2),
              linkedQuestion3Id = ko.observable(linkQ3),

              linkedQuestion4Id = ko.observable(linkQ4),
              linkedQuestion5Id = ko.observable(linkQ5),
              linkedQuestion6Id = ko.observable(linkQ6),
              pqAnswerId = ko.observable(ansId),

              pqId = ko.observable(qstId),
              sortOrder = ko.observable(srtOrder),
              type = ko.observable(spcType || "1"),


              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({

              }),
              // Has Changes
              hasChanges = ko.computed(function () {
                  return dirtyFlag.isDirty();
              }),
              // Reset
              reset = function () {
                  dirtyFlag.reset();
              },
              // Convert to server data
              convertToServerData = function () {
                  return {
                      AnswerString: answerString(),
                      ImagePath: imagePath(),
                      LinkedQuestion1Id: linkedQuestion1Id(),
                      LinkedQuestion2Id: linkedQuestion2Id(),
                      LinkedQuestion3Id: linkedQuestion3Id(),

                      LinkedQuestion4Id: linkedQuestion4Id(),
                      LinkedQuestion5Id: linkedQuestion5Id(),
                      LinkedQuestion6Id: linkedQuestion6Id(),
                      PqAnswerId: pqAnswerId(),

                      PqId: pqId(),
                      SortOrder: sortOrder(),
                      Type: type(),
                  };
              };
          return {
              answerString :answerString,
              imagePath :imagePath,
              linkedQuestion1Id :linkedQuestion1Id,
              linkedQuestion2Id :linkedQuestion2Id,
              linkedQuestion3Id :linkedQuestion3Id,

              linkedQuestion4Id :linkedQuestion4Id,
              linkedQuestion5Id :linkedQuestion5Id,
              linkedQuestion6Id :linkedQuestion6Id,
              pqAnswerId :pqAnswerId,

              pqId :pqId,
              sortOrder :sortOrder,
              type: type,
           
              hasChanges: hasChanges,
              reset: reset,
              convertToServerData: convertToServerData,
              isValid: isValid,
              errors: errors
          };
      };


    /////////////////////////////////////////////////////////QUESTION
    //server to client mapper For QUESTION
    var questionServertoClientMapper = function (itemFromServer) {
        return new question(itemFromServer.PqId, itemFromServer.Question, itemFromServer.Priority,
            itemFromServer.HasLinkedQuestions, itemFromServer.ProfileGroupName, itemFromServer.LanguageId, itemFromServer.CountryId, itemFromServer.ProfileGroupId, itemFromServer.Type, itemFromServer.RefreshTime
        , itemFromServer.SkippedCount, itemFromServer.CreationDate);
    };
    
    // Function to attain cancel button functionality QUESTION
    question.CreateFromClientModel = function (item) {
        return new question(item.qId, item.questionString, item.priority, item.hasLinkedQuestions, item.profileGroupName, item.languageId, item.countryId, item.profileGroupId, item.type, item.refreshTime
        , item.skippedCount, item.creationDate);
    };
    //////////////////////////////////////////////  QUESTION ANSWER  

    //server to client mapper For QUESTION ANSWER 
    var questionAnswerServertoClientMapper = function (itemFromServer) {
        return new questionAnswer(itemFromServer.AnswerString, itemFromServer.ImagePath, itemFromServer.LinkedQuestion1Id,
            itemFromServer.LinkedQuestion2Id, itemFromServer.LinkedQuestion3Id, itemFromServer.LinkedQuestion4Id, itemFromServer.LinkedQuestion5Id, itemFromServer.LinkedQuestion6Id, itemFromServer.PqAnswerId, itemFromServer.PqId
        , itemFromServer.SortOrder, itemFromServer.Type);
    };

    // Function to attain cancel button functionality QUESTION ANSWER
    questionAnswer.CreateFromClientModel = function (item) {
        return new questionAnswer(item.answerString, item.imagePath, item.linkedQuestion1Id,
            item.linkedQuestion2Id, item.linkedQuestion3Id, item.linkedQuestion4Id, item.linkedQuestion5Id, item.linkedQuestion6Id, item.pqAnswerId, item.pqId
        , item.sortOrder, item.type);
    };


    return {
        question: question,
        questionServertoClientMapper: questionServertoClientMapper,
        
        questionAnswer: questionAnswer,
        questionAnswerServertoClientMapper: questionAnswerServertoClientMapper
    };
});