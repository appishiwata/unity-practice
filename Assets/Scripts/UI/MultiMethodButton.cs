using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MultiMethodButton : MonoBehaviour
{
    [SerializeField] Button _button;
    
    public static Subject<string> OnClickedButtonMethod1 = new();
    public static Subject<string> OnClickedButtonMethod2 = new();

    private void Start()
    {
        _button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                OnClickedButtonMethod1.OnNext("Method1");
                OnClickedButtonMethod2.OnNext("Method2");
            });
    }
}
