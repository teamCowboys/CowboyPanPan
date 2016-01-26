using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDestroyable{

    [Header("Weapon")]
    public AbstractWeapon currentWeapon;

    [Header("Stats")]
    public int playerId;
    public float healthPoint;
    public float maxHealthPoint;

    public float lastShot;
    public float smokeDuration = 1.0f;
    public TrailRenderer canonSmoke;

    Animator anim;
    bool invincible = false;

    void Awake()
    {
        Gun gun = gameObject.AddComponent<Gun>();
        gun.AttachTo(gameObject,typeof(Gun));
        healthPoint = maxHealthPoint;
        anim = GetComponentInChildren<Animator>();
    }

    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Fire"+playerId))
        {
            currentWeapon.Shoot(playerId);
            lastShot = Time.time;
        }
        if (Time.time > lastShot + smokeDuration)
            canonSmoke.enabled  = false;
        else
            canonSmoke.enabled = true;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            //Debug.Log("mouse position " + Input.mousePosition+" Cursor position "+ GetComponent<PlayerAim>().getCursorPosition());
            //if (Physics.Raycast(Camera.main.ScreenPointToRay(GetComponent<PlayerAim>().getCursorPosition()), out hitInfo))
            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
               
                IDestroyable test = hitInfo.collider.GetComponent(typeof(IDestroyable)) as IDestroyable;
                test.applyDamage(5, playerId);
            }
        }
    }

    public void ChangeGun(EnumerationGun.GunType typeOfGun)
    {
        System.Type type = null;
        switch(typeOfGun)
        {
            case EnumerationGun.GunType.GUN:
                type = typeof(Gun);
                break;
            case EnumerationGun.GunType.LASERGUN:
                type = typeof(LaserGun);
                break;
            /*case EnumerationGun.GunType.SHOTGUN:
                weapon = new shotGun();
                break;
            case EnumerationGun.GunType.SNIPER:
                weapon = new Sniper();
                break;*/
        }
        ChangeGun(type);


    }

    public void ChangeGun(System.Type type)
    {
        AbstractWeapon nextGun = (AbstractWeapon)gameObject.AddComponent(type);
        nextGun.AttachTo(gameObject,type);

    }

    public void applyDamage(float damage, int killerID = -1)
    {
        if (!invincible)
        {
            healthPoint -= damage;
            if (healthPoint <= 0)
            {
                Death();
            }
            invincible = true;
            anim.SetTrigger("hit");
            //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).length);
            Invoke("ResetInvincibility", anim.GetCurrentAnimatorStateInfo(0).length);
        }
        
            

    }

    public void Death()
    {
        // do nothing yet
        // use Credit & respawn

        bool isCreditLeft = PlayerManager.Instance.UseCredit(playerId);
        if (!isCreditLeft)
        {
            // Can't Respawn
            this.gameObject.SetActive(false);
        }
        else
        {
            healthPoint = maxHealthPoint;
        }
    }

    void ResetInvincibility()
    {
        invincible = false;
    }
}
