namespace HyperCell.Test.Jelly {
    using UnityEngine;
    public class JellyInputController : IJellyInputController {
        private Vector2 prevFramePos;
        private float frameDelta;
        
        public JellyInput GetInput() {
            if (Input.GetMouseButtonDown(0))
            {
                this.prevFramePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                this.frameDelta = Input.mousePosition.y - this.prevFramePos.y;
                this.prevFramePos = Input.mousePosition;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                this.frameDelta = 0;
            }
            
            return new JellyInput(this.frameDelta);
        }
    }
}