/****** Object:  StoredProcedure [dbo].[sp_GetSafetyMeasuresListByReservoirId]    Script Date: 22/05/2023 19:11:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetSafetyMeasuresListByReservoirId]
(
     @reservoirid int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
     Select * from SafetyMeasures where ReservoirId = @reservoirid
END
GO


