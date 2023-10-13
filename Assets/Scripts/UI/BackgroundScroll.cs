using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] Image _bgYokonaga;
    [SerializeField] Image _bgTatenaga;
    
    void Start()
    {
        // 横長画像
        // 画像自体を動かす 往復する設定
        _bgYokonaga.transform.DOLocalMoveX(-540f, 10f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
        
        // 縦長画像
        // 画像自体を動かす 端まで行ったら戻る設定
        _bgTatenaga.transform.DOLocalMoveX(-400f, 10f)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
}
