/****** Object:  StoredProcedure [dbo].[sp_UpdateFirstTimeUser]    Script Date: 26/09/2023 12:57:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[sp_UpdateFirstTimeUser]
(
    @email nvarchar(256)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
UPDATE [dbo].[AspNetUsers]
SET [c_IsFirstTimeUser] = 0
Where [Email] = @email
END
