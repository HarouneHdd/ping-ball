using System.Collections;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    private ScoreManagement scoreManagement;

    private void Awake()
    {
        scoreManagement = GetComponent<ScoreManagement>();
    }

    public void UpdateScore(int pointsAmount)
    {

    }

    public void SetNextBall()
    {

    }
}
