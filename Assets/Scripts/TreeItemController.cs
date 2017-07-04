using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeItemController : MonoBehaviour {

	[SerializeField]
	public DataManager.TreeType treeType;

//	[SerializeField]
	public string treeName {get; set;}
//	[SerializeField]
	public int price{get; set;}
//	[SerializeField]
	public string intro{get; set;}

	public int profit{get; set;}

//	[SerializeField]
//	Text infoText;
	[SerializeField]
	Text buttonText;

	// Use this for initialization
	void Start () {
		treeName = DataManager.instance.treesDataDict[treeType].treeName;
		buttonText.text = treeName;
		price = DataManager.instance.treesDataDict[treeType].price;
		intro = DataManager.instance.treesDataDict[treeType].intro;
		profit = DataManager.instance.treesDataDict [treeType].profit;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
