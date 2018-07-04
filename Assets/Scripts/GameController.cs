using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour {

    public GameObject m_coverEnemy;
    public GameObject m_coverMe;

    // プレイヤーの種別 
    public enum LocalPlayer
    {
        None,
        Player1,
        Player2
    }

    // ターンの状態
    [SyncVar(hook = "OnTurnChanged")]
    public Turn m_Turn = Turn.Initializing;

    // フェイズの状態
    [SyncVar(hook = "OnPhaseChanged")]
    public Phase m_Phase = Phase.Initializing;

    // ローカルクライアントにあるプレイヤーの参照
    public LocalPlayer m_Player = LocalPlayer.None;

    // ターン種別Textの参照
    Text m_TurnText;

	void Start () {
        m_TurnText = GameObject.Find("TurnText").GetComponent<Text>();
       if (isServer)
        {
            ChangeTurn(Turn.Player1);
        }

        if (isClient)
        {
            SetLocalPlayer();
            OnTurnChanged(m_Turn);

        }
    }
	
    // Updateはサーバのみで行う
    [ServerCallback]
	void Update () {
        if(m_Turn == Turn.Player1)
        {
            switch (m_Phase)
            {
                case Phase.End:
                    ChangeTurn(Turn.Player2);
                    ChangePhase(Phase.Initializing);
                    break;
                default:
                    break;
            }
        }
        else if(m_Turn == Turn.Player2)
        {
            switch (m_Phase)
            {
                case Phase.End:
                    ChangeTurn(Turn.Player1);
                    ChangePhase(Phase.Initializing);
                    break;
                default:
                    break;
            }
        }
	}

    [Client]
    void SetLocalPlayer()
    {
        foreach (Player player in FindObjectsOfType<Player>())
        {
            if (player.isLocalPlayer)
            {
                switch (player.chosenNum)
                {
                    case 1:
                        m_Player = LocalPlayer.Player1;
                        break;
                    case 2:
                        m_Player = LocalPlayer.Player2;
                        break;
                    default:
                        Debug.Log("想定外のプレイヤー");
                        break;
                }
            }
        }
    }

    // ターンの状態を変更する
    [Server]
    public void ChangeTurn(Turn turn)
    {
        // m_Phaseを変更すれば、あとはhookによりOnTurnChangedが実行される
        m_Turn = turn;
    }

    void OnTurnChanged(Turn turn)
    {
        m_TurnText.text = turn + "";
        CoverChange(turn);

    }

    // フェイズの状態を変更する
    [Server]
    public void ChangePhase(Phase phase)
    {
        // m_Phaseを変更すれば、あとはhookによりOnPhaseChangedが実行される
        m_Phase = phase;
    }

    void OnPhaseChanged(Phase phase)
    {
        
    }

    void CoverChange(Turn turn)
    {
        Debug.Log("coverChange");
        if(turn == Turn.Player1 && ClientScene.localPlayers[0].gameObject.GetComponent<Player>().chosenNum == 1)
        {
            m_coverMe.SetActive(false);
        }
        else if(turn == Turn.Player1 && ClientScene.localPlayers[0].gameObject.GetComponent<Player>().chosenNum == 2)
        {
            m_coverMe.SetActive(true);
        }
        else if (turn == Turn.Player2 && ClientScene.localPlayers[0].gameObject.GetComponent<Player>().chosenNum == 1)
        {
            m_coverMe.SetActive(true);
        }
        else if (turn == Turn.Player2 && ClientScene.localPlayers[0].gameObject.GetComponent<Player>().chosenNum == 2)
        {
            m_coverMe.SetActive(false);
        }
    }
}
