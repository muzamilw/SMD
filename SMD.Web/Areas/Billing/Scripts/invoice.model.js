define(["ko", "underscore", "underscore-ko"], function(ko) {

    var // ReSharper disable InconsistentNaming
      Invoice = function (invoiceId, spcDate, spcRef, spcTotal,spcUser) {
          var
              id = ko.observable(invoiceId),
              invoDate = ko.observable(spcDate),
              refrence = ko.observable(spcRef),
              invoiceTotal = ko.observable(spcTotal),
              user = ko.observable(spcUser),

              invoiceDetails = ko.observableArray([]),
              errors = ko.validation.group({

              }),
              // Is Valid
              isValid = ko.computed(function () {
                  return errors().length === 0;
              }),
              dirtyFlag = new ko.dirtyFlag({
                  invoDate: invoDate,
                  refrence: refrence,
                  invoiceDetails: invoiceDetails
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
              invoiceTotal: invoiceTotal,
              user: user,
              invoiceDetails:invoiceDetails,

              hasChanges: hasChanges,
              convertToServerData:convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors
          };
      };


    var // ReSharper disable InconsistentNaming
      InvoiceItem = function (spcId, spcName, spcGrossAmount, spcInvoiceId, spcInvoiceDetailId) {
          var
              id = ko.observable(spcId),
              name = ko.observable(spcName),
              grossAmount = ko.observable(spcGrossAmount),
              invoiceId = ko.observable(spcInvoiceId),
              invoiceDetailId = ko.observable(spcInvoiceDetailId),

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
              convertToServerData = function () {
                  return {

                  };
              };
          return {
              id: id,
              name: name,
              grossAmount: grossAmount,
              invoiceId: invoiceId,
              invoiceDetailId: invoiceDetailId,

              hasChanges: hasChanges,
              convertToServerData: convertToServerData,
              reset: reset,
              isValid: isValid,
              errors: errors
          };
      };


    ////=================================== Invoice
    //server to client mapper For Invoice
    var InvoiceServertoClientMapper = function (itemFromServer) {
        return new Invoice(itemFromServer.InvoiceId, itemFromServer.InvoiceDate, itemFromServer.CreditCardRef,
            itemFromServer.NetTotal, itemFromServer.UserName);
    };
    
    // Function to attain cancel button functionality Invoice
    Invoice.CreateFromClientModel = function (item) {
        return new Invoice(item.id, item.invoDate, item.refrence, item.invoiceTotal);
    };
   

    ////================================ Invoice Item 
    //server to client mapper For Invoice
    var InvoiceItemServertoClientMapper = function (itemFromServer) {
        return new InvoiceItem(itemFromServer.ItemId, itemFromServer.ItemName, itemFromServer.ItemGrossAmount,
            itemFromServer.InvoiceId, itemFromServer.InvoiceDetailId);
    };

    return {
        Invoice: Invoice,
        InvoiceServertoClientMapper: InvoiceServertoClientMapper,

        InvoiceItem: InvoiceItem,
        InvoiceItemServertoClientMapper: InvoiceItemServertoClientMapper
    };
});