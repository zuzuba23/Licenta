using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using JsonFx.Json;

public class WebSocketManagerScript : MonoBehaviour {

	public WebSocket ws;
	public List<Device> deviceList;

	// Use this for initialization
	IEnumerator Start () {
		ws = new WebSocket (new System.Uri("wss://timf.upg-ploiesti.ro:443/3d/viznet/ws/w/2"));
		yield return StartCoroutine(ws.Connect ());
		ws.SendString ("net_worlds 0");
		deviceList = JsonReader.Deserialize<JsonObject> (getWebsocketResponse (ws)).getDeviceList ();
		GameObject.Find ("DeviceWorldManager").GetComponent<DeviceWorldManagerScript> ().SpawnWorlds (deviceList);
	}

	string getWebsocketResponse(WebSocket ws){
		string s;
		do{
			s = ws.RecvString();
		} while(s == null);
		Debug.Log (s);
		return s;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
