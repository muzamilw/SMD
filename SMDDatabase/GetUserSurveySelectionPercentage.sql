
GO
/****** Object:  UserDefinedFunction [dbo].[GetUserSurveySelectionPercentage]    Script Date: 10/27/2016 12:22:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[GetUserSurveySelectionPercentage]
(	--  select * from [GetUserSurveySelectionPercentage](10332)
	-- Add the parameters for the function here
	@sqId int = 0
)
RETURNS 
@SurveySelectionPercentage TABLE
(
	-- Add the column definitions for the TABLE variable here
	leftImagePercentage float null, 
	rightImagePercentage float null
)
AS
BEGIN
	-- Fill the table variable with the rows for your result set
	DECLARE @surveyResponses float
	SELECT @surveyResponses = count(*) from SurveyQuestionResponse where SQID = @sqId and responsetype = 3

	-- Add the SELECT statement with parameter references here
	insert into @SurveySelectionPercentage
	values
	((select 
	CASE
		WHEN @surveyResponses is null or @surveyResponses <= 0
		THEN @surveyResponses
		WHEN @surveyResponses > 0
		THEN  
		count(*) 
	END as leftImagePercentage
	  from SurveyQuestionResponse
	where SQID = @sqId and UserSelection = 1 and responsetype = 3),
	((select 
	CASE
		WHEN @surveyResponses is null or @surveyResponses <= 0
		THEN @surveyResponses
		WHEN @surveyResponses > 0
		THEN  
		count(*) 
	END as rightImagePercentage
	 from SurveyQuestionResponse
	where SQID = @sqId and UserSelection = 2 and responsetype = 3)))

	RETURN 
END
