using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PlayerKeyScript : MonoBehaviour {
	// Use this for initialization
	public static bool isPaused = false;
	public static bool showMessage = false;
	public static bool showPanel = false;
	public static bool deviceGrabbed = false;
	public static bool showStatus = false;
	GameObject deviceToPlace;
	GameObject deviceInfoHolder;
	string GUIMessage;

	float targetAngle = 0f;
	float degreesPerClick = 2f;
	float secsPerClick = 0.3f;
	private float curAngle = 0f;
	private float startAngle=0f;
	private float startTime=0f;

	void Start () {
		GUIMessage = "Click to enter";
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown(0)){	//detectez click
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if (hit.transform.tag == "world") {
					float distance = Vector3.Distance (transform.position, hit.transform.position);
					if (distance <= 3) {
						string id = hit.transform.GetChild (1).GetComponent<TextMesh> ().text;
						GameObject.Find ("WorldSpawnManager").GetComponent<WorldSpawnManagerScript> ().GoToAnotherRoom (id, gameObject);
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.E)) {	// detectez tasta E
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.tag == "device") {
					float distance = Vector3.Distance (transform.position, hit.transform.position);
					if (distance <= 3 && deviceGrabbed == false) {
						deviceToPlace = hit.transform.gameObject;
						deviceGrabbed = true;
						curAngle = deviceToPlace.transform.rotation.eulerAngles.y;  // salvez rotatia initiala a obiectului atunci cand am dat E sa il ridic
						targetAngle = deviceToPlace.transform.rotation.eulerAngles.y;		// la fel si aici deoarece trebuie modificata rotatia pentru fiecare obiect in parte

						if (hit.transform.gameObject.GetComponent<DeviceInfo> ().devInfo.getType () == "SWITCH")
							GameObject.Find("DeviceSpawnManager").GetComponent<LinkManager> ().Init ();
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {	//detectez tasta escape
			if (isPaused == false) {
				isPaused = true;
				gameObject.GetComponent<FirstPersonController> ().enabled = false;
			} else {
				isPaused = false;
				gameObject.GetComponent<FirstPersonController> ().enabled = true;
			}
		}
		if (Input.GetKeyDown (KeyCode.M)) {	// detectez tasta M
			GameObject.Find ("WorldSpawnManager").GetComponent<WorldSpawnManagerScript> ().GoToAnotherRoom ("0", gameObject);
		}
			
		Ray rayb = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitb;
		if (Physics.Raycast (rayb, out hitb)) {
			if (hitb.transform.tag == "world") {
				float distance = Vector3.Distance (transform.position, hitb.transform.position);
				if (distance <= 3) {
					showMessage = true;
				} else
					showMessage = false;
			} else if (hitb.transform.tag == "device") {		// cod pentru afisare device info
				float distance = Vector3.Distance (transform.position, hitb.transform.position);
				if (distance <= 3 && deviceGrabbed == false) {	// afisez detalii device
					showPanel = true;
					deviceInfoHolder = hitb.transform.gameObject;
				} else {	// opresc afisarea detaliilor
					showPanel = false;
				}
			} else {
				showMessage = false;
				showPanel = false;
			}
		}
		if (showMessage == true) {
			GameObject.Find ("GUIText").GetComponent<Text> ().text = GUIMessage;
		} else if(showStatus == false){
			GameObject.Find("GUIText").GetComponent<Text>().text = "";
		}
		if (showPanel == true) {
			if (deviceInfoHolder.GetComponent<DeviceInfo> ().devInfo.getType () == "SWITCH") {	//daca e switch afisez pe GUI
				deviceInfoHolder.GetComponent<DeviceInfoViewer> ().ShowSwitchInfo (deviceInfoHolder);
				deviceInfoHolder.GetComponent<DeviceInfoViewer> ().textHolderSwitch.SetActive (true);
			} else {	//afisez pe obiect
				deviceInfoHolder.GetComponent<DeviceInfoViewer> ().ShowInfo (deviceInfoHolder);
			}
		} else {
			if (deviceInfoHolder != null) {
				if (deviceInfoHolder.GetComponent<DeviceInfo> ().devInfo.getType () == "SWITCH") {
					deviceInfoHolder.GetComponent<DeviceInfoViewer> ().textHolderSwitch.SetActive (false);
				} else {
					deviceInfoHolder.GetComponent<DeviceInfoViewer> ().HideInfo ();
				}
			}
		}

		if (deviceGrabbed == true) {
			if (deviceToPlace.GetComponent<DeviceInfo> ().devInfo.getType () == "PC") {
				Vector3 newpos = Camera.main.transform.forward * 3;
				deviceToPlace.transform.position = transform.position + newpos;
			} else {	//la switch e diferita mutarea
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				deviceToPlace.transform.position = ray.origin + ray.direction * 2;
			}
			if (Input.GetKeyDown (KeyCode.F)) {		//dau drumul la obiect, ii actualizez pozitia in baza de date, generez din nou legaturile
				deviceGrabbed = false;
				deviceToPlace.transform.tag = "device";
				GameObject.Find("DeviceSpawnManager").GetComponent<LinkManager> ().GenerateLines ();
				StartCoroutine(GameObject.Find ("WebSocketManager").GetComponent<WebSocketManagerScript> ().SaveDevicePosition (SavePositionStringGenerate(deviceToPlace)));
			}

			// se verifica daca se da de scroll si se modifica rotatia
			float clicks = Mathf.Round(Input.GetAxis("Mouse ScrollWheel") * 100);
			if (clicks != 0) {
				targetAngle += clicks * degreesPerClick;
				startAngle = curAngle;
				startTime = Time.time;
			}

			var t = (Time.time - startTime) / secsPerClick;
			if (t <= 1) {
				curAngle = Mathf.Lerp(startAngle, targetAngle, t);
				// execut rotatia
				Vector3 rot = deviceToPlace.transform.rotation.eulerAngles;
				rot.y = curAngle;
				deviceToPlace.transform.eulerAngles = rot;
			}
		}
	}

	private string SavePositionStringGenerate(GameObject device){	//metoda ce formeaza stringul ce trebuie trimis pe ws
		Device d = device.GetComponent<DeviceInfo> ().devInfo;
		d.setPosX (device.transform.position.x);
		d.setPosY (device.transform.position.y);
		d.setPosZ (device.transform.position.z);
		//d.setRotX (device.transform.rotation.eulerAngles.x);	//ignor
		d.setRotY (device.transform.rotation.eulerAngles.y);
		//d.setRotZ (device.transform.rotation.eulerAngles.z);	//ignor
		string s = "save_pos_dev ";
		s += d.getId().Replace("DEV_","") + "," + GameObject.Find ("WorldSpawnManager").GetComponent<WorldSpawnManagerScript> ().getCurrentRoomId () + ",";	//id dev si id world
		s += device.transform.position.x + "," + device.transform.position.y + "," + device.transform.position.z + ",";		//pozitii
		s += device.transform.rotation.eulerAngles.x + "," + device.transform.rotation.eulerAngles.y + "," + device.transform.rotation.eulerAngles.z + ",";		//rotatii
		s += d.getSclX() + "," + d.getSclY() + "," + d.getSclZ();

		return s;
	}

	public IEnumerator ShowSavePositionStatus(SaveResponse sr){	//async se modifica status message in status save msg primit pe websocket
		GUIMessage = sr.getMsg();
		showMessage = true;	
		showStatus = true;	//sa nu imi scrie null peste
		GameObject.Find ("GUIText").GetComponent<Text> ().fontSize = 60;
		yield return new WaitForSeconds (2f);
		GameObject.Find ("GUIText").GetComponent<Text> ().fontSize = 23;
		showMessage = false;	//revert dupa 2 secunde
		showStatus = false;
		GUIMessage = "Click to enter";
		yield return null;
	}
}
