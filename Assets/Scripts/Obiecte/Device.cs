using UnityEngine;
using System.Collections.Generic;

public class Device		//adaugat lista de interfete pentru fiecare device
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

	private string status = "down";
	public List<DeviceConnection> neighbours;

	public string ShowDetails(){
		return "id=" + id + " hostname=" + hostname + " managementIpAddress=" + ipAddress + " mac=" + macAddress +" x:" + pos_x + " y:" + pos_y + " z:" + pos_z + "\nrot: x:" + rot_x + " y:" + rot_y + " z:" + rot_z + "\n";
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

	public void setRotX(double p){
		rot_x = (float)p;
	}

	public void setRotY(double p){
		rot_y = (float)p;
	}

	public void setRotZ(double p){
		rot_z = (float)p;
	}

	public void setStatus(string st){
		status = st;
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

	public double getRotX(){
		return rot_x;
	}

	public double getRotY(){
		return rot_y;
	}

	public double getRotZ(){
		return rot_z;
	}

	public double getSclX(){
		return scl_x;
	}

	public double getSclY(){
		return scl_y;
	}

	public double getSclZ(){
		return scl_z;
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

	public Quaternion getRotation(){
		Vector3 v = new Vector3(0,(float)rot_y,0);
		return Quaternion.Euler (v);
	}

	public string getType(){
		return this.type;
	}

	public string getIpAddress(){
		return ipAddress;
	}

	public string getMacAddress(){
		return macAddress;
	}

	public string getStatus(){
		return status;
	}
}
