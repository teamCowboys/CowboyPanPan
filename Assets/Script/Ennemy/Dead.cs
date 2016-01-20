using UnityEngine;
using System.Collections;

public class Dead : IEnemyState
{
    public int playerKillerID = 0;
    public int scoreValue= 100;
    public override void Run()
    {

    }

    public override void Stop()
    {
    }

    public override void Init(GameObject obj)
    {
        PlayerManager.Instance.addScore(playerKillerID, scoreValue);
    }
}
