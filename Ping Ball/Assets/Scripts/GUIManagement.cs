using UnityEngine;
using TMPro;

public class GUIManagement : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;

    public void UpdateScoreGUI(int curScore)
    {
        Debug.Log("Updating the score ui");
        scoreUI.SetText("Score: " + curScore);
    }
}
