using UnityEngine;

public class PointsRegister : MonoBehaviour
{
    private GameManagement gameManagement;
    public int pointsAmount;
    private bool collidingWithPlayer = false;

    private void Awake()
    {
        // Connecting with game manager
        gameManagement = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagement>();

        if (gameManagement == null)
        {
            Debug.LogError("GameManagement COMPONENT is needed!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check the points amount of the object
        if (other.tag == "Player Col" && !collidingWithPlayer)
        {
            if (pointsAmount == 0)
            {
                // notify the developer about the point amount
                Debug.LogWarning("The amount of points is set to 0");
            }

            collidingWithPlayer = true;
            // passing points amount to the gameManagement component (gameManagement class)
            gameManagement.UpdateScore(pointsAmount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        collidingWithPlayer = false;
    }
}
