using UnityEngine;
using System.Collections;

public interface IWeapon : IPickable{

    public EnumerationGun.GunType type;
    public float maxAmmo = 100.0f;
    public float chargeurMax = 10.0f;
    public float chargeurCurrent = 0.0f;
    public float damage = 10.0f;
    public float fireRate = 0.2f;
    public float reloadingTime = 1.0f;
    private bool reloading = false;

    public virtual void Init()
    {
        base.Init();
        chargeurCurrent = chargeurMax;
    }

    public void Shoot()
    {
        Debug.Log("shoot");
        if (reloading)
            return;
        Ray ray = Camera.main.ScreenPointToRay(GetComponent<PlayerAim>().getCursorPosition());
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            IDestroyable component = hit.collider.GetComponent(typeof(IDestroyable)) as IDestroyable;
            if (component!= null)
            {
                component.applyDamage(damage);
                chargeurCurrent--;
            }
        }

        if (chargeurCurrent == 0)
        {
            StartCoroutine(Reload());

        }
    }

    public abstract void AttachTo(GameObject obj);

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadingTime);
        chargeurCurrent = 10.0f;
        reloading = false;
    }


}
