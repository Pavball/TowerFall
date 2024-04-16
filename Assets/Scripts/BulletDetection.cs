using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    private HighScoreManager highScore;
    private PowerUp powerUp;
    private SoundEffectManager soundEffect;

    public AudioSource audioSource;
    public GameObject enemyHitParticles;
    public GameObject enemyHit2Particles;
    public GameObject coinHitParticles;
    public GameObject bulletObj;
    public GameObject player;
    private Vector3 enemyPos;
    private Vector3 coinPos;
    private Vector3 enemy2Pos;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        powerUp = GameObject.Find("Player").GetComponent<PowerUp>();
        highScore = GameObject.Find("Game Manager").GetComponent<HighScoreManager>();
        soundEffect = GameObject.Find("Player").GetComponent<SoundEffectManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        //If coin is picked up gain 5 points
        if (collision.gameObject.CompareTag("Coin") && powerUp.shootEn == true)
        {
            soundEffect.Coin();
            coinPos = collision.transform.position;
            coinHit();
            Destroy(collision.gameObject);
            highScore.UpdateScore(5);
        }
        if (collision.gameObject.CompareTag("Coin2") && powerUp.shootEn == true)
        {
            soundEffect.Coin();
            coinPos = collision.transform.position;
            coinHit();
            Destroy(collision.gameObject);
            highScore.UpdateScore(10);
        }

        //If enemy collides with a player that has a shield, just destroy the enemy
        if (collision.gameObject.CompareTag("Enemy") && powerUp.shootEn == true)
        {
            soundEffect.Enemy();
            enemyPos = collision.transform.position;
            EnemyHit();
            Destroy(collision.gameObject);
            highScore.UpdateScore(8);
        }

        if (collision.gameObject.CompareTag("Enemy2") && powerUp.shootEn == true)
        {
            soundEffect.Enemy();
            enemy2Pos = collision.transform.position;
            EnemyHit2();
            Destroy(collision.gameObject);
            highScore.UpdateScore(8);
        }


    }

    void EnemyHit()
    {

        GameObject enemyHit = (GameObject)Instantiate(enemyHitParticles, enemyPos, Quaternion.identity);
        enemyHit.GetComponent<ParticleSystem>().Play();
        Destroy(enemyHit, 1.5f);
    }

    void EnemyHit2()
    {
    
        GameObject enemyHit2 = (GameObject)Instantiate(enemyHit2Particles, enemy2Pos, Quaternion.identity);
        enemyHit2.GetComponent<ParticleSystem>().Play();
        Destroy(enemyHit2, 1.5f);
    }

    void coinHit()
    {
        bulletObj = GameObject.FindGameObjectWithTag("Bullet");

        GameObject coinHit = (GameObject)Instantiate(coinHitParticles, coinPos, Quaternion.identity);
        coinHit.GetComponent<ParticleSystem>().Play();
        Destroy(coinHit, 1.5f);
    }

}
