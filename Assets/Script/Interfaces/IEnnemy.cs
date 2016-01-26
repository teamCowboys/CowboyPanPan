using UnityEngine;
using System.Collections;

public abstract class IEnnemy : MonoBehaviour, IDestroyable
{

    public abstract void applyDamage(float dmg, int killerID = -1);

    public abstract void Death();
}
