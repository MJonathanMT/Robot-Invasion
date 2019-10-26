using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour {

    public Text scoreDisplay;
    public ScoreManager scoreManager;
    public PlayerController player;

    void Start()
    {
        this.scoreDisplay.text = "Score: ";
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenInGame()
    {
        SceneManager.LoadScene("InGame");
    }
}
