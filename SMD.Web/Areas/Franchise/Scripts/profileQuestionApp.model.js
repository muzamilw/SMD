define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      ProfileQuestion = function (pqSubBy, pquestion, pqsubmissionDateTime, pqCompanyId, pqprofileGroupId, pqType, PqId, pqApproved, pqRejectedReason, pqAgeRangeStart, pqAgeRangeEnd, pqAmountCharged, pqAnswerNeeded, PqUserID) {
          var
              submittedBy = ko.observable(pqSubBy),
              question = ko.observable(pquestion),
              submissionDate = ko.observable(pqsubmissionDateTime),
              companyId = ko.observable(pqCompanyId),
              profileGroupId = ko.observable(pqprofileGroupId),
              type = ko.observable(pqType),
              id = ko.observable(PqId),
              isApproved = ko.observable(pqApproved),
              rejectedReason = ko.observable(pqRejectedReason),
              pqAnswers = ko.observableArray([]),
              ageRangeStart = ko.observable(pqAgeRangeStart),
              ageRangeEnd = ko.observable(pqAgeRangeEnd),
              amountCharged = ko.observable(pqAmountCharged),
              deliverCount = ko.observable(pqAnswerNeeded),
              userID = ko.observable(PqUserID),
           
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
                      PqId: id(),
                      Approved: isApproved(),
                      RejectionReason: rejectedReason(),
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
              amountCharged:amountCharged,
              isApproved :isApproved,
              rejectedReason :rejectedReason,
              pqAnswers:pqAnswers,
              type: type,
              deliverCount: deliverCount,
              userID:userID,
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


        return new ProfileQuestion(itemFromServer.CreatedBy, itemFromServer.Question, itemFromServer.SubmissionDateTime, itemFromServer.CompanyId, itemFromServer.ProfileGroupId, itemFromServer.Type, itemFromServer.PqId, itemFromServer.Approved, itemFromServer.RejectionReason, itemFromServer.AgeRangeStart, itemFromServer.AgeRangeEnd, itemFromServer.AmountCharged, itemFromServer.AnswerNeeded, itemFromServer.UserID);
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