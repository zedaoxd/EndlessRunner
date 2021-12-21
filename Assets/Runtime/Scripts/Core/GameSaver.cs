using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class SaveGameData
{
    public int LastScore { get; set; } = 0;
    public int HighestScore { get; set; } = 0;
    public int TotalCherriesCollected { get; set; } = 0;
    public int TotalPeanutColledted { get; set; } = 0;
}

public class AudioPreferences
{
    public float MainVolume { get; set; } = 1;
    public float MusicVolume { get; set; } = 1;
    public float SFXVolume { get; set; } = 1;
}

public class GameSaver : MonoBehaviour
{
    public SaveGameData CurrentSave { get; private set; }
    public AudioPreferences AudioPreferences { get; private set; }
    private bool IsLoaded => CurrentSave != null && AudioPreferences != null;
    private string PathSaveGame => Application.persistentDataPath + "/save.json";
    private string pathSaveAudioPrefs => Application.persistentDataPath + "/audioPrefs.json";

    public void SaveGame(SaveGameData saveData)
    {
        CurrentSave = saveData;
        Serialize<SaveGameData>(CurrentSave, PathSaveGame);
    }

    public void LoadGame()
    {
        if (IsLoaded)
        {
            return;
        }
        CurrentSave = Deserialize<SaveGameData>(PathSaveGame);
        AudioPreferences = Deserialize<AudioPreferences>(pathSaveAudioPrefs);
    }

    public void SaveAudioPreferences(AudioPreferences preferences)
    {
        AudioPreferences = preferences;
        Serialize<AudioPreferences>(AudioPreferences, pathSaveAudioPrefs);
    }

    public void DeleteAllData()
    {
        File.Delete(pathSaveAudioPrefs);
        File.Delete(PathSaveGame);
        CurrentSave = null;
        AudioPreferences = null;
        LoadGame();
    }

    private void Serialize<T>(T save, string path)
    {
        using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
        using (var sw = new StreamWriter(stream))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            var serializer = new JsonSerializer();
            serializer.Serialize(writer, save);
        }
    }

    private T Deserialize<T>(string path) where T : new()
    {
        using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
        using (var sw = new StreamReader(stream))
        using (var reader = new JsonTextReader(sw))
        {
            var serializer = new JsonSerializer();
            T save = serializer.Deserialize<T>(reader);
            if (save == null)
            {
                save = new T();
            }
            return save;
        }
    }
}