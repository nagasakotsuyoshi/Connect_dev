using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateField : MonoBehaviour {
    public GameObject cell;
	// Use this for initialization
	void Start () {
        //GameObject obj = (GameObject)Resources.Load("Prefabs/Cell2d");
        //GameObject prefab = (GameObject)Instantiate(obj,);
        //prefab.transform.SetParent(this.transform, false);
        int i = 670;
        GameObject obj = Instantiate<GameObject>(cell, new Vector3(i,472,0), Quaternion.Euler(new Vector3(0, 0, 0)));
        i += 280;
        GameObject obj2 = Instantiate<GameObject>(cell, new Vector3(i, 472, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        i += 280;
        GameObject obj3 = Instantiate<GameObject>(cell, new Vector3(i, 472, 0), Quaternion.Euler(new Vector3(0, 0, 0)));

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
