using UnityEngine;
using System.Collections;

public abstract class IEnnemy : MonoBehaviour, IDestroyable
{

    public abstract void applyDamage(float dmg);

    public abstract void Death();
}
