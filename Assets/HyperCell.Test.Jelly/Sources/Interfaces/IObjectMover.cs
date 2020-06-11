namespace HyperCell.Test.Jelly {
    using UnityEngine;
    
    interface IObjectMover {
        void Move(Vector3 direction, float deltaTime);
        void ForceStop();
    }
}