﻿using UnityEngine;
using System.Collections;

public class MeteoScript : MonoBehaviour {

	GameObject cloud1;
	GameObject cloud2;
	GameObject cloudT;

	GameObject cloud;
	GameObject papaCloud;

	public float delay;
	float timerCloud;

	int nbCloud;

	// Use this for initialization
	void Start () {
		papaCloud = new GameObject ();
		papaCloud.name = "PapaCloud";
		cloud1 = Resources.Load ("Prefab/Cloud") as GameObject;
		cloud2 = Resources.Load ("Prefab/CloudBis") as GameObject;
		cloudT = Resources.Load ("Prefab/CloudT") as GameObject;
		timerCloud = delay;
		nbCloud = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timerCloud += Time.deltaTime;

		if (timerCloud >= delay)
		{
			int rnd = Random.Range(0,11);
			if (rnd < 5){cloud = Instantiate(cloud1);}
			else if (rnd > 5 && rnd < 10){cloud = Instantiate(cloud2);}
			else{cloud = Instantiate(cloudT);}
			cloud.transform.parent = papaCloud.transform;
			cloud.name = "Cloud-" + nbCloud;
			nbCloud++;
			timerCloud = 0;
		}
	}
}
