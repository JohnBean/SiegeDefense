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
		
		Vector3 mousePointerWorldPos = Camera.main.ScreenToWorldPoint( Input.mousePosition);
		//Debug.Log ("MOUSEPOINTERWORLD: " + mousePointerWorldPos);
	}
	
	void onMouseClick()
	{
		
	}
	
	void BlockFunction()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Debug.Log ("MOUSE BUTTON 0");
			//Debug.Log ("DRAG!");
		}
		else if(Input.GetMouseButtonDown (1))
		{
			Debug.Log ("MOUSE BUTTON 1");
		}
	}
	
	void OnMouseDown()
	{
		Debug.Log ("DOWN!");
		screenPoint = Camera.main.WorldToScreenPoint (transform.position);
		//Debug.Log ("ScreenPoint");
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		/*while (Input.GetMouseButton(0))
    	{
			Debug.Log ("DRAGGING!");
		}*/
	}
	void OnMouseDrag()
	{	
		Debug.Log ("DRAGGING.");
		
		
		
		Vector3 currentScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    
		Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPos) + offset;
       
		Debug.Log ("CURRENT POS: " + currentPos);
		transform.position = currentPos;
	
		//Debug.Log ("DRAGGING!");
	}
}
