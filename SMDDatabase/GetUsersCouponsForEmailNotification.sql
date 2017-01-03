
GO
/****** Object:  StoredProcedure [dbo].[GetUsersCouponsForEmailNotification]    Script Date: 12/26/2016 3:17:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[GetUsersCouponsForEmailNotification]
  @mode AS INT
--   [GetUsersCouponsForEmailNotification] 1
AS

BEGIN

if (@mode = 1)  --deal announced today
begin

		select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,(case when discountType = 1 then cpopt.price * c.discount/100 else   cpopt.price -  c.discount end ) as savings, c.DaysLeft, (case when discountType = 1 then cpopt.price * c.discount/100 else   cpopt.price -  c.discount end ) as SavingsNew, c.LocationCity,c.CurrencySymbol from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol,


					(case when c.DealFirstDiscountType = 0 then 10
					when c.DealFirstDiscountType = 1 then 20
					when c.DealFirstDiscountType = 2 then 25
					when c.DealFirstDiscountType = 3 then 30
					when c.DealFirstDiscountType = 4 then 40
					when c.DealFirstDiscountType = 5 then 50
					when c.DealFirstDiscountType = 6 then 60
					when c.DealFirstDiscountType = 7 then 50
					when c.DealFirstDiscountType = 8 then 1
					when c.DealFirstDiscountType = 9 then 3
					when c.DealFirstDiscountType = 10 then 5
					when c.DealFirstDiscountType = 11 then 10
					when c.DealFirstDiscountType = 12 then 15
					when c.DealFirstDiscountType = 13 then 20
					when c.DealFirstDiscountType = 14 then 25
					when c.DealFirstDiscountType = 15 then 30
					when c.DealFirstDiscountType = 16 then 40
					when c.DealFirstDiscountType = 17 then 50
					



					end ) discount,
					(case when c.DealFirstDiscountType = 0 then 1
					when c.DealFirstDiscountType = 1 then 1
					when c.DealFirstDiscountType = 2 then 1
					when c.DealFirstDiscountType = 3 then 1
					when c.DealFirstDiscountType = 4 then 1
					when c.DealFirstDiscountType = 5 then 1
					when c.DealFirstDiscountType = 6 then 1
					when c.DealFirstDiscountType = 7 then 1
					when c.DealFirstDiscountType = 8 then 2
					when c.DealFirstDiscountType = 9 then 2
					when c.DealFirstDiscountType = 10 then 2
					when c.DealFirstDiscountType = 11 then 2
					when c.DealFirstDiscountType = 12 then 2
					when c.DealFirstDiscountType = 13 then 2
					when c.DealFirstDiscountType = 14 then 2
					when c.DealFirstDiscountType = 15 then 2
					when c.DealFirstDiscountType = 16 then 2
					when c.DealFirstDiscountType = 17 then 2

					end ) discountType

					from coupon c
					inner join company comp on comp.companyid = c.companyid  and comp.IsSpecialAccount is null and  datediff(hh,ApprovalDateTime,getdate()) between 0 and 24
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null
		
					) c

					OUTER APPLY (SELECT TOp 1 Price, c.discount
													FROM   CouponPriceOption cpo
													WHERE  cpo.CouponId = c.CouponId
													ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null and status=1
			 and 
			 (geography::Point(u.LastKnownLocationLat, u.LastKnownLocationLong, 4326)).STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 < 200000
			 order by UserId





			 
 	
	end	
else	if (@mode = 2)	--  additioanl 20% on 3rd last day or 20% on all last 3 days
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,(case when discountType = 1 then (cpopt.price * c.discount + 20) /100 else   cpopt.price -  c.discount - 10 end ) as savings, c.DaysLeft,(case when discountType = 1 then (cpopt.price * c.discount + 20) /100 else   cpopt.price -  c.discount - 10 end ) SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol,
					

					(case when c.DealFirstDiscountType = 0 then 10
					when c.DealFirstDiscountType = 1 then 20
					when c.DealFirstDiscountType = 2 then 25
					when c.DealFirstDiscountType = 3 then 30
					when c.DealFirstDiscountType = 4 then 40
					when c.DealFirstDiscountType = 5 then 50
					when c.DealFirstDiscountType = 6 then 60
					when c.DealFirstDiscountType = 7 then 50
					when c.DealFirstDiscountType = 8 then 1
					when c.DealFirstDiscountType = 9 then 3
					when c.DealFirstDiscountType = 10 then 5
					when c.DealFirstDiscountType = 11 then 10
					when c.DealFirstDiscountType = 12 then 15
					when c.DealFirstDiscountType = 13 then 20
					when c.DealFirstDiscountType = 14 then 25
					when c.DealFirstDiscountType = 15 then 30
					when c.DealFirstDiscountType = 16 then 40
					when c.DealFirstDiscountType = 17 then 50
					
					end ) discount,
					(case when c.DealFirstDiscountType = 0 then 1
					when c.DealFirstDiscountType = 1 then 1
					when c.DealFirstDiscountType = 2 then 1
					when c.DealFirstDiscountType = 3 then 1
					when c.DealFirstDiscountType = 4 then 1
					when c.DealFirstDiscountType = 5 then 1
					when c.DealFirstDiscountType = 6 then 1
					when c.DealFirstDiscountType = 7 then 1
					when c.DealFirstDiscountType = 8 then 2
					when c.DealFirstDiscountType = 9 then 2
					when c.DealFirstDiscountType = 10 then 2
					when c.DealFirstDiscountType = 11 then 2
					when c.DealFirstDiscountType = 12 then 2
					when c.DealFirstDiscountType = 13 then 2
					when c.DealFirstDiscountType = 14 then 2
					when c.DealFirstDiscountType = 15 then 2
					when c.DealFirstDiscountType = 16 then 2
					when c.DealFirstDiscountType = 17 then 2

					end ) discountType
					from coupon c
					inner join company comp on comp.companyid = c.companyid and  comp.IsSpecialAccount is null 
					and (
					
						(c.DealEndingDiscountType = 1 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) between 0 and 3)
						or
						(c.DealEndingDiscountType = 2 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) = 3 )
						or
						(c.DealEndingDiscountType = 3 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) = 3 )
					)
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c

			OUTER APPLY (SELECT TOp 1 Price
											FROM   CouponPriceOption cpo
											WHERE  cpo.CouponId = c.CouponId
											ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null and u.status=1
			 and 
			 (geography::Point(u.LastKnownLocationLat, u.LastKnownLocationLong, 4326)).STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 < 200000
			 order by UserId
 	
 	
 	
end	
else	if (@mode = 3)	--additioanl 25% on 2nd last day or 25% on all last 2  days
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,(case when discountType = 1 then (cpopt.price * c.discount + 25) /100 else   cpopt.price -  c.discount - 10 end ) as savings, c.DaysLeft,(case when discountType = 1 then (cpopt.price * c.discount + 20) /100 else   cpopt.price -  c.discount - 10 end ) SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol,
					

					(case when c.DealFirstDiscountType = 0 then 10
					when c.DealFirstDiscountType = 1 then 20
					when c.DealFirstDiscountType = 2 then 25
					when c.DealFirstDiscountType = 3 then 30
					when c.DealFirstDiscountType = 4 then 40
					when c.DealFirstDiscountType = 5 then 50
					when c.DealFirstDiscountType = 6 then 60
					when c.DealFirstDiscountType = 7 then 50
					when c.DealFirstDiscountType = 8 then 1
					when c.DealFirstDiscountType = 9 then 3
					when c.DealFirstDiscountType = 10 then 5
					when c.DealFirstDiscountType = 11 then 10
					when c.DealFirstDiscountType = 12 then 15
					when c.DealFirstDiscountType = 13 then 20
					when c.DealFirstDiscountType = 14 then 25
					when c.DealFirstDiscountType = 15 then 30
					when c.DealFirstDiscountType = 16 then 40
					when c.DealFirstDiscountType = 17 then 50
					
					end ) discount,
					(case when c.DealFirstDiscountType = 0 then 1
					when c.DealFirstDiscountType = 1 then 1
					when c.DealFirstDiscountType = 2 then 1
					when c.DealFirstDiscountType = 3 then 1
					when c.DealFirstDiscountType = 4 then 1
					when c.DealFirstDiscountType = 5 then 1
					when c.DealFirstDiscountType = 6 then 1
					when c.DealFirstDiscountType = 7 then 1
					when c.DealFirstDiscountType = 8 then 2
					when c.DealFirstDiscountType = 9 then 2
					when c.DealFirstDiscountType = 10 then 2
					when c.DealFirstDiscountType = 11 then 2
					when c.DealFirstDiscountType = 12 then 2
					when c.DealFirstDiscountType = 13 then 2
					when c.DealFirstDiscountType = 14 then 2
					when c.DealFirstDiscountType = 15 then 2
					when c.DealFirstDiscountType = 16 then 2
					when c.DealFirstDiscountType = 17 then 2

					end ) discountType
					from coupon c
					inner join company comp on comp.companyid = c.companyid and  comp.IsSpecialAccount is null 
					and (
						
						(c.DealEndingDiscountType = 2 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) between 0 and 2 )
						or
						(c.DealEndingDiscountType = 3 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) = 2 )
					)
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c

			OUTER APPLY (SELECT TOp 1 Price
											FROM   CouponPriceOption cpo
											WHERE  cpo.CouponId = c.CouponId
											ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null and u.status=1
			 and 
			 (geography::Point(u.LastKnownLocationLat, u.LastKnownLocationLong, 4326)).STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 < 200000
			 order by UserId
 	
end	
else	if (@mode = 4)	--last day  additioanl 30%
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,(case when discountType = 1 then (cpopt.price * c.discount + 25) /100 else   cpopt.price -  c.discount - 10 end ) as savings, c.DaysLeft,(case when discountType = 1 then (cpopt.price * c.discount + 20) /100 else   cpopt.price -  c.discount - 10 end ) SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol,
					

					(case when c.DealFirstDiscountType = 0 then 10
					when c.DealFirstDiscountType = 1 then 20
					when c.DealFirstDiscountType = 2 then 25
					when c.DealFirstDiscountType = 3 then 30
					when c.DealFirstDiscountType = 4 then 40
					when c.DealFirstDiscountType = 5 then 50
					when c.DealFirstDiscountType = 6 then 60
					when c.DealFirstDiscountType = 7 then 50
					when c.DealFirstDiscountType = 8 then 1
					when c.DealFirstDiscountType = 9 then 3
					when c.DealFirstDiscountType = 10 then 5
					when c.DealFirstDiscountType = 11 then 10
					when c.DealFirstDiscountType = 12 then 15
					when c.DealFirstDiscountType = 13 then 20
					when c.DealFirstDiscountType = 14 then 25
					when c.DealFirstDiscountType = 15 then 30
					when c.DealFirstDiscountType = 16 then 40
					when c.DealFirstDiscountType = 17 then 50
					
					end ) discount,
					(case when c.DealFirstDiscountType = 0 then 1
					when c.DealFirstDiscountType = 1 then 1
					when c.DealFirstDiscountType = 2 then 1
					when c.DealFirstDiscountType = 3 then 1
					when c.DealFirstDiscountType = 4 then 1
					when c.DealFirstDiscountType = 5 then 1
					when c.DealFirstDiscountType = 6 then 1
					when c.DealFirstDiscountType = 7 then 1
					when c.DealFirstDiscountType = 8 then 2
					when c.DealFirstDiscountType = 9 then 2
					when c.DealFirstDiscountType = 10 then 2
					when c.DealFirstDiscountType = 11 then 2
					when c.DealFirstDiscountType = 12 then 2
					when c.DealFirstDiscountType = 13 then 2
					when c.DealFirstDiscountType = 14 then 2
					when c.DealFirstDiscountType = 15 then 2
					when c.DealFirstDiscountType = 16 then 2
					when c.DealFirstDiscountType = 17 then 2

					end ) discountType
					from coupon c
					inner join company comp on comp.companyid = c.companyid and  comp.IsSpecialAccount is null 
					and (
						
						(c.DealEndingDiscountType = 3 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) between 0 and 1 )
					)
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c

			OUTER APPLY (SELECT TOp 1 Price
											FROM   CouponPriceOption cpo
											WHERE  cpo.CouponId = c.CouponId
											ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null and u.status=1
			 and 
			 (geography::Point(u.LastKnownLocationLat, u.LastKnownLocationLong, 4326)).STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 < 200000
			 order by UserId
 	
end	


else	if (@mode = 5)	--last 3 day  additioanl 10 dollar
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,(case when discountType = 1 then (cpopt.price * c.discount + 20) /100 else   cpopt.price - c.discount - 10 end ) as savings, c.DaysLeft,(case when discountType = 1 then (cpopt.price * c.discount + 20) /100 else   cpopt.price -  c.discount - 10 end ) SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol,
					

					(case when c.DealFirstDiscountType = 0 then 10
					when c.DealFirstDiscountType = 1 then 20
					when c.DealFirstDiscountType = 2 then 25
					when c.DealFirstDiscountType = 3 then 30
					when c.DealFirstDiscountType = 4 then 40
					when c.DealFirstDiscountType = 5 then 50
					when c.DealFirstDiscountType = 6 then 60
					when c.DealFirstDiscountType = 7 then 50
					when c.DealFirstDiscountType = 8 then 1
					when c.DealFirstDiscountType = 9 then 3
					when c.DealFirstDiscountType = 10 then 5
					when c.DealFirstDiscountType = 11 then 10
					when c.DealFirstDiscountType = 12 then 15
					when c.DealFirstDiscountType = 13 then 20
					when c.DealFirstDiscountType = 14 then 25
					when c.DealFirstDiscountType = 15 then 30
					when c.DealFirstDiscountType = 16 then 40
					when c.DealFirstDiscountType = 17 then 50
					
					end ) discount,
					(case when c.DealFirstDiscountType = 0 then 1
					when c.DealFirstDiscountType = 1 then 1
					when c.DealFirstDiscountType = 2 then 1
					when c.DealFirstDiscountType = 3 then 1
					when c.DealFirstDiscountType = 4 then 1
					when c.DealFirstDiscountType = 5 then 1
					when c.DealFirstDiscountType = 6 then 1
					when c.DealFirstDiscountType = 7 then 1
					when c.DealFirstDiscountType = 8 then 2
					when c.DealFirstDiscountType = 9 then 2
					when c.DealFirstDiscountType = 10 then 2
					when c.DealFirstDiscountType = 11 then 2
					when c.DealFirstDiscountType = 12 then 2
					when c.DealFirstDiscountType = 13 then 2
					when c.DealFirstDiscountType = 14 then 2
					when c.DealFirstDiscountType = 15 then 2
					when c.DealFirstDiscountType = 16 then 2
					when c.DealFirstDiscountType = 17 then 2

					end ) discountType
					from coupon c
					inner join company comp on comp.companyid = c.companyid and  comp.IsSpecialAccount is null 
					and (
					
						(c.DealEndingDiscountType = 4 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) between 0 and 3 )
						or
						(c.DealEndingDiscountType = 5 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) = 3 )
						or
						(c.DealEndingDiscountType = 6 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) = 3 )
					)
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c

			OUTER APPLY (SELECT TOp 1 Price
											FROM   CouponPriceOption cpo
											WHERE  cpo.CouponId = c.CouponId
											ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null and u.status=1
			 and 
			 (geography::Point(u.LastKnownLocationLat, u.LastKnownLocationLong, 4326)).STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 < 200000
			 order by UserId
 	
end	

else	if (@mode = 6)	--2nd last day additioanl 20 dollar or last 2 days
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,(case when discountType = 1 then (cpopt.price * c.discount + 25) /100 else   cpopt.price -  c.discount - 20 end ) as savings, c.DaysLeft,(case when discountType = 1 then (cpopt.price * c.discount + 20) /100 else   cpopt.price -  c.discount - 20 end ) SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol,
					

					(case when c.DealFirstDiscountType = 0 then 10
					when c.DealFirstDiscountType = 1 then 20
					when c.DealFirstDiscountType = 2 then 25
					when c.DealFirstDiscountType = 3 then 30
					when c.DealFirstDiscountType = 4 then 40
					when c.DealFirstDiscountType = 5 then 50
					when c.DealFirstDiscountType = 6 then 60
					when c.DealFirstDiscountType = 7 then 50
					when c.DealFirstDiscountType = 8 then 1
					when c.DealFirstDiscountType = 9 then 3
					when c.DealFirstDiscountType = 10 then 5
					when c.DealFirstDiscountType = 11 then 10
					when c.DealFirstDiscountType = 12 then 15
					when c.DealFirstDiscountType = 13 then 20
					when c.DealFirstDiscountType = 14 then 25
					when c.DealFirstDiscountType = 15 then 30
					when c.DealFirstDiscountType = 16 then 40
					when c.DealFirstDiscountType = 17 then 50
					
					end ) discount,
					(case when c.DealFirstDiscountType = 0 then 1
					when c.DealFirstDiscountType = 1 then 1
					when c.DealFirstDiscountType = 2 then 1
					when c.DealFirstDiscountType = 3 then 1
					when c.DealFirstDiscountType = 4 then 1
					when c.DealFirstDiscountType = 5 then 1
					when c.DealFirstDiscountType = 6 then 1
					when c.DealFirstDiscountType = 7 then 1
					when c.DealFirstDiscountType = 8 then 2
					when c.DealFirstDiscountType = 9 then 2
					when c.DealFirstDiscountType = 10 then 2
					when c.DealFirstDiscountType = 11 then 2
					when c.DealFirstDiscountType = 12 then 2
					when c.DealFirstDiscountType = 13 then 2
					when c.DealFirstDiscountType = 14 then 2
					when c.DealFirstDiscountType = 15 then 2
					when c.DealFirstDiscountType = 16 then 2
					when c.DealFirstDiscountType = 17 then 2

					end ) discountType
					from coupon c
					inner join company comp on comp.companyid = c.companyid and  comp.IsSpecialAccount is null 
					and (
						
						(c.DealEndingDiscountType = 5 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) between 0 and 2 )
						or
						(c.DealEndingDiscountType = 6 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) = 2 )
					)
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c

			OUTER APPLY (SELECT TOp 1 Price
											FROM   CouponPriceOption cpo
											WHERE  cpo.CouponId = c.CouponId
											ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null and u.status=1
			 and 
			 (geography::Point(u.LastKnownLocationLat, u.LastKnownLocationLong, 4326)).STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 < 200000
			 order by UserId
 	
end	

else	if (@mode = 7)	--last day  additioanl 30 dollar
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,(case when discountType = 1 then (cpopt.price * c.discount + 25) /100 else   cpopt.price -  c.discount - 30 end ) as savings, c.DaysLeft,(case when discountType = 1 then (cpopt.price * c.discount + 20) /100 else   cpopt.price -  c.discount - 30 end ) SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol,
					

					(case when c.DealFirstDiscountType = 0 then 10
					when c.DealFirstDiscountType = 1 then 20
					when c.DealFirstDiscountType = 2 then 25
					when c.DealFirstDiscountType = 3 then 30
					when c.DealFirstDiscountType = 4 then 40
					when c.DealFirstDiscountType = 5 then 50
					when c.DealFirstDiscountType = 6 then 60
					when c.DealFirstDiscountType = 7 then 50
					when c.DealFirstDiscountType = 8 then 1
					when c.DealFirstDiscountType = 9 then 3
					when c.DealFirstDiscountType = 10 then 5
					when c.DealFirstDiscountType = 11 then 10
					when c.DealFirstDiscountType = 12 then 15
					when c.DealFirstDiscountType = 13 then 20
					when c.DealFirstDiscountType = 14 then 25
					when c.DealFirstDiscountType = 15 then 30
					when c.DealFirstDiscountType = 16 then 40
					when c.DealFirstDiscountType = 17 then 50
					
					end ) discount,
					(case when c.DealFirstDiscountType = 0 then 1
					when c.DealFirstDiscountType = 1 then 1
					when c.DealFirstDiscountType = 2 then 1
					when c.DealFirstDiscountType = 3 then 1
					when c.DealFirstDiscountType = 4 then 1
					when c.DealFirstDiscountType = 5 then 1
					when c.DealFirstDiscountType = 6 then 1
					when c.DealFirstDiscountType = 7 then 1
					when c.DealFirstDiscountType = 8 then 2
					when c.DealFirstDiscountType = 9 then 2
					when c.DealFirstDiscountType = 10 then 2
					when c.DealFirstDiscountType = 11 then 2
					when c.DealFirstDiscountType = 12 then 2
					when c.DealFirstDiscountType = 13 then 2
					when c.DealFirstDiscountType = 14 then 2
					when c.DealFirstDiscountType = 15 then 2
					when c.DealFirstDiscountType = 16 then 2
					when c.DealFirstDiscountType = 17 then 2

					end ) discountType
					from coupon c
					inner join company comp on comp.companyid = c.companyid and  comp.IsSpecialAccount is null 
					and (
						
						(c.DealEndingDiscountType = 6 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) between 0 and 1 )
					)
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c

			OUTER APPLY (SELECT TOp 1 Price
											FROM   CouponPriceOption cpo
											WHERE  cpo.CouponId = c.CouponId
											ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null and u.status=1
			 and 
			 (geography::Point(u.LastKnownLocationLat, u.LastKnownLocationLong, 4326)).STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 < 200000
			 order by UserId
 	
end	

else	if (@mode = 8)	--all deals expired few minutes ago to be sent to the advertiser
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,0 as price,0 as savings, c.DaysLeft,0 as SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			inner join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,ApprovalDateTime,dateadd(d,7, getdate())) else datediff(d,ApprovalDateTime,dateadd(d,30,getdate() )) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol, c.companyid
					from coupon c
					inner join company comp on comp.companyid = c.companyid and comp.IsSpecialAccount is null
					and (case when couponlistingmode = 1 then datediff(mi,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(mi,getdate(),dateadd(d,30, ApprovalDateTime)) end)  < 0
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c on c.companyid = u.companyid
		
			
			
			 order by UserId
 	
end	


else	if (@mode =9)	--Deal will delist in 3 days
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,0 as price,0 as savings, c.DaysLeft,0 as SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			inner join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol, c.companyid
					from coupon c
					inner join company comp on comp.companyid = c.companyid and comp.IsSpecialAccount is null
					and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  < 3
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null
		
					) c on c.companyid = u.companyid
		
			
			
			 order by UserId
 	
end	

END



