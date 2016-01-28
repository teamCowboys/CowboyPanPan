using UnityEngine;
using System.Collections;

public class Shotgun : AbstractWeapon
{

    public override void Init()
    {
        type = EnumerationGun.GunType.SHOTGUN;
        maxAmmo = 12.0f;
        chargeurMax = 2.0f;
        chargeurCurrent = 0.0f;
        damage = 3.0f;
        fireRate = 0.5f;
        reloadingTime = 1.0f;
        cursor = Resources.Load<Sprite>("Graph/cursor/Cursor7");
        base.Init();
    }

    public override void GiveTo(Player player)
    {
        if (player.currentWeapon.type == EnumerationGun.GunType.SHOTGUN)
            return;
        Destroy(player.currentWeapon);
        AbstractWeapon nextGun = player.gameObject.AddComponent<Shotgun>();
        AttachTo(player.gameObject, typeof(Shotgun));
        if (gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
