﻿using UnityEngine;
using System.Collections;

public class Gun : AbstractWeapon {
    


    public override void GiveTo(Player player)
    {
        AttachTo(player.gameObject,typeof(Gun));
    }

}
