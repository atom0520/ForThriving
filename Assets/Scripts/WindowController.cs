using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour {

    public enum State
    {
        Close,       
        Hide,
        Open,
    }

    public State state { get; protected set; }
    public bool destroyable { get; set; }

    void Awake()
    {
        //Debug.Log("WindowController.Awake!");
    }
	// Use this for initialization
	void Start () {
        //Debug.Log("WindowController.Start!");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Open(object context)
    {
        //Debug.Log("WindowController.Open!");
        state = State.Open;

        gameObject.SetActive(true);

		WindowManager.instance.openedWindowNum += 1;

    }

    void Hide()
    {
        state = State.Hide;

        gameObject.SetActive(false);
    }

    public virtual void Close()
    {
        state = State.Close;

        if (destroyable)
        {
            Destroy(gameObject);
        }else
        {
            gameObject.SetActive(false);
        }

		WindowManager.instance.openedWindowNum -= 1;
    }
}
