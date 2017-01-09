﻿/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
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







GO
/****** Object:  UserDefinedFunction [dbo].[GetUserSurveySelectionPercentage]    Script Date: 10/13/2016 1:44:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[GetUserSurveySelectionPercentage]
(	--  select * from [GetUserSurveySelectionPercentage](10300)
	-- Add the parameters for the function here
	@sqId int = 0
)
RETURNS 
@SurveySelectionPercentage TABLE
(
	-- Add the column definitions for the TABLE variable here
	leftImagePercentage float null, 
	rightImagePercentage float null
)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	DECLARE @surveyResponses float
	SELECT @surveyResponses = count(*) from SurveyQuestionResponse where SQID = @sqId

	-- Add the SELECT statement with parameter references here
	insert into @SurveySelectionPercentage
	values
	((select 
	CASE
		WHEN @surveyResponses is null or @surveyResponses <= 0
		THEN @surveyResponses
		WHEN @surveyResponses > 0
		THEN  
		CEILING((count(*) / @surveyResponses) * 100)
	END as leftImagePercentage
	  from SurveyQuestionResponse
	where SQID = @sqId and UserSelection = 1),
	((select 
	CASE
		WHEN @surveyResponses is null or @surveyResponses <= 0
		THEN @surveyResponses
		WHEN @surveyResponses > 0
		THEN  
		floor(((count(*) / @surveyResponses) * 100))
	END as rightImagePercentage
	 from SurveyQuestionResponse
	where SQID = @sqId and UserSelection = 2)))

	RETURN 
END


------------------------------------------ all above scripts executed on live server.






GO
/****** Object:  StoredProcedure [dbo].[GetTransactions]    Script Date: 10/14/2016 5:51:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		mz
-- Create date: 

---- Description:	        AdClick = 1,
--        ApproveSurvey = 2,
--        ViewSurveyReport = 3,
--        CouponPurchased = 4,
--        SurveyWatched = 5,
--        ProfileQuestionAnswered = 6,
--        ApproveProfileQuestion = 7,
--        ApproveCoupon = 8,
--        ApproveAd = 9,
--        UserCashOutPaypal = 10,
--        AdWeeklyCollection = 11,
--        WelcomeGiftBalance = 12,
--        PromotionalCentz = 13,
--        ReferFriendBalance = 14

-- exec GetTransactions 3126,4,30
-- =============================================
ALTER PROCEDURE [dbo].[GetTransactions] 
	-- Add the parameters for the stored procedure here
	@CompanyID int = 0, 
	@AccountType int = 0,
	@Rows int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	select * 
	from (

					SELECT t.[TxID]
						  ,t.[TransactionDate]
						  ,t.[Type]
						  ,isnull(t.[DebitCredit],0) as [DebitCredit]
						  ,isnull(t.[CreditAmount],0) as [CreditAmount]
						  ,isnull(t.[DebitAmount],0) as [DebitAmount]
						  ,t.[AccountID]
						  ,t.[TaxPerc]
						  ,t.[TaxValue]
						  ,t.[AdCampaignID]
						  ,t.[SQID]
						  ,t.[isProcessed]
						  ,t.[CurrencyID]
						  ,t.[CurrencyRateID]
						  ,t.[Sequence]
						  ,t.[CouponId]
						  ,t.[AccountBalance]
						  ,t.[PQID],
					  a.accountbalance CurrentBalance,
					case when t.[type] = 1 then 'Ad Viewed ' + ad.CampaignName
							when t.[type] = 2 then 'Survey Approved ' + sq.Question
							when t.[type] = 4 then 'Coupon Purchased ' + CouponTitle
							when t.[type] = 5 then 'Survey Answered ' + sq.Question
							when t.[type] = 6 then 'Profile Question Answered ' + pq.Question
							when t.[type] = 7 then 'Profile Question Approved ' + pq.Question
							when t.[type] = 8 then 'Coupon Offer Approved ' + c.CouponTitle
							when t.[type] = 9 then 'Ad Approved ' + ad.CampaignName 
							when t.[type] = 10 then 'Cashout to Paypal' 
							when t.[type] = 11 then 'Weekly Ad Collection ' + ad.CampaignName 
							when t.[type] = 12 then 'Welcome Gift Balance '
							when t.[type] = 13 then 'Promotional Centz Awarded' 
							when t.[type] = 14 then 'Refer friend Balance '
		  
					end as description



					from [Transaction] t
					inner join Account a on t.AccountID = a.AccountId and a.CompanyId = @CompanyID and a.AccountType = @AccountType
					inner join Company co on a.CompanyId = co.CompanyId
					left outer join AdCampaign ad on t.AdCampaignID = ad.CampaignID
					left outer join Coupon c on t.CouponId = c.CouponId
					left outer join ProfileQuestion pq on t.PQID = pq.PQID
					left outer join SurveyQuestion sq on t.SQID = sq.SQID
					



	)  a
		
	order by Transactiondate DESC
	OFFSET 0 ROWS
	FETCH NEXT @Rows ROWS ONLY
END




ALTER TABLE AdCampaign
ADD ShowBuyitBtn bit





GO
/****** Object:  StoredProcedure [dbo].[SearchCampaigns]    Script Date: 10/14/2016 3:43:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mz
-- Create date: 8 dec 2015
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[SearchCampaigns] 
--  exec [SearchCampaigns] 0,'',416,0,10,0
	-- Add the parameters for the stored procedure here
	@Status int,
	@keyword nvarchar(100),
	@companyId int,
	@fromRoww int,
	@toRow int,
	@adminMode bit
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--select @toRow, @fromRow
	--return


	select *, COUNT(*) OVER() AS TotalItems
	from (

    -- Insert statements for procedure here
	select  a.CampaignID,a.CompanyId, a.CampaignName,
	(select count (*) from adcampaignresponse cr where cr.campaignid = a.campaignid and  cast(CreatedDateTime as DATE)  = cast (GETDATE() as DATE) ) viewCountToday,
	(select count (*) from adcampaignresponse cr where cr.campaignid = a.campaignid and cast(CreatedDateTime as date) = cast(getdate()-1 as date)) viewCountYesterday,
	(select count (*) from adcampaignresponse cr where cr.campaignid = a.campaignid) viewCountAllTime,
	left(( select
			stuff((
					select ', ' + c1.CountryName, ', ' + ci.CityName
					from [AdCampaignTargetLocation] l1 
					left outer join country c1 on l1.countryid = c1.countryid
					left outer join city ci on l1.CityID = ci.CityID
					where l1.campaignid = l.campaignid
					order by c1.CountryName
					for xml path('')
				),1,1,'') as name_csv
			from [AdCampaignTargetLocation] l 
			where CampaignID = a.CampaignID
			group by l.campaignid  
		),30) +'..' Locationss,
		
		 StartDateTime, MaxBudget, MaxDailyBudget,AmountSpent,
		Status, ApprovalDateTime, ClickRate, a.CreatedDateTime, a.Type, a.priority
		
		
		
	
	 from AdCampaign a
	 
	--inner join AspNetUsers u on 
	
	
	--left outer join AdCampaignResponse aResp on a.CampaignID = aResp.CampaignID 
	where 
	(
		(@keyword is null or (a.CampaignName like '%'+ @keyword +'%' or a.DisplayTitle like '%'+ @keyword +'%'  ))
		and a.Status <> 7
		and 
		( @Status = 0 or a.[status] = @Status)
		and  ( @adminMode = 1 or a.companyid = @companyId )

	)
	


	group by CampaignID, CampaignName,StartDateTime,MaxBudget,MaxDailyBudget,AmountSpent,Status, ApprovalDateTime, ClickRate, a.CreatedDateTime, a.Type, a.priority,a.CompanyId
		)as items
	order by priority desc
	OFFSET @fromRoww ROWS
	FETCH NEXT @toRow ROWS ONLY
	
END



------------------------------------------ all above scripts executed on live server.




ALTER TABLE Coupon
ADD YoutubeLink varchar(MAX)


ALTER TABLE CouponPriceOption
ADD ExpiryDate datetime

ALTER TABLE CouponPriceOption
ADD URL varchar(MAX)


ALTER TABLE Coupon
ADD CouponImage4 nvarchar(MAX)

ALTER TABLE Coupon
ADD CouponImage5 nvarchar(MAX)

ALTER TABLE Coupon
ADD CouponImage6 nvarchar(MAX)



------------------------------------------ all above scripts executed on live server.



ALTER TABLE AdCampaign
ADD IsPaymentCollected bit

ALTER TABLE AdCampaign
ADD PaymentDate datetime



ALTER TABLE Coupon
ADD IsPaymentCollected bit

ALTER TABLE Coupon
ADD PaymentDate datetime


------------------------------------------ all above scripts executed on live server.



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  <Author,,Name>
-- Create date: <Create Date,,>
-- Description: <Description,,>
-- =============================================
alter PROCEDURE GetRegisteredUserData
-- GetRegisteredUserData 0,'',1,10
 @Status int,
 @keyword nvarchar(100),
 @fromRoww int,
 @toRow int
 
AS
BEGIN
SET NOCOUNT ON;


select *, COUNT(*) OVER() AS TotalItems
 from (


  SELECT AspNetUsers.Id,AspNetUsers.fullname,AspNetUsers.LastLoginTime,AspNetUsers.Email,AspNetUsers.[Status],Company.CompanyId, Company.CompanyName,acc.AccountBalance
  FROM AspNetUsers
  INNER JOIN Company ON AspNetUsers.CompanyId=Company.CompanyId
  inner join Account acc on company.CompanyId = acc.CompanyId and acc.AccountType = 4
  where AspNetUsers.EmailConfirmed =1
  )o
  
   order by LastLoginTime desc
  OFFSET @fromRoww ROWS
  FETCH NEXT @toRow ROWS ONLY
 

 

END






ALTER TABLE profilequestionuseranswer ALTER COLUMN PQAnswerID int NULL




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
	StripeSubscriptionId nvarchar(70) NULL,
	StripeSubscriptionStatus nvarchar(50) NULL
GO
ALTER TABLE dbo.Company
	DROP COLUMN ChargeBeesubscriptionID
GO
ALTER TABLE dbo.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



ALTER TABLE dbo.Invoice ADD
	StripeReceiptNo nvarchar(50) NULL,
	StripeInvoiceId nvarchar(50) NULL




GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[vw_Coupons]
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
  left join Country cc on a.LocationCountryId = cc.CountryID
  left join Currency curr on curr.CurrencyID = cc.CurrencyID
  


GO




USE [SMDv2]
GO

/****** Object:  View [dbo].[vw_CompanyUsers]    Script Date: 10/27/2016 6:52:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER VIEW [dbo].[vw_CompanyUsers]
AS

select cu.id,u.id as UserId, 

(case when u.email is null then cu.InvitationEmail else u.email end) as email


, u.FullName, r.Name as RoleName, cu.CreatedOn, (case  when cu.status = 1 then 'Invitation Sent' when cu.status = 2 then 'Active' end) status, cu.companyid , c.CompanyName, cu.RoleId, c.Logo
from dbo.CompaniesAspNetUsers  cu
inner join   dbo.AspNetRoles r on r.Id = roleid
left outer join AspNetUsers u on u.Id = cu.userid
inner join Company c on cu.CompanyId = c.CompanyId





GO

ALTER TABLE dbo.Coupon
ADD IsShowReviews bit Null,
 IsShowAddress bit Null,
 IsShowPhoneNo bit Null,
 IsShowMap     bit Null,
 IsShowyouTube bit Null,
 IsShowAboutUs bit Null



 use smdv2
GO
/****** Object:  StoredProcedure [dbo].[SearchCampaigns]    Script Date: 01/11/2016 10:16:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mz
-- Create date: 8 dec 2015
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[SearchCampaigns] 
--  exec [SearchCampaigns] 0,'',416,0,10,0
	-- Add the parameters for the stored procedure here
	@Status int,
	@keyword nvarchar(100),
	@companyId int,
	@fromRoww int,
	@toRow int,
	@adminMode bit
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--select @toRow, @fromRow
	--return


	select *, COUNT(*) OVER() AS TotalItems
	from (

    -- Insert statements for procedure here
	select  a.CampaignID,a.CompanyId, a.CampaignName,
	(select isnull(count (*),0) from adcampaignresponse cr where cr.campaignid = a.campaignid and  cast(CreatedDateTime as DATE)  = cast (GETDATE() as DATE) and responsetype = 1 ) viewCountToday,
	(select isnull(count (*),0) from adcampaignresponse cr where cr.campaignid = a.campaignid and cast(CreatedDateTime as date) = cast(getdate()-1 as date) and responsetype = 1) viewCountYesterday,
	(select isnull(count (*),0) from adcampaignresponse cr where cr.campaignid = a.campaignid and responsetype = 1) viewCountAllTime,
	(select isnull(count (*),0) from adcampaignresponse cr where cr.campaignid = a.campaignid and  cast(CreatedDateTime as DATE)  = cast (GETDATE() as DATE) and responsetype = 2 ) clickThroughsToday,
	(select isnull(count (*),0) from adcampaignresponse cr where cr.campaignid = a.campaignid and cast(CreatedDateTime as date) = cast(getdate()-1 as date) and responsetype = 2) clickThroughsYesterday,
	(select isnull(count (*),0)from adcampaignresponse cr where cr.campaignid = a.campaignid and responsetype = 2) clickThroughsAllTime,
	left(( select
			stuff((
					select ', ' + c1.CountryName, ', ' + ci.CityName
					from [AdCampaignTargetLocation] l1 
					left outer join country c1 on l1.countryid = c1.countryid
					left outer join city ci on l1.CityID = ci.CityID
					where l1.campaignid = l.campaignid
					order by c1.CountryName
					for xml path('')
				),1,1,'') as name_csv
			from [AdCampaignTargetLocation] l 
			where CampaignID = a.CampaignID
			group by l.campaignid  
		),30) +'..' Locationss,
		
		 StartDateTime, MaxBudget, MaxDailyBudget,AmountSpent,
		Status, ApprovalDateTime, ClickRate, a.CreatedDateTime, a.Type, a.priority
		
		
		
	
	 from AdCampaign a
	 
	--inner join AspNetUsers u on 
	
	
	--left outer join AdCampaignResponse aResp on a.CampaignID = aResp.CampaignID 
	where 
	(
		(@keyword is null or (a.CampaignName like '%'+ @keyword +'%' or a.DisplayTitle like '%'+ @keyword +'%'  ))
		and a.Status <> 7
		and 
		( @Status = 0 or a.[status] = @Status)
		and  ( @adminMode = 1 or a.companyid = @companyId )

	)
	


	group by CampaignID, CampaignName,StartDateTime,MaxBudget,MaxDailyBudget,AmountSpent,Status, ApprovalDateTime, ClickRate, a.CreatedDateTime, a.Type, a.priority,a.CompanyId
		)as items
	order by priority desc
	OFFSET @fromRoww ROWS
	FETCH NEXT @toRow ROWS ONLY
	
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
CREATE TABLE dbo.SharedSurveyQuestion
	(
	SSQID bigint NOT NULL IDENTITY (1, 1),
	UserId nvarchar(128) NULL,
	CompanyId int NULL,
	SurveyTitle nvarchar(200) NULL,
	LeftPicturePath nvarchar(300) NULL,
	RightPicturePath nvarchar(300) NULL,
	CreationDate datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.SharedSurveyQuestion ADD CONSTRAINT
	PK_SharedSurveyQuestion PRIMARY KEY CLUSTERED 
	(
	SSQID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SharedSurveyQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.SurveySharingGroupMembers
	(
	MemberId bigint NULL,
	UserId nvarchar(128) NULL,
	PhoneNumber nvarchar(150) NULL,
	MemberStatus int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.SurveySharingGroupMembers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.SurveySharingGroup
	(
	GroupId bigint NOT NULL IDENTITY (1, 1),
	CompanyId int NULL,
	UserId nvarchar(128) NULL,
	GroupName nvarchar(500) NULL,
	CreationDate datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.SurveySharingGroup ADD CONSTRAINT
	PK_SurveySharingGroup PRIMARY KEY CLUSTERED 
	(
	GroupId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SurveySharingGroup SET (LOCK_ESCALATION = TABLE)
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
ALTER TABLE dbo.SharedSurveyQuestion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.SurveySharingGroupShares
	(
	ShareId bigint NOT NULL IDENTITY (1, 1),
	SharingGroupId bigint NULL,
	UserId nvarchar(128) NULL,
	SharingGroupMemberId bigint NULL,
	SharingDate datetime NULL,
	SSQID bigint NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.SurveySharingGroupShares ADD CONSTRAINT
	PK_SurveySharingGroupShares PRIMARY KEY CLUSTERED 
	(
	ShareId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SurveySharingGroupShares ADD CONSTRAINT
	FK_SurveySharingGroupShares_SharedSurveyQuestion FOREIGN KEY
	(
	SSQID
	) REFERENCES dbo.SharedSurveyQuestion
	(
	SSQID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveySharingGroupShares SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.SharedSurveyQuestionResponse
	(
	SSQResponseId bigint NOT NULL IDENTITY (1, 1),
	SSQID bigint NULL,
	UserId nvarchar(128) NULL,
	ResponseDateTime datetime NULL,
	UserSelection int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.SharedSurveyQuestionResponse ADD CONSTRAINT
	PK_SharedSurveyQuestionResponse PRIMARY KEY CLUSTERED 
	(
	SSQResponseId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SharedSurveyQuestionResponse ADD CONSTRAINT
	FK_SharedSurveyQuestionResponse_SharedSurveyQuestion FOREIGN KEY
	(
	SSQID
	) REFERENCES dbo.SharedSurveyQuestion
	(
	SSQID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SharedSurveyQuestionResponse SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.SurveySharingGroup.GroupId', N'Tmp_SharingGroupId', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.SurveySharingGroup.Tmp_SharingGroupId', N'SharingGroupId', 'COLUMN' 
GO
ALTER TABLE dbo.SurveySharingGroup SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SurveySharingGroupMembers
	(
	SharingGroupMemberId bigint NOT NULL,
	UserId nvarchar(128) NULL,
	PhoneNumber nvarchar(150) NULL,
	MemberStatus int NULL,
	SharingGroupId bigint NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SurveySharingGroupMembers SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.SurveySharingGroupMembers)
	 EXEC('INSERT INTO dbo.Tmp_SurveySharingGroupMembers (SharingGroupMemberId, UserId, PhoneNumber, MemberStatus)
		SELECT MemberId, UserId, PhoneNumber, MemberStatus FROM dbo.SurveySharingGroupMembers WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.SurveySharingGroupMembers
GO
EXECUTE sp_rename N'dbo.Tmp_SurveySharingGroupMembers', N'SurveySharingGroupMembers', 'OBJECT' 
GO
ALTER TABLE dbo.SurveySharingGroupMembers ADD CONSTRAINT
	PK_SurveySharingGroupMembers PRIMARY KEY CLUSTERED 
	(
	SharingGroupMemberId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SurveySharingGroupMembers ADD CONSTRAINT
	FK_SurveySharingGroupMembers_SurveySharingGroup FOREIGN KEY
	(
	SharingGroupId
	) REFERENCES dbo.SurveySharingGroup
	(
	SharingGroupId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
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
ALTER TABLE dbo.SurveySharingGroupMembers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveySharingGroupShares ADD CONSTRAINT
	FK_SurveySharingGroupShares_SurveySharingGroupMembers FOREIGN KEY
	(
	SharingGroupMemberId
	) REFERENCES dbo.SurveySharingGroupMembers
	(
	SharingGroupMemberId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveySharingGroupShares SET (LOCK_ESCALATION = TABLE)
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
EXECUTE sp_rename N'dbo.SurveySharingGroupShares.ShareId', N'Tmp_SurveyQuestionShareId_2', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.SurveySharingGroupShares.Tmp_SurveyQuestionShareId_2', N'SurveyQuestionShareId', 'COLUMN' 
GO
ALTER TABLE dbo.SurveySharingGroupShares SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Notifications ADD
	SurveyQuestionShareId bigint NULL
GO
ALTER TABLE dbo.Notifications ADD CONSTRAINT
	FK_Notifications_SurveySharingGroupShares FOREIGN KEY
	(
	SurveyQuestionShareId
	) REFERENCES dbo.SurveySharingGroupShares
	(
	SurveyQuestionShareId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Notifications SET (LOCK_ESCALATION = TABLE)
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
DROP TABLE dbo.SharedSurveyQuestionResponse
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveySharingGroupShares ADD
	ResponseDateTime datetime NULL,
	UserSelection int NULL
GO
ALTER TABLE dbo.SurveySharingGroupShares SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SharedSurveyQuestion SET (LOCK_ESCALATION = TABLE)
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
ALTER TABLE dbo.SurveySharingGroupMembers ADD
	FullName nvarchar(250) NULL
GO
ALTER TABLE dbo.SurveySharingGroupMembers SET (LOCK_ESCALATION = TABLE)
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
ALTER TABLE dbo.SurveySharingGroupMembers
	DROP CONSTRAINT FK_SurveySharingGroupMembers_SurveySharingGroup
GO
ALTER TABLE dbo.SurveySharingGroup SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SurveySharingGroupMembers
	(
	SharingGroupMemberId bigint NOT NULL IDENTITY (1, 1),
	UserId nvarchar(128) NULL,
	PhoneNumber nvarchar(150) NULL,
	MemberStatus int NULL,
	SharingGroupId bigint NULL,
	FullName nvarchar(250) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SurveySharingGroupMembers SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SurveySharingGroupMembers ON
GO
IF EXISTS(SELECT * FROM dbo.SurveySharingGroupMembers)
	 EXEC('INSERT INTO dbo.Tmp_SurveySharingGroupMembers (SharingGroupMemberId, UserId, PhoneNumber, MemberStatus, SharingGroupId, FullName)
		SELECT SharingGroupMemberId, UserId, PhoneNumber, MemberStatus, SharingGroupId, FullName FROM dbo.SurveySharingGroupMembers WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SurveySharingGroupMembers OFF
GO
ALTER TABLE dbo.SurveySharingGroupShares
	DROP CONSTRAINT FK_SurveySharingGroupShares_SurveySharingGroupMembers
GO
DROP TABLE dbo.SurveySharingGroupMembers
GO
EXECUTE sp_rename N'dbo.Tmp_SurveySharingGroupMembers', N'SurveySharingGroupMembers', 'OBJECT' 
GO
ALTER TABLE dbo.SurveySharingGroupMembers ADD CONSTRAINT
	PK_SurveySharingGroupMembers PRIMARY KEY CLUSTERED 
	(
	SharingGroupMemberId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SurveySharingGroupMembers ADD CONSTRAINT
	FK_SurveySharingGroupMembers_SurveySharingGroup FOREIGN KEY
	(
	SharingGroupId
	) REFERENCES dbo.SurveySharingGroup
	(
	SharingGroupId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SurveySharingGroupShares ADD CONSTRAINT
	FK_SurveySharingGroupShares_SurveySharingGroupMembers FOREIGN KEY
	(
	SharingGroupMemberId
	) REFERENCES dbo.SurveySharingGroupMembers
	(
	SharingGroupMemberId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SurveySharingGroupShares SET (LOCK_ESCALATION = TABLE)
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
ALTER TABLE dbo.Notifications ADD
	PhoneNumber nvarchar(150) NULL
GO
ALTER TABLE dbo.Notifications SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



--all scripts above executed on live server  11 nov 2016





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
ALTER TABLE dbo.SurveySharingGroup SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SharedSurveyQuestion ADD
	SharingGroupId bigint NULL
GO
ALTER TABLE dbo.SharedSurveyQuestion ADD CONSTRAINT
	FK_SharedSurveyQuestion_SurveySharingGroup FOREIGN KEY
	(
	SharingGroupId
	) REFERENCES dbo.SurveySharingGroup
	(
	SharingGroupId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SharedSurveyQuestion SET (LOCK_ESCALATION = TABLE)
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
ALTER TABLE dbo.SurveySharingGroupShares ADD
	Status int NULL
GO
ALTER TABLE dbo.SurveySharingGroupShares SET (LOCK_ESCALATION = TABLE)
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
ALTER TABLE dbo.Game ADD
	GameSmallImage nvarchar(500) NULL,
	GameLargeImage nvarchar(500) NULL
GO
ALTER TABLE dbo.Game SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



ALTER TABLE Coupon
  ADD IsMarketingStories bit Null;






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
CREATE TABLE dbo.UserGameResponse
	(
	ResponseId bigint NOT NULL,
	UserId nvarchar(128) NULL,
	GameId bigint NULL,
	ResponseDateTime datetime NULL,
	PlayTime float(53) NULL,
	Score int NULL,
	Accuracy float(53) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.UserGameResponse ADD CONSTRAINT
	PK_UserGameResponse PRIMARY KEY CLUSTERED 
	(
	ResponseId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.UserGameResponse SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.GameExerciseCategory
	(
	ExerciseCategoryId int NOT NULL IDENTITY (1, 1),
	ExerciseCategoryName nvarchar(150) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.GameExerciseCategory ADD CONSTRAINT
	PK_GameExerciseCategory PRIMARY KEY CLUSTERED 
	(
	ExerciseCategoryId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.GameExerciseCategory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Game SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.GameExerciseCategories
	(
	GameId bigint NOT NULL,
	ExerciseCategoryId int NOT NULL,
	CategoryContribution int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.GameExerciseCategories ADD CONSTRAINT
	PK_GameExerciseCategories PRIMARY KEY CLUSTERED 
	(
	GameId,
	ExerciseCategoryId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.GameExerciseCategories ADD CONSTRAINT
	FK_GameExerciseCategories_GameExerciseCategory FOREIGN KEY
	(
	ExerciseCategoryId
	) REFERENCES dbo.GameExerciseCategory
	(
	ExerciseCategoryId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.GameExerciseCategories ADD CONSTRAINT
	FK_GameExerciseCategories_Game FOREIGN KEY
	(
	GameId
	) REFERENCES dbo.Game
	(
	GameId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.GameExerciseCategories SET (LOCK_ESCALATION = TABLE)
GO
COMMIT









GO

/****** Object:  View [dbo].[vw_Notifications]    Script Date: 11/17/2016 9:48:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- select * from [vw_Notifications]
alter VIEW [dbo].[vw_Notifications]
AS
SELECT        n.ID, Type, n.UserID, IsRead, GeneratedOn, GeneratedBy, n.SurveyQuestionShareId, n.PhoneNumber,
				(case when n.[type] = 1 then u.FullName +  ' wants your opinion' else '' end) NotificationDetails,
				(case when n.[type] = 1 then q.SurveyTitle else '' end) PollTitle
FROM            dbo.Notifications n
			left outer join SurveySharingGroupShares s on n.SurveyQuestionShareId = s.SurveyQuestionShareId
			left outer join SharedSurveyQuestion q on q.SSQID = s.SSQID
			left outer join AspNetUsers u on q.UserId = u.Id

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
ALTER TABLE dbo.AspNetUsers ADD
	Phone1CountryCode nvarchar(50) NULL
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
ALTER TABLE dbo.Country ADD
	CountryPhoneCode nvarchar(50) NULL
GO
ALTER TABLE dbo.Country SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




update c
set c.CountryPhoneCode = p.code
from country c
inner join [dbo].[Country_Phone_codes] p on c.countryname like CONCAT('%', p.name, '%') 








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
CREATE TABLE dbo.CouponRatingReview
	(
	CouponReviewId bigint NOT NULL IDENTITY (1, 1),
	CouponId bigint NULL,
	StarRating int NULL,
	Review nvarchar(800) NULL,
	RatingDateTime datetime NULL,
	UserId nvarchar(128) NULL,
	CompanyId int NULL,
	Status int NULL,
	ReviewImage1 nvarchar(250) NULL,
	ReviewImage2 nvarchar(250) NULL,
	Reviewimage3 nvarchar(250) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.CouponRatingReview ADD CONSTRAINT
	PK_CouponRatingReview PRIMARY KEY CLUSTERED 
	(
	CouponReviewId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CouponRatingReview ADD CONSTRAINT
	FK_CouponRatingReview_Coupon FOREIGN KEY
	(
	CouponId
	) REFERENCES dbo.Coupon
	(
	CouponId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CouponRatingReview SET (LOCK_ESCALATION = TABLE)
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
ALTER TABLE dbo.AspNetUsers
	DROP COLUMN Phone1CountryCode
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
ALTER TABLE dbo.Country SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers ADD
	Phone1CodeCountryID int NULL
GO
ALTER TABLE dbo.AspNetUsers ADD CONSTRAINT
	FK_AspNetUsers_Country FOREIGN KEY
	(
	Phone1CodeCountryID
	) REFERENCES dbo.Country
	(
	CountryID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT






GO

/****** Object:  View [dbo].[vw_Notifications]    Script Date: 11/23/2016 10:12:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- select * from [vw_Notifications]
ALTER VIEW [dbo].[vw_Notifications]
AS
SELECT        n.ID, Type, n.UserID, IsRead, GeneratedOn, GeneratedBy, n.SurveyQuestionShareId, n.PhoneNumber,
				(case when n.[type] = 1 then u.FullName +  ' wants your opinion' else '' end) NotificationDetails,
				(case when n.[type] = 1 then q.SurveyTitle else '' end) PollTitle, 
				(case when n.[type] = 1 then q.SSQID else 0 end) SSQID

				
FROM            dbo.Notifications n
			left outer join SurveySharingGroupShares s on n.SurveyQuestionShareId = s.SurveyQuestionShareId
			left outer join SharedSurveyQuestion q on q.SSQID = s.SSQID
			left outer join AspNetUsers u on q.UserId = u.Id


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
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.AspNetUsersNotificationTokens
	(
	NotificationTokenId bigint NOT NULL IDENTITY (1, 1),
	UserId nvarchar(128) NULL,
	ClientType int NULL,
	Token nvarchar(500) NULL,
	DateAdded datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.AspNetUsersNotificationTokens ADD CONSTRAINT
	PK_AspNetUsersNotificationTokens PRIMARY KEY CLUSTERED 
	(
	NotificationTokenId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.AspNetUsersNotificationTokens ADD CONSTRAINT
	FK_AspNetUsersNotificationTokens_AspNetUsers FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AspNetUsersNotificationTokens SET (LOCK_ESCALATION = TABLE)
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
ALTER TABLE dbo.Notifications ADD
	CouponId bigint NULL
GO
ALTER TABLE dbo.Notifications SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





GO

/****** Object:  View [dbo].[vw_Notifications]    Script Date: 11/23/2016 12:32:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- select * from [vw_Notifications]
ALTER VIEW [dbo].[vw_Notifications]
AS
SELECT        n.ID, Type, n.UserID, IsRead, GeneratedOn, GeneratedBy, n.SurveyQuestionShareId, n.PhoneNumber,
				(case when n.[type] = 1 then u.FullName +  ' wants your opinion' else 'New Deal around you is available' end) NotificationDetails,
				(case when n.[type] = 1 then q.SurveyTitle else '' end) PollTitle, 
				(case when n.[type] = 1 then q.SSQID else 0 end) SSQID,
				(case when n.[type] = 2 then n.CouponId else 0 end) CouponId,
				(case when n.[type] = 2 then c.CouponTitle else '' end) DealTitle

				
FROM            dbo.Notifications n
			left outer join SurveySharingGroupShares s on n.SurveyQuestionShareId = s.SurveyQuestionShareId
			left outer join SharedSurveyQuestion q on q.SSQID = s.SSQID
			left outer join AspNetUsers u on q.UserId = u.Id
			left outer join Coupon c on n.CouponId = c.CouponId



GO




ALTER TABLE SurveyQuestion
ADD IsUseFilter bit null



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
	DeleteConfirmationToken nvarchar(150) NULL
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
ALTER TABLE dbo.AspNetUsers ADD
	LastKnownLocationLat nvarchar(50) NULL,
	LastKnownLocationLong nvarchar(50) NULL,
	LastKnownLocation geography NULL
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
ALTER TABLE dbo.CouponRatingReview
	DROP CONSTRAINT FK_CouponRatingReview_Coupon
GO
ALTER TABLE dbo.Coupon SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CouponRatingReview
	(
	CouponReviewId bigint NOT NULL IDENTITY (1, 1),
	CouponId bigint NULL,
	StarRating float(53) NULL,
	Review nvarchar(800) NULL,
	RatingDateTime datetime NULL,
	UserId nvarchar(128) NULL,
	CompanyId int NULL,
	Status int NULL,
	ReviewImage1 nvarchar(250) NULL,
	ReviewImage2 nvarchar(250) NULL,
	Reviewimage3 nvarchar(250) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CouponRatingReview SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CouponRatingReview ON
GO
IF EXISTS(SELECT * FROM dbo.CouponRatingReview)
	 EXEC('INSERT INTO dbo.Tmp_CouponRatingReview (CouponReviewId, CouponId, StarRating, Review, RatingDateTime, UserId, CompanyId, Status, ReviewImage1, ReviewImage2, Reviewimage3)
		SELECT CouponReviewId, CouponId, CONVERT(float(53), StarRating), Review, RatingDateTime, UserId, CompanyId, Status, ReviewImage1, ReviewImage2, Reviewimage3 FROM dbo.CouponRatingReview WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CouponRatingReview OFF
GO
DROP TABLE dbo.CouponRatingReview
GO
EXECUTE sp_rename N'dbo.Tmp_CouponRatingReview', N'CouponRatingReview', 'OBJECT' 
GO
ALTER TABLE dbo.CouponRatingReview ADD CONSTRAINT
	PK_CouponRatingReview PRIMARY KEY CLUSTERED 
	(
	CouponReviewId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CouponRatingReview ADD CONSTRAINT
	FK_CouponRatingReview_Coupon FOREIGN KEY
	(
	CouponId
	) REFERENCES dbo.Coupon
	(
	CouponId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT



ALTER TABLE dbo.Coupon
ADD  IsPerSaving3days Bit  NULL,
     IsPerSaving2days Bit Null,
  IsPerSavingLastday Bit Null,
  IsDollarSaving3days Bit Null,
  IsDollarSaving2days Bit Null,
  IsDollarSavingLastday Bit Null




  ALTER TABLE Coupon
ADD isSaveBtnLable int Null







GO

/****** Object:  Table [dbo].[UserGameResponse]    Script Date: 12/16/2016 5:46:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserGameResponse](
	[UserGameResponseId] [bigint] NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[GameId] [bigint] NULL,
	[ResponseDateTime] [datetime] NULL,
	[PlayTime] [float] NULL,
	[Score] [int] NULL,
	[Accuracy] [float] NULL,
	[AdCampaignResponseID] [int] NULL,
 CONSTRAINT [PK_UserGameResponse] PRIMARY KEY CLUSTERED 
(
	[UserGameResponseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserGameResponse]  WITH CHECK ADD  CONSTRAINT [FK_UserGameResponse_AdCampaignResponse] FOREIGN KEY([AdCampaignResponseID])
REFERENCES [dbo].[AdCampaignResponse] ([ResponseID])
GO

ALTER TABLE [dbo].[UserGameResponse] CHECK CONSTRAINT [FK_UserGameResponse_AdCampaignResponse]
GO

ALTER TABLE [dbo].[UserGameResponse]  WITH CHECK ADD  CONSTRAINT [FK_UserGameResponse_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[UserGameResponse] CHECK CONSTRAINT [FK_UserGameResponse_AspNetUsers]
GO

ALTER TABLE [dbo].[UserGameResponse]  WITH CHECK ADD  CONSTRAINT [FK_UserGameResponse_Game] FOREIGN KEY([GameId])
REFERENCES [dbo].[Game] ([GameId])
GO

ALTER TABLE [dbo].[UserGameResponse] CHECK CONSTRAINT [FK_UserGameResponse_Game]
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
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AdCampaignResponse SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Game ADD
	GameInstructions nvarchar(MAX) NULL
GO
ALTER TABLE dbo.Game SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.UserGameResponse.ResponseId', N'Tmp_UserGameResponseId', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.UserGameResponse.Tmp_UserGameResponseId', N'UserGameResponseId', 'COLUMN' 
GO
ALTER TABLE dbo.UserGameResponse ADD
	AdCampaignResponseID int NULL
GO
ALTER TABLE dbo.UserGameResponse ADD CONSTRAINT
	FK_UserGameResponse_Game FOREIGN KEY
	(
	GameId
	) REFERENCES dbo.Game
	(
	GameId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UserGameResponse ADD CONSTRAINT
	FK_UserGameResponse_AspNetUsers FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UserGameResponse ADD CONSTRAINT
	FK_UserGameResponse_AdCampaignResponse FOREIGN KEY
	(
	AdCampaignResponseID
	) REFERENCES dbo.AdCampaignResponse
	(
	ResponseID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UserGameResponse SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





/****** Object:  Table [dbo].[GameExerciseCategoryList]    Script Date: 12/16/2016 6:18:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GameExerciseCategoryList](
	[ExerciseCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseCategoryName] [nvarchar](150) NULL,
 CONSTRAINT [PK_GameExerciseCategory] PRIMARY KEY CLUSTERED 
(
	[ExerciseCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO




/****** Object:  Table [dbo].[GameExerciseCategories]    Script Date: 12/16/2016 6:19:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GameExerciseCategories](
	[GameId] [bigint] NOT NULL,
	[ExerciseCategoryId] [int] NOT NULL,
	[CategoryContribution] [int] NULL,
 CONSTRAINT [PK_GameExerciseCategories] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC,
	[ExerciseCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GameExerciseCategories]  WITH CHECK ADD  CONSTRAINT [FK_GameExerciseCategories_Game] FOREIGN KEY([GameId])
REFERENCES [dbo].[Game] ([GameId])
GO

ALTER TABLE [dbo].[GameExerciseCategories] CHECK CONSTRAINT [FK_GameExerciseCategories_Game]
GO

ALTER TABLE [dbo].[GameExerciseCategories]  WITH CHECK ADD  CONSTRAINT [FK_GameExerciseCategories_GameExerciseCategory] FOREIGN KEY([ExerciseCategoryId])
REFERENCES [dbo].[GameExerciseCategoryList] ([ExerciseCategoryId])
GO

ALTER TABLE [dbo].[GameExerciseCategories] CHECK CONSTRAINT [FK_GameExerciseCategories_GameExerciseCategory]
GO











ALTER TABLE dbo.Coupon
ADD DealFirstDiscountType int Null,
DealEndingDiscountType int Null


ALTER TABLE dbo.CouponPriceOption
ADD 
VoucherCode2 Varchar(100) Null,
VoucherCode3 Varchar(100) Null,
VoucherCode4 Varchar(100) Null



USE [SMDv2]
GO

/****** Object:  Table [dbo].[CompanyComments]    Script Date: 12/27/2016 4:33:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CompanyComments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[CommentsDateTime] [datetime] NULL,
	[Comments] [nvarchar](max) NULL,
	[CommentingUserId] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

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
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CouponCategory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.UserCouponCategoryClicks
	(
	UserCouponCategoryClick bigint NOT NULL IDENTITY (1, 1),
	CouponCategoryId int NULL,
	UserId nvarchar(128) NULL,
	ClickDateTime datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.UserCouponCategoryClicks ADD CONSTRAINT
	PK_UserCouponCategoryClicks PRIMARY KEY CLUSTERED 
	(
	UserCouponCategoryClick
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.UserCouponCategoryClicks ADD CONSTRAINT
	FK_UserCouponCategoryClicks_CouponCategory FOREIGN KEY
	(
	CouponCategoryId
	) REFERENCES dbo.CouponCategory
	(
	CategoryId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UserCouponCategoryClicks ADD CONSTRAINT
	FK_UserCouponCategoryClicks_AspNetUsers FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UserCouponCategoryClicks SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
