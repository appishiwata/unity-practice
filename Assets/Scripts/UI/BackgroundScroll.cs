using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] Image _backgroundImage;
    void Start()
    {
        // 横長画像
        // 画像自体を動かす 往復する設定
        _backgroundImage.transform.DOLocalMoveX(-540f, 10f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
}
