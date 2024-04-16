using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerSkin : MonoBehaviour
{
    private CustomizationMenu customizationMenu;
    public Material[] materials;
    //public GameObject playerSkin;
    public int value;
    public int xUnlocked;
    Renderer rend;
    public Image tickMark;
    public Text selectedSkin;
    public float speed = 0.4f;
    void Start()
    {
        value = PlayerPrefs.GetInt("savedX", 0); 
        customizationMenu = GameObject.Find("Game Manager").GetComponent<CustomizationMenu>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[value];
    }

    // Update is called once per frame
    void Update()
    {
       if (PlayerPrefs.GetInt("xValue", 0) != 9)
        {
                rend.sharedMaterial = materials[value];
            }
        else
        {
            RainbowSkin();
        }
    }

    public void SetSkin()
    {
        value = PlayerPrefs.GetInt("xValue", 0); 
        xUnlocked = PlayerPrefs.GetInt("xValue", 0);
        PlayerPrefs.SetInt("savedX", xUnlocked);
        tickMark.gameObject.SetActive(true);
        selectedSkin.gameObject.SetActive(true);
    }

    public void RainbowSkin()
    {
        if (PlayerPrefs.GetInt("xValue", 0) == 9)
        {
            rend.material.SetColor("_Color", Color.HSVToRGB(Mathf.PingPong(Time.time * speed, 1), 1, 1));
        }
    }
}
