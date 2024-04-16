using UnityEngine;

public class Enemy2Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    public MainMenuMov mainMov;
    public float scoreToNextSpeed = 110f;
    public float speed = 10.0f;
    private float enemySpeed;
    public float multiplier;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        mainMov = GameObject.Find("Game Manager").GetComponent<MainMenuMov>();
        if (gameManager.isGameActive == false)
        {
            enemySpeed = Random.Range(10, 120);
            transform.Translate(Vector3.up * Time.deltaTime * enemySpeed);
        }


    }

    // Update is called once per frame

    void Update()
    {

        if (gameManager.score >= scoreToNextSpeed)
        {
            EnemySpeedIncrease();
        }

        //Projectile movement

        transform.Translate(Vector3.up * Time.deltaTime * speed);

    }


    public void EnemySpeedIncrease()
    {
        multiplier += 0.5f;    // increases the value of multiplier by 1.
        speed += multiplier;   // adds the multiplier value to basespeed.//calls the enemymovement method
        scoreToNextSpeed *= 1.5f; // multiplies the score to next level by 2.

    }

}
