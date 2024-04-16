using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    public float speed = 7.0f;
    public float scoreToNextSpeed = 10f;
    public float multiplier;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (gameManager.isGameActive == false)
        {
            float coinSpeed = Random.Range(10, 120);
            transform.Translate(Vector3.up * Time.deltaTime * coinSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
            if (gameManager.score >= scoreToNextSpeed)
            {
                CoinSpeedIncrease();
            }

            //Projectile movement
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        
    }


    public void CoinSpeedIncrease()
    {
        multiplier += 0.15f;    // increases the value of multiplier by 1.
        speed += multiplier;   // adds the multiplier value to basespeed.//calls the enemymovement method
        scoreToNextSpeed *= 1.5f;   // multiplies the score to next level by 2.
    }
}