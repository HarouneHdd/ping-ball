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
        // passing points amount to the GUIManagement component (GUIManagement class)
        currentScore += pointsAmount;
        guiManagement.UpdateScoreGUI(currentScore);
    }

    public void ResetScore()
    {
        // update UI on the reset
        currentScore = 0;
        guiManagement.UpdateScoreGUI(currentScore);
    }
}
