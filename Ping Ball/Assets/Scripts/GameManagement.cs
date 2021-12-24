using System.Collections;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    private ScoreManagement scoreManagement;
    private GUIManagement guiManagement;
    // Spawning Vars
    private Transform ballSpawnerTransform;
    public GameObject ballInstance;
    private float waitTimeBeforeNextSpawning = 1.4f;
    // ---
    private int chances = 3;
    private bool gameIsOver = false;

    private void Awake()
    {
        scoreManagement = GetComponent<ScoreManagement>();

        if (scoreManagement == null)
        {
            Debug.LogError("ScoreManagement COMPONENT is needed!");
        }

        guiManagement = GetComponent<GUIManagement>();

        if (guiManagement == null)
        {
            Debug.LogError("GUIManagement COMPONENT is needed!");
        }

        ballSpawnerTransform = GameObject.FindGameObjectWithTag("Ball Spawner").transform;

        if (ballSpawnerTransform == null)
        {
            Debug.LogError("Object with the TAG 'Ball Spawner' do NOT EXIST!");
        }
    }

    private void Start()
    {
        guiManagement.UpdateChancesGUI(chances);
        SpawnNextBall();
    }

    public void UpdateScore(int pointsAmount)
    {
        scoreManagement.AddToScore(pointsAmount);
    }

    public void SetNextEvents()
    {
        if (gameIsOver)
        {
            return;
        }

        if (chances < 0)
        {
            gameIsOver = true;
            TriggerGameOver();
            return;
        }

        UpdateChances();
        StartCoroutine(WaitAndSpawnNextBall());
    }

    public void OnGameRestart()
    {
        Debug.Log("Game RESTARTED!");
    }

    private void UpdateChances()
    {
        if (chances == 0)
        {
            return;
        }

        chances--;
        guiManagement.UpdateChancesGUI(chances);
    }

    private void SpawnNextBall()
    {
        GameObject clone = Instantiate(ballInstance) as GameObject;
        clone.transform.position = ballSpawnerTransform.position;
    }

    private IEnumerator WaitAndSpawnNextBall()
    {
        yield return new WaitForSeconds(waitTimeBeforeNextSpawning);
        SpawnNextBall();
    }

    private void TriggerGameOver()
    {

    }
}
