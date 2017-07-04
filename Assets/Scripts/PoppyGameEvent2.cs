using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoppyGameEvent2 : CoroutineGameEvent {


	// Use this for initialization
	void Start () {
		trigger = Trigger.Custom;
		base.Start ();
	}


	IEnumerator GameEvent(){

		GameManager.instance.poppyPlanted = true;

		Fader screenFader = ScreenManager.instance.fadingScreenCover.GetComponent<Fader> ();

		yield return screenFader.StartCoroutine (screenFader.FadeIn ());

		WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
			new Dictionary<string,object>{

				{"message","我败给了魔鬼的诱惑……我不会再犹豫了，为了家人，我可以付出任何代价。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);

		PageWindowController.Page newLog = new PageWindowController.Page(
			GameManager.instance.date+"\n"+GameManager.instance.weather,
			"我败给了魔鬼的诱惑……我不会再犹豫了，为了家人，我可以付出任何代价。");
		GameManager.instance.logs.Add (newLog);


		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
			yield return null;
		}

		yield return screenFader.StartCoroutine (screenFader.FadeOut ());
			
		//erased = true;
	

		//TimeManager.instance.paused = false;

	}

	override protected void RefreshCurrEvent(){
		currEvent = GameEvent ();

	}
}
