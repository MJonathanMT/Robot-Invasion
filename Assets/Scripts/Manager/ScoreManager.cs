using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
    
    public int score = 0;
    public int killValue = 100; 
    
	public void ResetScore()
    {
        this.score = 0;
    }
    public void killScore(){
        this.score += killValue;
    }
}
