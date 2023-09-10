using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class AnimationParameter
    {
        public Canvas Canvas { get; set; }
        public RectTransform Start { get; set; }
        public RectTransform End { get; set; }
        public GameObject Prefab { get; set; }
        public float InitialDelay { get; set; }
        public float Duration { get; set; }
        public int Copies { get; set; }
    }

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
            var animationParameter = new AnimationParameter
            {
                Canvas = _canvas,
                Start = _start,
                End = _end,
                Prefab = _item,
                InitialDelay = 0.2f,
                Duration = 1.0f,
                Copies = 10
            };
            await ItemAnimation(animationParameter);
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

        private async Task ItemAnimation(AnimationParameter param)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(param.InitialDelay));
            for (int i = 0; i < param.Copies; i++)
            {
                var obj = Instantiate(param.Prefab, param.Canvas.transform);
                obj.transform.position = param.Start.position;
                obj.transform.DOMove(param.End.position, param.Duration)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        Destroy(obj);
                    });
                await UniTask.Delay(TimeSpan.FromSeconds(param.Duration / param.Copies));
            }
        }
    }
}