
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  <Author,,Name>
-- Create date: <Create Date,,>
-- Description: <Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetRegisteredUserData]
-- GetRegisteredUserData -1,'',1,10
 @Status int,
 @keyword nvarchar(100),
 @fromRoww int,
 @toRow int
 
AS
BEGIN
SET NOCOUNT ON;


select *, COUNT(*) OVER() AS TotalItems
 from (


  SELECT AspNetUsers.Id,AspNetUsers.fullname,AspNetUsers.LastLoginTime,AspNetUsers.Email,AspNetUsers.[Status],Company.CompanyId, Company.CompanyName,acc.AccountBalance
  FROM AspNetUsers
  INNER JOIN Company ON AspNetUsers.CompanyId=Company.CompanyId
  inner join Account acc on company.CompanyId = acc.CompanyId and acc.AccountType = 4
  where AspNetUsers.EmailConfirmed =1
  and (
   (@Status = -1) or (isnull(AspNetUsers.status,0) = @Status)
  )
  and (
   (@keyword = '') or (AspNetUsers.fullname like concat('%',@keyword,'%') or AspNetUsers.email like concat('%',@keyword,'%') or Company.companyname like concat('%',@keyword,'%'))
  )

 )o


 order by LastLoginTime desc
 OFFSET @fromRoww ROWS
 FETCH NEXT @toRow ROWS ONLY
 

 

END