using UnityEngine;
using System.Collections;
using System;

public abstract class AbstractPickable : MonoBehaviour,IPickable {
    public GameObject[] players;
    public bool Picked = false;

    [Header("test")]
    public float CollisionDistance = 1f;

    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void checkCollisionWithPlayers()
    {
        foreach (GameObject p in players)
        {
            if (Vector3.Distance(transform.position, p.transform.position) < CollisionDistance)
                GiveTo(p.GetComponent<Player>());
        }
    }

    public abstract void GiveTo(Player player);

    void Update()
    {
        throw new NotImplementedException();
    }
}
