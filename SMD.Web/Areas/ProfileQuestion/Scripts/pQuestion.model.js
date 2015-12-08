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
                status = ko.observable(spcStatus || 1),
                answers = ko.observableArray([]),

                errors = ko.validation.group({                    
                    
                }),
                // Is Valid
                isValid = ko.computed(function() {
                    return errors().length === 0;
                }),
                dirtyFlag = new ko.dirtyFlag({
                    questionString: questionString,
                    priority: priority,
                    hasLinkedQuestions: hasLinkedQuestions,
                    profileGroupName: profileGroupName,

                    languageId: languageId,
                    countryId: countryId,
                    profileGroupId: profileGroupId,
                    type: type,

                    refreshTime: refreshTime,
                    skippedCount: skippedCount,
                    creationDate: creationDate,
                    modifiedDate: modifiedDate,

                    penalityForNotAnswering: penalityForNotAnswering,
                    status: status,
                    answers: answers
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
                        ProfileQuestionAnswers: null,
                        LanguageId: languageId(),
                        CountryId: countryId(),
                        ProfileGroupId: profileGroupId(),
                        Type: type(),
                        RefreshTime: refreshTime(),
                        SkippedCount: skippedCount(),
                        PenalityForNotAnswering: penalityForNotAnswering(),
                        Status: status()
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
                dirtyFlag:dirtyFlag,
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
              
              linkedQustionsCount = ko.observable(0),
              
              question1String = ko.observable(undefined),
              question2String = ko.observable(undefined),
              question3String = ko.observable(undefined),
              question4String = ko.observable(undefined),
              question5String = ko.observable(undefined),
              question6String = ko.observable(undefined),
              pqAnswerId = ko.observable(ansId),

              pqId = ko.observable(qstId),
              sortOrder = ko.observable(srtOrder),
              type = ko.observable(spcType == 1 ? "1" : "2"),


              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                  answerString: answerString,
                  imagePath: imagePath,
                  linkedQuestion1Id: linkedQuestion1Id,
                  linkedQuestion2Id: linkedQuestion2Id,
                  linkedQuestion3Id: linkedQuestion3Id,

                  linkedQuestion4Id: linkedQuestion4Id,
                  linkedQuestion5Id: linkedQuestion5Id,
                  linkedQuestion6Id: linkedQuestion6Id,
                  pqAnswerId: pqAnswerId,

                  pqId: pqId,
                  sortOrder: sortOrder,
                  type: type,
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
           
              question1String : question1String,
              question2String : question2String,
              question3String : question3String,
              question4String : question4String,
              question5String : question5String,
              question6String : question6String,
              linkedQustionsCount: linkedQustionsCount,
              
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
        , itemFromServer.SkippedCount, moment(itemFromServer.CreationDate), itemFromServer.ModifiedDate, itemFromServer.PenalityForNotAnswering, itemFromServer.Status);
    };
    
    // Function to attain cancel button functionality QUESTION
    question.CreateFromClientModel = function (item) {
        return new question(item.qId, item.questionString, item.priority, item.hasLinkedQuestions, item.profileGroupName, item.languageId, item.countryId, item.profileGroupId, item.type, item.refreshTime
        , item.skippedCount, item.creationDate);
    };
    //////////////////////////////////////////////  QUESTION ANSWER  

    //server to client mapper For QUESTION ANSWER 
    var questionAnswerServertoClientMapper = function (itemFromServer) {
        var obj= new questionAnswer(itemFromServer.AnswerString, itemFromServer.ImagePath, itemFromServer.LinkedQuestion1Id,
            itemFromServer.LinkedQuestion2Id, itemFromServer.LinkedQuestion3Id, itemFromServer.LinkedQuestion4Id, itemFromServer.LinkedQuestion5Id, itemFromServer.LinkedQuestion6Id, itemFromServer.PqAnswerId, itemFromServer.PqId
        , itemFromServer.SortOrder, itemFromServer.Type);
       //  Setting strings of linked questinos
        obj.question1String(setAnswerString(obj.linkedQuestion1Id(), obj));
        obj.question2String(setAnswerString(obj.linkedQuestion2Id(), obj));
        obj.question3String(setAnswerString(obj.linkedQuestion3Id(), obj));
        obj.question4String(setAnswerString(obj.linkedQuestion4Id(), obj));
        obj.question5String(setAnswerString(obj.linkedQuestion5Id(), obj));
        obj.question6String(setAnswerString(obj.linkedQuestion6Id(), obj));
        return obj;
    };

    // Function to attain cancel button functionality QUESTION ANSWER
    questionAnswer.CreateFromClientModel = function (item) {
        return new questionAnswer(item.answerString, item.imagePath, item.linkedQuestion1Id,
            item.linkedQuestion2Id, item.linkedQuestion3Id, item.linkedQuestion4Id, item.linkedQuestion5Id, item.linkedQuestion6Id, item.pqAnswerId, item.pqId
        , item.sortOrder, item.type);
    };

    // Sets answer string of linked questions grid
    var setAnswerString = function (id, obj) {
        if ( id == null  )
            return undefined;
        var qst = ist.ProfileQuestion.viewModel.linkedQuestions.find(function (temp) {
            return temp.PqId == id;
        });
        if (qst) {
            obj.linkedQustionsCount(obj.linkedQustionsCount()+1);
            return qst.Question;
        }
        else
            return undefined;
    };
    return {
        question: question,
        questionServertoClientMapper: questionServertoClientMapper,
        
        questionAnswer: questionAnswer,
        questionAnswerServertoClientMapper: questionAnswerServertoClientMapper,
        setAnswerString: setAnswerString
    };
});