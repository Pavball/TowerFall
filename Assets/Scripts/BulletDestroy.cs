using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    // Start is called before the first frame update

    private float topBound = 17.2f;
    private float downBound = -9f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //If projectile goes outside of players view, destroy it.
        if (transform.position.y > topBound)
        {

            Destroy(gameObject);
        }

        if (transform.position.y < downBound)
        {

            Destroy(gameObject);
        }


    }
}
