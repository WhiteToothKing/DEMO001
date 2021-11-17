using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 10;
    public float nspeed = 10;
    private GameObject focalPoint;
    private bool forwardInput;
    private bool backwardsInput;
    private bool rightInput;
    private bool leftInput;

    public bool hasPowerup;
    public GameObject powerupIndicator;

    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup

    private float dashSpeed = 20;
    public GameObject dashEffect;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();


        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        dashEffect.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dashEffect.SetActive(true);

            speed = dashSpeed;

        }
        else
        {

            speed = nspeed;
            dashEffect.SetActive(false);


        }

    }
    private void FixedUpdate()
    {

        PlayerMovement();
    }

    void PlayerInput()
    {
        //Input with Keycodes
        forwardInput = Input.GetKey(KeyCode.W);
        backwardsInput = Input.GetKey(KeyCode.S);
        rightInput = Input.GetKey(KeyCode.D);
        leftInput = Input.GetKey(KeyCode.A);



    }

    void PlayerMovement()
    {

        //Physics movement
        if (forwardInput)
        {
            playerRb.AddForce(focalPoint.transform.forward * speed);
        }


        if (backwardsInput)
        {
            playerRb.AddForce(-focalPoint.transform.forward * speed);
        }

        if (rightInput)
        {
            playerRb.AddForce(focalPoint.transform.right * speed);
        }

        if (leftInput)
        {
            playerRb.AddForce(-focalPoint.transform.right * speed);
        }

    }

    // If Player collides with powerup, activate powerup


    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {


        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}