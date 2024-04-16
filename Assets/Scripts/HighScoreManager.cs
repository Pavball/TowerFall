using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public Text highScoreNumber;
    public TextMeshProUGUI highScore;
    private GameManager gameManager;
    private int counter = 0;

    public GameObject highScoreParticles;
    public GameObject playerObj;

    private AudioSource highScoreEffect;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //Sets default value of the score
        highScoreNumber.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        highScoreEffect = GameObject.Find("HighScoreEffect").GetComponent<AudioSource>();
    }

    // Update score with value from coin collected
    public void UpdateScore(int scoreToAdd)
    {
        
            gameManager.score += scoreToAdd;
            gameManager.scoreText.text = "Score: " + gameManager.score;
            gameManager.finalScoreText.text = "Final score was: " + gameManager.score;
      
        //Saves the score as highscore
        if (gameManager.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            counter++;
            PlayerPrefs.SetInt("HighScore", gameManager.score);
            highScoreNumber.text = gameManager.score.ToString();
            highScore.text = gameManager.score.ToString();
           if(counter == 1)
            {
                highScoreParticle();
                highScoreEffect.Play();
            }
        }
    }

    void highScoreParticle()
    {
        playerObj = GameObject.Find("Player");

        GameObject highScore = (GameObject)Instantiate(highScoreParticles, playerObj.transform.position, Quaternion.identity);
        highScore.GetComponent<ParticleSystem>().Play();
        Destroy(highScore, 1.5f);
    }
}
