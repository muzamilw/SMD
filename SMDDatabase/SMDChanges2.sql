/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProfileQuestion ADD
	AmountCharged float(53) NULL
GO
ALTER TABLE dbo.ProfileQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[GetCouponByID]    Script Date: 9/8/2016 10:40:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mz
-- Create date: 30 august 2016
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[GetCouponByID] 

--    getcouponbyid 23, '','','d70b04d3-e76c-4ca2-95fe-9746ceff1a88'
	-- Add the parameters for the stored procedure here
	@CouponId as bigint = 0, 
	@Lat as nvarchar(50),
	@Lon as nvarchar(50),
	@UserId as nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


if @lat = '' 
	set @lat = '31.482932'


if @lon = '' 
	set @lon = '74.288557'


DECLARE @source geography = geography::Point(@lat, @lon, 4326)


	update coupon set CouponViewcount = CouponViewcount + 1 where CouponId= @CouponId


INSERT INTO [dbo].[UserCouponView]
           ([CouponId]
           ,[UserId]
           ,[ViewDateTime])
     VALUES
           (@CouponId
           ,@UserId
           ,GETDATE())


SELECT [CouponId]
      ,[LanguageId]
      ,[UserId]
      ,[CouponTitle]
      ,[SearchKeywords]
      ,[Status]
      ,[Archived]
      ,[Approved]
      ,[ApprovedBy]
      ,[ApprovalDateTime]
      ,[CreatedDateTime]
      ,[CreatedBy]
      ,[ModifiedDateTime]
      ,[ModifiedBy]
      ,[RejectedReason]
      ,[Rejecteddatetime]
      ,[RejectedBy]
      ,c.[CurrencyId]
      ,[Price]
      ,[Savings]
      
      ,[CouponViewCount]
      ,[CouponIssuedCount]
      ,[CouponRedeemedCount]
      ,[CouponQtyPerUser]
      ,[CouponListingMode]
      ,[CompanyId]
      ,[CouponActiveMonth]
      ,[CouponActiveYear]
      ,[CouponExpirydate]
      ,[couponImage1]
      ,[CouponImage2]
      ,[CouponImage3]
      
      ,[HighlightLine1]
      ,[HighlightLine2]
      ,[HighlightLine3]
      ,[HighlightLine4]
      ,[HighlightLine5]
      ,[FinePrintLine1]
      ,[FinePrintLine2]
      ,[FinePrintLine3]
      ,[FinePrintLine4]
      ,[FinePrintLine5]
      ,[LocationBranchId]
      ,[LocationTitle]
      ,[LocationLine1]
      ,[LocationLine2]
      ,[LocationCity]
      ,[LocationState]
      ,[LocationZipCode]
      ,[LocationLAT]
      ,[LocationLON]
      ,[LocationPhone]
      ,[GeographyColumn]
      ,[HowToRedeemLine1]
      ,[HowToRedeemLine2]
      ,[HowToRedeemLine3]
      ,[HowToRedeemLine4]
      ,[HowToRedeemLine5]
      ,[SubmissionDateTime]
 ,


	case when c.CouponListingMode = 3 then 0 
	else
	@source.STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 
       
	end  as distance, curr.CurrencySymbol
	,
		(select 
	CASE
		WHEN c.LogoUrl is null or c.LogoUrl = ''
		THEN 'http://manage.cash4ads.com/' + comp.Logo
		WHEN c.LogoUrl is not null
		THEN 'http://manage.cash4ads.com/' + c.LogoUrl
	END as AdvertisersLogoPath from company comp
	 where comp.CompanyId = c.CompanyId) as [LogoUrl],

	 case when c.CouponListingMode  = 2
	 then 
	  DATEDIFF(d, getdate(), DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth+2,day(EOMONTH ( DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,1)))))
	   when c.CouponListingMode  = 3
	 then 
	  DATEDIFF(d, getdate(), DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth+2,day(EOMONTH ( DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,1)))))
	 when CouponListingMode = 1
	 then
	 DATEDIFF(d, getdate(), DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(c.CouponActiveYear, c.CouponActiveMonth,1)))))
	 end as DaysLeft
	 ,
	 case when 

	 
	 (c.Savings / curr.SMDCreditRatio) / 100  > 0.50 then cast(round((c.Savings / curr.SMDCreditRatio),2) as numeric(36,2))
	 else
	 50
	 end
	 
	 as SwapCost



	from coupon as c 
	inner join Currency curr on c.CurrencyId = curr.CurrencyID 
	where c.CouponId = @CouponId
