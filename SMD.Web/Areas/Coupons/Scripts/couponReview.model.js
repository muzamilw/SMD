define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      CouponReview = function (coSubBy, coSubmissionDateTime, couponid, couponTitle) {
          var
              submittedBy = ko.observable(coSubBy),
              submissionDate = ko.observable(coSubmissionDateTime),
              couponId = ko.observable(couponid),
              title = ko.observable(couponTitle)

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
                      //CouponId: couponId(),
                      //IsMarketingStories: IsMarketingStories(),

                  };
              };
          return {

              submittedBy: submittedBy,
              submissionDate: submissionDate,
              couponId: couponId,
              title: title,
              hasChanges: hasChanges,
              convertToServerData: convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors


          };
      };


    ///////////////////////////////////////////////////////// Ad-Campaign
    //server to client mapper For AdCampaign
    var CouponReviewServertoClientMapper = function (itemFromServer) {


        return new CouponReview(itemFromServer.CreatedBy, itemFromServer.SubmissionDateTime, itemFromServer.CouponId, itemFromServer.CouponTitle);
    };
    // Function to attain cancel button functionality Coupons
    CouponReview.CreateFromClientModel = function (item) {
        // To be Implemented
    };

    return {
        CouponReview: CouponReview,
        CouponReviewServertoClientMapper: CouponReviewServertoClientMapper

    };
});