﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PlayerKeyScript : MonoBehaviour {
	// Use this for initialization
	public static bool isPaused = false;
	public static bool showMessage = false;
	public static bool showPanel = false;
	public static bool deviceGrabbed = false;
	GameObject deviceToPlace;
	GameObject devicePanelInfo;

	float targetAngle = 0f;
	float degreesPerClick = 2f;
	float secsPerClick = 0.3f;

	private float curAngle = 0f;
	private float startAngle=0f;
	private float startTime=0f;

	void Start () {
		devicePanelInfo = GameObject.Find ("Panel_DeviceInfo");
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown(0)){	//detectez click
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if (hit.transform.tag == "world") {
					float distance = Vector3.Distance (transform.position, hit.transform.position);
					if (distance <= 3) {
						string id = hit.transform.GetChild (1).GetComponent<TextMesh> ().text;
						GameObject.Find ("WorldSpawnManager").GetComponent<WorldSpawnManagerScript> ().GoToAnotherRoom (id, gameObject);
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.E)) {	// detectez tasta E
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.tag == "device") {
					float distance = Vector3.Distance (transform.position, hit.transform.position);
					if (distance <= 3 && deviceGrabbed == false) {
						deviceToPlace = hit.transform.gameObject;
						deviceGrabbed = true;
						curAngle = deviceToPlace.transform.rotation.eulerAngles.y;  // salvez rotatia initiala a obiectului atunci cand am dat E sa il ridic
						targetAngle = deviceToPlace.transform.rotation.eulerAngles.y;		// la fel si aici deoarece trebuie modificata rotatia pentru fiecare obiect in parte
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {	//detectez tasta escape
			if (isPaused == false) {
				isPaused = true;
				gameObject.GetComponent<FirstPersonController> ().enabled = false;
			} else {
				isPaused = false;
				gameObject.GetComponent<FirstPersonController> ().enabled = true;
			}
		}
		if (Input.GetKeyDown (KeyCode.M)) {	// detectez tasta M
			GameObject.Find ("WorldSpawnManager").GetComponent<WorldSpawnManagerScript> ().GoToAnotherRoom ("0", gameObject);
		}
			
		Ray rayb = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitb;
		if (Physics.Raycast (rayb, out hitb)) {
			if (hitb.transform.tag == "world") {
				float distance = Vector3.Distance (transform.position, hitb.transform.position);
				if (distance <= 3) {
					showMessage = true;
				} else
					showMessage = false;
			} else if (hitb.transform.tag == "device") {		// cod pentru afisare device info
				float distance = Vector3.Distance (transform.position, hitb.transform.position);
				if (distance <= 3 && deviceGrabbed == false) {	// afisez detalii device
					showPanel = true;
					GameObject.Find ("Panel_DeviceInfo").GetComponent<PanelManagerDeviceInfoScript> ().ShowInfo (hitb.transform.gameObject);
				} else {	// opresc afisarea detaliilor
					showPanel = false;
				}
			} else {
				showMessage = false;
				showPanel = false;
			}
		}
		if (showMessage == true) {
			GameObject.Find("GUIText").GetComponent<Text>().text = "Click to Enter";
		} else {
			GameObject.Find("GUIText").GetComponent<Text>().text = "";
		}
		if (showPanel == true) {
			devicePanelInfo.GetComponent<Canvas>().enabled = true;
		} else {
			devicePanelInfo.GetComponent<Canvas>().enabled = false;
		}

		if (deviceGrabbed == true) {
			Vector3 newpos = Camera.main.transform.forward * 3;
			deviceToPlace.transform.position = transform.position + newpos;
			/*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			deviceToPlace.transform.position = ray.origin + ray.direction * grabDistance;*/
			if (Input.GetKeyDown (KeyCode.F)) {
				deviceGrabbed = false;
				deviceToPlace.transform.tag = "device";
				GameObject.Find("DeviceSpawnManager").GetComponent<LinkManager> ().GenerateLines ();
			}

			// se verifica daca se da de scroll si se modifica rotatia
			float clicks = Mathf.Round(Input.GetAxis("Mouse ScrollWheel") * 100);
			if (clicks != 0) {
				targetAngle += clicks * degreesPerClick;
				startAngle = curAngle;
				startTime = Time.time;
			}

			var t = (Time.time - startTime) / secsPerClick;
			if (t <= 1) {
				curAngle = Mathf.Lerp(startAngle, targetAngle, t);
				// finally, do the actual rotation
				Vector3 rot = deviceToPlace.transform.rotation.eulerAngles;
				rot.y = curAngle;
				deviceToPlace.transform.eulerAngles = rot;
			}
		}
	}
}
