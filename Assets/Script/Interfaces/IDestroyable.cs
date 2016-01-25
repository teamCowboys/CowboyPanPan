using UnityEngine;
using System.Collections;

public interface IDestroyable
{

    void applyDamage(float damage, int killerID = -1);
    void Death();
}
