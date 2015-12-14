define(["ko", "underscore", "underscore-ko"], function (ko) {
    var AdvertGridModel = function () {
        var
        self,
       CampaignId = ko.observable(),
                DisplayTitle = ko.observable(),
                Status = ko.observable(),
                StartDateTime = ko.observable(),
                EndDateTime = ko.observable(),
                MaxBudget = ko.observable(),
                ResultClicks = ko.observable(),
                AmountSpent = ko.observable(),
                StatusName = ko.observable(),
                StatusColor = ko.observable(),
        self = {
            CampaignId: CampaignId,
            DisplayTitle: DisplayTitle,
            Status: Status,
            StartDateTime: StartDateTime,
            EndDateTime: EndDateTime,
            MaxBudget: MaxBudget,
            ResultClicks: ResultClicks,
            AmountSpent: AmountSpent,
            StatusName: StatusName,
            StatusColor: StatusColor
        };
        return self;
    };
    var campaignModel = function (source) {

        if (source != undefined) {

            var
                   CampaignID = ko.observable(source.CampaignID),
                   LanguageID = ko.observable(source.LanguageID),
                   CampaignName = ko.observable(source.CampaignName),
                   CampaignDescription = ko.observable(source.CampaignDescription),
                   Archived = ko.observable(source.Archived),
                   StartDateTime = ko.observable(source.StartDateTime),
                   EndDateTime = ko.observable(source.EndDateTime),
                   MaxBudget = ko.observable(source.MaxBudget),
                   Type = ko.observable(source.Type),
                   DisplayTitle = ko.observable(source.DisplayTitle),
                   LandingPageVideoLink = ko.observable(source.LandingPageVideoLink),
                   VerifyQuestion = ko.observable(source.VerifyQuestion),
                   Answer1 = ko.observable(source.Answer1),
                   Answer2 = ko.observable(source.Answer2),
                   Answer3 = ko.observable(source.Answer3),
                   CorrectAnswer = ko.observable(source.CorrectAnswer),
                   AgeRangeStart = ko.observable(source.AgeRangeStart),
                   AgeRangeEnd = ko.observable(source.AgeRangeEnd),
                   Gender = ko.observable(source.Gender)

        } else {
            var
                   CampaignID = ko.observable(),
                   LanguageID = ko.observable(),
                   CampaignName = ko.observable(),
                   CampaignDescription = ko.observable(),
                   Archived = ko.observable(),
                   StartDateTime = ko.observable(),
                   EndDateTime = ko.observable(),
                   MaxBudget = ko.observable(),
                   Type = ko.observable(),
                   DisplayTitle = ko.observable(),
                   LandingPageVideoLink = ko.observable(),
                   VerifyQuestion = ko.observable(),
                   Answer1 = ko.observable(),
                   Answer2 = ko.observable(),
                   Answer3 = ko.observable(),
                   CorrectAnswer = ko.observable(),
                   AgeRangeStart = ko.observable(),
                   AgeRangeEnd = ko.observable(),
                   Gender = ko.observable()

        }

        convertToServerData = function () {
            return {
                CampaignID: CampaignID(),
                LanguageID: LanguageID(),
                CampaignName: CampaignName(),
                CampaignDescription: CampaignDescription(),
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
                Gender: Gender()
            };
        };
        self = {
            CampaignID: CampaignID,
            LanguageID: LanguageID,
            CampaignName: CampaignName,
            CampaignDescription: CampaignDescription,
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
            Gender: Gender,
            convertToServerData: convertToServerData
        };
        return self;
    };
    var CriteriaModel = function (CriteriaID, Type, PQID, PQAnswerID, SQID, SQAnswer, IncludeorExclude, questionString,
       answerString, surveyQuestLeftImageSrc, surveyQuestRightImageSrc) {
           var
               CriteriaID = ko.observable(CriteriaID),
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
               // Convert to server data
               convertToServerData = function () {
                   return {
                       CriteriaID: CriteriaID(),
                       Type: Type(),
                       PQID: PQID(),
                       PQAnswerID: PQAnswerID(),
                       SQID: SQID(),
                       SQAnswer: SQAnswer(),
                       IncludeorExclude: IncludeorExclude(),
                       questionString: questionString(),
                       answerString: answerString(),
                       surveyQuestLeftImageSrc: answerString(),
                       surveyQuestRightImageSrc: answerString()
                   };
               };
           return {
               CriteriaID: CriteriaID,
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
               convertToServerData: convertToServerData
           };
    };

   
    return {
        AdvertGridModel: AdvertGridModel,
        campaignModel: campaignModel,
        CriteriaModel: CriteriaModel
    };
});