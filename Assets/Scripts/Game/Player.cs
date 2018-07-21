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
        if (chosenNum == 1) {


        GameObject obj = Instantiate<GameObject>(axe, new Vector3(0, 0, 0), Quaternion.identity);
        Cell cell = GameObject.Find("HandCard/HandCell").GetComponent<Cell>();
        NetworkInstanceId netId = cell.GetNetId();
        GameObject targetCell = NetworkServer.FindLocalObject(netId);
        //NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
        NetworkServer.Spawn(obj);
        obj.GetComponent<Transform>().transform.SetParent(targetCell.transform, false);
        obj.GetComponent<Item>().parentNetId = netId;
        Debug.Log("Axe Spawned");
    }
        else if(chosenNum == 2) {
            GameObject obj = Instantiate<GameObject>(axe, new Vector3(0, 0, 0), Quaternion.identity);
            Cell cell = GameObject.Find("HandCard/HandCell2").GetComponent<Cell>();
            NetworkInstanceId netId = cell.GetNetId();
            GameObject targetCell = NetworkServer.FindLocalObject(netId);
            //NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
            NetworkServer.Spawn(obj);
            obj.GetComponent<Transform>().transform.SetParent(targetCell.transform, false);
            obj.GetComponent<Item>().parentNetId = netId;
            Debug.Log("Axe Spawned");

        }
    }
}