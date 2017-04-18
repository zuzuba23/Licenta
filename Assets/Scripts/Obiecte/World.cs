using UnityEngine;
using System.Collections;

public class World {
	private string id;
	private string hostname;
	private string description;
	private string group;
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

	/*public device(string i, string h, string m, string mac, string t, int lcc, int sa, double px, double py, double pz, double rox, double roy, double roz, double scx, double scy, double scz, string loc){
		id = i;
		hostname = h;
		managementIpAddress = m;
		macAddress = mac;
		type = t;
		lineCardCount = lcc;
		saved = sa;
		posX = px;
		posY = py;
		posZ = pz;
		rotX = rox;
		rotY = roy;
		rotZ = roz;
		sclX = scx;
		sclY = scy;
		sclZ = scz;
		locationName = loc;
	}

	public Device(){
		
		id = "";
		hostname = "";
		managementIpAddress = "";
		macAddress = "";
		type = "";
		lineCardCount = 0;
		saved = 0;
		pos_x = pos_y = pos_z = rot_x = rot_y = rot_z = scl_x = scl_y = scl_z = 0;
		locationName = "";

	} 
*/

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
}
