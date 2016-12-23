USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[GetUsersCouponsForEmailNotification]    Script Date: 12/21/2016 3:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[GetUsersCouponsForEmailNotification]
  @mode AS INT
--   [GetUsersCouponsForEmailNotification] 1
AS

BEGIN

if (@mode = 1)
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,cpopt.savings, c.DaysLeft,cpopt.SavingsNew, c.LocationCity,c.CurrencySymbol from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol
					from coupon c
					inner join company comp on comp.companyid = c.companyid and comp.IsSpecialAccount is null and  datediff(hh,ApprovalDateTime,getdate()) between 0 and 24
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null
		
					) c

			OUTER APPLY (SELECT TOp 1 Price, Savings,Price - ( ((((price-Savings)/price) * 100)+20 )  *  Price/100) as SavingsNew
											FROM   CouponPriceOption cpo
											WHERE  cpo.CouponId = c.CouponId
											ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null and status=1
			 and 
			 (geography::Point(u.LastKnownLocationLat, u.LastKnownLocationLong, 4326)).STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 < 200000
			 order by UserId
 	
	end	
else	if (@mode = 2)	--last 3 days  additioanl 20%
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,cpopt.savings, c.DaysLeft,cpopt.SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol
					from coupon c
					inner join company comp on comp.companyid = c.companyid and  comp.IsSpecialAccount is null and c.IsPerSaving3days = 1 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) = 3
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c

			OUTER APPLY (SELECT TOp 1 Price, savings,Price - ( ((((price-Savings)/price) * 100)+20 )  *  Price/100) as SavingsNew
											FROM   CouponPriceOption cpo
											WHERE  cpo.CouponId = c.CouponId
											ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null and u.status=1
			 and 
			 (geography::Point(u.LastKnownLocationLat, u.LastKnownLocationLong, 4326)).STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 < 200000
			 order by UserId
 	
end	
else	if (@mode = 3)	--last 2 days  additioanl 25%
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,cpopt.savings, c.DaysLeft,cpopt.SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol
					from coupon c
					inner join company comp on comp.companyid = c.companyid and comp.IsSpecialAccount is null and c.IsPerSaving2days = 1 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) = 2
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c

			OUTER APPLY (SELECT TOp 1 Price, savings,Price - ( ((((price-Savings)/price) * 100)+25 )  *  Price/100) as SavingsNew
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

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,cpopt.savings, c.DaysLeft,cpopt.SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol
					from coupon c
					inner join company comp on comp.companyid = c.companyid and comp.IsSpecialAccount is null and c.[IsPerSavingLastday] = 1 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end) between 1 and 0
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c

			OUTER APPLY (SELECT TOp 1 Price, savings,Price - ( ((((price-Savings)/price) * 100)+30 )  *  Price/100) as SavingsNew
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

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,cpopt.savings, c.DaysLeft,cpopt.SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol
					from coupon c
					inner join company comp on comp.companyid = c.companyid and comp.IsSpecialAccount is null and c.IsDollarSaving3days = 1 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  = 3
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c

			OUTER APPLY (SELECT TOp 1 Price, savings,savings + 10 as SavingsNew
											FROM   CouponPriceOption cpo
											WHERE  cpo.CouponId = c.CouponId
											ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null  and u.status=1
			 and 
			 (geography::Point(u.LastKnownLocationLat, u.LastKnownLocationLong, 4326)).STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 < 200000
			 order by UserId
 	
end	

else	if (@mode = 6)	--last 2 day  additioanl 20 dollar
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,cpopt.savings, c.DaysLeft,cpopt.SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol
					from coupon c
					inner join company comp on comp.companyid = c.companyid and comp.IsSpecialAccount is null and c.IsDollarSaving2days = 1 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  = 2
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null  
		
					) c

			OUTER APPLY (SELECT TOp 1 Price, savings,savings + 20 as SavingsNew
											FROM   CouponPriceOption cpo
											WHERE  cpo.CouponId = c.CouponId
											ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null  and u.status=1
			 and 
			 (geography::Point(u.LastKnownLocationLat, u.LastKnownLocationLong, 4326)).STDistance(geography::Point(c.LocationLAT, c.LocationLON, 4326))/1000 < 200000
			 order by UserId
 	
end	

else	if (@mode = 7)	--last day  additioanl 30 dollar
begin

			select  u.id as UserId, u.FullName, u.Email, c.CouponId, c.CouponTitle,c.couponimage1,cpopt.price,cpopt.savings, c.DaysLeft,cpopt.SavingsNew, c.LocationCity,c.CurrencySymbol  from AspNetUsers u 
			cross join (
					select couponid, coupontitle, LocationLAT,LocationLON, ApprovalDateTime, couponImage1 
					,(case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  DaysLeft,
					c.ApprovalDateTime ccc, c.LocationCity, curr.CurrencySymbol
					from coupon c
					inner join company comp on comp.companyid = c.companyid and comp.IsSpecialAccount is null and c.IsDollarSavingLastday = 1 and (case when couponlistingmode = 1 then datediff(d,getdate(),dateadd(d,7, ApprovalDateTime)) else datediff(d,getdate(),dateadd(d,30, ApprovalDateTime)) end)  between 1 and 0
					inner join Country cnt on cnt.CountryID = comp.BillingCountryId
					inner join Currency curr on curr.CurrencyID = cnt.CurrencyID
					where c.Status = 3 and LocationLAT is not null 
		
					) c

			OUTER APPLY (SELECT TOp 1 Price, savings,savings + 30 as SavingsNew
											FROM   CouponPriceOption cpo
											WHERE  cpo.CouponId = c.CouponId
											ORDER  BY cpo.Price) cpopt
			where u.optDealsNearMeEmails = 1 and  u.LastKnownLocationLat is not null  and u.status=1
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



