﻿using UnityEngine;
using System.Collections;
using System;

public abstract class AbstractWeapon : AbstractPickable {

    public EnumerationGun.GunType type;
    public float maxAmmo;
    public float chargeurMax;
    public float chargeurCurrent;
    public float damage;
    public float fireRate;
    public float reloadingTime;
    private bool reloading = false;
    public bool isAuto = false;
    public Sprite cursor;
    public GameObject bulletHole;
    public AudioClip sound;



    public override void Init()
    {
        base.Init();
        chargeurCurrent = chargeurMax;
        bulletHole = Resources.Load("Graph/Weapons/bulletholeGameobject") as GameObject;
        maxAmmo -= chargeurCurrent;
    }

    public IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadingTime);
        chargeurCurrent = maxAmmo>chargeurMax?chargeurMax:maxAmmo;
        maxAmmo -= chargeurCurrent;
        reloading = false;
    }

    public void Shoot(int ShooterID)
    {
        if (reloading)
            return;
        chargeurCurrent--;
        if(!isAuto)
            GetComponent<AudioSource>().PlayOneShot(sound);
        Ray ray = Camera.main.ScreenPointToRay(GetComponent<PlayerAim>().getCursorPosition());
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            IDestroyable component = hit.collider.GetComponent(typeof(IDestroyable)) as IDestroyable;
            if (component!= null)
            {
                component.applyDamage(damage, ShooterID);
                if(hit.collider.tag != "enemy")
                {
                    GameObject inst = Instantiate(bulletHole, hit.point, bulletHole.transform.rotation) as GameObject;
                    inst.transform.parent = hit.collider.transform;

                }
                //component.applyDamage(damage);
            }
        }

        if (chargeurCurrent == 0)
        {
            if(maxAmmo == 0)
                GetComponent<Player>().ChangeGun(EnumerationGun.GunType.GUN);
            else
                StartCoroutine(Reload());
        }
        
    }

    public void SetPicked(bool value)
    {
        Picked = value;
    }



    public void AttachTo(GameObject obj,System.Type type)
    {
        obj.GetComponent<AudioSource>().Stop();
        Player player = obj.GetComponent<Player>();
        player.currentWeapon = (AbstractWeapon)obj.GetComponent(type);
        player.currentWeapon.SetPicked(true);
        obj.GetComponent<PlayerAim>().cursor.sprite = cursor;
        if(player.playerId == 1)
            obj.GetComponent<PlayerAim>().cursor.color = Color.red;
        else
            obj.GetComponent<PlayerAim>().cursor.color = Color.blue;

    }



}
