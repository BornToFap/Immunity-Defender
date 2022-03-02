using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManger : MonoBehaviour
{

    // [SerializeField] List<GameObject> enemyActve = new List<GameObject>();
    [SerializeField] GameObject[] enemyholder;
    [SerializeField] GameObject lvlclear;
    private void Update()
    {
        enemyholder = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyholder.Length <= 0)
        {
            lvlclear.SetActive(true);
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            StartCoroutine(MapLoad());
        }
    }

    IEnumerator MapLoad()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MapScene");
    }
}
