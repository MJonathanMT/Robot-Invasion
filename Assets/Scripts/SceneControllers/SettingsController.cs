using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour {
    
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
