using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WindowManager : MonoBehaviour
{

    [SerializeField]
    Transform guiRoot;

    [Serializable]
    private class WindowPrefab
    {
        public string name;
        public GameObject prefab;
    }

    [SerializeField]
    List<WindowPrefab> windowPrefabList;
    Dictionary<string, GameObject> windowPrefabDict;
    public Dictionary<string, GameObject> singletonWindows { get; private set; }
    Dictionary<string, List<GameObject>> nonsingletonWindows;

	public int openedWindowNum { get; set;}

    public static WindowManager instance { get; private set; }

    static public WindowManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;

        windowPrefabDict = new Dictionary<string, GameObject>();
        singletonWindows = new Dictionary<string, GameObject>();
		nonsingletonWindows = new Dictionary<string, List<GameObject>>();
	

        foreach (WindowPrefab windowPrefab in windowPrefabList)
        {
            windowPrefabDict.Add(windowPrefab.name, windowPrefab.prefab);
        }
    }

    void Start()
    {
		openedWindowNum = 0;
        //OpenSingletonWindow("OKBtnWindow", new Dictionary<string, object>{
        //    { "titleText", "TestWindow"},
        //    { "contentText", "This is to test the OK Button Window! I do hope it works. ^_^"},
        //});   
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetSingletonWindow(string windowName)
    {
        GameObject window = null;

        if (singletonWindows.ContainsKey(windowName))
        {
            window = singletonWindows[windowName];
        }
        else
        {
            if (windowPrefabDict.ContainsKey(windowName))
            {
                window = Instantiate(windowPrefabDict[windowName], guiRoot, false);
                window.SetActive(false);
                window.GetComponent<WindowController>().destroyable = false;
                singletonWindows.Add(windowName, window);

            }
        }
        return window;
    }

    public bool OpenSingletonWindow(string windowName, object context)
    {
        //Debug.Log("OpenSingletonWindow");
        GameObject window = this.GetSingletonWindow(windowName);
        if (window == null)
            return false;

        window.GetComponent<WindowController>().Open(context);


        return true;
        //return this.OpenWindow(window, context);
    }

    public bool CloseSingletonWindow(string windowName)
    {
        //GameObject window = null;

        if (!singletonWindows.ContainsKey(windowName))
            return false;

        singletonWindows[windowName].GetComponent<WindowController>().Close();


        return true;


        //return this.CloseWindow(window);
    }

    public bool IsSingletonWindowOpen(string windowName)
    {
        if (!singletonWindows.ContainsKey(windowName))
            return false;
        else
            return singletonWindows[windowName].GetComponent<WindowController>().state
                == WindowController.State.Open;
    }

    public bool CreateNonsingletonWindow(string windowName, object context)
    {
		GameObject availableWindow = GetAwailableWindow (windowName);
		if (availableWindow == null)
			return false;
		availableWindow.GetComponent<WindowController> ().Open (context);

		return true;
    }

	GameObject GetAwailableWindow(string windowName){
		GameObject availableWindow = null;

		if (!nonsingletonWindows.ContainsKey(windowName))
		{
			nonsingletonWindows.Add (windowName, new List<GameObject> ());	
		}
		foreach (GameObject obj in nonsingletonWindows[windowName]) {
			if (obj.GetComponent<WindowController> ().state == WindowController.State.Close) {
				availableWindow = obj;
			}
		}
		if (availableWindow == null) {
			availableWindow = Instantiate(windowPrefabDict[windowName], guiRoot, false);
			availableWindow.SetActive(false);
			availableWindow.GetComponent<WindowController>().destroyable = false;
			nonsingletonWindows[windowName].Add(availableWindow);
		}

		return availableWindow;
	}

	public bool HasNonsingletonWindowOpen(string windowName)
	{
		if (!nonsingletonWindows.ContainsKey (windowName))
			return false;
		else {
			foreach (GameObject window in nonsingletonWindows[windowName]) {
				if (window.GetComponent<WindowController> ().state == WindowController.State.Open) {
					return true;
				}
			}

		}
		return false;
			
	}
//	public bool CreateWindow(string windowName, object context)
//	{
//		//Debug.Log("OpenSingletonWindow");
//		GameObject window = this.GetAwailableWindow(windowName);
//		if (window == null)
//			return false;
//
//		window.GetComponent<WindowController>().Open(context);
//		return true;
//		//return this.OpenWindow(window, context);
//	}
    //public bool OpenWindow(GameObject window, object context)
    //{
    //    if (window == null || window.activeSelf == true)
    //        return false;

    //    window.SetActive(true);   

    //    ProgramEventManager.GetInstance().DispatchLocalEvent(window, "OnWindowOpen", context);
    //    return true;
    //}

    //public bool CloseWindow(GameObject window)
    //{
    //    if (window == null || window.activeSelf == false)
    //        return false;

    //    window.SetActive(false);
    //    //ProgramEventManager.GetInstance().DispatchLocalEvent(window, "OnWindowClose", 
    //    //    new Dictionary<string, object> { { "window", window } });
        
    //    return true;
    //}
}


