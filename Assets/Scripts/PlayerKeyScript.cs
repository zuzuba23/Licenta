using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PlayerKeyScript : MonoBehaviour {
	// Use this for initialization
	public static bool isPaused = false;
	public static bool showMessage = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if (hit.transform.tag == "door") {
					float distance = Vector3.Distance (transform.position, hit.transform.position);
					if (distance <= 3) {
						string id = hit.transform.GetChild (1).GetComponent<TextMesh> ().text;
						GameObject.Find ("DeviceWorldManager").GetComponent<DeviceWorldManagerScript> ().GoToAnotherRoom (id, gameObject);
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isPaused == false) {
				isPaused = true;
				gameObject.GetComponent<FirstPersonController> ().enabled = false;
			} else {
				isPaused = false;
				gameObject.GetComponent<FirstPersonController> ().enabled = true;
			}
		}

		Ray rayb = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitb;
		if (Physics.Raycast (rayb, out hitb)) {
			if (hitb.transform.tag == "door") {
				float distance = Vector3.Distance (transform.position, hitb.transform.position);
				if (distance <= 3) {
					showMessage = true;
				} else
					showMessage = false;
			} else
				showMessage = false;
		}
		if (showMessage == true) {
			GameObject.Find("GUIText").GetComponent<Text>().text = "Click to Enter";
		} else {
			GameObject.Find("GUIText").GetComponent<Text>().text = "";
		}
	}
}
