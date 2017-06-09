using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour {

	public Material on,off;

	public void SphereOn(){
		gameObject.GetComponent<MeshRenderer> ().material = on;
	}

	public void SphereOff(){
		gameObject.GetComponent<MeshRenderer> ().material = off;
	}
}
