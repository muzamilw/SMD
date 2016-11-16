-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
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
alter PROCEDURE GetSharedSurveyQuestion 
	-- Add the parameters for the stored procedure here
	@SSQID bigint = 0

AS
BEGIN
	SELECT [SSQID]
      ,survey.[UserId]
      ,survey.[CompanyId]
      ,[SurveyTitle]
      ,[LeftPicturePath]
      ,[RightPicturePath]
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

	  leftanswercount.tcount/answers.tcount*100 LeftAnswerPerc,
	  rightanswercount.tcount/answers.tcount*100 RightAnswerPerc


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
GO
