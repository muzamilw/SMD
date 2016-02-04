/*
    Module with the view model for the AdCampaign
*/
define("addApproval/addApproval.viewModel",
    ["jquery", "amplify", "ko", "addApproval/addApproval.dataservice", "addApproval/addApproval.model", "common/pagination",
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
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                    //selected AdCampaign
                    selectedCampaign = ko.observable(),
                    //Get AdCampaign
                    getCampaigns = function() {
                        dataservice.searchAdCampaigns(
                            {
                                PageSize: pager().pageSize(),
                                PageNo: pager().currentPage(),
                                SortBy: sortOn(),
                                IsAsc: sortIsAsc()
                            },
                            {
                                success: function(data) {
                                    campaigns.removeAll();
                                    pager().totalCount(0);
                                    pager().totalCount(data.TotalCount);
                                    _.each(data.AdCampaigns, function (item) {
                                        campaigns.push(model.AdCampaignServertoClientMapper(item));
                                    });
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
                    },
                    // On editing of existing AdCampaign
                    onEditCampaign = function(item) {
                        selectedCampaign(item);
                        isEditorVisible(true);
                    },                  
                    // Save AdCampaign 
                    onSaveCampaign = function() {
                        dataservice.saveAdCampaign(selectedCampaign().convertToServerData(), {
                            success: function(obj) {
                                var newObjtodelete = campaigns.find(function(temp) {
                                    return obj.CampaignId == temp.id();
                                });
                                campaigns.remove(newObjtodelete);
                                toastr.success("You are Good!");
                                isEditorVisible(false);
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
                        selectedCampaign().isApproved(true) ;
                        onSaveCampaign();
                    },
                    // Reject buttoin handler 
                    onRejectCampaign = function() {
                        if (selectedCampaign().rejectionReason() == undefined || selectedCampaign().rejectionReason() == "" || selectedCampaign().rejectionReason() == " ") {
                            toastr.info("Please add rejection reason!");
                            return false;
                        }
                        selectedCampaign().isApproved(false);
                        onSaveCampaign();
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, campaigns, getCampaigns));
                       
                        // First request for LV
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
                    onRejectCampaign: onRejectCampaign
                };
            })()
        };
        return ist.AdCampaign.viewModel;
    });
