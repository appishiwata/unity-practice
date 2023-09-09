using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class ItemPickupAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _prefab;
        [SerializeField] private RectTransform _target;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private int _copies = 10;

        public void AnimateItemPickup()
        {
            StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            for (int i = 0; i < _copies; i++)
            {
                var instance = Instantiate(_prefab, _canvas.transform);
                instance.transform.DOMove(_target.position, _duration).SetEase(Ease.InOutQuad).OnComplete(() =>
                {
                    Destroy(instance.gameObject);
                });
                yield return new WaitForSeconds(_duration / _copies);
            }
        }
    }
}