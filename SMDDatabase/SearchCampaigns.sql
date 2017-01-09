GO
/****** Object:  StoredProcedure [dbo].[SearchCampaigns]    Script Date: 1/9/2017 11:57:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Mz
-- Create date: 8 dec 2015
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[SearchCampaigns] 
--  exec [SearchCampaigns] 0,'',416,0,10,0
	-- Add the parameters for the stored procedure here
	@Status int,
	@keyword nvarchar(100),
	@companyId int,
	@fromRoww int,
	@toRow int,
	@adminMode bit
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--select @toRow, @fromRow
	--return


	select *, COUNT(*) OVER() AS TotalItems
	from (

    -- Insert statements for procedure here
	select  a.CampaignID,a.CompanyId, a.CampaignName, (case when a.Type = 1 then a.VideoLink2 else a.LogoUrl end )  ImagePath ,a.VerifyQuestion , Max(eh.EventDateTime) ModifiedDateTime, a.Answer1, a.Answer2, a.Answer3,
	(select isnull(count (*),0) from adcampaignresponse cr where cr.campaignid = a.campaignid and  cast(CreatedDateTime as DATE)  = cast (GETDATE() as DATE) and responsetype = 1 ) viewCountToday,
	(select isnull(count (*),0) from adcampaignresponse cr where cr.campaignid = a.campaignid and cast(CreatedDateTime as date) = cast(getdate()-1 as date) and responsetype = 1) viewCountYesterday,
	(select isnull(count (*),0) from adcampaignresponse cr where cr.campaignid = a.campaignid and cr.ResponseType = 1) viewCountAllTime,
	(select isnull(count (*),0) from adcampaignresponse cr where cr.campaignid = a.campaignid and  cast(CreatedDateTime as DATE)  = cast (GETDATE() as DATE) and responsetype = 2 ) clickThroughsToday,
	(select isnull(count (*),0) from adcampaignresponse cr where cr.campaignid = a.campaignid and cast(CreatedDateTime as date) = cast(getdate()-1 as date) and responsetype = 2) clickThroughsYesterday,
	(select isnull(count (*),0)from adcampaignresponse cr where cr.campaignid = a.campaignid and cr.ResponseType = 2) clickThroughsAllTime,
	(select isnull(count (*),0)from adcampaignresponse cr where cr.campaignid = a.campaignid and cr.ResponseType = 3) SurveyAnsweredAllTime,
	(select isnull(count (*),0)  from AdCampaignResponse cr where cr.campaignid = a.campaignid and cr.ResponseType = 3 and cr.UserSelection = 1 ) Answer1Stats,
	(select isnull(count (*),0)  from AdCampaignResponse cr where cr.campaignid = a.campaignid and cr.ResponseType = 3 and cr.UserSelection = 2 ) Answer2Stats,
	(select isnull(count (*),0)  from AdCampaignResponse cr where cr.campaignid = a.campaignid and cr.ResponseType = 3 and cr.UserSelection = 3 ) Answer3Stats,
	left(( select
			stuff((
					select ', ' + c1.CountryName, ', ' + ci.CityName
					from [AdCampaignTargetLocation] l1 
					left outer join country c1 on l1.countryid = c1.countryid
					left outer join city ci on l1.CityID = ci.CityID
					where l1.campaignid = l.campaignid
					order by c1.CountryName
					for xml path('')
				),1,1,'') as name_csv
			from [AdCampaignTargetLocation] l 
			where CampaignID = a.CampaignID
			group by l.campaignid  
		),30) +'..' Locationss,
		
		 StartDateTime, MaxBudget, MaxDailyBudget,AmountSpent,
		Status, ApprovalDateTime, ClickRate, a.CreatedDateTime, a.Type, a.priority 
		
		
		
	
	 from AdCampaign a
	 
	--inner join AspNetUsers u on 
	
	
	left outer join CampaignEventHistory eh on a.CampaignID = eh.CampaignID 
	where 
	(
		(@keyword is null or (a.CampaignName like '%'+ @keyword +'%' or a.DisplayTitle like '%'+ @keyword +'%'  ))
		and a.Status <> 7
		and 
		( @Status = 0 or a.[status] = @Status)
		and  ( @adminMode = 1 or a.companyid = @companyId )

	)
	


	group by a.CampaignID, CampaignName,StartDateTime,MaxBudget,MaxDailyBudget,AmountSpent,Status, ApprovalDateTime, ClickRate, a.CreatedDateTime, a.Type, a.priority,a.CompanyId,a.VideoLink2, a.LogoUrl, a.VerifyQuestion ,  a.Answer1, a.Answer2, a.Answer3, a.ModifiedDateTime, eh.CampaignID
		)as items
	order by priority desc
	OFFSET @fromRoww ROWS
	FETCH NEXT @toRow ROWS ONLY
	
END






