public class FireExtinguisherStateMachine
{
    private FireExtinguisherState currentState;
    public FireExtinguisherStateMachine()
    {
        //currentState = new LockedState(this);
    }
    public bool TryPullPin()
    {
        return currentState == FireExtinguisherState.Locked;
    }
    public bool TryPressLever()
    {
        return currentState == FireExtinguisherState.Ready;
    }

    public bool TryReleaseLever()
    {
        return currentState == FireExtinguisherState.Discharging;
    }

    public bool TryEmptyBottle()
    {
        return currentState == FireExtinguisherState.Empty;
    }
}