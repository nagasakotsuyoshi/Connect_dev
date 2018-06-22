using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    public GameObject axe;

    void Start()
    {
        //DragAndDropCell cell = GameObject.Find("BattleCell4").GetComponent<DragAndDropCell>();
        //Debug.Log(cell);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSpawnIt();
        }
    }

    [Command]
    void CmdSpawnIt()
    {
        Debug.Log("Spawned.");
        GameObject obj = Instantiate<GameObject>
            (
                axe,
                transform.position,
                Quaternion.Euler(new Vector3(0, 0, 0))
            );
        NetworkServer.Spawn(obj);
        DragAndDropItem item = obj.GetComponent<DragAndDropItem>();
        DragAndDropCell cell = GameObject.Find("BattleCell4").GetComponent<DragAndDropCell>();
        item.transform.SetParent(cell.transform, false);
        item.transform.localPosition = Vector3.zero;
        item.MakeRaycast(true);

        //NetworkServer.Spawn(obj);
    }
}