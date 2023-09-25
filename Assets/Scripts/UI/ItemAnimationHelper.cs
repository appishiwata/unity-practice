using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using TMPro;

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
            public GameObject CounterObject { get; set; }
            public TextMeshProUGUI CounterText { get; set; }
        }

        [Serializable]
        public class RectTransformPair
        {
            public RectTransform _start;
            public RectTransform _end;
        }

        public void AnimateNumberIncrease(float startValue, float increaseAmount, TextMeshProUGUI textComponent, float duration)
        {
            float endValue = startValue + increaseAmount;
            //textComponent.text = $"{Mathf.RoundToInt(startValue)}";
            DOVirtual.Float(startValue, endValue, duration, value =>
                {
                    textComponent.text = $"{Mathf.RoundToInt(value)}";
                })
                .SetEase(Ease.Linear);
        }

        public async Task AnimateItemPickup(AnimationParameter param)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(param.InitialDelay));

            if (param.CounterObject != null) {
                param.CounterObject.SetActive(true);
                AnimateNumberIncrease(0, 30, param.CounterText, param.Duration);
            }

            for (int i = 0; i < param.Copies; i++)
            {
                var obj = _instantiateFunc(param.Prefab, param.Canvas.transform);
                obj.transform.position = param.Start.position;
                obj.transform.DOMove(param.End.position, param.Duration)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(async () =>
                    {
                        _destroyAction(obj);
                        if (i == param.Copies) {
                            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                            if (param.CounterObject != null) param.CounterObject.SetActive(false);
                        }
                    });
                await UniTask.Delay(TimeSpan.FromSeconds(param.Duration / param.Copies));
            }
        }
    }
}