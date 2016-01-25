using UnityEngine;
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
    public Sprite cursor;



    public override void Init()
    {
        base.Init();
        chargeurCurrent = chargeurMax;
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

    public void Shoot()
    {
        Debug.Log("shoot");
        if (reloading)
            return;
        chargeurCurrent--;
        Ray ray = Camera.main.ScreenPointToRay(GetComponent<PlayerAim>().getCursorPosition());
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            IDestroyable component = hit.collider.GetComponent(typeof(IDestroyable)) as IDestroyable;
            if (component!= null)
            {
                component.applyDamage(damage);
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
        Player player = obj.GetComponent<Player>();
        if(player.currentWeapon)
            Destroy(player.currentWeapon);
        if (!obj.GetComponent(type))
            player.currentWeapon = (AbstractWeapon)obj.AddComponent(type);
        else
            player.currentWeapon = (AbstractWeapon)obj.GetComponent(type);
        player.currentWeapon.SetPicked(true);
        obj.GetComponent<PlayerAim>().cursor.sprite = cursor;
    }



}
