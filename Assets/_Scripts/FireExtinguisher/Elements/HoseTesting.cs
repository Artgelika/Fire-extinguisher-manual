using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HoseTesting : MonoBehaviour
{
    public Rigidbody[] hoseSegments;
    public Rigidbody hoseAnchor;

    private void Start()
    {
        // 1. Zwiększamy dokładność obliczeń fizyki dla całej sceny (zabezpieczenie przed jitterem)
        Physics.defaultSolverIterations = 12;
        Physics.defaultSolverVelocityIterations = 12;

        SetupHose();
    }

    private void SetupHose()
    {
        int count = hoseSegments.Length;

        for (int i = 0; i < count; i++)
        {
            Rigidbody currentSegment = hoseSegments[i];

            // 1. Zmiana trybu detekcji kolizji na ciągły
            currentSegment.collisionDetectionMode = CollisionDetectionMode.Continuous;
            currentSegment.interpolation = RigidbodyInterpolation.Interpolate;

            // Masa i tłumienie
            currentSegment.mass = Mathf.Lerp(1.0f, 0.2f, (float)i / count);
            currentSegment.linearDamping = 5f; // Wysokie tłumienie liniowe
            currentSegment.angularDamping = 15f; // Bardzo wysokie tłumienie kątowe

            if (!currentSegment.TryGetComponent<CapsuleCollider>(out var collider))
            {
                collider = currentSegment.gameObject.AddComponent<CapsuleCollider>();
            }

            collider.direction = 2; // Oś Z
            float halfHeight = collider.height / 2f;

            ConfigurableJoint joint = currentSegment.gameObject.AddComponent<ConfigurableJoint>();
            joint.enableCollision = false;

            // Anchor w punkcie styku
            joint.anchor = new Vector3(0, 0, halfHeight);

            if (i == 0)
            {
                joint.connectedBody = hoseAnchor;
                joint.connectedAnchor = hoseAnchor.transform.InverseTransformPoint(
                    currentSegment.transform.TransformPoint(joint.anchor)
                );

                if (hoseAnchor.TryGetComponent<Collider>(out var anchorCollider))
                {
                    Physics.IgnoreCollision(collider, anchorCollider);
                }
            }
            else
            {
                Rigidbody previousSegment = hoseSegments[i - 1];
                joint.connectedBody = previousSegment;

                var prevCollider = previousSegment.GetComponent<CapsuleCollider>();
                float prevHalfHeight = prevCollider.height / 2f;

                joint.connectedAnchor = new Vector3(0, 0, -prevHalfHeight);

                Physics.IgnoreCollision(collider, prevCollider);
            }

            // Zablokuj pozycję
            joint.xMotion = ConfigurableJointMotion.Locked;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;

            // Uproszczone gięcie - Free z napędem sprężynowym (Drives) zamiast ciasnych limitów
            // To całkowicie eliminuje drgania od PhysX!
            joint.angularXMotion = ConfigurableJointMotion.Free;
            joint.angularYMotion = ConfigurableJointMotion.Free;
            joint.angularZMotion = ConfigurableJointMotion.Locked;

            // Slinger/Dampener na obrót zamiast sztywnych limitów
            JointDrive drive = new JointDrive
            {
                positionSpring = 100f,
                positionDamper = 20f,
                maximumForce = float.MaxValue,
            };

            joint.slerpDrive = drive;
            joint.rotationDriveMode = RotationDriveMode.Slerp;

            joint.projectionMode = JointProjectionMode.PositionAndRotation;
            joint.projectionDistance = 0.01f;
            joint.projectionAngle = 5f;

            joint.enablePreprocessing = false;

            if (i == count - 1)
            {
                var grabInteractable = currentSegment.gameObject.AddComponent<XRGrabInteractable>();
                grabInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous;
            }
        }
    }
}
