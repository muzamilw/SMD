define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      WalletReport = function (wrDescription, wrTransactionDate, wrCreditAmount, wrDebitAmount) {
          var
              description = ko.observable(wrDescription),
              transatinDate = ko.observable(wrTransactionDate),
              creditAmount = ko.observable(wrCreditAmount == 0 ? '-' + wrDebitAmount :'+'+wrCreditAmount),
              debitAmount = ko.observable(wrDebitAmount),
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
              };
          return {

              description: description,
              transatinDate: transatinDate,
              creditAmount: creditAmount,
              debitAmount: debitAmount,
              hasChanges: hasChanges,
              //convertToServerData: convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors

          };
      };


    ///////////////////////////////////////////////////////// Ad-Campaign
    //server to client mapper For AdCampaign
    var WalletReportServertoClientMapper = function (itemFromServer) {
        return new WalletReport(itemFromServer.description, itemFromServer.TransactionDate, itemFromServer.CreditAmount, itemFromServer.DebitAmount);
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