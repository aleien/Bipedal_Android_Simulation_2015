using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServoMotorConfigurable : MonoBehaviour {
	public Slider targetAngleSlider;
	public Text targetAngleText;
	//public Text currentAngleText;
	
	public Text currentRotationText;
	public Text currentLocalRotationText;
	public Text currentEulerText;
	public Text currentLocalEulerText;

	// Use this for initialization
	void Start () {
		
		Quaternion q = Quaternion.Euler(0,targetAngleSlider.value,0);
		ConfigurableJoint cj = GetComponent<ConfigurableJoint>();
		cj.targetRotation = q;
		currentRotationText.text = transform.rotation.ToString();
		currentLocalRotationText.text = transform.localRotation.ToString();
		currentEulerText.text = transform.eulerAngles.ToString();
		currentLocalEulerText.text = transform.rotation.eulerAngles.ToString();
		targetAngleText.text = targetAngleSlider.value.ToString();
	
	}
	
	// Update is called once per frame
	void Update () {
		currentRotationText.text = transform.rotation.ToString();
		currentLocalRotationText.text = transform.localRotation.ToString();
		currentEulerText.text = transform.eulerAngles.ToString();
		currentLocalEulerText.text = transform.localEulerAngles.ToString();
		targetAngleText.text = targetAngleSlider.value.ToString();
	}
	
	public void OnSliderChange() {
		Quaternion q = Quaternion.Euler(0,targetAngleSlider.value,0);
		ConfigurableJoint cj = GetComponent<ConfigurableJoint>();
		cj.targetRotation = q;
	}
}
