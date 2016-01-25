using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Decor : MonoBehaviour,IDestroyable {


	public float maxHealthPoints;
    public float healthPoints;
	public float T1;
	public float T2;
	public float T3;
	Sprite ST1;
	Sprite ST2;
	Sprite ST3;
	string phase;

    void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Destroyable");
        GetComponent<Rigidbody>().useGravity = false;
    }

	void Start () 
	{
		if (this.transform.childCount < 1 || !this.transform.GetChild(0).GetComponent<SpriteRenderer>())
		{
			GameObject child = new GameObject();
			child.AddComponent<SpriteRenderer>();
			child.name = "CrackSprite";
			child.GetComponent<SpriteRenderer>().sortingOrder = 2;
			child.transform.parent = this.gameObject.transform;
			child.transform.position = new Vector3(0,0,0);
			Debug.Log(child.transform.position);
		}
		healthPoints = maxHealthPoints;
		T1 = maxHealthPoints / 4;
		T2 = T1 * 2;
		T3 = T1 * 3;
		ST1 = Resources.Load ("Graph/Crack/Crack3") as Sprite;
		ST2 = Resources.Load ("Graph/Crack/Crack2") as Sprite;
		ST3 = Resources.Load ("Graph/Crack/Crack1") as Sprite;
		phase = "T4";
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.A)) {applyDamage(0.5f);}


		if (healthPoints <= T3 && phase != "T3"){this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ST3;phase = "T3";}
		else if (healthPoints <= T2 && phase != "T2"){this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ST2;phase = "T2";}
		else if (healthPoints <= T1 && phase != "T1"){this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ST1;phase = "T1";}
	}

    public void applyDamage(float damage)
    {
        healthPoints -= damage;
		if (healthPoints <= 0){Death();}
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
