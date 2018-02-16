using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart_Model : MonoBehaviour
{
	public float RECOVERFACTOR = 1;
	public float EPSILON = 2;
	public float xrot;
	public float yrot;
	public float zrot;

	// Update is called once per frame
	void Update ()
	{
	// -- This block intends to rotate and move the model back to center relative to its hitbox
		// Find the x, y, and z components of the model's rotation relative to its hitbox
		xrot = transform.localEulerAngles.x;
		yrot = transform.localEulerAngles.y;
		zrot = transform.localEulerAngles.z;

		// for each component
		// if rotation is outside acceptable bounds, increase rotation if delta > 180deg, decrease if delta < 180deg
		// ie rotate the model by RECOVERFACTOR in the direction that fastest gets you to 0 relative to hitbox 
		if (xrot > EPSILON && xrot - 180 < EPSILON)
			xrot = -RECOVERFACTOR;
		else if (xrot - 180 > EPSILON)
			xrot = RECOVERFACTOR;
		else
			xrot = 0;
		if (yrot > EPSILON && yrot - 180 < EPSILON)
			yrot = -RECOVERFACTOR;
		else if (yrot - 180 > EPSILON)
			yrot = RECOVERFACTOR;
		else
			yrot = 0;
		if (zrot > EPSILON && zrot - 180 < EPSILON)
			zrot = -RECOVERFACTOR;
		else if (zrot - 180 > EPSILON)
			zrot = RECOVERFACTOR;
		else
			zrot = 0;

		// Give the rotation over to the engine
		transform.Rotate(new Vector3(xrot, yrot, zrot));
		// Move the model half the distance to center. With the magic of Zeno's Paradox, this eventually gets "close enough"
		transform.Translate(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z) / -2);
	// --
	}
}