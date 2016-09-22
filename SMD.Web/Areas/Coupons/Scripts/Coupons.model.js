define(["ko", "underscore", "underscore-ko"], function (ko) {
  
    var // ReSharper disable InconsistentNaming
      Coupon = function (FinePrintLine1, FinePrintLine2, FinePrintLine3, FinePrintLine4, FinePrintLine5, GeographyColumn, HighlightLine1, HighlightLine2, HighlightLine3, HighlightLine4,
            HighlightLine5, HowToRedeemLine1, HowToRedeemLine2, HowToRedeemLine3, HowToRedeemLine4, HowToRedeemLine5, LanguageId, LocationBranchId, LocationCity, LocationLAT,
            LocationLine1, LocationLine2, LocationLON, LocationPhone, LocationState, LocationTitle, LocationZipCode, LogoUrl, ModifiedBy, ModifiedDateTime, Price, RejectedBy,
            Rejecteddatetime, RejectedReason, Savings, SearchKeywords, Status, SwapCost, UserId, CouponTitle, CouponExpirydate, CouponQtyPerUser, CouponId, couponImage1, CouponImage2, CouponImage3,
            CurrencyId, CouponListingMode, CouponActiveMonth, CouponActiveYear, CouponRedeemedCount, CouponViewCount, CouponIssuedCount, SubmissionDateTime, LocationCountryId, CouponStartDate, CouponEndDate, Priority,
            ShowBuyitBtn, BuyitLandingPageUrl, BuyitBtnLabel
          ) {
          var
              //type and userID will be set on server sside
              ApprovalDateTime = ko.observable(ApprovalDateTime),
              Approved = ko.observable(Approved),
              ApprovedBy = ko.observable(ApprovedBy),
              Archived = ko.observable(Archived),
              LocationCountryId = ko.observable(LocationCountryId),
              CompanyId = ko.observable(CompanyId),
              CouponCategories = ko.observableArray([]),
              CouponActiveMonth = ko.observable(CouponActiveMonth),
              CouponActiveMonthName = ko.computed(function () {

                  return getMonthName(CouponActiveMonth());
              }),
              SubmissionDateTime = ko.observable(SubmissionDateTime),
              CouponActiveYear = ko.observable(CouponActiveYear),
              CouponExpirydate = ko.observable((CouponExpirydate !== null && CouponExpirydate !== undefined) ? moment(CouponExpirydate).toDate() : undefined),//ko.observable(),
              CouponId = ko.observable(CouponId),
              couponImage1 = ko.observable(couponImage1),
              CouponImage2 = ko.observable(CouponImage2),
              CouponImage3 = ko.observable(CouponImage3),
              CouponIssuedCount = ko.observable(CouponIssuedCount),
              CouponListingMode = ko.observable(CouponListingMode),
              CouponQtyPerUser = ko.observable(CouponQtyPerUser).extend({ required: true}),
              CouponRedeemedCount = ko.observable(CouponRedeemedCount),
              CouponTitle = ko.observable(CouponTitle),
              CouponViewCount = ko.observable(CouponViewCount),
              CreatedBy = ko.observable(CreatedBy),
              CreatedDateTime = ko.observable(CreatedDateTime),
              CurrencyId = ko.observable(CurrencyId),
              FinePrintLine1 = ko.observable(FinePrintLine1),
              FinePrintLine2 = ko.observable(FinePrintLine2),
              FinePrintLine3 = ko.observable(FinePrintLine3),
              FinePrintLine4 = ko.observable(FinePrintLine4),
              FinePrintLine5 = ko.observable(FinePrintLine5),
              GeographyColumn = ko.observable(GeographyColumn),
              HighlightLine1 = ko.observable(HighlightLine1),
              HighlightLine2 = ko.observable(HighlightLine2),
              HighlightLine3 = ko.observable(HighlightLine3),
              HighlightLine4 = ko.observable(HighlightLine4),
              HighlightLine5 = ko.observable(HighlightLine5),
              HowToRedeemLine1 = ko.observable(HowToRedeemLine1),
              HowToRedeemLine2 = ko.observable(HowToRedeemLine2),
              HowToRedeemLine3 = ko.observable(HowToRedeemLine3),
              HowToRedeemLine4 = ko.observable(HowToRedeemLine4),
              HowToRedeemLine5 = ko.observable(HowToRedeemLine5),
              LanguageId = ko.observable(LanguageId),
              LocationBranchId = ko.observable(LocationBranchId),
              LocationCity = ko.observable(LocationCity),
              LocationLAT = ko.observable(LocationLAT),
              LocationLine1 = ko.observable(LocationLine1),
              LocationLine2 = ko.observable(LocationLine2),
              LocationLON = ko.observable(LocationLON),
              LocationPhone = ko.observable(LocationPhone),
              LocationState = ko.observable(LocationState),
              LocationTitle = ko.observable(LocationTitle),
              LocationZipCode = ko.observable(LocationZipCode),
              LogoUrl = ko.observable(LogoUrl),
              ModifiedBy = ko.observable(ModifiedBy),
              ModifiedDateTime = ko.observable(ModifiedDateTime),
              Price = ko.observable(Price),
              RejectedBy = ko.observable(RejectedBy),
              Rejecteddatetime = ko.observable(Rejecteddatetime),
              RejectedReason = ko.observable(RejectedReason),
              Savings = ko.observable(Savings),
              SearchKeywords = ko.observable(SearchKeywords),
              Status = ko.observable(Status),
              StatusValue = ko.observable(""),
              SwapCost = ko.observable(SwapCost),
              UserId = ko.observable(UserId),
              LogoImageBytes = ko.observable(LogoUrl),
              
              CouponStartDate = ko.observable((CouponStartDate !== null && CouponStartDate !== undefined) ? moment(CouponStartDate).toDate() : undefined),//ko.observable(),

              CouponEndDate = ko.observable((CouponEndDate !== null && CouponEndDate !== undefined) ? moment(CouponEndDate).toDate() : undefined),//ko.observable(),
              Priority = ko.observable(Priority),

              ShowBuyitBtn = ko.observable(ShowBuyitBtn),
              BuyitLandingPageUrl = ko.observable(BuyitLandingPageUrl),
              BuyitBtnLabel = ko.observable(BuyitBtnLabel),






               // Errors
              errors = ko.validation.group({ }),
                // Is Valid 
             isValid = ko.computed(function () {
                    return errors().length === 0 ? true : false;
             }),
             dirtyFlag = new ko.dirtyFlag({
                 ApprovalDateTime : (ApprovalDateTime),
                 Approved : (Approved),
                 ApprovedBy : (ApprovedBy),
                 Archived : (Archived),
                 CompanyId : (CompanyId),
                 CouponActiveMonth: (CouponActiveMonth),
                 SubmissionDateTime:SubmissionDateTime,
                 CouponActiveYear : (CouponActiveYear),
                 CouponExpirydate : (CouponExpirydate),
                 CouponId : (CouponId),
                 couponImage1 : (couponImage1),
                 CouponImage2 : (CouponImage2),
                 CouponImage3 : (CouponImage3),
                 CouponIssuedCount : (CouponIssuedCount),
                 CouponListingMode : (CouponListingMode),
                 CouponQtyPerUser : (CouponQtyPerUser),
                 CouponRedeemedCount: (CouponRedeemedCount),
                 CouponTitle : (CouponTitle),
                 CouponViewCount : (CouponViewCount),
                 CreatedBy : (CreatedBy),
                 CreatedDateTime : (CreatedDateTime),
                 CurrencyId : (CurrencyId),
                 FinePrintLine1 : (FinePrintLine1),
                 FinePrintLine2 : (FinePrintLine2),
                 FinePrintLine3 : (FinePrintLine3),
                 FinePrintLine4 : (FinePrintLine4),
                 FinePrintLine5 : (FinePrintLine5),
                 GeographyColumn : (GeographyColumn),
                 HighlightLine1 : (HighlightLine1),
                 HighlightLine2 : (HighlightLine2),
                 HighlightLine3 : (HighlightLine3),
                 HighlightLine4 : (HighlightLine4),
                 HighlightLine5 : (HighlightLine5),
                 HowToRedeemLine1 : (HowToRedeemLine1),
                 HowToRedeemLine2 : (HowToRedeemLine2),
                 HowToRedeemLine3 : (HowToRedeemLine3),
                 HowToRedeemLine4 : (HowToRedeemLine4),
                 HowToRedeemLine5 : (HowToRedeemLine5),
                 LanguageId : (LanguageId),
                 LocationBranchId : (LocationBranchId),
                 LocationCity : (LocationCity),
                 LocationLAT : (LocationLAT),
                 LocationLine1 : (LocationLine1),
                 LocationLine2 : (LocationLine2),
                 LocationLON : (LocationLON),
                 LocationPhone : (LocationPhone),
                 LocationState : (LocationState),
                 LocationTitle : (LocationTitle),
                 LocationZipCode : (LocationZipCode),
                 LogoUrl : (LogoUrl),
                 ModifiedBy : (ModifiedBy),
                 ModifiedDateTime : (ModifiedDateTime),
                 Price : (Price),
                 RejectedBy : (RejectedBy),
                 Rejecteddatetime : (Rejecteddatetime),
                 RejectedReason : (RejectedReason),
                 Savings : (Savings),
                 SearchKeywords : (SearchKeywords),
                 Status: (Status),
                 StatusValue :StatusValue ,
                 SwapCost : (SwapCost),
                 UserId: (UserId),
                 LogoImageBytes: LogoImageBytes,
                 LocationCountryId: LocationCountryId,
                 Priority: Priority,
                 CouponStartDate: CouponStartDate,
                 CouponEndDate: CouponEndDate,

                ShowBuyitBtn : ShowBuyitBtn,
                BuyitLandingPageUrl : BuyitLandingPageUrl,
                BuyitBtnLabel: BuyitBtnLabel

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
                  
                  var selectedCoupons = [];
                  _.each(CouponCategories(), function (item) {

                      selectedCoupons.push(item.convertToServerData());
                  });
                  return {
                      ApprovalDateTime: ApprovalDateTime(),
                      Approved: Approved(),
                      ApprovedBy: ApprovedBy(),
                      Archived: Archived(),
                      CompanyId: CompanyId(),
                      CouponActiveMonth: CouponActiveMonth(),
                      
                      CouponActiveYear: CouponActiveYear(),
                      CouponExpirydate: moment(CouponExpirydate()).format(ist.utcFormat) + 'Z',
                      CouponId: CouponId(),
                      couponImage1: bannerImage1 == "" ? couponImage1() : bannerImage1,
                      CouponImage2: bannerImage2 == "" ? CouponImage2() : bannerImage2,
                      CouponImage3: bannerImage3 == "" ? CouponImage3() : bannerImage3,
                      CouponIssuedCount: CouponIssuedCount(),
                      CouponListingMode: CouponListingMode(),
                      CouponQtyPerUser: CouponQtyPerUser(),
                      CouponRedeemedCount: CouponRedeemedCount(),
                      CouponTitle: CouponTitle(),
                      CouponViewCount: CouponViewCount(),
                      CreatedBy: CreatedBy(),
                      SubmissionDateTime:SubmissionDateTime(),
                      CreatedDateTime: CreatedDateTime(),
                      CurrencyId: CurrencyId(),
                      FinePrintLine1: FinePrintLine1(),
                      FinePrintLine2: FinePrintLine2(),
                      FinePrintLine3: FinePrintLine3(),
                      FinePrintLine4: FinePrintLine4(),
                      FinePrintLine5: FinePrintLine5(),
                      GeographyColumn: GeographyColumn(),
                      HighlightLine1: HighlightLine1(),
                      HighlightLine2: HighlightLine2(),
                      HighlightLine3: HighlightLine3(),
                      HighlightLine4: HighlightLine4(),
                      HighlightLine5: HighlightLine5(),
                      HowToRedeemLine1: HowToRedeemLine1(),
                      HowToRedeemLine2: HowToRedeemLine2(),
                      HowToRedeemLine3: HowToRedeemLine3(),
                      HowToRedeemLine4: HowToRedeemLine4(),
                      HowToRedeemLine5: HowToRedeemLine5(),
                      LanguageId: LanguageId(),
                      LocationCountryId:LocationCountryId(),
                      LocationBranchId: LocationBranchId(),
                      LocationCity: LocationCity(),
                      LocationLAT: LocationLAT(),
                      LocationLine1: LocationLine1(),
                      LocationLine2: LocationLine2(),
                      LocationLON: LocationLON(),
                      LocationPhone: LocationPhone(),
                      LocationState: LocationState(),
                      LocationTitle: LocationTitle(),
                      LocationZipCode: LocationZipCode(),
                      LogoUrl: logoImage == "" ? LogoUrl() : logoImage,
                      ModifiedBy: ModifiedBy(),
                      ModifiedDateTime: ModifiedDateTime(),
                      Price: Price(),
                      RejectedBy: RejectedBy(),
                      Rejecteddatetime: Rejecteddatetime(),
                      RejectedReason: RejectedReason(),
                      Savings: Savings(),
                      SearchKeywords: SearchKeywords(),
                      Status: Status(),
                      StatusValue :StatusValue (),
                      SwapCost: SwapCost(),
                      UserId: UserId(),
                      LogoImageBytes: LogoImageBytes(),
                      hasChanges: hasChanges(),
                      CouponCategories: selectedCoupons,
                      CouponStartDate: moment(CouponStartDate()).format(ist.utcFormat) + 'Z',
                      CouponEndDate: moment(CouponEndDate()).format(ist.utcFormat) + 'Z',
                      Priority: Priority(),
                      ShowBuyitBtn: ShowBuyitBtn(),
                      BuyitLandingPageUrl: BuyitLandingPageUrl(),
                      BuyitBtnLabel: BuyitBtnLabel()
                      
                  };
              };
          return {
              ApprovalDateTime: (ApprovalDateTime),
              Approved: (Approved),
              ApprovedBy: (ApprovedBy),
              Archived: (Archived),
              CompanyId: (CompanyId),
              CouponActiveMonth: (CouponActiveMonth),
              CouponActiveMonthName:(CouponActiveMonthName),
              CouponActiveYear: (CouponActiveYear),
              CouponExpirydate: (CouponExpirydate),
              CouponId: (CouponId),
              couponImage1: (couponImage1),
              CouponImage2: (CouponImage2),
              CouponImage3: (CouponImage3),
              CouponIssuedCount: (CouponIssuedCount),
              CouponListingMode: (CouponListingMode),
              CouponQtyPerUser: (CouponQtyPerUser),
              CouponRedeemedCount: (CouponRedeemedCount),
              CouponTitle: (CouponTitle),
              CouponViewCount: (CouponViewCount),
              CreatedBy: (CreatedBy),
              CreatedDateTime: (CreatedDateTime),
              CurrencyId: (CurrencyId),
              FinePrintLine1: (FinePrintLine1),
              FinePrintLine2: (FinePrintLine2),
              FinePrintLine3: (FinePrintLine3),
              FinePrintLine4: (FinePrintLine4),
              FinePrintLine5: (FinePrintLine5),
              GeographyColumn: (GeographyColumn),
              HighlightLine1: (HighlightLine1),
              HighlightLine2: (HighlightLine2),
              HighlightLine3: (HighlightLine3),
              HighlightLine4: (HighlightLine4),
              HighlightLine5: (HighlightLine5),
              HowToRedeemLine1: (HowToRedeemLine1),
              HowToRedeemLine2: (HowToRedeemLine2),
              HowToRedeemLine3: (HowToRedeemLine3),
              HowToRedeemLine4: (HowToRedeemLine4),
              HowToRedeemLine5: (HowToRedeemLine5),
              LanguageId: (LanguageId),
              LocationBranchId: (LocationBranchId),
              LocationCity: (LocationCity),
              LocationLAT: (LocationLAT),
              LocationLine1: (LocationLine1),
              LocationLine2: (LocationLine2),
              LocationLON: (LocationLON),
              LocationPhone: (LocationPhone),
              LocationState: (LocationState),
              LocationTitle: (LocationTitle),
              LocationZipCode: (LocationZipCode),
              LogoUrl: (LogoUrl),
              ModifiedBy: (ModifiedBy),
              ModifiedDateTime: (ModifiedDateTime),
              Price: (Price),
              RejectedBy: (RejectedBy),
              Rejecteddatetime: (Rejecteddatetime),
              RejectedReason: (RejectedReason),
              Savings: (Savings),
              SearchKeywords: (SearchKeywords),
              Status: (Status),
              StatusValue :StatusValue ,
              SwapCost: (SwapCost),
              UserId: (UserId),
              LogoImageBytes: LogoImageBytes,
              hasChanges: hasChanges,
              convertToServerData: convertToServerData,
              CouponCategories: CouponCategories,
              SubmissionDateTime: (SubmissionDateTime),
              LocationCountryId: (LocationCountryId),
              CouponStartDate: (CouponStartDate),
              CouponEndDate: (CouponEndDate),
              Priority: (Priority),
              ShowBuyitBtn : (ShowBuyitBtn),
              BuyitLandingPageUrl : (BuyitLandingPageUrl),
              BuyitBtnLabel: (BuyitBtnLabel),
              reset: (reset)
          };
      };

   
    // Factory Method
    Coupon.Create = function (source) {
        
        var coupon = new Coupon(source.FinePrintLine1, source.FinePrintLine2, source.FinePrintLine3, source.FinePrintLine4, source.FinePrintLine5,
            source.GeographyColumn, source.HighlightLine1, source.HighlightLine2, source.HighlightLine3, source.HighlightLine4,
            source.HighlightLine5, source.HowToRedeemLine1, source.HowToRedeemLine2, source.HowToRedeemLine3, source.HowToRedeemLine4,
            source.HowToRedeemLine5, source.LanguageId, source.LocationBranchId, source.LocationCity, source.LocationLAT,
            source.LocationLine1, source.LocationLine2, source.LocationLON, source.LocationPhone, source.LocationState,
            source.LocationTitle, source.LocationZipCode, source.LogoUrl, source.ModifiedBy, source.ModifiedDateTime, source.Price, source.RejectedBy,
            source.Rejecteddatetime, source.RejectedReason, source.Savings, source.SearchKeywords, source.Status, source.SwapCost, source.UserId,source.CouponTitle,source.CouponExpirydate,
            source.CouponQtyPerUser, source.CouponId, source.couponImage1, source.CouponImage2, source.CouponImage3, source.CurrencyId, source.CouponListingMode, source.CouponActiveMonth, source.CouponActiveYear, source.CouponRedeemedCount, source.CouponViewCount, source.CouponIssuedCount, source.SubmissionDateTime, source.LocationCountryId, source.CouponStartDate, source.CouponEndDate, source.Priority
            , source.ShowBuyitBtn, source.BuyitLandingPageUrl, source.BuyitBtnLabel
            );

        _.each(source.CouponCategories, function (item) {

            coupon.CouponCategories.push(selectedCouponCategory.Create(item));
        });

        return coupon;
    };
    selectedCouponCategory = function (CategoryId, Name) {

        var
            //type and userID will be set on server sside
            CategoryId = ko.observable(CategoryId),
            Name = ko.observable(Name),
            IsSelected = ko.observable(),
            // Convert to server data
            convertToServerData = function () {
                return {
                    CategoryId: CategoryId(),
                    Name: Name()
                };
            };
        return {
            CategoryId: CategoryId,
            Name: Name,
            IsSelected: IsSelected,
            convertToServerData: convertToServerData
        };
    };
    // Factory Method
    selectedCouponCategory.Create = function (source) {

        return new selectedCouponCategory(source.CategoryId, source.Name);
    };
    return {
        Coupon: Coupon,
        selectedCouponCategory: selectedCouponCategory,
    };
});