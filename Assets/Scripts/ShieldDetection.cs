using UnityEngine;
using EZCameraShake;

public class ShieldDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    private HighScoreManager highScore;
    private PowerUp powerUp;
   
    public GameObject enemyHitParticles;
    public GameObject enemyHit2Particles;
    public GameObject coinHitParticles;
    public GameObject shieldObj;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        powerUp = GameObject.Find("Player").GetComponent<PowerUp>();
        highScore = GameObject.Find("Game Manager").GetComponent<HighScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        //If coin is picked up gain 5 points
        if (collision.gameObject.CompareTag("Coin") && powerUp.shieldActive == true)
        {
            coinHit();
            Destroy(collision.gameObject);
            highScore.UpdateScore(5);
        }

        if (collision.gameObject.CompareTag("Coin2") && powerUp.shieldActive == true)
        {
            coinHit();
            Destroy(collision.gameObject);
            highScore.UpdateScore(10);
        }

        //If enemy collides with a player that has a shield, just destroy the enemy
        if (collision.gameObject.CompareTag("Enemy") && powerUp.shieldActive == true)
        {
            EnemyHit();
            Destroy(collision.gameObject);
            CameraShaker.Instance.ShakeOnce(3.3f, 3.3f, .08f, 1.5f);
            highScore.UpdateScore(8);
        }

        if (collision.gameObject.CompareTag("Enemy2") && powerUp.shieldActive == true)
        {
            EnemyHit2();
            Destroy(collision.gameObject);
            CameraShaker.Instance.ShakeOnce(3.3f, 3.3f, .08f, 1.5f);
            highScore.UpdateScore(8);
        }


    }

    void EnemyHit()
    {
        shieldObj = GameObject.FindGameObjectWithTag("Shield");

        GameObject enemyHit = (GameObject)Instantiate(enemyHitParticles, shieldObj.transform.position, Quaternion.identity);
        enemyHit.GetComponent<ParticleSystem>().Play();
        Destroy(enemyHit, 1.5f);
    }

    void EnemyHit2()
    {
        shieldObj = GameObject.FindGameObjectWithTag("Shield");

        GameObject enemyHit2 = (GameObject)Instantiate(enemyHit2Particles, shieldObj.transform.position, Quaternion.identity);
        enemyHit2.GetComponent<ParticleSystem>().Play();
        Destroy(enemyHit2, 1.5f);
    }

    void coinHit()
    {
        shieldObj = GameObject.FindGameObjectWithTag("Shield");

        GameObject coinHit = (GameObject)Instantiate(coinHitParticles, shieldObj.transform.position, Quaternion.identity);
        coinHit.GetComponent<ParticleSystem>().Play();
        Destroy(coinHit, 1.5f);
    }


}
