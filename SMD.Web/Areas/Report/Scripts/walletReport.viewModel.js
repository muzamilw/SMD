define("Report/walletReport.viewModel",
    ["jquery", "amplify", "ko", "Report/walletReport.dataservice", "Report/walletReport.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.Report = {
            viewModel: (function () {
                var view,
                    walletReport = ko.observableArray([]),
                    referralComponies = ko.observableArray([]),
                    linkedComponiesCount = ko.observable(),
                    activeCampaignsCount = ko.observable(),
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
                                    balance(data.Balance);
                                    var amount = data.Balance / 100;
                                    dollarBalance(amount);
                                    getReferralComponies();
                                    ////pager().totalCount(0);
                                    //pager().totalCount(data.TotalCount);
                                },
                                error: function () {
                                    toastr.error("Failed to load Wallet Report");
                                }
                            });
                    },
                getReferralComponies = function ()
                {
                    dataservice.getreferralComponies({
                        success: function (data) {
                            var count;
                            for (var i = 0, len = data.length; i < len; i++) {
                                referralComponies.push(data[i]);
                            }
                            linkedComponiesCount(data.length);
                            _.each(data, function (item) {
                                count = item.pcount + item.vcount + item.scount;
                            });
                            activeCampaignsCount(count);
                          
                        },
                        error: function () {
                            toastr.error("Failed to load branchCategory.");
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
                    referralComponies: referralComponies,
                    linkedComponiesCount: linkedComponiesCount,
                    activeCampaignsCount:activeCampaignsCount,
                    balance: balance,
                    dollarBalance: dollarBalance,
                };
            })()
        };
        return ist.Report.viewModel;
    });