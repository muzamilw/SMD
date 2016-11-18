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
                    selectedCompany = ko.observable(),
                    //sorting
                    sortOn = ko.observable(5),
                    adType = ko.observable(1),
                    company = ko.observable(),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                    // Controlls editor visibility 
                    isEditorVisible = ko.observable(false),
                    isShowCopounMode = ko.observable(false),
                    // variables 
                    lblPageTitle = ko.observable(null),
                    //selected AdCampaign
                    selectedCampaign = ko.observable(),
                    confrmText = ko.observable(),
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
                                    getApprovalCount();
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
                        getCompanyData(item);
                       // isEditorVisible(true);
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
                    getCompanyData = function (selectedItem) {
                          dataservice.getCompanyData(
                       {
                           companyId: selectedItem.companyId,
                           userId: selectedItem.userID,
                       },
                       {
                           success: function (comData) {
                               selectedCompany(comData);
                               var cType = companyTypes().find(function (item) {
                                   return comData.CompanyType === item.Id;
                               });
                               if (cType != undefined)
                                   company(cType.Name);
                               else
                                   company(null);
                               isEditorVisible(true);

                           },
                           error: function () {
                               toastr.error("Failed to load Company");
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
                        confirmation.messageText(confrmText() + "<br\>" + "System will attempt to collect payment and generate invoice");
                        confirmation.show();
                        confirmation.afterCancel(function () {
                            confirmation.hide();
                        });
                        confirmation.afterProceed(function () {
                            selectedCampaign().isApproved(true) ;
                            onSaveCampaign();
                            $("#topArea").css("display", "block");
                            $("#divApprove").css("display", "block");
                        });
                    },
                    companyTypes = ko.observableArray([
                    { Id: 1, Name: 'Amusement, Gambling, and Recreation Industries' },
                    { Id: 2, Name: 'Arts, Entertainment, and Recreation' },
                    { Id: 3, Name: 'Broadcasting (except Internet)' },
                    { Id: 4, Name: 'Building Material and Garden Equipment and Supplies Dealers' },
                    { Id: 5, Name: 'Clothing and Clothing Accessories Stores' },
                    { Id: 6, Name: 'Computer and Electronics' },
                    { Id: 7, Name: 'Construction' },
                    { Id: 8, Name: 'Couriers and Messengers' },
                    { Id: 9, Name: 'Data Processing, Hosting, and Related Services' },
                    { Id: 10, Name: 'Health Services' },
                    { Id: 11, Name: 'Educational Services' },
                    { Id: 12, Name: 'Electronics and Appliance Stores' },
                    { Id: 13, Name: 'Finance and Insurance' },
                    { Id: 14, Name: 'Food Services' },
                    { Id: 15, Name: 'Food and Beverage Stores' },
                    { Id: 16, Name: 'Furniture and Home Furnishings Stores' },
                    { Id: 17, Name: 'General Merchandise Stores' },
                    { Id: 18, Name: 'Health Care' },
                    { Id: 19, Name: 'Internet Publishing and Broadcasting' },
                    { Id: 20, Name: 'Leisure and Hospitality' },
                    { Id: 21, Name: 'Manufacturing' },
                    { Id: 22, Name: 'Merchant Wholesalers ' },
                    { Id: 23, Name: 'Motor Vehicle and Parts Dealers ' },
                    { Id: 24, Name: 'Museums, Historical Sites, and Similar Institutions ' },
                    { Id: 25, Name: 'Performing Arts, Spectator Sports' },
                    { Id: 26, Name: 'Printing Services' },
                    { Id: 27, Name: 'Professional and Business Services' },
                    { Id: 28, Name: 'Real Estate' },
                    { Id: 29, Name: 'Repair and Maintenance' },
                    { Id: 30, Name: 'Scenic and Sightseeing Transportation' },
                    { Id: 31, Name: 'Service-Providing Industries' },
                    { Id: 32, Name: 'Social Assistance' },
                    { Id: 33, Name: 'Sporting Goods, Hobby, Book, and Music Stores' },
                    { Id: 34, Name: 'Telecommunications' },
                    { Id: 35, Name: 'Transportation' },
                    { Id: 36, Name: 'Utilities' }
                      ]),
                    // Reject buttoin handler 
                    onRejectCampaign = function () {
                        confirmation.messageText("Do you want to Reject this video ad campaign ?");
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
                            $("#topArea").css("display", "block");
                            $("#divApprove").css("display", "block");
                        });
                     
                    },
                      getApprovalCount = function () {
                          dataservice.getapprovalCount({
                              success: function (data) {
                                  $('#couponCount').text(data.CouponCount);
                                  $('#vidioAdCount').text(data.AdCmpaignCount);
                                  $('#displayAdCount').text(data.DisplayAdCount);
                                  $('#surveyCount').text(data.SurveyQuestionCount);
                                  $('#profileCount').text(data.ProfileQuestionCount);

                              },
                              error: function () {
                                  toastr.error("Failed to load Approval Count.");
                              }
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
                            confrmText("Do you want to Approve this video ad campaign ?");
                            lblPageTitle("Video Ads For Approval");
                            $("#btnVideaAddApprov").css("background-color", "#34b9c7");
                        }
                        else {
                            confrmText("Do you want to Approve this display ad campaign ?");
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
                    adType:adType,
                    isShowCopounMode: isShowCopounMode,
                    selectedCompany: selectedCompany,
                    getApprovalCount: getApprovalCount,
                    companyTypes: companyTypes,
                    company: company,
                };
            })()
        };
        return ist.AdCampaign.viewModel;
    });
