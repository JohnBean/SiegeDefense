using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {
	float shotAngle;
	float shotPower;
	// Use this for initialization
	void Start () {
		shotAngle=45.0F;
		shotPower=1880.0F*rigidbody.mass;
		rigidbody.AddForce(Mathf.Sin(shotAngle)*shotPower,Mathf.Cos(shotAngle)*shotPower,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void doSomething(float angle){
		shotAngle=angle;
	}
}
