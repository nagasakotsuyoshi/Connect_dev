using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            once_draw();
            
        }
    }
    public void once_draw()
    {


        GameObject obj = (GameObject)Resources.Load("Prefabs/Cell");
        GameObject prefab = (GameObject)Instantiate(obj);
        prefab.transform.SetParent(this.transform,false);

        //prefab.transform.localScale = new Vector3(50, 70, 0);
        //prefab.AddComponent<DragAndDropCell>();
        //Instantiate(obj, this.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);


    }

}
