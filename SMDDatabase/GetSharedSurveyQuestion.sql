
GO
/****** Object:  StoredProcedure [dbo].[GetSharedSurveyQuestion]    Script Date: 11/17/2016 10:47:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mz
-- Create date: 
-- Description:	
-- =============================================

--   GetSharedSurveyQuestion 5
ALTER PROCEDURE [dbo].[GetSharedSurveyQuestion] 
	-- Add the parameters for the stored procedure here
	@SSQID bigint = 0

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
	  isnull(answers.tcount,0) TotalAnswers,
	  isnull(leftanswercount.tcount,0) LeftAnswerCount,
	  isnull(rightanswercount.tcount,0) RightAnswerCount,
	  isnull(leftanswercount.malecount,0) LefMmaleCount,
	  isnull(leftanswercount.femalecount,0) LeftFemaleCount,
	    isnull(rightanswercount.malecount,0) RightMaleCount,
	  isnull(rightanswercount.femalecount,0) RightFemaleCount,
	  grp.GroupName,

	  (case when answers.tcount > 0 then leftanswercount.tcount/answers.tcount*100 else 0 end ) LeftAnswerPerc,
	  (case when answers.tcount > 0 then rightanswercount.tcount/answers.tcount*100 else 0 end ) RightAnswerPerc


  FROM [dbo].[SharedSurveyQuestion] survey
  inner join SurveySharingGroup grp on survey.SharingGroupId = grp.SharingGroupId and survey.SSQID = @SSQID
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


END
