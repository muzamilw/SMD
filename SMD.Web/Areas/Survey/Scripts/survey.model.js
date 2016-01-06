define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
        Survey = function (SQID, LanguageID, CountryID, UserID, Status, StatusValue, Question, Gender, Language, Country,
            Description, DisplayQuestion, StartDate, EndDate, ModifiedDate, LeftPicturePath, RightPicturePath, ProjectedReach, AgeRangeStart,
            AgeRangeEnd, LeftPictureBytes, RightPictureBytes, ParentSurveyId, Priority) {
           
            var
                //type and userID will be set on server sside
                SQID = ko.observable(SQID),
                LanguageID = ko.observable(LanguageID),
                CountryID = ko.observable(CountryID),
                UserID = ko.observable(UserID),
                Status = ko.observable(Status),
                Question = ko.observable(Question).extend({  // custom message
                    required: true
                }),
                Gender = ko.observable(Gender),
                Language = ko.observable(Language),
                Country = ko.observable(Country),
                StatusValue = ko.observable(StatusValue),
                Description = ko.observable(Description),
                DisplayQuestion = ko.observable(DisplayQuestion).extend({  // custom message
                    required: true
                }),
                StartDate = ko.observable((StartDate !== null && StartDate !== undefined) ? moment(StartDate).toDate() : undefined).extend({  // custom message
                    required: true
                }),
                EndDate = ko.observable((EndDate !== null && EndDate !== undefined) ? moment(EndDate).toDate() : undefined).extend({  // custom message
                    required: true,
                }).extend({
                    validation: {
                        validator: function (val, someOtherVal) {

                            return moment(val).toDate() > moment(StartDate()).toDate();
                        },
                        message: 'End date must be greater than start date',
                    }
                }),
                CreationDate = ko.observable(CreationDate),
                ModifiedDate = ko.observable(ModifiedDate),
                LeftPicturePath = ko.observable(LeftPicturePath),
                RightPicturePath = ko.observable(RightPicturePath),
                ProjectedReach = ko.observable(ProjectedReach),
                AgeRangeStart = ko.observable(AgeRangeStart),
                AgeRangeEnd = ko.observable(AgeRangeEnd).extend({
                    validation: {
                        validator: function (val, someOtherVal) {
                            return val > AgeRangeStart();
                        },
                        message: 'Age end range must be greater than start range',
                    }
                }),
                DiscountVoucherApplied = ko.observable(DiscountVoucherApplied),
                VoucherCode = ko.observable(VoucherCode),
                DiscountVoucherID = ko.observable(DiscountVoucherID),
                SubmissionDate = ko.observable(SubmissionDate),
                SurveyQuestionTargetCriteria = ko.observableArray([]),
                SurveyQuestionTargetLocation = ko.observableArray([]),
                LeftPictureBytes = ko.observable(LeftPictureBytes),
                RightPictureBytes = ko.observable(RightPictureBytes),
                ParentSurveyId = ko.observable(ParentSurveyId),
                Priority = ko.observable(Priority),
                errors = ko.validation.group({
                    Question: Question,
                    DisplayQuestion: DisplayQuestion,
                    StartDate: StartDate,
                    EndDate: EndDate,
                    AgeRangeEnd: AgeRangeEnd,
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
                    RightPictureBytes: RightPictureBytes,
                    ParentSurveyId: ParentSurveyId,
                    Priority: Priority
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
                        StartDate: moment(StartDate()).format(ist.utcFormat) + 'Z',
                        EndDate: moment(EndDate()).format(ist.utcFormat) + 'Z',
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
                        SurveyQuestionTargetLocations: targetLocation,
                        ParentSurveyId: ParentSurveyId(),
                        Priority: Priority()
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
                convertToServerData: convertToServerData,
                ParentSurveyId: ParentSurveyId,
                Priority: Priority
            };
        };

    var // ReSharper disable InconsistentNaming
      SurveyQuestionTargetCriteria = function (ID, SQID, Type, PQID, PQAnswerID, LinkedSQID, LinkedSqAnswer, IncludeorExclude, LanguageID, questionString, answerString, Language, surveyQuestLeftImageSrc, surveyQuestRightImageSrc, IndustryID, Industry, EducationId, Education) {
          var
              //type and userID will be set on server side
              ID = ko.observable(ID),
              SQID = ko.observable(SQID),
              Type = ko.observable(Type),
              PQID = ko.observable(PQID),
              PQAnswerID = ko.observable(PQAnswerID),
              LinkedSQID = ko.observable(LinkedSQID),
              LinkedSQAnswer = ko.observable(LinkedSqAnswer + "" ),
              IncludeorExclude = ko.observable(IncludeorExclude == true?"1":"0"),
              LanguageID = ko.observable(LanguageID),
              questionString = ko.observable(questionString),
              answerString = ko.observable(answerString),
              Language = ko.observable(Language),
              surveyQuestLeftImageSrc = ko.observable(surveyQuestLeftImageSrc),
              surveyQuestRightImageSrc = ko.observable(surveyQuestRightImageSrc),
              IndustryID = ko.observable(IndustryID),
              Industry = ko.observable(Industry),
              Education = ko.observable(Education),
              EducationId = ko.observable(EducationId),
              // Convert to server data
              convertToServerData = function () {
                  return {
                      Id:ID(),
                      SqId: SQID(),
                      Type: Type(),
                      PqId: PQID(),
                      PqAnswerId: PQAnswerID(),
                      LinkedSqId: LinkedSQID(),
                      LinkedSqAnswer: LinkedSQAnswer(),
                      IncludeorExclude: IncludeorExclude() == 1 ? true : false,
                      surveyQuestLeftImageSrc: surveyQuestLeftImageSrc(),
                      surveyQuestRightImageSrc: surveyQuestRightImageSrc(),
                      LanguageId: LanguageID(),
                      IndustryID: IndustryID(),
                      Industry: Industry(),
                      EducationId: EducationId(),
                      Education: Education()
                  };
              };
          return {
              ID :ID,
              SQID :SQID,
              Type :Type,
              PQID :PQID,
              PQAnswerID: PQAnswerID,
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
              IndustryID: IndustryID,
              Industry: Industry,
              EducationId: EducationId,
              Education: Education
          };
      };
    var // ReSharper disable InconsistentNaming
    SurveyQuestionTargetLocation = function (ID, SQID, CountryID, CityID, Radius, Country, City, IncludeorExclude, Latitude, Longitude) {
        var
            //type and userID will be set on server sside
            ID = ko.observable(ID),
            SQID = ko.observable(SQID),
            CountryID = ko.observable(CountryID),
            CityID = ko.observable(CityID),
            Radius = ko.observable(Radius),
            Country = ko.observable(Country),
            City = ko.observable(City),
           IncludeorExclude = ko.observable(IncludeorExclude == true ? "1" : "0"),
           Latitude = ko.observable(Latitude),
           Longitude = ko.observable(Longitude),
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
                    IncludeorExclude: IncludeorExclude() == 1 ? true:false
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
            Longitude: Longitude
        };
    };
    // Factory Method
    Survey.Create = function (source) {
        var survey = new Survey(source.SqId, source.LanguageId, source.CountryId, source.UserId, source.Status, source.StatusValue, source.Question,
            source.Gender + "", source.Language, source.Country, source.Description, source.DisplayQuestion, source.StartDate, source.EndDate, source.ModifiedDate,
            source.LeftPicturePath, source.RightPicturePath, source.ProjectedReach, source.AgeRangeStart, source.AgeRangeEnd, source.LeftPictureBytes,
            source.RightPictureBytes, source.ParentSurveyId, source.Priority);
        _.each(source.SurveyQuestionTargetCriterias, function (item) {
            survey.SurveyQuestionTargetCriteria.push(SurveyQuestionTargetCriteria.Create(item));
        });
        _.each(source.SurveyQuestionTargetLocations, function (item) {
            survey.SurveyQuestionTargetLocation.push(SurveyQuestionTargetLocation.Create(item));
        });
        return survey;
    };
    // Factory Method
    SurveyQuestionTargetCriteria.Create = function (source) {
        return new SurveyQuestionTargetCriteria(source.Id, source.SqId, source.Type, source.PqId, source.PqAnswerId, source.LinkedSqId, source.LinkedSqAnswer, source.IncludeorExclude, source.LanguageId, source.questionString, source.answerString, source.Language, source.surveyQuestLeftImageSrc, source.surveyQuestRightImageSrc, source.IndustryId, source.Industry,source.EducationId, source.Education);
    };
    SurveyQuestionTargetLocation.Create = function (source) {
        return new SurveyQuestionTargetLocation(source.Id, source.SqId, source.CountryId, source.CityId, source.Radius,
            source.Country, source.City, source.IncludeorExclude, source.Latitude, source.Longitude);
    };
    return {
        Survey: Survey,
        SurveyQuestionTargetLocation: SurveyQuestionTargetLocation,
        SurveyQuestionTargetCriteria:SurveyQuestionTargetCriteria
    };
});