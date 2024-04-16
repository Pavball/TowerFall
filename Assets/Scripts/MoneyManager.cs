using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    private JoystickPlayerMovement playerMov;
    private GameManager gameManager;
    public Text moneyNumber;
    public TextMeshProUGUI inGameMoney;
    public int br = 0;
    public int moneyInt = 0;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerMov = GameObject.Find("Player").GetComponent<JoystickPlayerMovement>();

        if (PlayerPrefs.GetInt("MoneySave", 0) == 1)
        {
            moneyNumber.text = "Money: "+PlayerPrefs.GetInt("MoneySave", 0).ToString() + " Bit";
        }
        else 
        { 
            moneyNumber.text = "Money: "+PlayerPrefs.GetInt("MoneySave", 0).ToString() + " Bits"; 
        }
    }

    void Update()
    {
      if(gameManager.isGameActive == false && playerMov.setLives == 0 && br == 0)
        {
            StartCoroutine(Calculate());
        }
      
    }

    public void MoneyManage()
    {
        if(gameManager.gameHasEnded == true)
        {
            int savedMoney = PlayerPrefs.GetInt("MoneySave", 0);
            int moneyToSave = savedMoney + moneyInt;
            PlayerPrefs.SetInt("MoneySave", moneyToSave);
        }
    }

    public void CalculateAd()
    {
        StartCoroutine(Calculate());
    }

    IEnumerator Calculate()
    {
        br++;
        float money =(float)gameManager.score / 10;
        moneyInt = (int)money;
        WaitToStart();
        if (moneyInt == 1)
        {
            inGameMoney.text = "You earned: "+moneyInt.ToString() + " Bit";
        }
        else
        {
            for (int i = 1; i <= moneyInt; i++)
            {
                inGameMoney.text = "You earned: " + i.ToString() + " Bits";
                yield return WaitToCalc();
            }
        }
    }

    IEnumerator WaitToStart()
    {
        float start = Time.fixedUnscaledTime;
        while (Time.fixedUnscaledTime < start + 1f)
        {
            yield return 0;
        }
    }

        IEnumerator WaitToCalc()
        {
            float start = Time.fixedUnscaledTime;
            while (Time.fixedUnscaledTime < start + 0.1f)
            {
                yield return 0;
            }
        }
    public void SaveMoney()
    {
        int savedMoney = PlayerPrefs.GetInt("MoneySave", 0);
        int moneyToSave = savedMoney + moneyInt;
        PlayerPrefs.SetInt("MoneySave", moneyToSave);
    }



    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveMoney();
        }
    }
}
