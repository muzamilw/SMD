﻿define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      ProfileQuestion = function (pqSubBy, pquestion,pqsubmissionDateTime) {
          var
              submittedBy = ko.observable(pqSubBy),
              question = ko.observable(pquestion),
              submissionDate = ko.observable(pqsubmissionDateTime),
           
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


        return new ProfileQuestion(itemFromServer.CreatedBy, itemFromServer.Question, itemFromServer.SubmissionDateTime);
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