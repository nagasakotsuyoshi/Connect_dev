using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoovCards : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData evendata)
    {
        Debug.Log("OnBeginDrag");

    }

    public void OnDrag(PointerEventData evendata)
    {
        Debug.Log("OnDrag");
        this.transform.position = evendata.position;
    }
    public void OnEndDrag(PointerEventData evendata)
    {
        Debug.Log("OnEndDrag");

    }
}
