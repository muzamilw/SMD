
GO
/****** Object:  StoredProcedure [dbo].[GetCouponByID]    Script Date: 9/29/2016 2:57:11 PM ******/
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

--    getcouponbyid 23, '','','d70b04d3-e76c-4ca2-95fe-9746ceff1a88',
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
      ,c.[Status]
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
      ,c.[CompanyId]
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
	 
	 as SwapCost,
	 c.ShowBuyitBtn,
	c.BuyitLandingPageUrl,
	c.BuyitBtnLabel,
	comp.AboutUsDescription



	from coupon as c 
	
	inner join Company comp on c.CompanyId = comp.CompanyId
	inner join Country countr on comp.BillingCountryId = countr.CountryID
	inner join Currency curr on countr.CurrencyId = curr.CurrencyID 
	where c.CouponId = @CouponId
END
