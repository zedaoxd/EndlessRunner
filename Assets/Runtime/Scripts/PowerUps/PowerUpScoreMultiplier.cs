using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScoreMultiplier : AbstractPowerUp
{
    [SerializeField] private int scoreMultiplier = 2;
    protected override void ActivatePowerUpBehaviour(PlayerController player)
    {
        player.GetComponentInChildren<PowerUpBehaviour_ScoreMultiplier>().Activate(scoreMultiplier, PowerUpTime);
    }
}
