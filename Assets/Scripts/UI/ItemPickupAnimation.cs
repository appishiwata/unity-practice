using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ItemPickupAnimation : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;
        [SerializeField] GameObject _prefab;
        [SerializeField] List<RectTransformPair> _rectTransformPairs;

        private ItemAnimationHelper _helper;

        async void Start()
        {
            _helper = new ItemAnimationHelper(Instantiate, Destroy);
            foreach (var t in _rectTransformPairs)
            {
                AnimationParameter param = new AnimationParameter
                {
                    Canvas = _canvas,
                    Start = t._start,
                    End = t._end,
                    Prefab = _prefab
                };
                
                await _helper.AnimateItemPickup(param);
            }
        }
    }
}