using UnityEngine;

public class PointsRegister : MonoBehaviour
{
    private GameManagement gameManagement;
    private GameObject pointsUI;
    public int pointsAmount;
    private bool collidedWithPlayer = false;

    private void Awake()
    {
        // Getting the GUI sibling object
        int thisIndex = transform.GetSiblingIndex();
        GameObject nextSibling = transform.parent.GetChild(thisIndex + 1).gameObject;

        if (nextSibling.tag == "GUI")
        {
            pointsUI = nextSibling;
        }
        else
        {
            Debug.LogError("There is no sibling object that has the tag 'GUI'!");
        }

        // Connecting with game manager
        gameManagement = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player Col" && !collidedWithPlayer)
        {
            if (pointsAmount == 0)
            {
                Debug.LogWarning("The amount of points is set to 0");
            }

            gameManagement.UpdateScore(pointsAmount);
            collidedWithPlayer = true;
            pointsUI.SetActive(false);
        }
    }

    public void ResetObjectValues()
    {
        collidedWithPlayer = false;
        pointsUI.SetActive(true);
    }
}
