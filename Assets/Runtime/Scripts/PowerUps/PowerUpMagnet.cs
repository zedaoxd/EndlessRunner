using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagnet : AbstractPowerUp
{
    protected override void ActivatePowerUpBehaviour(PlayerController player)
    {
        player.GetComponentInChildren<PowerUpBehaviour_Magnet>().Activate(PowerUpTime);
    }
}
