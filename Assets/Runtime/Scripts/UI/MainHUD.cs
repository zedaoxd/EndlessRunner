using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MainHUDAudioController))]
public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameMode gameMode;

    [Header("Overlays")]
    [SerializeField] private GameObject startGameOverlay;
    [SerializeField] private GameObject hudOverlay;
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject settingsOverlay;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private TextMeshProUGUI cherryCountText;

    private MainHUDAudioController audioController;

    private void Awake()
    {
        ShowHudOverlay();
        audioController = GetComponent<MainHUDAudioController>();
    }

    private void LateUpdate()
    {
        scoreText.text = $"Score : {gameMode.Score}";
        distanceText.text = $"{Mathf.RoundToInt(player.TravelledDistance)}m";
        cherryCountText.text = $"{gameMode.CherriesPicked}";
    }

    public void StartGame()
    {
        gameMode.StartGame();
    }

    public void PauseGame()
    {
        ShowPauseOverlay();
        gameMode.PauseGame();
    }

    public void ResumeGame()
    {
        gameMode.ResumeGame();
        ShowHudOverlay();
    }

    public void ShowStartGameOverlay()
    {
        startGameOverlay.SetActive(true);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(false);
        settingsOverlay.SetActive(false);
    }

    public void ShowHudOverlay()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(true);
        settingsOverlay.SetActive(false);
    }

    public void ShowPauseOverlay()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(true);
        hudOverlay.SetActive(false);
        settingsOverlay.SetActive(false);
    }

    public void ShowSettingsOverlay()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(false);
        settingsOverlay.SetActive(true);
    }

    public IEnumerator PlayStartGameCountdown(int countdownSeconds)
    {
        ShowHudOverlay();
        countdownText.gameObject.SetActive(false);

        if (countdownSeconds == 0)
        {
            yield break;
        }

        float timeToStart = Time.time + countdownSeconds;
        yield return null;
        countdownText.gameObject.SetActive(true);
        int previousRemainingTimeInt = countdownSeconds;
        while (Time.time <= timeToStart)
        {
            float remainingTime = timeToStart - Time.time;
            int remainingTimeInt = Mathf.FloorToInt(remainingTime);
            countdownText.text = (remainingTimeInt + 1).ToString();

            if (previousRemainingTimeInt != remainingTimeInt)
            {
                audioController.PlayCountdownAudio();
            }

            previousRemainingTimeInt = remainingTimeInt;

            float percent = remainingTime - remainingTimeInt;
            countdownText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, percent);
            yield return null;
        }

        audioController.PlayCountdownFinishedAudio();

        countdownText.gameObject.SetActive(false);
    }
}
