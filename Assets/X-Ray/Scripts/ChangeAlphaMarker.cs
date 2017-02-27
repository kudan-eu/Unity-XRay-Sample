using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeAlphaMarker : MonoBehaviour 
{
	public GameObject cam;

	public Color color;

	public float distance, expDistance, lnDistance;
	public float alphaRate;
	public float alpha;

	void Update () 
	{
		distance = ((gameObject.transform.position - cam.transform.position).magnitude / alphaRate);

		expDistance = Mathf.Exp (distance);
		lnDistance = Mathf.Log (distance);
		color.a = alpha = Mathf.Clamp01(lnDistance);
		gameObject.GetComponent<Renderer> ().material.color = color;
	}
}
