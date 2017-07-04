using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameEvent : CoroutineGameEvent {

	//int storyProgress;
	[SerializeField]
	Animator letterBirdAnimator;

	// Use this for initialization
	void Start () {
		trigger = Trigger.Custom;



		//GameManager.instance.storyProgress = 0;
		//Atom.SceneManager.instance.SwapScene ("GameScene2");
		base.Start ();
//		if (GameManager.instance.monthCounter >= 5) {
//			AttemptStartMainGameEvent ();
//		}


	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("GameManager.instance.storyProgress:" + GameManager.instance.storyProgress);
		switch (GameManager.instance.storyProgress) {
		case 0:
			
			AttemptStartMainGameEvent ();

			break;
		case 1:
			if (GameManager.instance.monthCounter >= 6) {
				AttemptStartMainGameEvent ();
			}
			break;
		case 2:
			if (GameManager.instance.monthCounter >= 12) {
				AttemptStartMainGameEvent ();
			}
			break;
		case 3:
			if (GameManager.instance.monthCounter >= 36) {
				AttemptStartMainGameEvent ();
			}
			break;

		}

	}

	IEnumerator GameEvent0(){

		AudioManager.instance.PlayBGM ("usual");

		Fader screenFader = ScreenManager.instance.fadingScreenCover.GetComponent<Fader> ();

		yield return screenFader.StartCoroutine (screenFader.FadeIn (0));


		WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
			new Dictionary<string,object>{
				

				{"message","我犯下了不可饶恕的罪孽，在这里劳苦一生就是我应有的惩罚。但我不会后悔，因为这是为了保护我的家人，我只能那样做，别无选择。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);

		PageWindowController.Page newLog = new PageWindowController.Page(
			GameManager.instance.date+"\n"+GameManager.instance.weather,
			"我犯下了不可饶恕的罪孽，在这里劳苦一生就是我应有的惩罚。但我不会后悔，因为这是为了保护我的家人，我只能那样做，别无选择。");
		GameManager.instance.logs.Add (newLog);

		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
			yield return null;
		}

		yield return new WaitForSeconds (1.0f);

		WindowManager.instance.OpenSingletonWindow("MessageWindow2", 
			new Dictionary<string,object>{

				{"characterName","狱卒"},
				{"message","到了，这块地方就是你的了。我会按期来收钱。。。如果你想要出去，也不是没有办法。。。就看你能不能付出足够的代价了。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);

		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow2")){
			yield return null;
		}

		yield return screenFader.StartCoroutine (screenFader.FadeOut ());

		GameManager.instance.storyProgress += 1;
	
	}

	IEnumerator GameEvent1(){
		
		//TimeManager.instance.paused = true;

		Fader screenFader = ScreenManager.instance.fadingScreenCover.GetComponent<Fader> ();

		yield return screenFader.StartCoroutine (screenFader.FadeIn ());

	

		WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
			new Dictionary<string,object>{
				
		
				{"message","自由的诱惑是如此之大。。。我渴望着和我的家人团聚，然而我现在需要为我犯下的罪行赎罪。。。不，我不能再犯下更多罪恶了。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);

		PageWindowController.Page newLog = new PageWindowController.Page(
			GameManager.instance.date+"\n"+GameManager.instance.weather,
			"自由的诱惑是如此之大。。。我渴望着和我的家人团聚，然而我现在需要为我犯下的罪行赎罪。。。不，我不能再犯下更多罪恶了。");
		GameManager.instance.logs.Add (newLog);


		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
			yield return null;
		}

		yield return new WaitForSeconds (1.0f);
		yield return screenFader.StartCoroutine (screenFader.FadeOut ());

		GameManager.instance.storyProgress += 1;

		//TimeManager.instance.paused = false;
	}



	IEnumerator GameEvent2(){
		//TimeManager.instance.paused = true;

		letterBirdAnimator.SetTrigger ("play");
		yield return new WaitForSeconds (3.0f);


		WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
			new Dictionary<string,object>{

				{"message","你收到了一封信。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);
			
		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
			yield return null;
		}

		Fader screenFader = ScreenManager.instance.fadingScreenCover.GetComponent<Fader> ();

		yield return screenFader.StartCoroutine (screenFader.FadeIn ());
			
		PageWindowController.Page newLetter = new PageWindowController.Page(
			"亲爱的：",
			"我不想让你担心，但是我现在实在承受不住了。。。我们的孩子，他患上了重病，医生说他的情况很危险。。。我从未如此的希望你能在我身边。");
		GameManager.instance.letters.Add (newLetter);

		WindowManager.instance.OpenSingletonWindow ("LetterWindow", 
			new Dictionary<string,object>{

				{"pages",new List<PageWindowController.Page>(){newLetter}},
				{"fadeInDuration", 1.0f},
				{"fadeOutDuration", 1.0f}
			}
		);

		while(WindowManager.instance.IsSingletonWindowOpen("LetterWindow")){
			yield return null;
		}

		yield return new WaitForSeconds (1.0f);

		WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
			new Dictionary<string,object>{
				
				{"message","。。。。。。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);


		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
			yield return null;
		}

		yield return new WaitForSeconds (1.0f);


		WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
			new Dictionary<string,object>{
		 
				{"message","我别无选择。。。我只能和魔鬼做交易，继续犯下更多的罪行，为了我的家人。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);

		PageWindowController.Page newLog = new PageWindowController.Page(
			GameManager.instance.date+"\n"+GameManager.instance.weather,
			"我别无选择。。。我只能和魔鬼做交易，继续犯下更多的罪行，为了我的家人。");
		GameManager.instance.logs.Add (newLog);


		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
			yield return null;
		}

		yield return screenFader.StartCoroutine (screenFader.FadeOut ());

		GameManager.instance.storyProgress += 1;

		//TimeManager.instance.paused = false;
	}

	IEnumerator GameEvent3(){
		//TimeManager.instance.paused = true;
	
		letterBirdAnimator.SetTrigger ("play");
		yield return new WaitForSeconds (3.0f);


		WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
			new Dictionary<string,object>{

				{"message","你收到了一封信。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);

		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
			yield return null;
		}

		Fader screenFader = ScreenManager.instance.fadingScreenCover.GetComponent<Fader> ();

		yield return screenFader.StartCoroutine (screenFader.FadeIn ());

		if (GameManager.instance.moralValue > 0) {
			PageWindowController.Page newLetter = new PageWindowController.Page(
				"亲爱的：",
				"我们的孩子状况稍微好转了一些，但是医生说他现在状况不太稳定。。。我会等待着你出来的那一天的，但是孩子他。。。愿上帝保佑我们。");
			GameManager.instance.letters.Add (newLetter);

			WindowManager.instance.OpenSingletonWindow ("LetterWindow", 
				new Dictionary<string,object>{

					{"pages",new List<PageWindowController.Page>(){newLetter}},
					{"fadeInDuration", 1.0f},
					{"fadeOutDuration", 1.0f}
				}
			);

			while(WindowManager.instance.IsSingletonWindowOpen("LetterWindow")){
				yield return null;
			}

			yield return new WaitForSeconds (1.0f);

			WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
				new Dictionary<string,object>{
					
					{"message","上帝保佑。。。我的孩子好转了。"},
					{"messageDisplaySpeed", 20.0f},
				}
			);
				
			while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
				yield return null;
			}

			WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
				new Dictionary<string,object>{
			
					{"message","请等待着我，我很快就能回去了。"},
					{"messageDisplaySpeed", 20.0f},
				}
			);
				
			PageWindowController.Page newLog = new PageWindowController.Page(
				GameManager.instance.date+"\n"+GameManager.instance.weather,
				"上帝保佑。。。我的孩子好转了。请等待着我，我很快就能回去了。");
			GameManager.instance.logs.Add (newLog);

			while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
				yield return null;
			}

		} else {
			PageWindowController.Page newLetter = new PageWindowController.Page("亲爱的：","我现在好害怕……孩子的情况依然很糟，周围的治安也开始乱了起来……沉迷毒品的野兽随时会为了一点毒资吞噬生命……绝望居高临下的压制着我，让我喘不过气。");
			GameManager.instance.letters.Add (newLetter);

			WindowManager.instance.OpenSingletonWindow ("LetterWindow", 
				new Dictionary<string,object>{
//					{"titleText","亲爱的："},
//					{"contentText", "我现在好害怕……孩子的情况依然很糟，周围的治安也开始乱了起来……沉迷毒品的野兽随时会为了一点毒资吞噬生命……绝望居高临下的压制着我，让我喘不过气。"},
					{"pages",new List<PageWindowController.Page>(){newLetter}},
					{"fadeInDuration", 1.0f},
					{"fadeOutDuration", 1.0f}
				}
			);

			while(WindowManager.instance.IsSingletonWindowOpen("LetterWindow")){
				yield return null;
			}

			yield return new WaitForSeconds (1.0f);

			WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
				new Dictionary<string,object>{
					
					{"message","不够，不够，不够。。。"},
					{"messageDisplaySpeed", 20.0f},
				}
			);



			while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
				yield return null;
			}

			WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
				new Dictionary<string,object>{
					
					{"message","我需要更多，我要更快回去。。。"},
					{"messageDisplaySpeed", 20.0f},
				}
			);

			while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
				yield return null;
			}

			WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
				new Dictionary<string,object>{
					
					{"message","没有时间了。。。"},
					{"messageDisplaySpeed", 20.0f},
				}
			);

			PageWindowController.Page newLog = new PageWindowController.Page(
				GameManager.instance.date+"\n"+GameManager.instance.weather,
				"不够，不够，不够。。。我需要更多，我要更快回去。没有时间了。。。");
			GameManager.instance.logs.Add (newLog);

			while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
				yield return null;
			}
		}

		yield return screenFader.StartCoroutine (screenFader.FadeOut ());

		GameManager.instance.storyProgress += 1;

		//erased = true;

		//TimeManager.instance.paused = false;
	}

	override protected void RefreshCurrEvent(){
		switch (GameManager.instance.storyProgress) {
		case 0:
			currEvent = GameEvent0 ();
			break;
		case 1:
			currEvent = GameEvent1 ();
			break;
		case 2:
			currEvent = GameEvent2 ();

			break;
		case 3:
			currEvent = GameEvent3 ();

			break;
		}
	}
}
