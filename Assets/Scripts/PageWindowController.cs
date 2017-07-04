using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageWindowController : WindowController {

	enum Style{
		SinglePage,
		DoublePage
	}

	[SerializeField]
	Style style;

	[SerializeField]
	Text titleText1;
	[SerializeField]
	Text  contentText1;
	[SerializeField]
	Text titleText2;
	[SerializeField]
	Text  contentText2;

	[SerializeField]
	Image background;

	[SerializeField]
	GameObject nextPageBtn;
	[SerializeField]
	GameObject lastPageBtn;



	public class Page{
		public Page(string title, string content){
			this.title = title;
			this.content = content;
		}

		public string title;
		public string content;
	}

	int pageIndex;
	List<Page> pages;


	float alpha;
	//[SerializeField]
	float fadeInDuration;
	//SerializeField]
	float fadeOutDuration;
	// Use this for initialization

	bool isFadingIn;
	bool isFadingOut;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isFadingOut) {
			if (fadeOutDuration > 0)
				alpha = Mathf.Max (0, alpha - Time.deltaTime * 1 / fadeInDuration);
			else
				alpha = 0;
			
			Color temp = background.color;
			temp.a = alpha;
			background.color = temp;

			temp = titleText1.color;
			temp.a = alpha;
			titleText1.color = temp;

			temp = contentText1.color;
			temp.a = alpha;
			contentText1.color = temp;

			if (style == Style.DoublePage) {
				temp = titleText2.color;
				temp.a = alpha;
				titleText2.color = temp;

				temp = contentText2.color;
				temp.a = alpha;
				contentText2.color = temp;
			}

			if (alpha <= 0) {
				isFadingOut = false;
				base.Close();

			}

			return;
		}

		if (isFadingIn) {
			if (fadeInDuration > 0)
				alpha = Mathf.Min (1.0f, alpha + Time.deltaTime * 1 / fadeInDuration);
			else
				alpha = 1;
			
			Color temp = background.color;
			temp.a = alpha;
			background.color = temp;

			temp = titleText1.color;
			temp.a = alpha;
			titleText1.color = temp;

			temp = contentText1.color;
			temp.a = alpha;
			contentText1.color = temp;

			if (style == Style.DoublePage) {
				temp = titleText2.color;
				temp.a = alpha;
				titleText2.color = temp;

				temp = contentText2.color;
				temp.a = alpha;
				contentText2.color = temp;
			}

			if (alpha >= 1) {
				isFadingIn = false;
				RefreshBtns ();
			}

			return;
		}

		if (Input.GetButtonDown("Submit"))
		{
	
			isFadingOut = true;
			
			//Debug.Log("CloseWindow!");
	
//			ProgramEventManager.GetInstance().DispatchLocalEvent(gameObject, "OnWindowClose",
//				new Dictionary<string, object> { { "window", gameObject } });
		}


	}

	public override void Open(object context)
	{
		Dictionary<string, object> dataValue = (Dictionary<string, object>)context;

		pages = (List<Page>)dataValue["pages"];

		if(dataValue.ContainsKey("fadeInDuration")){
			fadeInDuration = (float)dataValue["fadeInDuration"];
		}else{
			fadeInDuration = 0f;
		}

		if(dataValue.ContainsKey("fadeOutDuration")){
		    fadeOutDuration = (float)dataValue["fadeOutDuration"];
		}else{
			fadeOutDuration = 0f;
		}

		//transform.localPosition = Vector2.zero;

		alpha = 0;

		Color temp = background.color;
		temp.a = 0;
		background.color = temp;

		temp = titleText1.color;
		temp.a = 0;
		titleText1.color = temp;

		temp = contentText1.color;
		temp.a = 0;
		contentText1.color = temp;

		if (style == Style.DoublePage) {
			temp = titleText2.color;
			temp.a = 0;
			titleText2.color = temp;

			temp = contentText2.color;
			temp.a = 0;
			contentText2.color = temp;
		}

		isFadingIn = true;
		
		isFadingOut = false;

		lastPageBtn.SetActive (false);
		nextPageBtn.SetActive (false);

		titleText1.text = "";
		contentText1.text = "";

		if (style == Style.DoublePage) {
			titleText2.text = "";
			contentText2.text = "";
		}


		if (style == Style.SinglePage)
			pageIndex = pages.Count - 1;
		else if (style == Style.DoublePage) {
			if (pages.Count % 2 != 0)
				pageIndex = pages.Count - 1;
			else
				pageIndex = pages.Count - 2;
		}


		RefreshPage ();

		base.Open(context);
	}

	void RefreshPage(){
		if (pages.Count == 0)
			return;
		
		titleText1.text = pages [pageIndex].title;
		contentText1.text = pages [pageIndex].content;

		if (style == Style.DoublePage) {
			if (pageIndex < pages.Count - 1) {
				titleText2.text = pages [pageIndex + 1].title;
				contentText2.text = pages [pageIndex + 1].content;
			} else {
				titleText2.text = "";
				contentText2.text = "";
			}
		}
	
	}

	void RefreshBtns(){
		if (pages.Count == 0)
			return;
		
		if (pageIndex > 0) {
			lastPageBtn.SetActive (true);
		} else {
			lastPageBtn.SetActive (false);
		}

		if (style == Style.SinglePage) {
			if (pageIndex < pages.Count - 1) {
				nextPageBtn.SetActive (true);
			} else {
				nextPageBtn.SetActive (false);
			}
		}
		else if (style == Style.DoublePage) {
			if (pageIndex < pages.Count - 2) {
				nextPageBtn.SetActive (true);
			} else {
				nextPageBtn.SetActive (false);
			}
		}



	}

	public void OnClickLastPageBtn(){
		if (style == Style.SinglePage) {
			pageIndex -= 1;
		}
		else if (style == Style.DoublePage) {
			pageIndex -= 2;
		}

		RefreshPage ();
		RefreshBtns ();
	}

	public void OnClickNextPageBtn(){
		if (style == Style.SinglePage) {
			pageIndex += 1;
		}
		else if (style == Style.DoublePage) {
			pageIndex += 2;
		}
		RefreshPage ();
		RefreshBtns ();
	}
}