END




/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CompanyBranch ADD
	CountryId int NULL
GO
ALTER TABLE dbo.CompanyBranch SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Coupon ADD
	LocationCountryId int NULL
GO
ALTER TABLE dbo.Coupon SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




/*--------------------- 9/20/2016----------------------------------
Added by Hadia 
*/

ALTER TABLE ProfileQuestion
ADD StartDate datetime default NULL

ALTER TABLE ProfileQuestion
ADD EndDate datetime default NULL

 ALTER TABLE UserPurchasedCoupon
ADD ResponseType int default NULL

 ALTER TABLE SurveyQuestionResponse
ADD ResponseType int default NULL

 ALTER TABLE ProfileQuestionUserAnswer
ADD ResponseType int default NULL

--ResponseType for video
--1 = click
--2 = skip
--3 = reffered to landing page
--Survay Cards / questions ResponseType
--1 = answered
--2 = skiped


--ResponseType = 3 // for referred to landing pages in UserPurchasedCoupon






ALTER TABLE dbo.coupon
	
	alter column HighlightLine1 nvarchar(800) NULL
ALTER TABLE dbo.coupon
	alter column FinePrintLine1 nvarchar(800) NULL




	ALTER TABLE dbo.coupon ADD
	ShowBuyitBtn bit NULL,
	BuyitLandingPageUrl nvarchar(500) NULL,
	BuyitBtnLabel nvarchar(200) NULL









	/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD
	QuizCampaignId bigint NULL,
	QuizAnswerId int NULL
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria ADD CONSTRAINT
	FK_SurveyQuestionTargetCriteria_AdCampaign FOREIGN KEY
	(
	QuizCampaignId
	) REFERENCES dbo.AdCampaign
	(
	CampaignID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestionTargetCriteria SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




















/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Coupon SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.CouponPriceOption
	(
	CouponPriceOptionId bigint NOT NULL IDENTITY (1, 1),
	CouponId bigint NULL,
	Description nvarchar(500) NULL,
	Price float(53) NULL,
	Savings float(53) NULL,
	CoucherCode nvarchar(100) NULL,
	OptionUrl nvarchar(500) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.CouponPriceOption ADD CONSTRAINT
	PK_CouponPriceOption PRIMARY KEY CLUSTERED 
	(
	CouponPriceOptionId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CouponPriceOption ADD CONSTRAINT
	FK_CouponPriceOption_Coupon FOREIGN KEY
	(
	CouponId
	) REFERENCES dbo.Coupon
	(
	CouponId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CouponPriceOption SET (LOCK_ESCALATION = TABLE)
GO
COMMIT








/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Company
	DROP CONSTRAINT FK_Company_City
GO
ALTER TABLE dbo.City SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Company ADD
	City nvarchar(200) NULL,
	BillingCity nvarchar(200) NULL
GO
ALTER TABLE dbo.Company
	DROP COLUMN CityId, BillingCityId
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Company ADD
	AboutUsDescription nvarchar(MAX) NULL,
	Status int NULL,
	PaymentMethodStatus int NULL,
	LastPaymentMethodErrorDate datetime NULL,
	LastPaymentMethodError nvarchar(MAX) NULL
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers ADD
	optDealsNearMeEmails bit NULL,
	optLatestNewsEmails bit NULL,
	optMarketingEmails bit NULL
GO
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Company ADD
	CompanyRegNo nvarchar(100) NULL,
	TaxRegNo nvarchar(100) NULL
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT








/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.PayOutHistory
	(
	PayOutId bigint NOT NULL IDENTITY (1, 1),
	CompanyId int NULL,
	RequestDateTime datetime NULL,
	CentzAmount float(53) NULL,
	DollarAmount float(53) NULL,
	StageOneStatus bit NULL,
	StageOneRejectionReason nvarchar(500) NULL,
	StageOneEventDate datetime NULL,
	StageOneUserId nvarchar(128) NULL,
	StageTwoStatus bit NULL,
	StageTwoRejectionReason nvarchar(500) NULL,
	StageTwoEventDate datetime NULL,
	StageTwoUserId nvarchar(128) NULL,
	TargetPayoutAccount nvarchar(500) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PayOutHistory ADD CONSTRAINT
	PK_PayOutHistory PRIMARY KEY CLUSTERED 
	(
	PayOutId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PayOutHistory ADD CONSTRAINT
	FK_PayOutHistory_Company FOREIGN KEY
	(
	CompanyId
	) REFERENCES dbo.Company
	(
	CompanyId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PayOutHistory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.City.CityID', N'Tmp_CityId', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.City.Tmp_CityId', N'CityId', 'COLUMN' 
GO
ALTER TABLE dbo.City SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.City.CountryID', N'Tmp_CountryId_4', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.City.Tmp_CountryId_4', N'CountryId', 'COLUMN' 
GO
ALTER TABLE dbo.City SET (LOCK_ESCALATION = TABLE)
GO
COMMIT






/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.CouponPriceOption.CoucherCode', N'Tmp_VoucherCode', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.CouponPriceOption.Tmp_VoucherCode', N'VoucherCode', 'COLUMN' 
GO
ALTER TABLE dbo.CouponPriceOption SET (LOCK_ESCALATION = TABLE)
GO
COMMIT








/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Currency SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Country ADD
	CurrencyID int NULL
GO
ALTER TABLE dbo.Country ADD CONSTRAINT
	FK_Country_Currency FOREIGN KEY
	(
	CurrencyID
	) REFERENCES dbo.Currency
	(
	CurrencyID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Country SET (LOCK_ESCALATION = TABLE)
GO
COMMIT






/****** Object:  View [dbo].[vw_Coupons]    Script Date: 9/26/2016 3:25:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create VIEW [dbo].[vw_Coupons]
AS
select a.*, ( select
			stuff((
					select ', ' + c1.Name
					from [CouponCategories] l1 
					left outer join CouponCategory c1 on l1.CategoryId = c1.CategoryId
					
					where l1.CouponId = l.CouponId
					--order by c1.CountryName
					for xml path('')
				),1,1,'') as name_csv
			from [dbo].[CouponCategories] l 
			where CouponId = a.CouponId
			group by l.CouponId  
		)  Categories,
		curr.CurrencyCode, curr.CurrencySymbol
		 from Coupon a
		left outer join Country cc on a.LocationCountryId = cc.CountryID
		left outer join Currency curr on curr.CurrencyID = cc.CurrencyID

GO




/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Company ADD
	CreationDateTime datetime NULL,
	NoOfBranches int NULL,
	BillingAddressName nvarchar(150) NULL
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT






/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers ADD
	PassportNo nvarchar(150) NULL
GO
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT









/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.AdCampaignEventHistory
	(
	EventId bigint NOT NULL IDENTITY (1, 1),
	EventStatus int NULL,
	CampaignID bigint NULL,
	EventDateTime datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.AdCampaignEventHistory ADD CONSTRAINT
	PK_AdCampaignEventHistory PRIMARY KEY CLUSTERED 
	(
	EventId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.AdCampaignEventHistory ADD CONSTRAINT
	FK_AdCampaignEventHistory_AdCampaign FOREIGN KEY
	(
	CampaignID
	) REFERENCES dbo.AdCampaign
	(
	CampaignID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AdCampaignEventHistory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT






/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.ProfileQuestionEventHistory
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.SurveyQuestionEventHistory
GO
COMMIT
BEGIN TRANSACTION
GO
DROP TABLE dbo.CouponEventHistory
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProfileQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaignEventHistory
	DROP CONSTRAINT FK_AdCampaignEventHistory_AdCampaign
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.AdCampaignEventHistory', N'CampaignEventHistory', 'OBJECT' 
GO
ALTER TABLE dbo.CampaignEventHistory
	DROP CONSTRAINT PK_AdCampaignEventHistory
GO
ALTER TABLE dbo.CampaignEventHistory ADD CONSTRAINT
	PK_CampaignEventHistory PRIMARY KEY CLUSTERED 
	(
	EventId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CampaignEventHistory ADD CONSTRAINT
	FK_CampaignEventHistory_AdCampaign FOREIGN KEY
	(
	CampaignID
	) REFERENCES dbo.AdCampaign
	(
	CampaignID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CampaignEventHistory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Coupon SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.EventStatuses
	(
	EventStatus int NOT NULL,
	EventName nvarchar(100) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.EventStatuses ADD CONSTRAINT
	PK_EventStatus PRIMARY KEY CLUSTERED 
	(
	EventStatus
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.EventStatuses SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProfileQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Coupon SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CampaignEventHistory ADD
	CouponId bigint NULL,
	SQID bigint NULL,
	PQID int NULL
GO
ALTER TABLE dbo.CampaignEventHistory ADD CONSTRAINT
	FK_CampaignEventHistory_EventStatus FOREIGN KEY
	(
	EventStatus
	) REFERENCES dbo.EventStatuses
	(
	EventStatus
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CampaignEventHistory ADD CONSTRAINT
	FK_CampaignEventHistory_Coupon FOREIGN KEY
	(
	CouponId
	) REFERENCES dbo.Coupon
	(
	CouponId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CampaignEventHistory ADD CONSTRAINT
	FK_CampaignEventHistory_SurveyQuestion FOREIGN KEY
	(
	SQID
	) REFERENCES dbo.SurveyQuestion
	(
	SQID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CampaignEventHistory ADD CONSTRAINT
	FK_CampaignEventHistory_ProfileQuestion FOREIGN KEY
	(
	PQID
	) REFERENCES dbo.ProfileQuestion
	(
	PQID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CampaignEventHistory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT






alter table Company add IsDeleted bit
alter table Company add DeleteDate datetime
alter table AspNetUsers add Title int
alter table CompanyBranch add IsDefault bit





/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.EventStatuses.EventStatus', N'Tmp_EventStatusId', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.EventStatuses.Tmp_EventStatusId', N'EventStatusId', 'COLUMN' 
GO
ALTER TABLE dbo.EventStatuses SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.CampaignEventHistory.EventStatus', N'Tmp_EventStatusId_1', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.CampaignEventHistory.Tmp_EventStatusId_1', N'EventStatusId', 'COLUMN' 
GO
ALTER TABLE dbo.CampaignEventHistory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT







/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.UserCouponView ADD
	userLocationLAT nvarchar(50) NULL,
	userLocationLONG nvarchar(50) NULL
GO
ALTER TABLE dbo.UserCouponView SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



-------------------------------------------  All previous scripts executed on live server on 9-30-2016








/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers
	DROP CONSTRAINT FK_AspNetUsers_Language
GO
ALTER TABLE dbo.Language SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers
	DROP CONSTRAINT FK_AspNetUsers_Industry
GO
ALTER TABLE dbo.Industry SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers
	DROP CONSTRAINT FK_AspNetUsers_Education
GO
ALTER TABLE dbo.Education SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers
	DROP CONSTRAINT FK_AspNetUsers_Company
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_AspNetUsers
	(
	Id nvarchar(128) NOT NULL,
	Email nvarchar(256) NULL,
	EmailConfirmed bit NOT NULL,
	PasswordHash nvarchar(MAX) NULL,
	SecurityStamp nvarchar(MAX) NULL,
	PhoneNumberConfirmed bit NOT NULL,
	TwoFactorEnabled bit NOT NULL,
	LockoutEndDateUtc datetime NULL,
	LockoutEnabled bit NOT NULL,
	AccessFailedCount int NOT NULL,
	UserName nvarchar(256) NOT NULL,
	FullName nvarchar(200) NOT NULL,
	AlternateEmail nvarchar(256) NULL,
	IsEmailVerified nchar(10) NULL,
	Status int NULL,
	CreatedDateTime datetime NULL,
	ModifiedDateTime datetime NULL,
	LastLoginTime datetime NULL,
	Phone1 nvarchar(100) NULL,
	Phone2 nvarchar(100) NULL,
	Jobtitle nvarchar(50) NULL,
	ContactNotes nvarchar(MAX) NULL,
	IsSubscribed bit NOT NULL,
	AppID int NULL,
	IsCompanyRepresentative bit NULL,
	UserTimeZone nvarchar(50) NULL,
	Gender int NULL,
	LanguageID int NULL,
	IndustryID int NULL,
	EducationId bigint NULL,
	ProfileImage nvarchar(200) NULL,
	UserCode nvarchar(300) NULL,
	SmsCode nvarchar(50) NULL,
	WebsiteLink nvarchar(100) NULL,
	DOB datetime NULL,
	CompanyId int NULL,
	authenticationToken nvarchar(500) NULL,
	DevicePlatform int NULL,
	optDealsNearMeEmails bit NULL,
	optLatestNewsEmails bit NULL,
	optMarketingEmails bit NULL,
	PassportNo nvarchar(150) NULL,
	Title nvarchar(50) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'1 - Male, 2 Female'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_AspNetUsers', N'COLUMN', N'Gender'
GO
IF EXISTS(SELECT * FROM dbo.AspNetUsers)
	 EXEC('INSERT INTO dbo.Tmp_AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, FullName, AlternateEmail, IsEmailVerified, Status, CreatedDateTime, ModifiedDateTime, LastLoginTime, Phone1, Phone2, Jobtitle, ContactNotes, IsSubscribed, AppID, IsCompanyRepresentative, UserTimeZone, Gender, LanguageID, IndustryID, EducationId, ProfileImage, UserCode, SmsCode, WebsiteLink, DOB, CompanyId, authenticationToken, DevicePlatform, optDealsNearMeEmails, optLatestNewsEmails, optMarketingEmails, PassportNo, Title)
		SELECT Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, FullName, AlternateEmail, IsEmailVerified, Status, CreatedDateTime, ModifiedDateTime, LastLoginTime, Phone1, Phone2, Jobtitle, ContactNotes, IsSubscribed, AppID, IsCompanyRepresentative, UserTimeZone, Gender, LanguageID, IndustryID, EducationId, ProfileImage, UserCode, SmsCode, WebsiteLink, DOB, CompanyId, authenticationToken, DevicePlatform, optDealsNearMeEmails, optLatestNewsEmails, optMarketingEmails, PassportNo, CONVERT(nvarchar(50), Title) FROM dbo.AspNetUsers WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.CouponCodes
	DROP CONSTRAINT FK_CouponCodes_AspNetUsers
GO
ALTER TABLE dbo.CompaniesAspNetUsers
	DROP CONSTRAINT FK_CompaniesAspNetUsers_AspNetUsers
GO
ALTER TABLE dbo.UserApps
	DROP CONSTRAINT FK_UserApps_AspNetUsers
GO
ALTER TABLE dbo.AspNetUserLogins
	DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE dbo.AspNetUserClaims
	DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE dbo.AdCampaignResponse
	DROP CONSTRAINT FK_AdCampaignResponse_AspNetUsers
GO
ALTER TABLE dbo.AspNetUserRoles
	DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE dbo.ProfileQuestionUserAnswer
	DROP CONSTRAINT FK_ProfileQuestionUserAnswer_AspNetUsers
GO
ALTER TABLE dbo.SurveyQuestionResponse
	DROP CONSTRAINT FK_SurveyQuestionResponse_AspNetUsers
GO
ALTER TABLE dbo.SurveyQuestion
	DROP CONSTRAINT FK_SurveyQuestion_AspNetUsers
GO
DROP TABLE dbo.AspNetUsers
GO
EXECUTE sp_rename N'dbo.Tmp_AspNetUsers', N'AspNetUsers', 'OBJECT' 
GO
ALTER TABLE dbo.AspNetUsers ADD CONSTRAINT
	[PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.AspNetUsers ADD CONSTRAINT
	FK_AspNetUsers_Company FOREIGN KEY
	(
	CompanyId
	) REFERENCES dbo.Company
	(
	CompanyId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AspNetUsers ADD CONSTRAINT
	FK_AspNetUsers_Education FOREIGN KEY
	(
	EducationId
	) REFERENCES dbo.Education
	(
	EducationId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AspNetUsers ADD CONSTRAINT
	FK_AspNetUsers_Industry FOREIGN KEY
	(
	IndustryID
	) REFERENCES dbo.Industry
	(
	IndustryID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AspNetUsers ADD CONSTRAINT
	FK_AspNetUsers_Language FOREIGN KEY
	(
	LanguageID
	) REFERENCES dbo.Language
	(
	LanguageID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestion ADD CONSTRAINT
	FK_SurveyQuestion_AspNetUsers FOREIGN KEY
	(
	UserID
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveyQuestionResponse ADD CONSTRAINT
	FK_SurveyQuestionResponse_AspNetUsers FOREIGN KEY
	(
	UserID
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveyQuestionResponse SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProfileQuestionUserAnswer ADD CONSTRAINT
	FK_ProfileQuestionUserAnswer_AspNetUsers FOREIGN KEY
	(
	UserID
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ProfileQuestionUserAnswer SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUserRoles ADD CONSTRAINT
	[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.AspNetUserRoles SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaignResponse ADD CONSTRAINT
	FK_AdCampaignResponse_AspNetUsers FOREIGN KEY
	(
	UserID
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AdCampaignResponse SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUserClaims ADD CONSTRAINT
	[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.AspNetUserClaims SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUserLogins ADD CONSTRAINT
	[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.AspNetUserLogins SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.UserApps ADD CONSTRAINT
	FK_UserApps_AspNetUsers FOREIGN KEY
	(
	UserID
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UserApps SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CompaniesAspNetUsers ADD CONSTRAINT
	FK_CompaniesAspNetUsers_AspNetUsers FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CompaniesAspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CouponCodes ADD CONSTRAINT
	FK_CouponCodes_AspNetUsers FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CouponCodes SET (LOCK_ESCALATION = TABLE)
GO
COMMIT









GO

/****** Object:  View [dbo].[vw_ReferringCompanies]    Script Date: 9/30/2016 4:57:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ReferringCompanies]
AS
 select u.FullName,u.Email, c.CompanyName, vads.vcount,scards.scount,pq.pcount, c.ReferringCompanyID
 
  from Company c 
 inner join AspNetUsers u on u.CompanyId = c.CompanyId
 outer apply (
	select count(*) vcount from AdCampaign ad where status = 3 and ad.CompanyId = c.CompanyId
 ) vads
 outer apply (
	select count(*) scount from SurveyQuestion ad where status = 3  and ad.CompanyId = c.CompanyId
 ) scards
  outer apply (
	select count(*) pcount from ProfileQuestion ad where status = 3  and ad.CompanyId = c.CompanyId
 ) pq
 
 

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Account"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 213
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_ReferringCompanies'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_ReferringCompanies'
GO










/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaign SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.AdCampaignClickRateHistory
	(
	ClickRateId bigint NOT NULL IDENTITY (1, 1),
	CampaignID bigint NULL,
	ClickRate float(53) NULL,
	RateChangeDateTime datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.AdCampaignClickRateHistory ADD CONSTRAINT
	PK_AdCampaignClickRateHistory PRIMARY KEY CLUSTERED 
	(
	ClickRateId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.AdCampaignClickRateHistory ADD CONSTRAINT
	FK_AdCampaignClickRateHistory_AdCampaign FOREIGN KEY
	(
	CampaignID
	) REFERENCES dbo.AdCampaign
	(
	CampaignID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AdCampaignClickRateHistory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT







/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.PayOutHistory
	DROP CONSTRAINT FK_PayOutHistory_Company
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_PayOutHistory
	(
	PayOutId bigint NOT NULL IDENTITY (1, 1),
	CompanyId int NULL,
	RequestDateTime datetime NULL,
	CentzAmount float(53) NULL,
	DollarAmount float(53) NULL,
	StageOneStatus int NULL,
	StageOneRejectionReason nvarchar(500) NULL,
	StageOneEventDate datetime NULL,
	StageOneUserId nvarchar(128) NULL,
	StageTwoStatus int NULL,
	StageTwoRejectionReason nvarchar(500) NULL,
	StageTwoEventDate datetime NULL,
	StageTwoUserId nvarchar(128) NULL,
	TargetPayoutAccount nvarchar(500) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_PayOutHistory SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_PayOutHistory ON
GO
IF EXISTS(SELECT * FROM dbo.PayOutHistory)
	 EXEC('INSERT INTO dbo.Tmp_PayOutHistory (PayOutId, CompanyId, RequestDateTime, CentzAmount, DollarAmount, StageOneStatus, StageOneRejectionReason, StageOneEventDate, StageOneUserId, StageTwoStatus, StageTwoRejectionReason, StageTwoEventDate, StageTwoUserId, TargetPayoutAccount)
		SELECT PayOutId, CompanyId, RequestDateTime, CentzAmount, DollarAmount, CONVERT(int, StageOneStatus), StageOneRejectionReason, StageOneEventDate, StageOneUserId, CONVERT(int, StageTwoStatus), StageTwoRejectionReason, StageTwoEventDate, StageTwoUserId, TargetPayoutAccount FROM dbo.PayOutHistory WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_PayOutHistory OFF
GO
DROP TABLE dbo.PayOutHistory
GO
EXECUTE sp_rename N'dbo.Tmp_PayOutHistory', N'PayOutHistory', 'OBJECT' 
GO
ALTER TABLE dbo.PayOutHistory ADD CONSTRAINT
	PK_PayOutHistory PRIMARY KEY CLUSTERED 
	(
	PayOutId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PayOutHistory ADD CONSTRAINT
	FK_PayOutHistory_Company FOREIGN KEY
	(
	CompanyId
	) REFERENCES dbo.Company
	(
	CompanyId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT



-------------------------------------- all scripts above executed on server








/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Company ADD
	IsSpecialAccount bit NULL
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



-------------------------------------- all scripts above executed on server   7 october.
/****** Added by hadia to delete previous SPs ******/

GO

/****** Object:  StoredProcedure [dbo].[GetActiveVSNewUsers]    Script Date: 10/7/2016 5:01:55 PM ******/
DROP PROCEDURE [dbo].[GetActiveVSNewUsers]
GO

GO
/****** Object:  StoredProcedure [dbo].[getApprovedCampaignsOverTime]    Script Date: 10/7/2016 5:01:55 PM ******/
DROP PROCEDURE [dbo].[getApprovedCampaignsOverTime]
GO

GO
/****** Object:  StoredProcedure [dbo].[GetRevenueOverTime]    Script Date: 10/7/2016 5:01:55 PM ******/
DROP PROCEDURE [dbo].[GetRevenueOverTime]
GO

GO
/****** Object:  StoredProcedure [dbo].[GetDealMetric]    Script Date: 10/7/2016 5:01:55 PM ******/
DROP PROCEDURE [dbo].[GetDealMetric]
GO


GO
/****** Object:  StoredProcedure [dbo].[GetSurvayCardsAnswered]    Script Date: 10/7/2016 5:01:55 PM ******/
DROP PROCEDURE [dbo].[GetSurvayCardsAnswered]
GO


GO
/****** Object:  StoredProcedure [dbo].[GetSurvayQestionsAnswered]    Script Date: 10/7/2016 5:01:55 PM ******/
DROP PROCEDURE [dbo].[GetSurvayQestionsAnswered]
GO





CREATE PROCEDURE dbo.GetApprovalCount
AS
BEGIN

 
  select (SELECT COUNT(CouponId) From Coupon where Status = 2) as CouponCount,
   (SELECT COUNT(CampaignID) From AdCampaign  where Status = 2 and type =1) as AdCmpaignCount,
   (SELECT COUNT(CampaignID) From AdCampaign  where Status = 2 and type =4) as DisplayAdCount,
   (SELECT COUNT(PQID) From ProfileQuestion  where Status = 2) as ProfileQuestionCount,
   (SELECT COUNT(SQID) From SurveyQuestion  where Status = 2) as SurveyQuestionCount


 


 
END
GO