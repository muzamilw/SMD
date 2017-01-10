USE [SMDv2]
GO
/****** Object:  UserDefinedFunction [dbo].[GetUserProfileQuestions]    Script Date: 1/10/2017 12:54:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER FUNCTION [dbo].[GetUserProfileQuestions] 
(	

--   select weightage,* from [GetUserProfileQuestions]('b8a3884f-73f3-41ec-9926-293ea919a5e1', 214,'cccccc4ads','t') x order by x.weightage asc
	-- Add the parameters for the function here
	@UserID uniqueidentifier = '', 
	@countryId int = 0, @cash4adsSocialHandle as nvarchar(128), @cash4adsSocialHandleType as nvarchar(128)
)
RETURNS TABLE 
AS
RETURN 
(
  -- Add the SELECT statement with parameter references here
  with profilequestions(pqid, question, linkedquestion1id, linkedquestion2id,
linkedquestion3id, linkedquestion4id, linkedquestion5id, linkedquestion6id,
linkedquestion7id, linkedquestion8id,
linkedquestion9id, linkedquestion10id, linkedquestion11id, linkedquestion12id,
linkedquestion13id, linkedquestion14id, linkedquestion15id, linkedquestion16id,
linkedquestion17id, linkedquestion18id, linkedquestion19id, linkedquestion20id,
linkedquestion21id, linkedquestion22id, linkedquestion23id, linkedquestion24id,
linkedquestion25id, linkedquestion26id, linkedquestion27id, linkedquestion28id,
linkedquestion29id, linkedquestion30id, linkedquestion31id, linkedquestion32id,
linkedquestion33id, linkedquestion34id, linkedquestion35id, linkedquestion36id,
 refreshtime,
countryid, status, type, rowNumber, weightage, ProfileGroupID, SocialHandle, SocialHandleType)
as
( 
	select pqo.pqid, pqo.question, pql.LinkedQuestion1ID, pql.LinkedQuestion2ID,pql.PQA1LinkedQ3,pql.PQA1LinkedQ4,
	pql.PQA1LinkedQ5,pql.PQA1LinkedQ6,pql2.LinkedQuestion1ID, pql2.LinkedQuestion2ID,pql2.PQA1LinkedQ3,pql2.PQA1LinkedQ4,
	pql2.PQA1LinkedQ5,pql2.PQA1LinkedQ6, pql3.LinkedQuestion1ID, pql3.LinkedQuestion2ID,pql3.PQA1LinkedQ3,pql3.PQA1LinkedQ4,
	pql3.PQA1LinkedQ5,pql3.PQA1LinkedQ6,pql4.LinkedQuestion1ID, pql4.LinkedQuestion2ID,pql4.PQA1LinkedQ3,pql4.PQA1LinkedQ4,
	pql4.PQA1LinkedQ5,pql4.PQA1LinkedQ6,pql5.LinkedQuestion1ID, pql5.LinkedQuestion2ID,pql5.PQA1LinkedQ3,pql5.PQA1LinkedQ4,
	pql5.PQA1LinkedQ5,pql5.PQA1LinkedQ6,pql6.LinkedQuestion1ID, pql6.LinkedQuestion2ID,pql6.PQA1LinkedQ3,pql6.PQA1LinkedQ4,
	pql6.PQA1LinkedQ5,pql6.PQA1LinkedQ6,pqo.refreshtime, pqo.CountryID, pqo.status, pqo.type,
	((row_number() over (order by pqo.pqid)) + isNUll(pqo.Priority,0)) rowNumber,
	--(((row_number() over (order by pqo.ProfileGroupID, [priority]) * 100 ) + 30) ) Weightage, 
	(((row_number() over (order by (case when isnull(pqo.companyid,0) = 0 then 10 else 1 end)) * 100) + 30) ) Weightage,
	ProfileGroupID,
	case when c.TwitterHandle is not null or c.TwitterHandle <> '' then c.TwitterHandle 
	when c.FacebookHandle is not null or c.FacebookHandle <> '' then c.FacebookHandle 
	when c.InstagramHandle is not null or c.InstagramHandle <> '' then c.InstagramHandle 
	when c.PinterestHandle is not null or c.PinterestHandle <> '' then c.PinterestHandle 
	else @cash4adsSocialHandle

	end as SocialHandle,
	case when c.TwitterHandle is not null or c.TwitterHandle <> '' then 't'
	when c.FacebookHandle is not null or c.FacebookHandle <> '' then 'f'
	when c.InstagramHandle is not null or c.InstagramHandle <> '' then 'i'
	when c.PinterestHandle is not null or c.PinterestHandle <> '' then 'p'
	else @cash4adsSocialHandleType

	end as SocialHandleType
	from ProfileQuestion pqo
	left outer join Company c on pqo.companyid = c.CompanyId
	outer apply
	(
		select top 1 pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc
		order by pq.PQID
	) as pql
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc
		order by pq.PQID
		OFFSET 1 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql2
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--
		order by pq.PQID
		OFFSET 2 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql3
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--order by pq.PQID
		OFFSET 3 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql4
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--
		order by pq.PQID
		OFFSET 4 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql5
	outer apply
	(
		select pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,
		pqa.LinkedQuestion3ID as PQA1LinkedQ3,
		pqa.LinkedQuestion4ID as PQA1LinkedQ4,
		pqa.LinkedQuestion5ID as PQA1LinkedQ5,
		pqa.LinkedQuestion6ID as PQA1LinkedQ6
		from ProfileQuestionAnswer pqa
		join ProfileQuestion pq on pq.PQID = pqa.PqID
		and pq.PQID  = pqo.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--
		order by pq.PQID
		OFFSET 5 Rows
		FETCH NEXT 1 Rows ONLY
	) as pql6
	where pqo.status = 3
	 --pqo.HasLinkedQuestions = 1 and pqo.Status = 1
	--UNION ALL
	--select pq.pqid, pq.question, null, null,
	--null, null, null, null,
	--null, null,
	--null, null, null, null, 
	--null, null, null, null, null, null, null, null, null, null, null, null, 
	--null, null, null, null, null, null, null, null, null, null, null, null, 
	--pq.RefreshTime, pq.CountryID, pq.status, pq.type,
	--pqs.rowNumber + isnull(pq.Priority, 0),
	--pqs.weightage + isnull(pq.Priority, 0) Weightage
	--from profilequestion pq
	--join profilequestions pqs on 
	--(pqs.linkedquestion1id = pq.PQID or pqs.linkedquestion2id = pq.PQID or pqs.linkedquestion3id = pq.PQID
	--or pqs.linkedquestion4id = pq.PQID or pqs.linkedquestion5id = pq.PQID or pqs.linkedquestion6id = pq.PQID
	--or pqs.linkedquestion7id = pq.PQID or pqs.linkedquestion8id = pq.PQID or pqs.linkedquestion9id = pq.PQID
	--or pqs.linkedquestion11id = pq.PQID or pqs.linkedquestion12id = pq.PQID or pqs.linkedquestion13id = pq.PQID
	--or pqs.linkedquestion14id = pq.PQID or pqs.linkedquestion15id = pq.PQID or pqs.linkedquestion16id = pq.PQID
	--or pqs.linkedquestion17id = pq.PQID or pqs.linkedquestion18id = pq.PQID or pqs.linkedquestion19id = pq.PQID
	--or pqs.linkedquestion20id = pq.PQID or pqs.linkedquestion21id = pq.PQID or pqs.linkedquestion22id = pq.PQID
	--or pqs.linkedquestion23id = pq.PQID or pqs.linkedquestion24id = pq.PQID or pqs.linkedquestion25id = pq.PQID
	--or pqs.linkedquestion26id = pq.PQID or pqs.linkedquestion27id = pq.PQID or pqs.linkedquestion28id = pq.PQID
	--or pqs.linkedquestion29id = pq.PQID or pqs.linkedquestion30id = pq.PQID or pqs.linkedquestion31id = pq.PQID
	--or pqs.linkedquestion32id = pq.PQID or pqs.linkedquestion33id = pq.PQID or pqs.linkedquestion34id = pq.PQID
	--or pqs.linkedquestion35id = pq.PQID or pqs.linkedquestion36id = pq.PQID	) 
	--and pqs.pqid = pq.PQID

	)
		


select pq.*,
pqa1.PQAnswerID as PQAnswerID1, 
pqa1.AnswerString as PQAnswer1, 
pqa1.LinkedQuestion1ID as PQA1LinkedQ1,
pqa1.LinkedQuestion2ID as PQA1LinkedQ2,
pqa1.LinkedQuestion3ID as PQA1LinkedQ3,
pqa1.LinkedQuestion4ID as PQA1LinkedQ4,
pqa1.LinkedQuestion5ID as PQA1LinkedQ5,
pqa1.LinkedQuestion6ID as PQA1LinkedQ6,
pqa1.type as PQA1Type, 
pqa1.SortOrder as PQA1SortOrder,
CASE
	WHEN pqa1.ImagePath is null or pqa1.ImagePath = ''
	THEN pqa1.ImagePath
	WHEN pqa1.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa1.ImagePath
END as PQA1ImagePath,
pqa2.PQAnswerID as PQAnswerID2, 
pqa2.AnswerString as PQAnswer2, 
pqa2.LinkedQuestion1ID as PQA2LinkedQ1, 
pqa2.LinkedQuestion2ID as PQA2LinkedQ2,
pqa2.LinkedQuestion3ID as PQA2LinkedQ3,
pqa2.LinkedQuestion4ID as PQA2LinkedQ4,
pqa2.LinkedQuestion5ID as PQA2LinkedQ5,
pqa2.LinkedQuestion6ID as PQA2LinkedQ6,
pqa2.type as PQA2Type,
 pqa2.SortOrder as PQA2SortOrder,
CASE
	WHEN pqa2.ImagePath is null or pqa2.ImagePath = ''
	THEN pqa2.ImagePath
	WHEN pqa2.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa2.ImagePath
END as PQA2ImagePath,
pqa3.PQAnswerID as PQAnswerID3, 
pqa3.AnswerString as PQAnswer3, 
pqa3.LinkedQuestion1ID as PQA3LinkedQ1, 
pqa3.LinkedQuestion2ID as PQA3LinkedQ2,
pqa3.LinkedQuestion3ID as PQA3LinkedQ3,
pqa3.LinkedQuestion4ID as PQA3LinkedQ4,
pqa3.LinkedQuestion5ID as PQA3LinkedQ5,
pqa3.LinkedQuestion6ID as PQA3LinkedQ6,
pqa3.type as PQA3Type, 
pqa3.SortOrder as PQA3SortOrder,
CASE
	WHEN pqa3.ImagePath is null or pqa3.ImagePath = ''
	THEN pqa3.ImagePath
	WHEN pqa3.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa3.ImagePath
END as PQA3ImagePath,
pqa4.PQAnswerID as PQAnswerID4, 
pqa4.AnswerString as PQAnswer4, 
pqa4.LinkedQuestion1ID as PQA4LinkedQ1, 
pqa4.LinkedQuestion2ID as PQA4LinkedQ2,
pqa4.LinkedQuestion3ID as PQA4LinkedQ3,
pqa4.LinkedQuestion4ID as PQA4LinkedQ4,
pqa4.LinkedQuestion5ID as PQA4LinkedQ5,
pqa4.LinkedQuestion6ID as PQA4LinkedQ6,
pqa4.type as PQA4Type,
 pqa4.SortOrder as PQA4SortOrder,
CASE
	WHEN pqa4.ImagePath is null or pqa4.ImagePath = ''
	THEN pqa4.ImagePath
	WHEN pqa4.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa4.ImagePath
END as PQA4ImagePath,
pqa5.PQAnswerID as PQAnswerID5, 
pqa5.AnswerString as PQAnswer5, 
pqa5.LinkedQuestion1ID as PQA5LinkedQ1,
pqa5.LinkedQuestion2ID as PQA5LinkedQ2,
pqa5.LinkedQuestion3ID as PQA5LinkedQ3,
pqa5.LinkedQuestion4ID as PQA5LinkedQ4,
pqa5.LinkedQuestion5ID as PQA5LinkedQ5,
pqa5.LinkedQuestion6ID as PQA5LinkedQ6,
pqa5.type as PQA5Type, 
pqa5.SortOrder as PQA5SortOrder,
CASE
	WHEN pqa5.ImagePath is null or pqa5.ImagePath = ''
	THEN pqa5.ImagePath
	WHEN pqa5.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa5.ImagePath
END as PQA5ImagePath,
pqa6.PQAnswerID as PQAnswerID6, 
pqa6.AnswerString as PQAnswer6,
pqa6.LinkedQuestion1ID as PQA6LinkedQ1, 
pqa6.LinkedQuestion2ID as PQA6LinkedQ2,
pqa6.LinkedQuestion3ID as PQA6LinkedQ3,
pqa6.LinkedQuestion4ID as PQA6LinkedQ4,
pqa6.LinkedQuestion5ID as PQA6LinkedQ5,
pqa6.LinkedQuestion6ID as PQA6LinkedQ6, 
pqa6.type as PQA6Type,
 pqa6.SortOrder as PQA6SortOrder,
CASE
	WHEN pqa6.ImagePath is null or pqa6.ImagePath = ''
	THEN pqa6.ImagePath
	WHEN pqa6.ImagePath is not null
	THEN 'http://manage.cash4ads.com/' + pqa6.ImagePath
END as PQA6ImagePath
from profilequestions pq

outer apply (
		select top 1 pqa.PQAnswerID, pqa.AnswerString
		, pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID,pqa.LinkedQuestion3ID,pqa.LinkedQuestion4ID,pqa.LinkedQuestion5ID,pqa.LinkedQuestion6ID,
		pqa.type, 
		pqa.ImagePath, pqa.SortOrder
		from ProfileQuestionAnswer pqa
		where pqa.PQID = pq.PQID and pqa.Status != 0
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--
		order by pqa.PQAnswerID
	) Pqa1
	outer apply (
		select pqa2.PQAnswerID, pqa2.AnswerString
		, pqa2.LinkedQuestion1ID, pqa2.LinkedQuestion2ID, pqa2.LinkedQuestion3ID,pqa2.LinkedQuestion4ID,pqa2.LinkedQuestion5ID,pqa2.LinkedQuestion6ID,
		pqa2.type, 
		pqa2.ImagePath, pqa2.SortOrder
		from ProfileQuestionAnswer pqa2
		where pqa2.PQID = pq.PQID  and pqa2.Status != 0
		--order by ISNULL( pqa2.SortOrder, 99999) asc--order by pqa2.SortOrder asc,pqa2.AnswerString asc--
		order by pqa2.PQAnswerID
		OFFSET 1 ROWS -- skip 1 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa2
	outer apply (
		select pqa3.PQAnswerID, pqa3.AnswerString
		, pqa3.LinkedQuestion1ID, pqa3.LinkedQuestion2ID, pqa3.LinkedQuestion3ID,pqa3.LinkedQuestion4ID,pqa3.LinkedQuestion5ID,pqa3.LinkedQuestion6ID,
		 pqa3.type, 
		pqa3.ImagePath, pqa3.SortOrder
		from ProfileQuestionAnswer pqa3
		where pqa3.PQID = pq.PQID  and pqa3.Status != 0
		--order by ISNULL( pqa3.SortOrder, 99999) asc--order by pqa3.SortOrder asc,pqa3.AnswerString asc--
		order by pqa3.PQAnswerID
		OFFSET 2 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa3
	outer apply (
		select pqa4.PQAnswerID, pqa4.AnswerString
		, pqa4.LinkedQuestion1ID, pqa4.LinkedQuestion2ID, pqa4.type, 
		pqa4.LinkedQuestion3ID,pqa4.LinkedQuestion4ID,pqa4.LinkedQuestion5ID,pqa4.LinkedQuestion6ID,
		pqa4.ImagePath, pqa4.SortOrder
		from ProfileQuestionAnswer pqa4
		where pqa4.PQID = pq.PQID  and pqa4.Status != 0
		--order by ISNULL( pqa4.SortOrder, 99999) asc--order by pqa4.SortOrder asc,pqa4.AnswerString asc--
		order by pqa4.PQAnswerID
		OFFSET 3 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa4
	outer apply (
		select pqa5.PQAnswerID, pqa5.AnswerString
		, pqa5.LinkedQuestion1ID, pqa5.LinkedQuestion2ID, pqa5.type, 
		pqa5.LinkedQuestion3ID,pqa5.LinkedQuestion4ID,pqa5.LinkedQuestion5ID,pqa5.LinkedQuestion6ID,
		pqa5.ImagePath, pqa5.SortOrder
		from ProfileQuestionAnswer pqa5
		where pqa5.PQID = pq.PQID  and pqa5.Status != 0
		--order by ISNULL( pqa5.SortOrder, 99999) asc--order by pqa5.SortOrder asc,pqa5.AnswerString asc--
		order by pqa5.PQAnswerID
		OFFSET 4 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa5
	outer apply (
		select pqa6.PQAnswerID, pqa6.AnswerString
		, pqa6.LinkedQuestion1ID, pqa6.LinkedQuestion2ID, pqa6.type, 
		pqa6.LinkedQuestion3ID,pqa6.LinkedQuestion4ID,pqa6.LinkedQuestion5ID,pqa6.LinkedQuestion6ID,
		pqa6.ImagePath, pqa6.SortOrder
		from ProfileQuestionAnswer pqa6
		where pqa6.PQID = pq.PQID  and pqa6.Status != 0
		--order by ISNULL( pqa6.SortOrder, 99999) asc--order by pqa6.SortOrder asc,pqa6.AnswerString asc--
		order by pqa6.PQAnswerID
		OFFSET 5 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa6
	where 
	(
		(((select count(*) from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pqu.ResponseType = 3 )=0)--and pq.CountryID = @countryId) = 0)
		  or 	 
		  ((select top 1 datediff(day, dateadd(month, pq.refreshtime,pqu.AnswerDateTime), getdate()) 
			from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pqu.ResponseType = 3 --and pq.CountryID = @countryId -- commented because no need for country
			order by pqu.pquanswerid) > 0 )	
		)
		and pq.status = 3
	)
	UNION ALL
	select pq.pqid, pq.question, null, null,
	null, null, null, null,
	null, null,
	null, null, null, null,  null, null,
	null, null, null, null,
	null, null,
	null, null, null, null,  null, null,
	null, null, null, null,
	null, null,
	null, null, null, null, 
	--null, null, null, null,  null, null,null, null, null, null,  null, null,
	--null, null, null, null,  null, null,null, null, null, null,  null, null,
	pq.refreshtime,
	 pq.countryid,
	 pq.Status,
	 pq.type,
	((select max(rownumber) + 1 from profilequestions))+(row_number() over (order by pq.pqid)) rowNumber,
	((select (((max(rowNumber) + 1) ) ) from profilequestions)+(row_number() over (order by pq.pqid))*100)+3 Weightage,
	ProfileGroupID,
	null as SocialHandle, null as SocialHandleType,

	pqa1.PQAnswerID as PQAnswerID1, 
	pqa1.AnswerString as PQAnswer1, 
	pqa1.LinkedQuestion1ID as PQA1LinkedQ1, 
	pqa1.LinkedQuestion2ID as PQA1LinkedQ2,
	pqa1.LinkedQuestion3ID as PQA1LinkedQ3,
	pqa1.LinkedQuestion4ID as PQA1LinkedQ4,
	pqa1.LinkedQuestion5ID as PQA1LinkedQ5,
	pqa1.LinkedQuestion6ID as PQA1LinkedQ6,
	pqa1.type as PQA1Type, 
	pqa1.SortOrder as PQA1SortOrder,
	CASE
		WHEN pqa1.ImagePath is null or pqa1.ImagePath = ''
		THEN pqa1.ImagePath
		WHEN pqa1.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa1.ImagePath
	END as PQA1ImagePath,
	pqa2.PQAnswerID as PQAnswerID2,
	 pqa2.AnswerString as PQAnswer2, 
	pqa2.LinkedQuestion1ID as PQA2LinkedQ1, 
	pqa2.LinkedQuestion2ID as PQA2LinkedQ2,
	pqa2.LinkedQuestion3ID as PQA2LinkedQ3,
	pqa2.LinkedQuestion4ID as PQA2LinkedQ4,
	pqa2.LinkedQuestion5ID as PQA2LinkedQ5,
	pqa2.LinkedQuestion6ID as PQA2LinkedQ6,
	pqa2.type as PQA2Type,
	 pqa2.SortOrder as PQA2SortOrder,
	CASE
		WHEN pqa2.ImagePath is null or pqa2.ImagePath = ''
		THEN pqa2.ImagePath
		WHEN pqa2.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa2.ImagePath
	END as PQA2ImagePath,
	pqa3.PQAnswerID as PQAnswerID3, 
	pqa3.AnswerString as PQAnswer3, 
	pqa3.LinkedQuestion1ID as PQA3LinkedQ1, 
	pqa3.LinkedQuestion2ID as PQA3LinkedQ2,
	pqa3.LinkedQuestion3ID as PQA3LinkedQ3,
	pqa3.LinkedQuestion4ID as PQA3LinkedQ4,
	pqa3.LinkedQuestion5ID as PQA3LinkedQ5,
	pqa3.LinkedQuestion6ID as PQA3LinkedQ6,
	pqa3.type as PQA3Type, 
	pqa3.SortOrder as PQA3SortOrder,
	CASE
		WHEN pqa3.ImagePath is null or pqa3.ImagePath = ''
		THEN pqa3.ImagePath
		WHEN pqa3.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa3.ImagePath
	END as PQA3ImagePath,
	pqa4.PQAnswerID as PQAnswerID4, 
	pqa4.AnswerString as PQAnswer4, 
	pqa4.LinkedQuestion1ID as PQA4LinkedQ1,
	 pqa4.LinkedQuestion2ID as PQA4LinkedQ2,
	pqa4.LinkedQuestion3ID as PQA4LinkedQ3,
	pqa4.LinkedQuestion4ID as PQA4LinkedQ4,
	pqa4.LinkedQuestion5ID as PQA4LinkedQ5,
	pqa4.LinkedQuestion6ID as PQA4LinkedQ6,
	pqa4.type as PQA4Type, 
	pqa4.SortOrder as PQA4SortOrder,
	CASE
		WHEN pqa4.ImagePath is null or pqa4.ImagePath = ''
		THEN pqa4.ImagePath
		WHEN pqa4.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa4.ImagePath
	END as PQA4ImagePath,
	pqa5.PQAnswerID as PQAnswerID5, 
	pqa5.AnswerString as PQAnswer5, 
	pqa5.LinkedQuestion1ID as PQA5LinkedQ1, 
	pqa5.LinkedQuestion2ID as PQA5LinkedQ2,
	pqa5.LinkedQuestion3ID as PQA5LinkedQ3,
	pqa5.LinkedQuestion4ID as PQA5LinkedQ4,
	pqa5.LinkedQuestion5ID as PQA5LinkedQ5,
	pqa5.LinkedQuestion6ID as PQA5LinkedQ6,
	pqa5.type as PQA5Type, 
	pqa5.SortOrder as PQA5SortOrder,
	CASE
		WHEN pqa5.ImagePath is null or pqa5.ImagePath = ''
		THEN pqa5.ImagePath
		WHEN pqa5.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa5.ImagePath
	END as PQA5ImagePath,
	pqa6.PQAnswerID as PQAnswerID6, 
	pqa6.AnswerString as PQAnswer6,
	pqa6.LinkedQuestion1ID as PQA6LinkedQ1, 
	pqa1.LinkedQuestion2ID PQA6LinkedQ2,
	pqa6.LinkedQuestion3ID as PQA6LinkedQ3,
	pqa6.LinkedQuestion4ID as PQA6LinkedQ4,
	pqa6.LinkedQuestion5ID as PQA6LinkedQ5,
	pqa6.LinkedQuestion6ID as PQA6LinkedQ6, 
	pqa6.type as PQA6Type, 
	pqa6.SortOrder as PQA6SortOrder,
	CASE
		WHEN pqa6.ImagePath is null or pqa6.ImagePath = ''
		THEN pqa6.ImagePath
		WHEN pqa6.ImagePath is not null
		THEN 'http://manage.cash4ads.com/' + pqa6.ImagePath
	END as PQA6ImagePath
	from profilequestion pq 
	
		outer apply (
		select top 1 pqa.PQAnswerID, pqa.AnswerString
		, pqa.LinkedQuestion1ID, pqa.LinkedQuestion2ID, pqa.type
		,pqa.LinkedQuestion3ID,pqa.LinkedQuestion4ID,pqa.LinkedQuestion5ID,pqa.LinkedQuestion6ID,
		pqa.ImagePath, pqa.SortOrder
		from ProfileQuestionAnswer pqa
		where pqa.PQID = pq.PQID
		--order by ISNULL( pqa.SortOrder, 99999) asc--order by pqa.SortOrder asc,pqa.AnswerString asc--
		order by pqa.PQAnswerID
	) Pqa1
	outer apply (
		select pqa2.PQAnswerID, pqa2.AnswerString
		, pqa2.LinkedQuestion1ID, pqa2.LinkedQuestion2ID, pqa2.type
		,pqa2.LinkedQuestion3ID,pqa2.LinkedQuestion4ID,pqa2.LinkedQuestion5ID,pqa2.LinkedQuestion6ID,
		pqa2.ImagePath, pqa2.SortOrder
		from ProfileQuestionAnswer pqa2
		where pqa2.PQID = pq.PQID
		--order by ISNULL( pqa2.SortOrder, 99999) asc--order by pqa2.SortOrder asc,pqa2.AnswerString asc--
		order by pqa2.PQAnswerID
		OFFSET 1 ROWS -- skip 1 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa2
	outer apply (
		select pqa3.PQAnswerID, pqa3.AnswerString
		, pqa3.LinkedQuestion1ID, pqa3.LinkedQuestion2ID, pqa3.type, 
		pqa3.LinkedQuestion3ID,pqa3.LinkedQuestion4ID,pqa3.LinkedQuestion5ID,pqa3.LinkedQuestion6ID,
		pqa3.ImagePath, pqa3.SortOrder
		from ProfileQuestionAnswer pqa3
		where pqa3.PQID = pq.PQID
		--order by ISNULL( pqa3.SortOrder, 99999) asc--order by pqa3.SortOrder asc,pqa3.AnswerString asc--
		order by pqa3.PQAnswerID
		OFFSET 2 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa3
	outer apply (
		select pqa4.PQAnswerID, pqa4.AnswerString
		, pqa4.LinkedQuestion1ID, pqa4.LinkedQuestion2ID, pqa4.type, 
		pqa4.LinkedQuestion3ID,pqa4.LinkedQuestion4ID,pqa4.LinkedQuestion5ID,pqa4.LinkedQuestion6ID,
		pqa4.ImagePath, pqa4.SortOrder
		from ProfileQuestionAnswer pqa4
		where pqa4.PQID = pq.PQID
		--order by ISNULL( pqa4.SortOrder, 99999) asc--order by pqa4.SortOrder asc,pqa4.AnswerString asc--
		order by pqa4.PQAnswerID
		OFFSET 3 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa4
	outer apply (
		select pqa5.PQAnswerID, pqa5.AnswerString
		, pqa5.LinkedQuestion1ID, pqa5.LinkedQuestion2ID, pqa5.type, 
		pqa5.LinkedQuestion3ID,pqa5.LinkedQuestion4ID,pqa5.LinkedQuestion5ID,pqa5.LinkedQuestion6ID,
		pqa5.ImagePath, pqa5.SortOrder
		from ProfileQuestionAnswer pqa5
		where pqa5.PQID = pq.PQID
		--order by ISNULL( pqa5.SortOrder, 99999) asc--order by pqa5.SortOrder asc,pqa5.AnswerString asc--
		order by pqa5.PQAnswerID
		OFFSET 4 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa5
	outer apply (
		select pqa6.PQAnswerID, pqa6.AnswerString
		, pqa6.LinkedQuestion1ID, pqa6.LinkedQuestion2ID, pqa6.type, 
		pqa6.LinkedQuestion3ID,pqa6.LinkedQuestion4ID,pqa6.LinkedQuestion5ID,pqa6.LinkedQuestion6ID,
		pqa6.ImagePath, pqa6.SortOrder
		from ProfileQuestionAnswer pqa6
		where pqa6.PQID = pq.PQID
		--order by ISNULL( pqa6.SortOrder, 99999) asc--order by pqa6.SortOrder asc,pqa6.AnswerString asc--
		order by pqa6.PQAnswerID
		OFFSET 5 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa6
	where pq.PQID not in
	(select pqid from profilequestions)
	and
	(
		(((select count(*) from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pqu.ResponseType = 3) =0)-- and pq.CountryID = @countryId) = 0)-- commented because no need for country
		  or 	 
		  ((select top 1 datediff(day, dateadd(month, pq.refreshtime,pqu.AnswerDateTime), getdate()) 
			from ProfileQuestionUserAnswer pqu 
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pqu.ResponseType = 3 --and pq.CountryID = @countryId -- commented because no need for country
			order by pqu.pquanswerid) > 0 )	
		)
	)
	and
	pq.Status = 3
)