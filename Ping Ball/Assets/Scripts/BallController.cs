using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    // Launching Vars
    private float baseLaunchingForce = 20f;
    private float currentForceMag;
    private bool launch = false;
    private bool isLaunched = false;
    private float holdDownStartTime;
    // Gravity Vars
    private float pullingForce = 12f;
    // DTL Effect Vars
    private float minLeftDrag = 0.4f;
    private float maxLeftDrag = 1.8f;
    

    private void Awake() {
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
}
