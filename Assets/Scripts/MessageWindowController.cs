using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Atom;

public class MessageWindowController : WindowController {

//    public enum Style {
//        Default,
//		Character,
//		Style3
//    }

//    static Sprite[] characterHeadSprites;
//    [SerializeField]
//    Image characterHeadImageBg;
//    [SerializeField]
//    Image characterHeadImage;    
//    [SerializeField]
//    Text characterNameText;
//    [SerializeField]
//    Text characterMessageText;
//    [SerializeField]
//    Text defaultMessageText;

	[SerializeField]
	protected Text messageText;

	//Style style;

	protected string completeMessage;

	int currDisplayedCharIndex;
	float messageDisplayCounter;

	[SerializeField]
	float messageDisplaySpeed;

//    void Awake()
//    {
//        if (characterHeadSprites == null)
//            characterHeadSprites = Resources.LoadAll<Sprite>("Graphics/CharacterHeads");
//             
//        //ProgramEventManager.GetInstance().AddLocalEventListener(gameObject, "OnWindowOpen", new ProgramEventManager.EventListener(OnWindowOpen));
//    }

 //   void OnEnable()
 //   {
 //       //characterHeadImageBg.gameObject.SetActive(false);
 //       //characterHeadImage.gameObject.SetActive(false);
 //       //characterNameText.gameObject.SetActive(false);
 //       //characterMessageText.gameObject.SetActive(false);
 //       //defaultMessageText.gameObject.SetActive(false);
 //   }

	// Use this for initialization
//	void Start () {
//		//completeMessage = "";
//
//	}
	
	// Update is called once per frame
	void Update () {

		messageDisplayCounter -= Time.deltaTime;

		//if (Input.GetKeyDown("space")) messageDisplayCounter -= Time.deltaTime;

		if (messageDisplayCounter <= 0) {
			if (currDisplayedCharIndex < completeMessage.Length) {
				currDisplayedCharIndex++;

				this.messageText.text = completeMessage.Substring(0,currDisplayedCharIndex);
//				if (style == Style.Character)
//				{
//					this.characterMessageText.text = completeMessage.Substring(0,currDisplayedCharIndex);
//				}
//				else if (style == Style.Default)
//				{
//
//					this.defaultMessageText.text = completeMessage.Substring(0,currDisplayedCharIndex);
//
//				}
		
			}

			//Debug.Log ("messageDisplayCounter:" + messageDisplayCounter);
			messageDisplayCounter = 1 / messageDisplaySpeed;
			//Debug.Log ("messageDisplayCounter:" + messageDisplayCounter);
		}

		if (Input.GetButtonDown("Submit"))
        {
			if (currDisplayedCharIndex >= completeMessage.Length) {
				base.Close ();
				ProgramEventManager.GetInstance ().DispatchLocalEvent (gameObject, "OnWindowClose",
					new Dictionary<string, object> { { "window", gameObject } });
			} else {
				currDisplayedCharIndex = completeMessage.Length;
				this.messageText.text = completeMessage.Substring(0,currDisplayedCharIndex);

			}	
           
            
        }


    }

    public override void Open(object context)
    {
//        characterHeadImageBg.gameObject.SetActive(false);
//        characterHeadImage.gameObject.SetActive(false);
//        characterNameText.gameObject.SetActive(false);
//        characterMessageText.gameObject.SetActive(false);
//        defaultMessageText.gameObject.SetActive(false);
        //base.state = WindowController.State.Open;

        //Debug.Log("MessageWindow.Controller.Open!");

        Dictionary<string, object> dataValue = (Dictionary<string, object>)context;
//        style = (Style)dataValue["style"];
//        if (style == Style.Character)
//        {
//            //Debug.Log("MessageWindow.style == Style.Character!");
////            string headSpriteName = (string)dataValue["headSpriteName"];
//            string characterName = (string)dataValue["characterName"];
//
//			completeMessage = (string)dataValue["message"];
//            
//
//            this.characterHeadImage.sprite = GetCharacterHeadSprite(characterName);
//      
//            this.characterNameText.text = characterName + ":";
//            
//
////            this.characterHeadImageBg.gameObject.SetActive(true);
////            this.characterHeadImage.gameObject.SetActive(true);
//            this.characterNameText.gameObject.SetActive(true);
//            this.characterMessageText.gameObject.SetActive(true);
//        }
//        else if (style == Style.Default)
//        {
//            
//			completeMessage = (string)dataValue["message"];
//         
//			//this.defaultMessageText.text = completeMessage;
//            
//
//            this.defaultMessageText.gameObject.SetActive(true);
//           
//        }


		if (dataValue.ContainsKey ("message")) {
			completeMessage = (string)dataValue["message"];
//			Debug.Log ("completeMessage:" + completeMessage);
//			Debug.Log ("completeMessage.Length:" + completeMessage.Length);

		}

		messageText.text = "";


		if (dataValue.ContainsKey ("messageDisplaySpeed")) {
			this.messageDisplaySpeed = (float)dataValue["messageDisplaySpeed"];
		}

		currDisplayedCharIndex = 0;
		messageDisplayCounter = 0;

        base.Open(context);
    }

    //void OnWindowOpen(object data)
    //{
    //    Dictionary<string, object> dataValue = (Dictionary<string, object>)data;
    //    Style style = (Style)dataValue["style"];
    //    if(style == Style.Character)
    //    {
    //        string headSpriteName = (string)dataValue["headSpriteName"];
    //        string characterName = (string)dataValue["characterName"];
    //        string message = (string)dataValue["message"];
    //        //Debug.Log("characterName:" + characterName);
    //        characterHeadImage.sprite = GetCharacterHeadSprite(characterName);
    //        //Debug.Log("characterHeadImage.sprite:" + characterHeadImage.sprite);
    //        characterNameText.text = characterName+":";
    //        characterMessageText.text = message;

    //        characterHeadImageBg.gameObject.SetActive(true);
    //        characterHeadImage.gameObject.SetActive(true);
    //        characterNameText.gameObject.SetActive(true);
    //        characterMessageText.gameObject.SetActive(true);
    //    }
    //    else if (style == Style.Default)
    //    {
    //        string message = (string)dataValue["message"];
    //        defaultMessageText.text = message;
    //        defaultMessageText.gameObject.SetActive(true);
    //    }
    //}

//    Sprite GetCharacterHeadSprite(string name)
//    {
//        foreach(Sprite sprite in characterHeadSprites)
//        {
//            if(sprite.name == name)
//            {
//                return sprite;
//            }
//        }
//        return null;
//    }
}
