using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private GameManagement gameManagement;
    private SoundManagement soundManagement;
    private Rigidbody rb;
    // Launching Vars
    private float baseLaunchingForce = 22f;
    private float currentForceMag;
    private bool launch = false;
    private bool isLaunched = false;
    private float holdDownStartTime;
    // Gravity Vars
    private float pullingForce = 12f;
    // DTL Effect Vars
    private float minLeftDrag = 0.4f;
    private float maxLeftDrag = 1.8f;
    // Destruction Vars
    private float waitTimeBeforeDestruction = 3.2f;
    private float wtbDestructionOnStay = 1.2f;
    private float destructionTimerStart;
    private bool destructionModeActivated = false;
    

    private void Awake() {
        GameObject gameManager = GameObject.FindGameObjectWithTag("Game Manager");

        // get the game manager to manage the all game 
        gameManagement = gameManager.GetComponent<GameManagement>();
        
        if (gameManagement == null)
        {
            Debug.LogError("GameManagement COMPONENT is needed!");
        }

        // get sound manager to manage the all game 
        soundManagement = gameManager.GetComponent<SoundManagement>();

        if (soundManagement == null)
        {
            Debug.LogError("SoundManagement COMPONENT is needed!");
        }

        // get the rigid the body
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) 
        {
            // start timer when clicking spacebar the first time 
            holdDownStartTime = Time.time;
        }

        // see if the spacebar is released
        if (Input.GetButtonUp("Jump"))
        {
            launch = true;
        }
    }

    private void FixedUpdate() {

        if (isLaunched)
        {
            ApplyGravity();
        }

        if (rb.velocity.z < 1f && rb.velocity.z > -0.6f && isLaunched)
        {
            // after certain amount of velocity value start draging to the left 
            ApplyDraggingToLeft();
        }

        if (launch && !isLaunched)
        {
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        // calculate holdtime between now and the time that the spacebar been clicked
        float holdDownTime = Time.time - holdDownStartTime;

        // calculate the force depending on the hold time
        currentForceMag = baseLaunchingForce * holdDownTime;

        // set maximum force
        if (currentForceMag > 35f)
        {
            currentForceMag = 35f;
        }

        // applying force to the rigid body (ForceMode.Impulsive to apply the force once)
        rb.AddForce(Vector3.forward * currentForceMag * Time.deltaTime * 60f, ForceMode.Impulse);
        // launch the sound when hitting the ball 
        soundManagement.PlayLaunchingSound();
        isLaunched = true;

        launch = false;
    }

    private void ApplyGravity()
    {
        // applying gravity so the ball stays on the table
        rb.AddForce(Vector3.forward * -1f * pullingForce * Time.deltaTime * 60f);
    }

    private void ApplyDraggingToLeft()
    {
        // so the ball drags a bit to the left after entring one of the scores 
        rb.AddForce(Vector3.right * -1f * Random.Range(minLeftDrag, maxLeftDrag) * Time.deltaTime * 60f);
    }

    // triger when the object collides with the ball
    private void OnTriggerEnter(Collider other)
    {
        // get the object that the ball collided with (25, 50, 75, 100)
        if (isLaunched && other.tag == "Hit Detector" && !destructionModeActivated)
        {
            // call the next event (game management class)
            gameManagement.SetNextEvents();
            // unity funtion (parallel processing)
            StartCoroutine(SetObjectDestruction());
        }
    }

    // this function to prevent the old ball interferring with the new ball object (collider on the start)
    private void OnTriggerStay(Collider other)
    {
        if (isLaunched && other.tag == "Hit Detector" && destructionModeActivated)
        {
            // check timer to see if the object stayed in the collider for more than 1.2 secondes 
            float timer = Time.time - destructionTimerStart;

            // I added this code so that the old Ball won't interfere with the new one
            if (timer >= wtbDestructionOnStay)
            {
                DestroyThisObject();
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Play the sound effect when the Ball hits another collider
        soundManagement.PlayGettingHitSound();
    }

    private IEnumerator SetObjectDestruction()
    {
        destructionModeActivated = true;
        destructionTimerStart = Time.time;
        // wait 3.2 seconds before destoying the object without interferring the other fucntion in the  script 
        yield return new WaitForSeconds(waitTimeBeforeDestruction);
        DestroyThisObject();
    }

    private void DestroyThisObject()
    {
        // destroy the object 
        Destroy(gameObject);
    }
}
