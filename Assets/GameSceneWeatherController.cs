using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneWeatherController : MonoBehaviour {

	[SerializeField]
	int cloudyMinMoralValue;
	[SerializeField]
	int rainMinMoralValue;
//	[SerializeField]
//	int snowMinMoralValue;

//	[SerializeField]
//	GameObject cloudyEffect;
	[SerializeField]
	GameObject rainEffect;
	[SerializeField]
	GameObject snowEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.moralValue >= cloudyMinMoralValue) {
			rainEffect.SetActive (false);
			snowEffect.SetActive (false);
			GameManager.instance.weather = "阴";
		} else if (GameManager.instance.moralValue >= rainMinMoralValue) {
			rainEffect.SetActive (true);
			snowEffect.SetActive (false);
			GameManager.instance.weather = "雨";
		} else {
			rainEffect.SetActive (false);
			snowEffect.SetActive (true);
			GameManager.instance.weather = "雪";
		}
	}
}
