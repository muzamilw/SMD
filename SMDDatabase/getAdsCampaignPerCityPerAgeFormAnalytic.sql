GO
/****** Object:  StoredProcedure [dbo].[getAdsCampaignByIdFormAnalytic]    Script Date: 11/30/2016 9:27:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[getAdsCampaignPerCityPerAgeFormAnalytic] (
@Id INT
)
AS
BEGIN
		
select city, ISNULL([1], 0) '10-20', ISNULL([2], 0) '20-30', ISNULL([3], 0) '30-40',ISNULL([4], 0) '40-50',ISNULL([5], 0) '50-60',ISNULL([6], 0) '60-70',ISNULL([7], 0) '70-80',ISNULL([8], 0) '80-90', ISNULL([9], 0) '90+' from (		
		Select acr.UserLocationCity city, COALESCE(case when usr.DOB > = DATEADD(YYYY,-20,getdate()) and usr.DOB < DATEADD(YYYY,-10,getdate()) then 1 else null end, 
		case when usr.DOB > = DATEADD(YYYY,-30,getdate())  and usr.DOB < DATEADD(YYYY,-20,getdate()) then 2 else null end, 
		case when usr.DOB > = DATEADD(YYYY,-40,getdate())  and usr.DOB < DATEADD(YYYY,-30,getdate()) then 3 else null end,
		case when usr.DOB > = DATEADD(YYYY,-50,getdate())  and usr.DOB < DATEADD(YYYY,-40,getdate()) then 4 else null end,
		case when usr.DOB > = DATEADD(YYYY,-60,getdate())  and usr.DOB < DATEADD(YYYY,-50,getdate()) then 5 else null end,
		case when usr.DOB > = DATEADD(YYYY,-70,getdate())  and usr.DOB < DATEADD(YYYY,-60,getdate()) then 6 else null end,
		case when usr.DOB > = DATEADD(YYYY,-80,getdate()) and usr.DOB < DATEADD(YYYY,-70,getdate()) then 7 else null end,
		case when usr.DOB > = DATEADD(YYYY,-90,getdate())  and usr.DOB < DATEADD(YYYY,-80,getdate())	then 8 else null end,
		case when usr.DOB > = DATEADD(YYYY,-200,getdate())  and usr.DOB < DATEADD(YYYY,-90,getdate()) then 9 else null end) as age,  count(*) stat
		from AdCampaignResponse acr
		inner join AspNetUsers usr on usr.Id = acr.UserID
		
		where acr.ResponseType = 1 and acr.CampaignID = @Id
		group by acr.UserLocationCity , usr.DOB
		) src
pivot
(
  sum(stat)
    for age in ([1], [2], [3], [4], [5], [6], [7], [8], [9]) 
) piv

	
END
 
--EXEC [getAdsCampaignPerCityPerAgeFormAnalytic] 10043
