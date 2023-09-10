using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
        
        [SerializeField] RectTransform _start;
        [SerializeField] RectTransform _end;
        [SerializeField] GameObject _item;

        public async void AnimateItemPickup()
        {
            await ItemAnimation(_canvas, _start, _end, _item, 0.2f, 1f, 10);
            //StartCoroutine(Animate());
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
        
        private async Task ItemAnimation(Canvas canvas, RectTransform start, RectTransform end, GameObject prefab, float initialDelay, float duration, int copies)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(initialDelay));
            for (int i = 0; i < copies; i++)
            {
                var obj = Instantiate(prefab, canvas.transform);
                obj.transform.position = start.position;
                obj.transform.DOMove(end.position, duration)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        Destroy(obj);
                    });
                await UniTask.Delay(TimeSpan.FromSeconds(duration / copies));
            }
        }
    }
}