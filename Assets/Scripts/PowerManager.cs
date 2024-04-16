using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{ 
    private GameManager gameManager;
    public List<GameObject> powerPrefabs;
    //Variables set for spawning objects
    private float spawnPower = 15f;
    private float spawnRangeX = 9.0f;
    private bool isInstantiated = true;
    void Start()
    {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
          
    }


void Update()
{

}

    //Used for spawning power-ups with a timer
   public IEnumerator SpawnPowerUp()
    {
        while (gameManager.isGameActive == true)
        {
         
            if (gameManager.isGameActive == true)
            {

                yield return new WaitForSeconds(spawnPower);
                int powerIndex = Random.Range(0, 25); //get powerIndex and check switch case to spawn powerup!
                Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), -20, -1);

                switch (powerIndex)
                {
                    //If powerIndex is equal or lower than 5, then spawn Shield PowerUp
                    case int n when (n <= 5 && isInstantiated):
                        isInstantiated = false;
                        Instantiate(powerPrefabs[0], spawnPos, powerPrefabs[0].transform.rotation);
                        Debug.Log("The powerIndex is " + powerIndex);
                        Invoke("SetStatus", 0.1f);
                        break;

                    //If powerIndex is equal or lower than 10 and greater than 5 then spawn ScaleEnemyPowerUp
                    case int n when (n <= 10 && n > 5 && isInstantiated):
                        isInstantiated = false;
                        Instantiate(powerPrefabs[1], spawnPos, powerPrefabs[1].transform.rotation);
                        Debug.Log("The powerIndex is " + powerIndex);
                        Invoke("SetStatus", 0.1f);
                        break;

                    case int n when (n <= 15 && n > 10 && isInstantiated):
                        isInstantiated = false;
                        Instantiate(powerPrefabs[2], spawnPos, powerPrefabs[2].transform.rotation);
                        Debug.Log("The powerIndex is " + powerIndex);
                        Invoke("SetStatus", 0.1f);
                        break;

                    case int n when (n <= 20 && n > 15 && isInstantiated):
                        isInstantiated = false;
                        Instantiate(powerPrefabs[3], spawnPos, powerPrefabs[3].transform.rotation);
                        Debug.Log("The powerIndex is " + powerIndex);
                        Invoke("SetStatus", 0.1f);
                        break;

                    case int n when (n <= 25 && n > 20 && isInstantiated):
                        isInstantiated = false;
                        Instantiate(powerPrefabs[4], spawnPos, powerPrefabs[4].transform.rotation);
                        Debug.Log("The powerIndex is " + powerIndex);
                        Invoke("SetStatus", 0.1f);
                        break;
                    default:
                        break;
                }
            }
        }

    }

    public void SetStatus()
    {
        isInstantiated = true;
    }

}
