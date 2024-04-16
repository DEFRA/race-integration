using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEndApp.FluxorImplementation.Stores
{
    public record AppStore(
        bool IsLoading,
        bool IsLoggedIn        
    );
}
