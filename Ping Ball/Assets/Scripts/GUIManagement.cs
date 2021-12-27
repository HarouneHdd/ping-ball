using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManagement : MonoBehaviour
{
    private SoundManagement soundManagement;
    public TextMeshProUGUI chancesUI;
    public TextMeshProUGUI scoreUI;
    public GameObject restartSection;
    public GameObject commentSection;
    public GameObject gameOverUI;
    // Audio Related Vars
    public Slider musicSlider;
    public Slider soundEffectsSlider;

    private void Awake()
    {
        soundManagement = GetComponent<SoundManagement>();    
    }

    private void Start()
    {
        // Just in case they are not active
        restartSection.SetActive(true);
        commentSection.SetActive(true);
        // Just in case it is active
        gameOverUI.SetActive(false);
    }

    public void UpdateScoreGUI(int curScore)
    {
        scoreUI.SetText(curScore.ToString());
    }

    public void UpdateChancesGUI(int chances)
    {
        chancesUI.SetText(chances.ToString());
    }

    public void OnGameOver()
    {
        restartSection.SetActive(false);
        commentSection.SetActive(false);

        gameOverUI.SetActive(true);
    }

    public void OnRestart(int chances)
    {
        UpdateChancesGUI(chances);

        gameOverUI.SetActive(false);

        restartSection.SetActive(true);
        commentSection.SetActive(true);
    }

    public void OnMusicSliderChange()
    {
        // get trigger by the music slider
        soundManagement.SetMusicVolume(musicSlider.value);
    }

    public void OnSFXSliderChange()
    {
        // trigger by sound effect slider
        soundManagement.SetSoundEffectsVolume(soundEffectsSlider.value);
    }
}
