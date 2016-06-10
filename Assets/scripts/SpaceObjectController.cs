using UnityEngine;
using System.Collections;

public class SpaceObjectController : MonoBehaviour {
	public double mass;
	public Vector3 speed;

	public void Move(double time, double timeWarp){
		transform.position += speed * (float)(time * timeWarp);
	}
}
