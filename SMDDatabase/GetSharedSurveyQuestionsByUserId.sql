
GO
/****** Object:  StoredProcedure [dbo].[GetSharedSurveyQuestion]    Script Date: 11/16/2016 10:40:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mz
-- Create date: 
-- Description:	
-- =============================================
-- 
--   [GetSharedSurveyQuestionsByUserId] '576be178-9d5b-4775-a902-420b4d7ee1d0'
alter PROCEDURE [dbo].[GetSharedSurveyQuestionsByUserId] 
	-- Add the parameters for the stored procedure here
	@UserId nvarchar(128) 

AS
BEGIN
	SELECT [SSQID]
      ,survey.[UserId]
      ,survey.[CompanyId]
      ,[SurveyTitle]
      ,'http://manage.cash4ads.com/' + [LeftPicturePath] as [LeftPicturePath]
      ,'http://manage.cash4ads.com/' + [RightPicturePath] as [RightPicturePath]
      ,survey.[CreationDate]
      ,survey.[SharingGroupId],
	  totalshares.tcount TotalShared,
	  answers.tcount TotalAnswers,
	  leftanswercount.tcount LeftAnswerCount,
	  rightanswercount.tcount RightAnswerCount,
	  leftanswercount.malecount LefMmaleCount,
	  leftanswercount.femalecount LeftFemaleCount,
	    rightanswercount.malecount RightMaleCount,
	  rightanswercount.femalecount RightFemaleCount,
	  grp.GroupName,

	  (case when answers.tcount > 0 then leftanswercount.tcount/answers.tcount*100 else 0 end ) LeftAnswerPerc,
	  (case when answers.tcount > 0 then rightanswercount.tcount/answers.tcount*100 else 0 end ) RightAnswerPerc


  FROM [dbo].[SharedSurveyQuestion] survey
  inner join SurveySharingGroup grp on survey.SharingGroupId = grp.SharingGroupId and survey.userid = @UserId
  outer apply 
(Select count(*) tcount from [dbo].[SurveySharingGroupShares] s where s.SSQID = survey.SSQID
) totalshares
  outer apply 
(Select count(*) tcount from [dbo].[SurveySharingGroupShares] s where s.SSQID = survey.SSQID and Status = 2
) answers
  outer apply 
(

Select count(*) tcount, sum(case when u.gender = 1 then 1 else 0 end) malecount,sum(case when u.gender =2 then 1 else 0 end) femalecount from [dbo].[SurveySharingGroupShares] s
inner join AspNetUsers u on s.UserId = u.Id
 where s.SSQID = survey.SSQID and s.Status = 2 and s.UserSelection = 1

) leftanswercount
  outer apply 
(
Select count(*) tcount,sum(case when u.gender = 1 then 1 else 0 end) malecount,sum(case when u.gender =2 then 1 else 0 end) femalecount from [dbo].[SurveySharingGroupShares] s 
inner join AspNetUsers u on s.UserId = u.Id
where s.SSQID = survey.SSQID and s.Status = 2 and s.UserSelection = 2
) rightanswercount
order by survey.CreationDate desc


END
