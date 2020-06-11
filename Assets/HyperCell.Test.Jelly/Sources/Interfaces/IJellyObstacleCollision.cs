namespace HyperCell.Test.Jelly {
    using UnityEngine;
    
    public interface ICollisionReaction {
        void OnCollisionEnter(GameObject collided, GameObject collider, Collision collision);
    }
}