define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
      Invoice = function (invoiceId, spcDate, spcRef, spcTotal) {
          var
              id = ko.observable(invoiceId),
              invoDate = ko.observable(spcDate),
              refrence = ko.observable(spcRef),
              invoiceTotal = ko.observable(spcTotal),
             
              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                  invoDate: invoDate,
                  refrence: refrence
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
                      
                  };
              };
          return {
              id: id,
              invoDate: invoDate,
              refrence: refrence,
              invoiceTotal:invoiceTotal,

              hasChanges: hasChanges,
              convertToServerData:convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors
          };
      };

    //server to client mapper For Invoice
    var InvoiceServertoClientMapper = function (itemFromServer) {
        return new Invoice(itemFromServer.InvoiceId, itemFromServer.InvoiceDate, itemFromServer.CreditCardRef,
            itemFromServer.NetTotal);
    };
    
    // Function to attain cancel button functionality Invoice
    Invoice.CreateFromClientModel = function (item) {
        return new Invoice(item.id, item.invoDate, item.refrence, item.invoiceTotal);
    };
   
    return {
        Invoice: Invoice,
        InvoiceServertoClientMapper: InvoiceServertoClientMapper
    };
});