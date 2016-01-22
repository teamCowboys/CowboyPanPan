using UnityEngine;
using System.Collections;

public class Gun : IWeapon {
    
    public Sprite cursor;

    public override void Init()
    {
        base.Init();

    }


    public override void AttachTo(GameObject obj)
    {
        Player player = obj.GetComponent<Player>();
        if (!obj.GetComponent<Gun>())
            player.currentWeapon = obj.AddComponent<Gun>();
        else
            player.currentWeapon = obj.GetComponent<Gun>();
        player.currentWeapon.Picked = true;
        player = obj.GetComponent<Player>();
        obj.GetComponent<PlayerAim>().cursor.sprite = cursor;
    }

    public override void GiveTo(Player player)
    {
        AttachTo(player.gameObject);
        Destroy(gameObject);
    }

    void Update()
    {
        if(!Picked)
            checkCollisionWithPlayers();
    }
}
