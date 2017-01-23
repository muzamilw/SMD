USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[getCampaignImpressionByProfessionByCId]    Script Date: 1/24/2017 12:03:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getCampaignImpressionByProfessionByCId] (
@Id INT
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;


		select count(*) as Stats, isnull(usr.Jobtitle,'No Job') label  from AdCampaignResponse ac 
		inner join AspNetUsers usr on ac.UserID = usr.Id
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		group by usr.Jobtitle
			
	
END
 
--EXEC [getCampaignImpressionByProfessionByCId] 20369

