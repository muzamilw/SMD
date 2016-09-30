define("Report/walletReport.viewModel",
    ["jquery", "amplify", "ko", "Report/walletReport.dataservice", "Report/walletReport.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.Report = {
            viewModel: (function () {
                var view,
                    walletReport = ko.observableArray([]),
                    balance = ko.observable();
                    dollarBalance = ko.observable();
                    //pager
                   
                
                    getWalletReportHistory = function () {
                        dataservice.getWalletReport(
                            {
                                CompanyId: gCompanyID,
                                authenticationToken :'test',
                            },
                            {
                                success: function (data) {
                                    walletReport.removeAll();
                                    console.log("wallet Report");
                                    console.log(data.Transactions);
                                    _.each(data.Transactions, function (item) {
                                        walletReport.push(model.WalletReportServertoClientMapper(item));
                                    });
                                    balance(data.Balance.toFixed(2));
                                    var amount = data.Balance / 100;
                                    dollarBalance(amount.toFixed(2));
                                    ////pager().totalCount(0);
                                    //pager().totalCount(data.TotalCount);
                                },
                                error: function () {
                                    toastr.error("Failed to load Wallet Report");
                                }
                            });
                    },
                   
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        getWalletReportHistory();
                    };
                return {

                    initialize: initialize,
                    getWalletReportHistory: getWalletReportHistory,
                    walletReport: walletReport,
                    balance: balance,
                    dollarBalance: dollarBalance,
                };
            })()
        };
        return ist.Report.viewModel;
    });