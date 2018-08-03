using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour {
//--------------------------Prameter-----------------------------------------------------------------------
    public GameObject m_coverEnemy;  //相手フィールドの操作制御用
    public GameObject m_coverMe;  //自フィールドの操作制御用

    public List<int> m_Deck = new List<int>();  //山札
    public SyncListInt m_P1Hands = new SyncListInt();  //プレイヤ１の手札数字リスト
    public List<GameObject> m_P1HandObjs = new List<GameObject>();  //プレイヤ１の手札オブジェクトリスト
    public SyncListInt m_P2Hands = new SyncListInt();  //プレイヤ２の手札数字リスト
    public List<GameObject> m_P2HandObjs = new List<GameObject>();  //プレイヤ２の手札オブジェクトリスト

    //----------カードオブジェクト----------------
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
    public GameObject cardJoker;
    public GameObject cardObj;
    //---------------------------------------------

    public GameObject axe;
    public int cardNum;

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
    Text m_PhaseText;
//-----------------------------------------------------------------------------------------------------------

//-----------------------------Function----------------------------------------------------------------------
	void Start () {
        m_TurnText = GameObject.Find("TurnText").GetComponent<Text>();
        m_PhaseText = GameObject.Find("PhaseText").GetComponent<Text>();
        if (isServer)
        {
            ChangeTurn(Turn.Player1);
            ChangePhase(Phase.Draw);
            InitializeDeck();
        }

        if (isClient)
        {
            SetLocalPlayer();
            OnTurnChanged(m_Turn);
            OnPhaseChanged(m_Phase);

        }
    }
	
    // Updateはサーバのみで行う
    //[ServerCallback]
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
        if (isServer)
        {
            // パラメータのデバッグ
            int i;
            for (i = 0; i < m_P1Hands.Count; i++)
                Debug.Log("p1手札の" + (i + 1) + "番目は" + m_P1Hands[i]);
            for (i = 0; i < m_P2Hands.Count; i++)
                Debug.Log("p2手札の" + (i + 1) + "番目は" + m_P2Hands[i]);
            Debug.Log("山札の一番上は" + m_Deck[0]);
        }
    }

    [Client]
    void SetLocalPlayer()
    {
        foreach (Player player in FindObjectsOfType<Player>())
        {
            if (player.isLocalPlayer)
            {
                switch (player.m_chosenNum)
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

    //山札のシャッフル
    [Server]
    void ShuffleDeck()
    {
        m_Deck = m_Deck.OrderBy(i => Guid.NewGuid()).ToList();
    }

    //山札の生成
    [Server]
    void InitializeDeck()
    {
        for (int i = 0; i < 48; i++)
        {
            switch (i%12+1)
            {
                case 1:
                    cardNum = 1;
                    break;
                case 2:
                    cardNum = 2;
                    break;
                case 3:
                    cardNum = 3;
                    break;
                case 4:
                    cardNum = 4;
                    break;               
                case 5:
                    cardNum = 5;
                    break;
                case 6:
                    cardNum = 6;
                    break;
                case 7:
                    cardNum = 7;
                    break;
                case 8:
                    cardNum = 8;
                    break;
                case 9:
                    cardNum = 9;
                    break;
                case 10:
                    cardNum = 10;
                    break;
                case 11:
                    cardNum = 11;
                    break;
                case 12:
                    cardNum = 12;
                    break;
                default:
                    break;
            }
            m_Deck.Add(cardNum);
        }
        cardNum = 13;
        m_Deck.Add(cardNum);
        m_Deck.Add(cardNum);
        cardNum = 14;
        m_Deck.Add(cardNum);
        m_Deck.Add(cardNum);
        ShuffleDeck();
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
        m_PhaseText.text = phase + "";
    }

    //操作制御オブジェクトのアクティブ変更
    void CoverChange(Turn turn)
    {
        Debug.Log("coverChange");
        if(turn == Turn.Player1 && ClientScene.localPlayers[0].gameObject.GetComponent<Player>().m_chosenNum == 1)
        {
            m_coverMe.SetActive(false);
        }
        else if(turn == Turn.Player1 && ClientScene.localPlayers[0].gameObject.GetComponent<Player>().m_chosenNum == 2)
        {
            m_coverMe.SetActive(true);
        }
        else if (turn == Turn.Player2 && ClientScene.localPlayers[0].gameObject.GetComponent<Player>().m_chosenNum == 1)
        {
            m_coverMe.SetActive(true);
        }
        else if (turn == Turn.Player2 && ClientScene.localPlayers[0].gameObject.GetComponent<Player>().m_chosenNum == 2)
        {
            m_coverMe.SetActive(false);
        }
    }

    //山札から一枚引数のプレイヤ手札に移動
    [Server]
    public void Draw(int playerNum)
    {
        int cardNum = m_Deck[0];
        m_Deck.RemoveAt(0);
        if (playerNum == 1)
        {
            m_P1Hands.Add(cardNum);
        }
        else if (playerNum == 2)
        {
            m_P2Hands.Add(cardNum);
        }
        SpawnDrawCard(playerNum, cardNum);
    }

    //山札から引いたカードのスポーン
    [Server]
    void SpawnDrawCard(int playerNum, int num)
    {
        Cell cell = new Cell();
        if (playerNum == 1)
        {
            cell = GameObject.Find("P1Hands/Hand1").GetComponent<Cell>();
        }
        else if(playerNum == 2)
        {
            cell = GameObject.Find("P2Hands/Hand1").GetComponent<Cell>();
        }
        NetworkInstanceId netId = cell.GetNetId();
        GameObject targetCell = NetworkServer.FindLocalObject(netId);
        //num = 15;
        switch (num)
        {
            case 1:
                cardObj = Instantiate<GameObject>(card1, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 2:
                cardObj = Instantiate<GameObject>(card2, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 3:
                cardObj = Instantiate<GameObject>(card3, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 4:
                cardObj = Instantiate<GameObject>(card4, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 5:
                cardObj = Instantiate<GameObject>(card5, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 6:
                cardObj = Instantiate<GameObject>(card6, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 7:
                cardObj = Instantiate<GameObject>(card7, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 8:
                cardObj = Instantiate<GameObject>(card8, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 9:
                cardObj = Instantiate<GameObject>(card9, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 10:
                cardObj = Instantiate<GameObject>(card10, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 11:
                cardObj = Instantiate<GameObject>(card11, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 12:
                cardObj = Instantiate<GameObject>(card12, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 13:
                cardObj = Instantiate<GameObject>(card13, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 14:
                cardObj = Instantiate<GameObject>(cardJoker, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            default:
                cardObj = Instantiate<GameObject>(axe, new Vector3(0, 0, 0), Quaternion.identity);
                break;
        }
        NetworkServer.Spawn(cardObj);
        cardObj.GetComponent<Transform>().transform.SetParent(targetCell.transform, false);
        cardObj.GetComponent<Item>().parentNetId = netId;
        Debug.Log("Draw card Spawned");
    }
//----------------------------------------------------------------------------------------------------------
}
