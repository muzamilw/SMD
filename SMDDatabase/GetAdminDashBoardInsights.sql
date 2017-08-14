GO
/****** Object:  StoredProcedure [dbo].[GetAdminDashBoardInsights]    Script Date: 10/12/2016 11:20:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetAdminDashBoardInsights]

as
Begin
select ordr, rectype, pMonth, us, uk, [pk] ca, au, ae
from 
(
  select ordr, stats, countrycode, rectype, pMonth
  from ( 
		select 0 as ordr, count(u.id) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, u.LastLoginTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Users who logged in' rectype
		from [SMDv2].[dbo].[AspNetUsers] u
		inner join Company co on u.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where u.LastLoginTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, u.LastLoginTime), 0) ,  c.CountryCode

		union

		select 1 as ordr,  count(u.id) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, u.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'New Users who registered' rectype
		from [SMDv2].[dbo].[AspNetUsers] u
		inner join Company co on u.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where u.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, u.CreatedDateTime), 0) ,  c.CountryCode
		union

		select 2 ordr, count(distinct(CompanyId)) stats, CountryCode , pMonth, 'Advertisers who created a new campaign' rectype  from  (
		select sq.sqid, sq.CompanyId as CompanyId , c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth
		from SurveyQuestion sq
		inner join 	(SELECT SQID, MIN(EventDateTime) AS MinDateTime
				FROM CampaignEventHistory
				where EventStatusId = 3
				GROUP BY SQID)  evntHis
			on sq.SQID = evntHis.SQID
			inner join Country c on sq.CountryID = c.CountryID
		where evntHis.MinDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		union
	
		select pq.pqid, (pq.CompanyId) as CompanyId, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth
		from ProfileQuestion pq
		inner join 	(SELECT PQID, MIN(EventDateTime) AS MinDateTime
				FROM CampaignEventHistory
				where EventStatusId = 3
				GROUP BY PQID)  evntHis
			on pq.PQID = evntHis.PQID
			inner join Country c on pq.CountryID = c.CountryID
			where evntHis.MinDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 

	union 
	select ac.campaignId,  (ac.CompanyId) as CompanyId, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth
		from AdCampaign ac
		inner join 	(SELECT CampaignID, MIN(EventDateTime) AS MinDateTime
				FROM CampaignEventHistory
				where EventStatusId = 3
				GROUP BY CampaignID)  evntHis
			on ac.CampaignID = evntHis.CampaignID
			inner join Company co on ac.CompanyId = co.CompanyId
			inner join Country c on co.CountryId = c.CountryID
		where evntHis.MinDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		
		union 
		select cpn.CouponId,  (cpn.CompanyId) as CompanyId, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth
		from Coupon cpn
		inner join 	(SELECT CouponId, MIN(EventDateTime) AS MinDateTime
				FROM CampaignEventHistory
				where EventStatusId = 3
				GROUP BY CouponId)  evntHis
			on cpn.CouponId = evntHis.CouponId
			inner join Company co on cpn.CompanyId = co.CompanyId
			inner join Country c on co.CountryId = c.CountryID
		where evntHis.MinDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		

		) as rec 
		group by pMonth ,  CountryCode
		union 
		select 3 ordr, count(ac.CampaignID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'New Video Ad campaigns' rectype
		from AdCampaign ac
		inner join 	(SELECT CampaignID, MIN(EventDateTime) AS MinDateTime
				FROM CampaignEventHistory
				where EventStatusId = 3
				GROUP BY CampaignID)  evntHis
			on ac.CampaignID = evntHis.CampaignID
			inner join Company co on ac.CompanyId = co.CompanyId
			inner join Country c on co.CountryId = c.CountryID
		where ac.Type = 1 and evntHis.MinDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0) ,  c.CountryCode

		union
		select 4 ordr, count(ac.CampaignID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'New Display Ad campaigns' rectype
		from AdCampaign ac
		inner join 	(SELECT CampaignID, MIN(EventDateTime) AS MinDateTime
				FROM CampaignEventHistory
				where EventStatusId = 3
				GROUP BY CampaignID)  evntHis
			on ac.CampaignID = evntHis.CampaignID
			inner join Company co on ac.CompanyId = co.CompanyId
			inner join Country c on co.CountryId = c.CountryID
		where ac.Type = 4 and evntHis.MinDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0) ,  c.CountryCode
		
		union

		select 5 ordr, count(pq.PQID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'New Survey campaigns' rectype
		from ProfileQuestion pq
		inner join 	(SELECT PQID, MIN(EventDateTime) AS MinDateTime
				FROM CampaignEventHistory
				where EventStatusId = 3
				GROUP BY PQID)  evntHis
			on pq.PQID = evntHis.PQID
			inner join Country c on pq.CountryID = c.CountryID
			where evntHis.MinDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0) ,  c.CountryCode

		union

		select 6 ordr, count(sq.SQID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'New Poll campaigns' rectype
		from SurveyQuestion sq
		inner join 	(SELECT SQID, MIN(EventDateTime) AS MinDateTime
				FROM CampaignEventHistory
				where EventStatusId = 3
				GROUP BY SQID)  evntHis
			on sq.SQID = evntHis.SQID
			inner join Country c on sq.CountryID = c.CountryID
		where evntHis.MinDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, evntHis.MinDateTime), 0) ,  c.CountryCode
		union
		select 9 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Video Quiz Ad clicks' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 1 AND acr.ResponseType = 3
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode
		union 

		select 10 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Game quiz Ad clicks' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 4 AND acr.ResponseType = 3
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode

		union
		select 11 as ordr,   count(pqua.PQUAnswerID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Survey' rectype
		from ProfileQuestionUserAnswer pqua
		inner join Company co on pqua.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where pqua.AnswerDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))  AND pqua.ResponseType = 3
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0) ,  c.CountryCode

		union
		select 12 as ordr,   count(sqr.SQResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Polls' rectype
		from SurveyQuestionResponse sqr
		inner join Company co on sqr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where sqr.ResoponseDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND sqr.ResponseType = 3
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0) ,  c.CountryCode

		union 
		
		select 15 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Video ads skipped' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 1 AND acr.ResponseType = 4
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode
		union 

		select 16 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Games skipped' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 4 AND acr.ResponseType = 4
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode

		union

		select 17 as ordr,   count(pqua.PQUAnswerID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Surveys' rectype
		from ProfileQuestionUserAnswer pqua
		inner join Company co on pqua.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where pqua.AnswerDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))  AND pqua.ResponseType = 4
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0) ,  c.CountryCode

		union
		select 18 as ordr,   count(sqr.SQResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Polls' rectype
		from SurveyQuestionResponse sqr
		inner join Company co on sqr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where sqr.ResoponseDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND sqr.ResponseType = 4
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0) ,  c.CountryCode

		union
		select 21 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Videos referred to landing pages' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 1 AND acr.ResponseType = 2
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode

		union
		select 22 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Games referred to landing pages' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 4 AND acr.ResponseType = 2
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode

		union
		select 23 as ordr,   count(upc.CouponPurchaseId) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.PurchaseDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Deals referred to landing pages' rectype
		from UserPurchasedCoupon upc
		inner join Coupon cpn on cpn.CouponId = upc.CouponId
		inner join Company co on cpn.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where upc.PurchaseDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND upc.ResponseType = 2
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.PurchaseDateTime), 0) ,  c.CountryCode

		union
		select 26 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Video Quiz Ad clicks' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 1 AND acr.ResponseType = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode
		union 

		select 27 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Game quiz Ad clicks' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 4 AND acr.ResponseType = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode

		union
		select 28 as ordr,   count(pqua.PQUAnswerID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Survey' rectype
		from ProfileQuestionUserAnswer pqua
		inner join Company co on pqua.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where pqua.AnswerDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))  AND pqua.ResponseType = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0) ,  c.CountryCode

		union
		select 29 as ordr,   count(sqr.SQResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Polls' rectype
		from SurveyQuestionResponse sqr
		inner join Company co on sqr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where sqr.ResoponseDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND sqr.ResponseType = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0) ,  c.CountryCode

		union
		select 30 as ordr,   count(upc.CouponPurchaseId) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.PurchaseDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Deals viewed (Opened)' rectype
		from UserPurchasedCoupon upc
		inner join Coupon cpn on cpn.CouponId = upc.CouponId
		inner join Company co on cpn.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where upc.PurchaseDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND upc.ResponseType = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.PurchaseDateTime), 0) ,  c.CountryCode


		
		union 
		select 33 as ordr,  sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Video Ads Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join AdCampaign ac on ac.CampaignID = t.AdCampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 1 AND a.AccountType = 1 AND ac.Type = 1 and a.CompanyId = 466
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode

		union 
		select 34 as ordr,  sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Display Ads Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join AdCampaign ac on ac.CampaignID = t.AdCampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 1 AND  a.AccountType = 1 AND ac.Type = 4 and a.CompanyId = 466
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode
		union

		select 35 as ordr,   sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Surveys Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join ProfileQuestion pq on pq.PQID = t.PQID
		inner join Country c on pq.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 7 AND  a.AccountType = 1 and a.CompanyId = 466
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode

		union 
		select 36 as ordr,   sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Survay Cards Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join SurveyQuestion sq on sq.SQID = t.SQID
		inner join Country c on sq.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 2 AND  a.AccountType = 1 and a.CompanyId = 466
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode

		
		union  


		 
		select 37 as ordr,   sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Deals revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join Coupon cpn on cpn.CouponId = t.CouponId
		inner join Company co on cpn.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 8 AND  a.AccountType = 1 and a.CompanyId = 466
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode

		union
		select 39 as ordr, CASE WHEN sum(t.CreditAmount) > 0 THEN sum(t.CreditAmount) ELSE 0 END stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'All Revenue (income from stripe)' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join Company co on a.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND  a.AccountType = 1 and a.CompanyId = 466
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode
		union  
		select 41 as ordr,  sum(t.DebitAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Payout via PayPal' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join Company co on a.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 10 AND  a.AccountType = 2 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode
		

	) as rec
				
)  src

pivot
(
  max(stats)
    for countrycode in ([us], [uk],[pk], [au], [ae]) 
) piv

--order by rectype
order by ordr
END

--EXEC GetAdminDashBoardInsights

