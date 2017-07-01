using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceInfo : MonoBehaviour {
	public Device devInfo;
	public List<DeviceConnection> devConn;
	public GameObject screen;
	public Material screen_on, screen_off;

	public void ScreenOn(){
		screen.GetComponent<MeshRenderer> ().material = screen_on;
	}

	public void ScreenOff(){
		screen.GetComponent<MeshRenderer> ().material = screen_off;
	}
}