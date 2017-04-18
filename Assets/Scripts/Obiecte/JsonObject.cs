using System;
using UnityEngine;
using System.Collections.Generic;
	

public class JsonObjectWorlds{
	private List<World> response;
	private string version;

	public List<World> getWorldsList(){
		return response;
	}

	public string getVersion(){
		return version;
	}
}

public class JsonObjectDevs{
	private List<Device> response;
	private string version;

	public List<Device> getDeviceList(){
		return response;
	}

	public string getVersion(){
		return version;
	}
}