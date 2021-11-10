using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMultiplier : AbstractPickup
{
    public override void ReactCollision(in Collider other, in GameMode gameMode)
    {
        OnPickedUp();
        gameMode.timePowerUp();
    }
}
