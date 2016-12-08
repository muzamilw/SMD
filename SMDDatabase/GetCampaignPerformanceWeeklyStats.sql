-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mz
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE GetCampaignPerformanceWeeklyStats 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
declare @lastweekstart datetime
declare @lastweekend datetime
declare @previousweekstart datetime
declare @previousweekend datetime

select @lastweekstart = DATEADD(wk, -1, DATEADD(DAY, 1-DATEPART(WEEKDAY, GETDATE()), DATEDIFF(dd, 0, GETDATE()))) --first day previous week
SELECT @lastweekend= DATEADD(wk, 0, DATEADD(DAY, 0-DATEPART(WEEKDAY, GETDATE()), DATEDIFF(dd, 0, GETDATE()))) --last day previous week

SELECT @previousweekstart = DATEADD(wk, -2, DATEADD(DAY, 1-DATEPART(WEEKDAY, GETDATE()), DATEDIFF(dd, 0, GETDATE()))) --first day previous week
SELECT @previousweekend = DATEADD(wk, -1, DATEADD(DAY, 0-DATEPART(WEEKDAY, GETDATE()), DATEDIFF(dd, 0, GETDATE()))) --last day previous week



select ad.userid, ad.companyid,ad.type, ad.campaignid, ad.CampaignName, ClickThroughsLastWeek.cnt ClickThroughsLastWeek,ClickThroughsPreviousWeek.cnt ClickThroughsPreviousWeek,  (ClickThroughsLastWeek.cnt-ClickThroughsPreviousWeek.cnt)/ClickThroughsPreviousWeek.cnt * 100 ratio,   AnsweredLastWeek.cnt AnsweredLastWeek,AnsweredPreviousWeek.cnt AnsweredPreviousWeek   from adcampaign ad
	OUTER APPLY (SELECT count(*) cnt
								FROM   [AdCampaignResponse] resp
								WHERE  ad.campaignid = resp.campaignid and responsetype = 2 and CreatedDateTime between @lastweekstart and dateadd(hh,24,@lastweekend)
								) ClickThroughsLastWeek
	OUTER APPLY (SELECT count(*) cnt
								FROM   [AdCampaignResponse] resp
								WHERE  ad.campaignid = resp.campaignid and responsetype = 2 and CreatedDateTime between @previousweekstart and dateadd(hh,24,@previousweekend)
								) ClickThroughsPreviousWeek
	OUTER APPLY (SELECT count(*) cnt
								FROM   [AdCampaignResponse] resp
								WHERE  ad.campaignid = resp.campaignid and responsetype = 3 and CreatedDateTime between @lastweekstart and dateadd(hh,24,@lastweekend)
								) AnsweredLastWeek
	OUTER APPLY (SELECT count(*) cnt
								FROM   [AdCampaignResponse] resp
								WHERE  ad.campaignid = resp.campaignid and responsetype = 2 and CreatedDateTime between @previousweekstart and dateadd(hh,24,@previousweekend)
								) AnsweredPreviousWeek
 where status = 3



END
GO
