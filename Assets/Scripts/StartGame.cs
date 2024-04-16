using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    private AudioSource source;
    void Start()
    {
        source = GameObject.Find("GameStartSound").GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(StartNow);
       
    }

    /* When a button is clicked, call the StartGame() method*/
    void StartNow()
    {
        Debug.Log(button.gameObject.name + " was clicked");
        
        //Lists and for-loops that keep track of gameobject appearing in the title screen, and destroys them all when the game starts
        List<GameObject> enemyToDestroy = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        List<GameObject> enemy2ToDestroy = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy2"));
        List<GameObject> coinToDestroy = new List<GameObject>(GameObject.FindGameObjectsWithTag("Coin"));
        for (int i = 0; i < enemyToDestroy.Count; i++)
        {
            Destroy(enemyToDestroy[i]);
        }

        for (int i = 0; i < enemy2ToDestroy.Count; i++)
        {
            Destroy(enemy2ToDestroy[i]);
        }

        for (int i = 0; i < coinToDestroy.Count; i++)
        {
            Destroy(coinToDestroy[i]);
        }

        source.Play();
        gameManager.StartGame();
    }



}