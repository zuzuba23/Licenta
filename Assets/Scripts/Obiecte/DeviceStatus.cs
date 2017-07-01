using System;

public class DeviceStatus
{
	string ip;
	string stat;

	public string getIP(){
		return ip;
	}

	public string getStat(){
		return stat;
	}

	public string Show(){
		return ip + " : " + stat;
	}
}