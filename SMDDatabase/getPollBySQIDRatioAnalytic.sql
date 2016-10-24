GO
/****** Object:  StoredProcedure [dbo].[getPollBySQIDRatioAnalytic]    Script Date: 10/22/2016 1:32:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getPollBySQIDRatioAnalytic] (
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
	 Select top 1 @dateFrom = ResoponseDateTime from SurveyQuestionResponse where SQID = @Id
	END
		Select r.UserSelection label, count(r.SQResponseID) value 
		from  SurveyQuestionResponse r 
		where r.SQID = @Id and r.ResoponseDateTime >= @dateFrom and r.ResoponseDateTime <= getdate() 
		group by r.UserSelection

END
 
 
--EXEC [getPollBySQIDRatioAnalytic] 18, 2
