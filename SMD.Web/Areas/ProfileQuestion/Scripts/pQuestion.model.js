define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
        question = function (questionId, spcQuestion, pr,linkQ,gName, langId, countId, groupId, spcType,
        spcRefreshtime, spcSkipped, spcCreationD, spcModDate, penality, spcStatus, CreatedBy, StatusValue, AnswerNeeded, AnswerCount, Gender, AgeRangeStart,AgeRangeEnd) {
            var 
                qId = ko.observable(questionId),
                questionString = ko.observable(spcQuestion).extend({ required: true }),
                priority = ko.observable(pr),
                hasLinkedQuestions = ko.observable(linkQ),
                profileGroupName = ko.observable(gName),
                answerNeeded = ko.observable(AnswerNeeded),
                answerCount = ko.observable(AnswerCount),
                languageId = ko.observable(langId),
                countryId = ko.observable(countId),
                profileGroupId = ko.observable(groupId).extend({ required: true }),
                type = ko.observable(spcType).extend({ required: true }),
                statusValue = ko.observable(StatusValue),
                Gender = ko.observable(Gender),
                AgeRangeStart = ko.observable(AgeRangeStart),
                AgeRangeEnd = ko.observable(AgeRangeEnd).extend({
                    validation: {
                        validator: function (val, someOtherVal) {
                            return val > AgeRangeStart();
                        },
                        message: 'Age end range must be greater than start range',
                    }
                }),
                ProfileQuestionTargetLocation = ko.observableArray([]),
                ProfileQuestionTargetCriteria = ko.observableArray([]),
                refreshTime = ko.observable(spcRefreshtime),
                skippedCount = ko.observable(spcSkipped),
                creationDate = ko.observable(spcCreationD),
                modifiedDate = ko.observable(spcModDate),
                createdBy = ko.observable(CreatedBy),
                penalityForNotAnswering = ko.observable(penality),
                status = ko.observable(spcStatus || 1),
                answers = ko.observableArray([]),
                errors = ko.validation.group({                    
                    questionString: questionString,
                    profileGroupId: profileGroupId,
                    type: type
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
                    ProfileQuestionTargetCriteria: ProfileQuestionTargetCriteria,
                    penalityForNotAnswering: penalityForNotAnswering,
                    status: status,
                    answers: answers,
                    createdBy: createdBy,
                    statusValue: statusValue,
                    answerCount: answerCount,
                    answerNeeded: answerNeeded,
                    AgeRangeStart: AgeRangeStart,
                    AgeRangeEnd: AgeRangeEnd
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
             
             
                convertToServerData = function () {                   
                  var  targetCriteria = [], targetLocation = []
                    
                    _.each(ProfileQuestionTargetLocation(), function (item) {
                           
                                targetLocation.push(item.convertToServerData());
                        });
                    
                    _.each(ProfileQuestionTargetCriteria(), function (item) {
                            targetCriteria.push(item.convertToServerData());
                        });
                    
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
                        Status: status(),
                        Gender: Gender(),
                        AgeRangeStart: AgeRangeStart(),
                        AgeRangeEnd:AgeRangeEnd(),
                        ProfileQuestionTargetLocation: targetLocation,
                        ProfileQuestionTargetCriteria: targetCriteria
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
                createdBy:createdBy,
                hasChanges: hasChanges,
                reset: reset,
                dirtyFlag:dirtyFlag,
                convertToServerData: convertToServerData,
                isValid: isValid,
                statusValue: statusValue,
                answerNeeded: answerNeeded,
                answerCount:answerCount,
                errors: errors,
                ProfileQuestionTargetLocation: ProfileQuestionTargetLocation,
                ProfileQuestionTargetCriteria: ProfileQuestionTargetCriteria,
                Gender: Gender,
                AgeRangeStart: AgeRangeStart,
                AgeRangeEnd: AgeRangeEnd
            };
        };


    var // ReSharper disable InconsistentNaming
      questionAnswer = function (ansString, imgpath, linkQ1, linkQ2, linkQ3, linkQ4, linkQ5, linkQ6, ansId,
      qstId, srtOrder, spcType, ProfleQuestion) {
          debugger;
          var
              linkedQuestion1Id = ko.observable(linkQ1),
              linkedQuestion2Id = ko.observable(linkQ2),
              linkedQuestion3Id = ko.observable(linkQ3),

              linkedQuestion4Id = ko.observable(linkQ4),
              linkedQuestion5Id = ko.observable(linkQ5),
              linkedQuestion6Id = ko.observable(linkQ6),
              
              linkedQustionsCount = ko.observable(0),
              ProfleQuestion = ko.observable(ProfleQuestion),
              question1String = ko.observable(undefined),
              question2String = ko.observable(undefined),
              question3String = ko.observable(undefined),
              question4String = ko.observable(undefined),
              question5String = ko.observable(undefined),
              question6String = ko.observable(undefined),
              resetLinkedQustionsCount = ko.computed(function() {
                  if (!question1String() && !question2String() && !question3String() &&
                      !question4String() && !question5String() && !question6String()) {
                      linkedQustionsCount(0);
                  }    
              }),
              pqAnswerId = ko.observable(ansId),

              pqId = ko.observable(qstId),
              sortOrder = ko.observable(srtOrder),
              type = ko.observable(spcType == 1 ? "1" : "2"),
              answerString = ko.observable(ansString).extend({
                  required: {
                      onlyIf: function () {
                          return type() === 1 || type() === "1";
                      }
                  }
              }),
              imagePath = ko.observable(imgpath).extend({
                  required: {
                      onlyIf: function () {
                          return type() === 2 || type() === "2";
                      }
                  }
              }),
              // Is image required
              isImageRequired = ko.computed(function () {
                  return !imagePath() && (type() === 2 || type() === "2");
              }),
              errors = ko.validation.group({
                  answerString: answerString,
                  imagePath: imagePath
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
                  ProfleQuestion:ProfleQuestion,
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
              ProfleQuestion:ProfleQuestion,
              hasChanges: hasChanges,
              reset: reset,
              convertToServerData: convertToServerData,
              isValid: isValid,
              isImageRequired: isImageRequired,
              errors: errors,
              resetLinkedQustionsCount: resetLinkedQustionsCount
          };
      };


    /////////////////////////////////////////////////////////QUESTION
    //server to client mapper For QUESTION
    var questionServertoClientMapper = function (itemFromServer) {
        
        return new question(itemFromServer.PqId, itemFromServer.Question, itemFromServer.Priority,
            itemFromServer.HasLinkedQuestions, itemFromServer.ProfileGroupName, itemFromServer.LanguageId, itemFromServer.CountryId, itemFromServer.ProfileGroupId, itemFromServer.Type, itemFromServer.RefreshTime
        , itemFromServer.SkippedCount, moment(itemFromServer.CreationDate), itemFromServer.ModifiedDate, itemFromServer.PenalityForNotAnswering, itemFromServer.Status, itemFromServer.CreatedBy, itemFromServer.StatusValue, itemFromServer.AnswerNeeded, itemFromServer.AsnswerCount, itemFromServer.Gender, itemFromServer.AgeRangeStart, itemFromServer.AgeRangeEnd);

        _.each(itemFromServer.ProfileQuestionTargetCriterias, function (item) {
            
            question.ProfileQuestionTargetCriteria.push(ProfileQuestionTargetCriteria.Create(item));
        });
        _.each(itemFromServer.ProfileQuestionTargetLocations, function (item) {
            
            question.ProfileQuestionTargetLocation.push(ProfileQuestionTargetLocations.Create(item));
        });
    };


    var questionServertoClientMapperQuestionAnswer = function (itemFromServer) {
        
        var Question= new question(itemFromServer.PqId, itemFromServer.Question, itemFromServer.Priority,
            itemFromServer.HasLinkedQuestions, itemFromServer.ProfileGroupName, itemFromServer.LanguageId, itemFromServer.CountryId, itemFromServer.ProfileGroupId, itemFromServer.Type, itemFromServer.RefreshTime
        , itemFromServer.SkippedCount, moment(itemFromServer.CreationDate), itemFromServer.ModifiedDate, itemFromServer.PenalityForNotAnswering, itemFromServer.Status, itemFromServer.CreatedBy, itemFromServer.StatusValue, itemFromServer.AnswerNeeded, itemFromServer.AsnswerCount, itemFromServer.Gender, itemFromServer.AgeRangeStart, itemFromServer.AgeRangeEnd);

        _.each(itemFromServer.ProfileQuestionTargetCriteria, function (item) {
           
            Question.ProfileQuestionTargetCriteria.push(ProfileQuestionTargetCriteria.Create(item));

          });
        _.each(itemFromServer.ProfileQuestionTargetLocation, function (item) {
            
            Question.ProfileQuestionTargetLocation.push(ProfileQuestionTargetLocation.Create(item));
        });
        return Question;
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
        , itemFromServer.SortOrder, itemFromServer.Type,itemFromServer.ProfileQuestion);
       //  Setting strings of linked questinos
        obj.question1String(setAnswerString(obj.linkedQuestion1Id(), obj));
        obj.question2String(setAnswerString(obj.linkedQuestion2Id(), obj));
        obj.question3String(setAnswerString(obj.linkedQuestion3Id(), obj));
        obj.question4String(setAnswerString(obj.linkedQuestion4Id(), obj));
        obj.question5String(setAnswerString(obj.linkedQuestion5Id(), obj));
        obj.question6String(setAnswerString(obj.linkedQuestion6Id(), obj));

        obj.ProfleQuestion(questionServertoClientMapperQuestionAnswer(itemFromServer.ProfileQuestion));

        //_.each(obj.ProfileQuestion, function (item) {
        //    item.ProfileQuestionTargetCriteria.push(ProfileQuestionTargetCriteria.Create(item));
        //});
        //_.each(obj.ProfileQuestion, function (item) {
        //    item.ProfileQuestionTargetLocation.push(ProfileQuestionTargetLocations.Create(item));
        //});

        return obj;
    };

    // Function to attain cancel button functionality QUESTION ANSWER
     questionAnswer.CreateFromClientModel = function (item) {
        return new questionAnswer(item.answerString, item.imagePath, item.linkedQuestion1Id,
            item.linkedQuestion2Id, item.linkedQuestion3Id, item.linkedQuestion4Id, item.linkedQuestion5Id, item.linkedQuestion6Id, item.pqAnswerId, item.pqId
        , item.sortOrder, item.type,item.ProfileQuestion);
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
   var ProfileQuestionTargetLocation = function (ID, SQID, CountryID, CityID, Radius, Country, City, IncludeorExclude, Latitude, Longitude, PQID, IsDeleted, ID) {
      
        var
            //type and userID will be set on server sside
           
            ID = ko.observable(ID),
            SQID = ko.observable(SQID),
            CountryID = ko.observable(CountryID),
            CityID = ko.observable(CityID),
            Radius = ko.observable(Radius),

            IsDeleted = ko.observable(IsDeleted),

            Country = ko.observable(Country),
            City = ko.observable(City),
           IncludeorExclude = ko.observable(IncludeorExclude == true ? "1" : "0"),
           Latitude = ko.observable(Latitude),
           Longitude = ko.observable(Longitude),
            PQID = ko.observable(PQID)
            // Convert to server data
            convertToServerData = function () {
                return {
                    Id: ID(),
                    SqId: SQID(),
                    CountryId: CountryID(),
                    CityId: CityID(),
                    Radius: Radius(),
                    Country: Country(),
                    City: City(),
                    IncludeorExclude: IncludeorExclude() == 1 ? true : false,
                    IsDeleted: IsDeleted()
                };
            };
        return {
            ID: ID,
            SQID: SQID,
            CountryID: CountryID,
            CityID: CityID,
            Radius: Radius,
            Country: Country,
            City: City,
            IncludeorExclude: IncludeorExclude,
            convertToServerData: convertToServerData,
            Latitude: Latitude,
            Longitude: Longitude,
            IsDeleted: IsDeleted
        };
  };
  var // ReSharper disable InconsistentNaming
 ProfileQuestionTargetCriteria = function (ID, PQID, Type, PQID, PQAnswerID, LinkedSQID, LinkedSqAnswer, IncludeorExclude, LanguageID, questionString, answerString, Language, IndustryID, Industry, EducationId, Education, AdCampaignAnswer, PQQuestionID, AdCampaignID, PQQuestionString, profileQuestRightImageSrc, profileQuestLeftImageSrc, IsDeleted,ID) {
     
     var
         //type and userID will be set on server side
         ID = ko.observable(ID),
         SQID = ko.observable(SQID),
         Type = ko.observable(Type),
         PQID = ko.observable(PQID),
         LinkedPQId = ko.observable(LinkedPQId),
         PQAnswerID = ko.observable(PQAnswerID),
         
         AdCampaignAnswer = ko.observable(AdCampaignAnswer),
         PQQuestionID = ko.observable(PQQuestionID),
         AdCampaignID = ko.observable(AdCampaignID),
         PQQuestionString = ko.observable(PQQuestionString),

         IsDeleted = ko.observable(IsDeleted),
         ID = ko.observable(ID),
         LinkedSQID = ko.observable(LinkedSQID),
         LinkedSQAnswer = ko.observable(LinkedSqAnswer + ""),
         IncludeorExclude = ko.observable(IncludeorExclude == true ? "1" : "0"),
         LanguageID = ko.observable(LanguageID),
         questionString = ko.observable(questionString),
         answerString = ko.observable(answerString),
         Language = ko.observable(Language),
         
         IndustryID = ko.observable(IndustryID),
         Industry = ko.observable(Industry),
         Education = ko.observable(Education),
         EducationId = ko.observable(EducationId)
         profileQuestLeftImageSrc = ko.observable(profileQuestLeftImageSrc),
         profileQuestRightImageSrc = ko.observable(profileQuestRightImageSrc),


          dirtyFlag = new ko.dirtyFlag({
              ID: ID,
              SQID: SQID,
              Type: Type,
              PQID: PQID,
              PQAnswerID: PQAnswerID,
              LinkedSQID: LinkedSQID,
              LinkedSQAnswer: LinkedSQAnswer,
              IncludeorExclude: IncludeorExclude,
              LanguageID: LanguageID,
              questionString: questionString,
              answerString: answerString,
              Language: Language,
              
              IndustryID: IndustryID,
              Industry: Industry,
              EducationId: EducationId,
              Education: Education,
              AdCampaignAnswer: AdCampaignAnswer,
              PQQuestionID: PQQuestionID,
              AdCampaignID: AdCampaignID,
              profileQuestLeftImageSrc: profileQuestLeftImageSrc,
              profileQuestRightImageSrc: profileQuestRightImageSrc,
              IsDeleted: IsDeleted,
              ID: ID
          }),
     // Has Changes
              hasChanges = ko.computed(function () {
                  return dirtyFlag.isDirty();
              }),
         // Convert to server data
         convertToServerData = function () {
             return {
                 Id: ID(),
                 SqId: SQID(),
                 Type: Type(),
                 PqId: PQID(),
                 PqAnswerId: PQAnswerID(),
                 LinkedSqId: LinkedSQID(),
                 LinkedSqAnswer: LinkedSQAnswer(),
                 IncludeorExclude: IncludeorExclude() == 1 ? true : false,
                
                 LanguageId: LanguageID(),
                 IndustryID: IndustryID(),
                 Industry: Industry(),
                 EducationId: EducationId(),
                 Education: Education(),
                 AdCampaignAnswer: AdCampaignAnswer(),
                 PQQuestionID: PQQuestionID(),
                 AdCampaignID: AdCampaignID(),
                 profileQuestLeftImageSrc: profileQuestLeftImageSrc(),
                 profileQuestRightImageSrc: profileQuestRightImageSrc(),
                 IsDeleted: IsDeleted(),
                 ID: ID()
             };
         };
     return {
         ID: ID,
         SQID: SQID,
         Type: Type,
         PQID: PQID,
         PQAnswerID: PQAnswerID,
         LinkedSQID: LinkedSQID,
         LinkedSQAnswer: LinkedSQAnswer,
         IncludeorExclude: IncludeorExclude,
         LanguageID: LanguageID,
         questionString: questionString,
         answerString: answerString,
         Language: Language,
         convertToServerData: convertToServerData,
         IndustryID: IndustryID,
         Industry: Industry,
         EducationId: EducationId,
         Education: Education,
         AdCampaignAnswer: AdCampaignAnswer,
         PQQuestionID: PQQuestionID,
         AdCampaignID: AdCampaignID,
         profileQuestLeftImageSrc: profileQuestLeftImageSrc,
         profileQuestRightImageSrc: profileQuestRightImageSrc,
         IsDeleted: IsDeleted,
         ID: ID,
         hasChanges: hasChanges
     };
 };

  ProfileQuestionTargetLocation.Create = function (source) {
      
        return new ProfileQuestionTargetLocation(source.Id, source.SqId, source.CountryId, source.CityId, source.Radius,
           source.Country, source.City, source.IncludeorExclude, source.Latitude, source.Longitude, source.pqid, source.IsDeleted,source.ID);
    }
    ProfileQuestionTargetCriteria.Create = function (source) {
        
        return new ProfileQuestionTargetCriteria(source.Id, source.SqId, source.Type, source.PqId, source.PqAnswerId, source.LinkedSqId, source.LinkedSqAnswer, source.IncludeorExclude, source.LanguageId, source.questionString, source.answerString, source.Language, source.IndustryId, source.Industry, source.EducationId, source.Education, source.AdCampaignAnswer, source.PQQuestionID, source.AdCampaignID, source.PQQuestionString, source.profileQuestRightImageSrc,source.profileQuestLeftImageSrc,source.IsDeleted,source.ID);
    };
    return {
        question: question,
        questionServertoClientMapper: questionServertoClientMapper,
        
        questionAnswer: questionAnswer,
        questionAnswerServertoClientMapper: questionAnswerServertoClientMapper,
        setAnswerString: setAnswerString,
        ProfileQuestionTargetLocation: ProfileQuestionTargetLocation,
        ProfileQuestionTargetCriteria: ProfileQuestionTargetCriteria
    };
});