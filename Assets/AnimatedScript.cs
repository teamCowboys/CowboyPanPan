using UnityEngine;
using System.Collections;

public class AnimatedScript : MonoBehaviour {

	Texture[] frames;
	int framesPerSecond = 10;
	int index;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//index = (int)(Time.time * framesPerSecond) % frames.Length;
		//this.gameObject.GetComponent<Renderer>().material.mainTexture = frames[index];
	}
}