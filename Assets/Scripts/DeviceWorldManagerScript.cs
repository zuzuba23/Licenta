using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceWorldManagerScript : MonoBehaviour {
	public GameObject prefab;
	public GameObject parent;

	public void SpawnWorlds(List<Device> deviceList){
		int i = 0;
		foreach (Device d in deviceList) {
			Vector3 position = new Vector3 (-10 + i, 1.5f , 15);
			prefab.GetComponentInChildren<TextMesh> ().text = d.getHostname ();
			Instantiate (prefab, position, Quaternion.identity).transform.parent = parent.transform;
			i += 3;
		}
	}
}
