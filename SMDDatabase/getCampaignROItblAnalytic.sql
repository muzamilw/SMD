﻿USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[getCampaignROItblAnalytic]    Script Date: 1/18/2017 11:26:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getCampaignROItblAnalytic] (
@Id INT  -- CampaignId
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @dateFrom DATE = getdate()-30, @openAds float = 1;
	
	BEGIN
	 Select top 1 @dateFrom= [CreatedDateTime] from AdCampaignResponse where CampaignID = @Id
	 Select  @openAds= count(acr.CampaignID) from AdCampaignResponse acr where acr.ResponseType = 1 and acr.CampaignID = @Id
	END
		Select 1 ordr, 'Campaign Cost to date' label, isnull(ROUND(sum(t.DebitAmount)/100,2), 0) stats
		from [Transaction] t
		where t.AdCampaignID = @Id		
		union	
		
		Select 2 ordr, 'Rewarded to audiences' label2, ROUND(isnull(sum(t.CreditAmount)/100, 0),2)   Stats
		FROM [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		where t.AdCampaignID = @Id and a.CompanyId != 466	
		union
	
		Select 3 ordr, 'Average ad click' label, isnull(ROUND((sum(t.DebitAmount)/count(t.DebitAmount))/100, 2), 0) Stats
		from [Transaction] t
		where t.AdCampaignID = @Id
		union
																								
		Select 4 ordr, 	 'Impressions to Click Thru' label, (case when @openAds > 0 then CAST((count(acr.CampaignID)/(@openAds))*100 as int) else count(acr.CampaignID) end )  	Stats		 
		from AdCampaignResponse acr where acr.ResponseType = 2 and acr.CampaignID = @Id 																												
		union
		Select 5 ordr,	'Impressions to Quiz Results' label,  (case when @openAds > 0 then CAST((count(acr.CampaignID)/(@openAds))*100 AS int) else count(acr.CampaignID) end )  Stats			 
		from AdCampaignResponse acr
		where acr.ResponseType = 3 and acr.CampaignID = @Id	 
		
			
END
 
 
--EXEC [getCampaignROItblAnalytic] 20303
