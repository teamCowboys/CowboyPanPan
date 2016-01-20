using UnityEngine;
using System.Collections;

enum S
    {
    SHOOT = 0,
    HIDE = 1,
    DEAD = 2
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
        if (canHide)
            canMove = false;
        isHiding = false;
        currentState = state[(int)S.SHOOT];
        currentState.Init(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        currentState.Run();

        if (Input.GetKeyDown(KeyCode.A))
        {
            GetDamage();
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
        ChangeState(S.SHOOT);
    }

    void ChangeState(S index)
    {
        currentState.Stop();
        currentState = state[(int)index];
        currentState.Init(this.gameObject);
    }
}
