
GO
/****** Object:  StoredProcedure [dbo].[SearchCoupons]    Script Date: 9/23/2016 6:38:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SearchCoupons]
--   EXEC [dbo].[SearchCoupons] 		@categoryId =3,		@type = 1,		@keywords = N'',		@distance = 1000000,		@Lat = N'31.483177',		@Lon = N'74.288167',		@UserId = N'88d8d269-6d4f-4310-9efdd-aed888ef7ac5',		@FromRow = 0,		@ToRow = 100

	-- Add the parameters for the stored procedure here
	@categoryId INT = 1 ,
	@type as int = 0,
	@keywords as nvarchar(500),
	@distance as int = 0,
	@Lat as nvarchar(50),
	@Lon as nvarchar(50),
	@UserId as nvarchar(128) = 0 ,
	@FromRow int = 0,
	@ToRow int = 0

AS
BEGIN




declare @currentDate datetime
set @currentDate =  getdate()

if @lat = '' 
	set @lat = '31.482932'


if @lon = '' 
	set @lon = '74.288557'


DECLARE @source geography = geography::Point(@lat, @lon, 4326)

	select *, COUNT(*) OVER() AS TotalItems
	from (
	
	select vchr.CouponId as CouponId, CouponTitle,
	
	
	CouponImage1,
	(select 
	CASE
		WHEN vchr.LogoUrl is null or vchr.LogoUrl = ''
		THEN 'http://manage.cash4ads.com/' + c.Logo
		WHEN vchr.LogoUrl is not null
		THEN 'http://manage.cash4ads.com/' + vchr.LogoUrl
	END as AdvertisersLogoPath from company c
	 where c.CompanyId = vchr.CompanyId) as LogoUrl,
	Price, Savings,SwapCost, CompanyId, CouponActiveMonth,CouponActiveYear
	,DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1)))) eod,
	DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1) strt,

	case when vchr.CouponListingMode = 3 then 0 
	else
	@source.STDistance(geography::Point(vchr.LocationLAT, vchr.LocationLON, 4326))/1000 
       
	end  as distance
	from Coupon vchr
	inner join CouponCategories cc on cc.CouponId = vchr.CouponId and cc.CategoryId = @categoryId and vchr.LocationLAT is not null and vchr.LocationLON is not null
	--left outer join [dbo].[UserPurchasedCoupon] upc on upc.couponID = vchr.couponid
	where (
		
		--and
		--(adcampaign.EndDateTime >= @currentDate and @currentDate >= adcampaign.StartDateTime)
		
		vchr.CouponExpirydate > GETDATE()
		and vchr.status = 3

		
		and 
		(--unlimited listing  -- valid for 3 months
		( CouponListingMode = 2 and @currentDate between DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1) and dateadd(month,2,DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1)))))
			and  vchr.CouponQtyPerUser > (select count(*) from [dbo].[UserPurchasedCoupon] upc where upc.couponID = vchr.couponid and upc.userId = @UserId) 
		)
			--
		or
		--free more - valid for 1 month now.
		(
			CouponListingMode = 1 and @currentDate  between DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1) and DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,30))
			and vchr.CouponIssuedCount < 10
		)
		or 
		--nationwide mode valid for 3 months but higher priority
		(
		CouponListingMode = 3 and @currentDate between DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1) and dateadd(month,2,DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1)))))
			and  vchr.CouponQtyPerUser > (select count(*) from [dbo].[UserPurchasedCoupon] upc where upc.couponID = vchr.couponid and upc.userId = @UserId) 
		)
		and -- keyword search
		(
			@keywords <> '' and (vchr.SearchKeywords like '%'+@keywords+'%' or vchr.CouponTitle like '%'+@keywords+'%' or LocationTitle like '%'+@keywords+'%' or LocationLine1 like '%'+@keywords+'%')
		)
		
		

		
		)
		group by vchr.CouponId, CouponTitle,vchr.CouponImage1,LogoUrl,Price, Savings,SwapCost,CompanyId,CouponActiveMonth,CouponActiveYear,vchr.LocationLAT, vchr.LocationLON,CouponListingMode
		)as items
		where distance < @distance
	order by distance
	OFFSET @FromRow ROWS
	FETCH NEXT @TORow ROWS ONLY
		
END


