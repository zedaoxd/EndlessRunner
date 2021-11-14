using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MainHUDAudioController))]
public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameMode gameMode;

    [Header("Overlays")]
    [SerializeField] private UIOverlay[] overlays;
    private UIOverlay previousOverlay;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI cherryCountText;
    [SerializeField] private TextMeshProUGUI peanutCountText;

    [Header("Image")]
    [SerializeField] private GameObject imagePowerUp;

    private MainHUDAudioController audioController;

    private void Awake()
    {
        audioController = GetComponent<MainHUDAudioController>();
    }

    private void LateUpdate()
    {
        scoreText.text = $"Score : {gameMode.Score}";
        distanceText.text = $"{Mathf.RoundToInt(player.TravelledDistance)}m";
        cherryCountText.text = $"{gameMode.CherriesPicked}";
        peanutCountText.text = $"{gameMode.PeanutPicked}";
        imagePowerUp.SetActive(gameMode.TemporaryScoreMultipler > 1);
    }

    public void StartGame()
    {
        gameMode.StartGame();
    }

    public void PauseGame()
    {
        ShowOverlay<UIOverlay_Pause>();
        gameMode.PauseGame();
    }

    public void ResumeGame()
    {
        gameMode.ResumeGame();
        ShowOverlay<UIOverlay_InGame>();
    }

    public void ShowOverlay<T>() where T : UIOverlay
    {
        foreach (UIOverlay overlay in overlays)
        {
            if (overlay is T)
            {
                if (previousOverlay != null)
                {
                    previousOverlay.gameObject.SetActive(false);
                }
                overlay.gameObject.SetActive(true);
                previousOverlay = overlay;
            }
        }
    }

    public IEnumerator PlayStartGameCountdown(int countdownSeconds)
    {
        ShowOverlay<UIOverlay_InGame>();
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
