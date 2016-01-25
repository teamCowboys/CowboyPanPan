using UnityEngine;
using System.Collections;
using System;

public abstract class AbstractPickable : MonoBehaviour {
    public GameObject[] players;
    public bool Picked = false;

    [Header("test")]
    public float CollisionDistance = 1f;

    public void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void checkCollisionWithPlayers()
    {
        foreach (GameObject p in players)
        {
            if (Vector3.Distance(transform.position, p.transform.position) < CollisionDistance)
            {
                GiveTo(p.GetComponent<Player>());
                if(gameObject.tag != "Player")
                    Destroy(gameObject);
            }
        }
    }

    public abstract void GiveTo(Player player);

    void Update()
    {
        if (!Picked)
            checkCollisionWithPlayers();
    }
}
