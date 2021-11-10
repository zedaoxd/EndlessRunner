using UnityEngine;


public class Pickup : AbstractPickup
{
    public override void ReactCollision(in Collider other,in GameMode gameMode)
    {
        gameMode.OnCherryPickedUp();
        OnPickedUp();
    }
}
