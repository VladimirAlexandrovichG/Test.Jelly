namespace HyperCell.Test.Jelly {
    using UnityEngine;
    
    public class JellyResizer : IJellyResizer {
        private float maxScale;
        private float minScale;

        public JellyResizer(Vector3 localScale, float maxScaleFactor, float minScaleFactor) {
            this.maxScale = localScale.y * maxScaleFactor;
            this.minScale = localScale.y * minScaleFactor;
        }

        public void Resize(Transform transform, JellyInput input, float timeDelta) {
            if (input.frameSwipeDelta > 0.0f && transform.localScale.y < this.maxScale)
            {
                this.ResizeHelper(transform, input, Time.deltaTime);
            }

            else if (input.frameSwipeDelta < 0.0f && transform.localScale.y > this.minScale)
            {
                this.ResizeHelper(transform, input, Time.deltaTime);
            }
        }
        
        private void ResizeHelper(Transform transform, JellyInput input, float deltaTime) {
            var curScale = transform.localScale;
            var newScale = curScale;

            var scaledFrameDelta = input.frameSwipeDelta * deltaTime;
            
            newScale.x -= scaledFrameDelta;
            newScale.y += scaledFrameDelta;
            
            if(newScale.y > maxScale || newScale.y < this.minScale) return;

            var newPosition = transform.localPosition;
            newPosition.y += scaledFrameDelta / 2.0f;

            transform.localScale = newScale;
            transform.localPosition = newPosition;
        }
    }
}