using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class VirusData : MonoBehaviour
{
    [SerializeField] Text virusName;
    [SerializeField] Text virusDetails;
    [SerializeField] Image virusImage;
    [SerializeField] List<Sprite> VirusSprite = new List<Sprite>();
    public int status = 0;
    [SerializeField] List<string> VirusDetails = new List<string>();
    public List<int> IDholder = new List<int>();
    public List<GameObject> virusesbutton = new List<GameObject>();
   public GameObject virusActive;
    public GameObject VDetails;
    public GameObject VirusButton;
    GameManagerSystem gameM;
    private void Start()
    {
        gameM = GetComponentInParent<GameManagerSystem>();
        if (virusActive.activeInHierarchy)
        {
            virusesbutton = GameObject.FindGameObjectsWithTag("VirusesDetails").ToList();
            foreach (GameObject a in virusesbutton)
            {
                a.GetComponent<Button>().interactable = false;


            }

            for (int i = 0; i < virusesbutton.Count; i++)
            {
                IDholder.Add(virusesbutton[i].GetComponentInChildren<Identifier>().IDNUM);

            }
        }
    }
    
    
    private void Update()
    {


        for (int a = 0; a < virusesbutton.Count; a++)
            {
                if (PlayerPrefs.GetInt("VID" + IDholder[a], 0) > 0)
                {
                    virusesbutton[a].GetComponent<Button>().interactable = true;
                }
            }

        if (gameM.ButtonMenu.activeInHierarchy)
        {
            VDetails.SetActive(false);
        }
        
    }
    public void Virus01()
    {
            gameM.ObjectFlip.SetActive(true);
            gameM.FlipABook = true;
        VDetails.SetActive(true);
        int i = VirusSprite.FindIndex(0, VirusSprite.Count, s => s.name == "Covid19");
            virusImage.sprite = VirusSprite[i];
            virusName.text = "SARS-CoV-2 (Covid-19)";
            virusDetails.text = VirusDetails[0];
        VirusButton.SetActive(false);
       
       

    }

    public void Virus02()
    {
        gameM.ObjectFlip.SetActive(true);
        gameM.FlipABook = true;
        VDetails.SetActive(true);
        int i = VirusSprite.FindIndex(0, VirusSprite.Count, s => s.name == "InfluenzaVirus");
        virusImage.sprite = VirusSprite[i];
        virusName.text = "Influenza A H1N1";
        virusDetails.text = VirusDetails[1];
        VirusButton.SetActive(false);
    }

    public void Virus03()
    {
        gameM.ObjectFlip.SetActive(true);
        gameM.FlipABook = true;
        VDetails.SetActive(true);
        int i = VirusSprite.FindIndex(0, VirusSprite.Count, s => s.name == "rhinoviruses");
        virusImage.sprite = VirusSprite[i];
        virusName.text = "Human Rhino Virus (HRV)";
        virusDetails.text = VirusDetails[2];
        VirusButton.SetActive(false);
    }

    public void Virus04()
    {
        gameM.ObjectFlip.SetActive(true);
        gameM.FlipABook = true;
        VDetails.SetActive(true);
        int i = VirusSprite.FindIndex(0, VirusSprite.Count, s => s.name == "rsv");
        virusImage.sprite = VirusSprite[i];
        virusName.text = "Respiratory Syncytial Virus (RSV)";
        virusDetails.text = VirusDetails[3];
        VirusButton.SetActive(false);
    }


}
