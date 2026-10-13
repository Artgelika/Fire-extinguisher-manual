namespace Assets._Scripts.FireExtinguisher.StateControl
{
    public class FireExtinguisherStateController
    {
        private FireExtinguisherState currentState;

        public FireExtinguisherStateController()
        {
            currentState = FireExtinguisherState.Locked;
        }

        public FireExtinguisherState CurrentState => currentState;

        public bool TryPullPin()
        {
            if (currentState == FireExtinguisherState.Locked)
            {
                currentState = FireExtinguisherState.Ready;
                return true;
            }
            return false;
        }

        public bool TryPressLever()
        {
            if (currentState == FireExtinguisherState.Ready)
            {
                currentState = FireExtinguisherState.Discharging;
                return true;
            }
            return false;
        }

        public bool TryReleaseLever()
        {
            if (currentState == FireExtinguisherState.Discharging)
            {
                currentState = FireExtinguisherState.Ready;
                return true;
            }
            return false;
        }

        public bool TryEmptyBottle()
        {
            if (currentState == FireExtinguisherState.Discharging)
            {
                currentState = FireExtinguisherState.Empty;
                return true;
            }
            return false;
        }
    }
}
