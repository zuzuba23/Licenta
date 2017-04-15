using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceWorldManagerScript : MonoBehaviour {
	public GameObject WorldPrefab;
	public GameObject parent;
	public GameObject roomPrefab;

	private int noOfSpawns = 0;

	//spawnez usi pentru un parinte room in spatiul local
	public void SpawnWorlds(List<Device> deviceList){
		int i = 0;
		foreach (Device d in deviceList) {
			Debug.Log (d.ShowDetails());
			Vector3 position;
			WorldPrefab.transform.GetChild (0).GetComponent<TextMesh> ().text = d.getHostname ();
			WorldPrefab.transform.GetChild (1).GetComponent<TextMesh> ().text = d.getId ();
			if (i >= 72) {
				position = new Vector3 (10 - i + 72, 1.5f, - 15);
				Instantiate (WorldPrefab, position, Quaternion.Euler(new Vector3(0,180,0))).transform.parent = parent.transform;
			}
			else if (i >= 48) {
				position = new Vector3 (-15, 1.5f, 10 - i + 48);
				Instantiate (WorldPrefab, position, Quaternion.Euler(new Vector3(0,-90,0))).transform.parent = parent.transform;
			}
			else if (i >= 24) {
				position = new Vector3 (15, 1.5f, 10 - i + 24);
				Instantiate (WorldPrefab, position, Quaternion.Euler(new Vector3(0,90,0))).transform.parent = parent.transform;
			} else {
				position = new Vector3 (-10 + i, 1.5f, + 15);
				Instantiate (WorldPrefab, position, Quaternion.identity).transform.parent = parent.transform;
			}
			i += 4;
		}
	}

	//trebuie sa spawnez o noua camera si sa setez gameobject parent la aceasta camera pentru spawn local
	public void GoToAnotherRoom(string id, GameObject player){
		Vector3 position = new Vector3 (0, 0, 0);
		Destroy (parent);    //Distruge camera in care am fost
		parent = Instantiate (roomPrefab, position, Quaternion.identity);
		player.transform.position = position;

		//testing purpose lines
		StartCoroutine(GameObject.Find("WebSocketManager").GetComponent<WebSocketManagerScript>().GetDevices("net_devs " + id));
	}
}
