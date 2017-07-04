using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeInfoWindowController : WindowController {
	[SerializeField]
	Text titleText;
	[SerializeField]
	Text introText;
	[SerializeField]
	Text priceText;
	[SerializeField]
	Text profitText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}

	public override void Open(object context)
	{
		Dictionary<string, object> dataValue = (Dictionary<string, object>)context;
		titleText.text = (string)dataValue["titleText"];
		priceText.text = (string)dataValue["priceText"];
		introText.text = (string)dataValue["introText"];
		profitText.text = (string)dataValue["profitText"];
		transform.position = (Vector3)dataValue["position"];
		base.Open(context);
	}
}
