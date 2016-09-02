define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      ProfileQuestion = function (pqSubBy, pquestion, pqsubmissionDateTime, pqCompanyId, pqprofileGroupId, pqType, PqId, pqAgeRangeStart, pqAgeRangeEnd) {
          var
              submittedBy = ko.observable(pqSubBy),
              question = ko.observable(pquestion),
              submissionDate = ko.observable(pqsubmissionDateTime),
              companyId = ko.observable(pqCompanyId),
              profileGroupId = ko.observable(pqprofileGroupId),
              type = ko.observable(pqType),
              id = ko.observable(PqId),
              pqAnswers = ko.observableArray([]),
              ageRangeStart = ko.observable(pqAgeRangeStart),
              ageRangeEnd = ko.observable(pqAgeRangeEnd),
           
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
                      //Approved: isApproved(),
                      //RejectedReason: rejectedReason(),
                  };
              };
          return {

              submittedBy: submittedBy,
              question: question,
              submissionDate: submissionDate,
              companyId: companyId,
              profileGroupId: profileGroupId,
              id: id,
              ageRangeStart: ageRangeStart,
              ageRangeEnd: ageRangeEnd,
              pqAnswers:pqAnswers,
              type:type,
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


        return new ProfileQuestion(itemFromServer.CreatedBy, itemFromServer.Question, itemFromServer.SubmissionDateTime, itemFromServer.CompanyId, itemFromServer.ProfileGroupId, itemFromServer.Type, itemFromServer.PqId, itemFromServer.AgeRangeStart, itemFromServer.AgeRangeEnd);
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