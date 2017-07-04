using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEndGameEvent : CoroutineGameEvent {

	// Use this for initialization
	void Start () {
		trigger = Trigger.Custom;
		base.Start ();
		AttemptStartMainGameEvent ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator GameEvent(){
		AudioManager.instance.PlayBGM ("goodEnd");

		yield return new WaitForSeconds (3.0f);

		Fader fader = ScreenManager.instance.fadingScreenCover.GetComponent<Fader> ();
		yield return fader.StartCoroutine (fader.FadeIn(2));

		yield return new WaitForSeconds (1.0f);

		WindowManager.instance.OpenSingletonWindow ("MessageWindow4", 
			new Dictionary<string,object>{
				{"message","我  回  来  了"},

				{"messageDisplaySpeed", 5.0f},
			}
		);

	

		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow4")){
			yield return null;
		}

		yield return new WaitForSeconds (2.0f);

	
		Atom.SceneManager.instance.SwapScene ("TitleScene",false,0,true,2);
	}

	override protected  void RefreshCurrEvent(){
		currEvent = GameEvent ();
	}
}
