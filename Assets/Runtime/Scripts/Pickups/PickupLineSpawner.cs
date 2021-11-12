using System.Collections.Generic;
using UnityEngine;

public class PickupLineSpawner : MonoBehaviour
{
    [Header("Pikups")]
    [SerializeField] private AbstractPickup regularPickupPrefab;
    [SerializeField] private AbstractPickup rarePickupPrefab;
    [Range(0,1)]
    [SerializeField] private float rarePickupChance;
    [Space]
    [SerializeField] private List<AbstractPowerUp> powerUps;
    [SerializeField] private float PowerUpChance = 0.1f;
    [Space]
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private float spaceBetweenPickups = 0.5f;

    public void SpawnPickups(Vector3[] skipPositions)
    {
        if (Random.value <= PowerUpChance)
        {
            SpawnPowerUp(skipPositions);
        }
        else
        {
            SpawnPickupLine(skipPositions);
        }
    }

    private void SpawnPickupLine(Vector3[] skipPositions)
    {
        Vector3 currentSpawnPosition = start.position;
        while (currentSpawnPosition.z < end.position.z)
        {
            if (!ShouldSkipPosition(currentSpawnPosition, skipPositions))
            {
                AbstractPickup pickupPrefab = ChoosePickupPrefab();
                AbstractPickup pickup = Instantiate(pickupPrefab, currentSpawnPosition, Quaternion.identity, transform);
            }
            currentSpawnPosition.z += spaceBetweenPickups;
        }
    }

    private void SpawnPowerUp(Vector3[] skipPositions)
    {
        Vector3 currentSpawnPosition = start.position;
        bool powerUpSpwanded = false;
        while (currentSpawnPosition.z < end.position.z  && !powerUpSpwanded)
        {
            if (!ShouldSkipPosition(currentSpawnPosition, skipPositions))
            {
                AbstractPowerUp powerUpPrefab = ChoosePowerUpPrefab();
                Vector3 spawnPosition = start.position;
                Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity, transform);
                powerUpSpwanded = true;
            }
        }
    }

    private AbstractPickup ChoosePickupPrefab()
    {
        return Random.value <= rarePickupChance ? rarePickupPrefab : regularPickupPrefab;
    }

    private AbstractPowerUp ChoosePowerUpPrefab()
    {
        return powerUps[Random.Range(0, powerUps.Count)];
    }

    private bool ShouldSkipPosition(Vector3 currentSpawnPosition, Vector3[] skipPositions)
    {
        foreach (var skipPosition in skipPositions)
        {
            float skipStart = skipPosition.z - spaceBetweenPickups * 0.5f;
            float skipEnd = skipPosition.z + spaceBetweenPickups * 0.5f;

            if (currentSpawnPosition.z >= skipStart && currentSpawnPosition.z <= skipEnd)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Vector3 currentSpawnPosition = start.position;
        while (currentSpawnPosition.z < end.position.z)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(currentSpawnPosition, Vector3.one);
            currentSpawnPosition.z += spaceBetweenPickups;
        }
    }
}
