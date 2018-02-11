using UnityEngine;
using System.Collections;

public class Kart : MonoBehaviour 
{
	public float topSpeed = 50;
	public float turnSpeed = 10;
	public float accFactor = 10;
	public float currentSpeed = 0;
	private Vector3 currentDir;
	//private static float rollingTime = 1;
	//private float timeLeft = 0;
	void Update()
	{
		// Pull in information from the Input class
		float ySpin = Input.GetAxis ("Horizontal");									// 1
		//float zAxis = Input.GetAxis ("Vertical");									// 1
		bool gasButtonPressed = Input.GetButton ("Gas");
		bool gasButtonUp = Input.GetButtonUp ("Gas");
		
		// Ask the engine for current Position and Direction vectors
		Vector3 myPos = transform.position;
		Vector3 myDir = transform.forward;

		// if the gas button is down
		if (gasButtonPressed) 
		{
			// If currentSpeed + amount to be accellerated is less than top speed, then that is the new current speed
			currentSpeed = (currentSpeed + accFactor * Time.deltaTime < topSpeed ? currentSpeed + accFactor * Time.deltaTime : topSpeed);
		}
		else
		{
			// If the button is not pushed down, slow down to a stop.
			currentSpeed = (currentSpeed - accFactor * Time.deltaTime > 0 ? currentSpeed - accFactor * Time.deltaTime : 0);
		}
		// Calculate the change in position in each direction
		myPos.z += myDir.z * currentSpeed * Time.deltaTime;
		myPos.x += myDir.x * currentSpeed * Time.deltaTime;
		// Tell the engine
		transform.position = myPos;

		// Rotate the Kart based on stick input
		transform.rotation *= Quaternion.Euler (0, ySpin * turnSpeed, 0);
	}

	// This variable holds a reference to the last triggereing GameObject
	public GameObject lastTriggerGo = null;
	// This variable holds a reference to the time of the last hit
	public float triggerTime;

	void OnTriggerEnter(Collider other)
	{
		// Find the tag of other.gameObject or its parent GameObjects
		GameObject go = Utils.FindTaggedParent (other.gameObject);
		// If there is a parent with a tag
		if (go != null) 
		{
			triggerTime = Time.time;
			// Make sure it's not the same triggering go as last time.
			if (go == lastTriggerGo) 
			{
				// If it is, then has it been long enough since something last made contact?
				if ((Time.time - triggerTime) > 1f)
				{
					return;
				}
			}
			lastTriggerGo = go;
			if (go.tag == "Boost Pad") 
			{
				// Increment timeLeft to get a speed boost for timeLeft seconds
				//timeLeft = 3;
				// AlterSpeed(2, 3);
			} 
			else if (go.tag == "Potion")
			{
				// make the player sit for 1 second
				// AlterSpeed(0, 1);
			}
			print ("Triggered: " + go.name);
		}
	}

	void AlterSpeed(float newSpeed, float seconds)
	{
		// change speed from it's current sate to newSpeed
		// wait for 'seconds' seconds
		// change speed back to it's original pace
	}
}
