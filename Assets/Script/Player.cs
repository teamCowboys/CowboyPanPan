using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDestroyable{

    [Header("Weapon")]
    public IWeapon currentWeapon;

    [Header("Stats")]
    public float healthPoint;
    public float maxHealthPoint;

    [Header("Cursor")]
    public Vector3 cursorPosition;
    public Texture cursorTexture = null;
    private Vector3 ScreenPos;

    void Awake()
    {
        Gun gun = gameObject.AddComponent<Gun>();
        gun.AttachTo(gameObject);
        healthPoint = maxHealthPoint;
        cursorPosition = Camera.main.WorldToScreenPoint(transform.position);

    }

    void OnGUI()
    {
        if(cursorTexture)
        {
            Vector3 ScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            float posx = Mathf.Clamp((-cursorTexture.width / 2f) + ScreenPos.x, (-cursorTexture.width / 2f), Screen.width - (cursorTexture.width / 2f));
            float posy = Mathf.Clamp(Screen.height - ScreenPos.y - (cursorTexture.height / 2f), (-cursorTexture.height / 2f), Screen.height - (cursorTexture.height / 2f));
            GUI.DrawTexture(new Rect(posx,posy, cursorTexture.width, cursorTexture.height), cursorTexture);
        }
    }
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetMouseButton(0))
            currentWeapon.Shoot();*/
	}

    public void ChangeGun(EnumerationGun.GunType typeOfGun)
    {
        IWeapon weapon = null;
        switch(typeOfGun)
        {
            case EnumerationGun.GunType.GUN:
                weapon = new Gun();
                break;
            /*case EnumerationGun.GunType.LASERGUN:
                weapon = new LaserGun();
                break;
            case EnumerationGun.GunType.SHOTGUN:
                weapon = new shotGun();
                break;
            case EnumerationGun.GunType.SNIPER:
                weapon = new Sniper();
                break;*/
        }
        ChangeGun(weapon);


    }

    public void ChangeGun(IWeapon nextGun)
    {
        Destroy(currentWeapon);
        nextGun.AttachTo(gameObject); 
        healthPoint = maxHealthPoint;

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
