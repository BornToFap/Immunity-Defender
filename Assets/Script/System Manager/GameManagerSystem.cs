using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Audio;
public class GameManagerSystem : MonoBehaviour
{
    public static GameManagerSystem instances;
 //   [SerializeField] List<GameObject> VirusButton;
    [SerializeField] GameObject MenuHandler;
    [SerializeField] private GameObject Encyclopedia;
  //  EnemySystem enemySystem;


    [SerializeField] private GameObject VirusButtons;
    [SerializeField] private GameObject CellsButtons;
   public Animator anim;
    public Animator Flipanim;
    public GameObject bookanim;
    public GameObject ObjectFlip;
    public bool FlipABook = false;
    public static GameManagerSystem Instances { get { return instances; } }

    public GameObject ButtonMenu;
    public GameObject BckBtn;


    public Animator closeAnim;
    public GameObject CloseGame;

    public GameObject ImmuneBoosterObject;
    PlayerSystem playeron;
    private void Awake()
    {
        if(instances != null && instances != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instances = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
       
    }
    public string scenename;

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }


        if (bookanim.activeInHierarchy)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            {
                MenuHandler.SetActive(false);
                Encyclopedia.SetActive(true);
                bookanim.SetActive(false);

            }
        }

        if (CloseGame.activeInHierarchy)
        {
            if (closeAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !closeAnim.IsInTransition(0))
            {
                CloseGame.SetActive(false);
            }
        }
        

        if (ObjectFlip.activeInHierarchy)
        {
            ButtonMenu.SetActive(false);
            Flipanim.SetBool("FlipOpen", FlipABook);

            if (Flipanim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !Flipanim.IsInTransition(0))
            {
                FlipABook = false;
                BckBtn.SetActive(true);
                ObjectFlip.SetActive(false);
                Encyclopedia.SetActive(true);
               

            }
        }
        if (SceneManager.GetActiveScene().buildIndex >= 3)
        {
            EnemyNumber.gameObject.SetActive(true);
            playeron = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSystem>();
            if (playeron != null)
            {
                
                if (playeron.playerHeath <= 0)
                {
                    int i = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(i);
                }
            }
        }
        else
        {
            EnemyNumber.gameObject.SetActive(false);
        }
        
    }
    private void LateUpdate()
    {
        CountEnemy();
    }

    public void DisplayTips()
    {
        MenuHandler.SetActive(true);
        Encyclopedia.SetActive(false);
        Time.timeScale = 0f;
    }

    public void CloseTipsMenu()
    {
        MenuHandler.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToMap() {
        if (SceneManager.GetActiveScene().buildIndex >= 3)
        {
            currentStars = 0;
            PlayerPrefs.SetInt("Lv" + levelindex, currentStars);
            SceneManager.LoadScene("MapScene");
            
        }

        MenuHandler.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        MenuHandler.SetActive(false);
    }

    public void RestartGame()
    {
        if (SceneManager.GetActiveScene().name != "MapScene")
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
            MenuHandler.SetActive(false);
        }

    }
    public void DisplayEncylopeia()
    {
        bookanim.SetActive(true);
        anim.SetBool("OpenBook", true);        
        
    }


    public void CloseEncylopedia()
    {
        CloseGame.SetActive(true);
        closeAnim.SetBool("CloseBook", true);
            Encyclopedia.SetActive(false);
        //CloseGame.SetActive(false);
        
    }

    public void BackButton()
    {
             ButtonMenu.SetActive(true);
             BckBtn.SetActive(false); 
    }


    
    public void DisplayVirus()
    {

 
            CellsButtons.SetActive(false);
            VirusButtons.SetActive(true);
        ImmuneBoosterObject.SetActive(false);

    }

    public void DisplayImmuneList()
    {
      
            VirusButtons.SetActive(false);
            CellsButtons.SetActive(true);
        ImmuneBoosterObject.SetActive(false);
    }


    public void DisplayImmuneBooster()
    {
        ImmuneBoosterObject.SetActive(true);
        VirusButtons.SetActive(false);
        CellsButtons.SetActive(false);
    }


    [SerializeField] GameObject[] enemyholder;
    [SerializeField] GameObject lvlclear;
    [SerializeField] Text EnemyNumber;
    public void CountEnemy()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 3)
        {
           enemyholder = GameObject.FindGameObjectsWithTag("Enemy");
           EnemyNumber.text = "Viruses Left: " + enemyholder.Length.ToString();
            if (enemyholder.Length <= 0)
            {
                
                lvlclear.SetActive(true);
              //  Time.timeScale = 0.5f;
             //   Time.fixedDeltaTime = 0.02f * Time.timeScale;
                // SceneManager.LoadScene("MapScene");
                StarsSetter();
                StartCoroutine(MapLoad());
            }
        }
        else
        {
           // Debug.Log("Map scene");
            lvlclear.SetActive(false);
            StopAllCoroutines();
        }
        
    }

    IEnumerator MapLoad()
    {
        yield return new WaitForSeconds(2f);
       // Time.timeScale = 1f;
        lvlclear.SetActive(false);
        SceneManager.LoadScene("MapScene");
       
    }

   
   public int currentStars = 0;
   public int levelindex;
    private void StarsSetter()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 3)
        {
            levelindex = SceneManager.GetActiveScene().buildIndex;
         }
        PlayerSystem playercomp = GameObject.Find("Player").GetComponent<PlayerSystem>();
            if(playercomp.playerHeath == playercomp.NumHeart)
            {
                currentStars = 3;
        
                PlayerPrefs.SetInt("Lv" + levelindex, currentStars);


        }
            else if(playercomp.playerHeath >= Mathf.RoundToInt((playercomp.playerHeath/playercomp.NumHeart)) && playercomp.playerHeath <= playercomp.NumHeart)
            {
                currentStars = 2;
                PlayerPrefs.SetInt("Lv" + levelindex, currentStars);
        
            }
            else if (playercomp.playerHeath >= playercomp.NumHeart  && playercomp.playerHeath <= Mathf.RoundToInt((playercomp.playerHeath / playercomp.NumHeart)))
            {
                currentStars = 1;      
                PlayerPrefs.SetInt("Lv" + levelindex, currentStars);
           }
        else
        {
            currentStars = 0;
            PlayerPrefs.SetInt("Lv" + levelindex, currentStars);
           
        }
        PlayerPrefs.Save();
    }

   private void SaveFunction()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveGame_" + DateTime.Today + ".txt";
        FileStream stream = new FileStream(path, FileMode.Create);
        GameManagerSystem data = new GameManagerSystem();
        formatter.Serialize(stream, data);
    }
  
}
