namespace HyperCell.Test.Jelly {
    using UnityEngine;
    using DG.Tweening;
    
    public class WinEffects : IWinEffects {
        private GameObject jelly;
        
        private WinEffects() {}

        public WinEffects(GameObject jelly) {
            this.jelly = jelly;
        }
        public void DoWinEffects() {
            var jumpPosition = this.jelly.transform.position;
            jumpPosition.z += 7;
            this.jelly.transform.DOLocalJump(jumpPosition, 4, 4, 4);
        }
    }
}