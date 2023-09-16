using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] Slider _slider;
        
        [SerializeField] Animator _roofAnimator;
        [SerializeField] Button _roofOpenButton;
        [SerializeField] Button _roofCloseButton;
        
        void Start()
        {
            _slider.ObserveEveryValueChanged(slider => slider.value)
                .Subscribe(value => _animator.speed = value)
                .AddTo(this);

            _roofOpenButton.OnClickAsObservable()
                .Subscribe(_ => _roofAnimator.SetBool("IsOpen", true))
                .AddTo(this);

            _roofCloseButton.OnClickAsObservable()
                .Subscribe(_ => _roofAnimator.SetBool("IsOpen", false))
                .AddTo(this);
            
            // TODO 最初に一度だけアニメーションが実行されてしまう。
        }
    }
}
