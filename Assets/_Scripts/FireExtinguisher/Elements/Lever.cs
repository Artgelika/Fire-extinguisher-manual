using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Assets._Scripts.FireExtinguisher.Elements
{
    public class Lever : MonoBehaviour
    {
        [SerializeField] private float minAngle = 0f;
        [SerializeField] private float maxAngle = 35f;
        [SerializeField, Range(0f, 90f)] private float pressThresholdAngle = 30f;

        private XRGrabInteractable _grabInteractable;
        private Rigidbody _rigidbody;
        private Transform _handTransform;
        private float _handStartAngle;
        private Quaternion _initialRotation;
        private float _leverAngle;

        public bool IsPressed { get; private set; }
        public event Action OnPressed;
        public event Action OnReleased;

        public float LeverAngle => _leverAngle;

        private void Awake()
        {
            if (!TryGetComponent(out _grabInteractable) || !TryGetComponent(out _rigidbody))
            {
                Debug.LogError("[Lever] Required components not found. Disabling component.");
                enabled = false;
                return;
            }

            _rigidbody.isKinematic = true;
            _initialRotation = transform.localRotation;
        }

        private void OnEnable()
        {
            if (_grabInteractable != null)
            {
                _grabInteractable.selectEntered.AddListener(HandleSelectEntered);
                _grabInteractable.selectExited.AddListener(HandleSelectExited);
            }
        }

        private void OnDisable()
        {
            if (_grabInteractable != null)
            {
                _grabInteractable.selectEntered.RemoveListener(HandleSelectEntered);
                _grabInteractable.selectExited.RemoveListener(HandleSelectExited);
            }
        }

        private void HandleSelectEntered(SelectEnterEventArgs args)
        {
            _handTransform = args.interactorObject?.transform;
            if (_handTransform != null)
                _handStartAngle = GetHandAngle();
        }

        private void HandleSelectExited(SelectExitEventArgs args)
        {
            _handTransform = null;
            transform.localRotation = _initialRotation;
            _leverAngle = 0f;
            UpdatePressState(false);
        }

        private void Update()
        {
            if (_handTransform == null)
                return;

            float currentHandAngle = GetHandAngle();
            float deltaAngle = Mathf.DeltaAngle(_handStartAngle, currentHandAngle);
            _leverAngle = Mathf.Clamp(deltaAngle, minAngle, maxAngle);

            transform.localRotation = _initialRotation * Quaternion.Euler(0f, 0f, _leverAngle);

            bool pressedNow = _leverAngle >= pressThresholdAngle;
            UpdatePressState(pressedNow);
        }

        private void UpdatePressState(bool pressedNow)
        {
            if (pressedNow && !IsPressed)
            {
                IsPressed = true;
                OnPressed?.Invoke();
            }
            else if (!pressedNow && IsPressed)
            {
                IsPressed = false;
                OnReleased?.Invoke();
            }
        }

        private float GetHandAngle()
        {
            Vector3 direction = _handTransform.position - transform.position;
            direction.z = 0f;
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
    }
}
