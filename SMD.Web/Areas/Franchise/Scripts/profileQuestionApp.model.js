define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      ProfileQuestion = function (coSubBy, coTitle, coCategory) {
          var
              submittedBy = ko.observable(coSubBy),
              title = ko.observable(coTitle),
              category = ko.observable(coCategory),
           
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
                      //CouponId: couponId(),
                      //Approved: isApproved(),
                      //RejectedReason: rejectedReason(),
                  };
              };
          return {

              submittedBy: submittedBy,
              title: title,
              category: category,
              hasChanges: hasChanges,
              convertToServerData: convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors

          };
      };


    ///////////////////////////////////////////////////////// Ad-Campaign
    //server to client mapper For AdCampaign
    var ProfileQuestionServertoClientMapper = function (itemFromServer) {


        return new ProfileQuestion(itemFromServer.CreatedBy, itemFromServer.CouponTitle, itemFromServer.CreatedBy);
    };
  
    // Function to attain cancel button functionality ProfileQuestion
    ProfileQuestion.CreateFromClientModel = function (item) {
        // To be Implemented
    };

    return {
        ProfileQuestion: ProfileQuestion,
        ProfileQuestionServertoClientMapper: ProfileQuestionServertoClientMapper
        
    };
});