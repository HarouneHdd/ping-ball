using System.Collections;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    private ScoreManagement scoreManagement;
    private GUIManagement guiManagement;
    // Spawning Vars
    private Transform ballSpawnerTransform;
    public GameObject ballInstance;
    private const float waitTimeBeforeNextSpawning = 1.4f;
    // ---
    private const int chances = 4;
    private int chancesLeft = 4;
    // Upon Restart
    private bool gameIsRestarted = false;
    // Upon Game Over
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

        chancesLeft = chances;
        guiManagement.UpdateChancesGUI(chancesLeft);
    }

    private void Start()
    {
        StartCoroutine(WaitToSpawnAndUpdateUI());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R) && !gameIsRestarted && !gameIsOver)
        {
            RestartGame();
        }

        if (gameIsOver && Input.GetButtonUp("Jump"))
        {
            RestartGame();
        }

        // Press Escape to get out of the game in the build version
        if (Input.GetButtonUp("Escape"))
        {
            Application.Quit();
        }
    }

    public void UpdateScore(int pointsAmount)
    {
        scoreManagement.AddToScore(pointsAmount);
    }

    public void SetNextEvents()
    {
        if (gameIsOver || gameIsRestarted)
        {
            return;
        }

        if (chancesLeft <= 0)
        {
            gameIsOver = true;
            TriggerGameOver();
            return;
        }

        StartCoroutine(WaitToSpawnAndUpdateUI());
    }

    private void LassenChances()
    {
        if (chancesLeft == 0 || gameIsOver || gameIsRestarted)
        {
            return;
        }

        chancesLeft--;
        guiManagement.UpdateChancesGUI(chancesLeft);
    }

    private void SpawnNextBall()
    {
        GameObject clone = Instantiate(ballInstance) as GameObject;
        clone.transform.position = ballSpawnerTransform.position;
    }

    private IEnumerator WaitToSpawnAndUpdateUI()
    {
        yield return new WaitForSeconds(waitTimeBeforeNextSpawning);
        LassenChances();
        SpawnNextBall();
    }

    public void RestartGame()
    {
        gameIsRestarted = true;

        chancesLeft = chances;

        // Reset UI
        scoreManagement.ResetScore();
        guiManagement.OnRestart(chances);
        
        // Here we stop all coroutines to prevent any ball from respawning upon restarting the game
        StopAllCoroutines();

        // Delete all existing balls
        GameObject[] playerBalls = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pBall in playerBalls)
        {
            Destroy(pBall);
        }

        // Setting the rest of variables back
        gameIsOver = false;
        gameIsRestarted = false;

        // Initiating the next ball
        StartCoroutine(WaitToSpawnAndUpdateUI());
    }

    private void TriggerGameOver()
    {
        guiManagement.OnGameOver();
    }
}
