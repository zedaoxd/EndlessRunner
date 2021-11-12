using UnityEngine;


public class Pickup : AbstractPickup
{
    protected override void ExecutePickupBehaviour(in PlayerCollisionInfo collisionInfo)
    {
        collisionInfo.GameMode.OnCherryPickedUp();
    }
}
