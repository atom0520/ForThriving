using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOverMoneyGameEvent : CoroutineGameEvent {

	bool selfSwitch1;

	[SerializeField]
	int moneyNeedPayEachTime;

	// Use this for initialization
	override protected void Start () {
		trigger = Trigger.Custom;
		selfSwitch1 = false;
		base.Start();
	}



	// Update is called once per frame
	void Update () {
		
		if (GameManager.instance.monthCounter >= GameManager.instance.nextHandOverMoneyMonth) {
			AttemptStartMainGameEvent ();
		}
			
		
	}
		
	IEnumerator HandOverGameEvent(){
		//Debug.Log("HandOverGameEvent!");
		//Time.timeScale = 0;
		//TimeManager.instance.paused = true;

		WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
			new Dictionary<string,object>{

				{"message","政府来收租金了。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);

		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
			yield return null;
		}

		if (GameManager.instance.money >= moneyNeedPayEachTime) {
			
			GameManager.instance.money -= moneyNeedPayEachTime;

			WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
				new Dictionary<string,object>{
					
					{"message","政府从你手里拿走了"+moneyNeedPayEachTime.ToString()+"金钱。"},
					{"messageDisplaySpeed", 20.0f},
				}
			);


			while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
				yield return null;
			}

			if (GameManager.instance.hasHandOverMoneyTime == 0) {
				WindowManager.instance.OpenSingletonWindow ("MessageWindow2", 
					new Dictionary<string,object> {

						{ "characterName","狱卒" },
						{ "message","嘿嘿，我没看错你……二百万。当然，每次该交的钱可不能少" },
						{ "messageDisplaySpeed", 20.0f },
					}
				);
			} else {
				WindowManager.instance.OpenSingletonWindow ("MessageWindow2", 
					new Dictionary<string,object> {
						
						{ "characterName","狱卒" },
						{ "message","这次的钱倒是够了……要想出去，可要加快啊……这可是为了你自己啊。没有人会愿意在这里做苦工，对吧？" },
						{ "messageDisplaySpeed", 20.0f },
					}
				);
			}
		

			while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow2")){
				yield return null;
			}

		} else {
			WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
				new Dictionary<string,object>{

					{"message","政府要求金钱"+moneyNeedPayEachTime.ToString()+"，由于你拿不出足够的钱支付政府要求的租金，你被重新逮捕并终生监禁。。。"},
					{"messageDisplaySpeed", 20.0f},
				}
			);

			while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
				yield return null;
			}

			yield return new WaitForSeconds (0.5f);

			Atom.SceneManager.instance.SwapScene (GameManager.instance.noMoneyEndSceneName);

//			Fader screenFader = ScreenManager.instance.fadingScreenCover.GetComponent<Fader> ();
//			yield return screenFader.StartCoroutine (screenFader.FadeIn ());
//
//			yield return new WaitForSeconds (0.5f);
//
//			AudioManager.instance.PlayBGM ("failure");
//
//			yield return new WaitForSeconds (0.5f);
//
//			WindowManager.instance.OpenSingletonWindow ("MessageWindow2", 
//				new Dictionary<string,object>{
//					
//					{"characterName","狱卒"},
//					{"message","连这都做不到的废物吗……呸，还是在牢里呆着吧，杂碎。本来还因为能从你这里捞到点钱。"},
//					{"messageDisplaySpeed", 20.0f},
//				}
//			);
//
//			while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow2")){
//				yield return null;
//			}
//				
//			yield return new WaitForSeconds (1.0f);

			erased = true;

			//Atom.SceneManager.instance.SwapScene ("TitleScene");

			yield break;
		}

		GameManager.instance.hasHandOverMoneyTime += 1;
		GameManager.instance.nextHandOverMoneyMonth += DataManager.instance.monthsPerHandOverMoney;
		//Time.timeScale = 1;
		//TimeManager.instance.paused = false;
	}

	protected override void RefreshCurrEvent(){
		currEvent = HandOverGameEvent();
	}
}
