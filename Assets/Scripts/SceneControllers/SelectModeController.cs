using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectModeController : MonoBehaviour {

    public void OpenInGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void OpenInGameNightMode()
    {
        SceneManager.LoadScene("InGameNightMode");
    }
}
