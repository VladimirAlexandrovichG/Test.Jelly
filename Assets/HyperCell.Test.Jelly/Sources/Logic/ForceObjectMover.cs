namespace HyperCell.Test.Jelly {
    using UnityEngine;
    
    public class ForceObjectMover : IObjectMover {
        private float maxSpeed;
        private float power;
        private Rigidbody rigidbody;
        
        private ForceObjectMover() {}

        public ForceObjectMover(Rigidbody rigidbody, float maxSpeed, float power) {
            this.rigidbody = rigidbody;
            this.maxSpeed = maxSpeed;
            this.power = power;
        }
        public void Move( Vector3 direction, float deltaTime) {
            if(this.rigidbody.velocity.magnitude >= this.maxSpeed) return;
            
            this.rigidbody.AddForce(direction * (deltaTime * this.power));
        }

        public void ForceStop() {
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = Vector3.zero; 
        }
    }
}