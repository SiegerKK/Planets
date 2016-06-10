using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhisicsController : MonoBehaviour {
	//-----------//-Constants//---------//
	const double CONST_TOTAL = UNIT_MASS / (UNIT_LENGTH * UNIT_LENGTH);
	const double CONST_G = 6.67408e-11;
	const double UNIT_MASS = 5.9726e+24;
	const double UNIT_LENGTH = 1.5e+5;
	//-----------//----------//---------//

	public double timeWarp = 1;
	public Transform camera;

	private List<GameObject> objects;
	private double timeCurrent = 0;
	private int wayElements = 0;

	void Start () {
		objects = new List<GameObject> ();
		initObjectsList ();
		printObjectsList ();
	}

	void Update () {
		calculatePhisics ();
		timeCurrent += Time.deltaTime * timeWarp;
		Debug.Log ("End Update(" + timeCurrent / 86400.0 + ")");
	}

	//-----------//--Phisics--//---------//

	private void calculatePhisics(){
		double massMain = 0.0;
		Vector3 speedMain = new Vector3();

		double massOther = 0.0;
		Vector3 speedOther = new Vector3();

		Vector3 dist;

		for (int main = 0; main < objects.Count; main++) {
			massMain = objects [main].gameObject.GetComponent<SpaceObjectController> ().mass;

			for (int i = main + 1; i < objects.Count; i++) {
				massOther = objects[i].gameObject.GetComponent<SpaceObjectController> ().mass;
				dist = distance (objects [main], objects [i]);

				double angleX = Mathf.Cos(Mathf.PI * (Vector3.Angle (Vector3.right, dist) / 180.0f));
				double angleY = Mathf.Cos(Mathf.PI * (Vector3.Angle (Vector3.up, dist) / 180.0f));
				double angleZ = Mathf.Cos(Mathf.PI * (Vector3.Angle (Vector3.forward, dist) / 180.0f));

				speedMain.x = (float)(MainFormula (massOther, dist.magnitude) * Time.deltaTime * angleX / (UNIT_LENGTH * 1000));
				speedMain.y = (float)(MainFormula (massOther, dist.magnitude) * Time.deltaTime * angleY / (UNIT_LENGTH * 1000));
				speedMain.z = (float)(MainFormula (massOther, dist.magnitude) * Time.deltaTime * angleZ / (UNIT_LENGTH * 1000));

				speedOther.x = -(float)(MainFormula (massMain, dist.magnitude) * Time.deltaTime * angleX / (UNIT_LENGTH * 1000));
				speedOther.y = -(float)(MainFormula (massMain, dist.magnitude) * Time.deltaTime * angleY / (UNIT_LENGTH * 1000));
				speedOther.z = -(float)(MainFormula (massMain, dist.magnitude) * Time.deltaTime * angleZ / (UNIT_LENGTH * 1000));

				/*Debug.Log ("Planet: " + objects[i].name + "->" + MainFormula (massMain, dist.magnitude));
				Debug.Log ("Planet: " + objects[main].name + "->" + MainFormula (massOther, dist.magnitude));*/

				objects [i].gameObject.GetComponent<SpaceObjectController> ().speed += speedOther * (float)(timeWarp);
				objects [main].gameObject.GetComponent<SpaceObjectController> ().speed += speedMain * (float)(timeWarp);
			}
			objects [main].GetComponent<SpaceObjectController> ().Move (Time.deltaTime, timeWarp);
			/*if (wayElements < timeCurrent / 86400.0) {
				wayElements = (int)(timeCurrent / 86400.0);
				objects [main].GetComponent<planetWayController> ().addPoint(objects[main].transform.position);
			}*/
		}
	}

	private Vector3 distance(GameObject firstObject, GameObject secondObject){
		return secondObject.transform.position - firstObject.transform.position;
	}


	//-----------//----------//---------//

	private double MainFormula(double mass, double dist){
		double value = CONST_G * (mass * UNIT_MASS) / (dist * dist * UNIT_LENGTH * UNIT_LENGTH * 1000 * 1000);
		return value;
	}

	//-----------//----------//---------//

	private void printObjectsList(){
		for(int i = 0; i < objects.Count; i++)
			Debug.Log ("PhisicsController: list of objects has: " + objects[i].transform.name);
	}

	private void initObjectsList(){
		for (int i = 0; i < transform.childCount; i++) {
			objects.Add (transform.GetChild (i).gameObject);
			Debug.Log ("PhisicsController: List->Added object: " + transform.GetChild (i).name);
			initObjectsList (transform.GetChild (i).gameObject);
		}
	}

	private void initObjectsList(GameObject obj){
		Vector3 speedParent = obj.GetComponent<SpaceObjectController> ().speed;
		obj = obj.transform.Find ("Satelites").gameObject;
		for (int i = 0; i < obj.transform.childCount; i++) {
			objects.Add (obj.transform.GetChild (i).gameObject);
			//----------//
			obj.transform.GetChild (i).gameObject.GetComponent<planetWayController> ().setCamera(camera);
			//----------//
			//obj.transform.GetChild (i).gameObject.GetComponent<SpaceObjectController> ().speed += speedParent;
			Debug.Log ("PhisicsController: List->Added object: " + obj.transform.GetChild (i).name);
			initObjectsList (obj.transform.GetChild (i).gameObject);
		}
	}
}
