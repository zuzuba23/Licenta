using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using JsonFx.Json;

public class WebSocketManagerScript : MonoBehaviour {

	public WebSocket ws;
	public List<World> worldsList;
	public List<Device> deviceList;

	IEnumerator Start(){	//se creeaza conexiunea la WS si primesc informatiile initiale
		ws = new WebSocket (new System.Uri("wss://timf.upg-ploiesti.ro:443/3d/viznet/ws/w/2"));
		yield return StartCoroutine(ws.Connect ());
		StartCoroutine ("GetWorlds", "net_worlds 0");
	}
	// Use this for initialization
	public IEnumerator GetWorlds (string toSendMessage) {		//functie ce trimite un string pe WS pentru a primi info despre worlds
		ws.SendString (toSendMessage);
		worldsList = JsonReader.Deserialize<JsonObjectWorlds> (getWebsocketResponse (ws)).getWorldsList ();
		GameObject.Find ("WorldSpawnManager").GetComponent<WorldSpawnManagerScript> ().SpawnWorlds (worldsList);
		yield return null;
	}
		
	// Update is called once per frame
	void Update () {
		
	}
		
	string getWebsocketResponse(WebSocket ws){	//primesc un string de la WS
		string s;
		do{
			s = ws.RecvString();
		} while(s == null);
		//Debug.Log (s);
		return s;
	}

	public IEnumerator GetDevices(string toSendMessage){	//functie ce trimite un string pe WS pentru a primi info despre device
		ws.SendString (toSendMessage);
		deviceList = JsonReader.Deserialize<JsonObjectDevs> (getWebsocketResponse (ws)).getDeviceList ();
		foreach (Device d in deviceList) {	// pentru fiecare device descoperit trebuie sa ii aflu vecinii
			ws.SendString ("dev_intfs " + d.getId());
			d.neighbours = JsonReader.Deserialize<JsonObjectNeighbours> (getWebsocketResponse (ws)).getNeighboursList ();
		}
		GameObject.Find ("DeviceSpawnManager").GetComponent<DeviceSpawnManagerScript> ().SpawnDevices (deviceList);
		yield return null;
	}
}
