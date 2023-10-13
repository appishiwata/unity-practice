using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] Image _bgYokonaga;
    [SerializeField] Image _bgTatenaga;
    [SerializeField] Image _bgRepeat;
    [SerializeField] Image _bgRepeatMaterial;
    
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

        // 縦長画像
        // 画像自体を動かす 端まで行ったら戻る設定
        // 画像をTiledに設定、Widthを2倍にして、もとのWidth分画像を左に配置、そのWidthの半分動かす
        _bgRepeat.transform.DOLocalMoveX(-540f, 10f)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
        
        // 縦長画像 + マテリアルを使用
        // マテリアルのオフセットを動かす
        // 画像はSimple, SpriteのWrapModeをRepeatに設定
        // マテリアルはUnlit/Transparent
        _bgRepeatMaterial.material.DOOffset(Vector2.zero, 0f);
        _bgRepeatMaterial.material.DOOffset(new Vector2(1f,0f), 10f)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
}
