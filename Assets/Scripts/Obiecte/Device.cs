using UnityEngine;
using System.Collections;

public class Device
{
	private string id;
	private string hostname;
	private string description;
	private string ipAddress;
	private string macAddress;
	private string type;
	private int saved;
	private double pos_x;
	private double pos_y;
	private double pos_z;
	private double rot_x;
	private double rot_y;
	private double rot_z;
	private double scl_x;
	private double scl_y;
	private double scl_z;
	private string locationName;

	public string ShowDetails(){
		return "id=" + id + " hostname=" + hostname + " managementIpAddress=" + ipAddress + " mac=" + macAddress +" x:" + pos_x + " y:" + pos_y + " z:" + pos_z + "\n";
	}

	public void setPosX(double p){
		pos_x = (float)p;
	}

	public void setPosY(double p){
		pos_y = (float)p;
	}

	public void setPosZ(double p){
		pos_z = (float)p;
	}

	public double getPosX(){
		return pos_x;
	}

	public double getPosY(){
		return pos_y;
	}

	public double getPosZ(){
		return pos_z;
	}

	public string getHostname(){
		return hostname;
	}

	public string getId(){
		return id;
	}

	public Vector3 getPosition(){
		Vector3 v = new Vector3 ((float)pos_x, (float)pos_y, (float)pos_z);
		return v;
	}
}
