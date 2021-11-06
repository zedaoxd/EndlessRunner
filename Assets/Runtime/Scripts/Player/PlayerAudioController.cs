using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip rollSound;

    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;

    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayJumpSound()
    {
        Play(jumpSound);
    }

    public void PlayRollSound()
    {
        Play(rollSound);
    }

    public void PlayDeathSound()
    {
        Play(deathSound);
    }

    private void Play(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);
    }
}
