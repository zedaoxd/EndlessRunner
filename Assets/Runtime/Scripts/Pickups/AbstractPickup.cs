using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AbstractPickup : MonoBehaviour, ICollisionReact
{
    [SerializeField] private AudioClip pickupAudio;
    [SerializeField] private GameObject model;

    protected abstract void ExecutePickupBehaviour(in PlayerCollisionInfo collisionInfo);

    protected virtual float LifeTimeAfterPickedUp => pickupAudio.length;

    public void OnPickedUp(in PlayerCollisionInfo collisionInfo)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioUtility.PlayAudioCue(audioSource, pickupAudio);
        model.SetActive(false);
        Destroy(gameObject, pickupAudio.length);
        ExecutePickupBehaviour(collisionInfo);
    }

    public void ReactCollision(in PlayerCollisionInfo collisionInfo)
    {
        OnPickedUp(collisionInfo);
    }
}
