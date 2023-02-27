using RACE2.DataModel;

namespace RACE2.FrontEndWeb.FluxorImplementation.Stores
{
    public record AppStore(
        bool IsLoading,
        UserDetail CurrentUserDetail,
        Reservoir CurrentReservoir
    );
}
