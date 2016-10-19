
GO
/****** Object:  StoredProcedure [dbo].[GetUserProfileCompletness]    Script Date: 10/13/2016 9:00:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mz
-- Create date: 6 sept 2016
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[GetUserProfileCompletness] 
	--  exec [GetUserProfileCompletness] '9a7fb205-ed88-4b05-b287-9c4d10bd4d68'
	@UserId nvarchar(128)
	
AS
BEGIN





	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
		select 
			isnull(
			(select count(*) from (
				select  distinct pqa.UserID from ProfileQuestionUserAnswer pqa
				inner join ProfileQuestion pqi on pqa.PQID = pqi.pqid and pqi.CompanyId is null and pqi.Status = 3

				where pqa.UserID = @userid
				group by pqa.PQID, pqa.UserID
			) pqans
		  )  / count(*) * 100,0)
		 from ProfileQuestion pq 

		where pq.CompanyId is null and pq.Status = 3





----select pq.Question, pqa.* from ProfileQuestion pq 
----OUTER APPLY (SELECT  top 1 *
----                    FROM   ProfileQuestionUserAnswer pqa
----                    WHERE  pqa.PQID = pq.PQID and pqa.UserID = @userid
----					--group by pqa.PQUAnswerID
----					order by pqa.AnswerDateTime 
					
----                    ) pqa
----		OUTER APPLY (SELECT  count(*) cnt
----                    FROM   ProfileQuestionUserAnswer pqa
----                    WHERE  pqa.PQID = pq.PQID and pqa.UserID = @userid
----					--group by pqa.PQUAnswerID
					
					
----                    ) pqac
					
					
----where pq.CompanyId is null


END
