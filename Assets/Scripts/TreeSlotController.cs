using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreeSlotController : MonoBehaviour{

	[SerializeField]
	public int treeSlotIndex;
	[SerializeField]
	Text treeNameText;
	[SerializeField]
	Text remainingMonthsNumText;

	// Use this for initialization
	void Start () {
		treeNameText.text = "";
		remainingMonthsNumText.text = "";
		ProgramEventManager.instance.AddLocalEventListener (transform.root.gameObject, "OnTreeSlotNeedRefresh", 
			new ProgramEventManager.EventListener (OnTreeSlotNeedRefresh));
	    OnTreeSlotNeedRefresh (null);
	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}

//	public void OnPointerEnter(PointerEventData data){
//		//Debug.Log ("TreeSlotController.OnPointerEnter!");
//	}

	void OnTreeSlotNeedRefresh(object data){
		//Debug.Log ("OnTreeSlotNeedRefresh");
		if ( GameManager.instance.treeSlots == null)
			return;

		int treeType = GameManager.instance.treeSlots [treeSlotIndex].treeType;
		if (treeType == -1) {
			treeNameText.text = "";
			remainingMonthsNumText.text = "";
		} else {
			DataManager.TreeData treeData = DataManager.instance.treesDataDict [(DataManager.TreeType)treeType];
			treeNameText.text = treeData.treeName;
			int remainingMonthsNum = treeData.matureMonthsNum - (int)(GameManager.instance.treeSlots [treeSlotIndex].growCounter / DataManager.instance.secondsPerMonth);
			remainingMonthsNumText.text = "还有" + remainingMonthsNum.ToString () + "月成熟";
		}

	}

	void OnDestroy(){
		ProgramEventManager.instance.RemoveLocalEventListener (transform.root.gameObject, "OnTreeSlotNeedRefresh", 
			new ProgramEventManager.EventListener (OnTreeSlotNeedRefresh));
	}
}
