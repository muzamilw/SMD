define(["ko", "underscore", "underscore-ko"], function (ko) {
  
    var // ReSharper disable InconsistentNaming
      Campaign = function (CampaignID, LanguageID, CampaignName, UserID, Status, StatusValue, CampaignDescription, Gender,
          Archived, StartDateTime, EndDateTime, MaxBudget, Type, DisplayTitle, LandingPageVideoLink, VerifyQuestion,
          Answer1, Answer2, Answer3, CorrectAnswer, AgeRangeStart, AgeRangeEnd, ResultClicks, AmountSpent
          , ImagePath, CampaignImagePath, CampaignTypeImagePath) {
          var
              //type and userID will be set on server sside
              CampaignID = ko.observable(CampaignID),
              LanguageID = ko.observable(LanguageID),
              CampaignName = ko.observable(CampaignName),
              UserID = ko.observable(UserID),
              Status = ko.observable(Status),
              StatusValue = ko.observable(StatusValue),
              CampaignDescription = ko.observable(CampaignDescription),
              Gender = ko.observable(Gender),
              Archived = ko.observable(Archived),
              StartDateTime = ko.observable(StartDateTime),
              EndDateTime = ko.observable(EndDateTime),
              MaxBudget = ko.observable(MaxBudget),
              Type = ko.observable(Type),
              DisplayTitle = ko.observable(DisplayTitle),
              LandingPageVideoLink = ko.observable(LandingPageVideoLink),
              VerifyQuestion = ko.observable(VerifyQuestion),
              Answer1 = ko.observable(Answer1),
              Answer2 = ko.observable(Answer2),
              Answer3 = ko.observable(Answer3),
              CorrectAnswer = ko.observable(CorrectAnswer),
              AgeRangeStart = ko.observable(AgeRangeStart),
              AgeRangeEnd = ko.observable(AgeRangeEnd),
              ResultClicks = ko.observable(ResultClicks),
              AmountSpent = ko.observable(AmountSpent),
              ImagePath = ko.observable(ImagePath),
              CampaignImagePath = ko.observable(CampaignImagePath),
              CampaignTypeImagePath = ko.observable(CampaignTypeImagePath),
              AdCampaignTargetCriterias = ko.observableArray([]),
              AdCampaignTargetLocation = ko.observableArray([]),
              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                  CampaignID: CampaignID,
                  LanguageID: LanguageID,
                  CampaignName: CampaignName,
                  UserID: UserID,
                  Status: Status,
                  StatusValue: StatusValue,
                  CampaignDescription: CampaignDescription,
                  Gender: Gender,
                  Archived: Archived,
                  StartDateTime: StartDateTime,
                  EndDateTime: EndDateTime,
                  MaxBudget: MaxBudget,
                  Type: Type,
                  DisplayTitle: DisplayTitle,
                  LandingPageVideoLink: LandingPageVideoLink,
                  VerifyQuestion: VerifyQuestion,
                  Answer1: Answer1,
                  Answer2: Answer2,
                  Answer3: Answer3,
                  CorrectAnswer: CorrectAnswer,
                  AgeRangeStart: AgeRangeStart,
                  AgeRangeEnd: AgeRangeEnd,
                  ResultClicks: ResultClicks,
                  AmountSpent: AmountSpent,
                  AdCampaignTargetCriterias: AdCampaignTargetCriterias,
                  AdCampaignTargetLocation: AdCampaignTargetLocation
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
                  var targetCriteria = [];
                  _.each(AdCampaignTargetCriterias(), function (item) {
                      targetCriteria.push(item.convertCriteriaToServerData());
                  });
                  var LocationtargetCriteria = [];
                  _.each(AdCampaignTargetLocation(), function (item) {
                      LocationtargetCriteria.push(item.convertToServerData());
                  });
                  return {
                      CampaignID: CampaignID(),
                      LanguageID: LanguageID(),
                      CampaignName: CampaignName(),
                      UserID: UserID(),
                      Status: Status(),
                      StatusValue: StatusValue(),
                      CampaignDescription: CampaignDescription(),
                      Gender: Gender(),
                      Archived: Archived(),
                      StartDateTime: StartDateTime(),
                      EndDateTime: EndDateTime(),
                      MaxBudget: MaxBudget(),
                      Type: Type(),
                      DisplayTitle: DisplayTitle(),
                      LandingPageVideoLink: LandingPageVideoLink(),
                      VerifyQuestion: VerifyQuestion(),
                      Answer1: Answer1(),
                      Answer2: Answer2(),
                      Answer3: Answer3(),
                      CorrectAnswer: CorrectAnswer(),
                      AgeRangeStart: AgeRangeStart(),
                      AgeRangeEnd: AgeRangeEnd(),
                      ResultClicks: ResultClicks(),
                      AmountSpent: AmountSpent(),
                      ImagePath: ImagePath(),
                      CampaignImagePath: CampaignImagePath(),
                      CampaignTypeImagePath: CampaignTypeImagePath(),
                      AdCampaignTargetCriterias: targetCriteria,
                      AdCampaignTargetLocation: LocationtargetCriteria
                  };
              };
          return {
              CampaignID: CampaignID,
              LanguageID: LanguageID,
              CampaignName: CampaignName,
              UserID: UserID,
              Status: Status,
              StatusValue: StatusValue,
              CampaignDescription: CampaignDescription,
              Gender: Gender,
              Archived: Archived,
              StartDateTime: StartDateTime,
              EndDateTime: EndDateTime,
              MaxBudget: MaxBudget,
              Type: Type,
              DisplayTitle: DisplayTitle,
              LandingPageVideoLink: LandingPageVideoLink,
              VerifyQuestion: VerifyQuestion,
              Answer1: Answer1,
              Answer2: Answer2,
              Answer3: Answer3,
              CorrectAnswer: CorrectAnswer,
              AgeRangeStart: AgeRangeStart,
              AgeRangeEnd: AgeRangeEnd,
              ResultClicks: ResultClicks,
              AmountSpent: AmountSpent,
              ImagePath: ImagePath,
              CampaignImagePath: CampaignImagePath,
              CampaignTypeImagePath: CampaignTypeImagePath,
              AdCampaignTargetCriterias: AdCampaignTargetCriterias,
              AdCampaignTargetLocation:AdCampaignTargetLocation,
              convertToServerData:convertToServerData,
              hasChanges: hasChanges,
              reset: reset,
              isValid: isValid,
              dirtyFlag:dirtyFlag,
              errors: errors
          };
      };

    var // ReSharper disable InconsistentNaming
      AdCampaignTargetCriteriasModel = function (CriteriaID, CampaignID, Type, PQID, PQAnswerID, SQID, SQAnswer, IncludeorExclude, questionString,
       answerString, surveyQuestLeftImageSrc, surveyQuestRightImageSrc, LanguageID, Language) {
          var
              //type and userID will be set on server sside
               CriteriaID = ko.observable(CriteriaID),
               CampaignID = ko.observable(CampaignID),
               Type = ko.observable(Type),
               PQID = ko.observable(PQID),
               PQAnswerID = ko.observable(PQAnswerID),
               SQID = ko.observable(SQID),
               SQAnswer = ko.observable(SQAnswer),
               IncludeorExclude = ko.observable(IncludeorExclude),
               questionString = ko.observable(questionString),
               answerString = ko.observable(answerString),
               surveyQuestLeftImageSrc = ko.observable(surveyQuestLeftImageSrc),
               surveyQuestRightImageSrc = ko.observable(surveyQuestRightImageSrc),
               LanguageID = ko.observable(LanguageID),
               Language = ko.observable(Language),
          // Convert to server data
          convertCriteriaToServerData = function () {
           
              return {
                  CriteriaID: CriteriaID(),
                  CampaignID: CampaignID(),
                  Type: Type(),
                  PQID: PQID(),
                  PQAnswerID: PQAnswerID(),
                  SQID: SQID(),
                  SQAnswer: SQAnswer(),
                  IncludeorExclude: IncludeorExclude(),
                  LanguageID: LanguageID(),
                  Language:Language(),
              };
          };
          return {
              CriteriaID: CriteriaID,
              CampaignID: CampaignID,
              Type: Type,
              PQID: PQID,
              PQAnswerID: PQAnswerID,
              SQID: SQID,
              SQAnswer: SQAnswer,
              IncludeorExclude: IncludeorExclude,
              questionString: questionString,
              answerString: answerString,
              surveyQuestLeftImageSrc: surveyQuestLeftImageSrc,
              surveyQuestRightImageSrc: surveyQuestRightImageSrc,
              LanguageID: LanguageID,
              Language:Language,
              convertCriteriaToServerData: convertCriteriaToServerData
          };
      };
    var // ReSharper disable InconsistentNaming
    AdCampaignTargetLocation = function (ID, CampaignID, CountryID, CityID, Radius, Country, City,IncludeorExclude) {
        var
            //type and userID will be set on server sside
            ID = ko.observable(ID),
            CampaignID = ko.observable(CampaignID),
            CountryID = ko.observable(CountryID),
            CityID = ko.observable(CityID),
            Radius = ko.observable(Radius),
            Country = ko.observable(Country),
            City = ko.observable(City),
            IncludeorExclude =ko.observable(IncludeorExclude)
            // Convert to server data
            convertToServerData = function () {
                return {
                    ID: ID(),
                    CampaignID: CampaignID(),
                    CountryID: CountryID(),
                    CityID: CityID(),
                    Radius: Radius(),
                    Country: Country(),
                    City: City(),
                    IncludeorExclude:IncludeorExclude()
                };
            };
        return {
            ID: ID,
            CampaignID: CampaignID,
            CountryID: CountryID,
            CityID: CityID,
            Radius: Radius,
            Country: Country,
            City: City,
            IncludeorExclude:IncludeorExclude,
            convertToServerData: convertToServerData
        };
    };
    // Factory Method
    Campaign.Create = function (source) {
        var campaign = new Campaign(source.CampaignID, source.LanguageID, source.CampaignName, source.UserID, source.Status, source.StatusValue, source.CampaignDescription, source.Gender, source.Archived, source.StartDateTime, source.EndDateTime, source.MaxBudget, source.Type, source.DisplayTitle, source.LandingPageVideoLink, source.VerifyQuestion, source.Answer1, source.Answer2, source.Answer3, source.CorrectAnswer, source.AgeRangeStart, source.AgeRangeEnd, source.ResultClicks, source.AmountSpent, source.ImagePath, source.CampaignImagePath, source.CampaignTypeImagePath);
        _.each(source.AdCampaignTargetCriterias, function (item) {
            campaign.AdCampaignTargetCriterias.push(AdCampaignTargetCriteriasModel.Create(item));
        });
        _.each(source.AdCampaignTargetLocation, function (item) {
            campaign.AdCampaignTargetLocation.push(AdCampaignTargetLocation.Create(item));
        });
        return campaign;
    };
    // Factory Method
    AdCampaignTargetCriteriasModel.Create = function (source) {
        return new AdCampaignTargetCriteriasModel(source.CriteriaID, source.CampaignID, source.Type, source.PQID, source.PQAnswerID, source.SQID, source.SQAnswer, source.IncludeorExclude, source.questionString, source.answerString, source.surveyQuestLeftImageSrc, source.surveyQuestRightImageSrc, source.LanguageID, source.Language);
    };
    AdCampaignTargetLocation.Create = function (source) {
        return new AdCampaignTargetLocation(source.ID, source.CampaignID, source.CountryID, source.CityID, source.Radius, source.Country, source.City,source.IncludeorExclude);
    };
    return {
        Campaign: Campaign,
        AdCampaignTargetCriteriasModel: AdCampaignTargetCriteriasModel,
        AdCampaignTargetLocation: AdCampaignTargetLocation
    };
});