using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
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

    public void PlayCollisionFeedback(Collider collider)
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
}
