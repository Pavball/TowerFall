using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationMenu : MonoBehaviour
{
    public Material[] materials;
    //public GameObject playerSkin;
    public int x;
    private int unlockedX;
    private int moneyToSpend;
    public int leftoverMoney;
    private int skinPrice;
    private int purValue;
    public Button purchase;
    public Image lockPng;
    public Image tickMark;
    public Text price;
    Renderer rend;
    public Text moneyNumber;
    public Text skinName;
    public Text skinDescription;
    public Text mainMenuMoney;
    public Text selectedSkin;
    public float speed = 0.5f;

    private AudioSource clickEffect;
    private AudioSource cashRegisterEffect;
    private GameManager gameManager;

    public Sprite sprite;
    public Material material;


    public bool[] boughtArray = new bool[] {true, false, false, false, false, false, false, false, false, false};
    List<string> listOfNames = new List<string> { "Default", "Dark Blue", "Pavball", "Emoji", "Glowing", "Bronze", "Silver", "Gold", "Block", "Rainbow"};
    List<string> listOfDescriptions = new List<string> { "Just the default skin!", "The author's favourite color!", "Pavball - The Logo Of Author's Gamer Tag!", "For The Memes!", "May This Guide You...!", "Being 3rd isn't so bad!", "Hey, you're getting there!", "Now you're the real winner!", "Seems kinda familiar...", "The Author Seemed It Looked Cool!" };
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (PlayerPrefs.GetInt("MoneySave", 0) == 1)
        {
            moneyNumber.text = "Money: " + PlayerPrefs.GetInt("MoneySave", 0).ToString() + " Bit";
        }
        else
        {
            moneyNumber.text = "Money: " + PlayerPrefs.GetInt("MoneySave", 0).ToString() + " Bits";
        }

        x = PlayerPrefs.GetInt("savedX", 0);
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[x];
        skinName.text = listOfNames[x];
        skinDescription.text = listOfDescriptions[x];
        clickEffect = GameObject.Find("ClickEffect").GetComponent<AudioSource>();
        cashRegisterEffect = GameObject.Find("CashRegister").GetComponent<AudioSource>();

        for (int i = 0; i < boughtArray.Length; i++)
        {
           boughtArray[i] = intToBool(PlayerPrefs.GetInt("Skin"+i, 0));
        }
        boughtArray[0] = true;
        CheckIfUnlocked(x);
        CheckIfSelected();
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("xValue", 0) != 9)
        {
            rend.sharedMaterial = materials[x];
        }
        else
        {
            RainbowSkin();
        }
    }
    //SETS THE NEXT SKIN
    public void NextSkin()
    {
        if (x < 9)
        {
            x++;
            clickEffect.Play();
            skinName.text = listOfNames[x];
            skinDescription.text = listOfDescriptions[x];
            CheckIfUnlocked(x);
            CheckIfSelected();
            PlayerPrefs.SetInt("xValue", x);
       
        }
        else
        {
            x = 0;
            clickEffect.Play();
            skinName.text = listOfNames[x];
            skinDescription.text = listOfDescriptions[x];
            CheckIfUnlocked(x);
            CheckIfSelected();
            PlayerPrefs.SetInt("xValue", x);  
        }
    }
    //SETS THE PREVIOUS SKIN
    public void PreviousSkin()
    {
        if (x > 0)
        {
            x--;
            clickEffect.Play();
            skinName.text = listOfNames[x];
            skinDescription.text = listOfDescriptions[x];
            CheckIfUnlocked(x);
            CheckIfSelected();
            PlayerPrefs.SetInt("xValue", x);

        }
        else
        {
            x = 9;
            clickEffect.Play();
            skinName.text = listOfNames[x];
            skinDescription.text = listOfDescriptions[x];
            CheckIfUnlocked(x);
            CheckIfSelected();
            PlayerPrefs.SetInt("xValue", x);
           
        }
    }

    public void RainbowSkin()
    {
        if (PlayerPrefs.GetInt("xValue", 0) == 9)
        {
            rend.sharedMaterial = materials[x];
            rend.material.SetColor("_Color", Color.HSVToRGB(Mathf.PingPong(Time.time * speed, 1), 1, 1));
        }
    }

    //FUNCTION TO PUT ON PURCHASE BUTTON
    public void PurchaseSkin()
    {
        purValue = PlayerPrefs.GetInt("xValue", 0);
        moneyToSpend = PlayerPrefs.GetInt("MoneySave", 0);
        if (moneyToSpend > skinPrice)
        {
            leftoverMoney = moneyToSpend - skinPrice;
            moneyNumber.text = "Money: " + leftoverMoney + " Bits";
            mainMenuMoney.text = "Money: " + leftoverMoney + " Bits";
            PlayerPrefs.SetInt("MoneySave", leftoverMoney);
            cashRegisterEffect.Play();
            if (boughtArray[purValue] == false)
            {
                boughtArray.SetValue(true, purValue);
                for (int i = 0; i < boughtArray.Length; i++)
                {
                    PlayerPrefs.SetInt("Skin" + i, boolToInt(boughtArray[i]));
                }
                for (int i = 0; i < boughtArray.Length; i++)
                {
                    boughtArray[i] = intToBool(PlayerPrefs.GetInt("Skin" + i, 0));
                }
                CheckIfUnlocked(purValue);
            }
        }
        else
        {
            return;
        }
    }

    public void GoBackCustomization()
    {
        x = PlayerPrefs.GetInt("savedX", 0);
        PlayerPrefs.SetInt("xValue", x);
        gameManager.titleScreen.SetActive(true);
        gameManager.customizationScreen.SetActive(false);
        gameManager.backEffect.Play();
    }

    //FUNCTION - CHECKS IF THE SKIN IS UNLOCKED OR IS IT LOCKED
    public void CheckIfUnlocked(int x)
    {
        if (boughtArray[x] == false)
        {
            GetPrice(x);
            purchase.gameObject.SetActive(true);
            lockPng.gameObject.SetActive(true);
            price.gameObject.SetActive(true);
        }
        else
        {
            purchase.gameObject.SetActive(false);
            lockPng.gameObject.SetActive(false);
            price.gameObject.SetActive(false);
        }

    }
    //CHECKS IF THE SKIN IS SELCTED OR NOT
    public void CheckIfSelected()
    {
        if(x == PlayerPrefs.GetInt("savedX", 0))
        {
            tickMark.gameObject.SetActive(true);
            selectedSkin.gameObject.SetActive(true);
        }
        else 
        {
            tickMark.gameObject.SetActive(false);
            selectedSkin.gameObject.SetActive(false);
        }
    }

    //FUNCTION THAT SETS THE PRICE OF A CERTAIN SKIN, IF THE SKIN IS BOUGHT MAKE THE STRING EMPTY
    public void GetPrice(int x)
    {
        switch (x)
        {
            //DEFAULT
            case int n when (n == 0 && boughtArray[x] == false):
                skinPrice = 0;
                price.text = "Price: " + skinPrice + " Bits";
                break;
            
            case int n when (n == 0 && boughtArray[x] == true):
                price.text = "";
                break;
            //DARK BLUE
            case int n when (n == 1 && boughtArray[x] == false):
                skinPrice = 50;
                price.text = "Price: " + skinPrice + " Bits";
                break;
            
            case int n when (n == 1 && boughtArray[x] == true):
                price.text = "";
                break;
            //PAVBALL LOGO
            case int n when (n == 2 && boughtArray[x] == false):
                skinPrice = 60;
                price.text = "Price: " + skinPrice + " Bits";
                break;
            
            case int n when (n == 2 && boughtArray[x] == true):
                price.text = "";
                break;
            //EMOJI
            case int n when (n == 3 && boughtArray[x] == false):
                skinPrice = 125;
                price.text = "Price: " + skinPrice + " Bits";
                break;
         
            case int n when (n == 3 && boughtArray[x] == true):
                price.text = "";
                break;
            //GLOWING
            case int n when (n == 4 && boughtArray[x] == false):
                skinPrice = 200;
                price.text = "Price: " + skinPrice + " Bits";
                break;

            case int n when (n == 4 && boughtArray[x] == true):
                price.text = "";
                break;
            //BRONZE
            case int n when (n == 5 && boughtArray[x] == false):
                skinPrice = 300;
                price.text = "Price: " + skinPrice + " Bits";
                break;

            case int n when (n == 5 && boughtArray[x] == true):
                price.text = "";
                break;
            //SILVER
            case int n when (n == 6 && boughtArray[x] == false):
                skinPrice = 450;
                price.text = "Price: " + skinPrice + " Bits";
                break;

            case int n when (n == 6 && boughtArray[x] == true):
                price.text = "";
                break;
            //GOLD
            case int n when (n == 7 && boughtArray[x] == false):
                skinPrice = 600;
                price.text = "Price: " + skinPrice + " Bits";
                break;

            case int n when (n == 7 && boughtArray[x] == true):
                price.text = "";
                break;
            //Block
            case int n when (n == 8 && boughtArray[x] == false):
                skinPrice = 800;
                price.text = "Price: " + skinPrice + " Bits";
                break;

            case int n when (n == 8 && boughtArray[x] == true):
                price.text = "";
                break;
            //RAINBOW
            case int n when (n == 9 && boughtArray[x] == false):
                skinPrice = 2500;
                price.text = "Price: " + skinPrice + " Bits";
                break;

            case int n when (n == 9 && boughtArray[x] == true):
                price.text = "";
                break;

            default:
                break;
        }
    }

    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}
