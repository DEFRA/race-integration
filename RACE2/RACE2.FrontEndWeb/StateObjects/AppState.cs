using RACE2.DataModel;

namespace RACE2.FrontEndWeb.StateObjects
{
    public class AppState
    {
        public string Email { get; set; }
        public UserDetail UserDetail { get; set; }
        public string ReservoirName { get; set; }

        public Reservoir Reservoir { get; set; }

        private string _currentEmail = String.Empty;
        private string _currentReservoirName = String.Empty;
        private Reservoir _currentReservoir=new Reservoir();
        private UserDetail _currentUser=new UserDetail();

        public event EventHandler StateChangeHandler;

        private void StateHasChanged()
        {
            StateChangeHandler?.Invoke(this, EventArgs.Empty);
        }

        public string GetEmail()
        {
            return _currentEmail;
        }
        public void SetEmail(string email)
        {
            _currentEmail = email;
            StateHasChanged();
        }

        public string GetReservoirName()
        {
            return _currentReservoirName;
        }
        public void SetReservoirName(string reservoirName)
        {
            _currentReservoirName = reservoirName;
            StateHasChanged();
        }

        public Reservoir GetReservoir()
        {
            return _currentReservoir;
        }
        public void SetReservoir(Reservoir reservoir)
        {
            _currentReservoir = reservoir;
            StateHasChanged();
        }

        public UserDetail GetUser()
        { return _currentUser; }
        public void SetUser(UserDetail user)
        {
            _currentUser = user;
            StateHasChanged();
        }
    }
}
