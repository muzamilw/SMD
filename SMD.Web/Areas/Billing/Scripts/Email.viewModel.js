/*
    Module with the view model for the Invoice
*/
define("invoice/Email.viewModel",
    ["jquery", "amplify", "ko", "invoice/Email.dataservice", "invoice/Email.model", "common/pagination", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.Email = {
            viewModel: (function () {
                var view,
                    // Invoices list on LV
                    emails = ko.observableArray([]),
                    // Pager
                    pager = ko.observable(),
                    // Sorting
                    sortOn = ko.observable(1),
                    // Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                    // Selected Invoice
                    selectedEmail = ko.observable(),



                    // Get Questions
                    getEmails = function () {
                        dataservice.getEmails(
                              {
                                  PageSize: pager().pageSize(),
                                  PageNo: pager().currentPage(),
                                  SortBy: sortOn(),
                                  IsAsc: sortIsAsc(),

                              },
                              {
                                  success: function (data) {
                                      emails.removeAll();
                                      console.log("Emails");
                                      console.log(data.Emails);
                                      _.each(data.Emails, function (item) {
                                          emails.push(model.EmailServertoClientMapper(item));
                                      });
                                      ////pager().totalCount(0);
                                      pager().totalCount(data.TotalCount);

                                  },
                                  error: function (error) {
                                      toastr.error("Failed to load Emails!");
                                  }
                              });
                    },

                     onUpdateEmail = function () {
                         var a = selectedEmail().convertToServerData();
           
                         dataservice.saveEmails(selectedEmail().convertToServerData(), {
                             success: function (response) {
                                 if (response = true)
                                 {
                                     isEditorVisible(false);
                                 }
                                 
                               
                             },
                             error: function () {
                                 toastr.error("Failed to save!");
                             }
                         });
                     },

                     onCloseClick = function () {
                          window.location.href = "/";
                      },
                    closeEditDialog = function () {
                        selectedEmail(undefined);
                        isEditorVisible(false);
                    },
                    // On editing of existing PQ
                    onEditEmail = function (item) {
       
                         $("#topArea").css("display", "none");
                         $("#divApprove").css("display", "none");
                         selectedEmail(item);
                         isEditorVisible(true);

                    },


                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, emails, getEmails));
                        // First request for LV
                        getEmails();

                    };
                return {
                    initialize: initialize,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    emails: emails,
                    getEmails: getEmails,
                    isEditorVisible: isEditorVisible,
                    closeEditDialog: closeEditDialog,
                    onEditEmail: onEditEmail,
                    selectedEmail: selectedEmail,
                    onCloseClick: onCloseClick,
                    onUpdateEmail: onUpdateEmail


                };
            })()
        };
        return ist.Email.viewModel;
    });
