using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour {
	public List<GameObject> devices;
	public int lineCount;
	LineRenderer line;

	public void GenerareLinii(){
		Init ();
		foreach (GameObject gam in devices) {
			string name1 = gam.GetComponent<DeviceInfo> ().devInfo.getId ();
			foreach(DeviceConnection dc in gam.GetComponent<DeviceInfo>().devConn){
				string name2 = dc.getConnectedNeighbour ();
				if (name2 != "DEV_0" && name2 != "DEV_-1") {	//Desenez linii intre nume
					DesenareLiniiIntreObiecte(name1,name2);
				}
			}
		}
	}

	public void Update(){
		
	}

	public void Start(){
		line = gameObject.AddComponent<LineRenderer> ();
	}

	public void Init(){
		lineCount = 0;
		line.startWidth = 0.05f; line.endWidth = 0.05f;
		line.startColor = Color.blue;
		line.endColor = Color.red;
		line.material = new Material(Shader.Find("Particles/Additive"));
	}

	public GameObject FindGameObjectById(string id){
		foreach (GameObject gam in devices) {
			if (gam.GetComponent<DeviceInfo> ().devInfo.getId () == id) {
				return gam;
			}
		}
		return null;
	}

	public void DesenareLiniiIntreObiecte(string name1, string name2){
		Debug.Log (name1 + "--->>>" + name2);
		lineCount += 2;
		line.positionCount = lineCount;
		GameObject o1 = FindGameObjectById (name1);
		GameObject o2 = FindGameObjectById (name2);
		if (o1 != null && o2 != null) {	//verific daca exista obiectele in aceeasi camera
			line.SetPosition (lineCount - 2, o1.transform.position);
			line.SetPosition (lineCount - 1, o2.transform.position);
		} else {	//unul din obiecte nu este aici resetez ce am modificat
			lineCount -= 2;
			line.positionCount = lineCount;
		}
	}
}
