namespace RACE2.FrontEnd.StateObjects
{
    public class AppState
    {
        public string Email { get; set; }
        public string ReservoirName { get; set; }

        private string _currentEmail = String.Empty;
        private string _currentReservoirName = String.Empty;

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
    }
}
