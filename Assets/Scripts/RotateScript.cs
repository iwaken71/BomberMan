using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour {

	Quaternion rotation; // 向かう先の回転値

	float rotateSpeed = 10;

	// Use this for initialization
	void Start () {
		rotation = transform.rotation;
		
	}
	
	// Update is called once per frame
	void Update ()
	{


		if (Input.GetKeyDown ("w")) {
			rotation = rotation * Quaternion.Euler(20,0,0);
		}
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation,rotateSpeed*Time.deltaTime);
		
	}
}
