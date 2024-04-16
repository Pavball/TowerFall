using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;

public class PauseMenu : MonoBehaviour
{
    public Button backToMenu;
    public TextMeshProUGUI areSureText;
    public TextMeshProUGUI displayCountdown;
    public TextMeshProUGUI gamePaused;
    public Text pauseButtonCooldown;
    public Button areSure;
    public Button backTo;
   

    public Button resume;
    public Button options;

    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject enemy;
    public GameObject enemy2;

    private AudioSource backEffect; 
    private AudioSource clickEffect;
    private AudioSource countdownEffect;

    private SongManager songManager;
    private SettingsMenu settingsMenu;
    public bool isPaused = false;

    public Slider volumeSlider;
    public Slider effectSlider;
    public Slider volumeGameSlider;
    public Slider effectGameSlider;

    void Start()
    {
        pauseMenu.SetActive(false);
        backEffect = GameObject.Find("BackEffect").GetComponent<AudioSource>();
        clickEffect = GameObject.Find("ClickEffect").GetComponent<AudioSource>();
        countdownEffect = GameObject.Find("SecondsCountdown").GetComponent<AudioSource>();
        songManager = GameObject.Find("Game Manager").GetComponent<SongManager>();
        settingsMenu = GameObject.Find("Canvas").GetComponent<SettingsMenu>();

    }

    
    void Update()
    {
       
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
        songManager.audioSource.mute = true;
        pauseButton.gameObject.SetActive(false);
        Time.timeScale = 0f;
        clickEffect.Play();
    }


    IEnumerator ResumeGame()
    {
      
        backToMenu.gameObject.SetActive(false);
        resume.gameObject.SetActive(false);
        options.gameObject.SetActive(false);

        countdownEffect.Play();
        displayCountdown.text = "3";
        yield return WaitToResumeGame();

        displayCountdown.text = "2";
        yield return WaitToResumeGame();

        displayCountdown.text = "1";
        yield return WaitToResumeGame();

        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;


        backToMenu.gameObject.SetActive(true);
        resume.gameObject.SetActive(true);
        options.gameObject.SetActive(true);

        displayCountdown.gameObject.SetActive(false);
        songManager.audioSource.mute = false;
    }

    IEnumerator WaitToResumeGame()
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + 1f)
        {
            yield return 0;
        }
    }
    IEnumerator PauseButton()
    {

        pauseButtonCooldown.gameObject.SetActive(true);
        
        yield return WaitToGiveButton();
        
        yield return WaitToGiveButton();
       
        yield return WaitToGiveButton();
       
        pauseButtonCooldown.text = "3";
        yield return WaitToGiveButton();
        pauseButtonCooldown.text = "2";
        yield return WaitToGiveButton();
        pauseButtonCooldown.text = "1";
        yield return WaitToGiveButton();

        pauseButtonCooldown.gameObject.SetActive(false);
        pauseButtonCooldown.text = " ";
        pauseButton.gameObject.SetActive(true);


    }
  

    IEnumerator WaitToGiveButton()
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + 1f)
        {
            yield return 0;
        }
    }

    public void Resume()
    {
        StartCoroutine(ResumeGame());
        displayCountdown.gameObject.SetActive(true);
        isPaused = false;
   
        StartCoroutine(PauseButton());
      
    }

    public void Options()
    {
        backToMenu.gameObject.SetActive(false);
        resume.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        gamePaused.gameObject.SetActive(false);

        optionsMenu.gameObject.SetActive(true);
        clickEffect.Play();
    }

    public void GoBackPause()
    {
        SaveSoundSettings();
        optionsMenu.gameObject.SetActive(false);
        backToMenu.gameObject.SetActive(true);
        resume.gameObject.SetActive(true);
        options.gameObject.SetActive(true);
        gamePaused.gameObject.SetActive(true);
        backEffect.Play();
 
    }

    public void AreSure()
    {
        areSureText.gameObject.SetActive(true);
        areSure.gameObject.SetActive(true);
        backTo.gameObject.SetActive(true);
        backToMenu.gameObject.SetActive(false);
        resume.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        clickEffect.Play();
       
    }

    public void Back()
    {
        areSureText.gameObject.SetActive(false);
        areSure.gameObject.SetActive(false);
        backTo.gameObject.SetActive(false);
        backToMenu.gameObject.SetActive(true);
        resume.gameObject.SetActive(true);
        options.gameObject.SetActive(true);
        backEffect.Play();
    }
    public void BackToMenu()
    {
    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        enemy.transform.localScale = new Vector3(1.3f, 1.05f, 1.05f);
        enemy2.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", volumeGameSlider.value);
        PlayerPrefs.SetFloat("EffectVolume", effectGameSlider.value);
    }



    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSoundSettings();
        }
    }


}
