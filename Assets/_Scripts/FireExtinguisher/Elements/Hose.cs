using UnityEngine;

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

            if (hoseSegments == null || topPoints == null || bottomPoints == null)
                return;

            int count = hoseSegments.Length;
            if (topPoints.Length < count) count = topPoints.Length;
            if (bottomPoints.Length < count) count = bottomPoints.Length;

            for (int i = 0; i < count; i++)
            {
                Rigidbody hoseSegment = hoseSegments[i];
                Vector3 top = topPoints[i];
                Vector3 bottom = bottomPoints[i];

                Debug.Log(
                    $"Segment {i}\n"
                        + $"Position: {hoseSegment.position}\n"
                        + $"TOP: {top}\n"
                        + $"BOTTOM: {bottom}\n"
                        + $"TOP-BOTTOM distance: {Vector3.Distance(top, bottom)}"
                );

                CapsuleCollider collider = hoseSegment.gameObject.GetComponent<CapsuleCollider>();
                ConfigurableJoint joint = CreateConfiguredJoint(hoseSegment.gameObject);

                joint.connectedBody = (i == 0) ? hoseAnchor : hoseSegments[i - 1];

                joint.anchor = hoseSegment.transform.InverseTransformPoint(top);
                if (i == 0)
                {
                    joint.connectedAnchor = hoseAnchor.transform.InverseTransformPoint(bottom);
                }
                else
                {
                    joint.connectedAnchor = hoseSegments[i - 1].transform.InverseTransformPoint(
                        bottomPoints[i - 1]
                    );
                }

                collider.direction = 1; // Y-axis

                if (IsLastSegment(i))
                {
                    SprayNozzle sprayNozzle = hoseSegment.gameObject.AddComponent<SprayNozzle>();
                    sprayNozzle.SetupSprayNozzle(hoseSegment);
                }
            }
        }

        private ConfigurableJoint CreateConfiguredJoint(GameObject go)
        {
            var joint = go.AddComponent<ConfigurableJoint>();

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
            joint.enablePreprocessing = true;

            return joint;
        }

        enum PointType
        {
            Top,
            Bottom,
        }
    }
}
