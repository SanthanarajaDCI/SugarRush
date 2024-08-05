using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    [SerializeField] GameObject engine;
    [SerializeField] GameObject extraPoint;
    [SerializeField] float screenWidthInUnits = 0f;
    [SerializeField] float mouseOffset; //offset for calibrating mouse with screen
    [SerializeField] float padding = 1f; //padding for catcher


    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip catchSound;

     public float tiltSpeed = 100f; // Adjust the tilt speed as needed
    public float maxTiltAngle = 10f; 

    private bool isFacingRight = true;
    public float tiltThreshold = 0.1f; 


    //max and min for x and y on screen
    float xMin;
    float xMax;

    float yMin;
    float yMax;


    [SerializeField]
    float tiltSensitivity = 1f; // Sensitivity for tilt control

    [SerializeField]
    float keyboardSensitivity = 10f; // Sensitivity for keyboard control

    void Start()
    {
        SetUpMoveBoundaries(); //set limit for catcher
    }

    void Update()
    {
      // Move(); //makes move every frame

        newMove();
        TildControl();

        // Get the accelerometer data
        Vector3 acceleration = Input.acceleration;

        // Check if the device is tilted beyond the threshold
        if (acceleration.x > tiltThreshold && !isFacingRight)
        {
            Flip();
        }
        else if (acceleration.x < -tiltThreshold && isFacingRight)
        {
            Flip();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int score = 1; //randomize score points

        if (collision.gameObject.tag == "goldenegg")
        {
            Instantiate(extraPoint, transform.position, Quaternion.identity);
            Debug.Log("Golden Egg");
            engine.GetComponent<Engine>().score += score + 4; //adds score
            engine.GetComponent<Engine>().LiveAdderForScore += score + 4; //adds score to life adder score
        }
        else if (collision.gameObject.tag == "egg")
        {
            Debug.Log("Normal Egg");
            engine.GetComponent<Engine>().score += score; //adds score
            engine.GetComponent<Engine>().LiveAdderForScore += score; //adds score to life adder score
        }

        audioSource.PlayOneShot(catchSound); //play sfx after catch
        Destroy(collision.gameObject); //remove the fruit object

    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main; //assign main camera to variable
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0.2f, 0f)).y;
    }
    private void Move()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * (screenWidthInUnits * 2) - mouseOffset; //convert mouse pos to unity units
        Vector2 paddlePos = new Vector2(mousePosInUnits, transform.position.y); //makes vector2 with x position dependendt from mouse
        paddlePos.x = Mathf.Clamp(paddlePos.x, -7.87f, 7.87f); //make limit for paddle pos
        if (!engine.GetComponent<Engine>().isPaused) //if false makes catcher stay in place
        {
            transform.position = paddlePos; //set catcher pos
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        // Multiply the player's x local scale by -1 to flip
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TildControl(){

        // Get tilt input
        float tiltInput = Input.acceleration.x;
        // Calculate tilt angle
        float targetTiltAngle = tiltInput * maxTiltAngle;

        if (tiltInput > 0)
        {
            // Tilt right
            Debug.Log("Tilting right");
        }
        else if (tiltInput < 0)
        {
            // Tilt left
            Debug.Log("Tilting left");
        }

        // Smoothly tilt the player
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetTiltAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);

    }
     private void newMove()
    {
        // Get keyboard input
        float moveHorizontal = Input.GetAxis("Horizontal") * keyboardSensitivity * Time.deltaTime;

        // Get tilt input
        Vector3 tilt = Input.acceleration;
        float moveTilt = tilt.x * tiltSensitivity;

        // Combine both inputs
        float combinedMovement = moveHorizontal + moveTilt;

        // Calculate new position
        float newXPos = transform.position.x + combinedMovement;

        // Clamp the position within the screen boundaries
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);

        // Update the position
        Vector2 paddlePos = new Vector2(newXPos, transform.position.y);
        if (!engine.GetComponent<Engine>().isPaused) // If false makes catcher stay in place
        {
            transform.position = paddlePos; // Set catcher position
        }
    }
}