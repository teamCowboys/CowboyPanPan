using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Decor : MonoBehaviour,IDestroyable {


    public float healthPoints;
    public float maxHealthPoints;

    void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Destroyable");
        GetComponent<Rigidbody>().useGravity = false;
    }
	void Start () {
        healthPoints = maxHealthPoints;
	}
	
	void Update () {
	
	}

    public void applyDamage(float damage)
    {
        healthPoints -= damage;
        if (healthPoints < 0)
            Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
