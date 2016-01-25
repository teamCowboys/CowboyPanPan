using UnityEngine;
using System.Collections;

enum S
    {
    SHOOT = 0,
    HIDE = 1,
    DEAD = 2,
    MOVE = 3,
};

public class Enemy : IEnnemy {

    public int value = 100;
    public IEnemyState currentState;
    public IEnemyState[] state;
    public bool canHide = false;
    public bool canMove = false;
    public float lifePoints= 2;                               // shoot needed before killing him
    float shootDuration;
    bool isHiding;
     
    // Use this for initialization
    void Start () {
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponentInChildren<SpriteRenderer>().enabled = true;
        if (canMove)
            canHide = false;
        isHiding = false;
        currentState = state[(int)S.SHOOT];
        fight();
    }

    public void Spawn(int Life =2, int scoreValue=100)
    {
        value = scoreValue;
        lifePoints = Life;
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        currentState.Run();

        
	}

    void fight()
    {
        if (canMove)
        {
            ChangeState(S.MOVE);
        }
        else
        {
            ChangeState(S.SHOOT);
        }
    }

    void Hide()
    {
        if (canHide && !isHiding)
        {
            StartCoroutine(Hidingtime(Random.Range(1, 5)));
        }            
    }

    IEnumerator Hidingtime(float time)
    {
        isHiding = true;
        ChangeState(S.HIDE);
        yield return new WaitForSeconds(time);
        isHiding = false;
        fight();
    }

    void ChangeState(S index)
    {
        currentState.Stop();
        currentState = state[(int)index];
        currentState.Init(this.gameObject);
    }

    public override void applyDamage(float dmg, int killerID)
    {
        if (lifePoints <= dmg)
        {
            lifePoints = 0;

            PlayerManager.Instance.applyScoring(killerID, value);
            Death();
        }
        else
        {
            lifePoints -= dmg;
            Hide();
        }
    }

    public override void Death()
    {
        ChangeState(S.DEAD);

        // GROSSE EXPLOSION
        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
    
}
