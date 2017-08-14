GO
/****** Object:  StoredProcedure [dbo].[getDealImpressionByAgeByCouponId]    Script Date: 1/22/2017 6:04:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[getDealImpressionByAgeByCouponId] (
@Id INT
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;


		select count(*) as Stats, '13-20' label  from UserCouponView ucv
		inner join AspNetUsers usr on ucv.UserID = usr.Id
		where ucv.CouponId = @Id 
		and usr.DOB > = DATEADD(YYYY,-20,getdate()) and usr.DOB < DATEADD(YYYY,-13,getdate())
		union
		select count(*) as Stats, '21-30' label  from UserCouponView ucv
		inner join AspNetUsers usr on ucv.UserID = usr.Id
		where ucv.CouponId = @Id 
		and usr.DOB > = DATEADD(YYYY,-30,getdate()) and usr.DOB < DATEADD(YYYY,-20,getdate())
		union
		select count(*) as Stats, '31-40' label  from UserCouponView ucv
		inner join AspNetUsers usr on ucv.UserID = usr.Id
		where ucv.CouponId = @Id
		and usr.DOB > = DATEADD(YYYY,-40,getdate()) and usr.DOB < DATEADD(YYYY,-30,getdate())
		union
		select count(*) as Stats, '41-50' label  from UserCouponView ucv
		inner join AspNetUsers usr on ucv.UserID = usr.Id
		where ucv.CouponId = @Id
		and usr.DOB > = DATEADD(YYYY,-50,getdate()) and usr.DOB < DATEADD(YYYY,-40,getdate())
		union
		select count(*) as Stats, '51-60' label  from UserCouponView ucv
		inner join AspNetUsers usr on ucv.UserID = usr.Id
		where ucv.CouponId = @Id
		and usr.DOB > = DATEADD(YYYY,-60,getdate()) and usr.DOB < DATEADD(YYYY,-50,getdate())
		union
		select count(*) as Stats, '+61' label  from UserCouponView ucv
		inner join AspNetUsers usr on ucv.UserID = usr.Id
		where ucv.CouponId = @Id
		and usr.DOB > = DATEADD(YYYY,-200,getdate()) and usr.DOB < DATEADD(YYYY,-60,getdate())
			
	
END
 
--EXEC [getDealImpressionByAgeByCouponId] 10255

