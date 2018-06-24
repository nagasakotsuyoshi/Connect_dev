using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player1 : NetworkBehaviour
{
    public GameObject axe;

    void Start()
    {

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
        NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
        obj.GetComponent<Transform>().transform.SetParent(targetCell.transform, false);
        obj.GetComponent<Item>().parentNetId = netId;
        Debug.Log("Axe Spawned");
    }
}