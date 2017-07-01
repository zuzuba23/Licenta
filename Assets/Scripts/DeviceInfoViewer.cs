using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceInfoViewer : MonoBehaviour {
	public GameObject textHolder;


	public void ShowInfo(GameObject gObj){		//Afisez detaliile despre device-uri
		Device d = gObj.GetComponent<DeviceInfo> ().devInfo;
		TextMesh t = textHolder.GetComponent<TextMesh> ();
		t.text = "";
		t.text += "Tip:" + d.getType () + "\n";
		t.text += "Id:" + d.getId () + "\n";
		t.text += "IP:" + d.getIpAddress () + "\n";
		t.text += "MAC:" + d.getMacAddress () + "\n";
		t.text += "Hostname:" + d.getHostname () + "\n";
		t.text += "Status:" + d.getStatus ();
	}

	public void HideInfo(){
		TextMesh t = textHolder.GetComponent<TextMesh> ();
		t.text = "";
	}

	public GameObject panelSwitch;
	public GameObject textHoldSw;
	public GameObject interfaceTextPrefab;
	public GameObject panelIntefaces;

	public void Start(){
		panelSwitch = GameObject.Find("Canvas").GetComponent<CanvasManager>().panelInfoSwitch;
		textHoldSw = GameObject.Find ("Canvas").GetComponent<CanvasManager> ().textHolder;
		interfaceTextPrefab = GameObject.Find ("Canvas").GetComponent<CanvasManager> ().interfaceTextPrefab;
		panelIntefaces = GameObject.Find ("Canvas").GetComponent<CanvasManager> ().panelInterfaces;
	}

	public void ShowSwitchInfo(GameObject gObj){		//Afisez detaliile despre device-uri
		Device d = gObj.GetComponent<DeviceInfo> ().devInfo;
		Text t = textHoldSw.GetComponent<Text> ();
		t.text = "";
		t.text += "Tip:" + d.getType () + "\n";
		t.text += "Id:" + d.getId () + "\n";
		t.text += "IP:" + d.getIpAddress () + "\n";
		t.text += "MAC:" + d.getMacAddress () + "\n";
		t.text += "Hostname:" + d.getHostname () + "\n";
		t.text += "Status:" + d.getStatus ();

		foreach (Transform child in panelIntefaces.transform)
		{
			Destroy (child.gameObject);
		}
		int count = 0;
		t = interfaceTextPrefab.GetComponent<Text> ();
		foreach (DeviceConnection dc in gObj.GetComponent<DeviceInfo>().devConn) {
			t.text = "";
			count++;
			string devName;
			if (GameObject.Find("DeviceSpawnManager").GetComponent<LinkManager>().FindGameObjectById(dc.getConnectedNeighbour ()) != null) {
				t.text += "<color=#00ff00ff>";
				devName = GameObject.Find ("DeviceSpawnManager").GetComponent<LinkManager> ().FindGameObjectById (dc.getConnectedNeighbour ()).GetComponent<DeviceInfo> ().devInfo.getHostname ();
			} else if(dc.getConnectedNeighbour() != "DEV_0"){
				t.text += "<color=#00ff00ff>";
				devName = dc.getConnectedNeighbour() + "(OUT)";
			} else {
				t.text += "<color=#ff0000ff>";
				devName = "NoDev";
			}
			t.text += devName + "</color>";
			interfaceTextPrefab.GetComponent<Text> ().text = t.text;
			(Instantiate (interfaceTextPrefab, panelIntefaces.transform.position, panelIntefaces.transform.rotation)).transform.SetParent (panelIntefaces.transform, false);
		}
	}
}
