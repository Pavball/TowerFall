using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private PowerUp powerUp;
 
    public AudioClip[] coinClips;
    public AudioClip[] shieldHits;
    public AudioClip[] enemyHits;
 

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        powerUp = GameObject.Find("Player").GetComponent<PowerUp>();
        audioSource = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnCollisionEnter(Collision collision)
    {

        //If coin is picked up gain 5 points
        if (collision.gameObject.CompareTag("Coin"))
        {
            audioSource.clip = GetRandomCoinClips();
            audioSource.Play();
        }

        if (collision.gameObject.CompareTag("Coin2"))
        {
            audioSource.clip = GetRandomCoinClips();
            audioSource.Play();
        }

        if (collision.gameObject.CompareTag("Enemy") && powerUp.shieldActive == false)
        {
            audioSource.clip = GetRandomEnemyHit();
            audioSource.Play();
        }


        if (collision.gameObject.CompareTag("Enemy") && powerUp.shieldActive == true)
        {
            audioSource.clip = GetRandomShieldHit();
            audioSource.Play();
        }

        if (collision.gameObject.CompareTag("Enemy2") && powerUp.shieldActive == false)
        {
            audioSource.clip = GetRandomEnemyHit();
            audioSource.Play();
        }


        if (collision.gameObject.CompareTag("Enemy2") && powerUp.shieldActive == true)
        {
            audioSource.clip = GetRandomShieldHit();
            audioSource.Play();
        }
      
    }

    public void Coin()
    {
        audioSource.clip = GetRandomCoinClips();
        audioSource.Play();
    }

    public void Enemy()
    {
        audioSource.clip = GetRandomShieldHit();
        audioSource.Play();
    }

    private AudioClip GetRandomShieldHit()
    {
        return shieldHits[Random.Range(0, shieldHits.Length)];
    }

    private AudioClip GetRandomEnemyHit()
    {
        return enemyHits[Random.Range(0, enemyHits.Length)];
    }

    private AudioClip GetRandomCoinClips()
    {
        return coinClips[Random.Range(0, coinClips.Length)];
    }



}
