using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerCollisionInfo
{
    public PlayerController Player;
    public PlayerAnimationController PlayerAnimation;
    public GameMode GameMode;
    public Collider MyCollider;
}


public interface ICollisionReact 
{
    void ReactCollision(in PlayerCollisionInfo collisionInfo);
}
