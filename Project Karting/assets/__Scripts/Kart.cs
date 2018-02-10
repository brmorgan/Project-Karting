using UnityEngine;
using System.Collections;

public class Kart : MonoBehaviour {

	public float speed = 10;
	public float turnSpeed = 10;
	private static float rollingTime = 1;
	private float timeLeft = 0;
	void Update()
	{
		// Pull in information from the Input class
		float ySpin = Input.GetAxis ("Horizontal");									// 1
		//float zAxis = Input.GetAxis ("Vertical");									// 1
		bool zoom = Input.GetButton ("Gas");
		bool up = Input.GetButtonUp ("Gas");
		
		// Change transform.position based on the axes
		Vector3 pos = transform.position;

		Vector3 dir = transform.forward;
		// if the gas button is down
		if (zoom) {
			//pos.z += speed * Time.deltaTime;
			pos.x += dir.x * speed * Time.deltaTime * (timeLeft/rollingTime);
			pos.z += dir.z * speed * Time.deltaTime * (timeLeft/rollingTime);

			// Rotate the car because it can't "crab".
			transform.rotation *= Quaternion.Euler (0, ySpin * turnSpeed, 0);
			if (timeLeft < rollingTime) {
				timeLeft += (2*Time.deltaTime);
			}
		}
		// if the gas has been released recently
		if (up || (timeLeft < rollingTime && timeLeft > 0)) {
			// deccelerate over rollingTime seconds
			pos.x += dir.x * speed * Time.deltaTime * (timeLeft / rollingTime);
			pos.z += dir.z * speed * Time.deltaTime * (timeLeft / rollingTime);
			// Rotate the car because it can't "crab".
			transform.rotation *= Quaternion.Euler (0, ySpin * turnSpeed, 0);
			if (timeLeft > 0) {
				timeLeft -= Time.deltaTime;
			}
		}
		if (timeLeft > rollingTime) {
			timeLeft -= Time.deltaTime;
		}

		transform.position = pos;
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
		if (go != null) {
			triggerTime = Time.time;
			// Make sure it's not the same triggering go as last time.
			if (go == lastTriggerGo) {
				// If it is, then has it been long enough since something last made contact?
				if ((Time.time - triggerTime) > 1f)
				{
					return;
				}
			}
			lastTriggerGo = go;
			if (go.tag == "Boost Pad") {
				// Increment timeLeft to get a speed boost for timeLeft seconds
				timeLeft = 3;
				// AlterSpeed(2, 3);
			} else if ( go.tag == "Potion")
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
