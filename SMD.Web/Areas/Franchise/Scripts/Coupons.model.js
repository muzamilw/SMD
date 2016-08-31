define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      Coupons = function (coSubBy, coTitle, coCategory, coSwabCost, coSubDate, couponid, coApproved,coRejectedReason, coExpireDate, coImage1, coImage2, coImage3, coLogoUrl, cohighlight1, cohighlight2, cohighlight3, cohighlight4, cohighlight5, coCurrencyId, coPrice
          , coSavings, couponExpirydate, couponListingMode, coSearchKeywords, couponQtyPerUser, coMonthYear, coFinePrintLine1, coFinePrintLine2, coFinePrintLine3, coFinePrintLine4, coFinePrintLine5, coHowToRedeemLine1, coHowToRedeemLine2
          , coHowToRedeemLine3, coHowToRedeemLine4, coHowToRedeemLine5, coLocationTitle, coLocationLine1, coLocationLine2, coLocationCity, coLocationState, coLocationZipCode, coLocationLAT, coLocationLON, coLocationPhone) {
          var
              submittedBy = ko.observable(coSubBy),
              title = ko.observable(coTitle),
              category = ko.observable(coCategory),
              swabCost = ko.observable(coSwabCost),
              submissionDate = ko.observable(coSubDate),
              couponId = ko.observable(couponid),
              isApproved = ko.observable(coApproved),
              rejectedReason = ko.observable(coRejectedReason),
              expireDate = ko.observable(coExpireDate),
              couponImage1 = ko.observable(coImage1),
              couponImage2 = ko.observable(coImage2),
              couponImage3 = ko.observable(coImage3),
              logoUrl = ko.observable(coLogoUrl),
              highlight1 = ko.observable(cohighlight1),
              highlight2 = ko.observable(cohighlight2),
              highlight3 = ko.observable(cohighlight3),
              highlight4 = ko.observable(cohighlight4),
              highlight5 = ko.observable(cohighlight5),
              currencyId = ko.observable(coCurrencyId),
              price = ko.observable(coPrice),
              savings = ko.observable(coSavings),
              couponExpirydate = ko.observable(couponExpirydate),
              couponListingMode = ko.observable(couponListingMode),
              coSearchKeywords = ko.observable(coSearchKeywords),
              couponQtyPerUser = ko.observable(couponQtyPerUser),
              coMonthYear = ko.observable(coMonthYear),
              printLine1 = ko.observable(coFinePrintLine1),
              printLine2 = ko.observable(coFinePrintLine2),
              printLine3 = ko.observable(coFinePrintLine3),
              printLine4 = ko.observable(coFinePrintLine4),
              printLine5 = ko.observable(coFinePrintLine5),
              redeemLine1 = ko.observable(coHowToRedeemLine1),
              redeemLine2 = ko.observable(coHowToRedeemLine2),
              redeemLine3 = ko.observable(coHowToRedeemLine3),
              redeemLine4 = ko.observable(coHowToRedeemLine4),
              redeemLine5 = ko.observable(coHowToRedeemLine5),
              locationTitle = ko.observable(coLocationTitle),
              locationLine1 = ko.observable(coLocationLine1),
              locationLine2 = ko.observable(coLocationLine2),
              locationCity = ko.observable(coLocationCity),
              locationState = ko.observable(coLocationState),
              locationZipCode = ko.observable(coLocationZipCode),
              locationLAT = ko.observable(coLocationLAT),
              locationLON = ko.observable(coLocationLON),
              locationPhone = ko.observable(coLocationPhone),



              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                 rejectedReason: rejectedReason
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
                      Approved: isApproved(),
                      RejectedReason: rejectedReason(),
                  };
              };
          return {

              submittedBy: submittedBy,
              title: title,
              category: category,
              swabCost: swabCost,
              submissionDate: submissionDate,
              couponId: couponId,
              isApproved: isApproved,
              rejectedReason:rejectedReason,
              expireDate: expireDate,
              couponImage1: couponImage1,
              couponImage2: couponImage2,
              couponImage3: couponImage3,
              logoUrl: logoUrl,
              highlight1: highlight1,
              highlight2: highlight2,
              highlight3: highlight3,
              highlight4: highlight4,
              highlight5: highlight5,
              currencyId: currencyId,
              price: price,
              savings: savings,
              couponExpirydate: couponExpirydate,
              couponListingMode: couponListingMode,
              coSearchKeywords: coSearchKeywords,
              couponQtyPerUser: couponQtyPerUser,
              coMonthYear: coMonthYear,
              printLine1: printLine1,
              printLine2: printLine2,
              printLine3: printLine3,
              printLine4: printLine4,
              printLine5: printLine5,
              redeemLine1: redeemLine1,
              redeemLine2: redeemLine2,
              redeemLine3: redeemLine3,
              redeemLine4: redeemLine4,
              redeemLine5: redeemLine5,
              locationTitle: locationTitle,
              locationLine1: locationLine1,
              locationLine2: locationLine2,
              locationCity: locationCity,
              locationState: locationState,
              locationZipCode: locationZipCode,
              locationLAT: locationLAT,
              locationLON: locationLON,
              locationPhone: locationPhone,

              hasChanges: hasChanges,
              convertToServerData: convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors,


          };
      };


    ///////////////////////////////////////////////////////// Ad-Campaign
    //server to client mapper For AdCampaign
    var CouponsServertoClientMapper = function (itemFromServer) {


        return new Coupons(itemFromServer.CreatedBy, itemFromServer.CouponTitle, itemFromServer.CreatedBy,
            itemFromServer.SwapCost, itemFromServer.SubmissionDateTime, itemFromServer.CouponId, itemFromServer.Approved,itemFromServer.RejectedReason, itemFromServer.CouponExpirydate, itemFromServer.couponImage1, itemFromServer.CouponImage2, itemFromServer.CouponImage3, itemFromServer.LogoUrl, itemFromServer.HighlightLine1, itemFromServer.HighlightLine2, itemFromServer.HighlightLine3, itemFromServer.HighlightLine4, itemFromServer.HighlightLine5, itemFromServer.CurrencyId, itemFromServer.Price
            , itemFromServer.Savings, itemFromServer.CouponExpirydate, itemFromServer.CouponListingMode, itemFromServer.SearchKeywords, itemFromServer.CouponQtyPerUser, GetMonth(itemFromServer.CouponActiveMonth, itemFromServer.CouponActiveYear), itemFromServer.FinePrintLine1, itemFromServer.FinePrintLine2, itemFromServer.FinePrintLine3, itemFromServer.FinePrintLine4, itemFromServer.FinePrintLine5, itemFromServer.HowToRedeemLine1
            , itemFromServer.HowToRedeemLine2, itemFromServer.HowToRedeemLine3, itemFromServer.HowToRedeemLine4, itemFromServer.HowToRedeemLine5, itemFromServer.LocationTitle, itemFromServer.LocationLine1, itemFromServer.LocationLine2, itemFromServer.LocationCity, itemFromServer.LocationState, itemFromServer.LocationZipCode, itemFromServer.LocationLAT, itemFromServer.LocationLON, itemFromServer.LocationPhone);
    };
    var GetMonth = function (monthstr, year) {

        var month = new Array();
        month[1] = "January";
        month[2] = "February";
        month[3] = "March";
        month[4] = "April";
        month[5] = "May";
        month[6] = "June";
        month[7] = "July";
        month[8] = "August";
        month[9] = "September";
        month[10] = "October";
        month[11] = "November";
        month[12] = "December";
        var index = month[monthstr];
        return index + ', ' + year;
    };

    // Function to attain cancel button functionality Coupons
    Coupons.CreateFromClientModel = function (item) {
        // To be Implemented
    };

    return {
        Coupons: Coupons,
        CouponsServertoClientMapper: CouponsServertoClientMapper,
        GetMonth: GetMonth
    };
});