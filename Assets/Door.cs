using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	House parentHouse;

	void Awake() {
		parentHouse = getHouse();
	}

	House getHouse() {
		return gameObject.transform.parent.gameObject.GetComponent<House>();
	}

	void OnTriggerEnter(Collider collider) {
		if(collider != null) {
			Debug.Log ("Hitting something");
			Debug.Log (collider);
			Debug.Log (collider.transform);
			Debug.Log (collider.transform.parent);
			if(collider.transform.parent != null) {
				Debug.Log ("Hitting something with a parent");
				Unit unit = collider.transform.parent.gameObject.GetComponent<Unit>();
				//				unit.collider.enabled = false;
				Debug.Log ("Got unit!");
				Debug.Log(unit);
				if(unit != null) {
					Debug.Log ("Hitting a unit");
					parentHouse.addInside(unit);
				}
			}
		}
	}
}
