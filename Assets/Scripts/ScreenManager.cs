using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

    public static ScreenManager instance { get; private set; }

    [SerializeField]
    public GameObject fadingScreenCover;

    [SerializeField]
    public GameObject loadingScreenCover;

	[SerializeField]
	public GameObject colorScreenCover;

    void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

	// Use this for initialization
	//void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}
}
