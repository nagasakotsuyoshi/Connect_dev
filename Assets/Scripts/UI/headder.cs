using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class headder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void Awake()
    {
        Destroyheadder.DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update () {
        if  (SceneManager.GetActiveScene().name == "Login") {
                Destroy(this.gameObject);
            }
        if (SceneManager.GetActiveScene().name == "Load")
        {
            Destroy(this.gameObject);
        }

    }
}
