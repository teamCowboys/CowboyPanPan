using UnityEngine;
using System.Collections;


public partial class PlayerManager : MonoBehaviour
{
    // tableau avec 2 cases
    //index 0 -> Player 1
    //index 1 -> Player 2

    long[] scoreValue = { 0, 0 };
    int[] comboMultiplicateur = { 0, 0 };
    int[] comboValue = { 0, 0 };
    float[] lastTimeScore = { 0f, 0f };

    // Temps sans Input avant qu'un combo se finisse
    public float delayCombo = 2f;   

    // Start
    void InitScoreUI()
    {
        StartCoroutine(comboDecrease(0));
        StartCoroutine(comboDecrease(1));
        UIManager.Instance.DisplayCredit(0, PlayerCredit[0]);
        UIManager.Instance.DisplayCredit(1, PlayerCredit[1]);
    }

    public void applyScoring(int playerID, int amount)
    {
        //Debug.Log(playerID + " " + amount);
        addCombo(playerID, 1);
        addScore(playerID, amount);
    }

    // Augmente le score du joueur OU du combo
    public void addScore(int playerID, int amount)
    {
        
        lastTimeScore[playerID] = Time.time;
        if (comboMultiplicateur[playerID] > 0)
        {
            comboValue[playerID] += amount;
            UIManager.Instance.DisplayCombo(playerID, comboValue[playerID], comboMultiplicateur[playerID]);
            UIManager.Instance.comboSlide(playerID, comboValue[playerID]);
        }
        else
        {
            
            scoreValue[playerID] += amount;
            UIManager.Instance.DisplayScore(playerID, scoreValue[playerID]);
        }

    }

    // Ajoute OU augmente le combo
    public void addCombo(int playerID, int amount = 1)
    {
        Debug.Log(playerID + " " + amount);
        lastTimeScore[playerID] = Time.time;
        comboMultiplicateur[playerID] += amount;
        UIManager.Instance.DisplayCombo(playerID, comboValue[playerID], comboMultiplicateur[playerID]);
        UIManager.Instance.comboMultSlide(playerID, comboMultiplicateur[playerID]);
    }

    // Gère la fin d'un combo
    IEnumerator comboDecrease(int tabID)
    {
        int cacheScore;
        while (true)
        {
            if (comboMultiplicateur[tabID] > 0 && delayCombo + lastTimeScore[tabID] < Time.time)
            {
                cacheScore = comboValue[tabID] * comboMultiplicateur[tabID];
                comboMultiplicateur[tabID] = 0;
                comboValue[tabID] = 0;
                addScore(tabID, cacheScore);

                UIManager.Instance.DisplayCombo(tabID, 0, 0);
                UIManager.Instance.endComboFeedback(tabID);
            }
            else
            {
                yield return null;
            }
        }
    }
}
