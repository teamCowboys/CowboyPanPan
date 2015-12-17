using UnityEngine;
using System.Collections;

public abstract class IWeapon : IPickable{

    public EnumerationGun.GunType type;
    public float chargeurMax;
    public float chargeurCurrent;
    public float damage;
    public float fireRate;

    public virtual void Init()
    {
        base.Init();
        chargeurCurrent = chargeurMax;
    }

    public abstract void Shoot();

    public abstract void AttachTo(GameObject obj);


}
