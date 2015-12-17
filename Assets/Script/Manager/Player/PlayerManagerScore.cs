using UnityEngine;
using System.Collections;


public partial class PlayerManager : MonoBehaviour
{
    
    int[] scoreValue = { 0, 0 };
    int[] comboMultiplicateur = { 0, 0 };
    int[] comboValue = { 0, 0 };
    float[] lastTimeScore = { 0f, 0f };
    public float delayCombo = 2f;

    // Use this for initialization
    void InitScoreUI()
    {
        StartCoroutine(comboDecrease(0));
        StartCoroutine(comboDecrease(1));
        
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
                //displayCombo(tabID);
                //displayScore(tabID);
                //endComboFeedback(tabID, comboUI[tabID]);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void addScore(int playerID, int amount)
    {
        lastTimeScore[playerID] = Time.time;
        if (comboMultiplicateur[playerID] > 0)
        {
            comboValue[playerID] += amount;

            UIManager.Instance.displayCombo(playerID, comboValue[playerID], comboMultiplicateur[playerID]);
            //comboSlide(playerID, comboValue[playerID]);
        }
        else
        {
            scoreValue[playerID] += amount;
            UIManager.Instance.displayScore(playerID, amount);
        }

    }

    public void addCombo(int playerID, int amount = 1)
    {
        lastTimeScore[playerID] = Time.time;
        comboMultiplicateur[playerID] += amount;
        

        UIManager.Instance.displayCombo(playerID, 0,amount);
    }

    
}
