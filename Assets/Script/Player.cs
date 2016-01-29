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

        //if (Input.GetKeyDown(KeyCode.M)){currentWeapon.Shoot();}
        if (Input.GetKeyDown(KeyCode.A))
            ChangeGun(EnumerationGun.GunType.GUN);
        if (Input.GetKeyDown(KeyCode.Z))
            ChangeGun(EnumerationGun.GunType.LASERGUN);
        if (Input.GetKeyDown(KeyCode.E))
            ChangeGun(EnumerationGun.GunType.MINIGUN);
        if (Input.GetKeyDown(KeyCode.R))
            ChangeGun(EnumerationGun.GunType.SHOTGUN);
        if (Input.GetKeyDown(KeyCode.T))
            ChangeGun(EnumerationGun.GunType.SNIPER);
        if (currentWeapon.isAuto)
        {
            if (Input.GetButtonDown("Fire" + playerId) && Time.time > lastShot + currentWeapon.fireRate)
            {
                AudioSource audio = GetComponent<AudioSource>();
                audio.clip = currentWeapon.sound;
                audio.loop = true;
                audio.Play();
            }
            if (Input.GetButton("Fire" + playerId) && Time.time > lastShot+currentWeapon.fireRate)
            {
                currentWeapon.Shoot(playerId);
                lastShot = Time.time;
            }
            if (Input.GetButtonUp("Fire" + playerId))
            {
                GetComponent<AudioSource>().Stop();
            }

        }
        else
        {
            if (Input.GetButtonDown("Fire"+playerId))
            {
                currentWeapon.Shoot(playerId);
                lastShot = Time.time;
            }
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
        GetComponent<AudioSource>().Stop();
        if (currentWeapon.type == typeOfGun)
            return;
        System.Type type = null;
        switch(typeOfGun)
        {
            case EnumerationGun.GunType.GUN:
                type = typeof(Gun);
                break;
            case EnumerationGun.GunType.LASERGUN:
                type = typeof(LaserGun);
                break;
            case EnumerationGun.GunType.MINIGUN:
                type = typeof(MiniGun);
                break;
            case EnumerationGun.GunType.SHOTGUN:
                type = typeof(Shotgun);
                break;
            case EnumerationGun.GunType.SNIPER:
                type = typeof(Sniper);
                break;
        }
        ChangeGun(type);


    }

    public void ChangeGun(System.Type type)
    {
        Destroy(currentWeapon);
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
