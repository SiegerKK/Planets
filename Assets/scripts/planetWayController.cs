using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class planetWayController : MonoBehaviour {
	public GameObject wayElement;

	private List<Point> points;
	private List<GameObject> wayElements;
	private Transform camera;

	void Start(){
		points = new List<Point> ();
		wayElements = new List<GameObject> ();
		points.Add (new Point (transform.position.x, transform.position.y, transform.position.z));
	}

	public void setCamera(Transform cam){
		this.camera = cam;
	}

	public void addPoint(Vector3 position){
		addPoint (position.x, position.y, position.z);
	}
	public void addPoint(double x, double y, double z){
		points.Add (new Point (x, y, z));
		wayElements.Add((GameObject)Instantiate (wayElement, (points [points.Count - 2].getVector () + points [points.Count - 1].getVector()) / 2, transform.rotation));
	}
}

class Point{
	public float x, y, z;

	public Point(double x1, double y1, double z1){
		x = (float)x1;
		y = (float)y1;
		z = (float)z1;
	}

	public Vector3 getVector(){
		return new Vector3 (x, y, z);
	}
}
