using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] Slider _slider;
        
        void Start()
        {
            _slider.ObserveEveryValueChanged(slider => slider.value)
                .Subscribe(value => _animator.speed = value)
                .AddTo(this);
        }
    }
}
