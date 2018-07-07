using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_move2 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onclick_home()
    {

        SceneManager.LoadScene("home");
    }
    public void onclick_accountLogin()
    {

        SceneManager.LoadScene("account login");
    }
    
    public void onclick_roomBattle()
    {

        SceneManager.LoadScene("room battle");
    }
    public void onclick_settingAccount()
    {

        SceneManager.LoadScene("setting account");
    }
    public void onclick_Load()
    {
        SceneManager.LoadScene("Load");
    }
    public void onclick_login()
    {
        SceneManager.LoadScene("login");
    }
    public void onclick_settingSystem()
    {
        SceneManager.LoadScene("setting system");
    }
    
}
