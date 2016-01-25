using UnityEngine;
using System.Collections;

enum S
    {
    SHOOT = 0,
    HIDE = 1,
    DEAD = 2,
    MOVE = 3,
};

public class Enemy : MonoBehaviour {
    public IEnemyState currentState;
    public IEnemyState[] state;
    public bool canHide = false;
    public bool canMove = false;
    public int lifePoints= 2;                               // shoot needed before killing him
    float shootDuration;
    bool isHiding;
    
    
    // Use this for initialization
    void Start () {
        if (canMove)
            canHide = false;
        isHiding = false;
        currentState = state[(int)S.SHOOT];
        fight();
    }
	
	// Update is called once per frame
	void Update () {
        currentState.Run();

        if (Input.GetKeyDown(KeyCode.A))
        {
            GetDamage();
        }
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

    void GetDamage()
    {
        lifePoints--;
        if (lifePoints <= 0)
        {
            // Die
            Debug.Log("Die");
            ChangeState(S.DEAD);
        }
        else
        {
            Hide();
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
}
