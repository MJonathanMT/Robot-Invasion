using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndGameSCript : MonoBehaviour
{
    public void openEndGameScene() {
        Cursor.visible = true;
        SceneManager.LoadScene("EndGame");
    }

    public void openEndGameNightModeScene() {
        Cursor.visible = true;
        SceneManager.LoadScene("EndGameNightMode");
    }
}
