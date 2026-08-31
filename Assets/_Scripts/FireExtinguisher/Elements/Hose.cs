using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Hose : MonoBehaviour
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
        // Optional: Add any logic needed when the hose is grabbed
    }

    void OnRelease(SelectExitEventArgs args)
    {
        // Optional: Add any logic needed when the hose is released
    }
}
