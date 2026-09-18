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

    private Vector3[] GetTopPoints()
    =>  GetListOfSegmentsVectors(PointType.Top); 

    private Vector3[] GetBottomPoints()
    =>  GetListOfSegmentsVectors(PointType.Bottom);

    private void SetupHose()
    {
        Vector3[] topPoints = GetTopPoints();
        Vector3[] bottomPoints = GetBottomPoints();

        for (int i = 0; i < hoseSegments.Length; i++)
        {
            Rigidbody hoseSegment = hoseSegments[i];
            ConfigurableJoint joint = hoseSegment.gameObject.AddComponent<ConfigurableJoint>();
            CapsuleCollider collider = hoseSegment.gameObject.AddComponent<CapsuleCollider>();

            joint.connectedBody = (i == 0) ? hoseAnchor : hoseSegments[i - 1];
            joint.xMotion = ConfigurableJointMotion.Locked;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;

            joint.angularXMotion = ConfigurableJointMotion.Limited;
            joint.angularYMotion = ConfigurableJointMotion.Limited;
            joint.angularZMotion = ConfigurableJointMotion.Limited;

            //SoftJointLimit limit = joint.lowAngularXLimit;
            //limit.limit = -25f;
            //joint.lowAngularXLimit = limit;

            //limit = joint.highAngularXLimit;
            //limit.limit = 25f;
            //joint.highAngularXLimit = limit;

            //SoftJointLimit yLimit = joint.angularYLimit;
            //yLimit.limit = 25f;
            //joint.angularYLimit = yLimit;

            //SoftJointLimit zLimit = joint.angularZLimit;
            //zLimit.limit = 25f;
            //joint.angularZLimit = zLimit;

            //joint.enableCollision = false;

            //SoftJointLimit linearLimit = joint.linearLimit;
            //linearLimit.limit = 25f;
            //joint.linearLimit = linearLimit;

            joint.anchor = hoseSegment.transform.InverseTransformPoint(topPoints[i]);
            joint.enablePreprocessing = true;

            if (i == 0)
            {
                joint.connectedAnchor = hoseAnchor.transform.InverseTransformPoint(bottomPoints[i]);
            }
            else
            {
                joint.connectedAnchor = hoseSegments[i - 1].transform.InverseTransformPoint(bottomPoints[i - 1]);
            }

            collider.direction = 1; // Y-axis


            if (i == hoseSegments.Length - 1)
            {
                XRGrabInteractable grabInteractable = hoseSegment.gameObject.AddComponent<XRGrabInteractable>();
                grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
            }
        }
    }
    
    private Vector3 GetPointInSegmentToConnect(CapsuleCollider hoseCollider, PointType pointType)
    {
        Vector3 centerOfCollider = hoseCollider.bounds.center;
        float radius = hoseCollider.radius;
        float yCoordinateInSegment = (pointType == PointType.Top)? centerOfCollider.y + radius : centerOfCollider.y - radius;
        return new Vector3(centerOfCollider.x, yCoordinateInSegment, centerOfCollider.z);
    }

    enum PointType
    {
        Top,
        Bottom
    }
}
