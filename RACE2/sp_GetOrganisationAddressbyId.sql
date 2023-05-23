/****** Object:  StoredProcedure [dbo].[sp_GetOrganisationAddressbyId]    Script Date: 22/05/2023 19:14:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetOrganisationAddressbyId]
(
     @orgId int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    select * from Organisations A
    inner join OrganisationAddresses B On A.Id = B.OrganisationId
    inner join Addresses C On C.id = B.Addressid
    where A.Id = @orgId
END
GO


