GO
/****** Object:  StoredProcedure [dbo].[getAdsCampaignByCampaignIdRatioAnalytic]    Script Date: 10/18/2016 1:26:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [dbo].[getAdsCampaignByCampaignIdtblAnalytic] (
@Id INT  -- 1 for Viewed, 2 for Conversions or 3 for Skipped
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @dateFrom DATE = getdate()-30, @Ctype int = 1;
	
	BEGIN
	 Select top 1 @dateFrom= [CreatedDateTime] from AdCampaignResponse where CampaignID = @Id
	 Select top 1 @Ctype = [Type] from AdCampaign where CampaignID = @Id
	END
		Select 1 ordr, (case when @Ctype = 1 then 'Opened' else 'Shown' end) label, count(acr.CampaignID) 'All time' ,
			 (Select count(acr.CampaignID) from AdCampaignResponse acr
			  where acr.CreatedDateTime >= getdate() - 30 and acr.CreatedDateTime <= getdate() and acr.ResponseType = 1 and acr.CampaignID = @Id) '30 days'
		from AdCampaignResponse acr
		where acr.CreatedDateTime >= @dateFrom and acr.CreatedDateTime <= getdate() and acr.ResponseType = 1 and acr.CampaignID = @Id
		union


		Select 2 ordr, 'Skipped' label, count(acr.CampaignID) 'All time' , 
			(Select count(acr.CampaignID) from AdCampaignResponse acr
			where acr.CreatedDateTime >= getdate() - 30 and acr.CreatedDateTime <= getdate() and acr.ResponseType = 4 and acr.CampaignID = @Id) '30 days'
		from AdCampaignResponse acr
		where acr.CreatedDateTime >= @dateFrom and acr.CreatedDateTime <= getdate() and acr.ResponseType = 4 and acr.CampaignID = @Id
		union

		Select 3 ordr, 'Landing Page Conversions' label, count(acr.CampaignID) 'All time' , 
			(Select count(acr.CampaignID) from AdCampaignResponse acr
			where acr.CreatedDateTime >= getdate() - 30 and acr.CreatedDateTime <= getdate() and acr.ResponseType = 2 and acr.CampaignID = @Id) '30 days'
		from AdCampaignResponse acr
		where acr.CreatedDateTime >= @dateFrom and acr.CreatedDateTime <= getdate() and acr.ResponseType = 2 and acr.CampaignID = @Id
		
		union
		Select 4 ordr, 'Quiz Surveys answered' label, count(acr.CampaignID) 'All time' , 
			(Select count(acr.CampaignID) from AdCampaignResponse acr
			where acr.CreatedDateTime >= getdate() - 30 and acr.CreatedDateTime <= getdate() and acr.ResponseType = 3 and acr.CampaignID = @Id) '30 days'
		from AdCampaignResponse acr
		where acr.CreatedDateTime >= @dateFrom and acr.CreatedDateTime <= getdate() and acr.ResponseType = 3 and acr.CampaignID = @Id
		
		union
		Select 5 ordr, 'Average Ad Click' label, isnull((sum(t.DebitAmount)/count(t.DebitAmount))/100, 0) 'All time' , 
			(Select isnull((sum(t.DebitAmount)/count(t.DebitAmount))/100, 0) from [Transaction] t
			where t.TransactionDate >= getdate() - 30 and t.TransactionDate <= getdate() and t.AdCampaignID = @Id) '30 days'
		from [Transaction] t
		where t.TransactionDate >= @dateFrom and t.TransactionDate <= getdate() AND t.AdCampaignID = @Id

		union
		Select 6 ordr, 'Cost' label, isnull(sum(t.DebitAmount)/100, 0) 'All time' , 
			(Select isnull(sum(t.DebitAmount)/100, 0) from [Transaction] t
			where t.TransactionDate >= getdate() - 30 and t.TransactionDate <= getdate() and t.AdCampaignID = @Id) '30 days'
		from [Transaction] t
		where t.TransactionDate >= @dateFrom and t.TransactionDate <= getdate() AND t.AdCampaignID = @Id
				
END
 
 
--EXEC [getAdsCampaignByCampaignIdtblAnalytic] 20303
