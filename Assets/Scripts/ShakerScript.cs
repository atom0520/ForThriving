using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakerScript : MonoBehaviour {

    public bool X;
    public bool Y;
    public bool Z;
    public float Scale = 0.0f;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("ShakerScript.Update!");

        float t = (Mathf.PingPong(Time.time, 2.0f) - 1.0f) * Scale;

        if(X && Y && Z)
        {
            this.transform.Rotate(t,t,t);
        }                  
        else if(Y && Z)    
        {                  
            this.transform.Rotate(0, t, t);
        }                  
        else if(X && Y)    
        {                  
            this.transform.Rotate(t, t, 0);
        }                  
        else if(Z)         
        {                  
            this.transform.Rotate(0, 0, t);
        }                  
        else if(Y)         
        {                  
            this.transform.Rotate(0, t, 0);
        }                  
        else if(Z)         
        {                  
            this.transform.Rotate(t, 0, 0);
        }
    }
}
