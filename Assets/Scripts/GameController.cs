using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour {


    //ターンのステータス
    public enum Turn
    {
        Initializing,
        Player1,
        Player2,
        Result,
    }

    //フェイズのステータス
    //Trello のconnectボードの”もっと細分化”のリストの”ステータス”カードを参照
    public enum Phase
    {
        Initializing, //初期値
        Draw,    //カードを引く
        UseCard, //カードを使う、置く
        Attack,  //敵に攻撃する
        Defend,  //敵の攻撃から守る
        Decision,//勝敗の判定
        End,     //ターンを終了
        Waiting, //待つ
    }

    // プレイヤーの状態
    [SyncVar(hook = "OnTurnChanged")]
    public Turn m_Turn = Turn.Initializing;
    [SyncVar(hook = "OnPhaseChanged")]
    public Phase m_Phase = Phase.Initializing;


	void Start () {
		
	}
	
	void Update () {
		
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
}
