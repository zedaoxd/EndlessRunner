using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject powerUpParticles;
    private float endTime;

    public bool IsPowerUpActivate => Time.time < endTime;

    private void Start() 
    {
        powerUpParticles.gameObject.SetActive(false);
    }

    protected void ActivateForDuration(float duration)
    {
        bool wasActive = IsPowerUpActivate;
        endTime = Time.time + duration;
        if (!wasActive)
        {
            StartCoroutine(UpdateBehaviourCourotine());
        }
    }

    private IEnumerator UpdateBehaviourCourotine()
    {
        StartBehaviour();
        powerUpParticles.gameObject.SetActive(true);
        while(IsPowerUpActivate)
        {
            UpdateBehaviour();
            yield return null;
        }
        powerUpParticles.gameObject.SetActive(false);
        EndBehaviour();
    }

    protected abstract void StartBehaviour();
    protected abstract void UpdateBehaviour();
    protected abstract void EndBehaviour();
}
