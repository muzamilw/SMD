USE [SMDv2]
GO
/****** Object:  StoredProcedure [dbo].[getAdsCampaignByCampaignIdRatioAnalytic]    Script Date: 11/15/2016 11:03:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [dbo].[getCampaignByIdFormDataAnalytic] (
@Id INT
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE @AgedateFrom DATE =  DATEADD(YYYY,-100,getdate()) ;
DECLARE @AgedateTo DATE = getdate();
 
 DECLARE @T1 TABLE (
	Question  VARCHAR(200),
	Answer VARCHAR(200),
	Id int,
	type int --2 for profile question, 3 for Survay Question and 4 for Ad Quiz question
	)

 --EXEC [getCampaignByIdFormDataAnalytic] 10043

	select pq.Question Question, pqa.AnswerString answer, act.PQID Id, 2 typ  from AdCampaignTargetCriteria act
	inner join ProfileQuestion pq on pq.PQID = act.PQID
	inner join ProfileQuestionAnswer pqa on pqa.PQAnswerID = act.PQAnswerID
	where act.CampaignID = @Id

	union
	select sq.Question Question, CONVERT(varchar(200), act.SQAnswer) answer, act.SQID Id , 3 typ  from AdCampaignTargetCriteria act
	inner join SurveyQuestion sq on sq.SQID = act.SQID
	where act.CampaignID = @Id	
	


	union 
	select ad.VerifyQuestion Question, COALESCE(case when act.QuizAnswerId = 1 then ad.Answer1 else null end, case when act.QuizAnswerId = 2 then ad.Answer2 else null end,case when act.QuizAnswerId = 3 then ad.Answer3 else null end ) answer, act.CampaignID Id , 4 typ  
	from AdCampaignTargetCriteria act
	inner join AdCampaign ad on ad.CampaignID = act.QuizCampaignId
	where act.CampaignID = @Id	

	
END
 
--EXEC [getCampaignByIdFormDataAnalytic] 10043
