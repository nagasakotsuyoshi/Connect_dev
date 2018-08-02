using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
//--------------------------Prameter-----------------------------------------------------------------------
    public GameObject card;
    public GameObject player1Camera;
    public GameObject player2Camera;
    [SyncVar]
    public int m_chosenNum;
//---------------------------------------------------------------------------------------------------------

//-------------------------Function------------------------------------------------------------------------
    void Start()
    {
        Debug.Log(m_chosenNum);
        if (isLocalPlayer)
        {
            if (m_chosenNum == 1)
            {
                Instantiate(player1Camera);
            }
            else if (m_chosenNum == 2)
            {
                Instantiate(player2Camera);
            }
        }
    }

    void Update()
    {
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                CmdSpawnCard();
            }
        }
    }

    //GameControllerのDrawメソッドのコマンド
    [Command]
    public void CmdDraw(int playerNum)
    {
        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.Draw(playerNum);
        Debug.Log("CmdDraw");
    }

    [Command]
    public void CmdChangeTurn(Turn turn)
    {
        GameObject.Find("GameController").GetComponent<GameController>().ChangeTurn(turn);
    }

    [Command]
    public void CmdChangePhase(Phase phase)
    {
        GameObject.Find("GameController").GetComponent<GameController>().ChangePhase(phase);
    }

    [Command]
    void CmdSpawnCard()
    {
        GameObject obj = Instantiate<GameObject>(card, new Vector3(0, 0, 0), Quaternion.identity);
        Cell cell = GameObject.Find("BattleCell0").GetComponent<Cell>();
        NetworkInstanceId netId = cell.GetNetId();
        GameObject targetCell = NetworkServer.FindLocalObject(netId);
        //NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
        NetworkServer.Spawn(obj);
        obj.GetComponent<Transform>().transform.SetParent(targetCell.transform, false);
        obj.GetComponent<Item>().parentNetId = netId;
        Debug.Log("Card Spawned");
    }
//---------------------------------------------------------------------------------------------------------
}