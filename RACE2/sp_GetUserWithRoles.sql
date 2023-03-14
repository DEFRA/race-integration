CREATE PROCEDURE [dbo].[sp_GetUserWithRoles]
 (@email nvarchar(256))  
AS  
BEGIN 

Select A.Id, A.Email,A.UserName,B.UserId,B.RoleId,c.Id,c.Name
                              from AspNetUsers A inner join AspNetUserRoles B
                              ON  A.Id =b.UserId inner join AspNetRoles c
                              On c.Id = b.RoleId Where A.Email=@email

END 
GO