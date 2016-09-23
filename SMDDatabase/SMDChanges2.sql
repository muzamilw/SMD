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
