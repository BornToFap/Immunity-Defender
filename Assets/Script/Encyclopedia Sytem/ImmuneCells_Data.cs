using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImmuneCells_Data : MonoBehaviour
{
    [SerializeField] Text CellsName;
    [SerializeField] Text CellDetails;
    [SerializeField] Image CellsImage;
    [SerializeField] List<Sprite> CellsSprite = new List<Sprite>();
    [SerializeField] List<string> CellsDetails = new List<string>();

    //public GameObject virusActive;
    public GameObject CellDetailsP;
    public GameObject CellButton;
    GameManagerSystem gameM;

    void Start()
    {
        gameM = GetComponentInParent<GameManagerSystem>();
    }

    private void Update()
    {
        if (gameM.ButtonMenu.activeInHierarchy)
        {
            CellDetailsP.SetActive(false);
        }
    }
    public void Cells01()
    {
        gameM.ObjectFlip.SetActive(true);
        gameM.FlipABook = true;
        int i = CellsSprite.FindIndex(0, CellsSprite.Count, s => s.name == "WBC");
        CellsImage.sprite = CellsSprite[i];
        CellsName.text = "White Blood Cells (Leukocytes)";
        CellDetails.text = CellsDetails[0];
        CellDetailsP.SetActive(true);
       CellButton.SetActive(false);
    }

    public void Cells02()
    {
        int i = CellsSprite.FindIndex(0, CellsSprite.Count, s => s.name == "DengueV");
        CellsImage.sprite = CellsSprite[i];
        CellsName.text = "Dengue Cells";
    }



}
