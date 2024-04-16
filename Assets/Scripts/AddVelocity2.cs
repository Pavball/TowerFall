using UnityEngine;

public class AddVelocity2 : MonoBehaviour
{
    public GameManager gameManager;
    private float rotateSpeed = 20.0f;
    public float scoreToNextSpeed = 110f;
    public float multiplier;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }



    // Update is called once per frame
    void Update()
    {
        rotate();
    }


    void rotate()
    {
        if (gameManager.score >= scoreToNextSpeed)
        {
            RotateSpeedIncrease();
        }

        transform.Rotate(0.0f, rotateSpeed * Time.deltaTime, 0.0f, Space.Self);
    }

    public void RotateSpeedIncrease()
    {
        multiplier += 20f;    // increases the value of multiplier by 1.
        rotateSpeed += multiplier;   // adds the multiplier value to basespeed.//calls the enemymovement method
        scoreToNextSpeed *= 1.5f;   // multiplies the score to next level by 2.
    }
}

