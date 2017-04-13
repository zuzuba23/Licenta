using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerKeyScript : MonoBehaviour {
	// Use this for initialization
	public static bool isPaused = false;
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
						Debug.Log (hit.transform.GetComponentInChildren<TextMesh> ().text);
						foreach (GameObject g in GameObject.FindGameObjectsWithTag("door"))
							Destroy (g);
						GameObject.Find ("DeviceWorldManager").GetComponent<DeviceWorldManagerScript> ().GoToAnotherRoom (hit.transform.GetComponentInChildren<TextMesh> ().text, gameObject);
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
	}
}
