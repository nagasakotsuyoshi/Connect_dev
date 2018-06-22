using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalWindow : MonoBehaviour {

    public Canvas CanvasModalSetting = null;

    // Use this for initialization
    void Start()
    {
        // ダイアログを表示するときまで、 Canvas を無効にしておく。
        if (CanvasModalSetting != null)
        {
            CanvasModalSetting.enabled = false;
        }
    }

    // クリックされた
    void OnMouseUpAsButton()
    {
        confirmAllHoshiDelete();
    }

    // ダイアログを表示
    public void confirmAllHoshiDelete()
    {
        // Canvas を有効にする
        if (CanvasModalSetting != null)
        {
            CanvasModalSetting.enabled = true;
        }
    }

    // Yes ボタンと関連づけたイベントハンドラ関数
    public void onButtonYes()
    {
        // Canvas を無効にする。(ダイアログを閉じる)
        CanvasModalSetting.enabled = true;

        // アイテムの削除処理(省略)
    }

    // No ボタンと関連づけたイベントハンドラ関数
    public void onButtonNo()
    {
        // Canvas を無効にする。(ダイアログを閉じる)
        CanvasModalSetting.enabled = false;
    }
}