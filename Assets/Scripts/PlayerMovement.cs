using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    private GameManager gameManager;
    
    //Input
    public float speed = 10.0f;
    private float horizontalInput;
    private float verticalInput;
    //Variable for boundry limit
    private float xRange = 9.0f;
    private float yRange = 7f;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

 
    void Update()
    {
        if (gameManager.isGameActive == true)
        {

            //Check if player is inside borders or touching them
            BorderCheck();

            //Player input value
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            //Player movement
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
            transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
          
        }
    }

    void BorderCheck()
    {
        //Checks if Player hits left boundray
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);

        }

        //Checks if Player hits right boundray
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        //Checks if Player hits down boundray
        if (transform.position.y < -yRange)
        {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);

        }

        //Checks if Player hits upper boundray
        if (transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }


    }

   

}
