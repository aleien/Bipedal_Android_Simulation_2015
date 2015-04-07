/* Отвечает за обновление параметров сервоприводов */
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class ServoManager : MonoBehaviour {
	public Slider targetAngleSlider;
	public Slider targetSpeedSlider;
	public Slider breakForceSlider;

	public Text angleText;
	public Text targetAngleText;
	public Text targetSpeedText;
	public Text breakForceText;

	private GameObject[] servoArray;
	private string fileName = @"Assets\Data\motions.txt";

	void Start () {
		servoArray = GameObject.FindGameObjectsWithTag("Joint");
		
		if (File.Exists(fileName)) {
			List<Motion> m = CreateMotions(ReadFromFile(fileName));			
		} else {
			print ("File doesn't exist");
		}
		
		
		foreach (GameObject g in servoArray) {
			g.GetComponent<ServoMotorHingeJoint>().Setup(0, 0, 100);
			g.GetComponent<ServoMotorHingeJoint>().isActive = true;

			//UpdateParameters(g);
		}
	}	

	void FixedUpdate () {
		foreach (GameObject g in servoArray) 	{
			//UpdateParameters (g);	
		}			
			
		angleText.text = servoArray[0].GetComponent<ServoMotorHingeJoint>().currentAngle.ToString();
		targetAngleText.text = servoArray[0].GetComponent<ServoMotorHingeJoint>().targetAngle.ToString();
		targetSpeedText.text = servoArray[0].GetComponent<ServoMotorHingeJoint>().targetSpeed.ToString();
		breakForceText.text = servoArray[0].GetComponent<ServoMotorHingeJoint>().hingeJoint.breakForce.ToString();
	}

	void UpdateParameters(GameObject g) {
		g.GetComponent<ServoMotorHingeJoint>().targetAngle = targetAngleSlider.value;
		g.GetComponent<ServoMotorHingeJoint>().targetSpeed = g.GetComponent<ServoMotorHingeJoint>().isActive ? targetSpeedSlider.value : 0;
		g.GetComponent<ServoMotorHingeJoint>().breakForce = breakForceSlider.value;
	}
	
	List<string> ReadFromFile(string fileName) {
		List<string> readStringsList = new List<string>();
		string[] s = File.ReadAllLines(fileName);
		for (int i = 0; i < s.Length; i++) {
			readStringsList.Add(s[i]);
		}
		return readStringsList;
	}
	
	List<Motion> CreateMotions(List<string> stringsList) {
		List<Motion> motionsList = new List<Motion>();
		foreach(string s in stringsList) {
			string[] split = s.Split(',');
			string n = split[0];
			List<int> array = new List<int>();
			for (int i = 1; i < split.Length; i++) {
				if (split[i] != "")	
					array.Add(int.Parse(split[i]));
				else 
					array.Add (-1);
					

			}
			List<float> v = new List<float>(3);
			for (int i = 0; i < 3; i++) 
				v.Add (0f);
			
			motionsList.Add(new Motion(n, array, v));
		}
		
		return motionsList;
	}
	
}
