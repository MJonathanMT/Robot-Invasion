using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InfoController : MonoBehaviour {
    
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
