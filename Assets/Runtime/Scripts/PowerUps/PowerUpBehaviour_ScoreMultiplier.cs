using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour_ScoreMultiplier : PowerUpBehaviour
{
    [SerializeField] private GameMode gameMode;
    private int scoreMultiplier;
    
    public void Activate(int multiplier, float duration)
    {
        scoreMultiplier = multiplier;
        ActivateForDuration(duration);
    }

    protected override void EndBehaviour()
    {
        gameMode.TemporaryScoreMultipler = 1;
    }

    protected override void StartBehaviour()
    {
        gameMode.TemporaryScoreMultipler = scoreMultiplier;
    }

    protected override void UpdateBehaviour()
    {
        
    }
}
