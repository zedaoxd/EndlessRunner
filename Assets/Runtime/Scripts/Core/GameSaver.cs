using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class SaveGameData
{
    public int LastScore { get; set; }
    public int HighestScore { get; set; }
    public int TotalCherriesCollected { get; set; }
}

public class AudioPreferences
{
    public float MainVolume { get; set; }
    public float MusicVolume { get; set; }
    public float SFXVolume { get; set; }
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
        SerializeSave(CurrentSave, PathSaveGame);
    }

    public void LoadGame()
    {
        if (IsLoaded)
        {
            return;
        }
        CurrentSave = DeserializeSave(PathSaveGame);
        AudioPreferences = DeserializeAudio(pathSaveAudioPrefs);
    }

    public void SaveAudioPreferences(AudioPreferences preferences)
    {
        AudioPreferences = preferences;
        SerializeAudio(AudioPreferences, pathSaveAudioPrefs);
    }

    public void DeleteAllData()
    {
        File.Delete(pathSaveAudioPrefs);
        File.Delete(PathSaveGame);
        CurrentSave = null;
        AudioPreferences = null;
        LoadGame();
    }

    
    private void SerializeSave(SaveGameData saveData, string path)
    {
        using(var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
        using (var sw = new StreamWriter(stream))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            var serializer = new JsonSerializer();
            serializer.Serialize(writer, saveData);
        }
    }

    private void SerializeAudio(AudioPreferences preferences, string path)
    {
        using(var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
        using (var sw = new StreamWriter(stream))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            var serializer = new JsonSerializer();
            serializer.Serialize(writer, preferences);
        }
    }

    public SaveGameData DeserializeSave(string path)
    {
        using(var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
        using (var sw = new StreamReader(stream))
        using (var reader = new JsonTextReader(sw))
        {
            var serializer = new JsonSerializer();  
            SaveGameData save = serializer.Deserialize<SaveGameData>(reader);
            if (save == null)
            {
                save = new SaveGameData()
                {
                    HighestScore = 0,
                    LastScore = 0,
                    TotalCherriesCollected = 0
                };
            }
            return save;
        }
    }

    public AudioPreferences DeserializeAudio(string path)
    {
        using(var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
        using (var sw = new StreamReader(stream))
        using (var reader = new JsonTextReader(sw))
        {
            var serializer = new JsonSerializer();
            AudioPreferences preferences = serializer.Deserialize<AudioPreferences>(reader);
            if (preferences == null)
            {
                preferences = new AudioPreferences();
                preferences.MainVolume = 1;
                preferences.MusicVolume = 1;
                preferences.SFXVolume = 1;
            }
            return preferences;
        }
    }
    
}