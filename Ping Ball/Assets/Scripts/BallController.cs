using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private GameManagement gameManagement;
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
        gameManagement = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagement>();
        
        if (gameManagement == null)
        {
            Debug.LogError("GameManagement COMPONENT is needed!");
        }

        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) 
        {
            holdDownStartTime = Time.time;
        }

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
            ApplyDraggingToLeft();
        }

        if (launch && !isLaunched)
        {
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        float holdDownTime = Time.time - holdDownStartTime;
        currentForceMag = baseLaunchingForce * holdDownTime;

        if (currentForceMag > 35f)
        {
            currentForceMag = 35f;
        }

        rb.AddForce(Vector3.forward * currentForceMag * Time.deltaTime * 60f, ForceMode.Impulse);
        isLaunched = true;

        launch = false;
    }

    private void ApplyGravity()
    {
        rb.AddForce(Vector3.forward * -1f * pullingForce * Time.deltaTime * 60f);
    }

    private void ApplyDraggingToLeft()
    {
        rb.AddForce(Vector3.right * -1f * Random.Range(minLeftDrag, maxLeftDrag) * Time.deltaTime * 60f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLaunched && other.tag == "Hit Detector" && !destructionModeActivated)
        {
            gameManagement.SetNextBall();
            StartCoroutine(SetObjectDestruction());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isLaunched && other.tag == "Hit Detector" && destructionModeActivated)
        {
            float timer = Time.time - destructionTimerStart;

            // I added this code so that the old Ball won't interfere with the new one
            if (timer >= wtbDestructionOnStay)
            {
                DestroyThisObject();
            }
        }
    }

    private IEnumerator SetObjectDestruction()
    {
        destructionModeActivated = true;
        destructionTimerStart = Time.time;
        yield return new WaitForSeconds(waitTimeBeforeDestruction);
        DestroyThisObject();
    }

    private void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}
