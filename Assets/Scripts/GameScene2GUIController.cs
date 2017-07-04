using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Atom;

public class GameScene2GUIController : MonoBehaviour {

	[SerializeField]
	GameObject treeMenu;

	[SerializeField]
	Toggle treeMemuToggle;

	[SerializeField]
	string sceneName;

	[SerializeField]
	GameObject spladeImageObj;
	GameObject splade;

	[SerializeField]
	PoppyGameEvent poppyGameEvent;

	GraphicRaycaster graphicRaycaster;

	// Use this for initialization
	void Start () {
		graphicRaycaster = GetComponent<GraphicRaycaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		graphicRaycaster.enabled = !GameManager.instance.isMainGameEventCoroutineRunning;
	}

	public void OnClickTreeMenuToggleBtn(){
		if (treeMemuToggle.isOn == false) {
			treeMenu.GetComponent<Animator> ().SetBool ("displayed", true);


			Debug.Log("OnClickTreeMenuToggleBtn!");

			if (GameManager.instance.hasHandOverMoneyTime >= 1 && !GameManager.instance.poppyAppear) {
				//Debug.Log("poppyGameEvent!");
				poppyGameEvent.AttemptStartMainGameEvent ();
			}
		}
		else if(treeMemuToggle.isOn==true)
			treeMenu.GetComponent<Animator> ().SetBool ("displayed", false);
		
	}

	public void OnClickReturnBtn(){
		SceneManager.instance.SwapScene (sceneName);
		//TimeManager.instance.paused = false;
	}

	public void OnClickRemoveTreeBtn(){
		splade = Instantiate (spladeImageObj, transform);
		splade.transform.position = spladeImageObj.transform.parent.position;
	}
}