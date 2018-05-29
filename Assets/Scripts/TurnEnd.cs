using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnd : MonoBehaviour {

	// Use this for initialization
	public void turnend() {
        if (transform.localEulerAngles.x <= 180)
        {
            transform.Rotate(new Vector3(180, 0, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }
	}
	

}
