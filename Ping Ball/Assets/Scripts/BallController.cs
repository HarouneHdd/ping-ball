using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float launchingForce = 35f;
    public float pullingForce = 12f;
    public float leftDrag = 1.8f;
    private bool isLaunched = false;
    

    private void awake() {
        
    }

    private void Update() {

    }

    private void FixedUpdate() {

        rb.AddForce(Vector3.forward * -1f * pullingForce * Time.deltaTime * 60f);

        if (rb.velocity.z < 1f && isLaunched)
        {
            rb.AddForce(Vector3.right * -1f * leftDrag * Time.deltaTime * 60f);
        }
        
        if (Input.GetButton("Jump") && !isLaunched)
        {
            rb.AddForce(Vector3.forward * launchingForce * Time.deltaTime * 60f, ForceMode.Impulse);
            isLaunched = true;
        }
    }
}
