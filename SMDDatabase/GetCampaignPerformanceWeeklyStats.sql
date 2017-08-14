USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[GetCampaignPerformanceWeeklyStats]    Script Date: 1/12/2017 5:19:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mz
-- Create date: 
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[GetCampaignPerformanceWeeklyStats] 

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


--video and display ads
select  u.email, u.FullName,
ad.userid, ad.companyid,ad.type, ad.campaignid, ad.CampaignName, ClickThroughsLastWeek.cnt ClickThroughsLastWeek,ClickThroughsPreviousWeek.cnt ClickThroughsPreviousWeek, 
round((case when ClickThroughsPreviousWeek.cnt = 0 then 100 else cast(ClickThroughsLastWeek.cnt-ClickThroughsPreviousWeek.cnt as float)/ cast(ClickThroughsPreviousWeek.cnt as float) * 100 end ),2) ProgressPercentage,
AnsweredLastWeek.cnt AnsweredLastWeek,AnsweredPreviousWeek.cnt AnsweredPreviousWeek,
round((case when AnsweredPreviousWeek.cnt = 0 then 100 else cast(AnsweredLastWeek.cnt-AnsweredPreviousWeek.cnt as float) / cast(AnsweredPreviousWeek.cnt as float) * 100 end ),2) ProgressPercentageAnswer  ,
0 LeftPicResponseCount, 0 RightPicResponseCount
   from adcampaign ad
   inner join AspNetUsers u on ad.userid = u.id and u.optMarketingEmails = 1
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
 where ad.status = 3
 union
 -- coupon deals
select  u.email, u.FullName,
 c.userid,c.companyid,5,c.couponid as campaignid, c.coupontitle as campaignname, ClickThroughsLastWeek.cnt ClickThroughsLastWeek,ClickThroughsPreviousWeek.cnt ClickThroughsPreviousWeek,
 round((case when ClickThroughsPreviousWeek.cnt = 0 then 100 else cast(ClickThroughsLastWeek.cnt-ClickThroughsPreviousWeek.cnt as float)/ cast(ClickThroughsPreviousWeek.cnt as float) * 100 end ),2) ProgressPercentage,
 AnsweredLastWeek.cnt AnsweredLastWeek,AnsweredPreviousWeek.cnt AnsweredPreviousWeek,
 round((case when AnsweredPreviousWeek.cnt = 0 then 100 else cast(AnsweredLastWeek.cnt-AnsweredPreviousWeek.cnt as float) / cast(AnsweredPreviousWeek.cnt as float) * 100 end ),2) ProgressPercentageAnswer  ,
 0 LeftPicResponseCount, 0 RightPicResponseCount
 from coupon c
    inner join AspNetUsers u on c.userid = u.id and u.optMarketingEmails = 1
	OUTER APPLY (SELECT count(*) cnt
								FROM   UserPurchasedCoupon resp
								WHERE  c.couponid = resp.couponid and resp.PurchaseDateTime between @lastweekstart and dateadd(hh,24,@lastweekend)
								) ClickThroughsLastWeek
	OUTER APPLY (SELECT count(*) cnt
								FROM   UserPurchasedCoupon resp
								WHERE  c.couponid = resp.couponid and resp.PurchaseDateTime between @previousweekstart and dateadd(hh,24,@previousweekend)
								) ClickThroughsPreviousWeek
	OUTER APPLY (SELECT count(*) cnt
								FROM   UserCouponView resp
								WHERE  c.couponid = resp.couponid and ViewDateTime between @lastweekstart and dateadd(hh,24,@lastweekend)
								) AnsweredLastWeek
	OUTER APPLY (SELECT count(*) cnt
								FROM   UserCouponView resp
								WHERE  c.couponid = resp.couponid  and ViewDateTime between @previousweekstart and dateadd(hh,24,@previousweekend)
								) AnsweredPreviousWeek
 where c.status = 3
 union
 -- picture polls / survey questions
select  u.email, u.FullName,
 sq.userid,sq.companyid,6,sq.SQID as campaignid, sq.Question as campaignname, 0 ClickThroughsLastWeek,0 ClickThroughsPreviousWeek,
0 ProgressPercentage,
0 AnsweredLastWeek,0 AnsweredPreviousWeek,
0 ProgressPercentageAnswer, LeftPicResponse.cnt LeftPicResponseCount ,RightPicResponse.cnt RightPicResponseCount
 from SurveyQuestion sq
    inner join AspNetUsers u on sq.userid = u.id and u.optMarketingEmails = 1 and sq.CompanyId is not null
	OUTER APPLY (SELECT count(*) cnt
								FROM   [SurveyQuestionResponse] resp
								WHERE  sq.SQID = resp.SQID and responsetype = 3 and resp.UserSelection = 1 and ResoponseDateTime between @lastweekstart and dateadd(hh,24,@lastweekend)
								) LeftPicResponse
	OUTER APPLY (SELECT count(*) cnt
								FROM   [SurveyQuestionResponse] resp
								WHERE  sq.SQID = resp.SQID and responsetype = 3 and resp.UserSelection = 2 and ResoponseDateTime between @lastweekstart and dateadd(hh,24,@lastweekend)
								) RightPicResponse

 where sq.status = 3

END
