using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionReact 
{
    void ReactCollision(in Collider other,in GameMode gameMode);
}
