using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyScript : MonoBehaviour {

	// Use this for initialization
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
						foreach (GameObject g in GameObject.FindGameObjectsWithTag("door"))
							Destroy (g);
						transform.position = new Vector3 (0, 1, 50);
					}
				}
			}
		}
	}
}
