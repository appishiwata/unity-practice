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
            _slider.onValueChanged.AddListener(ChangeAnimatorSpeed);
        }
        
        void ChangeAnimatorSpeed(float value)
        {
            _animator.speed = value;
        }
    }
}
