using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text finalScore;
    private int score;

    void Start()
    {
        score = ScoreManager.score;
        if (score == 0)
        {
            this.finalScore.text = "Score: " + ScoreManager.score;
        }
        else {
            this.finalScore.text = "Score: " + (ScoreManager.score - 100);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
