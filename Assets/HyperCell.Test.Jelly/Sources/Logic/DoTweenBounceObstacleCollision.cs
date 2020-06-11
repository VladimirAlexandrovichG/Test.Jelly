namespace HyperCell.Test.Jelly {
    using DG.Tweening;
    using UnityEngine;
    
    public class DoTweenBounceObstacleJellyOfCollisionReaction : ICollisionReaction {
        private float bounceStateDuration;
        private float bounceDistance;
        private float bouncePower;
        private Jelly jelly;

        public DoTweenBounceObstacleJellyOfCollisionReaction(Jelly jelly, float bounceStateDuration, float bounceDistance, float bouncePower) {
            this.bounceStateDuration = bounceStateDuration;
            this.bounceDistance = bounceDistance;
            this.bouncePower = bouncePower;
            this.jelly = jelly;
        }

        public void OnCollisionEnter(GameObject collided, GameObject collider, Collision collision) {
            this.jelly.bounceEffectTimer = this.bounceStateDuration;
            var jumpPosition = this.jelly.transform.localPosition;
            jumpPosition.z -= this.bounceDistance;
            this.jelly.transform.DOLocalJump(jumpPosition, this.bouncePower, 1, this.bounceStateDuration);
        }
    }
}