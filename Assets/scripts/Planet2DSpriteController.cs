using UnityEngine;
using System.Collections;

public class Planet2DSpriteController : MonoBehaviour {
	public Transform camera;
	public float scale = 1;

	void Update () {
		transform.LookAt (camera);
		Vector3 dist = transform.position - camera.position;
		transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f) * dist.magnitude / 1000 * scale;
	}
}
