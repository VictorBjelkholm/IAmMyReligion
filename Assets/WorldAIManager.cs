using UnityEngine;
using System.Collections;

public class WorldAIManager : MonoBehaviour {


	public float AICount;
	public float Restlessness;
	public ArrayList AllUnits;

	// Use this for initialization
	void Start () {
		Vector3 randomVector = Vector3.zero;
		for(int i = 0; i < AICount; i++) {
			randomVector.x = Random.Range(0, 10);
			randomVector.z = Random.Range(0, 10);
			GameObject newUnit = Resources.Load<GameObject>("Unit");
//			Debug.Log("New unit underway!");
//			Debug.Log(newUnit);
			Instantiate(newUnit, randomVector, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
