using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Item : NetworkBehaviour {

    [SyncVar(hook = "OnParentNetIdChange")]
    public NetworkInstanceId parentNetId;

    void Start () {
	}

	void Update () {
    }

    public NetworkInstanceId GetNetId()
    {
        return this.GetComponent<NetworkIdentity>().netId;
    }

    [ClientCallback]
    void OnParentNetIdChange(NetworkInstanceId newVal)
    {
        Debug.Log("Attached item net id:" + newVal);
        NetworkInstanceId itemNetId = this.GetNetId();
        GameObject targetItem = ClientScene.FindLocalObject(itemNetId);
        GameObject parentCell = ClientScene.FindLocalObject(newVal);
        targetItem.transform.SetParent(parentCell.transform, false);
    }

}
