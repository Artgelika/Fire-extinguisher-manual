using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SprayNozzle : MonoBehaviour
{
    //public FireExtinguisherController extinguisher;
    private XRGrabInteractable _grab;
    void Start()
    {
        _grab = GetComponent<XRGrabInteractable>();
        _grab.selectEntered.AddListener(OnGrab);
        _grab.selectExited.AddListener(OnRelease);
    }
    void OnGrab(SelectEnterEventArgs args)
    {
        // Optional: Add any logic needed when the spray nozzle is grabbed
    }
    void OnRelease(SelectExitEventArgs args)
    {
        // Optional: Add any logic needed when the spray nozzle is released
    }
}