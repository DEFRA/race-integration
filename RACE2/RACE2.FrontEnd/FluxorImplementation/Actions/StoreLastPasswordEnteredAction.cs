using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEnd.FluxorImplementation.Actions
{
    public class StoreLastPasswordEnteredAction
    {
        public string LastPasswordEntered { get; }
        public StoreLastPasswordEnteredAction(string lastPasswordEntered)
            => LastPasswordEntered = lastPasswordEntered;
    }
}
