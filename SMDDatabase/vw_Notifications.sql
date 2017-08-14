

/****** Object:  View [dbo].[vw_Notifications]    Script Date: 1/10/2017 1:23:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- select * from [vw_Notifications] where type =1
ALTER VIEW [dbo].[vw_Notifications]
AS
SELECT        n.ID, Type, n.UserID, IsRead, GeneratedOn, GeneratedBy, n.SurveyQuestionShareId, n.PhoneNumber,
				(case when n.[type] = 1 then u.FullName +  ' wants your opinion' else 'New Deal around you is available' end) NotificationDetails,
				(case when n.[type] = 1 then q.SurveyTitle else '' end) PollTitle, 
				(case when n.[type] = 1 then q.SSQID else 0 end) SSQID,
				(case when n.[type] = 2 then n.CouponId else 0 end) CouponId,
				(case when n.[type] = 2 then c.CouponTitle else '' end) DealTitle,
				isnull(couponPrice.Price,0) DealPrice,
				isnull((case when selfr.discountType = 1 then couponPrice.price - (couponPrice.price * selfr.discount/100) else   couponPrice.price -  selfr.discount end ),0) DealSavings,
				isnull(DealCount.cnt,0) as DealCount,
				isnull(couponPrice.descrip,'') as DealDescription,
				isnull(comp.CompanyName,'') DealCompany,
				isnull(c.LocationCity,'') DealCity,
				u.ProfileImage,
				curr.CurrencyCode,
				curr.CurrencySymbol

				
FROM            dbo.Notifications n
			left outer join SurveySharingGroupShares s on n.SurveyQuestionShareId = s.SurveyQuestionShareId
			left outer join SharedSurveyQuestion q on q.SSQID = s.SSQID
			left outer join AspNetUsers u on q.UserId = u.Id
			left outer join Coupon c on n.CouponId = c.CouponId
			left outer join Company comp on c.CompanyId = comp.CompanyId
			
			left outer join Country cntry on comp.BillingCountryId = cntry.CountryID
			left outer join Currency curr on cntry.CurrencyID = curr.CurrencyID


			OUTER APPLY (SELECT TOp 1 Price, Savings, [Description] as descrip
									FROM   CouponPriceOption cpo
									WHERE  cpo.CouponId = n.CouponId
									ORDER  BY cpo.Price) couponPrice
			OUTER APPLY (SELECT count(*) cnt
								FROM   CouponPriceOption cpo
								WHERE  cpo.CouponId = n.CouponId
								) DealCount
			outer apply (
							select 
							(case when crr.DealFirstDiscountType = 0 then 10
					when crr.DealFirstDiscountType = 1 then 20
					when crr.DealFirstDiscountType = 2 then 25
					when crr.DealFirstDiscountType = 3 then 30
					when crr.DealFirstDiscountType = 4 then 40
					when crr.DealFirstDiscountType = 5 then 50
					when crr.DealFirstDiscountType = 6 then 60
					when crr.DealFirstDiscountType = 7 then 50
					when crr.DealFirstDiscountType = 8 then 1
					when crr.DealFirstDiscountType = 9 then 3
					when crr.DealFirstDiscountType = 10 then 5
					when crr.DealFirstDiscountType = 11 then 10
					when crr.DealFirstDiscountType = 12 then 15
					when crr.DealFirstDiscountType = 13 then 20
					when crr.DealFirstDiscountType = 14 then 25
					when crr.DealFirstDiscountType = 15 then 30
					when crr.DealFirstDiscountType = 16 then 40
					when crr.DealFirstDiscountType = 17 then 50
					



					end ) discount,
					(case when crr.DealFirstDiscountType = 0 then 1
					when crr.DealFirstDiscountType = 1 then 1
					when crr.DealFirstDiscountType = 2 then 1
					when crr.DealFirstDiscountType = 3 then 1
					when crr.DealFirstDiscountType = 4 then 1
					when crr.DealFirstDiscountType = 5 then 1
					when crr.DealFirstDiscountType = 6 then 1
					when crr.DealFirstDiscountType = 7 then 1
					when crr.DealFirstDiscountType = 8 then 2
					when crr.DealFirstDiscountType = 9 then 2
					when crr.DealFirstDiscountType = 10 then 2
					when crr.DealFirstDiscountType = 11 then 2
					when crr.DealFirstDiscountType = 12 then 2
					when crr.DealFirstDiscountType = 13 then 2
					when crr.DealFirstDiscountType = 14 then 2
					when crr.DealFirstDiscountType = 15 then 2
					when crr.DealFirstDiscountType = 16 then 2
					when crr.DealFirstDiscountType = 17 then 2

					end ) discountType
					from coupon crr where crr.CouponId = c.CouponId
							) selfr





GO


