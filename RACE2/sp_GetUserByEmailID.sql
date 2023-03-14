CREATE PROCEDURE [dbo].[sp_GetUserByEmailID] 
    (@email nvarchar(256))  
AS  
BEGIN    
    SELECT * FROM AspNetUsers WHERE Email = @email 
END 