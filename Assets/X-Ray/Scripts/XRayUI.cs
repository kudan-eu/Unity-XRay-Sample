using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace Kudan.AR.Samples
{
	/// <summary>
	/// Script used in the Kudan Samples. Provides functions that switch between different tracking methods and start abitrary tracking.
	/// </summary>
	public class XRayUI : MonoBehaviour
	{
		public KudanTracker _kudanTracker;	// The tracker to be referenced in the inspector. This is the Kudan Camera object.
		public TrackingMethodMarker _markerTracking;	// The reference to the marker tracking method that lets the tracker know which method it is using
		public TrackingMethodMarkerless _markerlessTracking;	// The reference to the markerless tracking method that lets the tracker know which method it is using

		public GameObject markerDriver;
		public GameObject markerlessDriver;
		public GameObject targetNode;

		public Button markerButton;
		public Button markerlessButton;

		public Image helpPanel;
		public Text helpText;

		bool fadeLabelMarker;
		bool fadeLabelMarkerless;
		bool fadeLabelMarkerlessTap;

		bool showMarkerLabel;
		bool showMarkerlessLabel;
		bool showMarkerlessTapLabel;

		private Vector3 initialTouch;
		private Vector3 finalTouch;

		void Start()
		{
			helpPanel.gameObject.SetActive (false);
			helpText.gameObject.SetActive (false);

			showMarkerLabel = true;
			showMarkerlessLabel = true;
			showMarkerlessTapLabel = true;
		}

		void Update()
		{
			checkForTap ();
			updateLabels ();
			checkTrackingMethod ();
		}

		void updateLabels()
		{
			if (markerDriver.activeInHierarchy)
			{
				fadeLabelMarker = true;
				helpText.text = "Try moving a bit closer";
			}

			if (fadeLabelMarker && showMarkerLabel)
			{
				StartCoroutine (fadeHelpText());
				showMarkerLabel = false;
			}

			if (targetNode.activeInHierarchy)
			{
				fadeLabelMarkerless = true;
				helpText.text = "Tap to place, Swipe to rotate";
			}

			if (fadeLabelMarkerless && showMarkerlessLabel) 
			{
				StartCoroutine (fadeHelpText ());
				showMarkerlessLabel = false;
			}

			if (markerlessDriver.activeInHierarchy)
			{
				fadeLabelMarkerlessTap = true;
				helpText.text = "What's in the box? Move closer to find out";
			}

			if (fadeLabelMarkerlessTap && showMarkerlessTapLabel)
			{
				StartCoroutine (fadeHelpText ());
				showMarkerlessTapLabel = false;
			}
		}
			
		void checkForTap()
		{
			for (int i = 0; i < Input.touches.Length; i++) {
				if (Input.GetTouch (i).phase == TouchPhase.Began && !(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))) {
					initialTouch = Input.GetTouch (i).position;
					finalTouch = Input.GetTouch (i).position;
				}
				else if (Input.GetTouch(i).phase == TouchPhase.Ended) {
					finalTouch = Input.GetTouch(i).position;
					float movementTolerance = (float)(Screen.width * 0.05);
					float distanceX = Mathf.Abs (initialTouch.x - finalTouch.x);
					float distanceY = Mathf.Abs (initialTouch.y - finalTouch.y);
					if(distanceX < movementTolerance && distanceY < movementTolerance && _kudanTracker.CurrentTrackingMethod == _markerlessTracking) 
					{
						updateMarkerless ();
					}
				}
			}
		}

		void updateMarkerless()
		{
			if (!_kudanTracker.ArbiTrackIsTracking ()) 
			{
				// from the floor placer.
				Vector3 floorPosition;			// The current position in 3D space of the floor
				Quaternion floorOrientation;	// The current orientation of the floor in 3D space, relative to the device

				_kudanTracker.FloorPlaceGetPose (out floorPosition, out floorOrientation);	// Gets the position and orientation of the floor and assigns the referenced Vector3 and Quaternion those values
				_kudanTracker.ArbiTrackStart (floorPosition, floorOrientation);				// Starts markerless tracking based upon the given floor position and orientations
			}

			else 
			{
				_kudanTracker.ArbiTrackStop ();
			}
		}

		void checkTrackingMethod()
		{
			if (_kudanTracker.CurrentTrackingMethod == _markerlessTracking) 
			{
				markerlessButton.image.color = new Color (0.8f, 0.8f, 0.8f, 1.0f);
				markerButton.image.color = new Color (0.4f, 0.4f, 0.4f, 0.5f);
			}

			else 
			{
				markerButton.image.color = new Color (0.8f, 0.8f, 0.8f, 1.0f);
				markerlessButton.image.color = new Color (0.4f, 0.4f, 0.4f, 0.5f);
			}
		}

		public void switchMarker()
		{
			_kudanTracker.ChangeTrackingMethod (_markerTracking);
		}

		public void switchMarkerless()
		{
			_kudanTracker.ChangeTrackingMethod (_markerlessTracking);
		}

		IEnumerator fadeHelpText()
		{
			helpPanel.gameObject.SetActive (true);
			helpText.gameObject.SetActive (true);

			helpPanel.color = new Color (helpPanel.color.r, helpPanel.color.g, helpPanel.color.b, 1.0f);
			helpText.color = new Color (helpText.color.r, helpText.color.g, helpText.color.b, 1.0f);

			yield return new WaitForSeconds (5);

			while (helpPanel.color.a > 0 || helpText.color.a > 0) 
			{
				helpPanel.color = new Color (helpPanel.color.r, helpPanel.color.g, helpPanel.color.b, helpPanel.color.a - 0.01f);
				helpText.color = new Color (helpText.color.r, helpText.color.g, helpText.color.b, helpText.color.a - 0.01f);

				yield return new WaitForEndOfFrame();
			}

			helpPanel.gameObject.SetActive (false);
			helpText.gameObject.SetActive (false);
		}
	}
}
