using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Assets._Scripts.FireExtinguisher.Elements
{
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

        private Vector3 GetPointInSegmentToConnect(
            CapsuleCollider hoseCollider,
            PointType pointType
        )
        {
            Vector3 localPoint = hoseCollider.center;
            float offset = hoseCollider.height / 2f;
            return pointType == PointType.Top
                ? hoseCollider.transform.TransformPoint(localPoint + new Vector3(0, offset, 0))
                : hoseCollider.transform.TransformPoint(localPoint - new Vector3(0, offset, 0));
        }

        private Vector3[] GetTopPoints() => GetListOfSegmentsVectors(PointType.Top);

        private Vector3[] GetBottomPoints() => GetListOfSegmentsVectors(PointType.Bottom);

        private bool IsLastSegment(int index) => index == hoseSegments.Length - 1;

        public void SetupHose()
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
                CapsuleCollider collider = hoseSegment.gameObject.GetComponent<CapsuleCollider>();

                ConfigurableJoint joint = hoseSegment.gameObject.AddComponent<ConfigurableJoint>();

                joint.connectedBody = (i == 0) ? hoseAnchor : hoseSegments[i - 1];
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;

                joint.angularXMotion = ConfigurableJointMotion.Limited;
                joint.angularYMotion = ConfigurableJointMotion.Limited;
                joint.angularZMotion = ConfigurableJointMotion.Limited;

                joint.linearLimitSpring = new SoftJointLimitSpring { spring = 100f, damper = 30f };

                joint.enableCollision = false;
                joint.projectionMode = JointProjectionMode.PositionAndRotation;
                joint.projectionDistance = 0.02f;

                joint.anchor = hoseSegment.transform.InverseTransformPoint(topPoints[i]);
                joint.enablePreprocessing = true;

                if (i == 0)
                {
                    joint.connectedAnchor = hoseAnchor.transform.InverseTransformPoint(
                        bottomPoints[i]
                    );
                }
                else
                {
                    joint.connectedAnchor = hoseSegments[i - 1]
                        .transform.InverseTransformPoint(bottomPoints[i - 1]);
                }

                collider.direction = 1; // Y-axis

                if (IsLastSegment(i))
                {
                    SprayNozzle sprayNozzle = hoseSegment.gameObject.AddComponent<SprayNozzle>();
                    sprayNozzle.SetupSprayNozzle(hoseSegment);
                }
            }
        }

        enum PointType
        {
            Top,
            Bottom,
        }
    }
}
