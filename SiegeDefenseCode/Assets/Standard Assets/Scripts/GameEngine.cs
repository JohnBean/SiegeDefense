using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {
	enum gameState {splash, build, attack, win, lose};
	gameState state;//states    0-Opening splash screen.    1-Build Mode    2-Attack mode    3-Win state    4-Lose state
	Texture splashScreen;
	int round;
	int maxRounds;
	// Use this for initialization
	void Start () {
		state=gameState.splash;
		round = 1;
		maxRounds =7;
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
			/* this can go away once there is a way to switch to attack mode after block placement*/
			if(Input.GetMouseButtonDown(0)||Input.GetKeyDown("space")||Input.GetMouseButtonDown(2)){
				state=gameState.attack;//this is just to advance the gamestate until we have the build functionality down
			}
		}
		else if(state == gameState.attack){
			Time.timeScale = 1.0F;
			if(Input.GetMouseButtonDown(0)||Input.GetKeyDown("space")||Input.GetMouseButtonDown(2)){
				round++;
	 			state=gameState.build;
	 		}	
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
		}
		else if(state == gameState.win){
			GUI.Label (new Rect (60, 60, 100, 200), "You made it to the end of the rounds, you won!");
		}
	}
}
