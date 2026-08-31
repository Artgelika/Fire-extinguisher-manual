using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Lever : MonoBehaviour
{
    private XRGrabInteractable _grab;
    private Rigidbody _rigidbody;

    private Transform _handTransform;

    private float _handStartAngle;
    private float _leverAngle;

    [SerializeField] private float minAngle = 0f;
    [SerializeField] private float maxAngle = 35f;

    private Quaternion _initialRotation;

    public bool IsPressed { get; private set; }

    void Start()
    {
        if (!TryGetComponent(out _grab) ||
            !TryGetComponent(out _rigidbody))
        {
            Debug.LogError("Required components not found on Lever.");
            enabled = false;
            return;
        }

        _grab.selectEntered.AddListener(OnGrab);
        _grab.selectExited.AddListener(OnRelease);

        _rigidbody.isKinematic = true;

        _initialRotation = transform.localRotation;
    }

    void OnDestroy()
    {
        if (_grab != null)
        {
            _grab.selectEntered.RemoveListener(OnGrab);
            _grab.selectExited.RemoveListener(OnRelease);
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        _handTransform = args.interactorObject.transform;

        _handStartAngle = GetHandAngle();

        Debug.Log("Dźwignia chwycona");
    }

    void OnRelease(SelectExitEventArgs args)
    {
        _handTransform = null;

        Debug.Log("Dźwignia puszczona");
    }

    void Update()
    {
        if (_handTransform == null)
            return;

        float currentHandAngle = GetHandAngle();

        float deltaAngle =
            Mathf.DeltaAngle(
                _handStartAngle,
                currentHandAngle
            );

        _leverAngle = Mathf.Clamp(
            deltaAngle,
            minAngle,
            maxAngle
        );

        transform.localRotation =
            _initialRotation *
            Quaternion.Euler(0f, 0f, _leverAngle);
    }

    private float GetHandAngle()
    {
        Vector3 direction =
            _handTransform.position - transform.position;

        // Obrót wokół osi Z,
        // dlatego ignorujemy wysokość.
        direction.z = 0f;

        return Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;
    }
}