using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEndWeb.FluxorImplementation.Stores
{
    public record AppStore(
        bool IsLoading,
        UserDetail CurrentUserDetail,
        Reservoir CurrentReservoir,
        NewReservoirDetails NewReservoirDetails
    );
}
