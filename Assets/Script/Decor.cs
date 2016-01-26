﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Decor : MonoBehaviour,IDestroyable {


	public float maxHealthPoints;
    public float healthPoints;
	public GameObject loot;
	public bool notInvolved;
	float T1;
	float T2;
	float T3;
	Sprite ST1;
	Sprite ST2;
	Sprite ST3;
	string phase;
	GameObject child;
	float valueShake;
	float timeShake;
	Vector3 startPos;
	GameObject smoke;

    void Awake()
    {
        //gameObject.layer = LayerMask.NameToLayer("Destroyable");
        GetComponent<Rigidbody>().useGravity = false;
    }

	void Start () 
	{
		healthPoints = maxHealthPoints;
		T1 = maxHealthPoints / 4;
		T2 = T1 * 2;
		T3 = T1 * 3;

		child = new GameObject ();
		child.name = "Crack-"+this.gameObject.name;
		child.AddComponent<SpriteRenderer> ();
		child.GetComponent<SpriteRenderer> ().sortingOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
		child.transform.parent = this.gameObject.transform;
		child.transform.localPosition = new Vector3(0,0,0);
		Vector3 boxScale = this.gameObject.GetComponent<BoxCollider> ().size;
		boxScale.x -= 0.4f;
		boxScale.y -= 0.4f;
		child.transform.localScale = boxScale;

		GameObject prefab;
		prefab = Resources.Load ("Crack/Crack3") as GameObject;
		ST1 = prefab.GetComponent<SpriteRenderer> ().sprite;
		prefab = Resources.Load ("Crack/Crack2") as GameObject;
		ST2 = prefab.GetComponent<SpriteRenderer> ().sprite;
		prefab = Resources.Load ("Crack/Crack1") as GameObject;
		ST3 = prefab.GetComponent<SpriteRenderer> ().sprite;

		smoke = Resources.Load ("Smoke/WhiteSmoke") as GameObject;
		phase = "T4";

		startPos = this.gameObject.transform.position;
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.P)) {applyDamage(0.5f);}

		valueShake = Mathf.Sin (Time.time * 50.0f);
		timeShake -= Time.deltaTime;

		if (!notInvolved) 
		{
			if (timeShake > 0) {shake ();} 
			else{this.gameObject.transform.position = startPos;}
		}


		if (healthPoints <= T3 && phase == "T4")
		{
			child.GetComponent<SpriteRenderer>().sprite = ST3;
			phase = "T3";
		}
		else if (healthPoints <= T2 && phase == "T3")
		{
			child.GetComponent<SpriteRenderer>().sprite = ST2;
			Vector3 pos = startPos;
			pos.x = startPos.x;
			pos.y = startPos.y/2;
			if (pos.y < 0){pos.y *= -1;}
			pos.z = startPos.z;
			GameObject smokeChild = Instantiate(smoke) as GameObject;
			this.gameObject.transform.position = Vector3.zero;
			//smokeChild.transform.position = Vector3.zero;
			smokeChild.transform.parent = this.gameObject.transform;
			smokeChild.name = "Smoke-" + this.gameObject.name;
			this.gameObject.transform.position = startPos;
			phase = "T2";
		}
		else if (healthPoints <= T1 && phase == "T2")
		{
			child.GetComponent<SpriteRenderer>().sprite = ST1;
			phase = "T1";
		}
	}

    public void applyDamage(float damage)
    {
        healthPoints -= damage;
		timeShake = 0.5f;
		if (healthPoints <= 0){Death();}
    }

	public void shake()
	{
		Vector3 shaking = startPos;
		shaking.x += valueShake/50;
		this.gameObject.transform.localPosition = shaking;
	}

    public void Death()
    {
		if (loot){Instantiate (loot, this.gameObject.transform.position, Quaternion.identity);}
        Destroy(gameObject);
    }
}
