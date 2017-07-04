using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float t1 = (Mathf.PingPong(Time.time, 5)-2.5f)*6.0f;
		float t2 = (Mathf.PingPong(Time.time, 3)-1.5f)*6.0f;


		Quaternion rotation = this.transform.rotation;
		rotation.eulerAngles = new Vector3 (t1, 0, 0);
		this.transform.rotation = rotation;
	}
}
