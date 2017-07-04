using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreeInfoHoverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
//	[SerializeField]
//	string treeName;
//	[SerializeField]
//	int price;
	[SerializeField]
	TreeItemController itemController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData data){
//		Debug.Log ("OnPointerEnter:" + gameObject);
//		Debug.Log ("transform.position:" + transform.position);

		//Debug.Log ("Camera.main.WorldToScreenPoint (transform.position):" + Camera.main.WorldToScreenPoint (transform.position));
		//Debug.Log ("Camera.main.WorldToViewportPoint(transform.position):" + Camera.main.WorldToViewportPoint(transform.position));

		Vector2 screenPos = Camera.main.WorldToScreenPoint (transform.position);

		WindowManager.instance.OpenSingletonWindow ("TreeInfoWindow", 
			new Dictionary<string,object>{ 
				{"titleText",itemController.treeName},
				{"priceText","价格:"+itemController.price.ToString()},
				{"profitText","利润:"+itemController.profit/10000.0f+"万"},
				{"introText","简介:"+itemController.intro},
				{"position",new Vector3(screenPos.x,screenPos.y+224, 0)}
			}
		);
	}

	public void OnPointerExit(PointerEventData data){
		WindowManager.instance.CloseSingletonWindow ("TreeInfoWindow");

	}
}
