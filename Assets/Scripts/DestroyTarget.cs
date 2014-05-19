using UnityEngine;
using System.Collections;

public class DestroyTarget : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	void DestroyParent() {
		Destroy (this.transform.parent.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
