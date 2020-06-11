namespace HyperCell.Test.Jelly {
    using UnityEngine;
    
    interface IJellyResizer {
        void Resize(Transform transform, JellyInput input, float timeDelta);
    }
}