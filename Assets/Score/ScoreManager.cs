using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    
    public RectTransform scorePrefab;
    public GameObject[] PlayerUI = {null,null};
    Text[] scoreUI = {null, null};
    Text[] comboUI = { null, null };
    int[] scoreValue = { 0, 0 };
    int[] comboMultiplicateur = { 0, 0 };
    int[] comboValue = { 0, 0 };
    float[] lastTimeScore = {0f,0f};
    public float delayCombo = 2f;
    bool[] isComboMultSliding = { false, false };

	// Use this for initialization
	void Start () {
        if (PlayerUI[0] == null) PlayerUI[0] = GameObject.Find("Player1UI");
        if (PlayerUI[0] == null) PlayerUI[0] = GameObject.Find("Player2UI");
        scoreUI[0] = GameObject.Find("PlayerScore1").GetComponent<Text>();
        scoreUI[1] = GameObject.Find("PlayerScore2").GetComponent<Text>();
        comboUI[0] = GameObject.Find("PlayerCombo1").GetComponent<Text>();
        comboUI[1] = GameObject.Find("PlayerCombo2").GetComponent<Text>();
        scoreUI[0].text = scoreValue[0].ToString();
        scoreUI[1].text = scoreValue[1].ToString();
        StartCoroutine(comboDecrease(0));
        StartCoroutine(comboDecrease(1));
        displayCombo(0);
        displayCombo(1);
        comboSlide(0);
        comboSlide(1);
	}
	
    

	// Update is called once per frame
	void Update () {
        // TEST INPUT
	    if(Input.GetKeyDown(KeyCode.A))
        {
            addScore(1, 100);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            addScore(2,100);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            addCombo(1, 2);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            addCombo(2, 2);
        }

        // Timer Combo
        
	}

    public void addScore(int playerID, int amount)
    {

        playerID = Mathf.Clamp(playerID, 1, 2);
        playerID--;

        lastTimeScore[playerID] = Time.time;
        if (comboMultiplicateur[playerID] > 0)
        {
            comboValue[playerID] += amount;
            displayCombo(playerID);
            comboSlide(playerID, comboValue[playerID]);
        }
        else
        {
            scoreValue[playerID] += amount;
            displayScore(playerID);
        }
        
    }

    public void addCombo(int playerID, int amount = 1)
    {
        playerID--;
        amount = Mathf.Abs(amount);
        
        lastTimeScore[playerID] = Time.time;
        comboMultiplicateur[playerID] += amount;
        if(corotineCount[playerID] == 0) StartCoroutine(comboSizeFeedback(playerID, comboUI[playerID]));
        comboMultSlide(playerID, comboMultiplicateur[playerID]);
        
        displayCombo(playerID);
    }

    void displayScore(int tabID)
    {
        scoreUI[tabID].text = scoreValue[tabID].ToString();
    }

    void displayCombo(int tabID)
    {
        if(comboMultiplicateur[tabID] ==0)
        {
            comboUI[tabID].text = "";
        }
        else
        {
            comboUI[tabID].text = comboValue[tabID] + " x" + comboMultiplicateur[tabID].ToString();
            
        }
        
    }

    
    RectTransform[]  trsCombo= {null, null};
    void comboMultSlide(int tabID, int value = 0)
    {
        if(trsCombo[tabID] == null)
        {
            trsCombo[tabID] = Instantiate(scorePrefab);
            trsCombo[tabID].parent = comboUI[tabID].transform;
        }
        trsCombo[tabID].GetComponent<Text>().text = "x" + value.ToString();
        if (!isComboMultSliding[tabID])
        {
            isComboMultSliding[tabID] = true;
            trsCombo[tabID].transform.position = comboUI[tabID].transform.position;
            StartCoroutine(comboMultSlide(tabID, trsCombo[tabID]));
        }
        

        
        
    }


    int[] corotineCount = {0,0};
    void comboSlide(int tabID, int value=0)
    {
        RectTransform trs;
        trs = Instantiate(scorePrefab);
        trs.transform.position = comboUI[tabID].transform.position;
        trs.parent = comboUI[tabID].transform.parent;
        trs.GetComponent<Text>().text = value.ToString();
        StartCoroutine(comboSliding(trs));
    }

    IEnumerator comboMultSlide(int tabID,RectTransform trs)
    {
       trs.sizeDelta = new Vector2(300,100);
        Text txt = trs.GetComponent<Text>();
        while (txt.fontSize < 80)
        {
            txt.fontSize += 1;
            txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y +1f, txt.transform.position.z);
            
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, Mathf.Sin(Time.deltaTime*30));
            yield return new WaitForSeconds(0.02f);

        }
        txt.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Destroy(trs.gameObject);
        isComboMultSliding[tabID] = false;
    }


    IEnumerator comboSliding(RectTransform trs)
    {
        Text txt = trs.GetComponent<Text>();
        while (txt.fontSize > 5)
        {
            //rotation
            //txt.transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time)*180);

            txt.fontSize -= 1;
            txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y -5f, txt.transform.position.z);
            
            yield return new WaitForSeconds(0.02f);
            
        }
        txt.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Destroy(trs.gameObject);
    }

    IEnumerator comboSizeFeedback(int tabID, Text txt)
    {
        /*corotineCount[tabID]++;
        txt.resizeTextMaxSize = 50;
        txt.transform.localEulerAngles = new Vector3(0,0,10);//new Quaternion(10f,0,0,0);
        yield return new WaitForSeconds(0.2f);
        endComboFeedback(tabID, txt);*/
        corotineCount[tabID]++;

        while (txt.resizeTextMaxSize < 50)
        {
            txt.transform.localEulerAngles = new Vector3(0, 0, 10);//new Quaternion(10f,0,0,0);
            txt.resizeTextMaxSize += 1;
            yield return new WaitForSeconds(0.01f);

        }
    }

    IEnumerator comboDecrease(int tabID)
    {
        int cacheScore;
        while (true)
        {
            /*if (comboMultiplicateur[ID] > 0)
            {
                yield return new WaitForSeconds(2);
                comboMultiplicateur[ID] -= 1;
                displayCombo(ID);
            }*/
            if (comboMultiplicateur[tabID] > 0 && delayCombo + lastTimeScore[tabID] < Time.time)
            {
                cacheScore = comboValue[tabID] * comboMultiplicateur[tabID];
                comboMultiplicateur[tabID] = 0;
                comboValue[tabID] = 0;
                addScore(tabID + 1, cacheScore);
                displayCombo(tabID);
                displayScore(tabID);
                endComboFeedback(tabID, comboUI[tabID]);
            }
            else
            {
                yield return null;
            }
        }


    }

    void endComboFeedback(int tabID, Text txt)
    {
        corotineCount[tabID]--;
        if (corotineCount[tabID]==0)
        {
            txt.transform.localRotation = new Quaternion(0, 0, 0, 0);
            txt.resizeTextMaxSize = 30;
        }
    }
}
