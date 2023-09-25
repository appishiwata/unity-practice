using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemPickupAnimation : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] Canvas _canvas;
        [SerializeField] GameObject _prefab;
        [SerializeField] GameObject _counterObject;
        [SerializeField] TextMeshProUGUI _counterText;
        [SerializeField] ItemAnimationHelper.RectTransformPair _rectTransformPairs;

        private ItemAnimationHelper _helper;

        async void Start()
        {
            await _button.OnClickAsync();
            
            _helper = new ItemAnimationHelper(Instantiate, Destroy);
            var counterParam = new ItemAnimationHelper.CounterParameter
            {
                CounterObject = _counterObject,
                CounterText = _counterText,
                StartValue = 0,
                EndValue = 10
            };
            var param = new ItemAnimationHelper.AnimationParameter
            {
                Canvas = _canvas,
                Start = _rectTransformPairs._start,
                End = _rectTransformPairs._end,
                Prefab = _prefab,
                Counter = counterParam
            };
            
            await _helper.AnimateItemPickup(param);
        }
    }
}