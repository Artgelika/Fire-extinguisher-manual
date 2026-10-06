using Assets._Scripts.FireExtinguisher.Behaviors.Interfaces;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Assets._Scripts.FireExtinguisher.Elements
{
    [RequireComponent(typeof(ISprayBehavior))]
    public class SprayNozzle : MonoBehaviour
    {
        //[SerializeField]
        private Transform sprayPoint;
        public Transform SprayPoint => sprayPoint;
        private ISprayBehavior _sprayBehavior;
        public ISprayBehavior SprayBehavior => _sprayBehavior;

        private void Awake()
        {
            _sprayBehavior = GetComponent<ISprayBehavior>();

            if (_sprayBehavior == null)
            {
                Debug.LogError("No ISprayBehavior found on SprayNozzle.", this);
            }
        }
        public void SetupSprayNozzle(Rigidbody hoseSegment)
        {
            XRGrabInteractable grabInteractable =
                        hoseSegment.gameObject.AddComponent<XRGrabInteractable>();
            grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        }

    }
}