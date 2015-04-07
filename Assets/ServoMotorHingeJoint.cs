using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServoMotorHingeJoint : MonoBehaviour {
	
	public float targetAngle = 0; 
	public float targetSpeed = 0;
	public float breakForce = 100;

	public bool isActive = false; 
	private float epsilon = 1f;
	

	int force = 200;
	JointMotor m = new JointMotor();
	
	private Vector3 lastForward;
	public float currentAngle = 0;
	
	void Start () {
		//	

	}

	void FixedUpdate () {		
		if (isActive) {
			currentAngle = transform.localEulerAngles.y;
			hingeJoint.breakForce = breakForce;
		}

	}

	public void Setup(float setAngle, float setSpeed, float setBreakForce) {
		targetAngle = setAngle;
		targetSpeed = setSpeed;
		breakForce = setBreakForce;
		m.targetVelocity = setSpeed;
		m.force = force;	
		lastForward = this.transform.forward;

		this.hingeJoint.useMotor = true;

		InvokeRepeating ("CheckAngle", 0, .0002f);
	}

	void CheckAngle() {
		if (isActive) {
			if (Mathf.Abs(currentAngle - targetAngle) < epsilon) {	
				isActive = false;
				m.targetVelocity = 0;		
				//m.targetVelocity = -1 * Mathf.Abs(m.targetVelocity);
				//m.force = force;
				//print (currentAngle);
				
			} else {
				//m.targetVelocity = m.targetVelocity;
				//m.force = force;
				//print (currentAngle);
			}
			m.force = force;		
		}
		/*Vector3 currentForward = transform.forward;
		float angle = Vector3.Angle(currentForward, lastForward);
		
		if (angle > 0.001){ 
			if (Vector3.Cross(currentForward, lastForward).x < 0) angle = -angle;
			currentAngle += angle; 
			lastForward = currentForward; 
		}*/

		/*if (currentAngle >= targetAngle) {			
			m.targetVelocity = -1 * Mathf.Abs(m.targetVelocity);
			m.force = force;
			//print (currentAngle);
		} else if (currentAngle <= 0) {
			m.targetVelocity = Mathf.Abs (m.targetVelocity);
			m.force = force;
			//print (currentAngle);
		}*/
		
		//m.targetVelocity = targetSpeed * Mathf.Sign (m.targetVelocity);
		this.hingeJoint.motor = m;
	}

	void RotateTo(float angle) {
		
	}

	void Rotate(float angle) {
	
	}

}
