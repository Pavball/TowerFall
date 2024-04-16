using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private JoystickPlayerMovement playerMov;
    private GameManager gameManager;
    private MoneyManager money;

    private string playStoreID = "3969149";
    private string appStoreID = "3969148";

    private string rewardedVideoAd = "rewardedVideo";

    public bool isTargetPlayStore;
    public bool isTestAd;
    private int numberOfAds = 0;
    private void Start()
    {
        Advertisement.AddListener(this);
        InitializeAdvertisement();
        playerMov = GameObject.Find("Player").GetComponent<JoystickPlayerMovement>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        money = GameObject.Find("Game Manager").GetComponent<MoneyManager>();
    }

    private void InitializeAdvertisement()
    {
        if (isTargetPlayStore)
        {
            Advertisement.Initialize(playStoreID, isTestAd);
            return;
        }

        Advertisement.Initialize(appStoreID, isTestAd);

    }

    public void PlayRewardedVideoAd()
    {
        if (!Advertisement.IsReady(rewardedVideoAd))
        {
            return;
        }
        else if(numberOfAds == 1)
        {
            return;
        }
        else 
        Advertisement.Show(rewardedVideoAd);
    }

    public void PlayRewardedVideoAd2()
    {
        if (!Advertisement.IsReady(rewardedVideoAd))
        {
            return;
        }
        else if (numberOfAds == 2)
        {
            return;
        }
        else
            Advertisement.Show(rewardedVideoAd);
    }

    public void OnUnityAdsReady(string placementId)
    {
       // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
      
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                if (placementId == rewardedVideoAd && numberOfAds == 0)
                {
                    numberOfAds++;
                    playerMov.setLives = 3;
                    StartNow();
                    gameManager.StartGameAfterDeath();

                }
                else if(placementId == rewardedVideoAd && numberOfAds == 1)
                {
                    numberOfAds++;
                    money.moneyInt = money.moneyInt * 2;
                    gameManager.moneyMadeText.gameObject.SetActive(true);
                    gameManager.moneyMadeText.text = "Doubled Money: " + money.moneyInt + " Bits";
                }
                break;

            case ShowResult.Skipped:
                break;

            case ShowResult.Finished:
                if (placementId == rewardedVideoAd && numberOfAds == 0)
                {
                    numberOfAds++;
                    playerMov.setLives = 3;
                    StartNow();
                    gameManager.StartGameAfterDeath();
                }
                else if (placementId == rewardedVideoAd && numberOfAds == 1)
                {
                    numberOfAds++;
                    money.moneyInt = money.moneyInt * 2;
                    gameManager.moneyMadeText.gameObject.SetActive(true);
                    gameManager.moneyMadeText.text = "Doubled Money: " + money.moneyInt + " Bits";
                }
                break;

        }
    }

    public void StartNow()
    {

        //Lists and for-loops that keep track of gameobject appearing in the title screen, and destroys them all when the game starts
        List<GameObject> enemyToDestroy = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        List<GameObject> coinToDestroy = new List<GameObject>(GameObject.FindGameObjectsWithTag("Coin"));
        List<GameObject> powerUpToDestroy = new List<GameObject>(GameObject.FindGameObjectsWithTag("PowerUp"));
        List<GameObject> powerUp2ToDestroy = new List<GameObject>(GameObject.FindGameObjectsWithTag("PowerUp2"));
        List<GameObject> powerUp3ToDestroy = new List<GameObject>(GameObject.FindGameObjectsWithTag("PowerUp3"));
        List<GameObject> powerUp4ToDestroy = new List<GameObject>(GameObject.FindGameObjectsWithTag("PowerUp4"));

        for (int i = 0; i < enemyToDestroy.Count; i++)
        {
            Destroy(enemyToDestroy[i]);
        }

        for (int i = 0; i < coinToDestroy.Count; i++)
        {
            Destroy(coinToDestroy[i]);
        }

        for (int i = 0; i < coinToDestroy.Count; i++)
        {
            Destroy(coinToDestroy[i]);
        }

        for (int i = 0; i < powerUpToDestroy.Count; i++)
        {
            Destroy(powerUpToDestroy[i]);
        }

        for (int i = 0; i < powerUp2ToDestroy.Count; i++)
        {
            Destroy(powerUp2ToDestroy[i]);
        }

        for (int i = 0; i < powerUp3ToDestroy.Count; i++)
        {
            Destroy(powerUp3ToDestroy[i]);
        }

        for (int i = 0; i < powerUp4ToDestroy.Count; i++)
        {
            Destroy(powerUp4ToDestroy[i]);
        }

    }
}
