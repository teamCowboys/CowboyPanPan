using UnityEngine;
using System.Collections;

public class Gun : AbstractWeapon {
    
    public override void Init()
    {
        type = EnumerationGun.GunType.GUN;
        maxAmmo = 100.0f;
        chargeurMax = 10.0f;
        chargeurCurrent = 0.0f;
        damage = 1.0f;
        fireRate = 0.2f;
        reloadingTime = 1.0f;
        base.Init();
    }

    public override void GiveTo(Player player)
    {
        AttachTo(player.gameObject,typeof(Gun));
    }

}
