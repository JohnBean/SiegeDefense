using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {
	Vector3 screenPoint;
	Vector3 offset;
	public int price;
	bool destroy;
	bool hasBeenHit;
	bool isAttackState;
	// Use this for initialization
	void Start () {
		hasBeenHit = false;
		destroy = false;
		isAttackState = false;
	}
	
	// Update is called once per frame
	void Update () {
		//BlockFunction ();
		if(Input.GetKeyDown("space"))
		{
			Debug.Log ("Space pressed.");
			destroy = true;
		}
		if(Input.GetKeyDown ("z"))
		{
			Debug.Log ("Z pressed.");
			destroy = false;
		}
		if(Input.GetKeyDown ("x"))
		{
			isAttackState = true;
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;

		}
		if(Input.GetKeyDown ("c"))
		{
			isAttackState = false;
		}
	}
	
	//First mouse button press down.
	void OnMouseDown()
	{
		Debug.Log ("DOWN!");
		if(destroy)
		{
			if(!hasBeenHit)
			{
				hasBeenHit = true;
			}
			else
			{
				//Add rigidbodies to EVERYTHING.
				Debug.Log ("Checking rigidbodies...");
				foreach(Transform child in transform)
				{
					child.gameObject.AddComponent ("Rigidbody");
					child.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;
					
				}
				
				//All children now have rigidbodies added.
			}
		}
		else if(isAttackState)
		{
			/*foreach(Transform child in transform)
				{
					child.gameObject.AddComponent ("Rigidbody");
					child.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ|RigidbodyConstraints.FreezePositionZ;
					
				}*/
			
					}
		else
		{
			screenPoint = Camera.main.WorldToScreenPoint (transform.position);
			//Debug.Log ("ScreenPoint");
			offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}
	//While object is being dragged.
	void OnMouseDrag()
	{	
		if(!destroy)
		{
			Vector3 currentScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPos) + offset;
			//Debug.Log ("CURRENT POS: " + currentPos);
			transform.position = currentPos;
		}
		else
		{
			
		}
		rotate ();
	}
	
	void rotate()
	{
		//Debug.Log ("ROTATING!");
		if(Input.GetKeyDown (KeyCode.RightArrow))
		{
			Debug.Log ("ROTATING RIGHT!");
			float rotation = transform.rotation.z;
			
			transform.Rotate (new Vector3(0,0,-90));
			//transform.gameObject.GetComponent<Rigidbody>().AddTorque (new Vector3(0,0,90));
		}
		else if(Input.GetKeyDown (KeyCode.LeftArrow))
		{
			float rotation = transform.rotation.z;
			
			transform.Rotate (new Vector3(0,0,90));
			//transform.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0,0,-90));
		}
	}
	
	void destroyBlock()
	{
		
	}
}
