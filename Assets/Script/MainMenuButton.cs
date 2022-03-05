using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    // Start is called before the first frame update

   
    public void StartGame()
    {
        if (PlayerPrefs.GetInt("Goto", 0) == 0)
        {
            SceneManager.LoadScene("IntroCenimatic");
        }
        else
        {
            FindObjectOfType<LevelManager>().LoadScene("MapScene");
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
