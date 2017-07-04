using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseInAdvanceGameEvent : CoroutineGameEvent {

	// Use this for initialization
	void Start () {
		trigger = Trigger.Custom;
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.money >= DataManager.instance.releaseNeedMoneyNum) {
			AttemptStartMainGameEvent ();
		}
	}

	IEnumerator ReleaseInAdvance(){
		WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
			new Dictionary<string,object>{
				
				{"message","由于你积累了足够的金钱，你被释放了。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);

		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
			yield return null;
		}

		WindowManager.instance.OpenSingletonWindow ("MessageWindow2", 
			new Dictionary<string,object>{

				{"characterName","狱卒："},
				{"message","好了，你自由了……像个老鼠一样的活在阴影中吧，至少你自由了，不是吗？"},
				{"messageDisplaySpeed", 20.0f},
			}
		);

		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow2")){
			yield return null;
		}

		erased = true;

		if (GameManager.instance.moralValue > 0) {
			Atom.SceneManager.instance.SwapScene (GameManager.instance.goodEndSceneName);
		}else
			Atom.SceneManager.instance.SwapScene (GameManager.instance.noMoralityEndSceneName);
	
	}

	protected override void RefreshCurrEvent(){
		currEvent = ReleaseInAdvance ();

	}
}
