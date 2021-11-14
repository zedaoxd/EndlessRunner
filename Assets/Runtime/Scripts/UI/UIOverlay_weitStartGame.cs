using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIOverlay_weitStartGame : UIOverlay
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private MainHUD mainHUD;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI totalCherriesText;
    [SerializeField] private TextMeshProUGUI totalPeanutText;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        highestScoreText.text = $"Highest Score\n{gameMode.CurrentSave.HighestScore}";
        lastScoreText.text = $"Last Score\n{gameMode.CurrentSave.LastScore}";
        totalCherriesText.text = $"{gameMode.CurrentSave.TotalCherriesCollected}";
        totalPeanutText.text = $"{gameMode.CurrentSave.TotalPeanutColledted}";
    }

    public void SettingsWindow()
    {
        mainHUD.ShowOverlay<UIOverlay_Settings>();
    }
}
