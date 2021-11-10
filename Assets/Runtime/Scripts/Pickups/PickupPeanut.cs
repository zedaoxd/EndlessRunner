using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPeanut : AbstractPickup
{
    public override void ReactCollision(in Collider other, in GameMode gameMode)
    {
        gameMode.OnPeanutPickedUp();
        OnPickedUp();
    }
}
