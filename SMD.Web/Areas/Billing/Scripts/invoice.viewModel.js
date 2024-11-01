﻿/*
    Module with the view model for the Invoice
*/
define("invoice/invoice.viewModel",
    ["jquery", "amplify", "ko", "invoice/invoice.dataservice", "invoice/invoice.model", "common/pagination"],
    function ($, amplify, ko, dataservice, model, pagination) {
        var ist = window.ist || {};
        ist.Invoice = {
            viewModel: (function() {
                var view,
                    // Invoices list on LV
                    invoices = ko.observableArray([]),
                    // Pager
                    pager = ko.observable(),
                    // Sorting
                    sortOn = ko.observable(1),
                    // Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                    // Selected Invoice
                    selectedInvoice = ko.observable(),
                    // From Date for filter
                    fromDateFilter = ko.observable(undefined),
                    // To filter
                    toDateFilter = ko.observable(undefined),
                    // Get Questions
                    getInvoices = function () {
                        dataservice.searchInvoices(
                            {
                                FromDate: fromDateFilter(),
                                ToDate:   toDateFilter(),
                                PageSize: pager().pageSize(),
                                PageNo:   pager().currentPage(),
                                SortBy:   sortOn(),
                                IsAsc:    sortIsAsc()
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
                        selectedInvoice(undefined);
                        isEditorVisible(false);
                    },
                    // On editing of existing PQ
                    onEditInvoice = function (invoice) {
                        selectedInvoice(invoice);
                        getInvoiceDetails(invoice);
                        isEditorVisible(true);

                    },
                    // Get Invoice Details 
                    getInvoiceDetails= function(invoice) {
                       dataservice.getInvoiceDetails(
                            {
                                InvoiceId: selectedInvoice().id()
                            },
                            {
                                success: function (invoiceDetails) {
                                    selectedInvoice().invoiceDetails.removeAll();
                                    _.each(invoiceDetails, function (det) {
                                        selectedInvoice().invoiceDetails.push(model.InvoiceItemServertoClientMapper(det));
                                    });
                                },
                                error: function () {
                                    toastr.error("Failed to load Invoices!");
                                }
                            });
                   },
                    // Has Changes Makes 
                    hasChangesOnQuestion = ko.computed(function () {
                        if (selectedInvoice() == undefined) {
                            return false;
                        }
                        return (selectedInvoice().hasChanges());
                    }),
                    // Search Button Func
                    onSearchButtonClick= function() {
                        getInvoices();
                    },
                    // Reset Filters
                    resetData= function() {
                        fromDateFilter(undefined);
                        toDateFilter(undefined);
                        getInvoices();
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
                    invoices: invoices,
                    getInvoices: getInvoices,
                    isEditorVisible: isEditorVisible,
                    closeEditDialog: closeEditDialog,
                    onEditInvoice: onEditInvoice,
                    selectedInvoice: selectedInvoice,
                    hasChangesOnQuestion: hasChangesOnQuestion,
                    fromDateFilter: fromDateFilter,
                    toDateFilter: toDateFilter,
                    onSearchButtonClick: onSearchButtonClick,
                    resetData: resetData
                };
            })()
        };
        return ist.Invoice.viewModel;
    });
