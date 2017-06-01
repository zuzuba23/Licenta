using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using JsonFx.Json;

public class WebSocketManagerScript : MonoBehaviour {

	public WebSocket ws;
	public List<World> worldsList;
	public List<Device> deviceList;

	IEnumerator Start(){
		ws = new WebSocket (new System.Uri("wss://timf.upg-ploiesti.ro:443/3d/viznet/ws/w/2"));
		yield return StartCoroutine(ws.Connect ());
		StartCoroutine ("GetWorlds", "net_worlds 0");
	}
	// Use this for initialization
	public IEnumerator GetWorlds (string toSendMessage) {
		ws.SendString (toSendMessage);
		worldsList = JsonReader.Deserialize<JsonObjectWorlds> (getWebsocketResponse (ws)).getWorldsList ();
		GameObject.Find ("WorldSpawnManager").GetComponent<WorldSpawnManagerScript> ().SpawnWorlds (worldsList);
		yield return null;
	}
		
	// Update is called once per frame
	void Update () {
		
	}
		
	string getWebsocketResponse(WebSocket ws){
		string s;
		do{
			s = ws.RecvString();
		} while(s == null);
		//Debug.Log (s);
		return s;
	}

	public IEnumerator GetDevices(string toSendMessage){
		ws.SendString (toSendMessage);
		deviceList = JsonReader.Deserialize<JsonObjectDevs> (getWebsocketResponse (ws)).getDeviceList ();
		foreach (Device d in deviceList) {
			ws.SendString ("dev_intfs " + d.getId());
			d.neighbours = JsonReader.Deserialize<JsonObjectNeighbours> (getWebsocketResponse (ws)).getNeighboursList ();
		}
		GameObject.Find ("DeviceSpawnManager").GetComponent<DeviceSpawnManagerScript> ().SpawnDevices (deviceList);
		yield return null;
	}
}
