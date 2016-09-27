USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[GetActiveVSNewUsers]    Script Date: 9/26/2016 11:58:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [dbo].[GetDealMetric] 
	
AS
BEGIN
	
Select count(ucv.CouponId) ViewStats, (Select  count(upc.CouponPurchaseId) from UserPurchasedCoupon upc where upc.PurchaseDateTime > =  getdate()-1  AND upc.ResponseType = 3) as LandingPageStats, '1 Day' Granual
from UserCouponView ucv
where ucv.ViewDateTime >= getdate()-1
union
Select count(ucv.CouponId) ViewStats, (Select  count(upc.CouponPurchaseId) from UserPurchasedCoupon upc where upc.PurchaseDateTime > =  getdate()-7  AND upc.ResponseType = 3) as LandingPageStats, '7 Days' Granual
from UserCouponView ucv
where ucv.ViewDateTime >= getdate()-7
union
Select count(ucv.CouponId) ViewStats, (Select  count(upc.CouponPurchaseId) from UserPurchasedCoupon upc where upc.PurchaseDateTime > =  getdate()-14  AND upc.ResponseType = 3) as LandingPageStats, '14 Days' Granual
from UserCouponView ucv
where ucv.ViewDateTime >= getdate()-14
union
Select count(ucv.CouponId) ViewStats, (Select  count(upc.CouponPurchaseId) from UserPurchasedCoupon upc where upc.PurchaseDateTime > =  getdate()-30  AND upc.ResponseType = 3) as LandingPageStats, '1 Months' Granual
from UserCouponView ucv
where ucv.ViewDateTime >= getdate()-30
union
Select count(ucv.CouponId) ViewStats, (Select  count(upc.CouponPurchaseId) from UserPurchasedCoupon upc where upc.PurchaseDateTime > =  getdate()-91  AND upc.ResponseType = 3) as LandingPageStats, '3 Months' Granual
from UserCouponView ucv
where ucv.ViewDateTime >= getdate()-91


END

--EXEC GetDealMetric
