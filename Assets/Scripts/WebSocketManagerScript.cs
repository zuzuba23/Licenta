using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebSocketManagerScript : MonoBehaviour {

	public WebSocket ws;
	// Use this for initialization
	IEnumerator Start () {
		ws = new WebSocket (new System.Uri("wss://timf.upg-ploiesti.ro:443/3d/viznet/ws/w/2"));
		yield return StartCoroutine(ws.Connect ());
		ws.SendString ("time");
		yield return new WaitForSeconds (1);
		Debug.Log (ws.RecvString ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
