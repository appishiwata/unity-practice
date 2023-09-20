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
                .Subscribe(_ =>
                {
                    _particleSystem.GetComponent<ParticleSystem>().Stop();
                    _button.gameObject.SetActive(false);
                })
                .AddTo(this);
        }
    }
}
