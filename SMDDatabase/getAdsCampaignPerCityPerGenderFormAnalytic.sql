GO
/****** Object:  StoredProcedure [dbo].[getAdsCampaignPerCityPerGenderFormAnalytic]    Script Date: 12/2/2016 12:37:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getAdsCampaignPerCityPerGenderFormAnalytic] (
@Id INT

)
AS
BEGIN
		
select city, ISNULL([1], 0) male, ISNULL([2], 0) female from (		
		Select c.CityName city, usr.gender,  count(*) stat
		from AdCampaignResponse acr
		inner join AdCampaignTargetLocation atl on atl.CampaignID = acr.CampaignID
		inner join AspNetUsers usr on usr.Id = acr.UserID
		inner join City c on atl.CityID = c.CityId and acr.UserLocationCity = c.CityName
		
		where acr.ResponseType = 3 and acr.CampaignID = @Id
		group by c.CityName , usr.gender
		) src
pivot
(
  max(stat)
    for gender in ([1], [2]) 
) piv

	
END
 
--EXEC [getAdsCampaignPerCityPerGenderFormAnalytic] 10043

