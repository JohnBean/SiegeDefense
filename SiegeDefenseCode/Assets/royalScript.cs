using UnityEngine;
using System.Collections;

public class royalScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision target){
		
      	if(target.gameObject.tag != "EditorOnly"){
			GameObject.Find("Main Camera").GetComponent<GameEngine>().lose();// = gameState.lose;	
			print ("something was hit in the kings script");
		}
	}
}
