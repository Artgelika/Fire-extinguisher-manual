using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    private Lever lever;
    private CotterPin cotterPin;
    private Bottle bottle;
    private Hose hose;
    private SprayNozzle sprayNozzle;
    private FireExtinguisherStateMachine fireExtinguisherStateMachine;
    private ISprayBehavior sprayBehavior;

    public FireExtinguisher()
    {
            
    }

    public void Initialize()
    {}

    public void StartDischarge()
    {
        // Implementation for starting discharge
    }

    public void StopDischarge()
    {
        // Implementation for stopping discharge
    }

    public void Reset() { }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //CotterPin();
    }

    // Update is called once per frame
    void Update() { }
}
