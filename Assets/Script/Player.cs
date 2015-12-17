using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDestroyable{

    [Header("Weapon")]
    public IWeapon currentWeapon;

    [Header("Stats")]
    public int playerId;
    public float healthPoint;
    public float maxHealthPoint;

    [Header("Cursor")]
    public float cursorSensibility;
    public Texture cursorTexture = null;
    private Vector3 cursorPosition;
    private Vector3 ScreenPos;

    void Awake()
    {
        Gun gun = gameObject.AddComponent<Gun>();
        gun.AttachTo(gameObject);
        healthPoint = maxHealthPoint;
        cursorPosition = Camera.main.WorldToScreenPoint(transform.position);
        cursorPosition.y = Screen.height / 2f;
        cursorSensibility = Screen.width / 3f;

    }

    void OnGUI()
    {
        if(cursorTexture)
        {
            cursorPosition.x = Mathf.Clamp(cursorPosition.x, (-cursorTexture.width / 2f), Screen.width - (cursorTexture.width / 2f));
            cursorPosition.y = Mathf.Clamp(cursorPosition.y, (-cursorTexture.height / 2f), Screen.height - (cursorTexture.height / 2f));
            GUI.DrawTexture(new Rect(cursorPosition.x, cursorPosition.y, cursorTexture.width, cursorTexture.height), cursorTexture);
        }
    }
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxisRaw("Horizontal " + playerId);
        float v = Input.GetAxisRaw("Vertical " + playerId);
        if(Mathf.Abs(h)>0.3f)
            cursorPosition.x += h * cursorSensibility * Time.deltaTime;
        if(Mathf.Abs(v) > 0.3f)
            cursorPosition.y += v * cursorSensibility * Time.deltaTime;
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
