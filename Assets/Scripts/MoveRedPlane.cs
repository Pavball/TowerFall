using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRedPlane : MonoBehaviour
{

    public GameManager gameManager;
    private float speed = 15.0f;
    private Vector3 targetPosition = new Vector3(0, 35, 0);
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        


        if(gameManager.isGameActive == true)
        {

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

 


    }
}
