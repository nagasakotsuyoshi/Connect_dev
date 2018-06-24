using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class CustomNetworkLobbyManager : NetworkLobbyManager {
    static int chosenNum;

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        if (conn.connectionId == 0)
            chosenNum = 0;
        else
            chosenNum = 1;
        GameObject player = Instantiate(spawnPrefabs[chosenNum]) as GameObject;
        return player;
    }

}
