using UnityEngine;

public class ScoreManagement : MonoBehaviour
{
    private GUIManagement guiManagement;
    private int currentScore = 0;

    private void Awake()
    {
        guiManagement = GetComponent<GUIManagement>();
        
        if (guiManagement == null)
        {
            Debug.LogError("GUIManagement COMPONENT is needed!");
        }
    }

    public void AddToScore(int pointsAmount)
    {
        currentScore += pointsAmount;
        guiManagement.UpdateScoreGUI(currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
