using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMovement : MonoBehaviour
{
  
    private float speed = 5.0f;
    void Start()
    {

    }


    void Update()
    {
        //Projectile movement
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
