namespace HyperCell.Test.Jelly {
    using UnityEngine;
    
    public enum JellyState {
        NormalState = 0, 
        ObstacleCollisionEffectState = 1,
        WinState = 2,
        LoseState = 3,
    }
    
    public class Jelly : MonoBehaviour {
        public GameObject targetPlane;
        public GameObject targetBox;

        public JellyState state = JellyState.NormalState;
        public ICollisionReaction collisionJellyOfObstacleReaction;
        public ICollisionReaction collisionObstacleOfJellyReaction;
        public float bounceEffectTimer;
        
        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.layer == (int) Layers.Obstacle)
            {
                this.collisionObstacleOfJellyReaction
                    .OnCollisionEnter(collision.gameObject, this.gameObject, collision);
                
                if(this.state != JellyState.NormalState) return;
                
                this.collisionJellyOfObstacleReaction
                    .OnCollisionEnter(this.gameObject, collision.gameObject, collision);
                this.state = JellyState.ObstacleCollisionEffectState;
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<Finish>() != null)
            {
                this.state = JellyState.WinState;
            }
            
            else if (other.GetComponent<Gap>() != null)
            {
                this.state = JellyState.LoseState;
            }
        }

        public void UpdateJelly(float deltaTime) {
            if (this.state == JellyState.ObstacleCollisionEffectState)
            {
                this.bounceEffectTimer -= deltaTime;

                if (this.bounceEffectTimer <= 0)
                {
                    this.state = JellyState.NormalState;
                }
            }
        }
    }
}