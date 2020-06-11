namespace HyperCell.Test.Jelly {
    using UnityEngine;
    using DG.Tweening;
    
    public class CameraMoveRelative : ICameraMover {
        private Transform containerTransform;
        private Transform cameraTransform;
        private Transform targetTransform;
        private CameraMoveConfig config;

        public CameraMoveRelative(Transform cameraTransform, Transform targetTransform,  CameraMoveConfig config) {
            this.containerTransform = cameraTransform;
            this.cameraTransform = this.containerTransform.GetChild(0);
            this.targetTransform = targetTransform;
            this.config = config;
        }

        public void SetMenuPosition() {
            this.cameraTransform.rotation = Quaternion.Euler(this.config.menuStateRotation);
            this.cameraTransform.position = this.config.menuStatePosition;
            
            var newCamPos = this.containerTransform.position;
            newCamPos.z = this.targetTransform.position.z - this.config.distance;
            this.containerTransform.position = newCamPos;
        }

        public void MoveToWinPosition() {
            this.cameraTransform.DOLocalRotate(
                this.config.winStateRotation,
                this.config.moveToWinStateDuration);
            this.cameraTransform.DOLocalMove(
                this.config.winStatePosition,
                this.config.moveToWinStateDuration);
        }

        public void MoveToGamePosition() {
            this.cameraTransform.DOLocalRotate(
                this.config.gameStateRotation,
                this.config.moveToGameStateDuration);
            this.cameraTransform.DOLocalMove(
                this.config.gameStatePosition,
                this.config.moveToGameStateDuration);
        }

        public void MoveCamera(float timeDelta) {
            var newCamPos = this.containerTransform.position;
            newCamPos.z = this.targetTransform.position.z - this.config.distance;
            this.containerTransform.position = newCamPos;
        }
    }
}