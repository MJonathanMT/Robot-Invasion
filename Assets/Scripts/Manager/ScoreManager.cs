using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
    
    public static int score = 0;
    public static int killValue = 100;
    
	public static void ResetScore()
    {
        score = 0;
    }
    public static void killScore(){
        score += killValue;

    }
}
