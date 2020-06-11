namespace HyperCell.Test.Jelly {
    using UnityEngine;
    
    public class CollisionObstacleOfJellyReaction : ICollisionReaction {
        private float obstacleMass;
        private float forceFrom;
        private float forceTo;

        public CollisionObstacleOfJellyReaction(float obstacleMass, float forceFrom, float forceTo) {
            this.obstacleMass = obstacleMass;
            this.forceFrom = forceFrom;
            this.forceTo = forceTo;
        }

        private CollisionObstacleOfJellyReaction() { }

        public void OnCollisionEnter(GameObject collided, GameObject collider, Collision collision) {
            var rb = collided.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.mass = this.obstacleMass;
            var forceDirection = collided.transform.position - collider.transform.position;
            rb.AddForce(forceDirection.normalized * Random.Range(this.forceFrom, this.forceTo));
            collided.layer = (int) Layers.NotActiveObstacle;
        }
    }
}