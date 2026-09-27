using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public GameObject fireExtinguisherGameObject;
    private Lever _lever;
    private CotterPin _cotterPin;
    private Bottle _bottle;
    private Hose _hose;
    private SprayNozzle _sprayNozzle;
    private FireExtinguisherStateController _fireExtinguisherStateController;
    private ISprayBehavior _sprayBehavior;

    public FireExtinguisher(
        CotterPin cotterPin,
        Lever lever,
        Bottle bottle,
        Hose hose,
        SprayNozzle sprayNozzle
    )
    {
        this._cotterPin = cotterPin;
        this._lever = lever;
        this._bottle = bottle;
        this._hose = hose;
        this._sprayNozzle = sprayNozzle;
    }

    // pin jest wyciągnięty

    public bool IsPinPulled => _cotterPin != null && _cotterPin.IsPulled;

    // dzwignia jest wciśnięta

    // Spray nozzle jest chwycony

    // butla jest pusta

    // butla jest pełna

    // butla jest w trakcie opróżniania

    public void Initialize() { }

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
    void Start()
    {
        //CotterPin();
    }

    // Update is called once per frame
    void Update() { }
}
