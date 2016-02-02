using UnityEngine;
using System.Collections;

public class Sniper : AbstractWeapon
{
    public override void Init()
    {
        type = EnumerationGun.GunType.SNIPER;
        maxAmmo = 30.0f;
        chargeurMax = 10.0f;
        chargeurCurrent = 0.0f;
        damage = 2.0f;
        fireRate = 0.1f;
        reloadingTime = 1.0f;
        cursor = Resources.Load<Sprite>("Graph/cursor/Cursor4");
        sound = Resources.Load<AudioClip>("Sound/Sniper");
        base.Init();
    }

    public override void GiveTo(Player player)
    {
        if (player.currentWeapon.type == EnumerationGun.GunType.SNIPER)
            return;
        Destroy(player.currentWeapon);
        AbstractWeapon nextGun = player.gameObject.AddComponent<Sniper>();
        AttachTo(player.gameObject, typeof(Sniper));
        if (gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
