using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CotterPin : MonoBehaviour
{
    public GameObject cotterPin;
    private XRGrabInteractable _grab;
    private Rigidbody _rigidbody;

    void Start()
    {
        _grab = GetComponent<XRGrabInteractable>();
        _rigidbody = GetComponent<Rigidbody>();
        _grab.selectExited.AddListener(OnPinPulled);
        _rigidbody.isKinematic = true;
    }

    void OnPinPulled(SelectExitEventArgs args)
    {
        _rigidbody.isKinematic = false;
    }
}
