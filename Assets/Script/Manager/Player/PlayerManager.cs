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

	// Use this for initialization
	void Start () {
        InitScoreUI();
	}

    // Update is called once per frame
    void Update()
    {
        // TEST INPUT
        if (Input.GetKeyDown(KeyCode.A))
        {
            addScore(0, 100);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            addScore(1, 100);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            addCombo(0, 2);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            addCombo(1, 2);
        }
    }
}
