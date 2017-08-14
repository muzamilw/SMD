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
-- Author:		mz
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE GetUserProfileQuestionsList 
	-- Add the parameters for the stored procedure here
	@UserID nvarchar(128)
	
AS
BEGIN


select pq.PQID,pq.Question,
pq.[Type],
pqa1.PQAnswerID as PQAnswerID1, 
pqa1.AnswerString as PQAnswer1, 
pqa2.PQAnswerID as PQAnswerID2, 
pqa2.AnswerString as PQAnswer2, 
pqa3.PQAnswerID as PQAnswerID3, 
pqa3.AnswerString as PQAnswer3, 
pqa4.PQAnswerID as PQAnswerID4, 
pqa4.AnswerString as PQAnswer4, 
pqa5.PQAnswerID as PQAnswerID5, 
pqa5.AnswerString as PQAnswer5, 
pqa6.PQAnswerID as PQAnswerID6, 
pqa6.AnswerString as PQAnswer6,
PQUA.PQUAnswerID as LastUserAnswerID,
PQUA.AnswerString AS LastUserAnswer
from profilequestion pq

outer apply (
		select top 1 pqa.PQAnswerID, pqa.AnswerString
		, 
		pqa.type, 
		pqa.ImagePath, pqa.SortOrder
		from ProfileQuestionAnswer pqa
		where pqa.PQID = pq.PQID and pqa.Status != 0
		
		order by pqa.PQAnswerID
	) Pqa1
	outer apply (
		select pqa2.PQAnswerID, pqa2.AnswerString
		, 
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
		,
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
		,
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
		, 
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
		, 
		pqa6.ImagePath, pqa6.SortOrder
		from ProfileQuestionAnswer pqa6
		where pqa6.PQID = pq.PQID  and pqa6.Status != 0
		--order by ISNULL( pqa6.SortOrder, 99999) asc--order by pqa6.SortOrder asc,pqa6.AnswerString asc--
		order by pqa6.PQAnswerID
		OFFSET 5 ROWS -- skip 2 rows
		FETCH NEXT 1 ROWS ONLY -- take 1 rows
	) Pqa6
	outer Apply 
	(
		select top 1  pqu.PQUAnswerID, pqu.AnswerDateTime, pqu.PQAnswerID, pqa.AnswerString  from ProfileQuestionUserAnswer pqu 
		inner join ProfileQuestionAnswer pqa on pqu.PQAnswerID = pqa.PQAnswerID
			where pq.PQID = pqu.PQID and pqu.UserID = @UserID and pqu.ResponseType = 3
			order by AnswerDateTime DESC
	
	) pqua
	where pq.status = 3
	order by priority




END
GO
