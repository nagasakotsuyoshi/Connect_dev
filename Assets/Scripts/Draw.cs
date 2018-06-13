using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

    public void once_draw()
    {
        GameObject obj = (GameObject)Resources.Load("Prefabs/Cell");

        Instantiate(obj, this.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);


    }

}
