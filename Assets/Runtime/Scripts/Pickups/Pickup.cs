using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour, ICollisionReact
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

    public void ReactCollision(in Collider other,in GameMode gameMode)
    {
        gameMode.OnCherryPickedUp();
        OnPickedUp();
    }
}
