using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEnd.FluxorImplementation.Stores
{
    public record AppStore(
        bool IsLoading,
        bool IsLoggedIn,
        UserDetail CurrentUserDetail,
        Reservoir CurrentReservoir,
        ReservoirDetailsDTO NewReservoirDetails
    );
}
