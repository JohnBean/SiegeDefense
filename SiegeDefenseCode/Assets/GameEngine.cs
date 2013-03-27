using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {
	enum gameState {splash, build, attack, win, lose};
	gameState state;//states    0-Opening splash screen.    1-Build Mode    2-Attack mode    3-Win state    4-Lose state
	Texture splashScreen;
	public Rigidbody RB;
	Quaternion cannonRotation;
	int round;
	int maxRounds;
	public float shotAngle;
	public float shotPower;
	float shotTime;
	float shotWait;
	// Use this for initialization
	void Start () {
		state=gameState.splash;
		round = 1;
		maxRounds =7;
		shotAngle= 0;
		shotPower= 0;
		shotWait = 0;//used to make sure the cannonball has been stationary for long enough before advancing
		cannonRotation=GameObject.Find("Cannon").transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(state == gameState.splash){
	 		Time.timeScale = 0.0F;
	 		if(Input.GetMouseButtonDown(0)||Input.GetKeyDown("space")||Input.GetMouseButtonDown(2)){
	 			state=gameState.build;
	 		}
		}
		else if(state == gameState.build){
			//resets the cannon rotation when in build mode
			GameObject.Find("Cannon").transform.rotation=cannonRotation;
			Time.timeScale = 0.0F;
			if(Input.GetMouseButtonDown(0)){
	 			//here is where the placement code will go
				/*This can be used to figure out what object is being clicked or where on screen the mouse clicked,
				 * it might be possible to use this to place blocks
				 * Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    			RaycastHit hit;
    			if (Physics.Raycast(ray, out hit)){
      				// the object identified by hit.transform was clicked
      				// do whatever you want
    			}*/
				
	 		}	
			/* this can go away once there is a way to switch to attack mode aft0er block placement*/
			if(Input.GetMouseButtonDown(0)||Input.GetKeyDown("space")||Input.GetMouseButtonDown(2)){
				state=gameState.attack;//this is just to advance the gamestate until we have the build functionality down
				//when we are ready to change to attack state we set the cannon up for the new round
				//var cannonBall = GetComponent<CannonBall.cs>();
				shotAngle=45F;
				shotPower=1250F*RB.mass;
				//GameObject ball = GameObject.Find("cannonBall");//.rigidbody;//.AddForce(new Vector3(Mathf.Sin(shotAngle)*shotPower,Mathf.Cos(shotAngle)*shotPower,0));//.GetComponent<CannonBall>();//.doSomething(10F);
				//Rigidbody ballRigidbody = ball.rigidbody;//transform.rigidbody;
				RB.position=new Vector3(-42F+(Mathf.Sin(shotAngle)*2.5F),4.5F+(Mathf.Cos(shotAngle)*2.5F),0);
				RB.AddForce(new Vector3(Mathf.Sin(shotAngle)*shotPower,Mathf.Cos(shotAngle)*shotPower,0));
				shotTime=Time.time;
				//BallControl cannonBall = ball.GetComponent<BallControl>();//GetComponent<CannonBall>();
				//MyCam myCam = target.GetComponent<MyCam>();
        		//otherScript.DoSomething(10F);
				GameObject.Find("Cannon").transform.Rotate(new Vector3(0,0,shotAngle));//.position+=Vector3(0,0,0);//.rigidbody.AddTorque(Vector3(0,10,0));				
			}
		}
		else if(state == gameState.attack){

     		//.RotateAround(Vector3(-42,4.3,0),10);
			Time.timeScale = 1.0F;
			if(Time.time > shotTime+Time.deltaTime*5 && RB.velocity.magnitude<1){
				shotWait=shotWait+Time.deltaTime;
				if(shotWait>5){
					round++;
	 			
					RB.position=new Vector3(-42F,4.5F,0);
					RB.velocity = new Vector3(0,0,0);//zero;
					state=gameState.build;
				}
			}
			else{
				shotWait = 0;
			}
			/*if(Input.GetMouseButtonDown(0)||Input.GetKeyDown("space")||Input.GetMouseButtonDown(2)){
				round++;
	 			state=gameState.build;
				
	 		}*/	
		}
		else if(state ==gameState.win){
			Time.timeScale = 0.0F;
		}
		if(round>maxRounds){
			state=gameState.win;	
		}
	}
	void OnGUI(){
		if(state == gameState.splash){
			GUI.Label (new Rect (60, 60, 100, 20), "Opening spash screen");
		}
		else if(state == gameState.build){
			GUI.Label (new Rect (60, 60, 100, 20), "Build state " + round);
		}
		else if(state == gameState.attack){
			GUI.Label (new Rect (60, 60, 100, 20), "Attack state " + round);
			
			GUI.Label (new Rect (90, 90, 100, 20),GameObject.Find("Cannon").transform.rotation.z + " ");
		}
		else if(state == gameState.win){
			GUI.Label (new Rect (60, 60, 100, 200), "You made it to the end of the rounds, you won!");
		}
	}
}
