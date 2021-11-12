using System.Collections;
using UnityEngine;

public abstract class AbstractPowerUp : AbstractPickup
{
    [SerializeField] protected float powerUpTime = 30;

    protected float PowerUpTime => powerUpTime;

    protected override sealed void ExecutePickupBehaviour(in PlayerCollisionInfo collisionInfo)
    {
        ActivatePowerUpBehaviour(collisionInfo.Player);
    }

    protected abstract void ActivatePowerUpBehaviour(PlayerController player);
}
