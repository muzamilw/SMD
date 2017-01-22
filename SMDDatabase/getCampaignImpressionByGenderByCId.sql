USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[getCampaignImpressionByGenderByCId]    Script Date: 1/22/2017 9:35:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getCampaignImpressionByGenderByCId] (
@Id INT
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;


		select count(*) as Stats, 'Male' label  from AdCampaignResponse ac 
		inner join AspNetUsers usr on ac.UserID = usr.Id
		where ac.CampaignID = @Id and ac.ResponseType = 3 and usr.Gender = 1
		
		union
		select count(*) as Stats, 'Female' label  from AdCampaignResponse ac 
		inner join AspNetUsers usr on ac.UserID = usr.Id
		where ac.CampaignID = @Id and ac.ResponseType = 3 and usr.Gender = 2
		
		
			
	
END
 
--EXEC [getCampaignImpressionByGenderByCId] 17

