using UnityEngine;
using System.Collections;

public class ScubaSwim : MonoBehaviour 
{
	Vector3 posUp, posDown, posLeft, posRight, posFront, posBack;

	public float swimUpSpeed, swimRightSpeed, swimForwardSpeed;

	public float maxX, maxY, maxZ;

	void Start()
	{
		posUp = new Vector3 (transform.localPosition.x, transform.localPosition.y + maxY, transform.localPosition.z);
		posDown = new Vector3 (transform.localPosition.x, transform.localPosition.y - maxY, transform.localPosition.z);
		posLeft = new Vector3 (transform.localPosition.x - maxX, transform.localPosition.y, transform.localPosition.z);
		posRight = new Vector3 (transform.localPosition.x + maxX, transform.localPosition.y, transform.localPosition.z);
		posFront = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + maxZ);
		posBack = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - maxZ);
	}

	void Update () 
	{
		Vector3 upSwim, rightSwim, forwardSwim;

		upSwim = Vector3.Slerp (posUp, posDown, Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time * swimUpSpeed, 1.0f)));
		rightSwim = Vector3.Slerp (posLeft, posRight, Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time * swimRightSpeed, 1.0f)));
		forwardSwim = Vector3.Slerp (posFront, posBack, Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time * swimForwardSpeed, 1.0f)));

		transform.localPosition = new Vector3 (rightSwim.x, upSwim.y, forwardSwim.z);
	}
}