using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceSpawnManagerScript : MonoBehaviour {
	public GameObject devicePrefab;
	public GameObject parent;
	public List<GameObject> devices;

	public void SpawnDevices(List<Device> deviceList){
		devices = new List<GameObject> ();
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
			Vector3 position;
			float a = count * angle;
			if (d.getType() != "PC") {	//acest device nu este PC, este altceva si fac verificari
				position = /*d.getPosition ();*/ GeneratePosition (center, 8f, a);position.y += 4;
			} else {
				position = /*d.getPosition ();*/ GeneratePosition (center, 8f, a);
			}
			Quaternion rotation = Quaternion.Euler (new Vector3 (0, a + 180, 0));
			GameObject deviceToInstantiate;
			deviceToInstantiate = Instantiate(devicePrefab, position, rotation);
			deviceToInstantiate.transform.parent = parent.transform;
			deviceToInstantiate.GetComponent<DeviceInfo> ().devInfo = d;	//asociez obiectului informatiile despre sine
			deviceToInstantiate.GetComponent<DeviceInfo> ().devConn = d.neighbours;	//asociez obiectului informatiile despre conexiuni
			devices.Add (deviceToInstantiate);	//adaug noul obiect in lista device-urilor din world.
			count++;
		}
		GetComponent<LinkManager> ().devices = devices;	//link manager primeste lista de device-uri din world si genreaza liniile dintre ele
		GetComponent<LinkManager> ().GenerateLines ();
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
