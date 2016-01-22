using UnityEngine;
using System.Collections;
using System;

public abstract class AbstractWeapon : AbstractPickable,IWeapon {
    public void AttachTo(GameObject obj)
    {
        throw new NotImplementedException();
    }

    public void Awake()
    {
        throw new NotImplementedException();
    }

    public void checkCollisionWithPlayers()
    {
        throw new NotImplementedException();
    }

    public void GiveTo(Player player)
    {
        throw new NotImplementedException();
    }

    public void Init()
    {
        base.Init();
        chargeurCurrent = chargeurMax;
    }

    public IEnumerator Reload()
    {
        throw new NotImplementedException();
    }

    public void Shoot()
    {
        Debug.Log("shoot");
        if (reloading)
            return;
        Ray ray = Camera.main.ScreenPointToRay(GetComponent<PlayerAim>().getCursorPosition());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            IDestroyable component = hit.collider.GetComponent(typeof(IDestroyable)) as IDestroyable;
            if (component != null)
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

    // Use this for initialization
    void Start () {
	
	}

    void IPickable.Update()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
