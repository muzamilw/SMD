GO
/****** Object:  StoredProcedure [dbo].[getAdsCampaignByCampaignIdRatioAnalytic]    Script Date: 10/16/2016 7:56:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[getAdsCampaignByCampaignIdRatioAnalytic] (
@Id INT,  -- 1 for Viewed, 2 for Conversions or 3 for Skipped
@DateRange INT -- 1 for last 30 days , 2 for All time
		
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @dateFrom DATE = getdate()-30;
 

IF @DateRange = 2
	BEGIN
	 Select top 1 @dateFrom= [CreatedDateTime] from AdCampaignResponse where CampaignID = @Id
	 
	END


		Select 'Landing Page Conversions' label, count(acr.CampaignID) value
		from AdCampaignResponse acr
		where acr.CreatedDateTime >= @dateFrom and acr.CreatedDateTime <= getdate() and acr.ResponseType = 2 and acr.CampaignID = @Id
		union 
		Select 'Reward Click count' label, count(acr.CampaignID) value
		from AdCampaignResponse acr
		where acr.CreatedDateTime >= @dateFrom and acr.CreatedDateTime <= getdate() and acr.ResponseType = 3 and acr.CampaignID = @Id


	
END
 
 
--EXEC [getAdsCampaignByCampaignIdRatioAnalytic] 10043, 2
