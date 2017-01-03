define(["ko", "underscore", "underscore-ko"], function (ko) {

    var // ReSharper disable InconsistentNaming
      Campaign = function (IsPaymentCollected, PaymentDate, CampaignID, LanguageID, CampaignName, UserID, Status, StatusValue, CampaignDescription, Gender,
          Archived, StartDateTime, EndDateTime, MaxBudget, Type, DisplayTitle, LandingPageVideoLink, VerifyQuestion,
          Answer1, Answer2, Answer3, CorrectAnswer, AgeRangeStart, AgeRangeEnd, ResultClicks, AmountSpent
          , ImagePath, CampaignImagePath, CampaignTypeImagePath, Description, ClickRate,
          Voucher1Heading, Voucher1Description, Voucher1Value, Voucher2Heading, Voucher2Description, Voucher2Value,
          Voucher1ImagePath, VoucherImagePath, CreatedBy, VideoUrl, BuuyItLine1, BuyItLine2, BuyItLine3, BuyItButtonLabel,
          BuyItImageUrl, AdViews, CompanyId, CouponSwapValue, CouponActualValue, CouponQuantity, CouponTakenCount, priority,
          CouponDiscountValue, couponImage2, CouponImage3, CouponImage4, CouponExpiryLabel,
          couponSmdComission, CouponCategories, DeliveryDays, IsUseFilter, logoUrl,
          VoucherAdditionalInfo, CouponId, IsShowVoucherSetting, VideoLink2, CouponType, IsSavedCoupon, viewCountToday, viewCountYesterday, viewCountAllTime, MaxDailyBudget, Locationss, ApprovalDateTime,
          ChannelType, VideoBytes, showBuyitBtn, clickThroughsToday, clickThroughsYesterday, clickThroughsAllTime,
          modifiedDateTime, surveyAnsweredAllTime, answer1Stats, answer2Stats, answer3Stats) {


          var
              //type and userID will be set on server sside
              IsPaymentCollected = ko.observable(IsPaymentCollected),
              PaymentDate = ko.observable(PaymentDate),
              CampaignID = ko.observable(CampaignID),
              LanguageID = ko.observable(LanguageID),
              CampaignName = ko.observable(CampaignName).extend({  // custom message
                  required: true
              }),
              UserID = ko.observable(UserID),
              Status = ko.observable(Status),
              StatusValue = ko.observable(StatusValue),
              CampaignDescription = ko.observable(CampaignDescription),
              Description = ko.observable(Description),
              Gender = ko.observable(Gender),
              Archived = ko.observable(Archived),
              CouponSwapValue = ko.observable(CouponSwapValue),
              CouponActualValue = ko.observable(CouponActualValue),
              CouponQuantity = ko.observable(CouponQuantity),
              CouponTakenCount = ko.observable(CouponTakenCount),
              priority = ko.observable(priority),
              CouponDiscountValue = ko.observable(CouponDiscountValue),
              CouponCategories = ko.observableArray([]),
              VideoBytes = ko.observable(VideoBytes),
              ShowBuyitBtn = ko.observable(showBuyitBtn),
                Answer1 = ko.observable(Answer1).extend({ required: true }),
              Answer2 = ko.observable(Answer2).extend({ required: true }),
              Answer3 = ko.observable(Answer3),
                SurveyAnsweredAllTime = ko.observable(surveyAnsweredAllTime),
                Answer1Stats = ko.observable(answer1Stats),
                Answer2Stats = ko.observable(answer2Stats),
                Answer3Stats = ko.observable(answer3Stats),
                 ModifiedDateTime = ko.observable(modifiedDateTime),
                CampaignRatioData = ko.observable({
                    labels: [Answer1, Answer2, Answer3],
                    datasets: [
                        {
                            data: [answer1Stats, answer2Stats, answer3Stats],
                            backgroundColor: ['green', 'blue','red']

                        }]

                }),

              StartDateTime = ko.observable((StartDateTime !== null && StartDateTime !== undefined) ? moment(StartDateTime).toDate() : undefined).extend({  // custom message
                  required: true
              }),//ko.observable(),
              EndDateTime = ko.observable((EndDateTime !== null && EndDateTime !== undefined) ? moment(EndDateTime).toDate() : undefined).extend({  // custom message
                  required: true
              }).extend({
                  validation: {
                      validator: function (val, someOtherVal) {

                          return moment(val).toDate() > moment(StartDateTime()).toDate();
                      },
                      message: 'End date must be greater than start date',
                  }
              }),// ko.observable(EndDateTime),
              MaxBudget = ko.observable(MaxBudget).extend({ required: true, number: true, min: 1 }),
              Type = ko.observable(Type),
              TypeName = ko.computed(function () {
                  var tname = ''
                  if (Type() == 1)
                      tname = 'Video';
                  else if (Type() == 2)
                      tname = 'Link';
                  else if (Type() == 3)
                      tname = 'Flyer';
                  else if (Type() == 4)
                      tname = 'Game';
                  return tname;
              }),
              DisplayTitle = ko.observable(DisplayTitle).extend({  // custom message
                  required: true
              }),
              LandingPageVideoLink = ko.observable(LandingPageVideoLink).extend({
                  required: {
                      onlyIf: function () {
                          if (Type() == "2" || Type() == "1") {
                              return true;
                          } else {
                              return false;
                          }
                      }
                  }
              }).extend({
                  pattern: {
                      message: 'Please enter valid web url.',
                      params: /^(?:(?:https?|ftp):\/\/)(?:\S+(?::\S*)?@)?(?:(?!10(?:\.\d{1,3}){3})(?!127(?:\.\d{1,3}){3})(?!169\.254(?:\.\d{1,3}){2})(?!192\.168(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/[^\s]*)?$/i,
                      onlyIf: function () {
                          if (Type() == "2") {
                              return true;
                          } else {
                              return false;
                          }
                      }
                  }
              }),
              VerifyQuestion = ko.observable(VerifyQuestion).extend({ required: true }),
              Voucher1Heading = ko.observable(Voucher1Heading),
              Voucher1Description = ko.observable(Voucher1Description),
              Voucher1Value = ko.observable(Voucher1Value),
              Voucher2Heading = ko.observable(Voucher2Heading),
              Voucher2Description = ko.observable(Voucher2Description),
              Voucher2Value = ko.observable(Voucher2Value),
            
              CreatedBy = ko.observable(CreatedBy),
              CorrectAnswer = ko.observable(CorrectAnswer),
              AgeRangeStart = ko.observable(AgeRangeStart),
              AgeRangeEnd = ko.observable(AgeRangeEnd).extend({
                  validation: {
                      validator: function (val, someOtherVal) {
                          return val > AgeRangeStart();
                      },
                      message: 'Age end range must be greater than start range',
                  }
              }),
              ResultClicks = ko.observable(ResultClicks),
              AmountSpent = ko.observable(AmountSpent),
              ImagePath = ko.observable(ImagePath),
              CampaignImagePath = ko.observable(CampaignImagePath),
              couponImage2 = ko.observable(couponImage2),
              CouponImage3 = ko.observable(CouponImage3),
              CouponImage4 = ko.observable(CouponImage4),
              CouponExpiryLabel = ko.observable(CouponExpiryLabel),
              couponSmdComission = ko.observable(couponSmdComission),
              CampaignTypeImagePath = ko.observable(CampaignTypeImagePath),
              ClickRate = ko.observable(ClickRate),
              Voucher1ImagePath = ko.observable(Voucher1ImagePath),
              VoucherImagePath = ko.observable(VoucherImagePath),
              AdCampaignTargetCriterias = ko.observableArray([]),
              AdCampaignTargetLocations = ko.observableArray([]),
              VideoUrl = ko.observable(VideoUrl),
              BuuyItLine1 = ko.observable(BuuyItLine1),
              BuyItLine2 = ko.observable(BuyItLine2),
              BuyItLine3 = ko.observable(BuyItLine3),
              BuyItButtonLabel = ko.observable(BuyItButtonLabel),
              BuyItImageUrl = ko.observable(BuyItImageUrl),
              buyItImageBytes = ko.observable(''),
              AdViews = ko.observable(AdViews),
              CompanyId = ko.observable(CompanyId),
              DeliveryDays = ko.observable(DeliveryDays),
              CouponCodes = ko.observableArray([]),
              IsUseFilter = ko.observable(IsUseFilter),

              LogoUrl = ko.observable(logoUrl != null ? logoUrl.startsWith('http') ? logoUrl : '/' + logoUrl : null).extend({  // custom message
                  required: true
              }),

              VoucherAdditionalInfo = ko.observable(VoucherAdditionalInfo),
              LogoImageBytes = ko.observable(''),
              CouponId = ko.observable(CouponId),
              IsShowVoucherSetting = ko.observable(IsShowVoucherSetting),
              VideoLink2 = ko.observable(VideoLink2),
              CouponType = ko.observable(CouponType),
              IsSavedCoupon = ko.observable(IsSavedCoupon),
               viewCountToday = ko.observable(viewCountToday),
               viewCountYesterday = ko.observable(viewCountYesterday),
               viewCountAllTime = ko.observable(viewCountAllTime),
               MaxDailyBudget = ko.observable(MaxDailyBudget),
               Locationss = ko.observable(Locationss),
               ApprovalDateTime = ko.observable(ApprovalDateTime),
              ChannelType = ko.observable(ChannelType),
              clickThroughsToday = ko.observable(clickThroughsToday),
              clickThroughsYesterday = ko.observable(clickThroughsYesterday),
              clickThroughsAllTime = ko.observable(clickThroughsAllTime),
               // Errors
                errors = ko.validation.group({
                    CampaignName: CampaignName,
                    Answer1: Answer1,
                    Answer2:Answer2,
                    DisplayTitle: DisplayTitle,
                    LandingPageVideoLink: LandingPageVideoLink,
                    VerifyQuestion:VerifyQuestion,
                    //StartDateTime: StartDateTime,
                    //EndDateTime: EndDateTime,
                    MaxBudget: MaxBudget,
                    AgeRangeEnd: AgeRangeEnd,
                    LogoUrl: LogoUrl
                }),
                // Is Valid 
                isValid = ko.computed(function () {
                    return errors().length === 0 ? true : false;
                }),
              dirtyFlag = new ko.dirtyFlag({
                  ShowBuyitBtn: ShowBuyitBtn,
                  IsPaymentCollected: IsPaymentCollected,
                  PaymentDate: PaymentDate,
                  CampaignName: CampaignName,
                  CampaignDescription: CampaignDescription,
                  Description: Description,
                  StartDateTime: StartDateTime,
                  EndDateTime: EndDateTime,
                  MaxBudget: MaxBudget,
                  Type: Type,
                  DisplayTitle: DisplayTitle,
                  LandingPageVideoLink: LandingPageVideoLink,
                  VerifyQuestion: VerifyQuestion,
                  Answer1: Answer1,
                  Answer2: Answer2,
                  Answer3: Answer3,
                  CorrectAnswer: CorrectAnswer,
                  AgeRangeStart: AgeRangeStart,
                  AgeRangeEnd: AgeRangeEnd,
                  AdCampaignTargetCriterias: AdCampaignTargetCriterias,
                  AdCampaignTargetLocations: AdCampaignTargetLocations,
                  Voucher1Heading: Voucher1Heading,
                  Voucher1Description: Voucher1Description,
                  Voucher1Value: Voucher1Value,
                  Voucher2Heading: Voucher2Heading,
                  Voucher2Description: Voucher2Description,
                  Voucher2Value: Voucher2Value,
                  VideoUrl: VideoUrl,
                  BuuyItLine1: BuuyItLine1,
                  BuyItLine2: BuyItLine2,
                  BuyItLine3: BuyItLine3,
                  BuyItButtonLabel: BuyItButtonLabel,
                  BuyItImageUrl: BuyItImageUrl,
                  buyItImageBytes: buyItImageBytes,
                  AdViews: AdViews,
                  CouponSwapValue: CouponSwapValue,
                  CouponActualValue: CouponActualValue,
                  CouponQuantity: CouponQuantity,
                  priority: priority,
                  VoucherImagePath: VoucherImagePath,
                  CouponDiscountValue: CouponDiscountValue,
                  //  DeliveryDays: DeliveryDays,
                  CouponCodes: CouponCodes,
                  IsUseFilter: IsUseFilter,
                  CouponType: CouponType,
                  IsSavedCoupon: IsSavedCoupon,
                  viewCountToday: viewCountToday,
                  viewCountYesterday: viewCountYesterday,
                  viewCountAllTime: viewCountAllTime,
                  clickThroughsToday: clickThroughsToday,
                  clickThroughsYesterday: clickThroughsYesterday,
                  clickThroughsAllTime: clickThroughsAllTime,
                   MaxDailyBudget : MaxDailyBudget,
                  Locationss: Locationss,
                  ApprovalDateTime: ApprovalDateTime,
                  ChannelType: ChannelType,
                  VideoBytes: VideoBytes,
                  LogoUrl: LogoUrl,
                  Gender: Gender,
                  ModifiedDateTime : ModifiedDateTime,
                  SurveyAnsweredAllTime : SurveyAnsweredAllTime,
                  Answer1Stats : Answer1Stats ,
                  Answer2Stats : Answer2Stats,
                  Answer3Stats: Answer3Stats ,
                   ClickRate: ClickRate,

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

                  var targetCriteria = [];
                  _.each(AdCampaignTargetCriterias(), function (item) {

                      targetCriteria.push(item.convertCriteriaToServerData());
                  });
                  var LocationtargetCriteria = [];

                  _.each(AdCampaignTargetLocations(), function (item) {

                      LocationtargetCriteria.push(item.convertToServerData());
                  });
                  var selectedCoupons = [];
                  _.each(CouponCategories(), function (item) {

                      selectedCoupons.push(item.convertToServerData());
                  });
                  var targetCouponCodes = [];
                  _.each(CouponCodes(), function (item) {
                      console.log(item);
                      targetCouponCodes.push(item.convertToServerData());
                  });
                  return {

                      CampaignID: CampaignID(),
                      LanguageID: LanguageID(),
                      CampaignName: CampaignName(),
                      UserID: UserID(),
                      Status: Status(),
                      StatusValue: StatusValue(),
                      CampaignDescription: CampaignDescription(),
                      Description: Description(),
                      Gender: Gender(),
                      Archived: Archived(),
                      StartDateTime: moment(StartDateTime()).format(ist.utcFormat) + 'Z',//StartDateTime(),
                      EndDateTime: moment(EndDateTime()).format(ist.utcFormat) + 'Z',// EndDateTime(),
                      MaxBudget: MaxBudget(),
                      Type: Type(),
                      DisplayTitle: DisplayTitle(),
                      LandingPageVideoLink: LandingPageVideoLink(),
                      VerifyQuestion: VerifyQuestion(),
                      Answer1: Answer1(),
                      Answer2: Answer2(),
                      Answer3: Answer3(),
                      CorrectAnswer: CorrectAnswer(),
                      AgeRangeStart: AgeRangeStart(),
                      AgeRangeEnd: AgeRangeEnd(),
                      ResultClicks: ResultClicks(),
                      AmountSpent: AmountSpent(),
                      ImagePath: ImagePath(),
                      CampaignImagePath: CampaignImagePath(),
                      couponImage2: couponImage2(),
                      CouponImage3: CouponImage3(),
                      CouponImage4: CouponImage4(),
                      CouponExpiryLabel: CouponExpiryLabel(),
                      couponSmdComission: couponSmdComission(),
                      CampaignTypeImagePath: CampaignTypeImagePath(),
                      ClickRate: ClickRate(),
                      AdCampaignTargetCriterias: targetCriteria,
                      AdCampaignTargetLocations: LocationtargetCriteria,
                      Voucher1Heading: Voucher1Heading(),
                      Voucher1Description: Voucher1Description(),
                      Voucher1Value: Voucher1Value(),
                      Voucher2Heading: Voucher2Heading(),
                      Voucher2Description: Voucher2Description(),
                      Voucher2Value: Voucher2Value(),
                      Voucher1ImagePath: Voucher1ImagePath(),
                      VoucherImagePath: VoucherImagePath(),
                      VideoBytes: VideoBytes(),
                      CreatedBy: CreatedBy(),
                      VideoUrl: VideoUrl(),
                      BuuyItLine1: BuuyItLine1(),
                      BuyItLine2: BuyItLine2(),
                      BuyItLine3: BuyItLine3(),
                      BuyItButtonLabel: BuyItButtonLabel(),
                      BuyItImageUrl: BuyItImageUrl(),
                      buyItImageBytes: buyItImageBytes(),
                      AdViews: AdViews(),
                      CompanyId: CompanyId(),
                      CouponSwapValue: CouponSwapValue(),
                      CouponActualValue: CouponActualValue(),
                      CouponQuantity: CouponQuantity(),
                      CouponTakenCount: CouponTakenCount(),
                      priority: priority(),
                      CouponDiscountValue: CouponDiscountValue(),
                      CouponCategories: selectedCoupons,
                      DeliveryDays: DeliveryDays(),
                      CouponCodes: targetCouponCodes,
                      IsUseFilter: IsUseFilter(),
                      LogoUrl: LogoUrl(),
                      ShowBuyitBtn: ShowBuyitBtn(),
                      IsPaymentCollected: IsPaymentCollected(),
                      PaymentDate: PaymentDate(),
                      VoucherAdditionalInfo: VoucherAdditionalInfo(),

                      LogoImageBytes: logoImage == "" ? LogoImageBytes() : logoImage,

                      CouponId: CouponId(),
                      IsShowVoucherSetting: IsShowVoucherSetting(),
                      VideoLink2: VideoLink2(),
                      CouponType: CouponType(),
                      IsSavedCoupon: IsSavedCoupon(),
                      viewCountToday: viewCountToday(),
                      viewCountYesterday: viewCountYesterday(),
                      viewCountAllTime: viewCountAllTime(),
                      MaxDailyBudget: MaxDailyBudget(),
                      Locationss: Locationss(),
                      ApprovalDateTime: ApprovalDateTime(),
                      ChannelType: ChannelType(),
                      clickThroughsToday: clickThroughsToday(),
                      clickThroughsYesterday: clickThroughsYesterday(),
                      clickThroughsAllTime: clickThroughsAllTime()

                  };
              };
          return {
              CampaignID: CampaignID,
              LanguageId: LanguageID,
              CampaignName: CampaignName,
              UserID: UserID,
              Status: Status,
              StatusValue: StatusValue,
              CampaignDescription: CampaignDescription,
              Description: Description,
              Gender: Gender,
              Archived: Archived,
              StartDateTime: StartDateTime,
              EndDateTime: EndDateTime,
              MaxBudget: MaxBudget,
              Type: Type,
              DisplayTitle: DisplayTitle,
              LandingPageVideoLink: LandingPageVideoLink,
              VerifyQuestion: VerifyQuestion,
              Answer1: Answer1,
              Answer2: Answer2,
              Answer3: Answer3,
              CorrectAnswer: CorrectAnswer,
              AgeRangeStart: AgeRangeStart,
              AgeRangeEnd: AgeRangeEnd,
              ResultClicks: ResultClicks,
              AmountSpent: AmountSpent,
              ImagePath: ImagePath,
              CampaignImagePath: CampaignImagePath,
              couponImage2: couponImage2,
              CouponImage3: CouponImage3,
              CouponImage4: CouponImage4,
              CouponExpiryLabel: CouponExpiryLabel,
              couponSmdComission: couponSmdComission,
              CampaignTypeImagePath: CampaignTypeImagePath,
              ClickRate: ClickRate,
              AdCampaignTargetCriterias: AdCampaignTargetCriterias,
              AdCampaignTargetLocations: AdCampaignTargetLocations,
              convertToServerData: convertToServerData,
              hasChanges: hasChanges,
              reset: reset,
              isValid: isValid,
              dirtyFlag: dirtyFlag,
              errors: errors,
              Voucher1Heading: Voucher1Heading,
              Voucher1Description: Voucher1Description,
              Voucher1Value: Voucher1Value,
              Voucher2Heading: Voucher2Heading,
              Voucher2Description: Voucher2Description,
              Voucher2Value: Voucher2Value,
              Voucher1ImagePath: Voucher1ImagePath,
              VoucherImagePath: VoucherImagePath,
              CreatedBy: CreatedBy,
              VideoUrl: VideoUrl,
              BuuyItLine1: BuuyItLine1,
              BuyItLine2: BuyItLine2,
              BuyItLine3: BuyItLine3,
              BuyItButtonLabel: BuyItButtonLabel,
              BuyItImageUrl: BuyItImageUrl,
              buyItImageBytes: buyItImageBytes,
              AdViews: AdViews,
              CompanyId: CompanyId,
              CouponSwapValue: CouponSwapValue,
              CouponActualValue: CouponActualValue,
              CouponQuantity: CouponQuantity,
              CouponTakenCount: CouponTakenCount,
              priority: priority,
              CouponDiscountValue: CouponDiscountValue,
              CouponCategories: CouponCategories,
              DeliveryDays: DeliveryDays,
              CouponCodes: CouponCodes,
              IsUseFilter: IsUseFilter,
              LogoUrl: LogoUrl,
              VoucherAdditionalInfo: VoucherAdditionalInfo,
              LogoImageBytes: LogoImageBytes,
              CouponId: CouponId,
              IsShowVoucherSetting: IsShowVoucherSetting,
              VideoLink2: VideoLink2,
              CouponType: CouponType,
              IsSavedCoupon: IsSavedCoupon,
              viewCountToday: viewCountToday,
              viewCountYesterday: viewCountYesterday,
              viewCountAllTime: viewCountAllTime,
              MaxDailyBudget: MaxDailyBudget,
              Locationss: Locationss,
              ApprovalDateTime: ApprovalDateTime,
              ChannelType: ChannelType,
              VideoBytes: VideoBytes,
              ShowBuyitBtn: ShowBuyitBtn,
              IsPaymentCollected: IsPaymentCollected,
              PaymentDate: PaymentDate,
              clickThroughsToday: clickThroughsToday,
              clickThroughsYesterday: clickThroughsYesterday,
              clickThroughsAllTime: clickThroughsAllTime,
              ModifiedDateTime: ModifiedDateTime,
              SurveyAnsweredAllTime: SurveyAnsweredAllTime,
              Answer1Stats: Answer1Stats,
              Answer2Stats: Answer2Stats,
              Answer3Stats: Answer3Stats,
              ClickRate: ClickRate,
              CampaignRatioData: CampaignRatioData

          };
      };

    var // ReSharper disable InconsistentNaming
      AdCampaignTargetCriteriasModel = function (CriteriaID, CampaignID, Type, PQID, PQAnswerID, SQID, SQAnswer, IncludeorExclude, questionString,
       answerString, surveyQuestLeftImageSrc, surveyQuestRightImageSrc, LanguageID, Language, IndustryID, Industry, EducationID, Education, QuizCampaignId, QuizAnswerId
       , criteriaPrice, surveyQuestThirdImageSrc) {

          var
              //type and userID will be set on server sside
               CriteriaID = ko.observable(CriteriaID),
               CampaignID = ko.observable(CampaignID),
               Type = ko.observable(Type),
               PQID = ko.observable(PQID),
               PQAnswerID = ko.observable(PQAnswerID),
               SQID = ko.observable(SQID),
               SQAnswer = ko.observable(SQAnswer),
               IncludeorExclude = ko.observable(IncludeorExclude == true ? "1" : "0"),
               questionString = ko.observable(questionString),
               answerString = ko.observable(answerString),
               surveyQuestLeftImageSrc = ko.observable(surveyQuestLeftImageSrc),
               surveyQuestRightImageSrc = ko.observable(surveyQuestRightImageSrc),
               surveyQuestThirdImageSrc = ko.observable(surveyQuestThirdImageSrc)
          LanguageID = ko.observable(LanguageID),
          Language = ko.observable(Language),
          IndustryID = ko.observable(IndustryID),
          Industry = ko.observable(Industry),
          Education = ko.observable(Education),
          EducationID = ko.observable(EducationID),
          QuizCampaignId = ko.observable(QuizCampaignId),
          QuizAnswerId = ko.observable(QuizAnswerId),
          criteriaPrice = ko.observable(criteriaPrice)
          // Convert to server data
          convertCriteriaToServerData = function () {

              return {
                  CriteriaId: CriteriaID(),
                  CampaignId: CampaignID(),
                  Type: Type(),
                  PQId: PQID(),
                  PQAnswerId: PQAnswerID(),
                  SQId: SQID(),
                  SQAnswer: SQAnswer(),
                  IncludeorExclude: IncludeorExclude() == 1 ? true : false,
                  LanguageId: LanguageID(),
                  Language: Language(),
                  IndustryId: IndustryID(),
                  Industry: Industry(),
                  EducationId: EducationID(),
                  Education: Education(),
                  QuizCampaignId: QuizCampaignId(),
                  QuizAnswerId: QuizAnswerId()
              };
          };
          return {
              CriteriaID: CriteriaID,
              CampaignID: CampaignID,
              Type: Type,
              PQID: PQID,
              PQAnswerID: PQAnswerID,
              SQID: SQID,
              SQAnswer: SQAnswer,
              IncludeorExclude: IncludeorExclude,
              questionString: questionString,
              answerString: answerString,
              surveyQuestLeftImageSrc: surveyQuestLeftImageSrc,
              surveyQuestRightImageSrc: surveyQuestRightImageSrc,
              surveyQuestThirdImageSrc: surveyQuestThirdImageSrc,
              LanguageID: LanguageID,
              Language: Language,
              IndustryID: IndustryID,
              Industry: Industry,
              EducationID: EducationID,
              Education: Education,
              convertCriteriaToServerData: convertCriteriaToServerData,
              QuizCampaignId: QuizCampaignId,
              QuizAnswerId: QuizAnswerId,
              criteriaPrice: criteriaPrice
          };
      };
    var // ReSharper disable InconsistentNaming
    AdCampaignTargetLocation = function (ID, CampaignID, CountryID, CityID, Radius, Country, City, IncludeorExclude, Latitude, Longitude) {

        var
            //type and userID will be set on server sside
            ID = ko.observable(ID),
            CampaignID = ko.observable(CampaignID),
            CountryID = ko.observable(CountryID),
            CityID = ko.observable(CityID),
            Radius = ko.observable(Radius),
            Country = ko.observable(Country),
            City = ko.observable(City),
            IncludeorExclude = ko.observable(IncludeorExclude == true ? "1" : "0"),
                     Latitude = ko.observable(Latitude),
           Longitude = ko.observable(Longitude),
            // Convert to server data
            convertToServerData = function () {
                return {
                    Id: ID(),
                    CampaignId: CampaignID(),
                    CountryId: CountryID(),
                    CityId: CityID(),
                    Radius: Radius(),
                    Country: Country(),
                    City: City(),
                    IncludeorExclude: IncludeorExclude() == 1 ? true : false
                };
            };
        return {
            ID: ID,
            CampaignID: CampaignID,
            CountryID: CountryID,
            CityID: CityID,
            Radius: Radius,
            Country: Country,
            City: City,
            IncludeorExclude: IncludeorExclude,
            convertToServerData: convertToServerData,
            Latitude: Latitude,
            Longitude: Longitude
        };
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

    var // ReSharper disable InconsistentNaming
  AdCampaignCouponCodes = function (CodeId, CampaignId, Code, IsTaken, UserId, UserName, TakenDateTime) {

      var
          //type and userID will be set on server sside
          CodeId = ko.observable(CodeId),
          CampaignId = ko.observable(CampaignId),
          Code = ko.observable(Code),
          IsTaken = ko.observable(IsTaken),
          UserId = ko.observable(UserId),
          UserName = ko.observable(UserName),
          TakenDateTime = ko.observable(TakenDateTime),
          // Convert to server data
          convertToServerData = function () {
              return {
                  CodeId: CodeId(),
                  CampaignId: CampaignId(),
                  Code: Code(),
                  IsTaken: IsTaken(),
                  UserId: UserId(),
                  TakenDateTime: TakenDateTime
              };
          };
      return {
          CodeId: CodeId,
          CampaignId: CampaignId,
          Code: Code,
          IsTaken: IsTaken,
          UserId: UserId,
          UserName: UserName,
          TakenDateTime: TakenDateTime,
          convertToServerData: convertToServerData
      };
  };
    var formAnalyticsDataModel = function (source) {

        var
            //type and userID will be set on server sside
            Question = ko.observable(source.Question),
            answer = ko.observable(source.answer),
            Id = ko.observable(source.Id),
            typ = ko.observable(source.typ),
            selectedGenderAnalytics = ko.observable(0),
            selectedAgeAnalytics = ko.observable(0),
            selectedCityAnalytics = ko.observable("All"),
            Stats = ko.observable(source.Stats)


        return {
            Question: Question,
            answer: answer,
            Id: Id,
            typ: typ,
            selectedGenderAnalytics: selectedGenderAnalytics,
            selectedAgeAnalytics: selectedAgeAnalytics,
            selectedCityAnalytics: selectedCityAnalytics,
            Stats: Stats

        };
    };

    // Factory Method
    Campaign.Create = function (source) {

        var campaign = new Campaign(source.IsPaymentCollected, source.PaymentDate, source.CampaignID > 0 ? source.CampaignID: source.CampaignId , source.LanguageId, source.CampaignName, source.UserId, source.Status, source.StatusValue,
            source.CampaignDescription, source.Gender + "", source.Archived, source.StartDateTime, source.EndDateTime, source.MaxBudget
            , source.Type + "", source.DisplayTitle, source.LandingPageVideoLink, source.VerifyQuestion, source.Answer1, source.Answer2, source.Answer3,
            source.CorrectAnswer, source.AgeRangeStart, source.AgeRangeEnd, source.ResultClicks, source.AmountSpent, source.ImagePath, source.CampaignImagePath,
            source.CampaignTypeImagePath, source.Description, source.ClickRate, source.Voucher1Heading, source.Voucher1Description, source.Voucher1Value, source.Voucher2Heading, source.Voucher2Description,
             source.Voucher2Value, source.Voucher1ImagePath, source.VoucherImagePath, source.CreatedBy, source.VideoUrl, source.BuuyItLine1, source.BuyItLine2, source.BuyItLine3, source.BuyItButtonLabel, source.BuyItImageUrl, source.AdViews, source.CompanyId,
            source.CouponSwapValue, source.CouponActualValue, source.CouponQuantity, source.CouponTakenCount, source.priority, source.CouponDiscountValue,
             source.couponImage2, source.CouponImage3, source.CouponImage4, source.CouponExpiryLabel, source.couponSmdComission, null, source.DeliveryDays + "", source.IsUseFilter + "", source.LogoUrl, source.VoucherAdditionalInfo, source.CouponId, source.IsShowVoucherSetting, source.VideoLink2, source.CouponType + "", source.IsSavedCoupon, source.viewCountToday, source.viewCountYesterday, source.viewCountAllTime, source.MaxDailyBudget, source.Locationss, source.ApprovalDateTime, source.ChannelType + "", source.VideoBytes, source.ShowBuyitBtn, source.clickThroughsToday,
            source.clickThroughsYesterday, source.clickThroughsAllTime, source.ModifiedDateTime, source.SurveyAnsweredAllTime,
            source.Answer1Stats, source.Answer2Stats, source.Answer3Stats);

        _.each(source.AdCampaignTargetCriterias, function (item) {

            campaign.AdCampaignTargetCriterias.push(AdCampaignTargetCriteriasModel.Create(item));
        });
        _.each(source.AdCampaignTargetLocations, function (item) {

            campaign.AdCampaignTargetLocations.push(AdCampaignTargetLocation.Create(item));
        });
        _.each(source.CouponCategories, function (item) {

            campaign.CouponCategories.push(selectedCouponCategory.Create(item));
        });

        _.each(source.CouponCodes, function (item) {

            campaign.CouponCodes.push(AdCampaignCouponCodes.Create(item));
        });
        return campaign;
    };
    // Factory Method
    AdCampaignTargetCriteriasModel.Create = function (source) {

        return new AdCampaignTargetCriteriasModel(source.CriteriaId, source.CampaignId, source.Type, source.PQId, source.PQAnswerId, source.SQId, source.SQAnswer,
            source.IncludeorExclude, source.questionString, source.answerString, source.surveyQuestLeftImageSrc, source.surveyQuestRightImageSrc, source.LanguageId,
            source.Language, source.IndustryId, source.Industry, source.EducationId, source.Education, source.QuizCampaignId, source.QuizAnswerId, source.criteriaPrice, source.surveyQuestThirdImageSrc);
    };
    AdCampaignTargetLocation.Create = function (source) {

        return new AdCampaignTargetLocation(source.Id, source.CampaignId, source.CountryId, source.CityId, source.Radius, source.Country, source.City, source.IncludeorExclude, source.Latitude, source.Longitude);
    };
    // Factory Method
    selectedCouponCategory.Create = function (source) {

        return new selectedCouponCategory(source.CategoryId, source.Name);
    };
    AdCampaignCouponCodes.Create = function (source) {

        return new AdCampaignCouponCodes(source.CodeId, source.CampaignId, source.Code, source.IsTaken, source.UserId, source.UserName, source.TakenDateTime);
    };
    return {
        Campaign: Campaign,
        AdCampaignTargetCriteriasModel: AdCampaignTargetCriteriasModel,
        AdCampaignTargetLocation: AdCampaignTargetLocation,
        selectedCouponCategory: selectedCouponCategory,
        AdCampaignCouponCodes: AdCampaignCouponCodes,
        formAnalyticsDataModel: formAnalyticsDataModel
    };
});