using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManagerDeviceInfoScript : MonoBehaviour {
	public GameObject text_type;
	public GameObject text_deviceId;
	public GameObject text_IP;
	public GameObject text_MAC;
	public GameObject text_Hostname;
	public GameObject text_Status;
	public GameObject text_etc;


	public void ShowInfo(GameObject gObj){		//Afisez detaliile despre device-uri
		Device d = gObj.GetComponent<DeviceInfo> ().devInfo;
		text_type.GetComponent<Text> ().text = d.getType ();
		text_deviceId.GetComponent<Text> ().text = d.getId ();
		text_IP.GetComponent<Text> ().text = d.getIpAddress ();
		text_MAC.GetComponent<Text> ().text = d.getMacAddress ();
		text_Hostname.GetComponent<Text> ().text = d.getHostname ();
		text_Status.GetComponent<Text> ().text = d.getStatus ();
	}
}
