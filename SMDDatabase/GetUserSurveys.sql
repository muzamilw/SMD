USE [SMDv2]
GO
/****** Object:  UserDefinedFunction [dbo].[GetUserSurveys]    Script Date: 1/10/2017 12:36:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[GetUserSurveys]
(	

-- select weightage,* from [GetUserSurveys]('b8a3884f-73f3-41ec-9926-293ea919a5e1','cccccc4ads','t')
	-- Add the parameters for the function here
	@userId uniqueidentifier = '', @cash4adsSocialHandle as nvarchar(128), @cash4adsSocialHandleType as nvarchar(128)
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	with surveyquestions(sqid, question, sqtype, description, surveytype, adclickrate, 
 adimagepath, advideolink, adanswer1, adanswer2, adanswer3, adcorrectanswer, adverifyquestion, 
 adrewardtype, advoucher1heading, advoucher1description, advoucher1value, sqleftimagepath,
 sqrightimagepath,  
 pqanswer1id, pqanswer1, pqa1Linkedq1, pqa1Linkedq2, pqa1Linkedq3, pqa1Linkedq4, pqa1Linkedq5, pqa1Linkedq6, pqa1Type, pqa1SortOrder, pqa1ImagePath,
 pqanswer2id, pqanswer2, pqa2Linkedq1, pqa2Linkedq2, pqa2Linkedq3, pqa2Linkedq4, pqa2Linkedq5, pqa2Linkedq6, pqa2Type, pqa2SortOrder, pqa2ImagePath,
 pqanswer3id, pqanswer3, pqa3Linkedq1, pqa3Linkedq2, pqa3Linkedq3, pqa3Linkedq4, pqa3Linkedq5, pqa3Linkedq6, pqa3Type, pqa3SortOrder, pqa3ImagePath,
 pqanswer4id, pqanswer4, pqa4Linkedq1, pqa4Linkedq2, pqa4Linkedq3, pqa4Linkedq4, pqa4Linkedq5, pqa4Linkedq6, pqa4Type, pqa4SortOrder, pqa4ImagePath,
 pqanswer5id, pqanswer5, pqa5Linkedq1, pqa5Linkedq2, pqa5Linkedq3, pqa5Linkedq4, pqa5Linkedq5, pqa5Linkedq6, pqa5Type, pqa5SortOrder, pqa5ImagePath,
 pqanswer6id, pqanswer6, pqa6Linkedq1, pqa6Linkedq2, pqa6Linkedq3, pqa6Linkedq4, pqa6Linkedq5, pqa6Linkedq6, pqa6Type, pqa6SortOrder, pqa6ImagePath,
 weightage, sqleftImagePercentage,
 sqRightImagePercentage, SocialHandle, SocialHandleType)
as (
	select sq.sqid, sq.question, 'Survey', 
	sq.Description, sq.Type SurveyType, NULL, '', '',
	'', '', '', NULL, '', NULL, '', '',
	'',
	CASE
		WHEN sq.LeftPicturePath is null or sq.LeftPicturePath = ''
		THEN sq.LeftPicturePath
		WHEN sq.LeftPicturePath is not null
		THEN sq.LeftPicturePath
	END as SqLeftImagePath, 
	CASE
		WHEN sq.RightPicturePath is null or sq.RightPicturePath = ''
		THEN sq.RightPicturePath
		WHEN sq.RightPicturePath is not null
		THEN sq.RightPicturePath
	END as SqRightImagePath, 
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer1
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer2
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer3
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer4
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer5
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer6
	(((row_number() over (order by (case when isnull(sq.companyid,0) = 0 then 10 else 1 end)) * 100) + 20) ) Weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage,
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


	from surveyquestion sq
	left outer join Company c on sq.companyid = c.CompanyId
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	(((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID and mySurveyQuestionResponse.responsetype = 3) = 0)
	  or  
	  ((select top 1 mySurveyQuestionResponse.UserSelection from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID and mySurveyQuestionResponse.responsetype = 3) is null)
	)
	and sq.ParentSurveyId is null and sq.Status = 3 -- live
	UNION ALL
	-- Recursive member definition
		select sqp.sqid, sqp.question, 'Survey',
	sqp.Description, sqp.Type SurveyType, NULL, '', '',
	'', '', '', NULL, '', NULL, '', '',
	'',
	CASE
		WHEN sqp.LeftPicturePath is null or sqp.LeftPicturePath = ''
		THEN sqp.LeftPicturePath
		WHEN sqp.LeftPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sqp.LeftPicturePath
	END as SqLeftImagePath, 
	CASE
		WHEN sqp.RightPicturePath is null or sqp.RightPicturePath = ''
		THEN sqp.RightPicturePath
		WHEN sqp.RightPicturePath is not null
		THEN 'http://manage.cash4ads.com/' + sqp.RightPicturePath
	END as SqRightImagePath, 
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer1
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer2
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer3
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer4
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer5
	NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', -- PQAnswer6
	(select (isnull(weightage, 0) + isnull(sq.Priority, 0)) from [GetRootParentSurvey](sq.SQID)) as weightage,
	sqResponsePercentages.leftImagePercentage, sqResponsePercentages.rightImagePercentage,
	null as SocialHandle, null as SocialHandleType
	from surveyquestion sq	
	inner join SurveyQuestion sqp on sqp.ParentSurveyId = sq.SQID
	outer apply
	(select * from [GetUserSurveySelectionPercentage](sq.sqid)) as sqResponsePercentages
	where -- If this survey has no response yet
	((((select count(*) from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID and mySurveyQuestionResponse.responsetype = 3) = 0 )
	  or  
	  ((select top 1 mySurveyQuestionResponse.UserSelection from SurveyQuestionResponse mySurveyQuestionResponse
			 where mySurveyQuestionResponse.UserID = @userId and 
			 mySurveyQuestionResponse.SQID = sq.SQID and mySurveyQuestionResponse.responsetype = 3) is null)
		)
	  and
	  sq.Status = 3 -- live
    ) 
  )
	select * from surveyquestions
)
