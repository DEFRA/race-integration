/****** Object:  StoredProcedure [dbo].[sp_GetActionsListByReservoirId]    Script Date: 22/05/2023 19:15:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetActionsListByReservoirId]
(
    @reservoirid int,
	@category nvarchar(64)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    Select * from Actions where ReservoirId = @reservoirid and Category = @category
END
GO


