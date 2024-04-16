using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMov : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;
    public EnemyManager enemyManager;
    private float spawnRate = 1.5f;
    private float spawnRangeX = 9.0f;
    void Start()
    {
        enemyManager = GameObject.Find("Game Manager").GetComponent<EnemyManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();


        StartCoroutine(MainMenuTarget());

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator MainMenuTarget()
    {
        while (gameManager.isGameActive == false)
        {


            yield return new WaitForSeconds(spawnRate);
            int enemyIndex = UnityEngine.Random.Range(0, enemyManager.enemyPrefabs.Count);
            Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-spawnRangeX, spawnRangeX), -20, -1);
    
          float y = Random.Range(0f, 1f);

            if (gameManager.isGameActive == false)
            {
                Instantiate(enemyManager.enemyPrefabs[enemyIndex], spawnPos, enemyManager.enemyPrefabs[enemyIndex].transform.rotation);

               
            }

        }
        
    }

}
