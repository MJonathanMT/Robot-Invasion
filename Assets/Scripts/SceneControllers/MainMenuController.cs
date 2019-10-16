using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    
    public void StartGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void OpenCharacterSelection()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void OpenInfo()
    {
        SceneManager.LoadScene("Info");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}
