GO
/****** Object:  StoredProcedure [dbo].[getDealImpressionByProfessionByCouponId]    Script Date: 1/22/2017 6:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[getDealImpressionByProfessionByCouponId] (
@Id INT
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;


		select count(*) as Stats, usr.Jobtitle label  from UserCouponView ucv
		inner join AspNetUsers usr on ucv.UserID = usr.Id
		where ucv.CouponId = @Id 
		group by usr.Jobtitle
			
	
END
 
--EXEC [getDealImpressionByProfessionByCouponId] 10255

