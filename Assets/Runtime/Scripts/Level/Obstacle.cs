using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollisionReact
{
    [SerializeField] private DecorationSpawner[] decorationSpawners;

    private List<ObstacleDecoration> decorations = new List<ObstacleDecoration>();

    public void SpawnDecorations()
    {
        foreach (var decorationSpawner in decorationSpawners)
        {
            decorationSpawner.SpawnDecorations();
            var obstacleDecoration = decorationSpawner.CurrentDecoration.GetComponent<ObstacleDecoration>();
            if (obstacleDecoration != null)
            {
                decorations.Add(obstacleDecoration);
            }
        }
    }

    public virtual void PlayCollisionFeedback(Collider collider)
    {
        ObstacleDecoration decorationHit = FindDecorationForCollider(collider);
        if (decorationHit != null)
        {
            decorationHit.PlayCollisionFeedback();
        }
    }

    private ObstacleDecoration FindDecorationForCollider(Collider collider)
    {
        float minDistX = Mathf.Infinity;
        ObstacleDecoration minDistDecoration = null;
        foreach (var decoration in decorations)
        {
            float decorationXDistToCollider = Mathf.Abs(collider.bounds.center.x - decoration.transform.position.x);
            if (decorationXDistToCollider < minDistX)
            {
                minDistDecoration = decoration;
                minDistX = decorationXDistToCollider;
            }
        }
        return minDistDecoration;
    }

    private static bool IsPlayerInvencible(PlayerController player)
    {
        PowerUpBehaviour_Invencible invencible = player.GetComponentInChildren<PowerUpBehaviour_Invencible>();
        return invencible != null && invencible.IsPowerUpActivate;
    }

    public void ReactCollision(in PlayerCollisionInfo collisionInfo)
    {
        Die(collisionInfo.MyCollider);
        if (!IsPlayerInvencible(collisionInfo.Player))
        {
            collisionInfo.Player.Die();
            collisionInfo.GameMode.OnGameOver();
            collisionInfo.PlayerAnimation.Die();
        } 
    }

    public virtual void Die(Collider collider)
    {
        ObstacleDecoration decoration = FindDecorationForCollider(collider);
        if (decoration != null)
        {
            decoration.PlayCollisionFeedback();
        }
    }
}
