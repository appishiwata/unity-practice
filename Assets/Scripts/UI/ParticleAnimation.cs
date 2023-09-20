using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ParticleAnimation : MonoBehaviour
    {
        [SerializeField] ParticleSystem _particleSystem;
        [SerializeField] Button _button;
    
        void Start()
        {
            _button.OnClickAsObservable()
                .Subscribe(_ => _particleSystem.gameObject.SetActive(true))
                .AddTo(this);
        }
    }
}
