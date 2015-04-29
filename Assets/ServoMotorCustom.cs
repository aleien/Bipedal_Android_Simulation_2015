using UnityEngine;
using System.Collections;

public class ServoMotorCustom : MonoBehaviour {
	//int motorSpeed = 80;
	public Rigidbody parent;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		rigidbody.MovePosition(transform.position + Vector3.up*Time.deltaTime);
	}
}
