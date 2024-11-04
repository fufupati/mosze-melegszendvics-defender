using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    Text scoreTextUI;
    int score;

    public int Score
    {

        get
        {
            return this.score;

        }set
        {
            this.score = value;
        }
    }

    public void Start()
    {
        scoreTextUI = GetComponent<Text> ();
    }

    // Update is called once per frame
    public void Update()
    {
        string scoreStr = string.Format ("{0:000000}",score );
        scoreTextUI.text = scoreStr;
    }
}
