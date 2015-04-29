using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServoMotorHingeJoint : MonoBehaviour {
	
	public float targetAngle = 0; 
	public float targetSpeed = 0;
	public float breakForce = 100;

	public bool isActive = false; 
	private float epsilon = 1f;
	

	int force = 500;
	JointMotor m = new JointMotor();
	
	public float currentAngle = 0;
	
	void Start () {
		//	

	}

	void FixedUpdate () {		
		currentAngle = TranslateAngleTo180(transform.localEulerAngles.x);
	}

	public void Setup(float setAngle, float setSpeed, float setBreakForce, float min, float max) {
	// Установка значений сервопривода в начале каждого движения
		targetAngle = Mathf.Clamp(setAngle, min, max);
		SetLimits(min, max);
		targetSpeed = setSpeed;
		breakForce = setBreakForce;
		m.targetVelocity = targetSpeed;
		m.force = force;	
		
		this.hingeJoint.useMotor = true;
		this.hingeJoint.motor = m;
	}
	
	public void Set(int setAngle, float setSpeed, float setBreakForce) {
		// Установка значений сервопривода в начале каждого движения
		targetAngle = setAngle;
		targetSpeed = SpeedDirection(setAngle, setSpeed);
		breakForce = setBreakForce;
		m.targetVelocity = targetSpeed;
		m.force = force;	
		
		this.hingeJoint.useMotor = true;
		
		InvokeRepeating ("CheckAngle", 0, .0002f);
	}

	void CheckAngle() {
		if (isActive) {
			if (Mathf.Abs(currentAngle - targetAngle) < epsilon) {	
				Stop ();
				m.targetVelocity = 0;		
				print ("Done! " + name + " " + currentAngle);
				isActive = false;	
				CancelInvoke();		
			} else {
				
			}
			m.force = force;
				
		} else if (!isActive) {
			
		}
		this.hingeJoint.motor = m;	
	}
	
	void Stop() {
		float limitAngle = Mathf.Round(currentAngle);
		JointLimits jl = new JointLimits();
		jl.min = limitAngle - 1;
		jl.max = 200;
		this.hingeJoint.limits = jl;
		this.hingeJoint.useLimits = true;
	}
	
	void SetLimits(float min, float max) {
		JointLimits jl = new JointLimits();
		jl.min = min;
		jl.max = max;
		this.hingeJoint.limits = jl;
		this.hingeJoint.useLimits = true;
	}
	
	float TranslateAngleTo180(float angle) {
		float result = 0f;
		result = angle < 180 ? angle : angle - 360;
		return result;
	}
	
	float SpeedDirection(int angle, float speed) {
		if (TranslateAngleTo180(currentAngle) > angle) 
			return -Mathf.Abs(speed);
		if (TranslateAngleTo180(currentAngle) < angle)
			return Mathf.Abs (speed);
		else 
			return Mathf.Abs (speed);
	}
	
	float ConvertAngleTo360 (float angle) {
		return 0f;
	}

	void RotateTo(float angle) {
		
	}

	void Rotate(float angle) {
	
	}

}
