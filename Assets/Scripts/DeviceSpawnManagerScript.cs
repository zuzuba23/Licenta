using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceSpawnManagerScript : MonoBehaviour {
	public GameObject devicePrefab;
	public GameObject parent;

	public void SpawnDevices(List<Device> deviceList){
		int count = 0;
		int numberOfObjects = deviceList.Count;
		float angle;
		if(numberOfObjects != 0)
			angle = 360 / numberOfObjects;
		else
			angle = 0;
		parent = GameObject.Find ("WorldSpawnManager").GetComponent<WorldSpawnManagerScript> ().parent;
		Vector3 center = new Vector3 (0, 0.5f, 0);
		foreach (Device d in deviceList) {
			devicePrefab.GetComponentInChildren<TextMesh> ().text = d.getHostname ();
			float a = count * angle;
			Vector3 position = d.getPosition ();//GeneratePosition (center, 8f, a);
			Quaternion rotation = Quaternion.Euler (new Vector3 (0, a, 0));
			Instantiate(devicePrefab, position, rotation).transform.parent = parent.transform;
			count++;
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
