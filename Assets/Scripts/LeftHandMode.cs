using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftHandMode : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform rect;
    public Toggle toggle;
    public GameManager gameManager;
    void Start()
    {
        toggle.isOn = intToBool(PlayerPrefs.GetInt("ToggleState", 0));
        SetJoyPos();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void SetJoyPos()
    {
        if(gameManager.isGameActive == false)
        {
            rect.anchoredPosition = new Vector3(1200f, -250f, 0f);
        }
        else if(gameManager.isGameActive == true && toggle.isOn == true)
        {
            rect.anchoredPosition = new Vector3(-880f, -250f, 0f);
        }
        else if(gameManager.isGameActive == true && toggle.isOn == false)
        {
            rect.anchoredPosition = new Vector3(640f, -250f, 0f);
        }
    }

    public void SaveToggleMode()
    {
        if (gameManager.isGameActive == false && toggle.isOn == true)
        {
            PlayerPrefs.SetInt("ToggleState", boolToInt(toggle.isOn));
        }
        else if (gameManager.isGameActive == false && toggle.isOn == false)
        {
            PlayerPrefs.SetInt("ToggleState", boolToInt(toggle.isOn));
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
