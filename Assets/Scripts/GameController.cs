﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour {

    // プレイヤーの種別 
    public enum LocalPlayer
    {
        None,
        Player1,
        Player2
    }

    // ターンの状態種別
    public enum Turn
    {
        Initializing,
        Player1,
        Player2,
        Result,
    }

    // フェイズの状態種別
    // Trello のconnectボードの”もっと細分化”のリストの”ステータス”カードを参照
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
        if (isClient)
        {
            SetLocalPlayer();
        }

        m_TurnText.text = m_Player + ""; //
    }
	
    // Updateはサーバのみで行う
    [ServerCallback]
	void Update () {
        m_Turn = Turn.Player1;
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
        //m_TurnText.text = turn + "";
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
