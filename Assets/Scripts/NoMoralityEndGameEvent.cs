using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMoralityEndGameEvent : CoroutineGameEvent {

	[SerializeField]
	GameObject bloodCover;
	[SerializeField]
	GameObject shakeBgImage;
	// Use this for initialization
	void Start () {
		trigger = Trigger.Custom;
		base.Start ();
		AttemptStartMainGameEvent ();
	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}

	IEnumerator GameEvent(){
		AudioManager.instance.PlayBGM ("badEnd");

		yield return new WaitForSeconds (2.0f);

		float screenShakeCounter = 0.25f;

		//Vector3 trueCameraPos = Camera.main.transform.position;
		AudioManager.instance.PlaySoundEffect ("Hit");
		yield return new WaitForSeconds (0.1f);
		Vector3 trueBgPos = shakeBgImage.transform.position;

		while (screenShakeCounter>0) {
			yield return null;


			shakeBgImage.transform.position = new Vector3(trueBgPos.x+(Mathf.PingPong (Time.time, 0.1f)-0.05f)*3, trueBgPos.y, trueBgPos.z);
			//Camera.main.transform.position = new Vector3(trueCameraPos.x+(Mathf.PingPong (Time.time, 0.1f)-0.05f)*3, trueCameraPos.y, trueCameraPos.z);

			screenShakeCounter -= Time.deltaTime;
		}

		//Camera.main.transform.position = trueCameraPos;
		shakeBgImage.transform.position = trueBgPos;

		bloodCover.SetActive (true);
		yield return new WaitForSeconds (1.0f);

		while (bloodCover.GetComponent<SpriteRenderer> ().color.a > 0) {
			
			Color temp = bloodCover.GetComponent<SpriteRenderer> ().color;
				temp.a -= 0.05f;
			bloodCover.GetComponent<SpriteRenderer> ().color = temp;
			yield return null;
		}

		bloodCover.SetActive (false);

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



		WindowManager.instance.OpenSingletonWindow ("MessageWindow2", 
			new Dictionary<string,object>{
	
				{"characterName","混混"},
				{"message","嘿嘿，没想到看起来一般，钱不少啊。能买不少粉了，嘻嘻嘻……"},
				{"messageDisplaySpeed", 20.0f},
			}
		);
			
		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow2")){
			yield return null;
		}

		yield return new WaitForSeconds (3.0f);

		Atom.SceneManager.instance.SwapScene ("TitleScene",false,0,true,2);
	}

	override protected  void RefreshCurrEvent(){
		currEvent = GameEvent ();
	}
}
