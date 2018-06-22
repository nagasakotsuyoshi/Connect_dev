using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Cell : NetworkBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    public NetworkInstanceId GetNetId()
    {
        return this.GetComponent<NetworkIdentity>().netId;
    }
}
