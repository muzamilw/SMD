/*
    Module with the view model for the Invoice
*/
define("invoice/invoice.viewModel",
    ["jquery", "amplify", "ko", "invoice/invoice.dataservice", "invoice/invoice.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.SurveyQuestion = {
            viewModel: (function() {
                var view,
                    //  Invoices list on LV
                    invoices = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(1),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                     //selected Answer
                    selectedInvoice = ko.observable(),
                    //Get Questions
                    getInvoices = function () {
                        dataservice.searchInvoices(
                            {
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc()
                            },
                            {
                                success: function (data) {
                                    invoices.removeAll();
                                    pager().totalCount(data.TotalCount);
                                    _.each(data.Invoices, function (item) {
                                        invoices.push(model.InvoiceServertoClientMapper(item));
                                    });

                                },
                                error: function () {
                                    toastr.error("Failed to load Invoices!");
                                }
                            });
                    },
                    // Close Editor 
                    closeEditDialog = function () {
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            selectedInvoice(undefined);
                            isEditorVisible(false);
                        });
                        confirmation.show();
                    },
                    // On editing of existing PQ
                    onEditQuestion = function (item) {
                        selectedInvoice(item);
                        isEditorVisible(true);
                    },
                  
                    // Save Question / Add 
                    onSaveQuestion = function () {
                        dataservice.saveSurveyQuestion(selectedInvoice().convertToServerData(), {
                            success: function (obj) {
                                var newObjtodelete = invoices.find(function (temp) {
                                    return obj.SqId == temp.id();
                                });
                                invoices.remove(newObjtodelete);
                                toastr.success("You are Good!");
                                isEditorVisible(false);
                            },
                            error: function () {
                                toastr.error("Failed to delete!");
                            }
                        });
                    },
                   
                    // Has Changes
                    hasChangesOnQuestion = ko.computed(function () {
                        if (selectedInvoice() == undefined) {
                            return false;
                        }
                        return (selectedInvoice().hasChanges());
                    }),
                    onRejectQuestion= function() {
                        if (selectedInvoice().rejectionReason() == undefined || selectedInvoice().rejectionReason() == "" || selectedInvoice().rejectionReason() == " ") {
                            toastr.info("Please add rejection reason!");
                            return false;
                        }
                        onSaveQuestion();
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, invoices, getInvoices));
                       
                        // First request for LV
                        getInvoices();
                    };
                return {
                    initialize: initialize,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    questions: invoices,
                    getInvoices: getInvoices,
                    isEditorVisible: isEditorVisible,
                    closeEditDialog: closeEditDialog,
                    onEditQuestion: onEditQuestion,
                    selectedQuestion: selectedInvoice,
                    onSaveQuestion: onSaveQuestion,
                    hasChangesOnQuestion: hasChangesOnQuestion,
                    onRejectQuestion: onRejectQuestion
                };
            })()
        };
        return ist.SurveyQuestion.viewModel;
    });
