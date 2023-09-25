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
        
        public class CounterParameter
        {
            public GameObject CounterObject { get; set; }
            public TextMeshProUGUI CounterText { get; set; }
            public float StartValue { get; set; } 
            public float EndValue { get; set; }   
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
            public CounterParameter Counter { get; set; }
        }

        [Serializable]
        public class RectTransformPair
        {
            public RectTransform _start;
            public RectTransform _end;
        }

        private void AnimateNumberIncrease(float startValue, float endValue, TextMeshProUGUI textComponent, float duration)
        {
            DOVirtual.Float(startValue, endValue, duration, value =>
                {
                    textComponent.text = $"{Mathf.RoundToInt(value)}";
                })
                .SetEase(Ease.Linear);
        }

        public async Task AnimateItemPickup(AnimationParameter param)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(param.InitialDelay));

            if (param.Counter != null) {
                param.Counter.CounterObject.SetActive(true);
                AnimateNumberIncrease(param.Counter.StartValue, param.Counter.EndValue, param.Counter.CounterText, param.Duration);
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
                            if (param.Counter != null)
                            {
                                param.Counter.CounterObject.SetActive(false);
                            }
                        }
                    });
                await UniTask.Delay(TimeSpan.FromSeconds(param.Duration / param.Copies));
            }
        }
    }
}