using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCenimatic : MonoBehaviour
{
    public LevelManager lvl;
    public string mapload;
    void Update()
    {
        if (this.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<LevelManager>().LoadScene(mapload);
        }
    }
}
