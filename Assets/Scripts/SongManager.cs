using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public AudioSource mainMenuSong;
    public AudioClip[] mainMenuclips;
    public AudioClip[] clips;
    public AudioSource audioSource;
    private int saveValue;

    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        mainMenuSong = GameObject.Find("MainMenuMusic").GetComponent<AudioSource>();
        audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = GetMainRandom();
        audioSource.Play();
    }

    private AudioClip GetMainRandom()
    {
        return mainMenuclips[Random.Range(0, mainMenuclips.Length)];
    }
    private AudioClip GetRandomClip()
    {
            return clips[Random.Range(0, clips.Length)]; 
    }

    void Update()
    {

        if (!audioSource.isPlaying && gameManager.isGameActive == true)
        {
           
                audioSource.clip = GetRandomClip();
                audioSource.Play();
           
        }

        if (!audioSource.isPlaying && gameManager.isGameActive == false && gameManager.gameHasEnded == false)
        {
            audioSource.clip = GetMainRandom();
            audioSource.Play();
        }

    }
}


