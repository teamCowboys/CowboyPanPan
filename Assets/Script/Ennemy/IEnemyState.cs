using UnityEngine;
using System.Collections;

public abstract class IEnemyState : MonoBehaviour {

    public abstract void Run();

    public abstract void Stop();

    public abstract void Init(GameObject obj);
}
