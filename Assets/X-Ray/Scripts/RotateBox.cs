using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class RotateBox : MonoBehaviour 
{
	#if UNITY_ANDROID || UNITY_IOS

	float moveSpeed;

	void Start()
	{
		moveSpeed = 0.75f;
	}

	void Update()
	{
		processDrag ();
	}

	void processDrag()
	{
		if (Input.touchCount == 1) 
		{
			//Store input
			Touch fing = Input.GetTouch (0);

			if(fing.phase == TouchPhase.Moved)	//If the finger has moved since the last frame
			{
				//Find the amount the finger has moved, and apply a rotation to this gameobject based on that amount
				Vector3 rotation = gameObject.transform.localRotation.eulerAngles;
				Vector2 fingMove = fing.deltaPosition;
				float deltaY = fingMove.x * moveSpeed;

	#if UNITY_IOS
				moveSpeed = 3f;
				deltaY *= Time.deltaTime;
	#endif

				rotation.y -= deltaY;

				gameObject.transform.localRotation = Quaternion.Euler (rotation);
			}
		}
	}
	#endif
}