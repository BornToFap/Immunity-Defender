using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStory: MonoBehaviour
{
    public LevelManager lvl;
    public string mapload;


   void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<LevelManager>().LoadScene(mapload);
        }
    }
    public void OnEnable()
    {
        FindObjectOfType<LevelManager>().LoadScene(mapload);
    }
}
