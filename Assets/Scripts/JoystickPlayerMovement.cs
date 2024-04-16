using UnityEngine;

public class JoystickPlayerMovement : MonoBehaviour
{

    private GameManager gameManager;


    public float speed;
    private float xRange = 9.0f;
    private float yRange = 7f;
    public VariableJoystick joystick;
    public int setLives = 3;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.isGameActive == true)
        {
            BorderCheck();

            Vector3 directionX = Vector3.right * joystick.Horizontal;
            Vector3 directionY = Vector3.up * joystick.Vertical;

            transform.Translate(directionX * speed * Time.fixedDeltaTime);
            transform.Translate(directionY * speed * Time.fixedDeltaTime);

            
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