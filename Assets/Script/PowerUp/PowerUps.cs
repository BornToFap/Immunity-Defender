using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerUps : MonoBehaviour
{
    [SerializeField]private int plusD;
    [SerializeField] Canvas levelCanvas;
    Font arial;
    Transform playerTransform;
    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<PlayerSystem>().transform;
    }
    private void CreateTextPowerUps(string powerText)
    {
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        GameObject newGO = new GameObject("PoweUpsText");
        newGO.transform.parent = levelCanvas.transform;

        Text myText = newGO.AddComponent<Text>();
        myText.text = powerText;
        myText.fontSize = 30;
        myText.font = arial;

      //  myText.rectTransform.TransformDirection()
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CreateTextPowerUps("Hello");
        Destroy(this.gameObject);
    }

}
