using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float launchingForce = 5f;
    public float pullingForce = 2.5f;
    private bool isLaunched = false;
    

    private void awake() {
        
    }

    private void Update() {

    }

    private void FixedUpdate() {
        if (isLaunched) {
            //rb.velocity = Vector3.forward * -1f * fallMultiplier * Time.deltaTime * 60f;
            rb.AddForce(Vector3.forward * -1f * pullingForce * Time.deltaTime * 60f);
        }
        else
        {
            if (Input.GetButton("Jump"))
            {
                rb.AddForce(Vector3.forward * launchingForce * Time.deltaTime * 60f, ForceMode.Impulse);
                isLaunched = true;
            }
        }
    }
}
