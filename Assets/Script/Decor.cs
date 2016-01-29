using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Decor : MonoBehaviour,IDestroyable {

    public int scoreValue = 100;

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
	bool goToDeath;

    void Awake()
    {
        //gameObject.layer = LayerMask.NameToLayer("Destroyable");
        GetComponent<Rigidbody>().useGravity = false;
    }

	void Start () 
	{
		goToDeath = false;
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
			if (!notInvolved)
			{
				GameObject smokeChild = Instantiate(smoke) as GameObject;
				this.gameObject.transform.position = Vector3.zero;
				smokeChild.transform.parent = this.gameObject.transform;
				smokeChild.name = "Smoke-" + this.gameObject.name;
				this.gameObject.transform.position = startPos;
			}
			phase = "T2";
		}
		else if (healthPoints <= T1 && phase == "T2")
		{
			child.GetComponent<SpriteRenderer>().sprite = ST1;
			phase = "T1";
		}

		if (healthPoints <= 0){Death();}
	}

    public void applyDamage(float damage, int killerID=-1)
    {
		if (goToDeath == false) 
		{
			healthPoints -= damage;
			timeShake = 0.5f;
			if (healthPoints <= 0 && notInvolved)
			{
				if (loot) { Instantiate(loot, this.gameObject.transform.position, Quaternion.identity); }
				Destroy(this.gameObject);
			}
			if (healthPoints <= 0)
			{
				if (killerID != -1)
				{
					PlayerManager.Instance.applyScoringDecor(killerID, scoreValue);
				}
				Death();
			}
		}
    }

	public void shake()
	{
		Vector3 shaking = startPos;
		shaking.x += valueShake/50;
		this.gameObject.transform.localPosition = shaking;
	}

    public void Death()
    {
		goToDeath = true;
        GetComponent<Rigidbody>().isKinematic = true;
        if (loot){
            Instantiate (loot, this.gameObject.transform.position, Quaternion.identity);
            loot = null;
        }

		foreach (Transform go in this.gameObject.GetComponentsInChildren<Transform>()) 
		{
			if (go.tag == "bullethole"){Destroy(go.gameObject);}
		}
		this.gameObject.transform.position -= new Vector3 (0,5,0) * Time.deltaTime;
		notInvolved = true;
		this.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = -2;
		child.GetComponent<SpriteRenderer> ().enabled = false;
		Destroy (this.gameObject.GetComponentInChildren<ParticleSystem> ());
		if (this.gameObject.transform.position.y < -10){Destroy(this.gameObject);}
    }
}
