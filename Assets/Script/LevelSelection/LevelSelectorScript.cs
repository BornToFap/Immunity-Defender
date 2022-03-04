using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectorScript : MonoBehaviour
{
    [SerializeField] private bool Unlocked;
    [SerializeField] Image unlockImage;
    [SerializeField] List<GameObject> star = new List<GameObject>();
    public int lvlindex;
    [SerializeField] Sprite star_sprite;
   
    private void Update()
    {
        UpdateLevelImage();
       UpdateLevelSelector();
    }

    void UpdateLevelSelector()
    {
        if(PlayerPrefs.GetInt("Lv" + (lvlindex - 1)) > 0)
        {
            Unlocked = true;
        }
        else
        {

        }
     
      //  Debug.Log(PlayerPrefs.GetInt("Lv" + lvlindex));
    }
    private void UpdateLevelImage()
    {
        if (!Unlocked)
        {
            unlockImage.gameObject.SetActive(true);
            for(int i = 0; i < star.Count; i++)
            {
                star[i].SetActive(false);
            }
        }
        else
        {
            unlockImage.gameObject.SetActive(false);
            for (int i = 0; i < star.Count; i++)
            {
                star[i].SetActive(true);
            }

            for(int a = 0; a < PlayerPrefs.GetInt("Lv" + lvlindex); a++) {
                star[a].GetComponent<Image>().sprite = star_sprite;
            }
        }
    }

    public void PressSelection(string levelName)
    {
        if (Unlocked == true)
        {
            FindObjectOfType<LevelManager>().LoadScene(levelName);
          
            //SceneManager.LoadScene(levelName);
           
        }
        else
        {
            return;
        }
       
    }
}
