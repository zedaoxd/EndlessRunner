using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPeanut : AbstractPickup
{
    protected override void ExecutePickupBehaviour(in PlayerCollisionInfo collisionInfo)
    {
        collisionInfo.GameMode.OnPeanutPickedUp();
    }
}
