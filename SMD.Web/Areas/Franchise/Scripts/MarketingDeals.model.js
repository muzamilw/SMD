define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      MarketingDeal = function (coSubBy, coSubmissionDateTime, couponid, couponTitle, coApprovedBy, coApprovalDateTime, coHighlightLine1, coFinePrintLine1, coCategories, coSearchKeywords, couponImage1, couponImage2, couponImage3, couponImage4, couponImage5, couponImage6,
          colocationLine1, coLocationLine2, coLocationCity, coLocationState, coLocationZipCode, coLocationLAT, coLocationLON, coLocationPhone, coBuyitLandingPageUrl, coBuyitBtnLabel, coCurrencyId, cocompanyId, userId, coIsMarketingStories) {
          var
              submittedBy = ko.observable(coSubBy),
              submissionDate = ko.observable(coSubmissionDateTime),
              couponId = ko.observable(couponid),
              title = ko.observable(couponTitle),
              approvedBy = ko.observable(coApprovedBy),
              approvalDateTime = ko.observable(coApprovalDateTime),
              highlightLine1 = ko.observable(coHighlightLine1),
              finePrintLine1 = ko.observable(coFinePrintLine1),
              categories = ko.observable(coCategories),
              searchKeywords = ko.observable(coSearchKeywords),
              couponimage1 = ko.observable(couponImage1),
              CouponImage2 = ko.observable(couponImage2),
              CouponImage3 = ko.observable(couponImage3),
              CouponImage4 = ko.observable(couponImage4),
              CouponImage5 = ko.observable(couponImage5),
              CouponImage6 = ko.observable(couponImage6),
              locationLine1 = ko.observable(colocationLine1),
              locationLine2 = ko.observable(coLocationLine2),
              locationCity = ko.observable(coLocationCity),
              locationState = ko.observable(coLocationState),
              locationZipCode = ko.observable(coLocationZipCode),
              locationLAT = ko.observable(coLocationLAT),
              locationLON = ko.observable(coLocationLON),
              locationPhone = ko.observable(coLocationPhone),
              buyitLandingPageUrl = ko.observable(coBuyitLandingPageUrl),
              buyitBtnLabel = ko.observable(coBuyitBtnLabel),
              CurrencyId = ko.observable(coCurrencyId),
              companyId = ko.observable(cocompanyId),
              userId = ko.observable(userId),
              IsMarketingStories = ko.observable(coIsMarketingStories),

             
             

              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                  //rejectedReason: rejectedReason
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
                      CouponId: couponId(),
                      IsMarketingStories: IsMarketingStories(),

                  };
              };
          return {

              submittedBy: submittedBy,
              submissionDate: submissionDate,
              couponId: couponId,
              title: title,
              approvedBy: approvedBy,
              approvalDateTime: approvalDateTime,
              highlightLine1: highlightLine1,
              finePrintLine1: finePrintLine1,
              categories: categories,
              searchKeywords: searchKeywords,
              couponimage1: couponimage1,
              CouponImage2:CouponImage2,
              CouponImage3: CouponImage3,
              CouponImage4: CouponImage4,
              CouponImage5: CouponImage5,
              CouponImage6: CouponImage6,
              locationLine1:locationLine1,
              locationLine2: locationLine2,
              locationCity: locationCity,
              locationState: locationState,
              locationZipCode: locationZipCode,
              locationLAT: locationLAT,
              locationLON: locationLON,
              locationPhone: locationPhone,
              buyitLandingPageUrl: buyitLandingPageUrl,
              buyitBtnLabel: buyitBtnLabel,
              CurrencyId: CurrencyId,
              companyId: companyId,
              userId: userId,
              IsMarketingStories:IsMarketingStories,
              hasChanges: hasChanges,
              convertToServerData: convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors


          };
      };


    ///////////////////////////////////////////////////////// Ad-Campaign
    //server to client mapper For AdCampaign
    var MarketingDealServertoClientMapper = function (itemFromServer) {


        return new MarketingDeal(itemFromServer.CreatedBy, itemFromServer.SubmissionDateTime, itemFromServer.CouponId, itemFromServer.CouponTitle, itemFromServer.ApprovedBy, itemFromServer.ApprovalDateTime, itemFromServer.HighlightLine1, itemFromServer.FinePrintLine1, itemFromServer.Categories, itemFromServer.SearchKeywords, itemFromServer.couponImage1,
             itemFromServer.CouponImage2, itemFromServer.CouponImage3, itemFromServer.CouponImage4, itemFromServer.CouponImage5, itemFromServer.CouponImage6, itemFromServer.LocationLine1, itemFromServer.LocationLine2, itemFromServer.LocationCity, itemFromServer.LocationState, itemFromServer.LocationZipCode, itemFromServer.LocationLAT, itemFromServer.LocationLON,
            itemFromServer.LocationPhone, itemFromServer.BuyitLandingPageUrl, itemFromServer.BuyitBtnLabel, itemFromServer.CurrencyId, itemFromServer.CompanyId, itemFromServer.UserId, itemFromServer.IsMarketingStories);
    };
    // Function to attain cancel button functionality Coupons
    MarketingDeal.CreateFromClientModel = function (item) {
        // To be Implemented
    };

    return {
        MarketingDeal: MarketingDeal,
        MarketingDealServertoClientMapper: MarketingDealServertoClientMapper

    };
});