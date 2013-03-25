using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {
	//var ball : Rigidbody;// : Rigidbody;
	float shotAngle;
	float shotPower;
	// Use this for initialization
	void Start () {
		shotAngle=45.0F;
		shotPower=1200.0F*rigidbody.mass;
		rigidbody.AddForce(Mathf.Sin(shotAngle)*shotPower,Mathf.Cos(shotAngle)*shotPower,0);//.x=0;//=Vector3(Mathf.Cos(shotAngle)*shotPower,Mathf.Sin (shotAngle),0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
