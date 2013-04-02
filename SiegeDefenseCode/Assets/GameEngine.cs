using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {
	public enum gameState {splash, build, attack, win, lose};
	public gameState state;//states    0-Opening splash screen.    1-Build Mode    2-Attack mode    3-Win state    4-Lose state
	public Texture2D splashScreen;
	public Texture2D winScreen;
	public Texture2D loseScreen;
	public Rigidbody RB;
	bool firstGame;
	public Transform Trans;
	Quaternion cannonRotation;
	int round;
	int maxRounds;
	public float shotAngle;
	public float shotPower;
	float shotTime;
	float shotWait;
	public AudioClip cannonSound;
	public AudioClip winSound;
	int optionBoxStartX;
	int optionBoxStartY;
	int optionBoxLength;
	int optionBoxWidth;
	
	int buttonStartX;
	int buttonStartY;
	int buttonLength;
	int buttonWidth;
	
	int buttonSpacing;
	
	public int maxCost;
	int cost;
	// Use this for initialization
	void Start () {
		state=gameState.splash;
		round = 1;
		maxRounds =7;
		shotAngle= 0;
		shotPower= 0;
		shotWait = 0;//used to make sure the cannonball has been stationary for long enough before advancing
		cannonRotation=GameObject.Find("Cannon").transform.rotation;
		firstGame = true;
		cost = maxCost;
		optionBoxStartX = 10;
		optionBoxStartY = 10;
		optionBoxLength = 550;
		optionBoxWidth = 50;
	
		buttonStartX = 20;
		buttonStartY = 40;
		buttonLength = 90;
		buttonWidth = 20;
	
		buttonSpacing = 20;

	}
	void resetGame(){
		firstGame=false;
		cost=maxCost;
		state=gameState.splash;
		round = 1;
		maxRounds =7;
		shotAngle= 0;
		shotPower= 0;
		shotWait = 0;//used to make sure the cannonball has been stationary for long enough before advancing
		Trans.position =new Vector3(-42F,4.5F,0);
		GameObject.Find("Cannon").transform.rotation=cannonRotation;
		RB.position=new Vector3(-42F,4.5F,0);
		RB.velocity = new Vector3(0,0,0);//zero;
		Trans.rotation=cannonRotation;
		removeAllBlocks ();
		shotAngle = 35 + Random.Range (0,10);
		shotPower = (1200 +  Random.Range(0,300)) *RB.mass;
	}
	
	void removeAllBlocks()
	{
		GameObject[] allBlocks;
		allBlocks = GameObject.FindGameObjectsWithTag("Block");
		
		foreach(GameObject block in allBlocks)
		{
			Destroy (block);
		}
			
		
	}
	
	void resetCost(int newCost)	//Call from game engine?
	{
		cost = newCost;
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
			Trans.position =new Vector3(-42F,4.5F,0);
			GameObject.Find("Cannon").transform.rotation=cannonRotation;
			RB.position=new Vector3(-42F,4.5F,0);
			RB.velocity = new Vector3(0,0,0);//zero;
			Trans.rotation=cannonRotation;
			
			Time.timeScale = 0.0F;
			/* this can go away once there is a way to switch to attack mode aft0er block placement*/
			if(Input.GetKeyDown("space")){
				state=gameState.attack;//this is just to advance the gamestate until we have the build functionality down
				//when we are ready to change to attack state we set the cannon up for the new round
				Trans.position =new Vector3(-42F,4.5F,0); 
				RB.position=new Vector3(-42F,4.5F,0);
				RB.velocity = new Vector3(0,0,0);//zero;
				int randomNumber =  Random.Range(0, 2);
				switch (randomNumber){
   				 	case 0: //low
        				shotAngle = 20 +  Random.Range(0,5);
						shotPower = (1400 +  Random.Range(0,200)) *RB.mass;
        				break;
    				case 1:
						shotAngle = 35 + Random.Range (0,10);
						shotPower = (1200 +  Random.Range(0,300)) *RB.mass;
        				break;
					case 2:
						shotAngle = 55 + Random.Range(0,10);
						shotPower = (900 +  Random.Range(0,300)) *RB.mass;
        				break;
				}
				shotAngle = 90F - shotAngle;
				RB.position=new Vector3(-42F+(Mathf.Sin(shotAngle*Mathf.Deg2Rad)*2.5F),4.5F+(Mathf.Cos(shotAngle*Mathf.Deg2Rad)*2.5F),0);
				RB.AddForce(new Vector3(Mathf.Sin(shotAngle * Mathf.Deg2Rad)*shotPower,Mathf.Cos(shotAngle * Mathf.Deg2Rad)*shotPower,0));
				if(round == 1 && !firstGame){
					RB.AddForce(new Vector3(Mathf.Sin(shotAngle * Mathf.Deg2Rad)*shotPower/1.5F,Mathf.Cos(shotAngle * Mathf.Deg2Rad)*shotPower/2F,0));
				}
				shotTime=Time.time;
				GameObject.Find("Cannon").transform.Rotate(new Vector3(0,0,90-shotAngle));//.position+=Vector3(0,0,0);//.rigidbody.AddTorque(Vector3(0,10,0));				
				audio.PlayOneShot(cannonSound, 1F);
			}
		}
		else if(state == gameState.attack){
     		Time.timeScale = 1.0F;
			if((Time.time > shotTime+Time.deltaTime*5 && RB.velocity.magnitude<1)){
				shotWait=shotWait+Time.deltaTime;
				if(shotWait>3){
					round++;
	 				Trans.position =new Vector3(-42F,4.5F,0); 
					RB.position=new Vector3(-42F,4.5F,0);
					RB.velocity = new Vector3(0,0,0);//zero;
					if(state != gameState.lose){
						state=gameState.build;
						resetCost (maxCost);
					}
				}
			}
			else{
				if(Trans.position.x > 60|| Trans.position.y < -100){
					round++;
	 				Trans.position =new Vector3(-42F,4.5F,0); 
					RB.position=new Vector3(-42F,4.5F,0);
					RB.velocity = new Vector3(0,0,0);//zero;
					state = gameState.build;
					resetCost (maxCost);
				}
				shotWait = 0;
			}
		}
		else if(state ==gameState.win){
			Time.timeScale = 0.0F;
			if(Input.GetMouseButtonDown(0)||Input.GetKeyDown("space")||Input.GetMouseButtonDown(2)){
				resetGame ();	
			}
		}
		else if(state == gameState.lose){
			Time.timeScale = 0.0F;
			if(Input.GetMouseButtonDown(0)||Input.GetKeyDown("space")||Input.GetMouseButtonDown(2)){
				resetGame ();
			}
		}
		if(round>maxRounds  && state != gameState.win){
			state=gameState.win;
			audio.PlayOneShot(winSound, .5F);
		}
	}
	void OnGUI(){
		if(state == gameState.splash){
			GUI.DrawTexture(new Rect(Screen.width/4,0,Screen.width/2,Screen.height),splashScreen);
		}
		else if(state == gameState.build){
			GUI.Label (new Rect ((Screen.width/4)*3, 10, 100, 20), "Round " + round);
			GUI.Label (new Rect ((Screen.width/4)*3, 40, 150, 20), "Space to continue");
			GUI.Box (new Rect(optionBoxStartX, optionBoxStartY, optionBoxLength,optionBoxWidth), "Blocks");
			GUI.Label (new Rect(optionBoxStartX, optionBoxStartY + optionBoxWidth+10, 70,20), "Count: " + cost);
		
		
			if(GUI.Button(new Rect(buttonStartX, buttonStartY,buttonLength,buttonWidth), "Single Block")) {
				//string path = AssetDatabase.GetAssetPath(TBlock);
			
				if(cost - 1 >= 0){
					GameObject.Instantiate (Resources.Load ("BlockTypes/SingleBlock"), new Vector3(0,10,0), transform.rotation);
					cost = cost - 1;
				}
				else{
				}
				//Application.LoadLevel(1);
			}
		
			if(GUI.Button(new Rect(buttonStartX+(buttonSpacing+buttonLength),buttonStartY, buttonLength, buttonWidth), "Square")) {
				if(cost - 4 >=0){	
					GameObject.Instantiate (Resources.Load ("BlockTypes/SquareBlock"));
					cost = cost-4;
				}
				else{
				}
			}
		
			if(GUI.Button(new Rect(buttonStartX+2*(buttonSpacing+buttonLength),buttonStartY, buttonLength, buttonWidth), "Line Piece")) {
			//string path = AssetDatabase.GetAssetPath(TBlock);
				if(cost - 4 >= 0){
					GameObject.Instantiate (Resources.Load ("BlockTypes/LineBlock"));
					cost = cost - 4;
				}
				else{
				}
				//Application.LoadLevel(1);
			}			
			if(GUI.Button(new Rect(buttonStartX+3*(buttonSpacing+buttonLength),buttonStartY, buttonLength, buttonWidth), "Fat T Block")) {
				if(cost - 6 >= 0){
					GameObject.Instantiate (Resources.Load ("BlockTypes/RealDBlock"));
					cost = cost-6;
				}
				else{
				}
			}
			if(GUI.Button(new Rect(buttonStartX+4*(buttonSpacing+buttonLength),buttonStartY, buttonLength, buttonWidth), "Big T Block")) {	
				if(cost - 4 >= 0){
					GameObject.Instantiate (Resources.Load ("BlockTypes/TBlock"));
					cost = cost-4;
				}
				else{
				}
			}
		}
		else if(state == gameState.attack){
			GUI.Label (new Rect ((Screen.width/4)*3, 10, 100, 20), "Round " + round);
		}
		else if(state == gameState.win){
			GUI.DrawTexture(new Rect(Screen.width/4,0,Screen.width/2,Screen.height),winScreen);
		}
		else if(state == gameState.lose){
			GUI.DrawTexture(new Rect(Screen.width/3,Screen.height/4,Screen.width/3,Screen.height/2),loseScreen);
		}
	}
	void OnCollisionEnter(Collision target){
      	if(target.gameObject.tag == "Finish"){
			state = gameState.lose;	
		}
	}
	public void lose(){
		state=gameState.lose;
	}
	public void attack(){
		state=gameState.attack;//this is just to advance the gamestate until we have the build functionality down
		//when we are ready to change to attack state we set the cannon up for the new round
		Trans.position =new Vector3(-42F,4.5F,0); 
		RB.position=new Vector3(-42F,4.5F,0);
		RB.velocity = new Vector3(0,0,0);//zero;
		resetCost (maxCost);
		int randomNumber =  Random.Range(0, 2);
		switch (randomNumber){
 		 	case 0: //low
   				shotAngle = 20 +  Random.Range(0,5);
				shotPower = (1400 +  Random.Range(0,200)) *RB.mass;
   				break;
 			case 1:
				shotAngle = 35 + Random.Range (0,10);
				shotPower = (1200 +  Random.Range(0,300)) *RB.mass;
   				break;
			case 2:
				shotAngle = 55 + Random.Range(0,10);
				shotPower = (1000 +  Random.Range(0,300)) *RB.mass;
   				break;
		}
		shotAngle = 90F - shotAngle;
		RB.position=new Vector3(-42F+(Mathf.Sin(shotAngle*Mathf.Deg2Rad)*2.5F),4.5F+(Mathf.Cos(shotAngle*Mathf.Deg2Rad)*2.5F),0);
		RB.AddForce(new Vector3(Mathf.Sin(shotAngle * Mathf.Deg2Rad)*shotPower,Mathf.Cos(shotAngle * Mathf.Deg2Rad)*shotPower,0));
		shotTime=Time.time;
		GameObject.Find("Cannon").transform.Rotate(new Vector3(0,0,90-shotAngle));
	}
}
