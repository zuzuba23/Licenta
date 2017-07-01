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

public class JsonObjectNeighbours{
	private List<DeviceConnection> response;
	private string version;

	public List<DeviceConnection> getNeighboursList(){
		return response;
	}

	public string getVersion(){
		return version;
	}
}

public class JsonObjectSavePositionStatus{
	private List<SaveResponse> response;
	private string version;

	public List<SaveResponse> getSaveResponse(){
		return response;
	}

	public string getVersion(){
		return version;
	}
}

public class JsonObjectDeviceStatus{
	private List<DeviceStatus> response;
	private string version;

	public List<DeviceStatus> getDeviceStatusResponse(){
		return response;
	}

	public string getVersion(){
		return version;
	}
}