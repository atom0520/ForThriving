using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindow2Controller : MessageWindowController {

	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}
	[SerializeField]
	Text characterNameText;

	public override void Open(object context){
		Dictionary<string, object> dataValue = (Dictionary<string, object>)context;
		if (dataValue.ContainsKey ("characterName")) {
			this.characterNameText.text = (string)dataValue["characterName"]+":";
		}

		base.Open(context);
	}
}
