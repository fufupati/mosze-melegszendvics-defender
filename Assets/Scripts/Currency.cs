using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    Text CurrencyTextUI;
    int score;

    public int Score
    {
        get { return this.score; }
        set { this.score = value; }
    }

    void Start()
    {
        CurrencyTextUI = GetComponent<Text>();
    }

    void Update()
    {
        string scoreStr = string.Format("{0:000000}", score);
        CurrencyTextUI.text = scoreStr;
    }

    public bool DeductPoints(int amount)
    {
        if (score >= amount)
        {
            score -= amount;
            return true;
        }
        return false; 
    }
}
