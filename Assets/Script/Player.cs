using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDestroyable{

    [Header("Weapon")]
    public AbstractWeapon currentWeapon;

    [Header("Stats")]
    public int playerId;
    public float healthPoint;
    public float maxHealthPoint;

 

    void Awake()
    {
        Gun gun = gameObject.AddComponent<Gun>();
        gun.AttachTo(gameObject,typeof(Gun));
        healthPoint = maxHealthPoint;

    }

    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Fire"+playerId))
        {
            currentWeapon.Shoot();
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            Debug.Log("mouse position " + Input.mousePosition+" Cursor position "+ GetComponent<PlayerAim>().getCursorPosition());
            //if (Physics.Raycast(Camera.main.ScreenPointToRay(GetComponent<PlayerAim>().getCursorPosition()), out hitInfo))
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                IDestroyable test = hitInfo.collider.GetComponent(typeof(IDestroyable)) as IDestroyable;
                test.applyDamage(5);
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

    public void applyDamage(float damage)
    {
        healthPoint -= damage;

    }

    public void Death()
    {
        // do nothing yet
    }
}
