﻿
GO
/****** Object:  StoredProcedure [dbo].[GetAdminDashBoardInsights]    Script Date: 9/20/2016 12:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetAdminDashBoardInsights]

as
Begin
select rectype, pMonth, us, uk, ca, au, ae
from 
(
  select ordr, stats, countrycode, rectype, pMonth
  from ( 
		select 1 as ordr, count(u.id) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, u.LastLoginTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Active App Users' rectype
		from [SMDv2].[dbo].[AspNetUsers] u
		inner join Company co on u.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where u.LastLoginTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, u.LastLoginTime), 0) ,  c.CountryCode

		union

		select 2 as ordr,  count(u.id) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, u.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'New App Users' rectype
		from [SMDv2].[dbo].[AspNetUsers] u
		inner join Company co on u.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where u.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, u.CreatedDateTime), 0) ,  c.CountryCode
		union

		select 3 as ordr,  count(ac.CampaignID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, ac.ApprovalDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Video Campaigns' rectype
		from AdCampaign ac
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where ac.ApprovalDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))  AND ac.Type = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, ac.ApprovalDateTime), 0) ,  c.CountryCode
		union 

		select 4 as ordr,  count(ac.CampaignID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, ac.ApprovalDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Game Campaigns' rectype
		from AdCampaign ac
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where ac.ApprovalDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))  AND ac.Type = 4
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, ac.ApprovalDateTime), 0) ,  c.CountryCode
		union
		select 5 as ordr,  count(coup.CouponId) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, coup.ApprovalDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Deals Campaigns' rectype
		from Coupon coup
		inner join Company co on coup.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where coup.ApprovalDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, coup.ApprovalDateTime), 0) ,  c.CountryCode

		union 
		select 6 as ordr,   count(sq.SQID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, sq.ApprovalDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Survey card Campaigns' rectype
		from SurveyQuestion sq
		inner join Country c on sq.CountryId = c.CountryID
		where sq.ApprovalDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, sq.ApprovalDate), 0) ,  c.CountryCode

		union
		select 7 as ordr,   count(pq.PQID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, pq.ApprovalDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Profile Question Answered' rectype
		from ProfileQuestion pq
		inner join Country c on pq.CountryId = c.CountryID
		where pq.ApprovalDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, pq.ApprovalDate), 0) ,  c.CountryCode 

		union 
		select 8 as ordr,   count(ac.CampaignID) stats, c.CountryCode , 'prev' pMonth,'Video Campaigns' rectype
		from AdCampaign ac
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where ac.EndDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.StartDateTime < =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1)) AND ac.Type = 1
		group by c.CountryCode
		union
		select 8 as ordr,  count(ac.CampaignID) stats, c.CountryCode , 'current' pMonth,'Video Campaigns' rectype
		from AdCampaign ac
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where ac.EndDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1)) AND ac.StartDateTime < =  GETDATE() AND ac.Type = 1
		group by c.CountryCode

		union

		select 9 as ordr,   count(ac.CampaignID) stats, c.CountryCode , 'prev' pMonth,'Game Campaigns' rectype
		from AdCampaign ac
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where ac.EndDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.StartDateTime < =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1)) AND ac.Type = 4
		group by c.CountryCode
		union
		select 9 as ordr,  count(ac.CampaignID) stats, c.CountryCode , 'current' pMonth,'Game Campaigns' rectype
		from AdCampaign ac
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where ac.EndDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1)) AND ac.StartDateTime < =  GETDATE() AND ac.Type = 4
		group by c.CountryCode
				
		union 
		select 10 as ordr,   count(cc.CouponId) stats, c.CountryCode , 'prev' pMonth,'Deals Campaigns' rectype
		from Coupon cc
		inner join Company co on cc.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where cc.CouponEndDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND cc.ApprovalDateTime < =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1)) 
		group by c.CountryCode
		union
		select 10 as ordr,  count(cc.CouponId) stats, c.CountryCode , 'current' pMonth,'Deals Campaigns' rectype
		from Coupon cc
		inner join Company co on cc.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where cc.CouponEndDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1)) AND cc.ApprovalDateTime < =  GETDATE()
		group by c.CountryCode
	
		union 
		select 11 as ordr,   count(sq.SQID) stats, c.CountryCode , 'prev' pMonth,'Survey card Campaigns' rectype
		from SurveyQuestion sq
		inner join Country c on sq.CountryId = c.CountryID
		where sq.EndDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND sq.StartDate < =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1)) 
		group by c.CountryCode
		union
		select 11 as ordr,   count(sq.SQID) stats, c.CountryCode , 'current' pMonth,'Survey card Campaigns' rectype
		from SurveyQuestion sq
		inner join Country c on sq.CountryId = c.CountryID
		where sq.EndDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1)) AND sq.StartDate < =  GETDATE()
		group by c.CountryCode

		union 
		select 12 as ordr,   count(pq.PQID) stats, c.CountryCode , 'prev' pMonth,'Survey question Campaigns' rectype
		from ProfileQuestion pq
		inner join Country c on pq.CountryId = c.CountryID
		where pq.EndDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND pq.StartDate < =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1)) 
		group by c.CountryCode
		union
		select 12 as ordr,    count(pq.PQID) stats, c.CountryCode , 'current' pMonth,'Survey question Campaigns' rectype
		from ProfileQuestion pq
		inner join Country c on pq.CountryId = c.CountryID
		where pq.EndDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-1)) AND pq.StartDate < =  GETDATE()
		group by c.CountryCode

		union
		select 13 as ordr,   count(ucv.CouponId) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, ucv.ViewDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Deals viewed' rectype
		from UserCouponView ucv
		inner join Coupon cpn on cpn.CouponId = ucv.CouponId
		inner join Company co on cpn.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where ucv.ViewDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, ucv.ViewDateTime), 0) ,  c.CountryCode
		

		union
		select 14 as ordr,   count(upc.CouponPurchaseId) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.PurchaseDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Deals referred to landing pages' rectype
		from UserPurchasedCoupon upc
		inner join Coupon cpn on cpn.CouponId = upc.CouponId
		inner join Company co on cpn.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where upc.PurchaseDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND upc.ResponseType = 3
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.PurchaseDateTime), 0) ,  c.CountryCode

		union 
		select 15 as ordr,  count(upc.CouponPurchaseId) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.PurchaseDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Deals saved' rectype
		from UserPurchasedCoupon upc
		inner join Coupon cpn on cpn.CouponId = upc.CouponId
		inner join Company co on cpn.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where upc.PurchaseDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.PurchaseDateTime), 0) ,  c.CountryCode

		union 

		select 16 as ordr,  count(upc.RedemptionDateTime) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.RedemptionDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Deals Redeemed' rectype
		from UserPurchasedCoupon upc
		inner join Coupon cpn on cpn.CouponId = upc.CouponId
		inner join Company co on cpn.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where upc.RedemptionDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, upc.RedemptionDateTime), 0) ,  c.CountryCode

		union
		select 17 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Videos referred to landing pages' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on acr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 1 AND acr.ResponseType = 3
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode
		union 
		
		select 18 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Video Quiz Ad clicks' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on acr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 1 AND acr.ResponseType = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode
		union 

		select 19 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Game quiz Ad clicks' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on acr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 4 AND acr.ResponseType = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode

		union

		select 20 as ordr,   count(pqua.PQUAnswerID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Survey Question Answered' rectype
		from ProfileQuestionUserAnswer pqua
		inner join Company co on pqua.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where pqua.AnswerDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))  AND pqua.ResponseType = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0) ,  c.CountryCode

		union
		select 21 as ordr,   count(sqr.SQResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Survey cards answered' rectype
		from SurveyQuestionResponse sqr
		inner join Company co on sqr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where sqr.ResoponseDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND sqr.ResponseType = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0) ,  c.CountryCode

		
		union 
		
		select 22 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Video ads skipped' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on acr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 1 AND acr.ResponseType = 2
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode
		union 

		select 23 as ordr,   count(acr.ResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Games skipped' rectype
		from AdCampaignResponse acr
		inner join AdCampaign ac on ac.CampaignID = acr.CampaignID
		inner join Company co on acr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where acr.CreatedDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND ac.Type = 4 AND acr.ResponseType = 2
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, acr.CreatedDateTime), 0) ,  c.CountryCode

		union

		select 24 as ordr,   count(pqua.PQUAnswerID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Survey question skipped' rectype
		from ProfileQuestionUserAnswer pqua
		inner join Company co on pqua.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where pqua.AnswerDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2))  AND pqua.ResponseType = 2
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, pqua.AnswerDateTime), 0) ,  c.CountryCode

		union
		select 25 as ordr,   count(sqr.SQResponseID) stats, c.CountryCode , (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Survey cards skipped' rectype
		from SurveyQuestionResponse sqr
		inner join Company co on sqr.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where sqr.ResoponseDateTime > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND sqr.ResponseType = 2
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, sqr.ResoponseDateTime), 0) ,  c.CountryCode

		union 
		select 26 as ordr,  sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Video Ads Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join AdCampaign ac on ac.CampaignID = t.AdCampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 1 AND a.AccountType = 1 AND ac.Type = 1
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode

		union 
		select 27 as ordr,  sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Game Ads Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join AdCampaign ac on ac.CampaignID = t.AdCampaignID
		inner join Company co on ac.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 1 AND  a.AccountType = 1 AND ac.Type = 4
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode
		
		--union 
		--select 28 as ordr,   sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Deal Listing revenue' rectype
		--from [Transaction] t
		--inner join Account a on a.AccountId = t.AccountID
		--inner join Coupon cpn on cpn.CouponId = t.CouponId
		--inner join Company co on cpn.CompanyId = co.CompanyId
		--inner join Country c on co.CountryId = c.CountryID
		--where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 8 AND  a.AccountType = 1 
		--group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode

		union 
		select 29 as ordr,   sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Survay Cards Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join SurveyQuestion sq on sq.SQID = t.SQID
		inner join Country c on sq.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 2 AND  a.AccountType = 1 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode

		union

		select 30 as ordr,   sum(t.CreditAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Profile Questions Revenue' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join ProfileQuestion pq on pq.PQID = t.PQID
		inner join Country c on pq.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 7 AND  a.AccountType = 1 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode
		union  
		select 31 as ordr, CASE WHEN sum(t.CreditAmount) > 0 THEN sum(t.CreditAmount) ELSE 0 END stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'All Revenue (income from stripe)' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join Company co on a.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND  a.AccountType = 1 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode
		union  
		select 32 as ordr,  sum(t.DebitAmount) stats, c.CountryCode, (case when month( DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0)) = month(getdate()) then 'current' else 'prev' end) pMonth,'Payout via PayPal' rectype
		from [Transaction] t
		inner join Account a on a.AccountId = t.AccountID
		inner join Company co on a.CompanyId = co.CompanyId
		inner join Country c on co.CountryId = c.CountryID
		where t.TransactionDate > =  DATEADD(DAY,1,EOMONTH(CURRENT_TIMESTAMP,-2)) AND t.Type = 10 AND  a.AccountType = 4 
		group by DATEADD(MONTH,DATEDIFF(MONTH, 0, t.TransactionDate), 0) ,  c.CountryCode


	) as rec
				
)  src

pivot
(
  max(stats)
    for countrycode in ([us], [uk],[ca], [au], [ae]) 
) piv

--order by rectype
order by ordr
END

--EXEC GetAdminDashBoardInsights
