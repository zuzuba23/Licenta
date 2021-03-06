﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour {
	public class item{
		public string name1, name2;

		public item(string name1, string name2){
			this.name1 = name1;
			this.name2 = name2;
		}
	}

	public List<GameObject> devices;
	public List<item> gameobjectsToDrawLineBetween;
	public List<GameObject> gameObjectLineRenderers;

	public void GenerateLines(){
		Init ();
		foreach (GameObject gam in devices) {
			string name1 = gam.GetComponent<DeviceInfo> ().devInfo.getId ();
			if (gam.GetComponent<DeviceInfo> ().devInfo.getType () == "SWITCH") {
				int interfaceCount = gam.GetComponent<DeviceInfo> ().devConn.Count;
				gam.GetComponent<CircleGenerator> ().Spawn (interfaceCount);
				List<GameObject> interfaces = gam.GetComponent<CircleGenerator> ().interfaces;
				foreach (DeviceConnection dc in gam.GetComponent<DeviceInfo>().devConn) {
					string name2 = dc.getConnectedNeighbour ();
					//Debug.Log (name1 + "--->>>" + name2);
					if (name2 == "DEV_0" || name2 == "DEV_-1") {	// marchez interfata ca fiind inactiva si nu desenez nimic
						interfaces[interfaceCount - 1].GetComponent<SphereScript>().SphereOff();
					} else {		//caut cel de-al doilea device si fac legatura cu el
						interfaces[interfaceCount - 1].GetComponent<SphereScript>().SphereOn();
						DrawLineBetween2Objects (interfaces [interfaceCount - 1], name2);
					}
					interfaceCount--;
				}
			} else if(gam.GetComponent<DeviceInfo> ().devInfo.getType () == "ROUTER"){  //nu e switch se face altceva
				int interfaceCount = gam.GetComponent<DeviceInfo> ().devConn.Count;
				gam.GetComponent<CircleGenerator> ().Spawn (interfaceCount);
				List<GameObject> interfaces = gam.GetComponent<CircleGenerator> ().interfaces;
				foreach (DeviceConnection dc in gam.GetComponent<DeviceInfo>().devConn) {
					string name2 = dc.getConnectedNeighbour ();
					//Debug.Log (name1 + "--->>>" + name2);
					if (name2 == "DEV_0" || name2 == "DEV_-1") {	// marchez interfata ca fiind inactiva si nu desenez nimic
						interfaces[interfaceCount - 1].GetComponent<SphereScript>().SphereOff();
					} else {		//caut cel de-al doilea device si fac legatura cu el
						interfaces[interfaceCount - 1].GetComponent<SphereScript>().SphereOn();
						DrawLineBetween2Objects (interfaces [interfaceCount - 1], name2);
					}
					interfaceCount--;
				}
			} else {

			}
		}
		/*
		foreach (item item in gameobjectsToDrawLineBetween) { //Desenez linii intre nume
			DrawLineBetween2Objects (item.name1, item.name2);
		}
		*/

	}

	public void Update(){
		
	}

	public void AddLineRendererComponent(GameObject gam){		//primeste un gameobject si ii adauga un lineRenderer
		LineRenderer line = gam.AddComponent<LineRenderer> ();
		line.sortingOrder = 1;
		line.positionCount = 0;
		line.startWidth = 0.05f; line.endWidth = 0.05f;
		line.startColor = Color.blue;
		line.endColor = Color.red;
		line.material = new Material(Shader.Find("Sprites/Default"));
	}

	public void Init(){
		foreach (GameObject item in gameObjectLineRenderers) {		//distrug toate liniile trasate
			Destroy (item);
		}
		gameobjectsToDrawLineBetween = new List<item> ();
	}

	public GameObject FindGameObjectById(string id){
		foreach (GameObject gam in devices) {
			if (gam.GetComponent<DeviceInfo> ().devInfo.getId () == id) {
				return gam;
			}
		}
		return null;
	}

	public void AddGameObjectsToList(string name1, string name2){	//populez lista de gameObject-uri cu obiecte valide si unice
		
		bool itemExists = false;
		foreach (item item in gameobjectsToDrawLineBetween) {
			if ((item.name1 == name1 && item.name2 == name2) || (item.name1 == name2 && item.name2 == name1)) {
				itemExists = true;
			}
		}
		if (itemExists == false) {
			//Debug.Log (name1 + "--->>>" + name2);
			gameobjectsToDrawLineBetween.Add (new item (name1, name2));
		}
	}

	public void DrawLineBetween2Objects(GameObject interf, string name2){		//generez cate un gameObject si ii atasez un LineRenderer dupa care trasez liniile
		GameObject o1 = interf;
		GameObject o2 = FindGameObjectById (name2);
		if (o1 != null && o2 != null) {
			GameObject line = new GameObject ();
			AddLineRendererComponent (line);
			line.GetComponent<LineRenderer>().positionCount = 3;
			if (o2.GetComponent<DeviceInfo> ().devInfo.getType () == "PC") {	// daca e pc linia va fi mai sus decat centrul obiectului
				line.GetComponent<LineRenderer> ().SetPosition (0, o1.transform.position);
				line.GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (o2.transform.position.x, o1.transform.position.y, o2.transform.position.z));
				line.GetComponent<LineRenderer> ().SetPosition (2, new Vector3 (o2.transform.position.x, o2.transform.position.y + 1, o2.transform.position.z));
			} else {
				line.GetComponent<LineRenderer> ().SetPosition (0, o1.transform.position);
				line.GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (o2.transform.position.x, o1.transform.position.y, o2.transform.position.z));
				line.GetComponent<LineRenderer> ().SetPosition (2, o2.transform.position);
			}
			gameObjectLineRenderers.Add (line);
		}else {	//unul din obiecte nu este aici resetez ce am modificat
			GameObject line = new GameObject ();
			AddLineRendererComponent (line);
			line.transform.position = new Vector3 (-13,7,15);
			line.GetComponent<LineRenderer>().positionCount = 3;
			if (o1 == null && o2 != null) {
				line.GetComponent<LineRenderer>().SetPosition(0, line.transform.position);
				line.GetComponent<LineRenderer>().SetPosition(1, new Vector3 (o2.transform.position.x, line.transform.position.y, o2.transform.position.z));
				line.GetComponent<LineRenderer>().SetPosition(2, o2.transform.position);
			} else if (o2 == null && o1 != null) {
				line.GetComponent<LineRenderer>().SetPosition(0, o1.transform.position);
				line.GetComponent<LineRenderer>().SetPosition(1, new Vector3 (line.transform.position.x, o1.transform.position.y, line.transform.position.z));
				line.GetComponent<LineRenderer>().SetPosition(2, line.transform.position);
			}
			gameObjectLineRenderers.Add (line);
		}
	}
}
