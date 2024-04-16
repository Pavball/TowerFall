using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCoin : MonoBehaviour
{


    public GameObject coinHitParticles;
    public GameObject enemyObj;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Coin"))
        {
            Vector3 linePos = collision.transform.position;
            coinHit(linePos);
            Debug.Log("It Works!");
            Destroy(collision.gameObject);
        }
    }

    void coinHit(Vector3 linePos)
    {
  
        GameObject coinHit = (GameObject)Instantiate(coinHitParticles, linePos, Quaternion.identity);
        coinHit.GetComponent<ParticleSystem>().Play();
        Destroy(coinHit, 1.5f);
    }

   

}
