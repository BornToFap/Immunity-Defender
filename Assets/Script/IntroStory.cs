using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStory: MonoBehaviour
{
    public LevelManager lvl;
    public string mapload;
    public void OnEnable()
    {
        FindObjectOfType<LevelManager>().LoadScene(mapload);
    }
}
