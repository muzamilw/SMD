/*
    Module with the view model for the AdCampaign
*/
define("FranchiseDashboard/addApproval.viewModel",
    ["jquery", "amplify", "ko", "FranchiseDashboard/addApproval.dataservice", "FranchiseDashboard/addApproval.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.AdCampaign = {
            viewModel: (function() {
                var view,
                    //  AdCampaign list on LV
                    campaigns = ko.observableArray([]),
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(5),
                    adType = ko.observable(1),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                    isShowCopounMode = ko.observable(false),
                    // variables 
                    lblPageTitle = ko.observable(null),
                    //selected AdCampaign
                    selectedCampaign = ko.observable(),
                    //Get AdCampaign
                    getCampaigns = function() {
                        dataservice.searchAdCampaigns(
                            {
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc(),
                                ShowCoupons: isShowCopounMode(),
                                status: adType(),

                            },
                            {
                                success: function(data) {
                                    campaigns.removeAll();
                                    console.log("approval campaign");
                                    console.log(data.AdCampaigns);
                                    _.each(data.AdCampaigns, function (item) {
                                        campaigns.push(model.AdCampaignServertoClientMapper(item));
                                    });
                                    pager().totalCount(0);
                                    pager().totalCount(data.TotalCount);
                                },
                                error: function() {
                                    toastr.error("Failed to load Ad Campaigns!");
                                }
                            });
                    },
                    // Close Editor 
                    closeEditDialog = function() {
                        // Ask for confirmation
                        //confirmation.afterProceed(function() {
                          
                        //});
                        //confirmation.show();
                        selectedCampaign(undefined);
                        isEditorVisible(false);
                        $("#topArea").css("display", "block");
                        $("#divApprove").css("display", "block");
                    },
                    // On editing of existing AdCampaign
                    onEditCampaign = function (item) {
                        $("#topArea").css("display", "none");
                        $("#divApprove").css("display", "none");
                        selectedCampaign(item);
                        isEditorVisible(true);
                    },                  
                    // Save AdCampaign 
                    onSaveCampaign = function () {
                        
                        var campId = selectedCampaign().id();
                        dataservice.saveAdCampaign(selectedCampaign().convertToServerData(), {
                            success: function (response) {
                                
                                if (response.indexOf("Failed") == -1) {
                                    dataservice.sendApprovalRejectionEmail(selectedCampaign().convertToServerData(), {
                                        success: function (obj) {
                                            getCampaigns();
                                            //var existingCampaigntodelete = $.grep(campaigns(), function (n, i) {
                                            //    return (n.id() == campId);
                                            //});

                                            //campaigns.remove(existingCampaigntodelete);
                                            
                                            isEditorVisible(false);
                                        },
                                        error: function () {
                                            toastr.error("Failed to save!");
                                        }
                                    });
                                } else {
                                    toastr.error(response);
                                }
                            },
                            error: function() {
                                toastr.error("Failed to save!");
                            }
                        });
                    },                   
                    // Has Changes
                    hasChangesOnCampaign = ko.computed(function() {
                        if (selectedCampaign() == undefined) {
                            return false;
                        }
                        return (selectedCampaign().hasChanges());
                    }),
                    onApproveCampaign = function () {
                        confirmation.messageText("Do you want to Approve this vidio ad campaign ? system will attempt to collect payment and generate invoice");
                        confirmation.show();
                        confirmation.afterCancel(function () {
                            confirmation.hide();
                        });
                        confirmation.afterProceed(function () {
                            selectedCampaign().isApproved(true) ;
                            onSaveCampaign();
                        });
                    },
                    // Reject buttoin handler 
                    onRejectCampaign = function () {
                        confirmation.messageText("Do you want to Reject this vidio ad campaign ?");
                        confirmation.show();
                        confirmation.afterCancel(function () {
                            confirmation.hide();
                        });
                        confirmation.afterProceed(function () {
                            if (selectedCampaign().rejectionReason() == undefined || selectedCampaign().rejectionReason() == "" || selectedCampaign().rejectionReason() == " ") {
                                toastr.info("Please add rejection reason!");
                                return false;
                            }
                            selectedCampaign().isApproved(false);
                            onSaveCampaign();
                        });
                     
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, campaigns, getCampaigns));
                        var mode = getParameter(window.location.href, "mode"); 
                        if (mode == 2) {
                            isShowCopounMode(true);
                        }
                        // First request for LV
                        var type = $('#typeParam').val();
                        if (type == 1) {
                            lblPageTitle("Ad Campaigns For Approval");
                            $("#btnVideaAddApprov").css("background-color", "#34b9c7");
                        }
                        else {
                            lblPageTitle("Display Ads For Approval");
                            $("#btnsponserGameApprov").css("background-color", "#34b9c7");
                        }
                        adType(type);
                        getCampaigns();
                    };
                return {
                    initialize: initialize,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    campaigns: campaigns,
                    getCampaigns: getCampaigns,
                    isEditorVisible: isEditorVisible,
                    closeEditDialog: closeEditDialog,
                    onEditCampaign: onEditCampaign,
                    selectedCampaign: selectedCampaign,
                    onSaveCampaign: onSaveCampaign,
                    onApproveCampaign : onApproveCampaign,
                    hasChangesOnCampaign: hasChangesOnCampaign,
                    onRejectCampaign: onRejectCampaign,
                    lblPageTitle: lblPageTitle,
                    isShowCopounMode: isShowCopounMode
                };
            })()
        };
        return ist.AdCampaign.viewModel;
    });
