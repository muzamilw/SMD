﻿define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
        Survey = function (SQID, LanguageID, CountryID, UserID, Status, StatusValue, Question, Gender, Language, Country, Description, DisplayQuestion, StartDate, EndDate, ModifiedDate, LeftPicturePath, RightPicturePath, ProjectedReach, AgeRangeStart, AgeRangeEnd, LeftPictureBytes, RightPictureBytes) {
            var
                //type and userID will be set on server sside
                SQID = ko.observable(SQID),
                LanguageID = ko.observable(LanguageID),
                CountryID = ko.observable(CountryID),
                UserID = ko.observable(UserID),
                Status = ko.observable(Status),
                Question = ko.observable(Question),
                Gender = ko.observable(Gender),
                Language = ko.observable(Language),
                Country = ko.observable(Country),
                StatusValue = ko.observable(StatusValue),
                Description = ko.observable(Description),
                DisplayQuestion = ko.observable(DisplayQuestion),
                StartDate = ko.observable(StartDate),
                EndDate = ko.observable(EndDate),
                CreationDate = ko.observable(CreationDate),
                ModifiedDate = ko.observable(ModifiedDate),
                LeftPicturePath = ko.observable(LeftPicturePath),
                RightPicturePath = ko.observable(RightPicturePath),
                ProjectedReach = ko.observable(ProjectedReach),
                AgeRangeStart = ko.observable(AgeRangeStart),
                AgeRangeEnd = ko.observable(AgeRangeEnd),
                DiscountVoucherApplied = ko.observable(DiscountVoucherApplied),
                VoucherCode = ko.observable(VoucherCode),
                DiscountVoucherID = ko.observable(DiscountVoucherID),
                SubmissionDate = ko.observable(SubmissionDate),
                SurveyQuestionTargetCriteria = ko.observableArray([]),
                SurveyQuestionTargetLocation = ko.observableArray([]),
                LeftPictureBytes = ko.observable(LeftPictureBytes),
                RightPictureBytes = ko.observable(RightPictureBytes),
                errors = ko.validation.group({

                    }),
                // Is Valid
                isValid = ko.computed(function () {
                    return errors().length === 0;
                }),
                dirtyFlag = new ko.dirtyFlag({
                    LanguageID: (LanguageID),
                    CountryID: (CountryID),
                    Question: (Question),
                    Gender: (Gender),
                    Language: (Language),
                    Country: (Country),
                    Description: Description,
                    DisplayQuestion: DisplayQuestion,
                    StartDate: StartDate,
                    EndDate: EndDate,
                    ProjectedReach: ProjectedReach,
                    AgeRangeStart: AgeRangeStart,
                    AgeRangeEnd: AgeRangeEnd,
                    VoucherCode: VoucherCode,
                    DiscountVoucherID: DiscountVoucherID,
                    SurveyQuestionTargetCriteria: SurveyQuestionTargetCriteria,
                    SurveyQuestionTargetLocation: SurveyQuestionTargetLocation,
                    LeftPictureBytes: LeftPictureBytes,
                    RightPictureBytes: RightPictureBytes
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
                    debugger;
                    var targetCriteria =[],targetLocation  = [];
                    _.each(SurveyQuestionTargetCriteria(), function (item) {
                        targetCriteria.push(item.convertToServerData());
                    });
                    _.each(SurveyQuestionTargetLocation(), function (item) {
                        targetLocation.push(item.convertToServerData());
                    });
                    return {
                        SQID: SQID(),
                        LanguageId: LanguageID(),
                        CountryId: CountryID(),
                        UserId: UserID(),
                        Status: Status(),
                        StatusValue: StatusValue(),
                        Question: Question(),
                        Gender: Gender(),
                        Language: Language(),
                        Country: Country(),
                        StatusValue : StatusValue(),
                        Description: Description(),
                        DisplayQuestion: DisplayQuestion(),
                        StartDate: StartDate(),
                        EndDate: EndDate(),
                        CreationDate: CreationDate(),
                        ModifiedDate : ModifiedDate(),
                        LeftPicturePath : LeftPicturePath(),
                        RightPicturePath : RightPicturePath(),
                        ProjectedReach: ProjectedReach(),
                        AgeRangeStart: AgeRangeStart(),
                        AgeRangeEnd : AgeRangeEnd(),
                        DiscountVoucherApplied : DiscountVoucherApplied(),
                        VoucherCode : VoucherCode(),
                        DiscountVoucherID : DiscountVoucherID(),
                        SubmissionDate: SubmissionDate(),
                        SurveyQuestionTargetCriterias: targetCriteria,
                        LeftPictureBytes:LeftPictureBytes(),
                        RightPictureBytes: RightPictureBytes(),
                        SurveyQuestionTargetLocations: targetLocation
                    };
                };
            return {
                SQID :(SQID),
                LanguageID :(LanguageID),
                CountryID :(CountryID),
                UserID :(UserID),
                Status: (Status),
                StatusValue: (StatusValue),
                Question :(Question),
                Gender :(Gender),
                Language :(Language),
                Country :(Country),
                hasChanges: hasChanges,
                reset: reset,
                isValid: isValid,
                errors: errors,
                Description :Description,
                DisplayQuestion :DisplayQuestion,
                StartDate :StartDate,
                EndDate :EndDate,
                CreationDate :CreationDate,
                ModifiedDate :ModifiedDate,
                LeftPicturePath :LeftPicturePath,
                RightPicturePath :RightPicturePath,
                ProjectedReach :ProjectedReach,
                AgeRangeStart :AgeRangeStart,
                AgeRangeEnd :AgeRangeEnd,
                DiscountVoucherApplied :DiscountVoucherApplied,
                VoucherCode :VoucherCode,
                DiscountVoucherID :DiscountVoucherID,
                SubmissionDate :SubmissionDate,
                SurveyQuestionTargetCriteria: SurveyQuestionTargetCriteria,
                SurveyQuestionTargetLocation:SurveyQuestionTargetLocation,
                LeftPictureBytes: LeftPictureBytes,
                RightPictureBytes: RightPictureBytes,
                dirtyFlag: dirtyFlag,
                convertToServerData: convertToServerData
            };
        };

    var // ReSharper disable InconsistentNaming
      SurveyQuestionTargetCriteria = function (ID, SQID, Type, PQID, PQAnswerID, LinkedSQID, LinkedSQAnswer, IncludeorExclude, LanguageID, questionString, answerString, Language, surveyQuestLeftImageSrc, surveyQuestRightImageSrc, IndustryID) {
          var
              //type and userID will be set on server sside
              ID = ko.observable(ID),
              SQID = ko.observable(SQID),
              Type = ko.observable(Type),
              PQID = ko.observable(PQID),
              PQAnswerID = ko.observable(PQAnswerID),
              LinkedSQID = ko.observable(LinkedSQID),
              LinkedSQAnswer = ko.observable(LinkedSQAnswer),
              IncludeorExclude = ko.observable(IncludeorExclude),
              LanguageID = ko.observable(LanguageID),
              questionString = ko.observable(questionString),
              answerString = ko.observable(answerString),
              Language = ko.observable(Language),
              surveyQuestLeftImageSrc = ko.observable(surveyQuestLeftImageSrc),
              surveyQuestRightImageSrc = ko.observable(surveyQuestRightImageSrc),
              IndustryID = ko.observable(IndustryID),
              // Convert to server data
              convertToServerData = function () {
                  return {
                      ID:ID(),
                      SqId: SQID(),
                      Type: Type(),
                      PqId: PQID(),
                      PqAnswerId: PQAnswerID(),
                      LinkedSqId: LinkedSQID(),
                      LinkedSqAnswer: LinkedSQAnswer(),
                      IncludeorExclude: IncludeorExclude(),
                      surveyQuestLeftImageSrc: surveyQuestLeftImageSrc(),
                      surveyQuestRightImageSrc: surveyQuestRightImageSrc(),
                      LanguageId: LanguageID,
                      IndustryID: IndustryID
                  };
              };
          return {
              ID :ID,
              SQID :SQID,
              Type :Type,
              PQID :PQID,
              PQAnswerID :PQAnswerID,
              LinkedSQID :LinkedSQID,
              LinkedSQAnswer :LinkedSQAnswer,
              IncludeorExclude :IncludeorExclude,
              LanguageID :LanguageID,
              questionString :questionString,
              answerString :answerString,
              Language: Language,
              surveyQuestLeftImageSrc: surveyQuestLeftImageSrc,
              surveyQuestRightImageSrc: surveyQuestRightImageSrc,
              convertToServerData: convertToServerData,
              IndustryID: IndustryID
          };
      };
    var // ReSharper disable InconsistentNaming
    SurveyQuestionTargetLocation = function (ID, SQID, CountryID, CityID, Radius, Country, City, IncludeorExclude) {
        var
            //type and userID will be set on server sside
            ID = ko.observable(ID),
            SQID = ko.observable(SQID),
            CountryID = ko.observable(CountryID),
            CityID = ko.observable(CityID),
            Radius = ko.observable(Radius),
            Country = ko.observable(Country),
            City = ko.observable(City),
           IncludeorExclude = ko.observable(IncludeorExclude),
            // Convert to server data
            convertToServerData = function () {
                return {
                    ID: ID(),
                    SqId: SQID(),
                    CountryId: CountryID(),
                    CityId: CityID(),
                    Radius: Radius(),
                    Country: Country(),
                    City: City(),
                    IncludeorExclude: IncludeorExclude()
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
            convertToServerData: convertToServerData
        };
    };
    // Factory Method
    Survey.Create = function (source) {
        var survey = new Survey(source.SqId, source.LanguageId, source.CountryId, source.UserId, source.Status, source.StatusValue, source.Question, source.Gender, source.Language, source.Country, source.Description, source.DisplayQuestion, source.StartDate, source.EndDate, source.ModifiedDate, source.LeftPicturePath, source.RightPicturePath, source.ProjectedReach, source.AgeRangeStart, source.AgeRangeEnd, source.LeftPictureBytes, source.RightPictureBytes);
        _.each(source.SurveyQuestionTargetCriteria, function (item) {
            survey.SurveyQuestionTargetCriteria.push(SurveyQuestionTargetCriteria.Create(item));
        });
        _.each(source.SurveyQuestionTargetLocation, function (item) {
            survey.SurveyQuestionTargetLocation.push(SurveyQuestionTargetLocation.Create(item));
        });
        return survey;
    };
    // Factory Method
    SurveyQuestionTargetCriteria.Create = function (source) {
        return new SurveyQuestionTargetCriteria(source.ID, source.SqId, source.Type, source.PqId, source.PqAnswerId, source.LinkedSqId, source.LinkedSQAnswer, source.IncludeorExclude, source.LanguageID, source.questionString, source.answerString, source.Language, source.surveyQuestLeftImageSrc, source.surveyQuestRightImageSrc, source.IndustryID);
    };
    SurveyQuestionTargetLocation.Create = function (source) {
        return new SurveyQuestionTargetLocation(source.ID, source.SqId, source.CountryId, source.CityId, source.Radius, source.Country, source.City, source.IncludeorExclude);
    };
    return {
        Survey: Survey,
        SurveyQuestionTargetLocation: SurveyQuestionTargetLocation,
        SurveyQuestionTargetCriteria:SurveyQuestionTargetCriteria
    };
});