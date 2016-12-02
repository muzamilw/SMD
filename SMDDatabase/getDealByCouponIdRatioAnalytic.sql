GO
/****** Object:  StoredProcedure [dbo].[getDealByCouponIdRatioAnalytic]    Script Date: 11/25/2016 5:13:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getDealByCouponIdRatioAnalytic] (
@Id INT,  -- 1 for Viewed, 2 for Conversions or 3 for Skipped
@DateRange INT -- 1 for last 30 days , 2 for All time	
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @dateFrom DATE = getdate()-30;
 
IF @DateRange = 2
	BEGIN
	 Select top 1 @dateFrom = ViewDateTime from UserCouponView where CouponId = @Id
	END

		Select 'Impressions' label, count(r.UserCouponViewId) value 
		from  UserCouponView r 
		where r.CouponId = @Id and r.ViewDateTime >= @dateFrom and r.ViewDateTime <= getdate() 
		union
		Select 'Click thru' label, count(r.CouponPurchaseId) value 
		from  UserPurchasedCoupon r
		where r.CouponId = @Id and r.ResponseType = 2 and r.PurchaseDateTime >= @dateFrom and r.PurchaseDateTime <= getdate() 

END
 
 
--EXEC [getDealByCouponIdRatioAnalytic] 10078, 1
