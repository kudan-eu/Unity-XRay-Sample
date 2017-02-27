using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeAlphaMarkerless : MonoBehaviour 
{
	public GameObject cam;

	public Color color;

	public float alphaRate;

	void Update () 
	{
		float distance = ((gameObject.transform.position - cam.transform.position).magnitude / alphaRate);

		color.a = Mathf.Clamp01(Mathf.Exp (Mathf.Exp (distance)) - distance - 3);
		gameObject.GetComponent<Renderer> ().material.color = color;
	}
}
