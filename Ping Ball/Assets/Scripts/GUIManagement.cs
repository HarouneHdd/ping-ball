using UnityEngine;
using TMPro;

public class GUIManagement : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;

    public void UpdateScoreGUI(int curScore)
    {
        scoreUI.SetText(curScore.ToString());
    }
}
