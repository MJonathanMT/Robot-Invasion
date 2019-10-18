using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour {
    
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenInGame()
    {
        SceneManager.LoadScene("InGame");
    }
}
