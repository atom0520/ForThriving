using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

	public static TimeManager instance{ get; set;}

	public float timeCounter { get; set;}
	public bool paused { get; set;}



	[SerializeField]
	string timedSceneName;

	void Awake(){
		if (instance != null) {
			Destroy (gameObject);
			return;
		}
		else
			instance = this;
	}

	// Use this for initialization
	void Start () {
		paused = false;
//		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name != timedSceneName)
//			paused = true;
		timeCounter = 0;
	}


	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("TimeManager.Update:"+timeCounter);

		if(!IsTimeGoing())
			return;
		
		timeCounter += Time.deltaTime;

	
	}

	public bool IsTimeGoing(){
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name != timedSceneName)
			return false;

		if (GameManager.instance.isMainGameEventCoroutineRunning==true)
			return false;

		if (WindowManager.instance.openedWindowNum > 0)
			return false;

		if (Atom.SceneManager.instance.currSwapSceneCoroutine != null)
			return false;

		if (paused)
			return false;

		return true;
	}


}
