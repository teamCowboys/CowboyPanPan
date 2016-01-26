using UnityEngine;
using System.Collections;

public class SpawnerOption : MonoBehaviour {
    //public float startDelaySpawn = 0.0f;
    [Header("Spawner Option")]
    public GameObject prefabEnnemy;
    public float waitBetweenSpawn = 1f;
    public int numberToSpawn = 10;
    [Space]
    public bool spawnWithoutWaiting;
    public float waitBetweenWave = 1f;
    public int numberByWave = 3;

    [Header("Loot")]
    public int lootLuck;
    public GameObject[] lootList;

    [Header("Ennemy Option")]
    
    public int layerID = 1;
    public bool canMove;
    public bool moveRight;
    public bool canHide;
    public int LifePoints=0 ;
    public int scoreValue = 0;
    public float shootRate = 2;
    public float speed = 1;
    
    Ennemy current;
    Transform trsm;
    bool randomScore, randomLife, alive;

    void Start () {
        trsm = this.gameObject.GetComponent<Transform>();
        if (LifePoints == 0)
        {
            randomLife = true;
        }
        else
        {
            randomLife = false;
        }
        if (scoreValue == 0)
        {
            randomScore = true;
        }
        else
        {
            randomScore = false;
        }
        if (!canMove)
            spawnWithoutWaiting = false;
        StartCoroutine(Spawn());
    }
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            CheckLife();
        }
	}

    IEnumerator Spawn()
    {
        if (spawnWithoutWaiting)
        {
            int wave = numberByWave;
            while (wave > 0 && numberToSpawn>0)
            {
                wave--;
                yield return new WaitForSeconds(waitBetweenSpawn);
                numberToSpawn--;
                current = ((GameObject)Instantiate(prefabEnnemy, trsm.position, prefabEnnemy.transform.rotation)).GetComponent<Ennemy>();
                current.gameObject.transform.parent = this.transform;
                Init();
            }
            StartCoroutine(nextWave());
        }
        else
        {
            yield return new WaitForSeconds(waitBetweenSpawn);
            
            numberToSpawn--;
            current = ((GameObject)Instantiate(prefabEnnemy, trsm.position, prefabEnnemy.transform.rotation)).GetComponent<Ennemy>();
            current.gameObject.transform.parent = this.transform;
            Init();
        }
        
    }

    IEnumerator nextWave()
    {
        yield return new WaitForSeconds(waitBetweenWave);
        StartCoroutine(Spawn());
    }

    void generateScore()
    {
        scoreValue = Mathf.FloorToInt(Random.Range(10, 50) * 10);
    }

    void generateLife()
    {
        LifePoints = Mathf.FloorToInt(Random.Range(1, 10));
    }

    void generateLoot()
    {
        int rand = Mathf.FloorToInt(Random.Range(0,100));
        if (lootLuck > rand)
        {
            rand = Mathf.FloorToInt(Random.Range(0, lootList.Length-1));
            current.loot = lootList[rand];
        }

        //current.loot = ;
    }

    void Init()
    {
        if (current != null)
        {
            current.canHide = canHide;
            current.canMove = canMove;
            if (canMove)
                current.gameObject.GetComponentInChildren<Move>().moveRight = moveRight;
            current.fight();
            //
            if (randomScore)
                generateScore();
            if (randomLife)
                generateLife();
            current.lifePoints = LifePoints;
            current.value = scoreValue;
            current.gameObject.GetComponentInChildren<Shoot>().shootRate = shootRate;
            current.gameObject.GetComponentInChildren<Move>().shootRate = shootRate;
            current.gameObject.GetComponentInChildren<Move>().speed = speed;
            current.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = layerID;
            generateLoot();
            alive = true;
            
        }
        
    }

    void CheckLife()
    {
        if (current != null)
        {
            if (current.lifePoints <= 0)
            {
                alive = false;
                Destroy(current.gameObject);

                if (numberToSpawn > 0 && !spawnWithoutWaiting)
                {
                    StartCoroutine(Spawn());
                }
            }
        }
        else
        {
            alive = false;
        }
        
    }
}
