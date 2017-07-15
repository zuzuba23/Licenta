using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using JsonFx.Json;
using UnityEngine.UI;

public class WebSocketManagerScript : MonoBehaviour {

	public WebSocket ws;
	public List<World> worldsList;
	public List<Device> deviceList;
	public GameObject wsStatusText;

	IEnumerator Start(){	//se creeaza conexiunea la WS si primesc informatiile initiale
		ws = new WebSocket (new System.Uri("ws://localhost:8080"));
		yield return StartCoroutine(ws.Connect ());
		yield return new WaitForSeconds(1);
		GetWorlds("net_worlds 0");
		
	}

	IEnumerator WSReConnect(){
		yield return StartCoroutine(ws.Connect ());
	}


	public void GetWorlds (string toSendMessage) {		//functie ce trimite un string pe WS pentru a primi info despre worlds
		ws.SendString (toSendMessage);
		Debug.Log("trimeis " + toSendMessage);
		worldsList = JsonReader.Deserialize<JsonObjectWorlds> (getWebsocketResponse (ws)).getWorldsList ();
		GameObject.Find ("WorldSpawnManager").GetComponent<WorldSpawnManagerScript> ().SpawnWorlds (worldsList);
	}
		
	// Update is called once per frame
	void Update () {
		if (ws.isConnected () == false) {
			wsStatusText.GetComponent<Text> ().text = "<color=#ff0000ff>WebSocket Disconnected</color>";
		} else {
			wsStatusText.GetComponent<Text> ().text = "<color=#00ff00ff>WebSocket Connected</color>";
		}
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
			//Debug.Log (d.ShowDetails ());
		}
		GameObject.Find ("DeviceSpawnManager").GetComponent<DeviceSpawnManagerScript> ().SpawnDevices (deviceList);
		yield return null;
	}

	public IEnumerator SaveDevicePosition(string toSendMessage){
		//Debug.Log (toSendMessage);
		ws.SendString (toSendMessage);
		SaveResponse s = JsonReader.Deserialize<JsonObjectSavePositionStatus>(getWebsocketResponse(ws)).getSaveResponse()[0];
		StartCoroutine(GameObject.Find ("FPSController").GetComponent<PlayerKeyScript> ().ShowSavePositionStatus (s));
		yield return null;
	}

	public IEnumerator GetDevicesStatus(string toSendMessage){
		ws.SendString (toSendMessage);
		List<DeviceStatus> devStatusList = JsonReader.Deserialize<JsonObjectDeviceStatus> (getWebsocketResponse (ws)).getDeviceStatusResponse ();
		GameObject.Find ("StatusCheckManager").GetComponent<StatusCheckManagerScript> ().GotStatusFromServer (devStatusList);
		yield return null;
	}

	public Device GetDeviceById(string toSendMessage){
		ws.SendString (toSendMessage);
		Device d = JsonReader.Deserialize<JsonObjectDevs> (getWebsocketResponse (ws)).getDeviceList ()[0];
		return d;
	}
}