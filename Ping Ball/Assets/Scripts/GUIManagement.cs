using UnityEngine;
using TMPro;

public class GUIManagement : MonoBehaviour
{
    public TextMeshProUGUI chancesUI;
    public TextMeshProUGUI scoreUI;

    public void UpdateScoreGUI(int curScore)
    {
        scoreUI.SetText(curScore.ToString());
    }

    public void UpdateChancesGUI(int chances)
    {
        chancesUI.SetText(chances.ToString());
    }
}
