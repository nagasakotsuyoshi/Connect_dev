using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headder : MonoBehaviour {

	// Use this for initialization
	void Start () {
<<<<<<< HEAD

        Destroyheadder.DontDestroyOnLoad(this);
    }
	
	// Update is called once per frame
	void Update () {
=======
		
	}
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update () {
>>>>>>> 91325cc608098c2a35e0daebb56406c99522e437
		
	}
}
