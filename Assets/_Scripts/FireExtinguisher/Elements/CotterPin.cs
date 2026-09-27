using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(Rigidbody))]
public class CotterPin : MonoBehaviour
{
    public GameObject cotterPin;
    private XRGrabInteractable _grab;
    private Rigidbody _rigidbody;

    public bool IsPulled { get; private set; }

    void Start()
    {
        if (!TryGetComponent(out _grab) || !TryGetComponent(out _rigidbody))
        {
            Debug.LogError("Required components not found on CotterPin.");
            enabled = false;
            return;
        }
        _grab.selectExited.AddListener(OnPinPulled);
        _rigidbody.isKinematic = true;
    }

    void OnPinPulled(SelectExitEventArgs args)
    {
        _rigidbody.isKinematic = false;
        IsPulled = true;

        // TODO: when CotterPin hit the ground, it should be destroyed or hidden
        //gameObject.SetActive(false); // Hide the pin after it's pulled
    }
}
