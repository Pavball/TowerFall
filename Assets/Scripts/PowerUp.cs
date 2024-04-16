using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using Pixelplacement;

public class PowerUp : MonoBehaviour
{
    //Calling scripts
    public GameManager gameManager;
    public JoystickPlayerMovement playerMov;
    public PlayerMovement playerKeyMov;
    private BulletManager bulletManager;
    //GUI
    public TextMeshProUGUI powerCountdown;
    public TextMeshProUGUI destroyEnemies;
    //GameObjects used to run code
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject shieldObject;
    private GameObject newShield;

    private AudioSource activateShield;
    private AudioSource deactivateShield;
    Coroutine co;
    //For scale changing  
    //Custom scale to scale enemy object
    private Vector3 scaleChange = new Vector3(-0.5f, -0.5f, -0.5f);
    //Bool to check if power is picked up
    public bool shieldActive = false;
    public bool enemyScale = false;
    public bool slowDown = false;
    public bool doublePoints = false;
    public bool shootEn = false;
    public PostProcessVolume volume = null;
    private Vignette vg = null;

    public GameObject shieldCollectParticles;
    public GameObject gameSlowParticles;
    public GameObject doublePointsParticles;
    public GameObject enemyShrinkParticles;
    public GameObject shootEnemyParticles;
    public GameObject playerObj;

    private AudioSource powerCollect;

    public float intensity = 0.625f;
    public float duration = 0.5f;

    void Start()
    {
        activateShield = GameObject.Find("ActivateShield").GetComponent<AudioSource>();
        deactivateShield = GameObject.Find("DeactivateShield").GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerMov = GameObject.Find("Player").GetComponent<JoystickPlayerMovement>();
        playerKeyMov = GameObject.Find("Player").GetComponent<PlayerMovement>();
        powerCollect = GameObject.Find("PowerCollect").GetComponent<AudioSource>();
        bulletManager = GameObject.Find("Game Manager").GetComponent<BulletManager>();
    }


    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        //PowerUp 1
        //If it picks up the shield powerup
        if (other.gameObject.CompareTag("PowerUp"))
        {

            //Destroy it and run ShieldPower();
            PowerUp1Particle();
            powerCollect.Play();
            shieldActive = true;
            Destroy(other.gameObject);

            ShieldPower();

        }

        //PowerUp 2
        //If it picks up the enemy scale powerup
        if (other.gameObject.CompareTag("PowerUp2"))
        {
            //Destroy it and run ScaleEnemy();
            PowerUp2Particle();
            powerCollect.Play();
            enemyScale = true;
            Destroy(other.gameObject);

            ScaleEnemy();

        }

        //PowerUp 3
        //When picked up, slows the speed down
        if (other.gameObject.CompareTag("PowerUp3"))
        {
            //Destroy it and run slowSpeed();
            PowerUp3Particle();
            powerCollect.Play();
            slowDown = true;
            Destroy(other.gameObject);

            slowSpeed();
        }

        //PowerUp 4
        //When picked up, gives double points for some time
        if (other.gameObject.CompareTag("PowerUp4"))
        {
            //Destroy it and run doublePoints();
            PowerUp4Particle();
            powerCollect.Play();
            doublePoints = true;
            Destroy(other.gameObject);

            doubleP();
        }

