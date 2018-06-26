using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

    void Start()
    {
        GameObject obj = null;
        for (int i = 0; i < 5; i++)
        {
            obj = once_draw();
            obj.transform.position = new Vector3(obj.transform.position.x + i*10, obj.transform.position.y, obj.transform.position.z);
        }
    }
    public GameObject once_draw()
    {


        GameObject obj = (GameObject)Resources.Load("Prefabs/Cell2d");
        GameObject prefab = (GameObject)Instantiate(obj);
        prefab.transform.SetParent(this.transform,false);
        return prefab;
        //prefab.transform.localScale = new Vector3(50, 70, 0);
        //prefab.AddComponent<DragAndDropCell>();
        //Instantiate(obj, this.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);


    }

}
