using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImmuneBooster : MonoBehaviour
{

    [SerializeField] Text CellsName;
    [SerializeField] Text CellDetails;
    [SerializeField] Image CellsImage;
    [SerializeField] List<Sprite> CellsSprite = new List<Sprite>();
    [SerializeField] List<string> CellsDetails = new List<string>();

 
    public GameObject ICellDetailsP;
    public GameObject ImnCellButton;
    GameManagerSystem gameM;

    
    void Start()
    {
        gameM = GetComponentInParent<GameManagerSystem>();
    }

    private void Update()
    {
        if (gameM.ButtonMenu.activeInHierarchy)
        {
            ICellDetailsP.SetActive(false);
            StopAllCoroutines();
        }
    }
    public void Cells01()
    {
        ImnCellButton.SetActive(false);
        gameM.ObjectFlip.SetActive(true);
        gameM.FlipABook = true;
     
          
            int i = CellsSprite.FindIndex(0, CellsSprite.Count, s => s.name == "VitaminC");
            CellsImage.sprite = CellsSprite[i];
            CellsName.text = "Vitamin C (Ascorbic Acid)";
            CellDetails.text = CellsDetails[0];
        StartCoroutine(ActivtaePanel());
       

    }

    IEnumerator ActivtaePanel()
    {
        yield return new WaitForSeconds(1f);
        ICellDetailsP.SetActive(true);
    }
    /*
    public void Cells02()
    {
        gameM.ObjectFlip.SetActive(true);
        gameM.FlipABook = true;
        int i = CellsSprite.FindIndex(0, CellsSprite.Count, s => s.name == "WBC");
        CellsImage.sprite = CellsSprite[i];
        CellsName.text = "Vitamin D";
        CellDetails.text = CellsDetails[1];
        CellDetailsP.SetActive(true);
        CellButton.SetActive(false);
    }

    public void Cells03()
    {
        gameM.ObjectFlip.SetActive(true);
        gameM.FlipABook = true;
        int i = CellsSprite.FindIndex(0, CellsSprite.Count, s => s.name == "WBC");
        CellsImage.sprite = CellsSprite[i];
        CellsName.text = "Vitamin D";
        CellDetails.text = CellsDetails[2];
        CellDetailsP.SetActive(true);
        CellButton.SetActive(false);
    }
    public void Cells04()
    {
        gameM.ObjectFlip.SetActive(true);
        gameM.FlipABook = true;
        int i = CellsSprite.FindIndex(0, CellsSprite.Count, s => s.name == "WBC");
        CellsImage.sprite = CellsSprite[i];
        CellsName.text = "Zinc";
        CellDetails.text = CellsDetails[3];
        CellDetailsP.SetActive(true);
        CellButton.SetActive(false);
    }
    */

}
