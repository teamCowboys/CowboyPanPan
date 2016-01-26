using UnityEngine;
using System.Collections;

public class LaserGun : AbstractWeapon {


    public override void Init()
    {
        type = EnumerationGun.GunType.LASERGUN;
        maxAmmo = 30.0f;
        chargeurMax = 30.0f;
        chargeurCurrent = 0.0f;
        damage = 50.0f;
        fireRate = 0.1f;
        reloadingTime = 1.0f;
        cursor = Resources.Load<Sprite>("Graph/cursor/Cursor6");
        base.Init();
    }

    public override void GiveTo(Player player)
    {
        Destroy(player.currentWeapon);
        AbstractWeapon nextGun = player.gameObject.AddComponent<LaserGun>();
        AttachTo(player.gameObject,typeof(LaserGun));
        if (gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
