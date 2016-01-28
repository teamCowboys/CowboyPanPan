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
        cursor = Resources.Load<Sprite>("Graph/cursor/Cursor1");
        base.Init();
    }

    public override void GiveTo(Player player)
    {
        if (player.currentWeapon.type == EnumerationGun.GunType.GUN)
            return;
        Destroy(player.currentWeapon);
        AbstractWeapon nextGun = player.gameObject.AddComponent<Gun>();
        AttachTo(player.gameObject,typeof(Gun));
        if (gameObject.tag != "Player")
            Destroy(gameObject);
    }

}
