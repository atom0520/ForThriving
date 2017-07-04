using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Atom;

public class GameSceneGUIController : MonoBehaviour {

	[SerializeField]
	public Text timeCounterText;

	[SerializeField]
	string swapSceneName;

	[SerializeField]
	Text moneyNumText;

	[SerializeField]
	Text moralValueText;

	[SerializeField]
	Text treeSlotsTestText;

	GraphicRaycaster graphicRayCaster;

	// Use this for initialization
	void Start () {
		graphicRayCaster = GetComponent<GraphicRaycaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		graphicRayCaster.enabled = !GameManager.instance.isMainGameEventCoroutineRunning;

		timeCounterText.text = "第 "+GameManager.instance.monthCounter+" 月";
		moneyNumText.text = GameManager.instance.money.ToString ();
		moralValueText.text = "道德值:"+GameManager.instance.moralValue;

		treeSlotsTestText.text = "";
		for (int i=0;i<GameManager.instance.treeSlots.Length;i++) {
			GameManager.TreeSlot slot = GameManager.instance.treeSlots [i];
			if(slot.treeType != -1)
				treeSlotsTestText.text += "Slot "+i+" TreeType:" + DataManager.instance.treesDataDict[(DataManager.TreeType)slot.treeType].treeName+" ";
			else
				treeSlotsTestText.text += "Slot "+i+" TreeType:" + "-- ";
			treeSlotsTestText.text += "growMonths:" + (int)(slot.growCounter/DataManager.instance.secondsPerMonth)+"\n";
		}
	}

	public void OnClickDiaryWindowToggleBtn(){
		if(WindowManager.instance.IsSingletonWindowOpen("DiaryWindow")!=true)
			WindowManager.instance.OpenSingletonWindow ("DiaryWindow", 
			new Dictionary<string,object>{
				
			});
		else
			WindowManager.instance.CloseSingletonWindow ("DiaryWindow");
	}

	public void OnClickLetterWindowBtn(){
//		if (letterWindowBtnImage.enabled == true)
//			letterWindowBtnImage.enabled = false;

		if(WindowManager.instance.IsSingletonWindowOpen("LetterWindow")!=true)
			WindowManager.instance.OpenSingletonWindow ("LetterWindow", 
				new Dictionary<string,object>{
					{"pages", GameManager.instance.letters}
				});
		else
			WindowManager.instance.CloseSingletonWindow ("LetterWindow");

	
	}

	public void OnClickSwapSceneBtn(){
		
		SceneManager.instance.SwapScene (swapSceneName);

	}

	public void OnClickLogWindowBtn(){

		if(WindowManager.instance.IsSingletonWindowOpen("LogWindow")!=true)
			WindowManager.instance.OpenSingletonWindow ("LogWindow", 
				new Dictionary<string,object>{
					{"pages", GameManager.instance.logs}
				});
		else
			WindowManager.instance.CloseSingletonWindow ("LogWindow");

	}

}
