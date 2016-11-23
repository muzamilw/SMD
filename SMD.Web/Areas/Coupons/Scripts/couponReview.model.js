define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      CouponReview = function (coProfileImage, coFullName, coReview, coRatingDateTime, coStatus, coCouponReviewId) {
          var
              profileImage = ko.observable(coProfileImage),
              fullName = ko.observable(coFullName),
              review = ko.observable(coReview),
              reviewDate = ko.observable(coRatingDateTime),
              status = ko.observable(coStatus),
              couponReviewId = ko.observable(coCouponReviewId),


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
                      CouponReviewId: couponReviewId(),
                      Status: status(),

                  };
              };
          return {

              profileImage: profileImage,
              fullName: fullName,
              review: review,
              reviewDate: reviewDate,
              status: status,
              couponReviewId: couponReviewId,
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


        return new CouponReview(itemFromServer.ProfileImage, itemFromServer.FullName, itemFromServer.Review, itemFromServer.RatingDateTime, itemFromServer.Status, itemFromServer.CouponReviewId);
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