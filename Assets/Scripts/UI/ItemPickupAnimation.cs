using System;
using System.Collections.Generic;
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

        public float InitialDelay { get; set; } = 0.2f;
        public float Duration { get; set; } = 1.0f;
        public int Copies { get; set; } = 10;
    }

    public class ItemPickupAnimation : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;
        [SerializeField] GameObject _prefab;

        [Serializable]
        public class RectTransformPair
        {
            public RectTransform _start;
            public RectTransform _end;
        }
        [SerializeField] List<RectTransformPair> _rectTransformPairs;

        public async void AnimateItemPickup()
        {
            foreach (var t in _rectTransformPairs)
            {
                var animationParameter = new AnimationParameter
                {
                    Canvas = _canvas,
                    Start = t._start,
                    End = t._end,
                    Prefab = _prefab
                };

                await AnimateItemPickup(animationParameter);
            }
        }

        private async Task AnimateItemPickup(AnimationParameter param)
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