using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //TextUGUI for user to see
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI finalScoreText;
    public Animator myAnim;
    private AudioSource clickEffect;
    public AudioSource backEffect;

    public GameObject doubleRewardButton;
    public TextMeshProUGUI extraScoreText;

    public GameObject extraLifeButton;
    public TextMeshProUGUI extraLifeText;

    public TextMeshProUGUI moneyMadeText;
    //Images for powerUps
    public TextMeshProUGUI powerUpText;
    public Image keyImage;
    public RawImage enemyScaleImage;
    public RawImage doublePointsImage;
    public GameObject gameSlowdownImage;
    public GameObject shieldPowerImage;
    public GameObject shootEnemyImage;
    //Enemy gameobject
    public GameObject enemy;
    public GameObject enemy2;
    //Title screen
    public GameObject titleScreen;
    public GameObject gameScreen = null;
    public GameObject gameOverScreen = null;
    public GameObject highScoreScreen;
    public GameObject customizationScreen;
    public GameObject descriptionScreen;
    public GameObject optionsScreen;
    public Button restartButton;
    public Button pauseButton;
    public GameObject playerObject;
    public GameObject redPlane;
    public GameObject start;
    public GameObject end;
    //Calling a class for some variables
    private JoystickPlayerMovement playerMov;
    private PowerUp powerUp;
    private EnemyManager enemyManager;
    private PowerManager powerManager;
    private EnemyMovement enemyMovement;
    private HighScoreManager highScore;
    private SongManager songManager;
    private MoneyManager moneyManager;
    private LeftHandMode leftHand;

    public int numberOfDeaths = 0;
    //Variables for spawning targets
    public int score = 0;
    public bool isGameActive = false;
    public bool gameHasEnded = false;
    Coroutine po;
    Coroutine en;
    //Music

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        myAnim.SetBool("playFadeIn", true);
        songManager = GameObject.Find("Game Manager").GetComponent<SongManager>();
        playerMov = GameObject.Find("Player").GetComponent<JoystickPlayerMovement>();
        powerUp = GameObject.Find("Game Manager").GetComponent<PowerUp>();
        enemyManager = GameObject.Find("Game Manager").GetComponent<EnemyManager>();
        powerManager = GameObject.Find("Game Manager").GetComponent<PowerManager>();
        enemyMovement = GameObject.Find("Game Manager").GetComponent<EnemyMovement>();
        highScore = GameObject.Find("Game Manager").GetComponent<HighScoreManager>();
        clickEffect = GameObject.Find("ClickEffect").GetComponent<AudioSource>();
        backEffect = GameObject.Find("BackEffect").GetComponent<AudioSource>();
        moneyManager = GameObject.Find("Game Manager").GetComponent<MoneyManager>();
        leftHand = GameObject.Find("Variable Joystick").GetComponent<LeftHandMode>();
    }

    void Update()
    {

    }

    // Start the game, remove title screen, reset score, and setup the game for the player
    public void StartGame()
    {
        songManager.audioSource.Stop();
        isGameActive = true;
        enemy.transform.localScale = new Vector3(1.3f, 1.05f, 1.05f);
        enemy2.transform.localScale = new Vector3(1f, 1f, 1f);
        playerMov.speed += 2.75f;
        titleScreen.SetActive(false);
        playerObject.SetActive(true);
        playerObject.transform.position = Vector3.Lerp(start.transform.position, end.transform.position, Time.time);
        gameScreen.SetActive(true);
        highScoreScreen.SetActive(true);
        livesText.text = "Lives: " + playerMov.setLives;
        scoreText.text = "Score: " + score;
        en = StartCoroutine(enemyManager.SpawnTarget());
        po = StartCoroutine(powerManager.SpawnPowerUp());
        myAnim.SetBool("playFadeIn", false);
        leftHand.SetJoyPos();
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        numberOfDeaths++;
        if (numberOfDeaths == 2)
        {
            doubleRewardButton.gameObject.SetActive(true);
            extraScoreText.gameObject.SetActive(true);
            extraLifeButton.gameObject.SetActive(false);
            extraLifeText.gameObject.SetActive(false);
        }
        gameHasEnded = true;
        Time.timeScale = 1.0f;
        gameOverScreen.SetActive(true);
        finalScoreText.text = "Final score was: " + score;
        gameScreen.SetActive(false);
        isGameActive = false;
        keyImage.gameObject.SetActive(false);
        enemyScaleImage.gameObject.SetActive(false);
        doublePointsImage.gameObject.SetActive(false);
        powerUp.powerCountdown.gameObject.SetActive(false);
        highScoreScreen.SetActive(false);
        leftHand.SetJoyPos();
        StopCoroutine(en);
        StopCoroutine(po);   
    }

    public void StartGameAfterDeath()
    {
        if (gameOverScreen != null)
        {
            gameHasEnded = false;
            songManager.audioSource.Stop();
            isGameActive = true;
            enemy.transform.localScale = new Vector3(1.3f, 1.05f, 1.05f);
            enemy2.transform.localScale = new Vector3(1f, 1f, 1f);
            gameOverScreen.SetActive(false);
            gameScreen.SetActive(true);
            highScoreScreen.SetActive(true);
            livesText.text = "Lives: " + playerMov.setLives;
            scoreText.text = "Score: " + score;
            playerObject.transform.position = Vector3.Lerp(start.transform.position, end.transform.position, Time.time);
            en = StartCoroutine(enemyManager.SpawnTarget());
            po = StartCoroutine(powerManager.SpawnPowerUp());
            myAnim.SetBool("playFadeIn", false);
            moneyManager.br = 0;
            leftHand.SetJoyPos();
        }
    }





    // Restart game by reloading the scene
    public void RestartGame()
    {
        gameHasEnded = true;
        moneyManager.MoneyManage();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        enemy.transform.localScale = new Vector3(1.3f, 1.05f, 1.05f);
        enemy2.transform.localScale = new Vector3(1f, 1f, 1f);
        myAnim.SetBool("playFadeIn", false);
    }

    public void Description()
    {
        titleScreen.SetActive(false);
        descriptionScreen.SetActive(true);
        clickEffect.Play();
    }

    public void Options()
    {
        titleScreen.SetActive(false);
        optionsScreen.SetActive(true);
        clickEffect.Play();
    }

    public void Customization()
    {
        titleScreen.SetActive(false);
        customizationScreen.SetActive(true);
        clickEffect.Play();
    }

    public void GoBackDescription()
    {
        titleScreen.SetActive(true);
        descriptionScreen.SetActive(false);
        backEffect.Play();
    }

    public void GoBackCustomization()
    {
        titleScreen.SetActive(true);
        customizationScreen.SetActive(false);
        backEffect.Play();
    }

    public void GoBackOptions()
    {
        titleScreen.SetActive(true);
        optionsScreen.SetActive(false);
        leftHand.SaveToggleMode();
        backEffect.Play();
    }

    IEnumerator AnimationTimer()
    {     
        yield return WaitToDamage();
        yield return WaitToDamage();
        myAnim.SetBool("playFadeIn", false);
    }

    IEnumerator WaitToDamage()
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + 1f)
        {
            yield return 0;
        }
    }

}


