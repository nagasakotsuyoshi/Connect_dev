using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour {

    public GameObject m_coverEnemy;
    public GameObject m_coverMe;
    public List<GameObject> m_Decks = new List<GameObject>();
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;
    public GameObject card5;
    public GameObject card6;
    public GameObject card7;
    public GameObject card8;
    public GameObject card9;
    public GameObject card10;
    public GameObject card11;
    public GameObject card12;
    public GameObject card13;
    public GameObject cardJorker;
    GameObject cardObj;


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

    void InitializeDeck()
    {

        for (int i = 0; i < 48; i++)
        {

            switch (i%12)
            {
                case 1:
                    cardObj = Instantiate(card1);
                    break;
                case 2:
                    cardObj = Instantiate(card2);
                    break;
                case 3:
                    cardObj = Instantiate(card3);
                    break;
                case 4:
                    cardObj = Instantiate(card4);
                    break;
                
                case 5:
                    cardObj = Instantiate(card5);
                    break;
                case 6:
                    cardObj = Instantiate(card6);
                    break;
                case 7:
                    cardObj = Instantiate(card7);
                    break;
                case 8:
                    cardObj = Instantiate(card8);
                    break;
                case 9:
                    cardObj = Instantiate(card9);
                    break;
                case 10:
                    cardObj = Instantiate(card10);
                    break;
                case 11:
                    cardObj = Instantiate(card11);
                    break;
                case 12:
                    cardObj = Instantiate(card12);
                    break;
                default:
                    break;
            }
            NetworkServer.Spawn(cardObj);
            m_Decks.Add(cardObj);
        }
        cardObj = Instantiate(cardJorker);
        NetworkServer.Spawn(cardObj);
        m_Decks.Add(cardObj);
        m_Decks.Add(cardObj);
    }

    
    /*//ボタンを押したらカードを引く ※途中
    void Draw()
    {
        GameObject targetCell = NetworkServer.FindLocalObject(netId);
        cardObj.GetComponent<Transform>().transform.SetParent(targetCell.transform, false);
        cardObj.GetComponent<Item>().parentNetId = netId;
    }*/



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
