define(["ko", "underscore", "underscore-ko"], function (ko) {

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
                LeftPictureBytes = ko.observable(LeftPictureBytes),
                RightPictureBytes = ko.observable(RightPictureBytes),
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
                    var targetCriteria =[];
                    _.each(SurveyQuestionTargetCriteria(), function (item) {
                        targetCriteria.push(SurveyQuestionTargetCriteria.convertToServerData(item));
                    });
                    return {
                        SQID: SQID(),
                        LanguageID: LanguageID(),
                        CountryID: CountryID(),
                        UserID: UserID(),
                        Status: Status(),
                        StatusValue: StatusValue(),
                        Question: Question(),
                        Gender: Gender(),
                        Language: Language(),
                        Country: Country(),
                        StatusValue : StatusValue(),
                        Description : Description(),
                        DisplayQuestion : DisplayQuestion(),
                        StartDate : StartDate(),
                        EndDate : EndDate(),
                        CreationDate : CreationDate(),
                        ModifiedDate : ModifiedDate(),
                        LeftPicturePath : LeftPicturePath(),
                        RightPicturePath : RightPicturePath(),
                        ProjectedReach : ProjectedReach(),
                        AgeRangeStart : AgeRangeStart(),
                        AgeRangeEnd : AgeRangeEnd(),
                        DiscountVoucherApplied : DiscountVoucherApplied(),
                        VoucherCode : VoucherCode(),
                        DiscountVoucherID : DiscountVoucherID(),
                        SubmissionDate: SubmissionDate(),
                        SurveyQuestionTargetCriteria: targetCriteria,
                        LeftPictureBytes:LeftPictureBytes(),
                        RightPictureBytes: RightPictureBytes()
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
                LeftPictureBytes: LeftPictureBytes,
                RightPictureBytes: RightPictureBytes
            };
        };

   
    // Factory Method
    Survey.Create = function (source) {
        var survey = new Survey(source.SQID, source.LanguageID, source.CountryID, source.UserID, source.Status, source.StatusValue, source.Question, source.Gender, source.Language, source.Country, source.Description, source.DisplayQuestion, source.StartDate, source.EndDate, source.ModifiedDate, source.LeftPicturePath, source.RightPicturePath, source.ProjectedReach, source.AgeRangeStart, source.AgeRangeEnd, source.LeftPictureBytes, source.RightPictureBytes);
        _.each(source.SurveyQuestionTargetCriteria, function (item) {
            survey.SurveyQuestionTargetCriteria.push(SurveyQuestionTargetCriteria.Create(item));
        });
        return survey;
    };
    var // ReSharper disable InconsistentNaming
      SurveyQuestionTargetCriteria = function (ID, SQID, Type, PQID, PQAnswerID, LinkedSQID, LinkedSQAnswer, IncludeorExclude, LanguageID, PQuestion, PQAnswer, LinkedSQ, LinkedSQImage) {
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
              PQuestion = ko.observable(PQuestion),
              PQAnswer = ko.observable(PQAnswer),
              LinkedSQ = ko.observable(LinkedSQ),
              LinkedSQImage = ko.observable(LinkedSQImage),
              // Convert to server data
              convertToServerData = function () {
                  return {
                      ID:ID(),
                      SQID: SQID(),
                      Type: Type(),
                      PQID: PQID(),
                      PQAnswerID: PQAnswerID(),
                      LinkedSQID: LinkedSQID(),
                      IncludeorExclude: IncludeorExclude()
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
              PQuestion :PQuestion,
              PQAnswer :PQAnswer,
              LinkedSQ :LinkedSQ,
              LinkedSQImage :LinkedSQImage,
          };
      };


    // Factory Method
    SurveyQuestionTargetCriteria.Create = function (source) {
        return new SurveyQuestionTargetCriteria(source.ID, source.SQID, source.Type, source.PQID, source.PQAnswerID, source.LinkedSQID, source.LinkedSQAnswer, source.IncludeorExclude, source.LanguageID, source.PQuestion, source.PQAnswer, source.LinkedSQ, source.LinkedSQImage);
    };
    return {
        Survey: Survey
       
    };
});