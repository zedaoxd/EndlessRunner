using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AbstractPickup : MonoBehaviour, ICollisionReact
{
    [SerializeField] private AudioClip pickupAudio;
    [SerializeField] private GameObject model;
    public void OnPickedUp()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioUtility.PlayAudioCue(audioSource, pickupAudio);

        model.SetActive(false);
        Destroy(gameObject, pickupAudio.length);
    }

    public abstract void ReactCollision(in Collider other, in GameMode gameMode);
}
