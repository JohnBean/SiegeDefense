using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {
	Vector3 screenPoint;
	Vector3 offset;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//BlockFunction ();
		
	}
	
	void onMouseClick()
	{
		
	}
	
	//First mouse button press down.
	void OnMouseDown()
	{
		Debug.Log ("DOWN!");
		screenPoint = Camera.main.WorldToScreenPoint (transform.position);
		//Debug.Log ("ScreenPoint");
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	//While object is being dragged.
	void OnMouseDrag()
	{	
		Vector3 currentScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPos) + offset;
		Debug.Log ("CURRENT POS: " + currentPos);
		transform.position = currentPos;
	}
}
