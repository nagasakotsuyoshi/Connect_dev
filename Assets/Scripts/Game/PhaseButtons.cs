using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PhaseButtons : MonoBehaviour {

    GameObject m_EndPhaseButton;
    GameObject m_DrawButton;

	void Start () {
        // ボタンオブジェクトの参照を取得
        m_EndPhaseButton = GameObject.Find("EndPhaseButton");
        m_DrawButton = GameObject.Find("DrawButton");

        // ボタンが押された時の処理を登録
        m_EndPhaseButton.GetComponent<Button>().onClick.AddListener(() => OnEndClick(Phase.End));
        m_DrawButton.GetComponent<Button>().onClick.AddListener(() => OnDrawClick());
    }

    //ボタンが押された時の処理
    void OnEndClick(Phase phase)
    {
        Debug.Log("OnEndClick");
        ClientScene.localPlayers[0].gameObject.GetComponent<Player>().CmdChangePhase(phase);
    }

    void OnDrawClick()
    {
        Debug.Log("OnDrawClick");
        ClientScene.localPlayers[0].gameObject.GetComponent<Player>().Draw();
    }
}
