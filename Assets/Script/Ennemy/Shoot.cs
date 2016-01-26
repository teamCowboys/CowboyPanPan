using UnityEngine;
using System.Collections;

public class Shoot : IEnemyState
{
    public GameObject Ammo;
    Transform badboy;
    GameObject[] target;
    Vector3 directionShoot;
    Vector3 directionMove;
    public float shootRate = 0.5f;
    float lastShoot;
    bool canMove ;

    public override void Run()
    {
        lastShoot += Time.deltaTime;
        if (lastShoot > shootRate)
        {
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
        target =  GameObject.FindGameObjectsWithTag("Player");
    }

    void PullTheTrigger()
    {
        GameObject go;
        int rnd = Mathf.FloorToInt(Random.Range(0, target.Length-1));
        //this.directionShoot = new Vector3(this.badboy.position.x + rnd, -5, 0);
        go = (GameObject) Instantiate(Ammo, this.badboy.position, this.badboy.rotation);
        Vector3 dir = target[rnd].transform.position - this.badboy.position;
        dir.Normalize();

        go.GetComponent<Fire>().Init(dir, badboy.GetComponent<Ennemy>().damage);
        lastShoot = 0;

    }


}
