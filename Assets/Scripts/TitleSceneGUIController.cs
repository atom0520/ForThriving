using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Atom;

public class TitleSceneGUIController : MonoBehaviour {

	[SerializeField]
	string gameSceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Submit")) {
			GameManager.instance.Init ();
			SceneManager.instance.SwapScene(gameSceneName);
		}

		if (Input.GetButtonDown("Cancel")) {
			Application.Quit ();
		}
	}

//	public void OnClickStartGameBtn(){
//		GameManager.instance.Init ();
//
//		SceneManager.instance.SwapScene(gameSceneName);
//	}
}
