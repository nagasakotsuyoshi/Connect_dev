using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

    public void once_draw()
    {

        GridLayout grid = this.GetComponent<GridLayout>();
        GameObject obj = (GameObject)Resources.Load("Prefabs/Cell");
        GameObject prefab = (GameObject)Instantiate(obj);
        prefab.transform.SetParent(this.transform);

        //prefab.transform.localScale = new Vector3(50, 70, 0);
        prefab.AddComponent<DragAndDropCell>();
        //Instantiate(obj, this.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);


    }

}
