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
create PROCEDURE [dbo].[getAdsCampaignPerCityPerGenderFormAnalytic] (
@Id INT

)
AS
BEGIN
		
select city, ISNULL([1], 0) male, ISNULL([2], 0) female from (		
		Select acr.UserLocationCity city, usr.gender,  count(*) stat
		from AdCampaignResponse acr
		inner join AspNetUsers usr on usr.Id = acr.UserID
		
		where acr.ResponseType = 1 and acr.CampaignID = @Id
		group by acr.UserLocationCity , usr.gender
		) src
pivot
(
  max(stat)
    for gender in ([1], [2]) 
) piv

	
END
 
--EXEC [getAdsCampaignPerCityPerGenderFormAnalytic] 10043

