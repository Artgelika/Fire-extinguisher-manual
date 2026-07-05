using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CotterPin : MonoBehaviour
{
    public FireExtinguisherController extinguisher;
    private XRGrabInteractable _grab;

    void Start()
    {
        _grab = GetComponent<XRGrabInteractable>();
        _grab.selectExited.AddListener(OnPinPulled);
    }

    void OnPinPulled(SelectExitEventArgs args)
    {
        extinguisher.RemovePin();
        Destroy(gameObject);
        // in the next iteration the cotter pin will trigger a main script with whole examination of the fire extinguisher
    }
}
