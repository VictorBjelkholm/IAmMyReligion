using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

	ArrayList owners;
	ArrayList unitsInside;
//	GameObject doorObject;
	GameObject houseObject;

	public Vector3 getDoorVector() {
		GameObject doorObject = gameObject.transform.Find("DoorModel").gameObject;
		return doorObject.transform.position;
	}

	public void destroy() {
		Destroy(gameObject);
	}

	void Awake() {
		unitsInside = new ArrayList();
//		GameObject doorObject = gameObject.transform.Find("DoorModel").gameObject;
		Debug.Log("Waking once!");
	}

	void OnGUI(){
		if(unitsInside != null) {
			GUI.Box (new Rect (10, 10, 200, 100), "House Inventory: " + unitsInside.Count);
			if (GUI.Button (new Rect (20, 20, 90, 20), "All exit!")) {
				exitAll();
			}
		}
	}

	public void addInside(Unit unit) {
		Debug.Log ("addInside");
		Debug.Log (unit);
		unitsInside.Add(unit);
		unit.deselect();
		unit.gameObject.SetActive(false);
		Debug.Log ("Added unit!");
		Debug.Log (unitsInside.Count);
	}

	public void exitAll() {

	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}
}
