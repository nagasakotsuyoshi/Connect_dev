using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class CustomNetworkLobbyManager : NetworkLobbyManager {
    static int chosenNum;

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log(conn.connectionId);
        if (conn.connectionId == 0)  //ローカルクライアントであるか(Host起動のみ)
            chosenNum = 1;
        else
            chosenNum = 2;
        GameObject player = Instantiate(spawnPrefabs[0]) as GameObject;
        player.GetComponent<Player>().chosenNum = chosenNum;
        return player;
    }

}
