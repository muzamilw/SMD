GO
/****** Object:  StoredProcedure [dbo].[getAdsCampaignByCampaignId]    Script Date: 10/15/2016 10:36:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[getSurveyByPQIDRatioAnalytic] (
@Id INT,  -- 1 for Viewed, 2 for Conversions or 3 for Skipped
@DateRange INT -- 1 for last 30 days , 2 for All time	
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @dateFrom DATE = getdate()-30;
 
IF @DateRange = 2
	BEGIN
	 Select top 1 @dateFrom = AnswerDateTime from ProfileQuestionUserAnswer where PQID = @Id
	END
		Select p.AnswerString label, count(u.PQAnswerID) value from ProfileQuestionAnswer p
		inner join ProfileQuestionUserAnswer u on u.PQAnswerID = p.PQAnswerID
		where p.PQID = @Id and u.AnswerDateTime >= @dateFrom and u.AnswerDateTime <= getdate() 
		group by u.PQAnswerID, p.AnswerString

END
 
 
--EXEC [getSurveyByPQIDRatioAnalytic] 6, 1
