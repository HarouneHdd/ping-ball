using System.Collections;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    private ScoreManagement scoreManagement;
    private GameObject ballSpawner;

    private void Awake()
    {
        scoreManagement = GetComponent<ScoreManagement>();

        if (scoreManagement == null)
        {
            Debug.LogError("ScoreManagement COMPONENT is needed!");
        }

        ballSpawner = GameObject.FindGameObjectWithTag("Ball Spawner");

        if (ballSpawner == null)
        {
            Debug.LogError("Object with the TAG 'Ball Spawner' do NOT EXIST!");
        }
    }

    public void UpdateScore(int pointsAmount)
    {
        scoreManagement.AddToScore(pointsAmount);
    }

    public void SetNextBall()
    {

    }
}
