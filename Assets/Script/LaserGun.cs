using UnityEngine;
using System.Collections;

public class LaserGun : AbstractWeapon {
	
    public override void GiveTo(Player player)
    {
        AttachTo(player.gameObject,typeof(LaserGun));
    }
}
