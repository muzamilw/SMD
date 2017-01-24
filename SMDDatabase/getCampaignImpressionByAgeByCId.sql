GO
/****** Object:  StoredProcedure [dbo].[getCampaignImpressionByAgeByCId]    Script Date: 1/22/2017 9:35:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getCampaignImpressionByAgeByCId] (
@Id INT
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

		select count(*) as Stats, '13-20' label  from AdCampaignResponse ac 
		inner join AspNetUsers usr on ac.UserID = usr.Id
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		and usr.DOB > = DATEADD(YYYY,-20,getdate()) and usr.DOB < DATEADD(YYYY,-13,getdate())
		union
		select count(*) as Stats, '21-30' label  from AdCampaignResponse ac 
		inner join AspNetUsers usr on ac.UserID = usr.Id
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		and usr.DOB > = DATEADD(YYYY,-30,getdate()) and usr.DOB < DATEADD(YYYY,-20,getdate())
		union
		select count(*) as Stats, '31-40' label  from AdCampaignResponse ac 
		inner join AspNetUsers usr on ac.UserID = usr.Id
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		and usr.DOB > = DATEADD(YYYY,-40,getdate()) and usr.DOB < DATEADD(YYYY,-30,getdate())
		union
		select count(*) as Stats, '41-50' label  from AdCampaignResponse ac 
		inner join AspNetUsers usr on ac.UserID = usr.Id
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		and usr.DOB > = DATEADD(YYYY,-50,getdate()) and usr.DOB < DATEADD(YYYY,-40,getdate())
		union
		select count(*) as Stats, '51-60' label  from AdCampaignResponse ac 
		inner join AspNetUsers usr on ac.UserID = usr.Id
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		and usr.DOB > = DATEADD(YYYY,-60,getdate()) and usr.DOB < DATEADD(YYYY,-50,getdate())
		union
		select count(*) as Stats, '+61' label  from AdCampaignResponse ac 
		inner join AspNetUsers usr on ac.UserID = usr.Id
		where ac.CampaignID = @Id and ac.ResponseType = 3 
		and usr.DOB > = DATEADD(YYYY,-200,getdate()) and usr.DOB < DATEADD(YYYY,-60,getdate())
			
	
END
 


