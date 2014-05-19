using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float height = this.gameObject.transform.position.y + 45.0f;

		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		float yAxisValue = 0.0f;
		if(this.gameObject.transform.position.y >= 3.0f) {
			yAxisValue = Input.GetAxis("Mouse ScrollWheel");
		} else {
			if(Input.GetAxis("Mouse ScrollWheel") > 0.0f) {
				yAxisValue = Input.GetAxis("Mouse ScrollWheel");
			}
		}

		this.gameObject.transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisValue));
		if(height > 0.0f && height < 95.0f) {
			GameObject camera = this.gameObject.transform.FindChild("Main Camera").gameObject;
			camera.transform.localEulerAngles = new Vector3(height, 0.0f, 0.0f);
		}
//		Camera.current.transform.Translate();
	}
}
