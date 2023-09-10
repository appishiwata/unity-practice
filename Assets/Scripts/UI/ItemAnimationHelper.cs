using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace UI
{
    public class ItemAnimationHelper
    {
        private readonly Func<GameObject, Transform, GameObject> _instantiateFunc;
        private readonly Action<GameObject> _destroyAction;

        public ItemAnimationHelper(Func<GameObject, Transform, GameObject> instantiateFunc, Action<GameObject> destroyAction)
        {
            _instantiateFunc = instantiateFunc;
            _destroyAction = destroyAction;
        }

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

        [Serializable]
        public class RectTransformPair
        {
            public RectTransform _start;
            public RectTransform _end;
        }

        public async Task AnimateItemPickup(AnimationParameter param)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(param.InitialDelay));
            for (int i = 0; i < param.Copies; i++)
            {
                var obj = _instantiateFunc(param.Prefab, param.Canvas.transform);
                obj.transform.position = param.Start.position;
                obj.transform.DOMove(param.End.position, param.Duration)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        _destroyAction(obj);
                    });
                await UniTask.Delay(TimeSpan.FromSeconds(param.Duration / param.Copies));
            }
        }
    }
}