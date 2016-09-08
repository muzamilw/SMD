define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      AdCampaign = function (sQId, spcName, spcDes, spcIsApproved, spcRejectionReason,
          subDate, spcCreatedBy, spcType, spcPath, spcLink, spcrate, spcBudget, CampaignDescription, Voucher1Heading, Voucher1Description, Voucher1Value, Voucher1ImagePath,
          CouponSwapValue, CouponActualValue, CouponQuantity, CouponTakenCount, MaxDailyBudget, VerifyQuestion, Answer1, Answer2, Answer3, CorrectAnswer, DeliveryDays, BuuyItLine1, BuyItButtonLabel, VideoUrl,VideoLink2) {
          var
              id = ko.observable(sQId),
              campaignName = ko.observable(spcName),
              description = ko.observable(spcDes),

              isApproved = ko.observable(spcIsApproved),
              rejectionReason = ko.observable(spcRejectionReason),
              submissionDate = ko.observable(subDate),
              createdBy = ko.observable(spcCreatedBy),
              type = ko.observable(spcType),
              imagePath = ko.observable(spcPath),
              landingLink = ko.observable(spcLink),
              clickRate = ko.observable(spcrate),
              maxBudget = ko.observable(spcBudget),
             CampaignDescription = ko.observable(CampaignDescription),
             Voucher1Heading = ko.observable(Voucher1Heading),
             Voucher1Description = ko.observable(Voucher1Description),
             Voucher1Value = ko.observable(Voucher1Value),
             Voucher1ImagePath = ko.observable(Voucher1ImagePath),
               CouponSwapValue = ko.observable(CouponSwapValue),
              CouponActualValue = ko.observable(CouponActualValue),
              CouponQuantity = ko.observable(CouponQuantity),
              CouponTakenCount = ko.observable(CouponTakenCount),
              maxDailyBudget = ko.observable(MaxDailyBudget),
              verifyQuestion = ko.observable(VerifyQuestion),
              answer1 = ko.observable(Answer1),
              answer2 = ko.observable(Answer2),
              answer3 = ko.observable(Answer3),
              correctAnswer = ko.observable(CorrectAnswer),
              deliveryDays = ko.observable(DeliveryDays),
              buyItLine1 = ko.observable(BuuyItLine1),
              buyItButtonLabel = ko.observable(BuyItButtonLabel),
              videoUrl = ko.observable(VideoUrl),
              videoLink2 = ko.observable(VideoLink2),

              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                  rejectionReason: rejectionReason
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
                      CampaignId: id(),
                      Approved: isApproved(),
                      RejectedReason: rejectionReason(),
                  };
              };
          return {
              id: id,
              campaignName: campaignName,
              description: description,
              isApproved: isApproved,
              rejectionReason: rejectionReason,
              submissionDate: submissionDate,
              createdBy: createdBy,
              type: type,
              imagePath: imagePath,
              landingLink: landingLink,
              clickRate: clickRate,
              maxBudget: maxBudget,
              CampaignDescription: CampaignDescription,
              hasChanges: hasChanges,
              convertToServerData: convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors,
              Voucher1Heading: Voucher1Heading,
              Voucher1Description: Voucher1Description,
              Voucher1Value: Voucher1Value,
              Voucher1ImagePath: Voucher1ImagePath,
              CouponSwapValue: CouponSwapValue(),
              CouponActualValue: CouponActualValue(),
              CouponQuantity: CouponQuantity(),
              CouponTakenCount: CouponTakenCount(),
              maxDailyBudget: maxDailyBudget,
              verifyQuestion: verifyQuestion,
              answer1: answer1,
              answer2: answer2,
              answer3: answer3,
              correctAnswer: correctAnswer,
              deliveryDays: deliveryDays,
              buyItLine1: buyItLine1,
              buyItButtonLabel: buyItButtonLabel,
              videoUrl: videoUrl,
              videoLink2: videoLink2

          };
      };


    ///////////////////////////////////////////////////////// Ad-Campaign
    //server to client mapper For AdCampaign
    var AdCampaignServertoClientMapper = function (itemFromServer) {
        var videoLink = itemFromServer.LandingPageVideoLink;
        if (videoLink != null) {
            videoLink = videoLink.replace('watch?v=', 'embed/');
        }

        return new AdCampaign(itemFromServer.CampaignId, itemFromServer.CampaignName, itemFromServer.Description,
            itemFromServer.Approved, itemFromServer.RejectedReason,
            itemFromServer.SubmissionDateTime, itemFromServer.CreatedBy, itemFromServer.Type, itemFromServer.LogoUrl, videoLink,
        itemFromServer.ClickRate, itemFromServer.MaxBudget, itemFromServer.CampaignDescription, itemFromServer.Voucher1Heading, itemFromServer.Voucher1Description,
            itemFromServer.Voucher1Value, itemFromServer.Voucher1ImagePath, itemFromServer.CouponSwapValue == null ? "" : itemFromServer.CouponSwapValue,
            itemFromServer.CouponActualValue == null ? "" : itemFromServer.CouponActualValue,
            itemFromServer.CouponQuantity == null ? "" : itemFromServer.CouponQuantity,
            itemFromServer.CouponTakenCount == null ? "" : itemFromServer.CouponTakenCount, itemFromServer.MaxDailyBudget, itemFromServer.VerifyQuestion, itemFromServer.Answer1, itemFromServer.Answer2, itemFromServer.Answer3, itemFromServer.CorrectAnswer, itemFromServer.DeliveryDays, itemFromServer.BuuyItLine1, itemFromServer.BuyItButtonLabel, itemFromServer.VideoUrl, itemFromServer.VideoLink2);
    };

    // Function to attain cancel button functionality AdCampaign
    AdCampaign.CreateFromClientModel = function (item) {
        // To be Implemented
    };

    return {
        AdCampaign: AdCampaign,
        AdCampaignServertoClientMapper: AdCampaignServertoClientMapper
    };
});