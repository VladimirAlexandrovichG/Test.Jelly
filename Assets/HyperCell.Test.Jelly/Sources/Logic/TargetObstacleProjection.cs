namespace HyperCell.Test.Jelly {
    using UnityEngine;
    public class TargetObstacleProjection : ITargetObstacleProjection {
        private Transform ownerTransform;
        private GameObject targetPlane;
        private GameObject targetBox;
        private float maxRaycastDistance = 100.0f;
        private LayerMask obstacleViewMask;

        private RaycastHit hit;

        public TargetObstacleProjection(Transform ownerTransform, GameObject targetPlane, 
            GameObject targetBox, float maxRaycastDistance, LayerMask obstacleViewMask) 
        {
            this.ownerTransform = ownerTransform;
            this.targetPlane = targetPlane;
            this.targetBox = targetBox;
            this.maxRaycastDistance = maxRaycastDistance;
            this.obstacleViewMask = obstacleViewMask;
        }

        public void UpdateProjection() {
            if (!Physics.Raycast(this.ownerTransform.position, this.ownerTransform.forward,
                out this.hit, this.maxRaycastDistance, this.obstacleViewMask)) return;
            
            this.targetPlane.transform.localPosition = new Vector3(0, 0, this.hit.distance ); 
            this.targetBox.transform.localScale = new Vector3(1, 1, this.hit.distance ); 
            this.targetBox.transform.localPosition = new Vector3(0, 0, this.hit.distance / 2.0f );
        }

        public void SetProjectionActive(bool value) {
            this.targetPlane.SetActive(value);
            this.targetBox.SetActive(value);
        }
    }
}