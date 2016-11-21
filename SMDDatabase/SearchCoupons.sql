USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[SearchCoupons]    Script Date: 11/21/2016 12:49:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SearchCoupons]
--   EXEC [dbo].[SearchCoupons] 		@categoryId =0,		@type = 1,		@keywords = N'',		@distance = 1000000000,		@Lat = N'31.483177',		@Lon = N'74.288167',		@UserId = N'88d8d269-6d4f-4310-9efdd-aed888ef7ac5lk',		@FromRow = 0,		@ToRow = 100

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


set @distance = 1000000


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
				isnull(cpopt.Price,0) Price, isnull(cpopt.Savings,0) Savings, isnull(SwapCost,0) SwapCost, vchr.CompanyId, CouponActiveMonth,CouponActiveYear
				,DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1)))) eod,
				vchr.ApprovalDateTime strt,

				case when vchr.CouponListingMode = 3 then 0 
				else
				@source.STDistance(geography::Point(vchr.LocationLAT, vchr.LocationLON, 4326))/1000 
       
				end  as distance,
				comp.CompanyName,
				vchr.LocationTitle,
				vchr.LocationCity,
				cpoptc.cnt DealsCount,
				curr.CurrencyCode,
				curr.CurrencySymbol
	

				from Coupon vchr
				inner join CouponCategories cc on cc.CouponId = vchr.CouponId and vchr.LocationLAT is not null and vchr.LocationLON is not null
				inner join Company comp on vchr.CompanyId = comp.CompanyId
				inner join Country cntry on comp.BillingCountryId = cntry.CountryID
				inner join Currency curr on cntry.CurrencyID = curr.CurrencyID

				OUTER APPLY (SELECT TOp 1 Price, Savings
								FROM   CouponPriceOption cpo
								WHERE  cpo.CouponId = vchr.CouponId
								ORDER  BY cpo.Price) cpopt
				OUTER APPLY (SELECT count(*) cnt
								FROM   CouponPriceOption cpo
								WHERE  cpo.CouponId = vchr.CouponId
								) cpoptc

	
				where (
		

		
					--vchr.CouponExpirydate > GETDATE()
					 vchr.status = 3 and (comp.IsSpecialAccount is null or comp.IsSpecialAccount = 0)

					and (cc.CategoryId = @categoryId or  @categoryId = 0)
		
					and -- keyword search
					(
						@keywords = '' or (vchr.SearchKeywords like '%'+@keywords+'%' or vchr.CouponTitle like '%'+@keywords+'%' or LocationTitle like '%'+@keywords+'%' or LocationLine1 like '%'+@keywords+'%')
					)
		
					and 
					(--unlimited listing  -- valid for 6 months
					( CouponListingMode = 2 and @currentDate between vchr.ApprovalDateTime and dateadd(day,14,vchr.ApprovalDateTime)--dateadd(month,2,DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1)))))
						--and  vchr.CouponQtyPerUser > (select count(*) from [dbo].[UserPurchasedCoupon] upc where upc.couponID = vchr.couponid and upc.userId = @UserId) 
					)
						--
					or
					--free more - valid for 1 month now.
					(
						CouponListingMode = 1 and @currentDate  between vchr.ApprovalDateTime and dateadd(day,14,vchr.ApprovalDateTime))
						--and vchr.CouponIssuedCount < 10
					)
					or 
					--nationwide mode valid for 3 months but higher priority
					(
					CouponListingMode = 3 and @currentDate between DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1) and dateadd(month,2,DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1)))))
						and  vchr.CouponQtyPerUser > (select count(*) from [dbo].[UserPurchasedCoupon] upc where upc.couponID = vchr.couponid and upc.userId = @UserId) 
					)

		
					)
					group by vchr.CouponId, CouponTitle,vchr.CouponImage1,LogoUrl,cpopt.Price, cpopt.Savings,SwapCost,vchr.CompanyId,CouponActiveMonth,CouponActiveYear,vchr.LocationLAT, vchr.LocationLON,CouponListingMode, comp.companyname, vchr.LocationTitle, vchr.LocationCity,vchr.ApprovalDateTime, cpoptc.cnt,curr.CurrencyCode,curr.CurrencySymbol


					

					)as items
		where distance < @distance

		union

	
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
					isnull(cpopt.Price,0) Price, isnull(cpopt.Savings,0) Savings, isnull(SwapCost,0) SwapCost, vchr.CompanyId, CouponActiveMonth,CouponActiveYear
					,DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,day(EOMONTH ( DATEFROMPARTS(vchr.CouponActiveYear, vchr.CouponActiveMonth,1)))) eod,
					vchr.ApprovalDateTime strt,
					@distance  as distance,
					comp.CompanyName,
					vchr.LocationTitle,
					vchr.LocationCity,
					cpoptc.cnt DealsCount,
					curr.CurrencyCode,
					curr.CurrencySymbol,
					0 as TotalItems

					from Coupon vchr
					
					inner join Company comp on vchr.CompanyId = comp.CompanyId
					inner join Country cntry on comp.BillingCountryId = cntry.CountryID
					inner join Currency curr on cntry.CurrencyID = curr.CurrencyID

					OUTER APPLY (SELECT TOp 1 Price, Savings
									FROM   CouponPriceOption cpo
									WHERE  cpo.CouponId = vchr.CouponId
									ORDER  BY cpo.Price) cpopt
					OUTER APPLY (SELECT count(*) cnt
									FROM   CouponPriceOption cpo
									WHERE  cpo.CouponId = vchr.CouponId
									) cpoptc

					where comp.IsSpecialAccount = 1

		





	order by distance
	OFFSET @FromRow ROWS
	FETCH NEXT @TORow ROWS ONLY
		
END


