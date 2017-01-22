USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[getCampaignImpressionByProfessionByCId]    Script Date: 1/22/2017 9:36:15 PM ******/
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


		select count(*) as Stats, usr.Jobtitle label  from AdCampaignResponse ac 
		inner join AspNetUsers usr on ac.UserID = usr.Id
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		group by usr.Jobtitle
			
	
END
 
--EXEC [getCampaignImpressionByProfessionByCId] 20369

