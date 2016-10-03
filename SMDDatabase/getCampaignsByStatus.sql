
GO
/****** Object:  StoredProcedure [dbo].[GetActiveVSNewUsers]    Script Date: 10/2/2016 8:06:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getCampaignsByStatus] 
	
AS
BEGIN
--EXEC getCampaignsByStatus	

	select rectype, (case when [1] is not null then [1] else 0 end ) Draft, (case when [2] is not null then [2] else 0 end ) Pending , (case when [3] is not null then [3] else 0 end ) Live, (case when [4] is not null then [4] else 0 end ) Paused , (case when [6] is not null then [6] else 0 end ) Rejected  from (
        select 1 ordr, count(ac.CampaignID) stats, evntHis.EventStatus , 'Video Ads' rectype 
		from AdCampaign ac
		inner join (SELECT tt.*
			FROM CampaignEventHistory tt
			INNER JOIN
				(SELECT CampaignID, MAX(EventDateTime) AS MaxDateTime
				FROM CampaignEventHistory
				GROUP BY CampaignID) lastEvent 
			ON tt.CampaignID = lastEvent.CampaignID AND tt.EventDateTime = lastEvent.MaxDateTime ) evntHis
			on ac.CampaignID = evntHis.CampaignID
		where ac.Type = 1
		group by evntHis.EventStatus
		union
		 select 2 ordr, count(ac.CampaignID) stats, evntHis.EventStatus , 'Display Ads' rectype 
		from AdCampaign ac
		inner join (SELECT tt.*
			FROM CampaignEventHistory tt
			INNER JOIN
				(SELECT CampaignID, MAX(EventDateTime) AS MaxDateTime
				FROM CampaignEventHistory
				GROUP BY CampaignID) lastEvent 
			ON tt.CampaignID = lastEvent.CampaignID AND tt.EventDateTime = lastEvent.MaxDateTime ) evntHis
			on ac.CampaignID = evntHis.CampaignID
		where ac.Type = 4
		group by evntHis.EventStatus

		union
		
		SELECT 3 ordr, count(tt.SQID) stats, tt.EventStatus, 'Surveys' rectype 
			FROM CampaignEventHistory tt
			INNER JOIN
				(SELECT SQID, MAX(EventDateTime) AS MaxDateTime
				FROM CampaignEventHistory
				GROUP BY SQID) lastEvent 
			ON tt.SQID = lastEvent.SQID AND tt.EventDateTime = lastEvent.MaxDateTime 
			group by tt.EventStatus 

			union
		
		SELECT  4 ordr, count(tt.PQID) stats, tt.EventStatus, 'Polls' rectype 
			FROM CampaignEventHistory tt
			INNER JOIN
				(SELECT PQID, MAX(EventDateTime) AS MaxDateTime
				FROM CampaignEventHistory
				GROUP BY PQID) lastEvent 
			ON tt.PQID = lastEvent.PQID AND tt.EventDateTime = lastEvent.MaxDateTime 
			group by tt.EventStatus 

			union

		select 5 ordr,  count(case when ac.CouponListingMode = 2 then ac.CouponId else null end) stats, evntHis.EventStatus , 'Paid Deals' rectype 
		from Coupon ac
		inner join (SELECT tt.*
			FROM CampaignEventHistory tt
			inner JOIN
				(SELECT CouponId, MAX(EventDateTime) AS MaxDateTime
				FROM CampaignEventHistory
				GROUP BY CouponId) lastEvent 
			ON tt.CouponId = lastEvent.CouponId AND tt.EventDateTime = lastEvent.MaxDateTime ) evntHis
					on ac.CouponId = evntHis.CouponId
		
		group by evntHis.EventStatus
	union
	select 6 ordr, count(case when ac.CouponListingMode = 1 then ac.CouponId else null end) stats , evntHis.EventStatus , 'Free Deals' rectype 
		from Coupon ac
		inner join (SELECT tt.*
			FROM CampaignEventHistory tt
			INNER JOIN
				(SELECT CouponId, MAX(EventDateTime) AS MaxDateTime
				FROM CampaignEventHistory
				GROUP BY CouponId) lastEvent 
			ON tt.CouponId = lastEvent.CouponId AND tt.EventDateTime = lastEvent.MaxDateTime ) evntHis
					on ac.CouponId = evntHis.CouponId
			
			group by evntHis.EventStatus


		) as res
		pivot
	(
	  sum(stats)
		for EventStatus in ([1] , [2], [3] , [4], [5] , [6])
	) piv
			--update Coupon
			--set CouponListingMode = 2
			--where CouponId = 10145
 

END


--EXEC getCampaignsByStatus