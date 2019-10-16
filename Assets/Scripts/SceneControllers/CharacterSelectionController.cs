using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterSelectionController : MonoBehaviour {
    
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
