using UnityEngine;
using System.Collections;

public class Hide : IEnemyState
{
    Transform badboy;
    public float Height = 0.2f;
    public override void Run()
    {

    }

    public override void Stop()
    {
        badboy.position = new Vector3(badboy.position.x, badboy.position.y + Height, -0.1f);
        Debug.Log("SORT");
    }

    public override void Init(GameObject obj)
    {
        badboy = obj.GetComponent<Transform>();
        badboy.position = new Vector3(badboy.position.x, badboy.position.y - Height, 1);
        Debug.Log("SE CACHE");
    }
}
