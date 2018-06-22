﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Drag and Drop item.
/// </summary>
public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler//ドラッグ開始前の状態、ドラック中、ドラック後のメソッドがある
{
	public static bool dragDisabled = false;										// Drag start global disable

	public static DragAndDropItem draggedItem;                                      // Item that is dragged now
	public static GameObject icon;                                                  // Icon of dragged item
	public static DragAndDropCell sourceCell;                                       // From this cell dragged item is

	public delegate void DragEvent(DragAndDropItem item);
	public static event DragEvent OnItemDragStartEvent;                             // Drag start event
	public static event DragEvent OnItemDragEndEvent;                               // Drag end event

	private static Canvas canvas;                                                   // Canvas for item drag operation
	private static string canvasName = "DragAndDropSprite";                   		// Name of canvas
	private static int canvasSortOrder = 100;										// Sort order for canvas
    private static SpriteRenderer sr;
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		if (canvas == null)
		{
			GameObject canvasObj = new GameObject(canvasName);
			sr = canvasObj.AddComponent<SpriteRenderer>();
            //canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            //canvas.sortingOrder = canvasSortOrder;
            sr.sortingOrder = 0;
            sr.sortingLayerName = "UI";

		}
	}

	/// <summary>
	/// This item started to drag.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (dragDisabled == false)
		{
			sourceCell = GetCell();                       							// Remember source cell　セルの場所を把握
			draggedItem = this;                                             		// Set as dragged item
			// Create item's icon
			icon = new GameObject();
            //icon.transform.SetParent(canvas.transform);
            icon.transform.SetParent(sr.transform);
			icon.name = "Icon";
            //Image myImage = GetComponent<Image>();
            //myImage.raycastTarget = false;                                        	// Disable icon's raycast for correct drop handling
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.sortingLayerName = "UI";
            //Image iconImage = icon.AddComponent<Image>();
            SpriteRenderer iconSprite = icon.AddComponent<SpriteRenderer>();
            iconSprite.sortingLayerName = "UI";
            //iconImage.raycastTarget = false;
            //iconImage.sprite = myImage.sprite;                                      //画像データをコピー
            iconSprite.sprite = mySprite.sprite;
            //RectTransform iconRect = icon.GetComponent<RectTransform>();
            Transform iconTran = icon.GetComponent<Transform>();

            // Set icon's dimensions
            Transform myTran = GetComponent<Transform>();
            //iconRect.pivot = new Vector2(0.5f, 0.5f);
            //iconRect.anchorMin = new Vector2(0.5f, 0.5f);
            //iconRect.anchorMax = new Vector2(0.5f, 0.5f);
            //iconRect.sizeDelta = new Vector2(myRect.rect.width, myRect.rect.height);
            iconTran.localScale = new Vector3(myTran.localScale.x*50, myTran.localScale.y*50, 0);
            if (OnItemDragStartEvent != null)                                       //DragAndDropCellのOnAnyItemDragStartメソッドを実行させるかどうか
            {
				OnItemDragStartEvent(this);                                			// Notify all items about drag start for raycast disabling
			}
		}
	}

	/// <summary>
	/// Every frame on this item drag.
	/// </summary>
	/// <param name="data"></param>
	public void OnDrag(PointerEventData data)                                       //引数はマウス／タッチを行ったときに関連する情報のデータ
	{
		if (icon != null)
		{
			icon.transform.position = Input.mousePosition;                          // Item's icon follows to cursor in screen pixels
		}

    }

	/// <summary>
	/// This item is dropped.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnEndDrag(PointerEventData eventData)
	{
		ResetConditions();
	}

	/// <summary>
	/// Resets all temporary conditions.
	/// </summary>
	private void ResetConditions()
	{
		if (icon != null)
		{
			Destroy(icon);                                                          // Destroy icon on item drop
		}
		if (OnItemDragEndEvent != null)
		{
			OnItemDragEndEvent(this);                                       		// Notify all cells about item drag end
		}
		draggedItem = null;
		icon = null;
		sourceCell = null;
	}

	/// <summary>
	/// Enable item's raycast.
	/// </summary>
	/// <param name="condition"> true - enable, false - disable </param>
	public void MakeRaycast(bool condition)
	{
		Image image = GetComponent<Image>();
		if (image != null)
		{
			image.raycastTarget = condition;
		}
	}

	/// <summary>
	/// Gets DaD cell which contains this item.
	/// </summary>
	/// <returns>The cell.</returns>
	public DragAndDropCell GetCell()
	{
		return GetComponentInParent<DragAndDropCell>();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		ResetConditions();
	}



}
