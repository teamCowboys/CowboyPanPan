using UnityEngine;
using System.Collections;
using System;

public abstract class AbstractWeapon : AbstractPickable {

    public EnumerationGun.GunType type;
    public float maxAmmo = 100.0f;
    public float chargeurMax = 10.0f;
    public float chargeurCurrent = 0.0f;
    public float damage = 10.0f;
    public float fireRate = 0.2f;
    public float reloadingTime = 1.0f;
    private bool reloading = false;
    public Sprite cursor;



    public override void Init()
    {
        base.Init();
        chargeurCurrent = chargeurMax;
    }

    public IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadingTime);
        chargeurCurrent = maxAmmo>chargeurMax?chargeurMax:maxAmmo%chargeurMax;
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
