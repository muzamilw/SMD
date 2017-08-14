GO
/****** Object:  StoredProcedure [dbo].[getDealStatByDDProfessionByCId]    Script Date: 1/22/2017 6:05:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[getDealStatByDDProfessionByCId] (
@Id INT,
@profession nvarchar(200)
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;


		select count(*) as Stats  from UserCouponView ucv
		left join AspNetUsers usr on ucv.UserID = usr.Id
		where ucv.CouponId = @Id 
		and (usr.Jobtitle = @profession OR @profession = 'All')
		
			
	
END
 
--EXEC [getDealStatByDDProfessionByCId] 10255

