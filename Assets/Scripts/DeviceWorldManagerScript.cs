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
			Vector3 position = new Vector3 (-10 + i, 1.5f , noOfSpawns * 50 + 15);
			WorldPrefab.GetComponentInChildren<TextMesh> ().text = d.getHostname ();
			Instantiate (WorldPrefab, position, Quaternion.identity).transform.parent = parent.transform;
			i += 3;
		}
	}

	//trebuie sa spawnez o noua camera si sa setez gameobject parent la aceasta camera pentru spawn local
	public void GoToAnotherRoom(string name, GameObject player){
		noOfSpawns++;
		Vector3 position = new Vector3 (0, 0, noOfSpawns * 50);
		//Destroy (parent);    //Distruge camera in care am fost
		parent = Instantiate (roomPrefab, position, Quaternion.identity);
		player.transform.position = position;


		//testing purpose lines
		List<Device> deviceList = GameObject.Find("WebSocketManager").GetComponent<WebSocketManagerScript>().deviceList;
		SpawnWorlds (deviceList);
	}
}
