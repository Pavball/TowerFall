using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private GameManager gameManager;
    private PowerUp powerUp;
    public List<GameObject> bulletPrefab;
    public GameObject playerObj;
    //Variables set for spawning objects
    private float fireRate = 0.275f;
    private bool isInstantiated = true;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        powerUp = GameObject.Find("Player").GetComponent<PowerUp>();
    }


    void Update()
    {

    }

    public void StartPower()
    {
        StartCoroutine(SpawnPowerUp());
    }


    //Used for spawning power-ups with a timer
    public IEnumerator SpawnPowerUp()
    {
        while (gameManager.isGameActive == true && powerUp.shootEn == true)
        {

            yield return new WaitForSeconds(fireRate);
            playerObj = GameObject.FindGameObjectWithTag("Player");
            Vector3 spawnPos = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y - 0.75f, playerObj.transform.position.z);

            if (gameManager.isGameActive == true && powerUp.shootEn == true && isInstantiated)
            {
                isInstantiated = false;
                Instantiate(bulletPrefab[0], spawnPos, bulletPrefab[0].transform.rotation);
                Invoke("SetStatus", 0.1f);
            }
        }

    }

    public void SetStatus()
    {
        isInstantiated = true;
    }

}

