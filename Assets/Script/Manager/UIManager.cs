using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    #region Singleton
    static private UIManager s_Instance;
    static public UIManager Instance
    {
        get
        {
            return s_Instance;
        }
    }
    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        DontDestroyOnLoad(this);
    }
    #endregion

    #region Score Variables
    public RectTransform scorePrefab;
    public GameObject[] PlayerUI = { null, null };
    Text[] scoreUI = { null, null };
    Text[] comboUI = { null, null };
    RectTransform[] trsCombo = { null, null };
    int[] corotineCount = { 0, 0 };
    bool[] isComboMultSliding = { false, false };
    #endregion


	void Start () {
        InitScore();
	}

    void InitScore()
    {
        if (PlayerUI[0] == null) PlayerUI[0] = GameObject.Find("Player1UI");
        if (PlayerUI[0] == null) PlayerUI[0] = GameObject.Find("Player2UI");
        scoreUI[0] = GameObject.Find("PlayerScore1").GetComponent<Text>();
        scoreUI[1] = GameObject.Find("PlayerScore2").GetComponent<Text>();
        comboUI[0] = GameObject.Find("PlayerCombo1").GetComponent<Text>();
        comboUI[1] = GameObject.Find("PlayerCombo2").GetComponent<Text>();
        scoreUI[0].text = "0";
        scoreUI[1].text = "0";
        DisplayCombo(0, 0, 0);
        DisplayCombo(1, 0, 0);
    }

    public void DisplayScore(int tabID, long score)
    {
        scoreUI[tabID].text = score.ToString();
    }

    public void DisplayCombo(int tabID, int value, int combo)
    {
        if (combo == 0){
            comboUI[tabID].text = "";
        }else{
            if (corotineCount[tabID] == 0) StartCoroutine(comboSizeFeedback(tabID, comboUI[tabID]));
            comboUI[tabID].text = value + " x" + combo;
        }
    }

    public void comboMultSlide(int tabID, int combo = 0)
    {
        if (trsCombo[tabID] == null)
        {
            trsCombo[tabID] = Instantiate(scorePrefab);
            trsCombo[tabID].parent = comboUI[tabID].transform;
        }
        trsCombo[tabID].GetComponent<Text>().text = "x" + combo.ToString();
        if (!isComboMultSliding[tabID])
        {
            isComboMultSliding[tabID] = true;
            trsCombo[tabID].transform.position = comboUI[tabID].transform.position;
            StartCoroutine(comboMultSlide(tabID, trsCombo[tabID]));
        }
    }

    

    public void comboSlide(int tabID, int value = 0)
    {
        RectTransform trs;
        trs = Instantiate(scorePrefab);
        trs.transform.position = comboUI[tabID].transform.position;
        trs.parent = comboUI[tabID].transform.parent;
        trs.GetComponent<Text>().text = value.ToString();
        StartCoroutine(comboSliding(trs));
    }

    IEnumerator comboMultSlide(int tabID, RectTransform trs)
    {
        trs.sizeDelta = new Vector2(300, 100);
        Text txt = trs.GetComponent<Text>();
        while (txt.fontSize < 80)
        {
            txt.fontSize += 1;
            txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y + 1f, txt.transform.position.z);

            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, Mathf.Sin(Time.deltaTime * 30));
            yield return new WaitForSeconds(0.02f);

        }
        txt.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Destroy(trs.gameObject);
        isComboMultSliding[tabID] = false;
    }

    public void endComboFeedback(int tabID)
    {
        corotineCount[tabID]--;
        if (corotineCount[tabID] == 0)
        {
            comboUI[tabID].transform.localRotation = new Quaternion(0, 0, 0, 0);
            comboUI[tabID].resizeTextMaxSize = 30;
        }
    }

    IEnumerator comboSliding(RectTransform trs)
    {
        Text txt = trs.GetComponent<Text>();
        while (txt.fontSize > 5)
        {
            //rotation
            //txt.transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time)*180);

            txt.fontSize -= 1;
            txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y - 5f, txt.transform.position.z);

            yield return new WaitForSeconds(0.02f);

        }
        txt.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Destroy(trs.gameObject);
    }

    IEnumerator comboSizeFeedback(int tabID, Text txt)
    {
        corotineCount[tabID]++;
        while (txt.resizeTextMaxSize < 50)
        {
            txt.transform.localEulerAngles = new Vector3(0, 0, 10);//new Quaternion(10f,0,0,0);
            txt.resizeTextMaxSize += 1;
            yield return new WaitForSeconds(0.01f);
        }
    }

    
}
