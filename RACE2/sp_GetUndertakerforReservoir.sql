/****** Object:  StoredProcedure [dbo].[sp_GetUndertakerforReservoir]    Script Date: 26/09/2023 12:52:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetUndertakerforReservoir]
(
   @id int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
   select *,C.c_first_name as UndertakerFirstName,C.c_last_name as UndertakerLastName
from AspNetUserRoles A 
inner join UserReservoirs B on B.UserDetailId = A.UserId
inner join AspNetUsers C on C.Id = B.UserDetailId
inner join Reservoirs E  on E.Id = B.ReservoirId
where A.RoleId = 43 and B.ReservoirId IN (Select ReservoirId from SubmissionStatus where SubmittedById = @id)
END
