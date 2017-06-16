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
		t.text += d.getType () + "\n";
		t.text += d.getId () + "\n";
		t.text += d.getIpAddress () + "\n";
		t.text += d.getMacAddress () + "\n";
		t.text += d.getHostname () + "\n";
		t.text += d.getStatus ();
	}

	public void HideInfo(){
		TextMesh t = textHolder.GetComponent<TextMesh> ();
		t.text = "";
	}

	public GameObject textHolderSwitch;

	public void Start(){
		textHolderSwitch = GameObject.Find("Canvas").GetComponent<CanvasManager>().panelInfoSwitch;
	}

	public void ShowSwitchInfo(GameObject gObj){		//Afisez detaliile despre device-uri
		Device d = gObj.GetComponent<DeviceInfo> ().devInfo;
		Text t = textHolderSwitch.GetComponentInChildren<Text> ();
		t.text = "";
		t.text += d.getType () + "\n";
		t.text += d.getId () + "\n";
		t.text += d.getIpAddress () + "\n";
		t.text += d.getMacAddress () + "\n";
		t.text += d.getHostname () + "\n";
		t.text += d.getStatus ();
	}
}
