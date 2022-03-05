using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private GameObject _LevelCanvas;
    [SerializeField] private Image progressBar;
    [SerializeField] private Text tipsHolder;
    [SerializeField] private Image TipsImage;
    private float Target;
    [SerializeField] private List<string> listofTips = new List<string>();
    [SerializeField] private List<Sprite> listofTipsImage = new List<Sprite>();

    [SerializeField] GameObject MainMenuCanvs;

    [SerializeField] private AudioAssets[] audios;


 
    private void Awake()
    {
       
    }

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);


        foreach (AudioAssets s in audios)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.PlayOnAwake;

        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Play("MainMenu");
        }
    }


    private void FixedUpdate()
    {


        if (SceneManager.GetActiveScene().name == "MainMenu")
        {

            MainMenuCanvs.SetActive(true);
            
        }else if (SceneManager.GetActiveScene().name != "MainMenu")
        {
          
            AudioAssets s = Array.Find(audios, sounds => sounds.audioName == "MainMenu");
            s.source.Stop();
        }


        if(SceneManager.GetActiveScene().name == "IntroCenimatic")
        {
            PlayerPrefs.SetInt("Goto", 1);
        }
      

            
        


    }

    public async void LoadScene(string sceneName)
    {
        
        Target = 0;
        progressBar.fillAmount = 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        _LevelCanvas.SetActive(true);
        DisplayTips();
        do
        {
          await Task.Delay(100);
          Target = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(3000);
        scene.allowSceneActivation = true;
        if (MainMenuCanvs != null)
        {
            MainMenuCanvs.SetActive(false);
        }

        await Task.Delay(1000);
        _LevelCanvas.SetActive(false);
       
    }

    private void Update()
    {
        
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, Target,  3 * Time.deltaTime);
    }
    

    void DisplayTips()
    {
        int randomT = UnityEngine.Random.Range(0, listofTips.Count);
        if (_LevelCanvas.activeInHierarchy)
        {
           tipsHolder.text = listofTips[randomT];
            TipsImage.sprite = listofTipsImage[randomT];
            
        }
    }
    

    public void Play(string SoundName)
    {
        AudioAssets s = Array.Find(audios, sounds => sounds.audioName == SoundName);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + SoundName + "Not Found in the array");
            return;
        }
        s.source.Play();   
        
       
        
    }


}
