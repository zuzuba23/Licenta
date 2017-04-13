using System;
using UnityEngine;
using System.Collections.Generic;
	

public class JsonObject{
	private List<Device> response;
	private string version;

	public List<Device> getDeviceList(){
		return response;
	}

	public string getVersion(){
		return version;
	}
}