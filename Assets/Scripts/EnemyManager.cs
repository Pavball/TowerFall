using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private GameManager gameManager;

    //Variables set for spawning objects
    private float spawnRate = 1.5f;
    private float spawnRangeX = 9.0f;

    public List<GameObject> enemyPrefabs;
    

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
          
    }


    public IEnumerator SpawnTarget()
    {
        while (gameManager.isGameActive == true)
        {


            yield return new WaitForSeconds(spawnRate);
            int enemyIndex = Random.Range(0, enemyPrefabs.Count - 2); 
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), -19, -1);

            if (gameManager.isGameActive == true && gameManager.score < 100)
            {
                Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
            }

            else if (gameManager.isGameActive == true && gameManager.score >= 100 && gameManager.score < 130)
            {
                int enemy2Index = Random.Range(0, enemyPrefabs.Count-1);
                Instantiate(enemyPrefabs[enemy2Index], spawnPos, enemyPrefabs[enemy2Index].transform.rotation);
            }
            else if (gameManager.isGameActive == true && gameManager.score >= 130)
            {
                int enemy2Index = Random.Range(0, enemyPrefabs.Count);
                Instantiate(enemyPrefabs[enemy2Index], spawnPos, enemyPrefabs[enemy2Index].transform.rotation);
            }

        }
    }

}
