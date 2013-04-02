using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {
	Vector3 screenPoint;
	Vector3 offset;
	public Transform cBall;
	public int cost;
	bool destroy;
	bool isCollapsing;
	bool hasBeenHit;
	bool rigidBodiesAdded;
	
	bool constraintsFixed;	//Stability will be based on phase.  Build phase is stable, attack is unstable.
	
	public int waitTime;
	int waitIndex;
	
	// Use this for initialization
	void Start () {
		hasBeenHit = false;
		destroy = false;
		constraintsFixed = false;
		isCollapsing = false;
		rigidBodiesAdded= false;
		waitIndex = 0;
		transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ|RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezePositionZ;
	}
	
	// Update is called once per frame
	void Update () {
		//BlockFunction ();
			
		if(isCollapsing)
		{
			if(rigidBodiesAdded)
			{
				if(waitIndex > waitTime)
				{
					destroyBlock ();
					waitIndex = 0;
				}
				else
				{
					waitIndex++;
				}
				//destroyBlock();
			}
			else
			{
				addRigidBodies ();
			}
		}
		
		
		//transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		/*if(isStable)	//No rotations.
		{
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;
		}
		else //Rotation along Z-axis.
		{
			
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;
		}
		*/
	}
	
	//First mouse button press down.
	void OnMouseDown()
	{
		//Debug.Log ("DOWN!");
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

		else
		{
			screenPoint = Camera.main.WorldToScreenPoint (transform.position);
			//Debug.Log ("ScreenPoint");
			offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}
	
	void OnMouseUp()
	{
		Debug.Log ("MOUSE UP!");
		if(!constraintsFixed)
		{
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;
			
			constraintsFixed = true;
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
		Debug.Log ("What was that?");
		if(collision.collider.tag=="Cannonball")
		{
			Debug.Log ("Was that a cannonball?");
			if(!hasBeenHit)
			{
				hasBeenHit=true;
			}
			else
			{
				Debug.Log ("COLLAPSING!");
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
				child.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;	
				child.gameObject.GetComponent<Rigidbody>().mass = 5f;
				child.gameObject.GetComponent<Rigidbody>().drag = 0.99f;
				child.gameObject.GetComponent<Rigidbody>().angularDrag = 0.92f;
			}
			Destroy(transform.gameObject.GetComponent<Rigidbody>());
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
		//Destroy (transform.gameObject);
	}

}
