using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSpladeController : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler {

	float pressCounter;
	RectTransform rectTransform;
	Vector2 pressedPointerOffset;
	GameObject draggingItem;

	// Use this for initialization
	void Start () {
		rectTransform = gameObject.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown(PointerEventData data){
		//Debug.Log ("OnPointerDown!");

		if (pressCounter > 0) {
			DoubleClick ();
		} else {
			pressCounter = 0.4f;
		}

		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out pressedPointerOffset);

	}

	public void OnDrag(PointerEventData data){
		//Debug.Log ("OnDrag!");
		if (data.button == PointerEventData.InputButton.Left) {
			if (draggingItem == null) {
				draggingItem = Instantiate (gameObject, transform.root);
				draggingItem.GetComponent<Image> ().raycastTarget = false;
				draggingItem.transform.position = transform.position;
				RectTransform draggingItemRectTransform = draggingItem.GetComponent<RectTransform> ();

			
				draggingItemRectTransform.sizeDelta = rectTransform.sizeDelta;
			}


			//transform.SetParent(transform.root);
			Vector2 localPointerPosition;
			//canvasGroup.blocksRaycasts = false;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)draggingItem.transform.parent, Input.mousePosition, data.pressEventCamera, out localPointerPosition))
			{
				draggingItem.transform.localPosition = localPointerPosition - pressedPointerOffset;

			}
		}
	}

	public void OnEndDrag(PointerEventData data){
		
		if (data.button == PointerEventData.InputButton.Left) {
			if (draggingItem != null) {
				GameObject enterObj = data.pointerEnter;
				//Debug.Log ("enterObj:"+enterObj);

				if (enterObj != null && enterObj.tag == "TreeSlot") {
					//Debug.Log ("remove tree!");
					int treeSlotIndex = enterObj.GetComponent<TreeSlotController> ().treeSlotIndex;
					GameManager.instance.treeSlots [treeSlotIndex].treeType = -1;
					GameManager.instance.treeSlots [treeSlotIndex].growCounter = 0;
					ProgramEventManager.instance.DispatchLocalEvent (transform.root.gameObject, "OnTreeSlotNeedRefresh", null);
				}

				Destroy (draggingItem);
			}

		}
	}

	void DoubleClick (){
		
	}
}
