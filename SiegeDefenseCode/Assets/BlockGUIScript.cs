using UnityEngine;
using System.Collections;

public class BlockGUIScript : MonoBehaviour {
	
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
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.Box (new Rect(optionBoxStartX, optionBoxStartY, optionBoxLength,optionBoxWidth), "Blocks");
		GUI.Label (new Rect(optionBoxStartX, optionBoxStartY + optionBoxWidth+10, 70,20), "Count: " + cost);
		
		
		if(GUI.Button(new Rect(buttonStartX, buttonStartY,buttonLength,buttonWidth), "Single Block")) {
		//string path = AssetDatabase.GetAssetPath(TBlock);
			
			if(cost - 1 >= 0)
			{
				GameObject.Instantiate (Resources.Load ("BlockTypes/SingleBlock"), new Vector3(0,10,0), transform.rotation);
				cost = cost - 1;
			}
			else
			{
			}
		//Application.LoadLevel(1);
		}
		
		if(GUI.Button(new Rect(buttonStartX+(buttonSpacing+buttonLength),buttonStartY, buttonLength, buttonWidth), "Square")) {
			if(cost - 4 >=0)
			{
				GameObject.Instantiate (Resources.Load ("BlockTypes/SquareBlock"));
				cost = cost-4;
			}
			else
			{
			}
		}
		
		if(GUI.Button(new Rect(buttonStartX+2*(buttonSpacing+buttonLength),buttonStartY, buttonLength, buttonWidth), "Line Piece")) {
			//string path = AssetDatabase.GetAssetPath(TBlock);
			
			if(cost - 4 >= 0)
			{
				GameObject.Instantiate (Resources.Load ("BlockTypes/LineBlock"));
				cost = cost - 4;
			}
			else
			{
			}
			//Application.LoadLevel(1);
		}
		
		
		
		if(GUI.Button(new Rect(buttonStartX+3*(buttonSpacing+buttonLength),buttonStartY, buttonLength, buttonWidth), "Fat T Block")) {
			
			if(cost - 6 >= 0)
			{
				GameObject.Instantiate (Resources.Load ("BlockTypes/RealDBlock"));
				cost = cost-6;
			}
			else
			{
			}
		}
		
		if(GUI.Button(new Rect(buttonStartX+4*(buttonSpacing+buttonLength),buttonStartY, buttonLength, buttonWidth), "Big T Block")) {
			
			if(cost - 4 >= 0)
			{
				GameObject.Instantiate (Resources.Load ("BlockTypes/TBlock"));
				cost = cost-4;
			}
			else
			{
			}
		}
		if(Input.GetKeyDown("space")){
			GameObject.Find("Main Camera").GetComponent<GameEngine>().attack();
			resetCost (25);
		}
		/*if(GUI.Button(new Rect(buttonStartX+5*(buttonSpacing+buttonLength),buttonStartY, buttonLength, buttonWidth), "D Block")) {
			if(cost - 5 >= 0)
			{
				GameObject.Instantiate (Resources.Load ("BlockTypes/DBlock"));
				cost = cost-5;
			}
			else
			{
			}
		}*/
		
	}
	
	void resetCost(int newCost)	//Call from game engine?
	{
		cost = newCost;
	}
	
	/*void OnGUI()
	{
		GUI.Box(new Rect(10,10,450,50), "Blocks");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Line Piece")) {
			//string path = AssetDatabase.GetAssetPath(TBlock);
			GameObject.Instantiate (Resources.Load ("BlockTypes/LineBlock"));
			//Application.LoadLevel(1);
		}

		// Make the second button
		if(GUI.Button(new Rect(110,40,80,20), "D Block")) {
			//Application.LoadLevel(2);
			GameObject.Instantiate (Resources.Load ("BlockTypes/DBlock"));
		}
		
		if(GUI.Button(new Rect(200,40,80,20), "T Block")) {
			//Application.LoadLevel(2);
			GameObject.Instantiate (Resources.Load ("BlockTypes/TBlock"));
		}
		
		if(GUI.Button(new Rect(290,40,80,20), "Big T Block")) {
			//Application.LoadLevel(2);
			GameObject.Instantiate (Resources.Load ("BlockTypes/RealDBlock"));
		}
		
		
		if(GUI.Button(new Rect(380,40,80,20), "Square")) {
			//Application.LoadLevel(2);
			GameObject.Instantiate (Resources.Load ("BlockTypes/SquareBlock"));
		}
		
		}
		*/
	
}
