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
create PROCEDURE [dbo].[getPollBySQIDRatioAnalytic] (
@Id INT,  -- SQID
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
		Select r.UserSelection label, count(r.SQResponseID) value 
		from  SurveyQuestionResponse r 
		where r.SQID = @Id and r.ResoponseDateTime >= @dateFrom and r.ResoponseDateTime <= getdate() 
		group by r.UserSelection

END
 
 
--EXEC [getPollBySQIDRatioAnalytic] 12, 2
