using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Hose : MonoBehaviour
{
    public Rigidbody[] hoseSegments;
    public Rigidbody hoseAnchor;

    private void Start()
    {
        SetupHose();
    }

    private Vector3[] GetListOfSegmentsVectors(PointType pointType)
    {
        Vector3[] points = new Vector3[hoseSegments.Length];
        for (int i = 0; i < hoseSegments.Length; i++)
        {
            CapsuleCollider capsuleCollider = hoseSegments[i].GetComponent<CapsuleCollider>();
            points[i] = GetPointInSegmentToConnect(capsuleCollider, pointType);
        }
        return points;
    }

    private Vector3 GetPointInSegmentToConnect(CapsuleCollider hoseCollider, PointType pointType)
    {
        Vector3 localPoint = hoseCollider.center;
        float offset = hoseCollider.height / 2f;

        //Debug.DrawRay(
        //    hoseCollider.transform.position,
        //    hoseCollider.transform.up * 0.1f,
        //    Color.green,
        //    10f
        //);

        //Debug.DrawRay(
        //    hoseCollider.transform.position,
        //    hoseCollider.transform.right * 0.1f,
        //    Color.red,
        //    10f
        //);

        //Debug.DrawRay(
        //    hoseCollider.transform.position,
        //    hoseCollider.transform.forward * 0.1f,
        //    Color.blue,
        //    10f
        //);

        return pointType == PointType.Top
            ? hoseCollider.transform.TransformPoint(localPoint + new Vector3(0, offset, 0))
            : hoseCollider.transform.TransformPoint(localPoint - new Vector3(0, offset, 0));

        //Vector3 centerOfCollider = hoseCollider.bounds.center;
        //float radius = hoseCollider.radius;
        //float yCoordinateInSegment =
        //    (pointType == PointType.Top)
        //        ? centerOfCollider.y + radius
        //        : centerOfCollider.y - radius;
        //var vex = new Vector3(centerOfCollider.x, yCoordinateInSegment, centerOfCollider.z);
        //return vex; // new Vector3(centerOfCollider.x, yCoordinateInSegment, centerOfCollider.z);
    }

    private Vector3[] GetTopPoints() => GetListOfSegmentsVectors(PointType.Top);

    private Vector3[] GetBottomPoints() => GetListOfSegmentsVectors(PointType.Bottom);

    private void SetupHose()
    {
        Vector3[] topPoints = GetTopPoints();
        Vector3[] bottomPoints = GetBottomPoints();

        for (int i = 0; i < hoseSegments.Length; i++)
        {
            Debug.Log(
                $"Segment {i}\n"
                    + $"Position: {hoseSegments[i].position}\n"
                    + $"TOP: {topPoints[i]}\n"
                    + $"BOTTOM: {bottomPoints[i]}\n"
                    + $"TOP-BOTTOM distance: {Vector3.Distance(topPoints[i], bottomPoints[i])}"
            );
            Rigidbody hoseSegment = hoseSegments[i];
            ConfigurableJoint joint = hoseSegment.gameObject.AddComponent<ConfigurableJoint>();
            CapsuleCollider collider = hoseSegment.gameObject.GetComponent<CapsuleCollider>();

            joint.connectedBody = (i == 0) ? hoseAnchor : hoseSegments[i - 1];
            joint.xMotion = ConfigurableJointMotion.Limited;
            joint.yMotion = ConfigurableJointMotion.Limited;
            joint.zMotion = ConfigurableJointMotion.Limited;

            joint.angularXMotion = ConfigurableJointMotion.Limited;
            joint.angularYMotion = ConfigurableJointMotion.Limited;
            joint.angularZMotion = ConfigurableJointMotion.Limited;

            joint.linearLimitSpring = new SoftJointLimitSpring
            {
                spring = 100f, // Zmniejsz z 1000f na 50-100f
                damper = 30f, // Zwiększ tłumienie, żeby wygasić drgania
            };

            joint.enableCollision = false;
            joint.projectionMode = JointProjectionMode.PositionAndRotation;
            joint.projectionDistance = 0.02f; // 1 cm tolerancji przed wymuszeniem pozycji
            //joint.projectionAngle = 6f;

            joint.anchor = hoseSegment.transform.InverseTransformPoint(topPoints[i]);
            joint.enablePreprocessing = true;

            if (i == 0)
            {
                joint.connectedAnchor = hoseAnchor.transform.InverseTransformPoint(bottomPoints[i]);
            }
            else
            {
                joint.connectedAnchor = hoseSegments[i - 1]
                    .transform.InverseTransformPoint(bottomPoints[i - 1]);
            }

            collider.direction = 1; // Y-axis

            if (i == hoseSegments.Length - 1)
            {
                XRGrabInteractable grabInteractable =
                    hoseSegment.gameObject.AddComponent<XRGrabInteractable>();
                grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
            }
        }
    }

    enum PointType
    {
        Top,
        Bottom,
    }
}
