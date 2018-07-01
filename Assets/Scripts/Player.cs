using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public GameObject axe;
    public GameObject player1Camera;
    public GameObject player2Camera;

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