using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{
 public void StartGame()
    {
        LevelManager.Instance.LoadScene("IntroCenimatic");
    }

 public void QuitGame()
    {
        Application.Quit();
    }
}
