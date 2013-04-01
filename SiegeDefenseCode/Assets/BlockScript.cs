using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {
	Vector3 screenPoint;
	Vector3 offset;
	public GameObject cBall;
	public int cost;
	bool destroy;
	bool isCollapsing;
	bool hasBeenHit;
	bool isAttackState;
	bool mouseDown = false;
	bool isStable;	//Stability will be based on phase.  Build phase is stable, attack is unstable.
	bool rigidBodiesAdded;
	public int waitTime;
	int waitIndex;
	
	// Use this for initialization
	void Start () {
		hasBeenHit = false;
		destroy = false;
		isAttackState = false;
		isStable = true;
		isCollapsing = false;
		rigidBodiesAdded= false;
		waitIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//BlockFunction ();
		
		if(Input.GetKeyDown("space"))
		{
			Debug.Log ("Space pressed.");
			isCollapsing = true;
		}
		else if(Input.GetKeyDown ("v"))
		{
			isStable = true;
		}
		
		if(isCollapsing)
		{
			if(rigidBodiesAdded)
			{
				
				destroyBlock();
			}
		}
		/*if(Input.GetKeyDown("space"))
		{
			Debug.Log ("Space pressed.");
			destroy = true;
		}
		if(Input.GetKeyDown ("z"))
		{
			Debug.Log ("Z pressed.");
			destroy = false;
		}*/
		/*if(Input.GetKeyDown ("x"))
		{
			isAttackState = true;
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;

		}
		if(Input.GetKeyDown ("c"))
		{
			isAttackState = false;
		}*/
		
		/*if(!mouseDown)
		{
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;

		}
		mouseDown = false;
		*/
		
		transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		if(isStable)	//No rotations.
		{
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;
		}
		else //Rotation along Z-axis.
		{
			
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;
		
		}
	}
	
	//First mouse button press down.
	void OnMouseDown()
	{
		//Debug.Log ("DOWN!");
		mouseDown = true;
		transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|
			RigidbodyConstraints.FreezeRotationZ|RigidbodyConstraints.FreezePositionZ;
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
					child.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ|RigidbodyConstraints.FreezePositionZ;
					
				}
				
				//All children now have rigidbodies added.
			}
		}
		else if(isAttackState)
		{
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
		mouseDown = true;
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
			
			transform.Rotate (new Vector3(0,0,-90));
			//transform.gameObject.GetComponent<Rigidbody>().AddTorque (new Vector3(0,0,90));
		}
		else if(Input.GetKeyDown (KeyCode.LeftArrow))
		{
			
			transform.Rotate (new Vector3(0,0,90));
			//transform.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0,0,-90));
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag=="Cannonball")
		{
			if(!hasBeenHit)
			{
				hasBeenHit=true;
			}
			else
			{
				isCollapsing = true;
				//destroyBlock ();
			}
		}
	}
	
	void wait()
	{
		
	}
	
	void addRigidBodies()
	{

		if(!rigidBodiesAdded)
		{
			foreach(Transform child in transform)
			{	
				child.gameObject.AddComponent ("Rigidbody");
				child.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ|RigidbodyConstraints.FreezePositionZ;	
			}
			rigidBodiesAdded = true;
		}
		/*for(int i = 0; i<1500; i++)
		{
			Debug.Log ("SCREWING AROUND.");
		}
		Destroy (transform.gameObject);
		*/
	}
	void destroyBlock()
	{
		Destroy (transform.gameObject);
	}

}
