using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoppyGameEvent : CoroutineGameEvent {

	[SerializeField]
	GameObject poppyTreeItem;
//	[SerializeField]
//	Toggle treeMenuToggle;
	// Use this for initialization
	void Start () {
		trigger = Trigger.Custom;

		if(GameManager.instance.poppyAppear)
			poppyTreeItem.SetActive (true);

		base.Start ();
	}


	IEnumerator GameEvent(){
		//TimeManager.instance.paused = true;
		//treeMenuToggle.interactable = false;
		WindowManager.instance.OpenSingletonWindow ("MessageWindow2", 
			new Dictionary<string,object>{
				
				{"characterName","供应处"},
				{"message","如果我没猜错的话，你现在很想要钱是不是？我这里有点好东西……你知道这是什么，对吧？……这可是我好不容易整来的，你懂我的意思吧，嘻嘻。"},
				{"messageDisplaySpeed", 20.0f},
			}
		);


		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow2")){
			yield return null;
		}

		poppyTreeItem.SetActive (true);
		GameManager.instance.poppyAppear = true;

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

		WindowManager.instance.OpenSingletonWindow ("MessageWindow", 
			new Dictionary<string,object>{
				
				{"message","这美丽又危险的花朵充满了诱惑……这会带来大量的利益。然而，我不能这么做……无论如何都不能"},
				{"messageDisplaySpeed", 20.0f},
			}
		);

		PageWindowController.Page newLog = new PageWindowController.Page(
			GameManager.instance.date+"\n"+GameManager.instance.weather,
			"这美丽又危险的花朵充满了诱惑……这会带来大量的利益。然而，我不能这么做……无论如何都不能");
		GameManager.instance.logs.Add (newLog);

		while(WindowManager.instance.IsSingletonWindowOpen("MessageWindow")){
			yield return null;
		}



		//erased = true;

		//TimeManager.instance.paused = false;
		//treeMenuToggle.interactable = true;
	}

	override protected void RefreshCurrEvent(){
		currEvent = GameEvent ();

	}
}
