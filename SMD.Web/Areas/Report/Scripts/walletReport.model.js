define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      WalletReport = function (pqSubBy, pquestion, pqsubmissionDateTime, pqCompanyId, pqprofileGroupId) {
          var
              submittedBy = ko.observable(pqSubBy),
              question = ko.observable(pquestion),
              submissionDate = ko.observable(pqsubmissionDateTime),
              companyId = ko.observable(pqCompanyId),
              profileGroupId = ko.observable(pqprofileGroupId),
             
              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
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
              //convertToServerData = function () {
              //    return {
              //        PqId: id(),
              //        Approved: isApproved(),
              //        RejectionReason: rejectedReason(),
              //    };
              //};
          return {

              submittedBy: submittedBy,
              question: question,
              submissionDate: submissionDate,
              companyId: companyId,
              profileGroupId: profileGroupId,
              id: id,
              ageRangeStart: ageRangeStart,
              ageRangeEnd: ageRangeEnd,
              amountCharged: amountCharged,
              isApproved: isApproved,
              rejectedReason: rejectedReason,
              pqAnswers: pqAnswers,
              type: type,
              hasChanges: hasChanges,
              convertToServerData: convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors

          };
      };


    ///////////////////////////////////////////////////////// Ad-Campaign
    //server to client mapper For AdCampaign
    var WalletReportServertoClientMapper = function (itemFromServer) {


        return new WalletReport(itemFromServer.CreatedBy, itemFromServer.Question, itemFromServer.SubmissionDateTime, itemFromServer.CompanyId, itemFromServer.ProfileGroupId, itemFromServer.Type, itemFromServer.PqId, itemFromServer.Approved, itemFromServer.RejectionReason, itemFromServer.AgeRangeStart, itemFromServer.AgeRangeEnd, itemFromServer.AmountCharged);
    };

    // Function to attain cancel button functionality WalletReport
    WalletReport.CreateFromClientModel = function (item) {
        // To be Implemented
    };

    return {
        WalletReport: WalletReport,
        WalletReportServertoClientMapper: WalletReportServertoClientMapper

    };
});