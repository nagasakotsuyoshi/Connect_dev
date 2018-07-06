using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void onclick_Home()
    {

        SceneManager.LoadScene("Home");
    }
    public void onclick_Battle()
    {

        SceneManager.LoadScene("Battle");
    }
    public void onclick_Result()
    {

        SceneManager.LoadScene("Result");
    }
    public void onclick_SelectRoom()
    {

        SceneManager.LoadScene("SelectRoom");
    }
    public void onclick_WaitingRoom()
    {

        SceneManager.LoadScene("WaitingRoom");
    }
    public void onclick_Load()
    {
        SceneManager.LoadScene("Load");
    }
    public void onclick_Login()
    {
        SceneManager.LoadScene("Login");
    }
}
