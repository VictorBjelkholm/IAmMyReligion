using UnityEngine;
using System.Collections;
using Pathfinding;

public class UnitPather : MonoBehaviour {

	public Vector3 targetPosition;

	private CharacterController controller;

	public Path path;

	private Seeker seeker;

	public float speed = 100;

	public float nextWaypointDistance = 3;

	private int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
		Seeker seeker = GetComponent<Seeker>();
		controller = GetComponent<CharacterController>();

		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
	}

	public void moveTo(Seeker seeker, Vector3 targetPosition) {
//		Debug.Log ("Moving units!");
		seeker.StartPath(transform.position, targetPosition, OnPathComplete);
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		if (path == null) {
			//We have no path to move after yet
			return;
		}
		
		if (currentWaypoint >= path.vectorPath.Count) {
//			Debug.Log ("End Of Path Reached");
			return;
		}
		
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized * speed;
		controller.SimpleMove (dir);
		
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
//		Debug.Log (currentWaypoint);
//		Debug.Log (nextWaypointDistance);
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
//			Debug.Log("Go to next waypoint");
			currentWaypoint++;
			return;
		}
	}

	public void OnPathComplete (Path p) {
//		Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		}
	}
}
