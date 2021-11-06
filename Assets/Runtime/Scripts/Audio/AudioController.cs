using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    private const string MainVolumeParameter = "MainVolume";
    private const string MusicVolumeParameter = "MusicVolume";
    private const string SFXVolumeParameter = "SFXVolume";

    private const int minVolumeDb = -60;
    private const int maxVolumeDb = 0;

    [SerializeField] private GameSaver gameSaver;
    [SerializeField] private AudioMixer mixer;

    public float MainVolume
    {
        get => GetMixerParameter(MainVolumeParameter);
        set => SetMixerVolumeParameter(MainVolumeParameter, value);
    }
    public float MusicVolume
    {
        get => GetMixerParameter(MusicVolumeParameter);
        set => SetMixerVolumeParameter(MusicVolumeParameter, value);
    }
    public float SFXVolume
    {
        get => GetMixerParameter(SFXVolumeParameter);
        set => SetMixerVolumeParameter(SFXVolumeParameter, value);
    }

    private void Start()
    {
        LoadAudioPreferences();
    }

    private void LoadAudioPreferences()
    {
        gameSaver.LoadGame();
        MainVolume = gameSaver.AudioPreferences.MainVolume;
        MusicVolume = gameSaver.AudioPreferences.MusicVolume;
        SFXVolume = gameSaver.AudioPreferences.SFXVolume;
    }

    public void SaveAudioPreferences()
    {
        gameSaver.SaveAudioPreferences(new AudioPreferences
        {
            MainVolume = MainVolume,
            MusicVolume = MusicVolume,
            SFXVolume = SFXVolume,
        });
    }

    private void SetMixerVolumeParameter(string key, float volume)
    {
        float volumeValue = Mathf.Lerp(minVolumeDb, maxVolumeDb, volume);
        mixer.SetFloat(key, volumeValue);
    }

    private float GetMixerParameter(string key)
    {
        if (mixer.GetFloat(key, out var value))
        {
            return Mathf.InverseLerp(minVolumeDb, maxVolumeDb, value);
        }
        return 1;
    }
}
