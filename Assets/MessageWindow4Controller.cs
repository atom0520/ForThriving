using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindow4Controller : MessageWindowController {

	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}


	public override void Open(object context){
		Dictionary<string, object> dataValue = (Dictionary<string, object>)context;
		if (dataValue.ContainsKey ("message")) {
			completeMessage = (string)dataValue["message"];
		}

		int windowWidth;
		if (dataValue.ContainsKey ("width")) {
			windowWidth = (int)dataValue ["width"];
		} else
			windowWidth = completeMessage.Length * 28;



		Vector2 temp = messageText.gameObject.GetComponent<RectTransform> ().sizeDelta;
		temp.x = windowWidth;
		messageText.gameObject.GetComponent<RectTransform> ().sizeDelta = temp;

		base.Open(context);
	}
}
