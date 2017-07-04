using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoMoneyEndGameEvent : CoroutineGameEvent {

	// Use this for initialization
	void Start () {
		trigger = Trigger.Custom;
		base.Start ();
		AttemptStartMainGameEvent ();
	}

	float lightningDuration = 0.5f;

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator GameEvent(){
		AudioManager.instance.PlayBGM ("failure");

		yield return new WaitForSeconds (2.0f);

		Image sreenCoverImage = ScreenManager.instance.colorScreenCover.GetComponent<Image> ();

		sreenCoverImage.color = new Color (1, 1, 1, 1);

		Color temp;

		while (sreenCoverImage.color.a > 0) {
			yield return null;
			temp = sreenCoverImage.color;
			temp.a -= Time.deltaTime * 1/0.1f;
			sreenCoverImage.color = temp;
		}

		temp = sreenCoverImage.color;
		temp.a = 1;
		sreenCoverImage.color = temp;

		while (sreenCoverImage.color.a > 0) {
			yield return null;
			temp = sreenCoverImage.color;
			temp.a -= Time.deltaTime * 1/0.7f;
			sreenCoverImage.color = temp;

		}

		yield return new WaitForSeconds (0.5f);

		WindowManager.instance.OpenSingletonWindow ("MessageWindow2", 
			new Dictionary<string,object>{
				
				{"characterName","狱卒"},
				{"message","连这都做不到的废物吗……呸，还是在牢里呆着吧，杂碎。本来还因为能从你这里捞到点钱。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);
			
		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow2")){
			yield return null;
		}

		yield return new WaitForSeconds (2.0f);

		WindowManager.instance.OpenSingletonWindow ("MessageWindow4", 
			new Dictionary<string,object>{
				{"message","就这样，你在牢房里度过了黑暗的余生。"},

				{"messageDisplaySpeed", 4.0f},
			}
		);
			
		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow4")){
			yield return null;
		}

		yield return new WaitForSeconds (2.0f);

		Atom.SceneManager.instance.SwapScene ("TitleScene",false,0,true,2);
	}

	protected override void RefreshCurrEvent(){
		currEvent = GameEvent ();

	}
}
