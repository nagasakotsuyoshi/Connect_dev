using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public GameController m_GameController;
    public GameObject axe;
    public GameObject player1Camera;
    public GameObject player2Camera;

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

    public SyncListInt m_IntHands = new SyncListInt();
    public List<GameObject> m_HandCards = new List<GameObject>();

    [SyncVar]
    public int chosenNum;

    void Start()
    {
        Debug.Log(chosenNum);
        if (isLocalPlayer)
        {
            if (chosenNum == 1)
            {
                Instantiate(player1Camera);
            }
            else if (chosenNum == 2)
            {
                Instantiate(player2Camera);
            }
        }
    }

    void Update()
    {
        if (isLocalPlayer) { 
            if (Input.GetKeyDown(KeyCode.Space)) {
                CmdSpawnAxe();
            }
        }
    }

    void Draw()
    {
        m_GameController = GameObject.Find("GameController").GetComponent<GameController>();
        m_IntHands.Add(m_GameController.m_Decks[0]);

        //GameObject targetCell = NetworkServer.FindLocalObject(netId);
        //cardObj.GetComponent<Transform>().transform.SetParent(targetCell.transform, false);
        //cardObj.GetComponent<Item>().parentNetId = netId;
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
    void CmdSpawnAxe()
    {
        GameObject obj = Instantiate<GameObject>(axe, new Vector3(0, 0, 0), Quaternion.identity);
        Cell cell = GameObject.Find("BattleCell0").GetComponent<Cell>();
        NetworkInstanceId netId = cell.GetNetId();
        GameObject targetCell = NetworkServer.FindLocalObject(netId);
        //NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
        NetworkServer.Spawn(obj);
        obj.GetComponent<Transform>().transform.SetParent(targetCell.transform, false);
        obj.GetComponent<Item>().parentNetId = netId;
        Debug.Log("Axe Spawned");
    }
}