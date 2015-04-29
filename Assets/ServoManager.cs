/* Отвечает за обновление параметров сервоприводов */
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
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
	
	private int basicSpeed = 30;
	private int basicBreakForce = 100;

	private GameObject[] servoArray;
	private string fileMotions = @"Assets\Data\motions.txt";
	private string fileServoLimits = @"Assets\Data\servoLimits.txt";
	private static int clickCounter = 0;
	
	private List<Motion> motionsList = new List<Motion>();
	private List<List<int>> limitsList = new List<List<int>>();

	void Start () {
		servoArray = GameObject.FindGameObjectsWithTag("Joint").OrderBy(go => go.name).ToArray();
		
		if (File.Exists(fileMotions)) {
			motionsList = CreateMotions(ReadFromFile(fileMotions));			
		} else {
			print ("File doesn't exist");
			
		}
		
		if (File.Exists(fileServoLimits)) {
			List<string> stringsServoLimits = ReadFromFile (fileServoLimits);	
			foreach (string s in stringsServoLimits) {
				string[] tmpStringArray = s.Split(' ');
				List<int> li = new List<int>();
				for (int i = 0; i < tmpStringArray.Length; i++) {
					li.Add(int.Parse(tmpStringArray[i]));
				}
				
				limitsList.Add (li);
			}
					
		} else {
			print ("File doesn't exist");
		}
		
		
		int j = 0;
		foreach (GameObject g in servoArray) {
			int[] servoLimits = limitsList[j].ToArray();
			g.GetComponent<ServoMotorHingeJoint>().Setup(0, 0, 100, servoLimits[0], servoLimits[1]);
			j++;
		}
	}	
	
	void FixedUpdate () {
		foreach (GameObject g in servoArray) 	{
			//UpdateParameters (g);	
		}			
			
	}

	void UpdateParameters(GameObject g) {
		//g.GetComponent<ServoMotorHingeJoint>().targetAngle = targetAngleSlider.value;
		//g.GetComponent<ServoMotorHingeJoint>().targetSpeed = g.GetComponent<ServoMotorHingeJoint>().isActive ? targetSpeedSlider.value : 0;
		//g.GetComponent<ServoMotorHingeJoint>().breakForce = breakForceSlider.value;
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
		List<Motion> m = new List<Motion>();
		foreach(string s in stringsList) {
			string[] split = s.Split(',');
			string n = split[0];
			List<int> array = new List<int>();
			for (int i = 1; i < split.Length; i++) {
				if (split[i] != "")	
					array.Add(int.Parse(split[i]));
				else 
					array.Add (-361);
					

			}
			List<float> v = new List<float>(3);
			for (int i = 0; i < 3; i++) 
				v.Add (0f);
			
			m.Add(new Motion(n, array, v));
		}
		
		return m;
	}
	
	public void OnButton() {
		print ("ClickCounter: " + clickCounter.ToString("00"));		
		foreach (GameObject g in servoArray) {	
			if (clickCounter < motionsList.Count) {
				int tryGetAngle = 0; 
				Dictionary<string, int> t = motionsList[clickCounter].servoAngles;
				if (motionsList[clickCounter].servoAngles.TryGetValue(g.name.Substring(g.name.Length - 2), out tryGetAngle)) {
					if (tryGetAngle != -361) {
						g.GetComponent<ServoMotorHingeJoint>().Set(tryGetAngle, basicSpeed, basicBreakForce);
						g.GetComponent<ServoMotorHingeJoint>().isActive = true;
					}
					
				}			
			}
			
			
		}

			
			//if (g.name.Contains(clickCounter.ToString("00"))) {
				//
			//	g.GetComponent<ServoMotorHingeJoint>().Setup(0, 0, 100);
			//}		
		
		clickCounter++;
			
	}
	
}
