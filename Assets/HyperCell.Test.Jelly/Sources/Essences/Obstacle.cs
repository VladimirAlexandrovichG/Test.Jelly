namespace HyperCell.Test.Jelly {
    using DG.Tweening;
    using UnityEngine;

    public class Obstacle : MonoBehaviour {
        public GameObject overcomeEffect;

        void Start() {
            this.overcomeEffect.SetActive(false);
        }

        private void OnTriggerExit(Collider other) {
            this.overcomeEffect.SetActive(true);
            var tr = this.overcomeEffect.transform;
            DOTween.Sequence()
                .Append(tr.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.4f))
                .Insert(1, tr.DOScale(new Vector3(1f, 1f, 1f), 0.25f))
                .OnComplete(() => { this.overcomeEffect.SetActive(false); }).Play();
        }
    }
}
