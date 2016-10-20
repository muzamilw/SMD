﻿/*
    Module with the view model for the Registered Users
*/
define("FranchiseDashboard/registeredUsers.viewModel",
    ["jquery", "amplify", "ko", "FranchiseDashboard/registeredUsers.dataservice", "FranchiseDashboard/registeredUsers.model", "common/pagination",
     "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation) {
        var ist = window.ist || {};
        ist.RegisteredUsers = {
            viewModel: (function () {
                var view,
                    registeredUsers = ko.observableArray([]),
                    company = ko.observable(),
                   
                    //pager
                    pager = ko.observable(),
                    //sorting
                    sortOn = ko.observable(5),
                    //Assending  / Desending
                    sortIsAsc = ko.observable(true),
                   
                    getRegisteredUsers = function () {
                       
                        dataservice.getpayOutHistoryForApproval(
                             {
                                 PageSize: pager().pageSize(),
                                 PageNo: pager().currentPage(),
                                 SortBy: sortOn(),
                                 IsAsc: sortIsAsc(),
                                 UserRole: UserRole

                             },
                             {
                                 success: function (data) {
                                  
                                     ////pager().totalCount(0);
                                     //pager().totalCount(data.TotalCount);
                                 },
                                 error: function (msg) {
                                     toastr.error("Failed to load data" + msg);
                                 }
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
                   

                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, registeredUsers, getRegisteredUsers));
                        var mode = getParameter(window.location.href, "mode");
                        //if (mode == 2) {
                        //    lblPageTitle("Coupons For Approval");
                        //    isShowCopounMode(true);
                        //}
                        //// First request for LV
                        getRegisteredUsers();
                    };
                return {

                    initialize: initialize,
                    registeredUsers: registeredUsers,
                    pager: pager,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    getRegisteredUsers: getRegisteredUsers,
                    companyTypes: companyTypes,
                    company: company




                };
            })()
        };
        return ist.RegisteredUsers.viewModel;
    });