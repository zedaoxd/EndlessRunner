using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObstacleDecoration : MonoBehaviour
{
    [SerializeField] private AudioClip collisionAudio;

    private AudioSource audioSource;

    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayCollisionFeedback()
    {
        AudioUtility.PlayAudioCue(AudioSource, collisionAudio);
        Animation animComp = GetComponentInChildren<Animation>();
        if (animComp != null)
        {
            animComp.Play();
        }
    }
}
