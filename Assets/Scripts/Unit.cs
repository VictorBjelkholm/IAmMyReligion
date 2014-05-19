using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	private float speed;
	public float belief;
	public bool preaching;
	public float reach;
	public bool selected;
	private GameObject selectionObject;

	void Awake () {
//		gameObject.tag = "Selectable";
	}

	public void select() {
		selected = true;
		GameObject unitSelection = getSelection();
		unitSelection.SetActive(true);
	}

	public void deselect() {
		selected = false;
		GameObject unitSelection = getSelection();
		unitSelection.SetActive(false);
	}

	public void moveTo(Vector3 target) {
		UnitPather unitPath = getUnitPather();
		Seeker seeker = getSeeker();
		unitPath.moveTo(seeker, target);
	}

	GameObject getSelection() {
		GameObject selectionObject = gameObject.transform.Find("Selection").gameObject;
		return selectionObject;
	}

	UnitPather getUnitPather() {
		return gameObject.GetComponent<UnitPather>();
	}

	Seeker getSeeker() {
		return gameObject.GetComponent<Seeker>();
	}
}
