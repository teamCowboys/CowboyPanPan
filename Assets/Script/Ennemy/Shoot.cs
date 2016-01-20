using UnityEngine;
using System.Collections;

public class Shoot : IEnemyState
{
    public GameObject Ammo;
    Transform badboy;
    Vector3 direction;
    public float shootRate = 0.5f;
    float lastShoot;

    public override void Run()
    {
        lastShoot += Time.deltaTime;
        if (lastShoot > shootRate)
        {
            Debug.Log("SHOOT");

            PullTheTrigger();
        }
        

    }

    public override void Stop()
    {
    }

    public override void Init(GameObject obj)
    {
        badboy = obj.GetComponent<Transform>();
        lastShoot = 0;
    }

    void PullTheTrigger()
    {
        float rnd = Random.Range(-5,5);
        direction = new Vector3(badboy.position.x+rnd, -5, 0);
        Instantiate(Ammo, badboy.position, badboy.rotation);
        Ammo.GetComponent<Fire>().Init(direction);
        lastShoot = 0;
    }

    
}
