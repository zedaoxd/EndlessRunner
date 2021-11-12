using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvencible : AbstractPowerUp
{
    protected override void ActivatePowerUpBehaviour(PlayerController player)
    {
        player.GetComponentInChildren<PowerUpBehaviour_Invencible>().Activate(PowerUpTime);
    }
}
