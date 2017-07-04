using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour {

	public enum TreeType{
		Poppy=0,
		SongTree=1,
		AppleTree=2,
		BingLangTree=3,
		RubberTree=4,
		JunJunTree=5
	}

	[Serializable]
	public class TreeData{
		[SerializeField]
		public TreeType treeType;
		[SerializeField]
		public string treeName;
		[SerializeField]
		public int price;
		[SerializeField]
		public int moralValue;
		[SerializeField]
		public int matureMonthsNum;
		[SerializeField]
		public string intro;
		[SerializeField]
		public int profit;
	}

	public static DataManager instance{get; set;}

	public Dictionary<TreeType,TreeData> treesDataDict;
	[SerializeField]
	public TreeData[] treesData;

	[SerializeField]
	public int monthsPerHandOverMoney;
	[SerializeField]
	public int monthsPerNewLetter;
	[SerializeField]
	public float secondsPerMonth;

	[SerializeField]
	public int initMoney;

	public int initTreeSlotNum;

	[SerializeField]
	public int releaseNeedMoneyNum;

	void Awake(){
		if (instance != null) {
			
			Destroy (gameObject);
			return;
		} else {
			instance = this;
		}
		treesDataDict = new Dictionary<TreeType,TreeData> ();
		foreach (TreeData treeData in treesData) {
			treesDataDict.Add (treeData.treeType, treeData);

		}
	}

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
