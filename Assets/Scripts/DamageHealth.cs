using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageHealth : MonoBehaviour
{
    public Animator damageFade;
    public GameObject damageObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayDamageFade()
    {

        StartCoroutine(AnimationTimer());

    }

    IEnumerator AnimationTimer()
    {
        damageObject.SetActive(true);
        damageFade.SetBool("damageFade", true);
        yield return WaitToDamage();
        damageFade.SetBool("damageFade", false);
        damageObject.SetActive(false);
    }

    IEnumerator WaitToDamage()
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + 1f)
        {
            yield return 0;
        }
    }
}
