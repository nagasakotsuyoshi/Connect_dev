using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change : MonoBehaviour {

    // Inspector上で次のシーン名を設定
    public string nextSceneName;

    void changeNext()
    {
        if (Time.timeSinceLevelLoad > 3.0f)
        {
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        }
    }

    // Update is called once per frame
    void Update()
    {
        changeNext();
    }
}
