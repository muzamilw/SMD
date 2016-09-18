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


/*----------------*/
USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[GetAdminDashBoardInsights]    Script Date: 9/17/2016 12:17:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetAdminDashBoardInsights]

as
Begin
select rectype, pMonth, us, uk, ca, au, ae
from 
(
  select ordr, stats, countrycode, rectype, pMonth
  from ( 
		select 1 as ordr, count(u.id) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, u.LastLoginTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Active App Users' rectype
		from [SMDv2].[dbo].[AspNetUsers] u
		inner join Company co on u.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where u.LastLoginTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, u.LastLoginTime), 0) ,  c.CountryCode

		union

		select 2 as ordr,  count(u.id) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, u.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'New App Users' rectype
		from [SMDv2].[dbo].[AspNetUsers] u
		inner join Company co on u.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where u.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, u.CreatedDateTime), 0) ,  c.CountryCode
		union

		select 3 as ordr,   count(ac.CampaignID) stats, c.CountryCode , 'prev' pMonth,'Active Campaigns' rectype
		from AdCampaign ac
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where ac.EndDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.StartDateTime < =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1))
		group by c.CountryCode
		union
		select 3 as ordr,  count(ac.CampaignID) stats, c.CountryCode , 'current' pMonth,'Active Campaigns' rectype
		from AdCampaign ac
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where ac.EndDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1)) AND ac.StartDateTime < =  GETDATE()
		group by c.CountryCode

		union 
		select 4 as ordr,  count(ac.CampaignID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, ac.ApprovalDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'New Campaigns' rectype
		from AdCampaign ac
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where ac.ApprovalDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, ac.ApprovalDateTime), 0) ,  c.CountryCode
		union 
				 
		select 5 as ordr,  count(coup.CouponId) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, coup.ApprovalDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Coupons approved' rectype
		from Coupon coup
		inner join Company co on coup.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where coup.ApprovalDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, coup.ApprovalDateTime), 0) ,  c.CountryCode

		union 
		select 6 as ordr,  count(upc.CouponPurchaseId) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.PurchaseDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Coupons Purchased' rectype
		from UserPurchasedCoupon upc
		inner join Coupon cpn on cpn.CouponId = upc.CouponId
		inner join Company co on cpn.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where upc.PurchaseDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.PurchaseDateTime), 0) ,  c.CountryCode

		union 

		select 7 as ordr,  count(upc.RedemptionDateTime) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.RedemptionDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Coupons Redeemed' rectype
		from UserPurchasedCoupon upc
		inner join Coupon cpn on cpn.CouponId = upc.CouponId
		inner join Company co on cpn.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where upc.RedemptionDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.RedemptionDateTime), 0) ,  c.CountryCode

		union
		select 8 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Video Ads delivered' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on acr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode
		
		union 

		select 9 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Games Ads delivered' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on acr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 4
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode

		union
		select 10 as ordr,   count(sqr.SQResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Survey Question Answered' rectype
		from SurveyQuestionResponse sqr
		inner join Company co on sqr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where sqr.ResoponseDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0) ,  c.CountryCode

		union

		select 11 as ordr,   count(pqua.PQUAnswerID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Profile Question Answered' rectype
		from ProfileQuestionUserAnswer pqua
		inner join Company co on pqua.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where pqua.AnswerDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0) ,  c.CountryCode

		union 
		select 12 as ordr,  sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Video Ads Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join AdCampaign ac on ac.CampaignID = t.AdCampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 1 AND a.AccountType = 1 AND ac.Type = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode

		union 
		select 13 as ordr,  sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Game Ads Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join AdCampaign ac on ac.CampaignID = t.AdCampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 1 AND  a.AccountType = 1 AND ac.Type = 4
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode


		union 
		select 14 as ordr,   sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Survay Cards Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join SurveyQuestion sq on sq.SQID = t.SQID
		-- inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on sq.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 2 AND  a.AccountType = 1 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode

		union

		select 15 as ordr,   sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Profile Questions Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join ProfileQuestion pq on pq.PQID = t.PQID
		-- inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on pq.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 7 AND  a.AccountType = 1 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode

		union  
		select 16 as ordr,  sum(t.DebitAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Payout via PayPal' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join Company co on a.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 10 AND  a.AccountType = 4 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode

		union  
		select 17 as ordr, CASE WHEN sum(t.CreditAmount) > 0 THEN sum(t.CreditAmount) ELSE 0 END stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Income from Stripe' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join Company co on a.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND  a.AccountType = 1 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode


		) as rec
				
)  src

pivot
(
  max(stats)
    for countrycode in ([us], [uk],[ca], [au], [ae]) 
) piv


order by ordr
END