        if (other.gameObject.CompareTag("PowerUp5"))
        {
            //Destroy it and run doublePoints();
            PowerUp5Particle();
            powerCollect.Play();
            shootEn = true;
            Destroy(other.gameObject);
            bulletManager.StartPower();

            shootEnemy();
        }


    }
    //4 functions controlling what images and stuff goes on the screen
    void ShieldPower()
    {

        //Set image and countdown for specific powerUp
        gameManager.shieldPowerImage.gameObject.SetActive(true);
        powerCountdown.gameObject.SetActive(true);
        destroyEnemies.gameObject.SetActive(true);
        //Creates Shield object and activets it, then it follows players position
        activateShield.Play();
        newShield = Instantiate(shieldObject, playerMov.transform.position, playerMov.transform.rotation);
        newShield.gameObject.SetActive(true);
        newShield.transform.parent = playerMov.transform;


        //Start coroutine that runs a cooldown code
        co = StartCoroutine(PowerUpTimer1());
    }

    void ScaleEnemy()
    {
        //Set image and countdown for specific powerUp
        gameManager.enemyScaleImage.gameObject.SetActive(true);
        powerCountdown.gameObject.SetActive(true);


        //Start coroutine that runs a cooldown code
        co = StartCoroutine(PowerUpTimer2());
    }

    void slowSpeed()
    {
        gameManager.gameSlowdownImage.gameObject.SetActive(true);
        powerCountdown.gameObject.SetActive(true);
        volume.profile.TryGetSettings(out vg);
            
        FadeIn();
        Time.timeScale = 0.7f;
        playerMov.speed += 1.275f;
        playerKeyMov.speed += 3;

        co = StartCoroutine(PowerUpTimer3());
    }


    void doubleP()
    {
        gameManager.doublePointsImage.gameObject.SetActive(true);
        powerCountdown.gameObject.SetActive(true);

        co = StartCoroutine(PowerUpTimer4());
    }

    void shootEnemy()
    {
        gameManager.shootEnemyImage.gameObject.SetActive(true);
        powerCountdown.gameObject.SetActive(true);

        co = StartCoroutine(PowerUpTimer5());
    }
    //------------------------------------------------------------------
    IEnumerator PowerUpTimer1()
    {
        //Countdown 

        powerCountdown.text = "Remaining: 10";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 9";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 8";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 7";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 6";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 5";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 4";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 3";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 2";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 1";
        yield return WaitToPower();

        //Disabeling all power related 
        shieldActive = false;
        deactivateShield.Play();
        gameManager.shieldPowerImage.gameObject.SetActive(false);
        powerCountdown.gameObject.SetActive(false);
        destroyEnemies.gameObject.SetActive(false);
        Destroy(transform.Find("Shield(Clone)").gameObject);
        StopCoroutine(co);
    }

    IEnumerator PowerUpTimer2()
    {

        enemy.transform.localScale += scaleChange;
        enemy2.transform.localScale += scaleChange;

        //Countdown
            powerCountdown.text = "Remaining: 10";
            yield return WaitToPower();
            powerCountdown.text = "Remaining: 9";
            yield return WaitToPower();
            powerCountdown.text = "Remaining: 8";
             yield return WaitToPower();
            powerCountdown.text = "Remaining: 7";
            yield return WaitToPower();
            powerCountdown.text = "Remaining: 6";
            yield return WaitToPower();
            powerCountdown.text = "Remaining: 5";
            yield return WaitToPower();
            powerCountdown.text = "Remaining: 4";
            yield return WaitToPower();
            powerCountdown.text = "Remaining: 3";
            yield return WaitToPower();
            powerCountdown.text = "Remaining: 2";
            yield return WaitToPower();
            powerCountdown.text = "Remaining: 1";
            yield return WaitToPower();
        

        //Disabeling all power related
        enemyScale = false;
        enemy.transform.localScale -= scaleChange;
        enemy2.transform.localScale -= scaleChange;
        gameManager.enemyScaleImage.gameObject.SetActive(false);
        powerCountdown.gameObject.SetActive(false);
        StopCoroutine(co);
    }

    IEnumerator PowerUpTimer3()
    {

        //Countdown
        powerCountdown.text = "Remaining: 10";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 9";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 8";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 7";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 6";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 5";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 4";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 3";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 2";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 1";
        yield return WaitToPower();


        //Disabeling all power related
        slowDown = false;
        Time.timeScale = 1.0f;
        FadeOut();
        playerMov.speed -= 0.975f;
        playerKeyMov.speed -= 3;
        powerCountdown.gameObject.SetActive(false);
        gameManager.gameSlowdownImage.gameObject.SetActive(false);
        StopCoroutine(co);
    }
    IEnumerator PowerUpTimer4()
    {

       
        //Countdown
        powerCountdown.text = "Remaining: 10";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 9";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 8";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 7";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 6";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 5";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 4";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 3";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 2";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 1";
        yield return WaitToPower();


        //Disabeling all power related
        doublePoints = false;
        gameManager.doublePointsImage.gameObject.SetActive(false);
        powerCountdown.gameObject.SetActive(false);
        StopCoroutine(co);
    }

    IEnumerator PowerUpTimer5()
    {
        //Countdown
        powerCountdown.text = "Remaining: 10";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 9";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 8";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 7";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 6";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 5";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 4";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 3";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 2";
        yield return WaitToPower();
        powerCountdown.text = "Remaining: 1";
        yield return WaitToPower();


        //Disabeling all power related
        shootEn = false;
        gameManager.shootEnemyImage.gameObject.SetActive(false);
        powerCountdown.gameObject.SetActive(false);
        StopCoroutine(co);
    }
    //The real timer
    IEnumerator WaitToPower()
    {
        float start = Time.fixedUnscaledTime;
        while (Time.fixedUnscaledTime < start + 1f)
        {
            yield return 0;
        }
    }

    //4 functions that instantiate particle systems after collecting a PowerUp
    void PowerUp1Particle()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        GameObject enemyHit = (GameObject)Instantiate(shieldCollectParticles, playerObj.transform.position, Quaternion.identity);
        enemyHit.GetComponent<ParticleSystem>().Play();
        Destroy(enemyHit, 1.5f);
    }

    void PowerUp2Particle()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        GameObject enemyHit = (GameObject)Instantiate(enemyShrinkParticles, playerObj.transform.position, Quaternion.identity);
        enemyHit.GetComponent<ParticleSystem>().Play();
        Destroy(enemyHit, 1.5f);
    }

    void PowerUp3Particle()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        GameObject enemyHit = (GameObject)Instantiate(gameSlowParticles, playerObj.transform.position, Quaternion.identity);
        enemyHit.GetComponent<ParticleSystem>().Play();
        Destroy(enemyHit, 1.5f);
    }

    void PowerUp4Particle()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        GameObject enemyHit = (GameObject)Instantiate(doublePointsParticles, playerObj.transform.position, Quaternion.identity);
        enemyHit.GetComponent<ParticleSystem>().Play();
        Destroy(enemyHit, 1.5f);
    }

    void PowerUp5Particle()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        GameObject enemyHit = (GameObject)Instantiate(shootEnemyParticles, playerObj.transform.position, Quaternion.identity);
        enemyHit.GetComponent<ParticleSystem>().Play();
        Destroy(enemyHit, 1.5f);
    }


    public void FadeIn()
    {
        Tween.Value(0.25f, intensity, ApplyValue, duration, 0);
    }

    public void FadeOut()
    {
        Tween.Value(intensity, 0.25f, ApplyValue, duration, 0);
    }
    

    private void ApplyValue(float value)
    {
        vg.intensity.Override(value);
    }
}


