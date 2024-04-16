using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEndWebapp.FluxorImplementation.Stores
{
    public record AppStore(
        bool IsLoading,
        bool IsLoggedIn        
    );
}
