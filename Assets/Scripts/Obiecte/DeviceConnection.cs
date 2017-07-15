using UnityEngine;
using System.Collections;

public class DeviceConnection
{
	private string id;
	private string deviceId;
	private string hostname;
	private string managementIpAddress;
	private string macAddress;
	private string type;
	private string serialNo;
	private string status;
	private string lineCardCount;
	private string ifIndex;
	private string connectedNeighbor;
	private string connectedNeighborHostname;
	private string locationName;

	public string ShowDetails(){
		return "" + "id=" + id + " devID=" + deviceId + " hostname=" + hostname + " neighbour=" + connectedNeighbor + " status=" + status + "\n";
	}

	public string getConnectedNeighbour(){
		return connectedNeighbor;
	}

	public string getStatus(){
		return status;
	}

	public string getConnectedNeighbourHostname(){
		return connectedNeighborHostname;
	}
}