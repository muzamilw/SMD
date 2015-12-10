define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
      Survey = function (sQId, spcQuestion, spcDes, spcDisplayQuestion, spcIsApproved, spcRejectionReason,
          subDate, spcCreatedBy,address,leftImg,righImg) {
          var
              id = ko.observable(sQId),
              question = ko.observable(spcQuestion),
              description = ko.observable(spcDes),
              displayQuestion = ko.observable(spcDisplayQuestion),
              isApproved = ko.observable(spcIsApproved),
              rejectionReason = ko.observable(spcRejectionReason),
              submissionDate = ko.observable(subDate),
              createdBy = ko.observable(spcCreatedBy),
              creatorAddress = ko.observable(address),
              leftImage = ko.observable(leftImg),
              rightImage = ko.observable(righImg),
             
              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                  isApproved: isApproved,
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
                      SqId:id(),
                      Approved: isApproved(),
                      RejectionReason: rejectionReason(),
                  };
              };
          return {
              id: id,
              question: question,
              description: description,
              displayQuestion: displayQuestion,
              isApproved: isApproved,
              rejectionReason: rejectionReason,
              submissionDate: submissionDate,
              createdBy: createdBy,
              creatorAddress: creatorAddress,
              leftImage: leftImage,
              rightImage:rightImage,
              
              hasChanges: hasChanges,
              convertToServerData:convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors
          };
      };


    ///////////////////////////////////////////////////////// Survey QUESTION
    //server to client mapper For Survey QUESTION
    var SurveyquestionServertoClientMapper = function (itemFromServer) {
        return new Survey(itemFromServer.SqId, itemFromServer.Question, itemFromServer.Description,
            itemFromServer.DisplayQuestion, itemFromServer.Approved, itemFromServer.RejectionReason,
            itemFromServer.SubmissionDate, itemFromServer.CreatedBy, itemFromServer.CreatorAddress,
        itemFromServer.LeftPicturePath, itemFromServer.RightPicturePath);
    };
    
    // Function to attain cancel button functionality Survey QUESTION
    Survey.CreateFromClientModel = function (item) {
        return new Survey(item.id, item.question, item.description, item.displayQuestion, item.isApproved,
            item.rejectionReason, item.submissionDate, item.createdBy, item.creatorAddress, item.leftImage,
        item.rightImage);
    };
   
    return {
        Survey: Survey,
        SurveyquestionServertoClientMapper: SurveyquestionServertoClientMapper
    };
});