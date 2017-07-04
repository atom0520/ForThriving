using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TreeItemDragController : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler {

	float pressCounter;
	//CanvasGroup canvasGroup;
	Vector2 pressedPointerOffset;
	GameObject draggingItem;

	RectTransform rectTransform;

	[SerializeField]
	TreeItemController itemController;

	[SerializeField]
	PoppyGameEvent2 poppyGameEvent2;

	// Use this for initialization
	void Start () {
		rectTransform = gameObject.GetComponent<RectTransform>();
		//canvasGroup = gameObject.GetComponent<CanvasGroup> ();
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
				draggingItem.transform.position = transform.position;
				draggingItem.GetComponent<CanvasGroup> ().blocksRaycasts = false;

				RectTransform draggingItemRectTransform = draggingItem.GetComponent<RectTransform> ();



				//draggingItem = gameObject;
				draggingItemRectTransform.sizeDelta = rectTransform.sizeDelta;

			}
		

			//transform.SetParent(transform.root);
			Vector2 localPointerPosition;
			//canvasGroup.blocksRaycasts = false;
			//GetComponent<Image>().raycastTarget = false;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)draggingItem.transform.parent, Input.mousePosition, data.pressEventCamera, out localPointerPosition))
			{
				draggingItem.transform.localPosition = localPointerPosition - pressedPointerOffset;

			}
		}
	}

	public void OnEndDrag(PointerEventData data){
		Debug.Log ("OnDrag!");
		if (data.button == PointerEventData.InputButton.Left) {

			if (draggingItem != null) {
				
				GameObject enterObj = data.pointerEnter;

				if (enterObj == null || enterObj.tag != "TreeSlot") {
					Destroy (draggingItem);
					return;
				}
					
				int price = DataManager.instance.treesDataDict [itemController.treeType].price;
				if (GameManager.instance.money >= price) {
					

					//GameManager.TreeSlot awailableSlot = GameManager.instance.GetAwailableSlot ();
					GameManager.TreeSlot awailableSlot = 
						GameManager.instance.treeSlots [enterObj.GetComponent<TreeSlotController> ().treeSlotIndex];

					if (awailableSlot.treeType != -1) {
						//Debug.Log ("空间不足！");
//						WindowManager.instance.OpenSingletonWindow ("OKBtnWindow", new Dictionary<string,object> {
//							{ "titleText","种植失败" },
//							{ "contentText","对不起，整个山都被你种满啦！" },
//						});

						WindowManager.instance.OpenSingletonWindow ("OKBtnWindow", new Dictionary<string,object> {
							{ "titleText","种植失败" },
							{ "contentText","对不起，这块地已经种了其他树！" },
						});
					} else {
						

						awailableSlot.treeType = (int)itemController.treeType;

						if (itemController.treeType == DataManager.TreeType.Poppy && !GameManager.instance.poppyPlanted) {
							Debug.Log ("GameManager.instance.poppyPlanted:" + GameManager.instance.poppyPlanted);
							poppyGameEvent2.AttemptStartMainGameEvent ();
						}
							

						awailableSlot.growCounter = 0;

						GameManager.instance.money -= price;
						GameManager.instance.moralValue += DataManager.instance.treesDataDict [itemController.treeType].moralValue;

						ProgramEventManager.instance.DispatchLocalEvent(transform.root.gameObject, "OnTreeSlotNeedRefresh", null);
					}
						

				}
				else{
					//Debug.Log ("钱不足！");
					WindowManager.instance.OpenSingletonWindow("OKBtnWindow", new Dictionary<string,object>{
						{"titleText","种植失败"},
						{"contentText","对不起，这种树您种不起。。。"},
					});
				}



				Destroy (draggingItem);
			}
		
		}
	}

	void DoubleClick(){
	}
}
