using UnityEngine;
using System.Collections;

public class Shoot : IEnemyState
{
    public GameObject Ammo;
    Transform badboy;
    Vector3 directionShoot;
    Vector3 directionMove;
    public float shootRate = 0.5f;
    public float speed = 1;
    float lastShoot;
    bool canMove ;
    public override void Run()
    {
        lastShoot += Time.deltaTime;
        if (lastShoot > shootRate)
        {
            Debug.Log("SHOOT");

            PullTheTrigger();
        }

        if (canMove)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 pos = badboy.position;
        directionMove = new Vector3(1,0,0);
        this.transform.position = new Vector3(pos.x + directionMove.x * speed * Time.deltaTime, pos.y + directionMove.y * speed * Time.deltaTime, pos.z);
    }

    public override void Stop()
    {
    }

    public override void Init(GameObject obj)
    {
        badboy = obj.GetComponent<Transform>();
        lastShoot = 0;
        canMove = obj.GetComponent<Enemy>().canMove;
    }

    void PullTheTrigger()
    {
        float rnd = Random.Range(-5,5);
        directionShoot = new Vector3(badboy.position.x+rnd, -5, 0);
        Instantiate(Ammo, badboy.position, badboy.rotation);
        Ammo.GetComponent<Fire>().Init(directionShoot);
        lastShoot = 0;
        
    }

    
}
