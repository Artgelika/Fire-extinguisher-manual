using Assets._Scripts.FireExtinguisher.Behaviors.Interfaces;
using Assets._Scripts.FireExtinguisher.Elements;
using Assets._Scripts.FireExtinguisher.StateControl;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField]
    private Lever _lever;
    [SerializeField]
    private CotterPin _cotterPin;
    private Bottle _bottle;
    [SerializeField]
    private Hose _hose;
    [SerializeField]
    private SprayNozzle _sprayNozzle;
    private FireExtinguisherStateController _fireExtinguisherStateController;
    private ISprayBehavior _sprayBehavior;


    // pin jest wyciągnięty

    public bool IsPinPulled => _cotterPin != null && _cotterPin.IsPulled;
    public bool IsLeverPressed => _lever != null && _lever.IsPressed;
    private void Awake()
    {
        _fireExtinguisherStateController = new FireExtinguisherStateController();
        if (_sprayNozzle != null)
        {
            _sprayBehavior = _sprayNozzle.GetComponent<ISprayBehavior>();
        }
        if (_sprayBehavior == null)
        {
            Debug.LogError("No ISprayBehavior found on FireExtinguisher.", this);
            enabled = false;
        }
    }

    private void OnEnable()
    {
        if (_cotterPin != null)
        {
            HandlePinPulled();
            _cotterPin.OnPinPulled += HandlePinPulled;
            //_cotterPin.OnPulled += HandlePinPulled;
        }
        if (_lever != null)
        {
            _lever.OnPressed += HandleLeverPressed;
            _lever.OnReleased += HandleLeverReleased;
        }
    }

    private void OnDisable()
    {
        //if (_cotterPin != null)
        //{
        //    HandlePinPulled();
        //    //_cotterPin.OnPulled -= HandlePinPulled;
        //}
        if (_lever != null)
        {
            _lever.OnPressed -= HandleLeverPressed;
            _lever.OnReleased -= HandleLeverReleased;
        }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }

    private void HandlePinPulled()
    {
        if (IsPinPulled)
        {
            _fireExtinguisherStateController.TryPullPin();
        }
    }

    private void HandleLeverPressed()
    {
        if (_fireExtinguisherStateController.TryPressLever())
        {
            _sprayNozzle.SprayBehavior.StartSpray();
            //_sprayBehavior.StartSpray();
        }
    }

    private void HandleLeverReleased()
    {
        if (_fireExtinguisherStateController.TryReleaseLever())
        {
            _sprayNozzle.SprayBehavior.StopSpray();
            //_sprayBehavior.StopSpray();
        }
    }
}
