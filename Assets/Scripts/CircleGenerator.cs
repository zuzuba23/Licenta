using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour {

	public GameObject sphere;
	public List<GameObject> interfaces;

	public void Spawn (int numObj) {
		if (interfaces.Count == 0) {	//first time init
			foreach (GameObject gam in interfaces)
				Destroy (gam);
			interfaces.Clear ();
			int count = 0;
			int numberOfObjects = numObj;
			float angle;
			if(numberOfObjects != 0)
				angle = 360 / numberOfObjects;
			else
				angle = 0;
			Vector3 center = transform.localPosition;

			for (int i = 1; i <= numObj; i++) {
				Vector3 position;
				float a = count * angle;
				position = /*d.getPosition ();*/ GeneratePosition (center, 1f, a);
				Quaternion rotation = Quaternion.Euler (new Vector3 (0, a + 180, 0));
				GameObject gam = Instantiate (sphere, position, rotation);
				gam.transform.parent = transform;
				interfaces.Add(gam);
				count++;
			}
		} else {		//pastrez pozitiile

		}

	}


	private Vector3 GeneratePosition(Vector3 center, float radius,float a)
	{
		float ang = a;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos;
	}

}
