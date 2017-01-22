GO
/****** Object:  StoredProcedure [dbo].[getDealImpressionByGenderByCouponId]    Script Date: 1/22/2017 6:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[getDealImpressionByGenderByCouponId] (
@Id INT
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;


		select count(*) as Stats, 'Male' label  from UserCouponView ucv
		inner join AspNetUsers usr on ucv.UserID = usr.Id
		where ucv.CouponId = @Id and usr.Gender = 1
		
		union
		select count(*) as Stats, 'Female' label  from UserCouponView ucv
		inner join AspNetUsers usr on ucv.UserID = usr.Id
		where ucv.CouponId = @Id and usr.Gender = 2
		
		
			
	
END
 
--EXEC [getDealImpressionByGenderByCouponId] 17

