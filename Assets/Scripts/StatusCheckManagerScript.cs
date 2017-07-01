using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCheckManagerScript : MonoBehaviour {

	private List<GameObject> devices;

	public void Start(){
		devices = new List<GameObject> ();
	}

	public void WorldDevicesInit(List<GameObject> devs){
		devices.Clear ();
		devices = devs;
		GetIPsFromDevices ();
	}
		
	public void GetIPsFromDevices(){
		if (devices.Count != 0) {
			string IPs = "";
			foreach (GameObject gam in devices) {
				if (gam.GetComponent<DeviceInfo> ().devInfo.getIpAddress () != "")	//verific daca e setat un ip din baza de date sa nu fie ceva invalid in string
					IPs += gam.GetComponent<DeviceInfo> ().devInfo.getIpAddress () + ",";
			}
			if (IPs.Length != 0) {	//inseamna ca a gasit dispozitive si se incearca detectarea statusului
				IPs = IPs.Substring (0, IPs.Length - 1);
				//Debug.Log (IPs);
									//trimit string catre WS si astept raspuns
				StartCoroutine(GameObject.Find("WebSocketManager").GetComponent<WebSocketManagerScript>().GetDevicesStatus("devs_stat " + IPs));
			}
		}
	}

	public void GotStatusFromServer(List<DeviceStatus> devsStatusList){
		foreach (DeviceStatus ds in devsStatusList) {
			
			foreach (GameObject gam in devices) {
				if (gam.GetComponent<DeviceInfo> ().devInfo.getIpAddress () == ds.getIP ()) {
					if (gam.GetComponent<DeviceInfo> ().devInfo.getType () == "PC") {	//verific daca e pc sa schimb culoarea monitorului
						if (ds.getStat () == "up") {
							gam.GetComponent<DeviceInfo> ().ScreenOn ();
							gam.GetComponent<DeviceInfo> ().devInfo.setStatus (ds.getStat ());
						} else {
							gam.GetComponent<DeviceInfo> ().ScreenOff ();
						}
					}else if (gam.GetComponent<DeviceInfo> ().devInfo.getType () == "SWITCH" || gam.GetComponent<DeviceInfo> ().devInfo.getType () == "ROUTER") {		//switch sau router
						gam.GetComponent<DeviceInfo> ().devInfo.setStatus (ds.getStat ());
					} else {//e alt dispozitiv
					}
				} 
			}
		}
	}
}
