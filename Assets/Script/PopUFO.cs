using UnityEngine;
using System.Collections;

public class PopUFO : MonoBehaviour {

	float delay;
	float rand;
	float timerPop;
	GameObject UFO;
	bool hasPop;
	// Use this for initialization
	void Start () {
		delay = 50;
		rand = 50;
		hasPop = false;
		delay += Random.Range (0, rand);
		UFO = Resources.Load ("Prefab/UFO") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasPop) 
		{
			timerPop += Time.deltaTime;
			if (timerPop >= delay){Instantiate(UFO); hasPop = true;}
		}
	}
}
