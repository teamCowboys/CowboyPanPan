using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public partial class PlayerManager : MonoBehaviour {
    
    #region Singleton
    static private PlayerManager s_Instance;
    static public PlayerManager Instance
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
    public int[] PlayerCredit = { 2, 2};
	// Use this for initialization
	void Start () {
        InitScoreUI();
	}

    // Update is called once per frame
    void Update()
    {
        // TEST INPUT
        if (Input.GetKeyDown(KeyCode.O))
        {
            addScore(0, 100);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            addScore(1, 100);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            addCombo(0, 2);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            addCombo(1, 2);
        }
    }

    public void CreditLost(int playerID)
    {
        PlayerCredit[playerID]--;
        if(PlayerCredit[playerID] == 0)
        {
            // Loser
        }
        else
        {
            // Another Try
        }
    }
}
