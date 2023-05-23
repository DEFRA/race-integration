/****** Object:  StoredProcedure [dbo].[sp_GetAddressForReservoir]    Script Date: 22/05/2023 19:14:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAddressForReservoir]
(
    @reservoirId int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
   select * 
from OrganisationReservoirs A 
inner join OrganisationAddresses B on B.OrganisationId = A.OrganisationId
inner join Addresses C on C.id = B.Addressid
where A.ReservoirId = 38 
END
GO


