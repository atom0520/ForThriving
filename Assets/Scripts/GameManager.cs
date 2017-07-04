using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {


	public class TreeSlot{
		
		public int treeType;
	
		public float growCounter;

	}

//	public class Letter{
//		public Letter(string title, string content){
//			this.title = title;
//			this.content = content;
//		}
//		public string title;
//		public string content;
//	}

	public int storyProgress{get; set;}
	public int hasHandOverMoneyTime{get; set;}

	public int monthCounter{ get; set;}

	public string date{
		get{
			return (2017+(monthCounter+6-1)/12).ToString()+"年"+((monthCounter+6-1)%12+1).ToString()+"月"+"18日";
		}
	}

	public string weather{ get; set;}

	public int nextHandOverMoneyMonth{ get; set;}

	public int nextNewLetterMonth{ get; set;}

	public static GameManager instance {get; set;}

	public bool isMainGameEventCoroutineRunning { get; set;}

//	public Dictionary<DataManager.TreeType, int> hillTreesNum{ get; set;}
//	[SerializeField]
//	int hillCapacity;
	//int remainingHillCapacity;

//	List<string> diaryTexts;


	public TreeSlot[] treeSlots { get; set;}

	public int money {get; set;}

	public int moralValue { get; set; }

	[SerializeField]
	public string noMoneyEndSceneName;
	[SerializeField]
	public string goodEndSceneName;
	[SerializeField]
	public string noMoralityEndSceneName;


	public List<PageWindowController.Page> letters { get; private set;}
	public List<PageWindowController.Page> logs { get; private set;}

	public bool poppyAppear{get; set;}
	public bool poppyPlanted{get; set;}

	void Awake(){
		

		if (instance != null) {
			Destroy (gameObject);
			return;
		} else {
			instance = this;
		}

	
	}

	// Use this for initialization
	void Start () {
		treeSlots = new TreeSlot[DataManager.instance.initTreeSlotNum];

		for(int i=0;i<DataManager.instance.initTreeSlotNum;i++) {
			treeSlots [i] = new TreeSlot ();
		}
		this.Init ();

		letters = new List<PageWindowController.Page> ();
		logs = new List<PageWindowController.Page> ();
	}

	public void Init(){
		weather = "雨";
		poppyAppear = false;
		poppyPlanted = false;
		TimeManager.instance.timeCounter = 0;
		monthCounter = 0;
		hasHandOverMoneyTime = 0;
		storyProgress = 0;
		nextHandOverMoneyMonth = DataManager.instance.monthsPerHandOverMoney;
		nextNewLetterMonth = DataManager.instance.monthsPerNewLetter;
		money = DataManager.instance.initMoney;

		for(int i=0;i<DataManager.instance.initTreeSlotNum;i++) {
			treeSlots [i].treeType = -1;
			treeSlots [i].growCounter = 0;
		}
	}

	// Update is called once per frame
	void Update () {
//		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name != timedSceneName)
//			return;

		if (TimeManager.instance == null || TimeManager.instance.IsTimeGoing()==false)
			return;
		
		monthCounter = (int)(TimeManager.instance.timeCounter / DataManager.instance.secondsPerMonth);

		foreach (TreeSlot slot in treeSlots) {
			if (slot == null) {
				//Debug.Log ("slot == null");
			} else {
				//Debug.Log ("slot != null");
			}

			if (slot.treeType != -1) {
				slot.growCounter += Time.deltaTime;

				DataManager.TreeData treeData = 
					DataManager.instance.treesDataDict[(DataManager.TreeType)slot.treeType];

				if (slot.growCounter >= treeData.matureMonthsNum*DataManager.instance.secondsPerMonth) {

					StartCoroutine(Harvest(slot));

				}
			}
		}


	}

	IEnumerator Harvest(TreeSlot slot){

		DataManager.TreeData treeData = 
			DataManager.instance.treesDataDict[(DataManager.TreeType)slot.treeType];
		money += treeData.profit;
		moralValue += treeData.moralValue;
		slot.growCounter = 0;

		while (isMainGameEventCoroutineRunning) {
			yield return null;
		}

		while (WindowManager.instance.HasNonsingletonWindowOpen("OKBtnWindow")) {
			yield return null;
		}

		//Debug.Log ("open harvest window!");

		//TimeManager.instance.paused = true;
		WindowManager.instance.CreateNonsingletonWindow("OKBtnWindow", new Dictionary<string,object> {
			{"titleText","收获提示"},
			{"contentText",treeData.treeName+"成熟啦，"+"收获"+treeData.profit+"金钱。"},

		});

		while (WindowManager.instance.HasNonsingletonWindowOpen("OKBtnWindow")) {
			yield return null;
		}

		//TimeManager.instance.paused = false;
	}

	public TreeSlot GetAwailableSlot(){
		foreach (TreeSlot slot in treeSlots) {
			if (slot.treeType == -1) {
				return slot;
			}
		}
		return null;
	}


}
