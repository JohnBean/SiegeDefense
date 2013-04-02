using UnityEngine;
using System.Collections;

public class royalScript : MonoBehaviour {
	public AudioClip pain;
	public AudioClip blood;
	public AudioClip bone;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision target){
		
      	if(target.gameObject.tag != "EditorOnly"){
			GameObject.Find("Main Camera").GetComponent<GameEngine>().lose();
			audio.PlayOneShot(pain, 1F);
			audio.PlayOneShot(bone,1F);
			audio.PlayOneShot(blood,1F);
		}
	}
}
