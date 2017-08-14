GO
/****** Object:  StoredProcedure [dbo].[getSurvayByPQIDtblAnalytic]    Script Date: 10/24/2016 9:34:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getSurvayByPQIDtblAnalytic] (
@Id INT  -- 1 for Viewed, 2 for Conversions or 3 for Skipped
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @dateFrom DATE = getdate()-30;--, @Ctype int = 1;
--	select * from ProfileQuestionUserAnswer
	BEGIN
	 Select top 1 @dateFrom= [AnswerDateTime] from ProfileQuestionUserAnswer where PQID = @Id
	 --Select top 1 @Ctype = [Type] from AdCampaign where CampaignID = @Id
	END
		Select 1 ordr, 'Delivered' label, count(pqu.PQUAnswerID) 'All time' ,
			 (Select count(pqu.PQUAnswerID) from ProfileQuestionUserAnswer pqu
			  where pqu.AnswerDateTime >= getdate() - 30 and pqu.AnswerDateTime <= getdate() and pqu.ResponseType = 1 and pqu.PQID = @Id) '30 days'
		from ProfileQuestionUserAnswer pqu
		where pqu.AnswerDateTime >= @dateFrom and pqu.AnswerDateTime <= getdate() and pqu.ResponseType = 1 and pqu.PQID = @Id
		union
		
		Select 2 ordr, 'Skipped' label, count(pqu.PQUAnswerID) 'All time' ,
			 (Select count(pqu.PQUAnswerID) from ProfileQuestionUserAnswer pqu
			  where pqu.AnswerDateTime >= getdate() - 30 and pqu.AnswerDateTime <= getdate() and pqu.ResponseType = 4 and pqu.PQID = @Id) '30 days'
		from ProfileQuestionUserAnswer pqu
		where pqu.AnswerDateTime >= @dateFrom and pqu.AnswerDateTime <= getdate() and pqu.ResponseType = 4 and pqu.PQID = @Id
		union
		Select 3 ordr, 'Answered' label, count(pqu.PQUAnswerID) 'All time' ,
			 (Select count(pqu.PQUAnswerID) from ProfileQuestionUserAnswer pqu
			  where pqu.AnswerDateTime >= getdate() - 30 and pqu.AnswerDateTime <= getdate() and pqu.ResponseType = 3 and pqu.PQID = @Id) '30 days'
		from ProfileQuestionUserAnswer pqu
		where pqu.AnswerDateTime >= @dateFrom and pqu.AnswerDateTime <= getdate() and pqu.ResponseType = 3 and pqu.PQID = @Id
		union
		Select 4 ordr, 'Total incurred cost' label, isnull(sum(t.DebitAmount)/100, 0) 'All time' , 
			(Select isnull(sum(t.DebitAmount)/100, 0) from [Transaction] t
			where t.TransactionDate >= getdate() - 30 and t.TransactionDate <= getdate() and t.PQID = @Id) '30 days'
		from [Transaction] t
		where t.TransactionDate >= @dateFrom and t.TransactionDate <= getdate() AND t.PQID = @Id
				
END
 
 
--EXEC [getSurvayByPQIDtblAnalytic] 3

