define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      Coupon = function (ApprovalDateTime, FinePrintLine1, FinePrintLine2, FinePrintLine3, FinePrintLine4, FinePrintLine5, GeographyColumn, HighlightLine1, HighlightLine2, HighlightLine3, HighlightLine4,
            HighlightLine5, HowToRedeemLine1, HowToRedeemLine2, HowToRedeemLine3, HowToRedeemLine4, HowToRedeemLine5, LanguageId, LocationBranchId, LocationCity, LocationLAT,
            LocationLine1, LocationLine2, LocationLON, LocationPhone, LocationState, LocationTitle, LocationZipCode, LogoUrl, ModifiedBy, ModifiedDateTime, Price, RejectedBy,
            Rejecteddatetime, RejectedReason, Savings, SearchKeywords, Status, SwapCost, UserId, CouponTitle, CouponExpirydate, CouponQtyPerUser, CouponId, couponImage1, CouponImage2, CouponImage3,
            CurrencyId, couponListingMode, CouponActiveMonth, CouponActiveYear, CouponRedeemedCount, CouponViewCount, CouponIssuedCount, SubmissionDateTime, LocationCountryId, CouponStartDate, CouponEndDate, Priority,
            ShowBuyitBtn, BuyitLandingPageUrl, BuyitBtnLabel, YoutubeLink, CouponImage4, CouponImage5, CouponImage6, IsPaymentCollected, PaymentDate, IsShowReviews, IsShowAddress, IsShowPhoneNo, IsShowMap, IsShowyouTube, IsShowAboutUs, DealsinGroupCount, IsPerSaving3days, IsPerSaving2days, IsPerSavingLastday, IsDollarSaving3days, IsDollarSaving2days, IsDollarSavingLastday, LastModifiedDate, isSaveBtnLable
          ) {
          var
              //type and userID will be set on server sside
              ApprovalDateTime = ko.observable(ApprovalDateTime),
              Approved = ko.observable(Approved),
              ApprovedBy = ko.observable(ApprovedBy),
              Archived = ko.observable(Archived),
              LocationCountryId = ko.observable(LocationCountryId),
              CompanyId = ko.observable(CompanyId),
              CouponImage4 = ko.observable(CouponImage4),
              CouponImage5 = ko.observable(CouponImage5),
              CouponImage6 = ko.observable(CouponImage6),
              IsPaymentCollected = ko.observable(IsPaymentCollected),
              PaymentDate = ko.observable(PaymentDate),
              CouponCategories = ko.observableArray([]),
              CouponActiveMonth = ko.observable(CouponActiveMonth),
              CouponActiveMonthName = ko.computed(function () {

                  return getMonthName(CouponActiveMonth());
              }),
              YoutubeLink = ko.observable(YoutubeLink),
              SubmissionDateTime = ko.observable(SubmissionDateTime),
              CouponActiveYear = ko.observable(CouponActiveYear),
              CouponExpirydate = ko.observable((CouponExpirydate !== null && CouponExpirydate !== undefined) ? moment(CouponExpirydate).toDate() : undefined),//ko.observable(),
              CouponId = ko.observable(CouponId),
              couponImage1 = ko.observable(couponImage1),
              CouponImage2 = ko.observable(CouponImage2),
              CouponImage3 = ko.observable(CouponImage3),
              lastModified = ko.observable(LastModifiedDate),
              CouponIssuedCount = ko.observable(CouponIssuedCount),
              CouponListingMode = ko.observable(couponListingMode == 1 ? "1" : "2"),
              CouponQtyPerUser = ko.observable(CouponQtyPerUser).extend({ required: true }),
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


              LogoUrl = ko.observable(LogoUrl != null ? LogoUrl.startsWith('http') ? LogoUrl : '/' + LogoUrl : null),

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
              CouponPriceOptions = ko.observableArray([]),
              IsShowReviews = ko.observable(IsShowReviews),
              IsShowAddress = ko.observable(IsShowAddress),
              IsShowPhoneNo = ko.observable(IsShowPhoneNo),
              IsShowMap = ko.observable(IsShowMap),
              IsShowyouTube = ko.observable(IsShowyouTube),
              IsShowAboutUs = ko.observable(IsShowAboutUs),
              DealsinGroupCount = ko.observable(DealsinGroupCount),
              IsPerSaving3days = ko.observable(IsPerSaving3days),
              IsPerSaving2days = ko.observable(IsPerSaving2days),
              IsPerSavingLastday = ko.observable(IsPerSavingLastday),
              IsDollarSaving3days = ko.observable(IsDollarSaving3days),
              IsDollarSaving2days = ko.observable(IsDollarSaving2days),
              IsDollarSavingLastday = ko.observable(IsDollarSavingLastday),
              isSaveBtnLable = ko.observable(isSaveBtnLable ==1 ? "1" :isSaveBtnLable == 2 ? "2" : isSaveBtnLable == 3 ? "3" :null),
    

               // Errors
          errors = ko.validation.group({
              CouponTitle: CouponTitle
          }),
                // Is Valid 
             isValid = ko.computed(function () {
                 return errors().length === 0 ? true : false;
             }),
             dirtyFlag = new ko.dirtyFlag({
                 YoutubeLink: YoutubeLink,
                 ApprovalDateTime: ApprovalDateTime,
                 Approved: Approved,
                 IsPaymentCollected: IsPaymentCollected,
                 PaymentDate: PaymentDate,
                 ApprovedBy: ApprovedBy,
                 Archived: Archived,
                 CompanyId: CompanyId,
                 CouponActiveMonth: CouponActiveMonth,
                 SubmissionDateTime: SubmissionDateTime,
                 CouponActiveYear: CouponActiveYear,
                 CouponExpirydate: CouponExpirydate,
                 CouponId: CouponId,
                 couponImage1: couponImage1,
                 CouponImage2: CouponImage2,
                 CouponImage3: CouponImage3,
                 CouponIssuedCount: CouponIssuedCount,
                 CouponListingMode: CouponListingMode,
                 CouponQtyPerUser: CouponQtyPerUser,
                 CouponRedeemedCount: CouponRedeemedCount,
                 CouponTitle: CouponTitle,
                 CouponViewCount: CouponViewCount,
                 CreatedBy: CreatedBy,
                 CreatedDateTime: CreatedDateTime,
                 CurrencyId: CurrencyId,
                 FinePrintLine1: FinePrintLine1,
                 FinePrintLine2: FinePrintLine2,
                 FinePrintLine3: FinePrintLine3,
                 FinePrintLine4: FinePrintLine4,
                 FinePrintLine5: FinePrintLine5,
                 GeographyColumn: GeographyColumn,
                 HighlightLine1: HighlightLine1,
                 HighlightLine2: HighlightLine2,
                 HighlightLine3: HighlightLine3,
                 HighlightLine4: HighlightLine4,
                 HighlightLine5: HighlightLine5,
                 HowToRedeemLine1: HowToRedeemLine1,
                 HowToRedeemLine2: HowToRedeemLine2,
                 HowToRedeemLine3: HowToRedeemLine3,
                 HowToRedeemLine4: HowToRedeemLine4,
                 HowToRedeemLine5: HowToRedeemLine5,
                 LanguageId: LanguageId,
                 LocationBranchId: LocationBranchId,
                 LocationCity: LocationCity,
                 LocationLAT: LocationLAT,
                 LocationLine1: LocationLine1,
                 LocationLine2: LocationLine2,
                 LocationLON: LocationLON,
                 LocationPhone: LocationPhone,
                 LocationState: LocationState,
                 LocationTitle: LocationTitle,
                 LocationZipCode: LocationZipCode,
                 LogoUrl: LogoUrl,
                 ModifiedBy: ModifiedBy,
                 ModifiedDateTime: ModifiedDateTime,
                 Price: Price,
                 RejectedBy: RejectedBy,
                 Rejecteddatetime: Rejecteddatetime,
                 RejectedReason: RejectedReason,
                 Savings: Savings,
                 SearchKeywords: SearchKeywords,
                 Status: Status,
                 StatusValue: StatusValue,
                 SwapCost: SwapCost,
                 UserId: UserId,
                 LogoImageBytes: LogoImageBytes,
                 LocationCountryId: LocationCountryId,
                 Priority: Priority,
                 CouponStartDate: CouponStartDate,
                 CouponEndDate: CouponEndDate,

                 ShowBuyitBtn: ShowBuyitBtn,
                 BuyitLandingPageUrl: BuyitLandingPageUrl,
                 BuyitBtnLabel: BuyitBtnLabel,
                 CouponImage4: CouponImage4,
                 CouponImage5: CouponImage5,
                 CouponImage6: CouponImage6,
                 CouponPriceOptions: CouponPriceOptions,
                 IsShowReviews: IsShowReviews,
                 IsShowAddress: IsShowAddress,
                 IsShowPhoneNo: IsShowPhoneNo,
                 IsShowMap: IsShowMap,
                 IsShowyouTube: IsShowyouTube,
                 IsShowAboutUs: IsShowAboutUs,
                 DealsinGroupCount: DealsinGroupCount,
                 IsPerSaving3days: IsPerSaving3days,
                 IsPerSaving2days: IsPerSaving2days,
                 IsPerSavingLastday: IsPerSavingLastday,
                 IsDollarSaving3days: IsDollarSaving3days,
                 IsDollarSaving2days: IsDollarSaving2days,
                 IsDollarSavingLastday: IsDollarSavingLastday,
                 lastModified: lastModified,
                 isSaveBtnLable: isSaveBtnLable,


             }),
    // Has Changes
          CouponhasChanges = ko.computed(function () {
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



              //SubCategories
              var priceOptions = [];
              _.each(CouponPriceOptions(), function (item) {
                  priceOptions.push(item.convertToServerData());
              });

              return {
                  ApprovalDateTime: ApprovalDateTime(),
                  YoutubeLink: YoutubeLink(),
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

                  CouponImage4: bannerImage4 == "" ? CouponImage4() : bannerImage4,
                  CouponImage5: bannerImage5 == "" ? CouponImage5() : bannerImage5,
                  CouponImage6: bannerImage6 == "" ? CouponImage6() : bannerImage6,
                  IsPaymentCollected: IsPaymentCollected(),
                  PaymentDate: PaymentDate(),

                  CouponIssuedCount: CouponIssuedCount(),
                  CouponListingMode: CouponListingMode(),
                  CouponQtyPerUser: CouponQtyPerUser(),
                  CouponRedeemedCount: CouponRedeemedCount(),
                  CouponTitle: CouponTitle(),
                  CouponViewCount: CouponViewCount(),
                  CreatedBy: CreatedBy(),
                  SubmissionDateTime: SubmissionDateTime(),
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
                  LocationCountryId: LocationCountryId(),
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
                  StatusValue: StatusValue(),
                  SwapCost: SwapCost(),
                  UserId: UserId(),
                  LogoImageBytes: LogoImageBytes(),
                  CouponCategories: selectedCoupons,
                  CouponStartDate: moment(CouponStartDate()).format(ist.utcFormat) + 'Z',
                  CouponEndDate: moment(CouponEndDate()).format(ist.utcFormat) + 'Z',
                  Priority: Priority(),
                  ShowBuyitBtn: ShowBuyitBtn(),
                  BuyitLandingPageUrl: BuyitLandingPageUrl(),
                  BuyitBtnLabel: BuyitBtnLabel(),
                  CouponPriceOptions: priceOptions,
                  IsShowReviews: IsShowReviews(),
                  IsShowAddress: IsShowAddress(),
                  IsShowPhoneNo: IsShowPhoneNo(),
                  IsShowMap: IsShowMap(),
                  IsShowyouTube: IsShowyouTube(),
                  IsShowAboutUs: IsShowAboutUs(),
                  DealsinGroupCount: DealsinGroupCount(),
                  IsPerSaving3days: IsPerSaving3days(),
                  IsPerSaving2days: IsPerSaving2days(),
                  IsPerSavingLastday: IsPerSavingLastday(),
                  IsDollarSaving3days: IsDollarSaving3days(),
                  IsDollarSaving2days: IsDollarSaving2days(),
                  IsDollarSavingLastday: IsDollarSavingLastday(),
                  lastModified: lastModified(),
                  isSaveBtnLable: isSaveBtnLable(),

              };
          };
          return {
              ApprovalDateTime: (ApprovalDateTime),
              IsPaymentCollected: (IsPaymentCollected),
              PaymentDate: (PaymentDate),
              Approved: (Approved),
              ApprovedBy: (ApprovedBy),
              Archived: (Archived),
              CompanyId: (CompanyId),
              CouponActiveMonth: (CouponActiveMonth),
              CouponActiveMonthName: (CouponActiveMonthName),
              CouponActiveYear: (CouponActiveYear),
              CouponExpirydate: (CouponExpirydate),
              CouponId: (CouponId),
              couponImage1: (couponImage1),
              CouponImage2: (CouponImage2),
              CouponImage3: (CouponImage3),
              CouponIssuedCount: (CouponIssuedCount),
              CouponListingMode: CouponListingMode,
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
              StatusValue: StatusValue,
              SwapCost: (SwapCost),
              UserId: (UserId),
              LogoImageBytes: LogoImageBytes,
              CouponhasChanges: CouponhasChanges,
              convertToServerData: convertToServerData,
              CouponCategories: CouponCategories,
              SubmissionDateTime: (SubmissionDateTime),
              LocationCountryId: (LocationCountryId),
              CouponStartDate: (CouponStartDate),
              CouponEndDate: (CouponEndDate),
              Priority: (Priority),
              ShowBuyitBtn: (ShowBuyitBtn),
              BuyitLandingPageUrl: (BuyitLandingPageUrl),
              BuyitBtnLabel: (BuyitBtnLabel),
              CouponPriceOptions: (CouponPriceOptions),
              reset: (reset),
              YoutubeLink: (YoutubeLink),
              CouponImage4: (CouponImage4),
              CouponImage5: (CouponImage5),
              CouponImage6: (CouponImage6),
              IsShowReviews: (IsShowReviews),
              IsShowAddress: (IsShowAddress),
              IsShowPhoneNo: (IsShowPhoneNo),
              IsShowMap: (IsShowMap),
              IsShowyouTube: (IsShowyouTube),
              IsShowAboutUs: (IsShowAboutUs),
              DealsinGroupCount: (DealsinGroupCount),
              IsPerSaving3days: (IsPerSaving3days),
              IsPerSaving2days: (IsPerSaving2days),
              IsPerSavingLastday: (IsPerSavingLastday),
              IsDollarSaving3days: (IsDollarSaving3days),
              IsDollarSaving2days: (IsDollarSaving2days),
              IsDollarSavingLastday: (IsDollarSavingLastday),
              lastModified: (lastModified),
              isSaveBtnLable: (isSaveBtnLable),

          };
      };


    // Factory Method
    Coupon.Create = function (source) {

        var coupon = new Coupon(source.ApprovalDateTime, source.FinePrintLine1, source.FinePrintLine2, source.FinePrintLine3, source.FinePrintLine4, source.FinePrintLine5,
            source.GeographyColumn, source.HighlightLine1, source.HighlightLine2, source.HighlightLine3, source.HighlightLine4,
            source.HighlightLine5, source.HowToRedeemLine1, source.HowToRedeemLine2, source.HowToRedeemLine3, source.HowToRedeemLine4,
            source.HowToRedeemLine5, source.LanguageId, source.LocationBranchId, source.LocationCity, source.LocationLAT,
            source.LocationLine1, source.LocationLine2, source.LocationLON, source.LocationPhone, source.LocationState,
            source.LocationTitle, source.LocationZipCode, source.LogoUrl, source.ModifiedBy, source.ModifiedDateTime, source.Price, source.RejectedBy,
            source.Rejecteddatetime, source.RejectedReason, source.Savings, source.SearchKeywords, source.Status, source.SwapCost, source.UserId, source.CouponTitle, source.CouponExpirydate,
            source.CouponQtyPerUser, source.CouponId, source.couponImage1, source.CouponImage2, source.CouponImage3, source.CurrencyId, source.CouponListingMode, source.CouponActiveMonth, source.CouponActiveYear, source.CouponRedeemedCount, source.CouponViewCount, source.CouponIssuedCount, source.SubmissionDateTime, source.LocationCountryId, source.CouponStartDate, source.CouponEndDate, source.Priority
            , source.ShowBuyitBtn, source.BuyitLandingPageUrl, source.BuyitBtnLabel, source.YoutubeLink, source.CouponImage4, source.CouponImage5, source.CouponImage6, source.IsPaymentCollected, source.PaymentDate, source.IsShowReviews, source.IsShowAddress, source.IsShowPhoneNo, source.IsShowMap, source.IsShowyouTube, source.IsShowAboutUs, source.DealsinGroupCount, source.IsPerSaving3days, source.IsPerSaving2days
            , source.IsPerSavingLastday, source.IsDollarSaving3days, source.IsDollarSaving2days, source.IsDollarSavingLastday, source.LastModifiedDate, source.isSaveBtnLable
            );

        _.each(source.CouponCategories, function (item) {
            coupon.CouponCategories.push(selectedCouponCategory.Create(item));
        });

        _.each(source.CouponPriceOptions, function (item) {
            coupon.CouponPriceOptions.push(CouponPriceOption.Create(item));
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


    // ReSharper disable once AssignToImplicitGlobalInFunctionScope
    CouponPriceOption = function (specifiedCouponPriceOptionId, specifiedCouponId, specifiedDescription, specifiedPrice, specifiedSavings, specifiedOptionUrl, specifiedVoucherCode, ExpiryDate, URL) {
        var
            self,
            CouponPriceOptionId = ko.observable(specifiedCouponPriceOptionId),
            CouponId = ko.observable(specifiedCouponId),
            Price = ko.observable(specifiedPrice).extend({ required: true }),
            Description = ko.observable(specifiedDescription).extend({ required: true }),
            Savings = ko.observable(specifiedSavings).extend({ required: true }),
            OptionUrl = ko.observable(specifiedOptionUrl),
            VoucherCode = ko.observable(specifiedVoucherCode),
            ExpiryDate = ko.observable((ExpiryDate !== null && ExpiryDate !== undefined) ? moment(ExpiryDate).toDate() : undefined),
            URL = ko.observable(URL),
        // Errors
        errors = ko.validation.group({
            Description: Description,
            Price: Price,
            Savings: Savings,
            VoucherCode: VoucherCode,
            ExpiryDate: ExpiryDate,
            URL: URL
        }),
        // Is Valid 
        isValid = ko.computed(function () {
            return errors().length === 0 ? true : false;
        }),
         perSaving = ko.computed(function () {

            var price = Price();
            var saving = Savings();
            var result = 0;
            if (price != undefined && price!=""&& saving !=undefined && saving!="")
                result = (((price - saving) * 100) / price).toFixed(2);
            return result+ "%";
        }),
        per20Saving = ko.computed(function () {

            if (Price() != undefined && Price() != "" && Savings() != undefined && Savings() != "") {
                var result = ((Price() - Savings()) * 100) / Price();
                var per = result + 20;
                var formula = ((Price() * per) / 100);
                return (Price() - formula).toFixed(2);
            } else
                return 0;

        }),
         per20Value = ko.computed(function () {
             var result = 0;
             if (Price() != undefined && Price() != "" && Savings() != undefined && Savings() != "") {
                 result = ((Price() - Savings()) * 100) / Price();
                 result=(result + 20).toFixed(2);
             }
             return result+"%";
         }),
          dollar10Value = ko.computed(function () {
              if (Savings() != undefined && Savings() != "")
                  return (Savings() - 10).toFixed(2);
              else
                  return 0;
          }),
         dollar10perSaving = ko.computed(function () {
             if (Price() != undefined && Price() != "" && Savings() != undefined && Savings() != "") {
                 var price = Price();
                 var saving = Savings() - 10;
                 var result = 0;
                 result = ((price - saving) * 100) / price;
                 return result.toFixed(2) + "%";
             }
             else
                 return 0 + "%";
           }),
       per25Saving = ko.computed(function () {
           if (Price() != undefined && Price() != "" && Savings() != undefined && Savings() != "") {
               var result = ((Price() - Savings()) * 100) / Price();
               var per = result + 25;
               var formula = ((Price() * per) / 100);
               return (Price() - formula).toFixed(2);
           }
           else
               return 0;
       }),
        per25Value = ko.computed(function () {
            if (Price() != undefined && Price() != "" && Savings() != undefined && Savings() != "") {
                var result = ((Price() - Savings()) * 100) / Price();
                return (result + 25).toFixed(2) + "%";
            } else
                return 0 + "%";
        }),
         dollar20Value = ko.computed(function () {
             if (Savings() != undefined && Savings() != "")
                 return (Savings() - 20).toFixed(2);
             else
                 return 0;
         }),
           dollar20perSaving = ko.computed(function () {
               if (Price() != undefined && Price() != "" && Savings() != undefined && Savings() != "") {
                   var price = Price();
                   var saving = Savings() - 20;
                   var result = 0;
                   result = ((price - saving) * 100) / price;
                   return result.toFixed(2) + "%";
               }
               else
                   return 0 + "%";
           }),
      per30Saving = ko.computed(function () {
          if (Price() != undefined && Price() != "" && Savings() != undefined && Savings() != "") {
              var result = ((Price() - Savings()) * 100) / Price();
              var per = result + 30;
              var formula = ((Price() * per) / 100);
              return (Price() - formula).toFixed(2);
          } else
              return 0;

      }),
       per30Value = ko.computed(function () {
           if (Price() != undefined && Price() != "" && Savings() != undefined && Savings() != "") {
               var result = ((Price() - Savings()) * 100) / Price();
               return (result + 30).toFixed(2) + "%";
           }
           else 
               return 0 + "%";
       }),
        dollar30Value = ko.computed(function () {
            if (Savings() != undefined && Savings() != "")
                return (Savings() - 30).toFixed(2);
            else
                return 0;
        }),
         dollar30perSaving = ko.computed(function () {
             if (Price() != undefined && Price() != "" && Savings() != undefined && Savings() != "") {
                 var price = Price();
                 var saving = Savings() - 30;
                 var result = 0;
                 result = ((price - saving) * 100) / price;
                 return result.toFixed(2) + "%";
             }
             else 
                 return 0 + "%";
         }),

        // True if the booking has been changed
        // ReSharper disable InconsistentNaming
        dirtyFlag = new ko.dirtyFlag({
            Description: Description,
            Price: Price,
            Savings: Savings,
            VoucherCode: VoucherCode,
            ExpiryDate: ExpiryDate,
            URL: URL

        }),
        // Has Changes
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),
        convertToServerData = function () {
            return {
                CouponPriceOptionId: CouponPriceOptionId(),
                CouponId: CouponId(),
                Price: Price(),
                Description: Description(),
                Savings: Savings(),
                OptionUrl: OptionUrl(),
                VoucherCode: VoucherCode(),
                ExpiryDate: moment(ExpiryDate()).format(ist.utcFormat) + 'Z',
                URL: URL()
            }
        },
        // Reset
        reset = function () {
            dirtyFlag.reset();
        };
        self = {

            CouponPriceOptionId: CouponPriceOptionId,
            CouponId: CouponId,
            Price: Price,
            Description: Description,
            Savings: Savings,
            OptionUrl: OptionUrl,
            VoucherCode: VoucherCode,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            ExpiryDate: ExpiryDate,
            URL: URL,
            reset: reset,
            perSaving: perSaving,
            per20Saving: per20Saving,
            per25Saving: per25Saving,
            per30Saving: per30Saving,
            per20Value: per20Value,
            per25Value: per25Value,
            per30Value: per30Value,
            dollar10Value:dollar10Value,
            dollar20Value:dollar20Value,
            dollar30Value: dollar30Value,
            dollar10perSaving: dollar10perSaving,
            dollar20perSaving: dollar20perSaving,
            dollar30perSaving: dollar30perSaving
        };
        return self;
    };

    //function to attain cancel button functionality 
    CouponPriceOption.CreateFromClientModel = function (source) {
        return new CouponPriceOption(source.CouponPriceOptionId, source.CouponId, source.Description, source.Price, source.Savings, source.OptionUrl, source.VoucherCode, source.ExpiryDate, source.URL);
    };


    CouponPriceOption.Create = function (source) {
        return new CouponPriceOption(source.CouponPriceOptionId, source.CouponId, source.Description, source.Price, source.Savings, source.OptionUrl, source.VoucherCode, source.ExpiryDate, source.URL);
    };

    return {
        Coupon: Coupon,
        selectedCouponCategory: selectedCouponCategory,
        CouponPriceOption: CouponPriceOption
    };

});