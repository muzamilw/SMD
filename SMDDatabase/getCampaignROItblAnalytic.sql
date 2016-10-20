GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[getCampaignROItblAnalytic] (
@Id INT  -- CampaignId
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @dateFrom DATE = getdate()-30, @openAds int = 1;
	
	BEGIN
	 Select top 1 @dateFrom= [CreatedDateTime] from AdCampaignResponse where CampaignID = @Id
	 Select  @openAds= count(acr.CampaignID) from AdCampaignResponse acr where acr.ResponseType = 1 and acr.CampaignID = @Id
	END
		Select 1 ordr, 'Free Video Ads (Skipped)' label1, count(acr.CampaignID) Stats1, 'Average Ad Click' label2, (Select isnull((sum(t.DebitAmount)/count(t.DebitAmount))/100, 0)   
																													from [Transaction] t
																													where t.AdCampaignID = @Id) Stats2, 'Imp. conversion to Landing Pages' label3, 
																													(Select (case when @openAds > 0 then (count(acr.CampaignID)/(@openAds))*100 else count(acr.CampaignID) end )  			 
																													from AdCampaignResponse acr where acr.ResponseType = 2 and acr.CampaignID = @Id ) Stats3 		 
		from AdCampaignResponse acr
		where acr.ResponseType = 4 and acr.CampaignID = @Id
		union
		Select 2 ordr, 'Conversion to Landing Pages' label1, count(acr.CampaignID) Stats1, 'Rewarded to audiences' label2, 
								(Select isnull(sum(t.CreditAmount)/100, 0)   
								FROM [Transaction] t
								inner join Account a on a.AccountId = t.AccountID
								where t.AdCampaignID = @Id and a.CompanyId != 466) 	stats2, 'Imp. conversion to Quiz Reward' label3, 
											(Select (case when @openAds > 0 then (count(acr.CampaignID)/(@openAds))*100 else count(acr.CampaignID) end )  			 
											from AdCampaignResponse acr
											where acr.ResponseType = 3 and acr.CampaignID = @Id	) Stats3		 
		from AdCampaignResponse acr
		where acr.ResponseType = 2 and acr.CampaignID = @Id
		union
		Select 3 ordr, 'Full Video + Quiz Survey' label1, count(acr.CampaignID) Stats1 , 'Campaign Cost to date' label2, (Select isnull(sum(t.DebitAmount)/100, 0) 
																														from [Transaction] t
																														where t.AdCampaignID = @Id) stats2 , ''  label3, null stats3			 
		from AdCampaignResponse acr
		where acr.ResponseType = 3 and acr.CampaignID = @Id
		
			
END
 
 
--EXEC [getCampaignROItblAnalytic] 20303
