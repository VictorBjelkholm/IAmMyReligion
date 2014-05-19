using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
	
	RaycastHit hit;
	
	public GameObject target;

	private GameObject selectedObject;
	private ArrayList selectedUnits = new ArrayList();

	private float raycastLength = Mathf.Infinity;
	
	// Use this for initialization
	void Start () {
		
	}

	void selectedEnter(GameObject houseGameObject) {
		House house = HouseFromGameObject(houseGameObject);
		Vector3 houseDoorLocation = house.getDoorVector();
		moveUnitsTo(houseDoorLocation);
	}

	void SelectUnit(GameObject unitGameObject) {
		Unit unit = UnitFromGameObject(unitGameObject);
		unit.select();
		selectedUnits.Add(unit);
	}

	void UnselectUnit(GameObject unitGameObject) {
		Unit unit = UnitFromGameObject(unitGameObject);
		unit.deselect();
		if(selectedUnits.Count > 1) {
			selectedUnits.Remove(unit);
		} else {
			selectedUnits.Clear();
		}
	}

	bool isUnitSelected(GameObject unitGameObject) {
		Unit unit = UnitFromGameObject(unitGameObject);
		if(selectedUnits.IndexOf(unit) >= 0) {
			return true;
		} else {
			return false;
		}
	}

	bool isUnitsSelected() {
		if(selectedUnits.Count > 0) {
			return true;
		} else {
			return false;
		}
	}

	void UnselectAllUnits() {
		for(int i = 0; i < selectedUnits.Count; i++) {
			Unit currentUnit = selectedUnits[i] as Unit;
			currentUnit.deselect();
		}
		selectedUnits.Clear();
	}

	void OnGUI(){
		if(isUnitsSelected()) {
			GUI.Box (new Rect (Screen.width - 200, Screen.height - 100, 200, 100), "Agent Actions");
			if (GUI.Button (new Rect (Screen.width - 195, Screen.height - 80, 90, 20), "Preach")) {
				preachStart();
			}
			if (GUI.Button (new Rect (Screen.width - 95, Screen.height - 80, 90, 20), "Stop Preach")) {
				preachStop();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = rayFromMousePoint();
		if(isLeftMouseButtonDown() && GUIUtility.hotControl == 0) {
			if(isRaycastColliding(ray)) {
//				Debug.Log (hit.collider.name);
				if(!isShiftDown()) {
					UnselectAllUnits();
				}
				if(isCollidingWithUnit(hit)) {
					GameObject selectedUnit = gameObjectFromHit(hit);
					if(isShiftDown() && isUnitSelected(selectedUnit)) {
						UnselectUnit(selectedUnit);
					} else {
						SelectUnit(selectedUnit);
					}
				}
			}
		}
		if(isRightMouseButtonDown()) {
			if(isRaycastColliding(ray)) {
				if(isCollidingWithGround(hit)) {
					if(selectedUnits.Count > 0) {
						moveUnitsTo(hit.point);
					}
					GameObject targetObj = Instantiate(target, hit.point, Quaternion.identity) as GameObject;
				}
				if(isCollidingWithHouse(hit)) {
					if(selectedUnits.Count > 0) {
						GameObject house = gameObjectFromHit(hit);
						selectedEnter(house);
						Debug.Log ("Unit should enter building now!");
					}
				}
			}
		}
		
		Debug.DrawRay(ray.origin, ray.direction * raycastLength, Color.red);
	}

	bool isShiftDown() {
		return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
	}

	bool isLeftMouseButtonDown() {
		return Input.GetMouseButtonDown(0);
	}

	bool isRightMouseButtonDown() {
		return Input.GetMouseButtonDown(1);
	}

	void moveUnitsTo(Vector3 position) {
		for(int i = 0; i < selectedUnits.Count; i++) {
			Unit currentUnit = selectedUnits[i] as Unit;
			currentUnit.moveTo(position);
		}
	}

	void preachStart() {
		preach(true);
	}

	void preachStop() {
		preach(false);
	}

	void preach(bool boolean) {
		for(int i = 0; i < selectedUnits.Count; i++) {
			GameObject currentUnit = selectedUnits[i] as GameObject;
			GameObject preachLight = currentUnit.transform.FindChild("PreachingLight").gameObject;
			preachLight.SetActive(boolean);
		}
	}

	GameObject gameObjectFromHit(RaycastHit hit) {
		return hit.collider.gameObject;
	}
	
	static public Unit UnitFromGameObject(GameObject unitGameObject) {
		return unitGameObject.transform.parent.gameObject.GetComponent<Unit>();
	}

	static public House HouseFromGameObject(GameObject houseGameObject) {
		return houseGameObject.transform.parent.gameObject.GetComponent<House>();
	}

	Ray rayFromMousePoint() {
		return Camera.main.ScreenPointToRay (Input.mousePosition);
	}

	bool isCollidingWithGround(RaycastHit hit) {
		return hit.collider.name == "Ground";
	}

	bool isCollidingWithUnit(RaycastHit hit) {
		return hit.collider.name == "UnitModel";
	}

	bool isCollidingWithHouse(RaycastHit hit) {
		return hit.collider.name == "HouseModel";
	}

	bool isRaycastColliding(Ray ray) {
		return Physics.Raycast(ray, out hit, raycastLength);
	}
}
