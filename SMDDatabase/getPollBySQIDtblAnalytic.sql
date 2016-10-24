GO
/****** Object:  StoredProcedure [dbo].[getPollBySQIDtblAnalytic]    Script Date: 10/20/2016 2:24:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getPollBySQIDtblAnalytic] (
@Id INT  -- 1 for Viewed, 2 for Conversions or 3 for Skipped
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @dateFrom DATE = getdate()-30;

	BEGIN
	 Select top 1 @dateFrom= [ResoponseDateTime] from SurveyQuestionResponse where SQID = @Id
	END


		Select 1 ordr, 'Delivered' label, count(sqr.SQID) 'All time' ,
			 (Select count(sqr.SQID) from SurveyQuestionResponse sqr
			  where sqr.ResoponseDateTime >= getdate() - 30 and sqr.ResoponseDateTime <= getdate() and sqr.ResponseType = 1 and sqr.SQID = @Id) '30 days'
		from SurveyQuestionResponse sqr
		where sqr.ResoponseDateTime >= @dateFrom and sqr.ResoponseDateTime <= getdate() and sqr.ResponseType = 1 and sqr.SQID = @Id
		
		union
		
		Select 2 ordr, 'Skipped' label, count(sqr.SQID) 'All time' ,
			 (Select  count(sqr.SQID) from SurveyQuestionResponse sqr
			  where sqr.ResoponseDateTime >= getdate() - 30 and sqr.ResoponseDateTime <= getdate() and sqr.ResponseType = 4 and sqr.SQID = @Id) '30 days'
		from SurveyQuestionResponse sqr
		where sqr.ResoponseDateTime >= @dateFrom and sqr.ResoponseDateTime <= getdate() and sqr.ResponseType = 4 and sqr.SQID = @Id

		union
		Select 3 ordr, 'Answered' label, count(sqr.SQID) 'All time' ,
			 (Select  count(sqr.SQID) from SurveyQuestionResponse sqr
			  where sqr.ResoponseDateTime >= getdate() - 30 and sqr.ResoponseDateTime <= getdate() and sqr.ResponseType = 3 and sqr.SQID = @Id) '30 days'
		from SurveyQuestionResponse sqr
		where sqr.ResoponseDateTime >= @dateFrom and sqr.ResoponseDateTime <= getdate() and sqr.ResponseType = 3 and sqr.SQID = @Id
			
		union
		Select 4 ordr, 'Cost' label, isnull(sum(t.DebitAmount)/100, 0) 'All time' , 
			(Select isnull(sum(t.DebitAmount)/100, 0) from [Transaction] t
			where t.TransactionDate >= getdate() - 30 and t.TransactionDate <= getdate() and t.SQID = @Id) '30 days'
		from [Transaction] t
		where t.TransactionDate >= @dateFrom and t.TransactionDate <= getdate() AND t.SQID = @Id
				
END
  
--EXEC [getPollBySQIDtblAnalytic] 12

