using System.Collections;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    private ScoreManagement scoreManagement;
    private GUIManagement guiManagement;
    private ArrayList pointsRegisters;
    // Spawning Vars
    private Transform ballSpawnerTransform;
    public GameObject ballInstance;
    private const float waitTimeBeforeNextSpawning = 1.4f;
    // ---
    private const int chances = 4;
    private int chancesRest = 4;
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

        pointsRegisters = new ArrayList();

        ballSpawnerTransform = GameObject.FindGameObjectWithTag("Ball Spawner").transform;

        if (ballSpawnerTransform == null)
        {
            Debug.LogError("Object with the TAG 'Ball Spawner' do NOT EXIST!");
        }

        chancesRest = chances;
        guiManagement.UpdateChancesGUI(chancesRest);
    }

    private void Start()
    {
        GetPointsRegisters();
        StartCoroutine(WaitToSpawnAndUpdateUI());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R) && !gameIsRestarted)
        {
            OnGameRestart();
        }
    }

    private void GetPointsRegisters()
    {
        GameObject[] hitDetectors = GameObject.FindGameObjectsWithTag("Hit Detector");

        foreach (GameObject hitDetect in hitDetectors)
        {
            PointsRegister pr = hitDetect.GetComponent<PointsRegister>();

            if (pr != null)
            {
                pointsRegisters.Add(pr);
            }
        }

        if (pointsRegisters == null || pointsRegisters.Count == 0)
        {
            Debug.LogWarning("There are no objects that register points");
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

        if (chancesRest <= 0)
        {
            gameIsOver = true;
            TriggerGameOver();
            return;
        }

        StartCoroutine(WaitToSpawnAndUpdateUI());
    }

    private void LassenChances()
    {
        if (chancesRest == 0 || gameIsOver || gameIsRestarted)
        {
            return;
        }

        chancesRest--;
        guiManagement.UpdateChancesGUI(chancesRest);
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

    public void OnGameRestart()
    {
        gameIsRestarted = true;

        chancesRest = chances;

        // Reset UI
        scoreManagement.ResetScore();
        guiManagement.UpdateChancesGUI(chancesRest);

        // ---
        foreach (PointsRegister pr in pointsRegisters)
        {
            pr.ResetObjectValues();
        }

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

    }
}
