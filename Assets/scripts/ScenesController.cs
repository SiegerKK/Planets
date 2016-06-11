using UnityEngine;
using System.Collections;

public class ScenesController : MonoBehaviour {
	public void loadScene1(){
		Application.LoadLevel ("test1");
	}
	public void loadScene2(){
		Application.LoadLevel ("test2");
	}
	public void loadScene3(){
		Application.LoadLevel ("test3");
	}
}
