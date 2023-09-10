using System;
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
        [SerializeField] Canvas _canvas;
        [SerializeField] RectTransform _start1;
        [SerializeField] RectTransform _end1;
        [SerializeField] RectTransform _start2;
        [SerializeField] RectTransform _end2;
        [SerializeField] GameObject _prefab;

        public async void AnimateItemPickup()
        {
            var animationParameter = new AnimationParameter
            {
                Canvas = _canvas,
                Start = _start1,
                End = _end1,
                Prefab = _prefab,
                InitialDelay = 0.2f,
                Duration = 1.0f,
                Copies = 10
            };
            await ItemAnimation(animationParameter);
            var animationParameter2 = new AnimationParameter
            {
                Canvas = _canvas,
                Start = _start2,
                End = _end2,
                Prefab = _prefab,
                InitialDelay = 0.2f,
                Duration = 1.0f,
                Copies = 10
            };
            await ItemAnimation(animationParameter2);
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