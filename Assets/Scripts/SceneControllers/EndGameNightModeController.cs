using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGameNightModeController : MonoBehaviour {
    
    public void OpenMainMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenInGame()
    {
        SceneManager.LoadScene("InGameNightMode");
    }
}
