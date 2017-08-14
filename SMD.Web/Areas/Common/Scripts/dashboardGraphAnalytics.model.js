define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      Entity = function (CampaignID, CouponID, PQID, Question, CampaignName,
          Status, LandingPageConv, AdViewed, Answer1, Answer2, Answer3,
          EventDateTime, Ans1Percentage, Ans2Percentage, Ans3Percentage, LastSevenDaysPer, LastfourteenDaysPer, Option1, Option2,
          Option3, Option4, Option5, Option6, Option1Percentage, Option2Percentage, Option3Percentage
          , Option4Percentage, Option5Percentage, Option6Percentage, AnsweredQuestion, Completed, Option1TAnswers, Option2TAnswers, Option3TAnswers, Option4TAnswers, Option5TAnswers, Option6TAnswers, StatusValue, Campaigntype, DivId) {
          var
              campaignID = ko.observable(CampaignID),
              couponID = ko.observable(CouponID),
              pQID = ko.observable(PQID),
              question = ko.observable(Question),
              divId = ko.observable(DivId),
              campaignName = ko.observable(CampaignName),

              status = ko.observable(Status),

              landingPageConv = ko.observable(LandingPageConv),
              adViewed = ko.observable(AdViewed),

              answer1 = ko.observable(Answer1),
              answer2 = ko.observable(Answer2),
              answer3 = ko.observable(Answer3),
              statusValue = ko.observable(StatusValue),
              campaigntype = ko.observable(Campaigntype),
              eventDateTime = ko.observable(EventDateTime ? moment(EventDateTime).toDate() : undefined),


              ans1Percentage = ko.observable(Ans1Percentage),
              ans2Percentage = ko.observable(Ans2Percentage),
              ans3Percentage = ko.observable(Ans3Percentage),
              
              lastSevenDaysPer = ko.observable(LastSevenDaysPer),
              lastfourteenDaysPer = ko.observable(LastfourteenDaysPer),

              option1 = ko.observable(Option1),
              option2 = ko.observable(Option2),
              option3 = ko.observable(Option3),

              option4 = ko.observable(Option4),

              option5 = ko.observable(Option5),

              option6 = ko.observable(Option6),


              option1Percentage = ko.observable(Option1Percentage),


              option2Percentage = ko.observable(Option2Percentage),
              option3Percentage = ko.observable(Option3Percentage),
              option4Percentage = ko.observable(Option4Percentage),
              option5Percentage = ko.observable(Option5Percentage),
              option6Percentage = ko.observable(Option6Percentage),


              answeredQuestion = ko.observable(AnsweredQuestion),
              completed = ko.observable(Completed),

              option1TAnswers = ko.observable(Option1TAnswers),
              option2TAnswers = ko.observable(Option2TAnswers),
              option3TAnswers = ko.observable(Option3TAnswers),
              option4TAnswers = ko.observable(Option4TAnswers),
              option5TAnswers = ko.observable(Option5TAnswers),
              option6TAnswers = ko.observable(option6TAnswers)

             
              // Convert to server data
              
          return {
              campaignID: campaignID,
              couponID: couponID,
              pQID: pQID,
              question: question,
              campaignName: campaignName,
              status: status,
              landingPageConv: landingPageConv,
              adViewed: adViewed,
              answer1: answer1,
              answer2: answer2,
              answer3: answer3,
              eventDateTime: eventDateTime,
              lastSevenDaysPer: lastSevenDaysPer,
              lastfourteenDaysPer: lastfourteenDaysPer,
              option1: option1,
              option2: option2,
              option3: option3,
              option4: option4,
              option5: option5,
              option6: option6,
              option1Percentage: option1Percentage,
              option2Percentage: option2Percentage,
              option3Percentage: option3Percentage,
              option4Percentage: option4Percentage,
              option5Percentage: option5Percentage,
              option6Percentage: option6Percentage,
              answeredQuestion: answeredQuestion,
              completed: completed,
              option1TAnswers: option1TAnswers,
              option2TAnswers: option2TAnswers,
              option3TAnswers: option3TAnswers,
              option4TAnswers: option4TAnswers,
              option5TAnswers: option5TAnswers,
              option6TAnswers: option6TAnswers,
              statusValue: statusValue,
              ans1Percentage: ans1Percentage,
              ans2Percentage: ans2Percentage,
              ans3Percentage: ans3Percentage,
              campaigntype: campaigntype,
              divId: divId
          };
      };

    ////=================================== User
    //Server to Client mapper For User
    var EntityServertoClientMapper = function (itemFromServer) {
        return new Entity(itemFromServer.CampaignID, itemFromServer.CouponID, itemFromServer.PQID, itemFromServer.Question,
            itemFromServer.CampaignName, itemFromServer.Status, itemFromServer.LandingPageConv, itemFromServer.AdViewed,
            itemFromServer.Answer1, itemFromServer.Answer2, itemFromServer.Answer3, itemFromServer.EventDateTime, itemFromServer.Ans1Percentage,
            itemFromServer.Ans2Percentage, itemFromServer.Ans3Percentage, itemFromServer.LastSevenDaysPer, itemFromServer.LastfourteenDaysPer, itemFromServer.Option1,
            itemFromServer.Option2, itemFromServer.Option3, itemFromServer.Option4, itemFromServer.Option5,
            itemFromServer.Option6, itemFromServer.Option1Percentage, itemFromServer.Option2Percentage, itemFromServer.Option3Percentage, itemFromServer.Option4Percentage, itemFromServer.Option5Percentage, itemFromServer.Option6Percentage, itemFromServer.AnsweredQuestion, itemFromServer.Completed, itemFromServer.Option1TAnswers, itemFromServer.Option2TAnswers, itemFromServer.Option3TAnswers, itemFromServer.Option4TAnswers, itemFromServer.Option5TAnswers, itemFromServer.Option6TAnswers, itemFromServer.StatusValue, itemFromServer.Campaigntype, itemFromServer.DivId);

    };

    Entity.Create = function (source) {
        return new Entity(source.campaignID, source.couponID, source.pQID, source.question, source.campaignName, source.status, source.landingPageConv, source.adViewed,
              source.answer1, source.answer2, source.answer3, source.eventDateTime, source.ans1Percentage, source.ans2Percentage, source.ans3Percentage, source.lastSevenDaysPer,
              source.lastfourteenDaysPer, source.option1, source.option2, source.option3, source.option4, source.option5, source.option6, source.option1Percentage, source.option1Percentage, source.option2Percentage, source.option3Percentage, source.option4Percentage, source.option5Percentage, source.option6Percentage, source.statusValue, source.campaigntype);
    };

    // Function to attain cancel button functionality User
    Entity.CreateFromClientModel = function () {
        //todo
    };

    return {
        Entity: Entity,
        EntityServertoClientMapper: EntityServertoClientMapper,

    };
});