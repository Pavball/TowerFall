using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;

public class DetectCollision : MonoBehaviour
{
    private SoundEffectManager soundEffectManager;
    private SongManager songManager;
    private GameManager gameManager;
    private DamageHealth damageHealth;
    public HighScoreManager highScore;
    private JoystickPlayerMovement playerMov;
    private PowerUp powerUp;
    private AudioSource gameOverSound;
    public GameObject enemyHitParticles;
    public GameObject enemyHit2Particles;
    public GameObject coinHitParticles;
    public GameObject playerObj;
    

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerMov = GameObject.Find("Player").GetComponent<JoystickPlayerMovement>();
        powerUp = GameObject.Find("Player").GetComponent<PowerUp>();
        highScore = GameObject.Find("Game Manager").GetComponent<HighScoreManager>();
        songManager = GameObject.Find("Game Manager").GetComponent<SongManager>();
        soundEffectManager = GameObject.Find("Player").GetComponent<SoundEffectManager>();
        gameOverSound = GameObject.Find("GameOverSound").GetComponent<AudioSource>();
        damageHealth = GameObject.Find("Player").GetComponent<DamageHealth>();
        
    }

 
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
       
        //If coin is picked up gain 5 points
        if (collision.gameObject.CompareTag("Coin") && powerUp.doublePoints == false)
        {
            coinHit();
            Destroy(collision.gameObject);
            highScore.UpdateScore(5);
        }

        if (collision.gameObject.CompareTag("Coin") && powerUp.doublePoints == true)
        {
            coinHit();
            Destroy(collision.gameObject);
            highScore.UpdateScore(10);
        }

        if (collision.gameObject.CompareTag("Coin2") && powerUp.doublePoints == false)
        {
            coinHit();
            Destroy(collision.gameObject);
            highScore.UpdateScore(10);
        }

        if (collision.gameObject.CompareTag("Coin2") && powerUp.doublePoints == true)
        {
            coinHit();
            Destroy(collision.gameObject);
            highScore.UpdateScore(20);
        }

        //If enemy collides with player, player loses a life
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy2") && powerUp.shieldActive == false)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                EnemyHit();
                Destroy(collision.gameObject);
                CameraShaker.Instance.ShakeOnce(4f, 4f, .15f, 1.5f);
                playerMov.setLives -= 1;
                gameManager.livesText.text = "Lives: " + playerMov.setLives;
                damageHealth.PlayDamageFade();
            }

            if (collision.gameObject.CompareTag("Enemy2"))
            {
                EnemyHit2();
                Destroy(collision.gameObject);
                CameraShaker.Instance.ShakeOnce(4f, 4f, .15f, 1.5f);
                playerMov.setLives -= 1;
                gameManager.livesText.text = "Lives: " + playerMov.setLives;
                damageHealth.PlayDamageFade();
            }

            //If player is out of lives, destroy him and call game over.
            if (collision.gameObject.CompareTag("Enemy") && playerMov.setLives == 0)
            {
                damageHealth.PlayDamageFade();
                songManager.audioSource.Stop();
                soundEffectManager.audioSource.Stop();
                gameOverSound.Play();
                gameManager.playerObject.transform.position = Vector3.Lerp(gameManager.end.transform.position, gameManager.start.transform.position, Time.time);
                gameManager.myAnim.SetBool("playFadeIn", true);
                gameManager.GameOver();
            }

            //If player is out of lives, destroy him and call game over.
            if (collision.gameObject.CompareTag("Enemy2") && playerMov.setLives == 0)
            {
                damageHealth.PlayDamageFade();
                songManager.audioSource.Stop();
                soundEffectManager.audioSource.Stop();
                gameOverSound.Play();
                gameManager.playerObject.transform.position = Vector3.Lerp(gameManager.end.transform.position, gameManager.start.transform.position, Time.time);
                gameManager.myAnim.SetBool("playFadeIn", true);
                gameManager.GameOver();
            }
        }

  

     
    }

    void EnemyHit()
    {
        playerObj = GameObject.Find("Player");

        GameObject enemyHit = (GameObject)Instantiate(enemyHitParticles, playerObj.transform.position, Quaternion.identity);
        enemyHit.GetComponent<ParticleSystem>().Play();
        Destroy(enemyHit, 1.5f);
    }

    void EnemyHit2()
    {
        playerObj = GameObject.Find("Player");

        GameObject enemyHit2 = (GameObject)Instantiate(enemyHit2Particles, playerObj.transform.position, Quaternion.identity);
        enemyHit2.GetComponent<ParticleSystem>().Play();
        Destroy(enemyHit2, 1.5f);
    }

    void coinHit()
    {
        playerObj = GameObject.Find("Player");

        GameObject coinHit = (GameObject)Instantiate(coinHitParticles, playerObj.transform.position, Quaternion.identity);
        coinHit.GetComponent<ParticleSystem>().Play();
        Destroy(coinHit, 1.5f);
    }

}