using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LeverPress : MonoBehaviour
{
    public FireExtinguisherController extinguisher;
    public XRGrabInteractable _grab;

    void Start()
    {
        _grab = GetComponent<XRGrabInteractable>();
        _grab.selectEntered.AddListener(OnGrab);
        _grab.selectExited.AddListener(OnLeverPressed);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if(!extinguisher.pinRemoved)
        {
            Debug.Log("Cannot press lever until the pin is removed!");
            return;
        }
        extinguisher.PressLever(true);
        // Optional: Add any logic needed when the lever is grabbed
    }

    void OnLeverPressed(SelectExitEventArgs args)
    {
        extinguisher.PressLever(false);
        // Optional: Add any logic needed when the lever is released
    }
}
