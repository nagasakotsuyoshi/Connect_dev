using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sample : MonoBehaviour {
    public GameObject card;

    // Use this for initialization
    void Start () {
        Vector3 tmp = card.transform.position;
        //GameObject.Find("1").transform.position = new Vector3(tmp.x, tmp.y, tmp.z);
        float x = tmp.x;
        float y = tmp.y;
        float z = tmp.z;
        Debug.Log(x);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
